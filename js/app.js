function updateDateTime(){
    var now = new Date();

    var days = ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
    var day = days[now.getDay()];

    var date = now.toLocaleDateString();
    var time = now.toLocaleTimeString();

    document.getElementById("date-time").innerHTML = day + ", " + date + ", " + time;
}
setInterval(updateDateTime, 1000);
updateDateTime();

function validateSignup(){
    var name = document.getElementById("signup-name").value.trim();
    var email = document.getElementById("signup-email").value.trim();
    var password = document.getElementById("signup-password").value.trim();
    var errorMsg = document.getElementById("signup-error");

    errorMsg.innerHTML = "";

    var emailpatt = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if(!email.match(emailpatt)){
        errorMsg.innerHTML = "Please enter a valid email address.";
        return false;
    }

    if(password.length < 8){
        errorMsg.innerHTML = "Password must be at least 8 characters.";
        return false;
    }

    alert("signup successful!!");
    return true;
}

function validateLogin(){
    var email = document.getElementById("login-email").value.trim();
    var password = document.getElementById("login-password").value.trim();
    var errorMsg = document.getElementById("login-error");

    errorMsg.innerHTML = "";

    if(email === "" || password === ""){
        errorMsg.innerHTML = "Both fields are required.";
        return false;
    }

    if(password.length < 8){
        errorMsg.innerHTML = "Incorrect email or password length.";
        return false;
    }

    alert("Login successful!!");
    return true;
}