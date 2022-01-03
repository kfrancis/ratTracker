$(function () {
    var l = abp.localization.getResource("RatTracker");
	
	var schoolService = window.ratTracker.schools.schools;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Schools/CreateModal",
        scriptUrl: "/Pages/Schools/createModal.js",
        modalClass: "schoolCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Schools/EditModal",
        scriptUrl: "/Pages/Schools/editModal.js",
        modalClass: "schoolEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			address1: $("#Address1Filter").val(),
			address2: $("#Address2Filter").val(),
			address3: $("#Address3Filter").val(),
			city: $("#CityFilter").val(),
			postalCode: $("#PostalCodeFilter").val()
        };
    };

    var dataTable = $("#SchoolsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"],[5, "asc"]],
        ajax: abp.libs.datatables.createAjax(schoolService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('RatTracker.Schools.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('RatTracker.Schools.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    schoolService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" },
			{ data: "address1" },
			{ data: "address2" },
			{ data: "address3" },
			{ data: "city" },
			{ data: "postalCode" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewSchoolButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
});
