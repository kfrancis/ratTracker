$(function () {
    var l = abp.localization.getResource("RatTracker");
	
	var resultService = window.ratTracker.results.results;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Results/CreateModal",
        scriptUrl: "/Pages/Results/createModal.js",
        modalClass: "resultCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Results/EditModal",
        scriptUrl: "/Pages/Results/editModal.js",
        modalClass: "resultEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            testDateMin: $("#TestDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			testDateMax: $("#TestDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			age: $("#AgeFilter").val(),
			outcome: $("#OutcomeFilter").val(),
			schoolId: $("#SchoolIdFilter").val()
        };
    };

    var dataTable = $("#ResultsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(resultService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('RatTracker.Results.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.result.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('RatTracker.Results.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    resultService.delete(data.record.result.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{
                data: "result.testDate",
                render: function (testDate) {
                    if (!testDate) {
                        return "";
                    }
                    
					var date = Date.parse(testDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            },
            {
                data: "result.age",
                render: function (age) {
                    if (age === undefined ||
                        age === null) {
                        return "";
                    }

                    var localizationKey = "Enum:AgeBrackets:" + age;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
            {
                data: "result.outcome",
                render: function (outcome) {
                    if (outcome === undefined ||
                        outcome === null) {
                        return "";
                    }

                    var localizationKey = "Enum:TestOutcome:" + outcome;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
            {
                data: "school.name",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewResultButton").click(function (e) {
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
