
function DatChoNgoi(idPhim, idRapPhim) {
    var choosePlaces = document.querySelector(".result-price");
    var place = getChosePlace("chosePlaces");
    if (choosePlaces.innerText === "0" || place.length == 0) {
        alert("Bạn chưa chọn chỗ ngồi!");
    } else {
        if (idPhim != null && idRapPhim != null) {
            var url = `/home/muahang?idphim=${idPhim}&idrapphim=${idRapPhim}`;
            window.location.href = url;
        }
    }
}
//
function updateChosePlace(selectedTime) {
    var arrChosePlace = [];
    document.cookie = "chosePlaces=" + arrChosePlace + "; path=/";
}
//
function getChosePlace(name) {
    const cookieValue = document.cookie
        .split('; ')
        .find(row => row.startsWith(`${name}=`));

    if (cookieValue) {
        const arrayString = cookieValue.split('=')[1];
        return JSON.parse(arrayString);
    }

    return [];
}
function setChosePlace(choosePlace) {
    var arrChosePlace = [];
    document.cookie = "chosePlaces=" + arrChosePlace + "; path=/";
}
//
function TinhTongTienGhe(price, nameChair, idGhe) {
    var resultPrice = document.querySelector(".result-price");
    var priceCurrent = parseFloat(resultPrice.innerText);
    resultPrice.innerText = parseFloat(price) + priceCurrent;
    //luu lai ghe
    var place = getChosePlace("chosePlaces");
    var isObjectInArray = place.some(item =>
        item.nameChair === nameChair && item.price === price && item.idGhe === idGhe
    );

    if (isObjectInArray) {
        // Nếu đối tượng đã tồn tại, loại bỏ nó khỏi mảng
        place = place.filter(item =>
            !(item.nameChair === nameChair && item.price === price && item.idGhe === idGhe)
        );
    } else {
        // Nếu đối tượng không tồn tại, thêm vào mảng
        place.push({ nameChair, price, idGhe });
    }

    // Cập nhật cookie
    document.cookie = "chosePlaces=" + JSON.stringify(place) + "; path=/";
}

function ActivePlace() {
    var arrPlaces = getChosePlace("chosePlaces");
    if (arrPlaces.length > 0) {
        var resultPrice = document.querySelector(".result-price");
        var totalPrice = 0;
        arrPlaces.forEach((p) => {
            var arrPlaceActived = document.querySelector(`.sits[data-place="${p.nameChair}"]`);
            var placeChecked = document.querySelector(`.checked-place`);

            var dataPriceValue = arrPlaceActived.getAttribute("data-price");
            if (arrPlaceActived != null) {
                arrPlaceActived.classList.add("sits-state--your");
            }
            if (placeChecked != null) {
                var div = document.createElement("div");
                div.className = `choosen-place ${p.nameChair}`;
                div.textContent = p.nameChair; // Use textContent to set the text inside the div
                placeChecked.appendChild(div);
            }
            totalPrice += parseFloat(dataPriceValue);
        });

        resultPrice.innerText = totalPrice;
    }
}
window.onload = function () {
    ActivePlace();

};
