# API Dokumentation För Rika_AccountProvider

## Create User (`CreateUser`)

**Endpoint:** `https://accountprovider.azurewebsites.net/api/CreateUser?code={apiKey}`  
**Metod:** `POST`

### Request Body

```json
{
  "FirstName": "string",
  "LastName": "string",
  "Email": "string",
  "Password": "string"
}

````

## Get All Users (`GetAllUsers`)

**Endpoint:** `https://accountprovider.azurewebsites.net/api/GetUsers?code={apiKey}`  
**Metod:** `GET`


## Get One User (`GetOneUserAsync`)

**Endpoint:** `https://accountprovider.azurewebsites.net/api/GetOneUserAsync?UserId={userId}&code={apiKey}`  
**Metod:** `GET`

To get a user send a GET http request with query param "UserId".

## Update User (`UpdateUser`)

**Endpoint:** `https://accountprovider.azurewebsites.net/api/UpdateUser?code={apiKey}`
**Metod:** `PUT`

### Request Body

```json
{
  "UserId": "string",
  "FirstName": "string",
  "LastName": "string",
  "Email": "string",
  "PhoneNumber": "string",
  "ProfileImageUrl": "string",
  "Age": 0
}

````

## Delete User (`DeleteUser`)

**Endpoint:** `https://accountprovider.azurewebsites.net/api/DeleteUser?UserId={userId}&code={apiKey}`  
**Metod:** `DELETE`
