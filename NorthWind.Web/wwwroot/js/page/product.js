import { ApiConfig } from "/js/config/api.js";

let ProductPage = (() => {

    let productDatasource, productgrid;

    const InitProductGrid = async () => {
        const response = await fetch(ApiConfig.ProductApi.GetByPage);
        const products = await response.json();
        productDatasource = new kendo.data.DataSource({
            data: products,
            schema: {
                model: {
                    fields:
                    {
                        productName: { type: "string" },
                        supplierId: { type: "number" },
                        categoryID: { type: "number" },
                        quantityPerUnit: { type: "string" },
                        unitPrice: { type: "number" },
                        Action: { type: "boolean" }
                    }

                }

            },
            pageSize: 10
        });
        productgrid = $("#grid").kendoGrid({
            dataSource: productDatasource,
            height: 550,
            width: 1050,
            groupable: true,
            sortable: true,
            pageable: true,
            editable: "inline",
            pageable: {
                input: true,
                numeric: false
            },
            columns: [

                { field: "productName", title: "ProductName", width: "130px" },
                { field: "supplierID", title: "SupplierID", width: "130px" },
                { field: "categoryID", title: "CategoryID", width: "130px" },
                { field: "quantityPerUnit", title: "QuantityPerUnit", width: "180px" },
                { field: "unitPrice", title: "UnitPrice", width: "130px" },
                {
                    command: ["edit", "destroy"],
                    title: "Options ",
                    width: "250px"
                }
            ],

            save: function (e) {
                var grid = $("#grid").data("kendoGrid");
                var dataItem = grid.dataItem(e.container);

                var productID = dataItem.productID;
                var productName = dataItem.productName;
                var supplierID = dataItem.supplierID;
                var categoryID = dataItem.categoryID;
                var quantityPerUnit = dataItem.quantityPerUnit;
                var unitPrice = dataItem.unitPrice;
                var unitsInStock = dataItem.unitsInStock;
                var unitsOnOrder = dataItem.unitsOnOrder;
                var reorderLevel = dataItem.reorderLevel;
                var discontinued = dataItem.discontinued;


                var product = {

                    ProductID: productID,
                    ProductName: productName,
                    SupplierID: supplierID,
                    CategoryID: categoryID,
                    UnitPrice: unitPrice,
                    QuantityPerUnit: quantityPerUnit,
                    UnitsInStock: unitsInStock,
                    UnitsOnOrder: unitsOnOrder,
                    ReorderLevel: reorderLevel,
                    Discontinued: discontinued

                }

                const response = fetch(ApiConfig.ProductApi.Update, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(product)

                });

            },
            remove: function (e) {
                // var grid = $("#grid").data("kendoGrid");
                // var dataItem = grid.dataItem(e.container);
                // var productID = dataItem.productID;

                var productId = e.model.productID
                const response = fetch(ApiConfig.ProductApi.Delete, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(productId)

                });

            }


        }).data("kendoGrid");
    }

    const InitBtnCreateProduct = () => {

        var btnCreateProduct = document.getElementById("btnCreateProduct");

        btnCreateProduct.onclick = () => {

            var productName = document.getElementById("productName").value;
            var supplierID = document.getElementById("supplierID").value;
            var categoryID = document.getElementById("categoryID").value;
            var quantityPerUnit = document.getElementById("quantityPerUnit").value;
            var unitPrice = document.getElementById("unitPrice").value;
            var unitsInStock = document.getElementById("unitsInStock").value;
            var unitsOnOrder = document.getElementById("unitsOnOrder").value;
            var reorderLevel = document.getElementById("reorderLevel").value;
            var discontinued = document.getElementById("discontinued");
            var isDiscontinued = discontinued.checked;

            var data = {

                ProductName: productName,
                SupplierID: supplierID,
                CategoryID: categoryID,
                QuantityPerUnit: quantityPerUnit,
                UnitPrice: unitPrice,
                UnitsInStock: unitsInStock,
                UnitsOnOrder: unitsOnOrder,
                ReorderLevel: reorderLevel,
                Discontinued: isDiscontinued

            }

            const response = fetch(ApiConfig.ProductApi.Create, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)

            });

        }
    }

    return {
        InitPage: () => {
            InitProductGrid();
            InitBtnCreateProduct();

        }
    }
})();

document.addEventListener("DOMContentLoaded", function () {
    ProductPage.InitPage();
});