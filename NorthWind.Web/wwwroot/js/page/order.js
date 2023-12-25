import { ApiConfig } from "/js/config/api.js";

let OrderPage = (() => {

    let orderDatasource, orderGrid;

    const CreateOrder = () => {

     
   
    }

    const InitOrderGrid = async () => {
        const response = await fetch(ApiConfig.OrderApi.GetByPage);
        const orders = await response.json();
        orderDatasource = new kendo.data.DataSource({
            data: orders,
            schema: {
                model: {
                    fields:
                        {
                            customerID: { type: "string" },
                            employeeID: { type: "number" },
                            orderDate: { type: "date" },
                            shipName: { type: "string" },
                            shippedDate: { type: "date" },
                            Action: { type: "function" }
                        }
                }
            },
            pageSize: 20
        });
        orderGrid = $("#grid").kendoGrid({
            dataSource: orderDatasource,
            height: 550,
            groupable: true,
            sortable: true,
            pageable: true,
            editable: "inline",
            pageable: {
                input: true,
                numeric: false
            },
            columns: [
                { field: "customerID", title: "CustomerID", width: "130px" },
                { field: "employeeID", title: "EmployeeID", width: "130px" },
                { field: "orderDate", title: "OrderDate", width: "180px", format: "{0:yyyy-MM-dd}" },
                { field: "shippedDate", title: "ShippedDate", width: "180px", format: "{0:yyyy-MM-dd}" },
                { field: "shipName", title: "ShipName", width: "170px"  },
                {
                    command: ["edit", "destroy"],
                    title: "Options ",
                    width: "250px"
                }
            ]

        }).data("kendoGrid");
    }

    const InitBtnCreateOrder =  () => {

        var btnCreateOrder = document.getElementById("btnCreateOrder");

        btnCreateOrder.onclick = () =>{
   
            var customerId = document.getElementById("customerId").value;
            var orderDate = document.getElementById("orderDate").value;
            var shipName = document.getElementById("shipName").value;
            var shippedDate = document.getElementById("shippedDate").value;
            var employeeId = document.getElementById("employeeId").value;
            var shipVia = document.getElementById("shipVia").value;
            var requiredDate = document.getElementById("requiredDate").value;
            var freight = document.getElementById("freight").value;
            var shipAddress = document.getElementById("shipAddress").value;
            var shipCity = document.getElementById("shipCity").value;
            var shipRegion = document.getElementById("shipRegion").value;
            var shipPostalCode = document.getElementById("shipPostalCode").value;
            var shipCountry = document.getElementById("shipCountry").value;
  
            const OrderDetails = [
                {
                    ProductID: 1,


                    UnitPrice: 19.99,
                    Quantity: 10,
                    Discount: 0
                },
                {
                    ProductID: 2,
                    UnitPrice: 29.99,
                    Quantity: 5,
                    Discount: 0
                },
                {
                    ProductID: 3,
                    UnitPrice: 9.99,
                    Quantity: 20,
                    Discount: 0
                }
            ];

            var data = {
                CustomerID: customerId,
                OrderDate: orderDate,   
                ShipName: shipName,
                ShippedDate: shippedDate,
                EmployeeID: employeeId,
                ShipVia: shipVia,
                RequiredDate: requiredDate,
                Freight: freight,
                ShipAddress: shipAddress,
                ShipCity: shipCity,
                ShipRegion: shipRegion,
                ShipPostalCode: shipPostalCode,
                ShipCountry: shipCountry,
                OrderDetails: OrderDetails
            }

            const response = fetch(ApiConfig.OrderApi.Create, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json' 
                },
                body: JSON.stringify(data ) 
             
            });

        }
    }

    return {
        InitPage: () => {
            InitOrderGrid();
            InitBtnCreateOrder();
        }
    }
})();



document.addEventListener("DOMContentLoaded", function () {
    OrderPage.InitPage();
});