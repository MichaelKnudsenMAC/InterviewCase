docker pull rabbitmq:3.11-management
docker run -d --hostname local-qb-rabbit --name qb-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management