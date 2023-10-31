document.addEventListener("DOMContentLoaded", function() {
    var checkbox = document.getElementById("discontinuedCheckbox");
    var modelValue = JSON.Serialize(Model.Discontinued); 
    checkbox.checked= modelValue;
    console.log(modelValue);
});