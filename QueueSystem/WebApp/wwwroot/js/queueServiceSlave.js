"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/queueHub").build();

//get roomNo from URL
var pathElements = window.location.pathname.split('/');
var roomNo = pathElements[pathElements.length - 1];

connection.on("ReceiveQueueNo", function (user, message) {
    console.log(Date.now());
    console.log(user);
    console.log(message);
    console.log(window.location.pathname);
    document.getElementById("QueueNo").textContent = message;
});

connection.on("ReceiveAdditionalInfo", function (id, message) {
    document.getElementById("additionalInfo").innerHTML = message;
});

connection.start().then(function () {
    // todo
    connection.invoke("RegisterPatientView", roomNo).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});
