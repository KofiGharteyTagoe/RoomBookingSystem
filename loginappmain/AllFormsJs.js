
function hideListItem() {
    document.getElementById('Modify Rooms').style.display = 'none';
    document.getElementById('Modify Rooms').style.visibility = 'hidden';

    document.getElementById('Users').style.visibility = 'hidden';
    document.getElementById('Users').style.display = 'none';

    document.getElementById('stats').style.visibility = 'hidden';
    document.getElementById('stats').style.display = 'none';
}

//Calendar

window.document.onkeydown = function (e) {
    if (!e) {
        e = event;
    }
    if (e.keyCode == 27) {
        lightbox_close();
    }
}
function lightbox_open(x) {
    var div = document.getElementById("light");
    //div.innerText = x;
    document.getElementsById('Label2').innerText = x;
    window.scrollTo(0, 0);
    document.getElementById('light').style.display = 'block';
    document.getElementById('fade').style.display = 'block';
}
function lightbox_close() {
    document.getElementById('light').style.display = 'none';
    document.getElementById('fade').style.display = 'none';
}

//HomePage
window.document.onkeydown = function (e) {
    if (!e) {
        e = event;
    }
    if (e.keyCode == 27) {
        lightbox_close();

    }
}
function lightbox_open(y, x) {
    var div1 = document.getElementById("textArea");
    var div2 = document.getElementById("buttonArea");
    if (y == "False") {
        div2.style.visibility = 'hidden';
    }
    else {
        div2.style.visibility = 'visible';
    }
   
    div1.value = x;

    window.scrollTo(0, 0);
    document.getElementById('light').style.display = 'block';
    document.getElementById('fade').style.display = 'block';
}


//Modify Booking
window.document.onkeydown = function (e) {
    if (!e) {
        e = event;
    }
    if (e.keyCode == 27) {
        HidePopup();

    }
}

function HidePopup() {
    var div2 = document.getElementById("PopupArea"); // Get the PopupArea
    div2.style.visibility = 'hidden'; // Make PopupArea visible
}

function ShowPopup() {
    var div2 = document.getElementById("PopupArea"); // Get the PopupArea
    div2.style.visibility = 'visible'; // Make PopupArea visible
    document.getElementById('PopupArea').style.display = 'block';
    //  document.getElementById('fade').style.display = 'block';
}

//ViewTheUser

window.document.onkeydown = function (e) {
    if (!e) {
        e = event;
    }
    if (e.keyCode == 27) {
        HidePopup();

    }
}

function HidePopup() {
    var div2 = document.getElementById("PopupArea"); // Get the PopupArea
    div2.style.visibility = 'hidden'; // Make PopupArea visible
}

function ShowPopup() {
    var div2 = document.getElementById("PopupArea"); // Get the PopupArea
    div2.style.visibility = 'visible'; // Make PopupArea visible
    document.getElementById('PopupArea').style.display = 'block';
    //  document.getElementById('fade').style.display = 'block';
}



//ADD BOOKIING/ CREATE BOOKING


$(function () {
    try{
        $("#txtDatePicker6M").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends, maxDate: "+6M" });
        $("#txtDatePicker5D").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends, maxDate: "+7D" });
        $("#txtRecurring").datepicker({ dateFormat: 'dd/mm/yy', minDate: -0, beforeShowDay: $.datepicker.noWeekends, maxDate: "+6M" });
    }
    catch(e) { } 
});
