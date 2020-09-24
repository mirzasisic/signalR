"use strict";

var _user = prompt("Please enter your name");

var connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chatHub?username=${_user}`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

const URL = window.location.origin;

connection.on("MessageToGroup", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = "Group message:" + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("MessageToUser", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = "User message:" + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("MessageToAll", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = "Message to all:" + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("MessageToAll", function (notification) {
    var msg = notification.message;
    var time = notification.time;
    var encodedMsg = time.toString("dd/MM/YYYY hh:mm") + " - " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("userId").value = connection.connectionId;

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("MessageToAll", message).catch(function (err) {
        return console.error(err.toString()); 
    });
    connection.invoke("AddUserToList", user).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});

document.getElementById("sendToGroupBtn").addEventListener("click", function (event) {
    
    var group = document.getElementById("sendToGroup").value;
    var message = document.getElementById("messageToGroup").value;
    const url = `${URL}/sendToGroup?message=${message}&group=${group}`;

    const data = {
        message: message,
        group: group
    };
    $.post(url, data, function (status) {
        console.log(`status is ${status}`)
    });
    event.preventDefault();
});

document.getElementById("addUser").addEventListener("click", function (event) {
    var user = document.getElementById("userId").value;
    var group = document.getElementById("addToGroup").value;
   
    var url = `${URL}/addUserToGroup?user=${user}&group=${group}`;
    var data = {
        user: user,
        group: group
    };
    $.post(url, data, function (data, status) {
        console.log(`${data} and status is ${status}`)
    });

    url = `${URL}/addRealUserToGroup?user=${_user}&group=${group}`;
    data = {
        user: _user,
        group: group
    };
    $.post(url, data, function (data, status) {
        console.log(`${data} and status is ${status}`)
    });

    event.preventDefault();
});

document.getElementById("removeUser").addEventListener("click", function (event) {
    var user = document.getElementById("userId").value;
    var group = document.getElementById("removeFromGroup").value;
   
    const url = `${URL}/RemoveUserFromGroup?user=${user}&group=${group}`;
    const data = {
        user: user,
        group: group
    };
    $.post(url, data, function (data, status) {
        console.log(`${data} and status is ${status}`)
    });
    event.preventDefault();
});