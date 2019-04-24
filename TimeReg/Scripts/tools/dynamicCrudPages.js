
//Ajax table handling

//In the following JS the variable "CRUD" represents the type of action requested eg: Create, Remove, Update, Details.

$(document).ready(function () {
    UpdateDynamicTable();
});


function UpdateDynamicTable() {
    var baseUrl = $("#currentControllerPath").val();
    $.ajax({
        url: baseUrl+"/IndexTable",
        type: "GET"
    }).done(function (partialViewResult) {
        $("#PartialViewTableWrapper").html(partialViewResult);
    })
}






//Ajax Handling for the modal on the Edit page - and a bit other stuff
//The click handler for trigering the partialViewHandler. For the Create New, Edit | Details | Delete.
$(document).on('click', '.Create', function () {
    PartialViewHandler($(this), "Create");
});

$(document).on('click', '.Edit', function () {
    PartialViewHandler($(this), "Edit");
});

$(document).on('click', '.Details', function () {
    PartialViewHandler($(this), "Details");
});

$(document).on('click', '.Delete', function () {
    PartialViewHandler($(this), "Delete");
});


//Partial View Handler, gets the correct information from the "links" clicked. Split due to the Dynamic tables
function PartialViewHandler(object, CRUD) {
    var DynamicOriginElement = object;
    var DynamicOriginId = DynamicOriginElement.data("dynamicid");
    AjaxDynamicHandler(CRUD, DynamicOriginId);
}


//Handles the AJAX call to return a partial View. The Switch is used to get the correct partial view and then enters it into the modal. Editing the content just enough for it to be ready for the stored procedure part
function AjaxDynamicHandler(CRUD, DynamicOriginId) {
    var DynamicPK_Id = DynamicOriginId;
    var baseUrl = $("#currentControllerPath").val();

    console.log(DynamicOriginId);
    switch (CRUD) {
        case "Create":
            $.ajax({
                url: baseUrl +"/DynamicCreate"
            }).done(function (partialViewResult) {
                $("#PartialViewModal").modal('show');
                $("#PartialViewModalBody").html(partialViewResult);
                $("#PartialViewModalTitle").text("Rep It -  " + CRUD);
                $("#PartialViewModalSaveButton").data("crud", CRUD);
                $("#PartialViewModalSaveButton").text("Create");
            });
            break;
        case "Edit":
            $.ajax({
                url: baseUrl + "/DynamicEdit",
                type: "GET",
                data: { CRUD: CRUD, PK_Id: DynamicPK_Id }
            }).done(function (partialViewResult) {
                $("#PartialViewModal").modal('show');
                $("#PartialViewModalTitle").text("Rep It -  " + CRUD);
                $("#PartialViewModalSaveButton").data({ crud: CRUD });
                $("#PartialViewModalSaveButton").text("Save Changes");
                $("#PartialViewModalBody").html(partialViewResult);

            });
            break;
        case "Details":
            $.ajax({
                url: baseUrl + "/DynamicDetails",
                type: "GET",
                data: { CRUD: CRUD, PK_Id: DynamicPK_Id }
            }).done(function (partialViewResult) {
                $("#PartialViewModal").modal('show');
                $("#PartialViewModalTitle").text("Rep It -  " + CRUD);
                $("#PartialViewModalSaveButton").data("crud", CRUD);
                $("#PartialViewModalSaveButton").text("Close");
                $("#PartialViewModalBody").html(partialViewResult);
            });
            break;

        case "Delete":
            $.ajax({
                url: baseUrl + "/DynamicDelete",
                type: "GET",
                data: { CRUD: CRUD, PK_Id: DynamicPK_Id }
            }).done(function (partialViewResult) {
                $("#PartialViewModal").modal('show');
                $("#PartialViewModalTitle").text("Rep It -  " + CRUD);
                $("#PartialViewModalSaveButton").data("crud", CRUD);
                $("#PartialViewModalSaveButton").text("Delete");
                $("#PartialViewModalBody").html(partialViewResult);
            });
            break;

        default:
            $.ajax({
                url: baseUrl + "/DynamicEdit",
                type: "GET",
                data: { CRUD: CRUD, PK_Id: DynamicPK_Id }
            }).done(function (partialViewResult) {
                $("#PartialViewModal").modal('show');
                $("#PartialViewModalTitle").text("Rep It -  " + CRUD);
                $("#PartialViewModalSaveButton").data("crud", CRUD);
                $("#PartialViewModalBody").html(partialViewResult);
            });
            break;
    }


}


//This section handles the AJAX calls for stored procedures for the model save button
$("#PartialViewModalSaveButton").click(function () {
    var CRUD = $(this).data("crud");
    var form = $("#PartialViewModalBody").children('form');
    console.log(CRUD);
    console.log(form.serialize());
    switch (CRUD) {
        case "Create":
            $.ajax({
                url: form.attr("action"),
                method: "POST",  // post
                data: form.serialize(),
                success: function (partialResult) {
                    console.log("Success");
                    if (partialResult == "Add Confirmed") {
                        form.find(':input').each(function () {
                            switch (this.type) {
                                case 'password':
                                case 'select-multiple':
                                case 'select-one':
                                case 'text':
                                case 'number':
                                case 'textarea':
                                    $(this).val('');
                                    break;
                                case 'checkbox':
                                case 'radio':
                                    this.checked = false;
                            }
                        });
                    } else {
                        $("#PartialViewModalBody").html(partialResult);
                    }
                    UpdateDynamicTable();
                    // $("#PartialViewModalBody").html(partialResult);
                    //console.log(partialResult);
                }
            });
            break;
        case "Delete":
            $.ajax({
                url: form.attr("action"),
                method: "POST",  // post
                data: form.serialize(),
                success: function (partialResult) {
                    console.log("Case 'Deleted' triggered");
                    console.log(partialResult);

                    if (partialResult == "Delete Confirmed") {
                        $("#PartialViewModal").modal('hide');
                    } else {
                        $("#PartialViewModalBody").html(partialResult);
                    }
                    UpdateDynamicTable();
                }
            });
            break;

        case "Edit":

            $.ajax({
                url: form.attr("action"),
                method: "POST",  // post
                data: form.serialize(),
                success: function (partialResult) {
                    if (partialResult == "Edit Confirmed") {
                        $("#PartialViewModal").modal('hide');
                    } else {
                        $("#PartialViewModalBody").html(partialResult);
                    }
                    UpdateDynamicTable();
                }
            });
            break;
        case "Details":
            $("#PartialViewModal").modal('hide');
            break;
        default:
            break;
    }

});


