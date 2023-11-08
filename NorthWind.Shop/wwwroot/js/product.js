document.getElementById("searchInput").addEventListener("keyup", function (event) {
    // Kiểm tra xem phím Enter (keyCode 13) đã được ấn
    if (event.key === "Enter") {
        // Lấy giá trị từ trường nhập liệu
        var searchTerm = document.getElementById("searchInput").value;

        // Sử dụng Ajax để gửi yêu cầu tìm kiếm đến Controller
        $.ajax({
            url: "/Home/Find",
            type: "POST",
            error: function (error) {
              
            }
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("AddToCart").addEventListener("click", function (event) {
        var productId = document.querySelector('input[name="productId"]').value;
        console.log("Product ID: " + productId);
    
        $.ajax({
            url: "/Cart/AddCart",
            type: "POST",
            data: { productId: productId },
            success: function (data) {
                if (data.success) 
                {
                    // Hiển thị thông báo khi thêm vào giỏ hàng thành công
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

