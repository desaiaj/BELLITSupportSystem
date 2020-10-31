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


//Used from W3School 
function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("TicketsTable");
    switching = true;
    // Set the sorting direction to ascending:
    dir = "asc";
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /* Loop through all table rows (except the
        first, which contains table headers): */
        for (i = 1; i < (rows.length - 1); i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Get the two elements you want to compare,
            one from current row and one from the next: */
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            /* Check if the two rows should switch place,
            based on the direction, asc or desc: */
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            // Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {
            /* If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again. */
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}