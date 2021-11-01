## Description
This project is created for my medium article about transactional outbox pattern.
 You can access it here : [project-link]

## Dependencies
* Docker

## Run The Project
On the root directory:
```
docker-compose up
```

## How To Use The APIs

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
"UserId" : int,
"TotalPrice": decimal,
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

Get MailQueues:
```
HTTP GET 
url: root-url/mailqueue
```