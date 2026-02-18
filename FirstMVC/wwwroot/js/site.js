// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function pay(priceId) {
    fetch('api/stripe/pay', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(priceId)
    })
    .then(response => response.text())
    .then(url => {
        window.location.href = url;
    })
    .catch(error => console.error('Error:', error));
}