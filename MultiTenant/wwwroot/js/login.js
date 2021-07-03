$(document).ready(function () {

    getLocation();

    

    $(document).on('click', '#btnNext', function (event) {
        GoNext();
    });
    $(document).on('click', '#btnLogin', function (event) {
        GoLogin();
    });
    $(document).on('click', '#btnBack', function (event) {
        window.location.href = "/login";
    });

    

});

function GoNext() {
    var reqRes = true;
    $('.req').each(function () {
        if ($(this).val() === '') {
            $(this).focus();
            $(this).addClass('is-invalid');
            reqRes = false;
        } else {
            $(this).removeClass('is-invalid');
            $(this).addClass('is-valid');
        }
    });
    if (reqRes === false)
        return false;

    var Remember_Me = "N";
    if ($('input[type=checkbox][id=RememberMe]').is(':checked')) {
        Remember_Me="Y"
    }
    var objModel = {
        UserName: $('#UserName').val(),
        RememberMe: Remember_Me
    };
    $.ajax({
        url: '/login/LoginPre'.toLowerCase(),
        data: {
            model: objModel
        },
        cache: false,
        type: "POST",
        dataType: "JSON",
        success: function (result) {
            if (result.success === "Y") {               
                window.location.href = result.returnValue;
            }
            else {
                alert('faild');
            }
        }        
    });
}

function GoLogin() {
    var reqRes = true;
    $('.req').each(function () {
        if ($(this).val() === '') {
            $(this).focus();
            $(this).addClass('is-invalid');
            reqRes = false;
        } else {
            $(this).removeClass('is-invalid');
            $(this).addClass('is-valid');
        }
    });
    if (reqRes === false)
        return false;

    var objModel = {
        UserName: $('#UserName').val(),
        Password: $('#Password').val(),
        Lat: $('#Lat').val(),
        Lng: $('#Lng').val(),
        Technology: $('#Technology').val()
    };
    $.ajax({
        url: '/account/Signin'.toLowerCase(),
        data: {
            model: objModel
        },
        cache: false,
        type: "POST",
        dataType: "JSON",
        success: function (result) {
            if (result.success === "Y") {
                window.location.href = result.returnValue;
            }
            else {
                alert('faild');
            }
        }
    });
}

//Get user Lat/Lng    
function getLocation() {
    var nVer = navigator.appVersion;
    var technology = navigator.userAgent;
    var browserName = navigator.appName;
    var fullVersion = '' + parseFloat(navigator.appVersion);
    var majorVersion = parseInt(navigator.appVersion, 10);
    var nameOffset, verOffset, ix;

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            Lat = position.coords.latitude;
            Lng = position.coords.longitude;

            $('#Lat').val(Lat);
            $('#Lng').val(Lng);
            $('#Technology').val(technology);

        });
    }
}
