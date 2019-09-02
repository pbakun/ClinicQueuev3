﻿"use strict";

//roomNo, queueNo and id defined in Index.cshtml. Value taken from the model

var connection = new signalR.HubConnectionBuilder().withUrl("/queueHub").build();



connection.on("ReceiveQueueNo", function (user, message) {
    document.getElementById("QueueNo").innerHTML = message;
console.log(message);
});

connection.start().then(function(){
    connection.invoke("RegisterDoctor", id, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("PrevNo").addEventListener("click", function (event) {
    queueNo--;
    connection.invoke("NewQueueNo", id, queueNo, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("NextNo").addEventListener("click", function (event) {
    queueNo++;
    connection.invoke("NewQueueNo", id, queueNo, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

