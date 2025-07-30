# API Dokumentation

## Skapa Order (`CreateOrder`)

**Endpoint:** `/api/orders/create`  
**Metod:** `POST`

### Request Body

```json
{
    "totalAmount": "string",
    "paymentMethod": "string",
    "shipmentMethod": "string",
    "orderCustomer": {
        "customerName": "string",
        "customerEmail": "string",
        "customerPhone": "string"
    },
    "orderAddress": {
        "address": "string",
        "city": "string",
        "postalCode": "string",
        "country": "string"
    },
    "orderProducts": [
        {
            "articleNumber": "string",
            "productName": "string",
            "unitPrice": "string",
            "quantity": "string",
            "color": "string",
            "size": "string"
        }
    ]
}

````

## Hämta Alla Orders (`GetAllOrders`)

**Endpoint:** `/api/orders/getallorders`  
**Metod:** `GET`

### Request Body

```json
[
    {
        "orderId": "int",
        "orderDate": "datetime",
        "totalAmount": "string",
        "paymentMethod": "string",
        "shipmentMethod": "string",
        "orderStatus": "string",
        "orderCustomer": {
            "customerName": "string",
            "customerEmail": "string",
            "customerPhone": "string"
        },
        "orderAddress": {
            "address": "string",
            "city": "string",
            "postalCode": "string",
            "country": "string"
        },
        "orderProducts": [
            {
                "articleNumber": "string",
                "productName": "string",
                "unitPrice": "string",
                "quantity": "string",
                "color": "string",
                "size": "string"
            }
        ]
    }
]

````
## Hämta En Order (`GetOneOrder`)

**Endpoint:** `/api/orders/GetOneOrder/{id}`  
**Metod:** `GET`

### Request Body

```json
[
    {
        "orderId": "int",
        "orderDate": "datetime",
        "totalAmount": "string",
        "paymentMethod": "string",
        "shipmentMethod": "string",
        "orderStatus": "string",
        "orderCustomer": {
            "customerName": "string",
            "customerEmail": "string",
            "customerPhone": "string"
        },
        "orderAddress": {
            "address": "string",
            "city": "string",
            "postalCode": "string",
            "country": "string"
        },
        "orderProducts": [
            {
                "articleNumber": "string",
                "productName": "string",
                "unitPrice": "string",
                "quantity": "string",
                "color": "string",
                "size": "string"
            }
        ]
    }
]

````

## Uppdatera En Order (`UpdateOrder`)

**Endpoint:** `/api/orders/UodateOrder`  
**Metod:** `POST`

### Request Body

```json
[
    {
        "orderId": "int",
        "totalAmount": "string",
        "paymentMethod": "string",
        "shipmentMethod": "string",
        "orderStatus": "string",
        "orderCustomer": {
            "customerName": "string",
            "customerEmail": "string",
            "customerPhone": "string"
        },
        "orderAddress": {
            "address": "string",
            "city": "string",
            "postalCode": "string",
            "country": "string"
        },
        "orderProducts": [
            {
                "articleNumber": "string",
                "productName": "string",
                "unitPrice": "string",
                "quantity": "string",
                "color": "string",
                "size": "string"
            }
        ]
    }
]

````

## Radera En Order (`DeleteOrder`)

**Endpoint:** `/api/orders/DeleteOrder/{id}`  
**Metod:** `DELETE`
