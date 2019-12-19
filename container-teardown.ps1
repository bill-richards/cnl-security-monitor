cls

# Stop and remove the RabbitMQ container
docker kill rabbitmq-server
docker rm rabbitmq-server

# delete the RabbitMQ image
docker image rm bitnami/rabbitmq

# Remove the network
docker network rm cnl-demo

docker ps -a
docker images