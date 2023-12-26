
function setSelectedTimeOnLoad() {
    var chooseArea = document.querySelector(".choosen-area");
    var selectedTime = getSelectedTime();
    if (selectedTime) {
        $('.selectable-time').removeClass('selected');
        var selectedTimeElement = $('.selectable-time[data-time="' + selectedTime + '"]');
        if (selectedTimeElement.length > 0) {
            selectedTimeElement.addClass('active');
            selectedTimeElement.addClass('selected');
            chooseArea.innerHTML = selectedTime.toString();
        }
    }
}
//

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
function setChooseDay() {
    var date = document.querySelector("#datepicker");
    document.cookie = "ngayDatGhe=" + date.value + "; path=/";
   
}
// Call the function to set the selected time on page load
window.onload = function () {
    setSelectedTimeOnLoad();
    //
    var date = document.querySelector("#datepicker");
    var getDateByCookie = getNgayDatGhe();
    date.value = getDateByCookie;
};

