pipeline {
    agent any

    options {
        timestamps()
        buildDiscarder(logRotator(numToKeepStr: '10'))
        disableConcurrentBuilds()
    }

    parameters {
        string(name: 'DOCKER_REGISTRY', defaultValue: 'docker.io', description: 'Docker registry host, for example docker.io or registry.example.com')
        string(name: 'DOCKER_REPOSITORY', defaultValue: 'your-dockerhub-user/tourismbooking', description: 'Docker image repository')
        choice(name: 'DEPLOY_ENVIRONMENT', choices: ['dev', 'staging', 'production'], description: 'Target environment for CD deployment')
        booleanParam(name: 'PUSH_IMAGE', defaultValue: false, description: 'Push the Docker image to the configured registry')
        booleanParam(name: 'DEPLOY_IMAGE', defaultValue: false, description: 'Run the deployment stage after the image is built')
    }

    environment {
        SOLUTION_FILE = 'TourismBooking.sln'
        PROJECT_FILE = 'TourismBooking/TourismBooking.csproj'
        CONFIGURATION = 'Release'
        IMAGE_NAME = "${params.DOCKER_REGISTRY}/${params.DOCKER_REPOSITORY}"
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DOTNET_NOLOGO = 'true'
    }

    stages {
        stage('CI: Checkout') {
            steps {
                checkout scm
                script {
                    def shortCommit = env.GIT_COMMIT ? env.GIT_COMMIT.take(7) : 'local'
                    env.IMAGE_TAG = "${env.BUILD_NUMBER}-${shortCommit}"
                    env.FULL_IMAGE_NAME = "${env.IMAGE_NAME}:${env.IMAGE_TAG}"
                    env.LATEST_IMAGE_NAME = "${env.IMAGE_NAME}:latest"
                }
                sh 'dotnet --info'
                sh 'docker version'
            }
        }

        stage('CI: Restore') {
            steps {
                sh 'dotnet restore "$SOLUTION_FILE"'
            }
        }

        stage('CI: Build') {
            steps {
                sh 'dotnet build "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-restore'
            }
        }

        stage('CI: Test') {
            steps {
                script {
                    def testProjects = sh(
                        script: "find . -name '*Test*.csproj' -o -name '*Tests*.csproj'",
                        returnStdout: true
                    ).trim()

                    if (testProjects) {
                        sh 'dotnet test "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-build'
                    } else {
                        echo 'No test project was found, so the test stage was skipped.'
                    }
                }
            }
        }

        stage('CI: Publish Artifact') {
            steps {
                sh 'dotnet publish "$PROJECT_FILE" --configuration "$CONFIGURATION" --no-build --output publish'
                archiveArtifacts artifacts: 'publish/**', fingerprint: true
            }
        }

        stage('CD: Build Docker Image') {
            steps {
                sh 'docker build --pull --tag "$FULL_IMAGE_NAME" --tag "$LATEST_IMAGE_NAME" .'
            }
        }

        stage('CD: Push Docker Image') {
            when {
                expression { return params.PUSH_IMAGE }
            }
            steps {
                withCredentials([usernamePassword(credentialsId: 'docker-registry-credentials', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                    sh '''
                        echo "$DOCKER_PASSWORD" | docker login "$DOCKER_REGISTRY" --username "$DOCKER_USERNAME" --password-stdin
                        docker push "$FULL_IMAGE_NAME"
                        docker push "$LATEST_IMAGE_NAME"
                    '''
                }
            }
        }

        stage('CD: Deploy') {
            when {
                expression { return params.DEPLOY_IMAGE }
            }
            steps {
                echo "Deploying ${env.FULL_IMAGE_NAME} to ${params.DEPLOY_ENVIRONMENT}"
                sh '''
                    # Replace this block with your deployment command.
                    # Examples:
                    # kubectl set image deployment/tourismbooking tourismbooking="$FULL_IMAGE_NAME" --namespace "$DEPLOY_ENVIRONMENT"
                    # docker compose pull && docker compose up -d
                    echo "Deployment hook is ready for $FULL_IMAGE_NAME"
                '''
            }
        }
    }

    post {
        success {
            echo "Pipeline completed successfully. Built image: ${env.FULL_IMAGE_NAME}"
        }
        failure {
            echo 'Pipeline failed. Check the Jenkins console log for the failing stage.'
        }
        always {
            sh '''
                docker logout "$DOCKER_REGISTRY" || true
            '''
          //  deleteDir()
        }
    }
}
