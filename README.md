
## Description

This project is created for my medium article about transactional outbox pattern.

You can access it here : https://medium.com/@karaoglufurkan/transactional-outbox-pattern-neden-ve-nasil-fbead861e3c0

  

## Dependencies

* Docker

  

## Run The Project

On the root directory:

```

docker-compose up

```

  

## Accessing to SQL Server DB

You can access SQL Server DB on localhost:1433 with sa password: PassWord1234

  

## Accessing to the RabbitMQ Management Page

  

url: localhost:15672

user: guest

password: guest

  

## Using the APIs

  

### Order API

root-url: localhost:5001

  

Get Orders:

```

HTTP GET

url: root-url/orders

```

Create Order

```

HTTP POST

url: root-url/orders,

body:

{

"userId" : int,

"totalPrice": decimal,

"email" : string

}

```

Cancel Order

```

HTTP POST

url: root-url/orders/{orderId}/cancel,

```

### Mail API

root-url: localhost:5002

  

Get MailQueue:

```

HTTP GET

url: root-url/mailqueue

```