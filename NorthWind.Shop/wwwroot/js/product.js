document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("searchInput").addEventListener("keyup", function (event) {
        if (event.key === "Enter") {
            var searchTerm = document.getElementById("searchInput").value;

            $.ajax({
                url: "/Home/Find",
                type: "POST",
                error: function (error) {

                }
            });
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var buttons = document.querySelectorAll("[name^='AddToCart']");
    buttons.forEach(function (button) {
        var productId = button.getAttribute("data-productid");
        button.addEventListener("click", function (event) {
            $.ajax({
                url: "/Cart/AddCart",
                type: "POST",
                data: { productId: productId },
                success: function (data) {
                    if (data.success) {
                        alert("Add Cart Success");
                    } else {
                        alert("Add Cart failed");
                    }
                },
                error: function (error) {
                    alert("Add Cart failed");
                }
            });
        });
    });
});
