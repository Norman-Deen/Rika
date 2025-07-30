# GetOne

## GetOne with Query params
To get a single product call the api with a GET http request with a query param named "ProductId".

Example:
/api/GetOne?ProductId=PRODUCTID

If there is no product in the DB with the selected ID the api will return "204 - no content".

# GetAll

## Get a list of all products
To get the full list of all products in the products DB use a GET http request.

## Get a filtered list of all products
To get a filtered list of products call the same endpoint but with a POST http request.

Schema for filtering products (the list will be filtered in the same order):

    {
        "ProductName": "",
        "ProductCategory" : 1,
        "ProductColor" : 1,
        "ProductSize" : 1
    }



The list of products schema:

    {
        "id": "STRING_1",
        "productName": "T-Shirt1",
        "productPrice": 99.00,
        "productSalePrice": 79.00,
        "productDescription": "Great T-Shirt",
        "productCategoryId": 1,
        "productSizeId": 3,
        "productSize": {
            "id": 3,
            "productSizeName": "M"
        },
        "productColorId": 4,
        "productColor": {
            "id": 4,
            "colorName": "Yellow"
        }
    },
    {
        "id": "STRING_2",
        "productName": "T-Shirt2",
        "productPrice": 99.00,
        "productSalePrice": 69.00,
        "productDescription": "Greater T-Shirt",
        "productCategoryId": 1,
        "productSizeId": 4,
        "productSize": {
            "id": 4,
            "productSizeName": "L"
        },
        "productColorId": 3,
        "productColor": {
            "id": 3,
            "colorName": "Red"
        }
    }



# Create

## Create a product

To create a product and save to products DB use a POST http request with schema as below:

    {
        "ProductName": "ExampleProduct",
        "ProductPrice": 0.00,
        "ProductSalePrice": 0.00,
        "ProductDescription": "An example product.",
        "ProductCategoryId": 1,
        "ProductSizeId": 2,
        "ProductColorId": 3
    }


# Delete

## Delete a product

To delete a product use a POST http request with schema as below:

    {
        "id": "STRING_ID"
    }


    
# Update

## Update a product

To update a product and save to products DB use a POST http request with schema as below:

    {
        "id": "STRING_1",
        "productName": "ExampleProduct",
        "productPrice": 0.00,
        "productSalePrice": 0.00,
        "productDescription": "An example product.",
        "productCategoryId": 1,
        "productSizeId": 3,
        "productColorId": 4
    }

Will return this:

    {
        "id": "STRING_1",
        "productName": "ExampleProduct",
        "productPrice": 0.00,
        "productSalePrice": 0.00,
        "productDescription": "An example product.,
        "productCategoryId": 1,
        "productSizeId": 3,
        "productSize": {
            "id": 3,
            "productSizeName": "M"
        },
        "productColorId": 4,
        "productColor": {
            "id": 4,
            "colorName": "Yellow"
        }
    }
