# API Dokumentation

## Skapa kategori (`CreateCategory`)

**Endpoint:** `/api/category/CreateCategory`  
**Metod:** `POST`

### Request Body

```json

{
    "CategoryName": "string"
}

````

## Hämta alla kategorier (`GetAllCategories`)

**Endpoint:** `/api/categories/GetAllCategories`  
**Metod:** `GET`

```json
[
    {
      "categoryName": "string"
      "categoryId": 123
    }
]

````
## Hämta en kategori (`GetOneCategory`)

**Endpoint:** `/api/category/GetOneCategory/{id}`  
**Metod:** `GET`

```json

{
    "categoryName": "string",
    "categoryId": 123
}

````

## Uppdatera en kategori (`UpdateCategory`)

**Endpoint:** `/api/category/UpdateCategory`  
**Metod:** `PUT`

### Request Body

```json

{
    "categoryId": 123,
    "categoryName": "string"
}

````

## Ta bort en kategori (`DeleteCategory`)

**Endpoint:** `/api/category/DeleteCategory/{id}`  
**Metod:** `DELETE`
