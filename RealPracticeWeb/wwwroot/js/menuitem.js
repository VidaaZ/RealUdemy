var dataTable; //one way of reloading our datatable
$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({  //one way of reloading our datatable
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",

                "render": function (data) {
                    return `<div class="w-75 btn-group" >
                             <a href="/Admin/MenuItems/upsert?id=${data}"  class="btn btn-success text-white mx-2">
                             <i class="bi bi-pencil-square"></i>Edit</a>
                             <a onClick=Delete('/api/MenuItem/'+${data}) class="btn btn-danger text-white mx-2">
                             <i class="bi bi-trash"></i>Delete</a>
                            </div>`
                 
                },
                "width": "15%"
            }
            
        ],
        "width": "100%"

    });   
});
/*delete is custom method in javascript which should be called in delete link*/
function Delete(url) {  //this url will be the our API endpoint(DASTOR code HtpDelete in MenuItemController.cs)
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
           
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        DataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
} 