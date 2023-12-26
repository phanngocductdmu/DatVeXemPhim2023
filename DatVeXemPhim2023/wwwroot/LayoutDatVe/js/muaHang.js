function getCart(name) {
    const cookieValue = document.cookie
        .split('; ')
        .find(row => row.startsWith(`${name}=`));

    if (cookieValue) {
        const arrayString = cookieValue.split('=')[1];
        return JSON.parse(arrayString);
    }

    return [];
}
function CartOnLoad() {
    var carts = getCart("carts");
    var total = document.getElementById('total-cost');
    var cartItems = document.querySelector("#cart-items");
    cartItems.innerHTML = "";
    var totalPrice = 0;
    if (carts.length > 0) {
        carts.forEach((cartItem, index) => {
            var li = document.createElement("li");
            li.textContent = `${cartItem?.itemName} ${cartItem?.itemPrice} VND `; // Corrected property access
            li.setAttribute('data-index', index);
            var deleteButton = document.createElement('button');
            deleteButton.textContent = 'Xóa';
            deleteButton.addEventListener('click', function () {
                removeFromCart(index);
            });

            li.appendChild(deleteButton);

            cartItems.appendChild(li);
            totalPrice = totalPrice + parseFloat(cartItem.itemPrice);
        });
    }
    total.innerText = totalPrice;
}
var cartItems = [];

function addToCart(itemName, itemPrice, idFood) {
    cartItems.push({ name: itemName, price: itemPrice, idFood });

    // Lấy mảng đối tượng từ cookie
    var carts = getCart("carts");

    // Kiểm tra xem mảng đã tồn tại trong cookie hay chưa
    if (carts) {
        carts.push({ itemName, itemPrice, idFood });
        document.cookie = "carts=" + JSON.stringify(carts) + "; path=/";
    } else {
        var carts = [{ name: itemName, price: itemPrice }];
        document.cookie = "carts=" + JSON.stringify(carts) + "; path=/";
    }
    CartOnLoad();
}
//function updateCart() {
//    var carts = getCart("carts");
//    if (carts.length > 0) {
//        var cartList = document.getElementById('cart-items');
//        var totalCost = 0;
//        cartList.innerHTML = "";
//        carts.forEach(function (item, index) {
//            var listItem = document.createElement('li');
//            listItem.textContent = item.itemName + ': ' + item.itemPrice + ' VND ';
//            listItem.setAttribute('data-index', index);
//            var deleteButton = document.createElement('button');
//            deleteButton.textContent = 'Xóa';
//            deleteButton.addEventListener('click', function () {
//                removeFromCart(index);
//            });

//            listItem.appendChild(deleteButton);

//            cartList.appendChild(listItem);
//            totalCost += item.itemPrice;
//        });
//        document.getElementById('total-cost').textContent = totalCost + ' VND ';
//    }
//}

function checkout() {
    alert('Đã thanh toán! Chúc bạn có một trải nghiệm tuyệt vời!');
}
function removeFromCart(index) {
    var carts = getCart("carts");
    carts.splice(index, 1);
    document.cookie = "carts=" + JSON.stringify(carts) + "; path=/";
    CartOnLoad();
}
window.onload = function () {
    CartOnLoad();
};