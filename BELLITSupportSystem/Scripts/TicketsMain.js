$(document).ready(function () {
    $('#dlDepartment').change(function () {
        GetEmployeeByDepartment();
    });
});

function GetEmployeeByDepartment() {
    debugger
    var DepartmentId = $('#dlDepartment').val();

    $.ajax({
        url: urlto,
        async: false,
        contentType: 'application/html; charset=utf-8',
        dataType: 'html',
        data: {
            DepartmentId: DepartmentID
        },
    })
        .success(function (result) {
            //debugger
            if (result != null) {
                $('#EmployeeList').html(null);
                $('#EmployeeList').html(result);
                //alert("success");
            }
            else {
                $('#EmployeeList').html(null);
                $('#EmployeeList').html('<div class="alert alert-danger"> <strong>NOTE: !</strong> No Employees found in this department.</div>');
            }
        })
        .error(function (xhr, textStatus, errorThrown) {
            if (xhr.status == "590" || xhr.status == "403")
                return false;
            else { }
        });
}
