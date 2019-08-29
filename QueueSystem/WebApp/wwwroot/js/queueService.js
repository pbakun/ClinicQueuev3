"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/queueHub").build();

connection.on("ReceiveQueueNo", function (user, message) {
    console.log(message);

});

connection.start().then().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("PrevNo").addEventListener("click", function (event) {
    connection.invoke("NewQueueNo", 1, 12).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});