function GetDataByCookie(name) {
    const cookieValue = document.cookie
        .split('; ')
        .find(row => row.startsWith(`${name}=`));

    if (cookieValue) {
        const arrayString = cookieValue.split('=')[1];
        return JSON.parse(arrayString);
    }

    return [];
}
function getNgayDatGhe() {
    var name = "ngayDatGhe=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(';');
    for (var i = 0; i < cookieArray.length; i++) {
        var cookie = cookieArray[i].trim();
        if (cookie.indexOf(name) == 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }
    return "";
}
function CheckHoaDon() {
    var dataPlaces = GetDataByCookie("chosePlaces") || [];
    var dataFoods = GetDataByCookie("carts") || [];
    var tableContentCheck = document.querySelector(".table-content-check");
    var totalCheck = document.querySelector("#total-check");
    var totalPrice = 0;
    dataPlaces.forEach((place) => {
        var tr = `<tr>
            <td>${place.nameChair}</td> 
            <td>${place.price} VND</td> 
        </tr>`;
        tableContentCheck.innerHTML += tr;
        totalPrice += parseFloat(place.price);
    })
    dataFoods.forEach((food) => {
        var tr = `<tr>
            <td>${food.itemName}</td> 
            <td>${food.itemPrice} VND</td> 
        </tr>`;
        tableContentCheck.innerHTML += tr;
        totalPrice += parseFloat(food.itemPrice);
    })
    totalCheck.innerHTML = `${totalPrice} VND`;
}
window.onload = function () {
    CheckHoaDon();

};

// lưu hóa đơn


function LuuHoaDon(idPhim, idUser) {
    var dataPlaces = GetDataByCookie("chosePlaces") || [];
    var dataFoods = GetDataByCookie("carts") || [];
    var emailUser = document.querySelector("#check-hoadon-emailUser");
    var phoneUser = document.querySelector("#check-hoadon-phoneUser");
    //
    const timestamp = new Date().getTime(); // Lấy thời gian hiện tại
    const randomInteger = Math.floor(Math.random() * 1000000); // Số nguyên ngẫu nhiên trong khoảng từ 0 đến 999999
    const idHoaDon = `${timestamp}${randomInteger}`;
    //
    function getCookieValue(name) {
        var decodedCookie = decodeURIComponent(document.cookie);
        var cookieArray = decodedCookie.split(';');
        for (var i = 0; i < cookieArray.length; i++) {
            var cookie = cookieArray[i].trim();
            if (cookie.indexOf(name) == 0) {
                return cookie.substring(name.length, cookie.length);
            }
        }
        return "";
    }
    var idRapPhim = getCookieValue("idRapPhim=");
    var idSuatChieu = getCookieValue("idSuatChieu=");

    //
    dataPlaces.forEach((place) => {
        if (place.idGhe != null) {
            var formData = new FormData();
            formData.append('IdHoaDon', idHoaDon);
            formData.append('IdPhim', idPhim);
            formData.append('IdGhe', place.idGhe);
            formData.append('TenGhe', place.nameChair);
            formData.append('GiaGhe', place.price);
            formData.append('IdRapPhim', Number(idRapPhim));
            formData.append('IdSuatChieu', Number(idSuatChieu));


            fetch('/Home/LuuHoaDon', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    // Xử lý phản hồi từ server (nếu cần)
                    console.log('Success:', data);
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
    })
    dataFoods.forEach((food) => {
        if (food.idFood != null) {
            var formData = new FormData();
            formData.append('IdHoaDon', idHoaDon);
            formData.append('IdPhim', idPhim);
            formData.append('IdFood', food.idFood);
            formData.append('TenFood', String(food.itemName));
            formData.append('GiaFood', parseFloat(food.itemPrice));
            formData.append('IdRapPhim', Number(idRapPhim));
            formData.append('IdSuatChieu', Number(idSuatChieu));

            fetch('/Home/LuuHoaDon', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    // Xử lý phản hồi từ server (nếu cần)
                    console.log('Success:', data);
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
    })
    if (dataFoods.length > 0 || dataPlaces.length > 0) {
        var ngayDatGhe = getNgayDatGhe();
        //api tao thanh toan
        var formData = new FormData();
        formData.append('IdHoaDon', idHoaDon);
        formData.append('EmailUser', String(emailUser.value));
        formData.append('PhoneUser', String(phoneUser.value));
        formData.append('NgayDatGhe', ngayDatGhe)
        fetch('/Home/CreateThanhToan', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                // Xử lý phản hồi từ server (nếu cần)
                console.log('Success:', data);
                if (data) {
                    window.location.href = `/item/chitiethoadon?idThanhToan=${data}`;
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    } else {
        alert('Bạn đã đặt vé rồi!');
    }
}