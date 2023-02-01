// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleElement(checkboxId, elementId) {
    var checkbox = document.getElementById(checkboxId);
    var element = document.getElementById(elementId);
    if (checkbox.checked) {
        element.style.display = "block";
    } else {
        element.style.display = "none";
    }
}