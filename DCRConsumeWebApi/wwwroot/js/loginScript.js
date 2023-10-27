$(document).ready(function () {
    $('#verifyOTP').hide();
    $('#Cpassword').hide();
    $('#submitBtn').hide();
});



//$(document).ready(function () {
//    $('#sendbtn').click(function () {
//        var button = document.getelementbyid("sendbtn");
//        var originaltext = button.innerhtml;
//        var countdown = 10; // countdown time in seconds
//        $('#verifyOTP').show();
//        button.disabled = true;
//        var countdowninterval = setinterval(function () {
//            countdown--;
//            if (countdown <= 0) {
//                button.innerhtml = "resend";
//                button.disabled = false;
//                clearinterval(countdowninterval);
//            } else {
//                button.innerhtml = ' ' + countdown + 's';
//            }
//        }, 1000); // update every 1 second (1000 milliseconds)
//    });
//});

$(document).ready(function () {
    $('#verifyBtn').click(function () {
        $('#Cpassword').show();
        $('#submitBtn').show();
    });
});


// Confirm Password Validation

$(document).ready(function () {
    $("#confirmPassword").keyup(function () {
        var pwd = $("#password").val();
        var cpwd = $("#confirmPassword").val();
        var errorElement = $("#showErrorcpwd");

        if (cpwd !== pwd) {
            errorElement.html("Passwords do not match");
            errorElement.css("color", "red");
            return false;
        } else {
            errorElement.html("");
            return true;
        }
    });
});




