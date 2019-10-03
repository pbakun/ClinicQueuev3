"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/queueHub").build();

//get roomNo from URL
var pathElements = window.location.pathname.split('/');
var roomNo = pathElements[pathElements.length - 1];

connection.on("ReceiveQueueNo", function (user, message) {
    DistributeQueueMessage(message);
    //document.getElementById("QueueNo").textContent = message;
});

connection.on("ReceiveAdditionalInfo", function (id, message) {
    document.getElementById("additionalInfo").innerHTML = message;
});

connection.on("ResetQueue", function (message) {
    document.getElementById("QueueNo").textContent = message;
    console.log(message);
});

connection.on("Refresh", function (roomNo) {
    console.log("refresh");
    location.reload();
});

connection.on("ReceiveDoctorFullName", function (user, message) {
    console.log(message);
    document.getElementById("DoctorFullName").textContent = message;
});

connection.start().then(function () {
    // todo
    connection.invoke("RegisterPatientView", roomNo).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});


function DistributeQueueMessage(message) {
    var mainField = document.getElementById("QueueNo");
    var secondField = document.getElementById("QueueMessageExtension");
    var headerField = document.getElementById("DoctorFullName");
    if (message.search("NZMR") === 0) {
        var firstPart = queueMessage.split(" ")[0];
        mainField.textContent = firstPart;
        secondField.textContent = queueMessage.substring(firstPart.length, queueMessage.length);
        headerField.hidden = true;
    }
    else {
        mainField.textContent = message;
        secondField.textContent = "";
        headerField.hidden = false
    }
}