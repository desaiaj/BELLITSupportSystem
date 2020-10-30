$(document).ready(function () {
    //debugger
    if (typeof action !== 'undefined' && action != '')
        if (action.toLowerCase() == 'true')
            myFunction("msgSuccess");
        else
            myFunction("msgFailure");

    $('#dlDepartment').change(function () {
        $('#EmployeeList').html(null);
        GetEmployeeByDepartment();
    });
});

function GetEmployeeByDepartment() {
    //debugger
    var DepartmentID = $('#dlDepartment').val();

    $.ajax({
        url: urlto,
        contentType: 'application/html; charset=utf-8',
        dataType: 'html',
        data: {
            DepartmentID: DepartmentID
        },
        success: function (result) {
            //debugger
            if (result != null && result != "") {
                $('#EmployeeList').html(null);
                $('#EmployeeList').html(result);
            }
            else {
                $('#EmployeeList').html(null);
                $('#EmployeeList').html('<div class="alert alert-danger"> <strong>NOTE: !</strong> No Employees found in this department, Please select other.</div>');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            if (xhr.status == "590" || xhr.status == "403")
                return false;
            else { }
        }
    });
}

function myFunction(domID) {
    // Get the snackbar DIV
    var dom = document.getElementById(domID);

    // Add the "show" class to DIV
    dom.classList.add("show");

    // After 3 seconds, remove the show class from DIV
    setTimeout(function () {
        dom.classList.remove("show");
    }, 3000);
}

