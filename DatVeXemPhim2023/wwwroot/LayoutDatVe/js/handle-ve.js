function test() {
    // Lấy giá trị được chọn từ thẻ select
    var selectedValue = document.getElementById("selectRapPhim").value;

    // Phân giải giá trị thành các thành phần
    var values = selectedValue.split(',');

    // values[0] chứa Idphim, values[1] chứa Idrapphim
    var idrapphim = values[0];
    var idphim = values[1];

    if (idphim != null && idrapphim != null) {
        var url = "/datve/diachi?idphim=" + idphim + "&idrapphim=" + idrapphim;
        window.location.href = url;

        //// Tạo đường dẫn URL với hai giá trị và gán nó cho thuộc tính href của liên kết
        //var url = "/datve/diachi?idphim=" + idphim + "&idrapphim=" + idrapphim;
        //document.getElementById("link").setAttribute("href", url);
    }
}
var chooseArea = document.querySelector(".choosen-area");
var diachiNextButton = document.querySelector("#diachi-nextButton");
function ChonChoNgoi(idPhim, idRapPhim) {
    if (chooseArea.innerText == "") {
        alert("Bạn chưa chọn giờ! Vui lòng chọn giờ để tiến hành chọn chỗ ngồi!")
    } else {
        if (idPhim != null && idRapPhim != null) {
            var url = `/home/datchongoi?idphim=${idPhim}&idrapphim=${idRapPhim}`;
            window.location.href = url;
        }
    }
};
//asp-controller="home" asp-action="datchongoi" asp-route-IdPhim="@Model.phims.Idphim" asp-route-IdRapPhim="@Model.rapChieuPhim.IdrapChieuPhim"
//tinh tong tien ghe khi chon ghe


//function TinhTongTienGhe(price) {
//    var resultPrice = document.querySelector(".result-price");
//    var priceCurrent = parseFloat(resultPrice.innerText);
//    resultPrice.innerText = parseFloat(price) + priceCurrent;
//    //luu lai ghe
//    arrChosePlace = [];
//    document.cookie = "chosePlaces=" + arrChosePlace + "; path=/";
//}

//

