function updateTotalPrice() {
    const unitPrice = parseFloat(document.getElementById('unitPrice').textContent);
    const quantity = parseInt(document.getElementById('quantity').value, 10);
    const totalPrice = unitPrice * quantity;

    document.getElementById('totalPrice').textContent = totalPrice.toFixed(2);
}

function addToCart() {
    const formData = {
        ArticleNumber: document.querySelector('input[name="ArticleNumber"]').value,
        ProductName: document.querySelector('input[name="ProductName"]').value,
        Price: parseFloat(document.querySelector('input[name="Price"]').value),
        Quantity: parseInt(document.getElementById('quantity').value, 10)
    };

    fetch('/cart/addtocart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                alert("Something went wrong, please try again.");
            }
            return response.json();
        })
        .then(data => {
            updateCartCountViewComponent();
        })
        .catch(error => {
            console.error('addToCart', error);
            alert("Something went wrong, please try again.");
        });
}

function updateCartCountViewComponent() {
    fetch('/cart/updatecartcount', {
        method: 'GET',
        headers: {
            'Content-Type': 'text/html'
        }
    })
        .then(response => {
            if (!response.ok) {
                alert("Something went wrong, please try again.");
            }
            return response.text();
        })
        .then(data => {
            document.getElementById('cartCountContainer').innerHTML = data;
        })
        .catch(error => {
            console.error('updateCartCountViewComponent', error);
        });
}

function updateCartItem(button, change) {

    const cartItem = button.closest('.cart-item');
    const articleNumber = cartItem.dataset.articleNumber;
    const quantityElement = cartItem.querySelector('.quantity');
    let quantity = parseInt(quantityElement.textContent, 10);

    quantity = Math.max(0, quantity + change);
    quantityElement.textContent = quantity;

    fetch('/cart/updatecartitem', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ ArticleNumber: articleNumber, Quantity: quantity })
    })
        .then(response => {
            if (!response.ok) {
                alert("Something went wrong, please try again.");
            }
            return response.json();
        })
        .then(data => {
            if (data.error) {
                alert("Something went wrong, please try again.");
                return;
            }

            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;
                return;
            }
            document.getElementById('totalQuantity').textContent = data.totalQuantity;
            document.getElementById('totalPrice').textContent = data.totalPrice.toFixed(2);

            if (quantity === 0) {
                document.querySelector(`.cart-item[data-article-number="${articleNumber}"]`).remove();
            }
        })
        .catch(error => {
            console.error('updateCartItem', error);
            alert("Something went wrong, please try again.");
        });
}