"use strict";

//roomNo, queueNo and id defined in Index.cshtml. Value taken from the model

var connection = new signalR.HubConnectionBuilder().withUrl("/queueHub").build();


//Set new QueueNoMessage if new one arrives. 
connection.on("ReceiveQueueNo", function (user, message) {
    document.getElementById("QueueNo").innerHTML = message;
});

connection.on("ReceiveAdditionalInfo", function (id, message) {
    document.getElementById("additionalInfo").value = message;
    var elementClassList = document.getElementById("SendAdditionalMessage").classList;
    if (message.length > 0) {
        elementClassList.replace("btn-dark", "btn-success");
    }
    else {
        elementClassList.replace("btn-success", "btn-dark");
    }
    
    
});

connection.on("NotifyQueueOccupied", function (message) {
    document.getElementById("serverMessage").innerHTML = message;
});

connection.start().then(function(){
    connection.invoke("RegisterDoctor", id, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("PrevNo").addEventListener("click", function (event) {
<<<<<<< HEAD
    
=======
>>>>>>> 16e271115eddaf32cfabc06f502893be9b21767c
    if (queueNo > 0) {
        queueNo--;
        connection.invoke("NewQueueNo", id, queueNo, roomNo).catch(function (err) {
            return console.error(err.toString());
        }); 
    }
    event.preventDefault();
});

document.getElementById("NextNo").addEventListener("click", function (event) {
    queueNo++;
    connection.invoke("NewQueueNo", id, queueNo, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("NewQueueNoSubmit").addEventListener("click", function (event) {
    var newNo = document.getElementById("NewQueueNoInputBox").value;
    ForceNewQueueNo(newNo);
    document.getElementById("NewQueueNoInputBox").value = "";
    event.preventDefault();
});

function ForceNewQueueNo(newNo) {
    newNo = parseInt(newNo);
    if (newNo > 0) {
        queueNo = newNo;
        connection.invoke("NewQueueNo", id, newNo, roomNo).catch(function (err) {
            return console.error(err.toString());
        });
    }
}

//send -1 (sets break to true) to the server
document.getElementById("Break").addEventListener("click", function (event) {
    connection.invoke("NewQueueNo", id, -1, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("Special").addEventListener("click", function (event) {
    connection.invoke("NewQueueNo", id, -2, roomNo).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("SendAdditionalMessage").addEventListener("click", function (event) {
    var message = document.getElementById("additionalInfo").value;
    connection.invoke("NewAdditionalInfo", id, roomNo, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("ClearAdditionalMessage").addEventListener("click", function (event) {
    connection.invoke("NewAdditionalInfo", id, roomNo, '').catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


