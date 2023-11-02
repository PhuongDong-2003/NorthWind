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
                // Xử lý lỗi ở đây
            }
        });
    }
});
