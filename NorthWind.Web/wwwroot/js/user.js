document.addEventListener("DOMContentLoaded", async function () {
    const response = await fetch("/employees.json");
    const data = await response.json();
    displayEmployeeData(data);

});
function displayEmployeeData(employeesData) {
    const employeeTable = document.getElementById("employeeslist");
    employeeTable.innerHTML = ""; // Xóa thông tin cũ
    employeesData.forEach(employee => {
    let html =  `
     <td>
                        <div class="form-check form-check-sm form-check-custom form-check-solid">
                            <input class="form-check-input" type="checkbox" value="1">
                        </div>
                    </td>
                    <td class="d-flex align-items-center">
                        <!--begin:: Avatar -->
                        <!--end::Avatar-->
                        <!--begin::User details-->
                        <div class="d-flex flex-column">
                            <a href="#" class="text-gray-800 text-hover-primary mb-1">${employee.lastName} + ${employee.firstname}</a>
                            <span>${employee.address}</span>
                        </div>
                        <!--begin::User details-->
                    </td>
                    <td>
                       ${employee.title}               </td>
                    <td >
                        <div class="badge badge-light fw-bold">${employee.titleOfCourtesy}</div>
                    </td>
                    <td>
                              ${employee.country}                           </td>
                    <td >
                    ${employee.birthDate}  
                                  </td>
                    <td class="text-end">
                        <a href="#" class="btn btn-light btn-active-light-primary btn-flex btn-center btn-sm" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
                            Actions
                            <i class="ki-duotone ki-down fs-5 ms-1"></i>                    </a>
                        <!--begin::Menu-->
            <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-125px py-4" data-kt-menu="true">
            <!--begin::Menu item-->
            <div class="menu-item px-3">
            <a href="#" class="menu-link px-3">
                Edit
            </a>
            </div>
            <!--end::Menu item-->

            <!--begin::Menu item-->
            <div class="menu-item px-3">
            <a href="#" class="menu-link px-3" data-kt-users-table-filter="delete_row">
                Delete
            </a>
            </div>
            <!--end::Menu item-->
            </div>
            <!--end::Menu-->
                    </td>

                            `;
    const row = document.createElement("tr");
    row.classList.add("odd")
    row.innerHTML += html;
    employeeTable.appendChild(row);
 
    });
}