function Find()
{
            var searchTerm = document.getElementById("searchInput").value;
            $.ajax({
                url: "/Home/Find",
                type: "POST",
                data: { productName: searchTerm },
                error: function (error) {

                }
            });
        
}

// document.addEventListener("DOMContentLoaded", function () {
//     var buttons = document.querySelectorAll("[name^='AddToCart']");
//     buttons.forEach(function (button) {
//         var productId = button.getAttribute("data-productid");
//         button.addEventListener("click", function (event) {
//             $.ajax({
//                 url: "/Cart/AddCart",
//                 type: "POST",
//                 data: { productId: productId },
//                 success: function (data) {
//                     if (data.success) {
//                         alert("Add Cart Success");
//                     } else {
//                         alert("Add Cart failed");
//                     }
//                 },
//                 error: function (error) {
//                     alert("Add Cart failed");
//                 }
//             });
//         });
//     });
// });

function AddToCart(id) {
    $.ajax({
        url: "/Cart/AddCart",
        type: "POST",
        data: { productId: id },
        success: function (data) {
            if (data.success) {
                Swal.fire({
					title: 'Giỏ hàng',
					text: 'Sản phẩm được thêm thành công',
					icon: 'info',
					timer: 2000
				})
            } 
            else 
            {
                Swal.fire({
					title: 'Giỏ hàng',
					text: 'Thêm sản phẩm không thành công',
					icon: 'info',
					timer: 2000
				})
            }
        },
        error: function (error) {
            alert("Add Cart failed");
        }
});
}
