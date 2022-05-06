"use strict";
// Class definition

var SpecialtyDirectory = function () {
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        var datatable = $('#dtSpecialtyDirectory').DataTable({

            // datasource definition
            // data: {
            //     type: 'remote',
            //     source: {
            //         read: {
            //             url: '../../Specialty/GetSpecialtyDirectoryData/',
            //             method: 'POST',
            //             //contentType: 'application/json',
            //             params: {
            //                 query: {
            //                     SpecialtyId: $("#hiddenSpecialtyId").val()
            //                 },
            //             },
            //         },
            //     },
            //     pageSize: 10, // display 20 records per page
            //     serverPaging: true,
            //     serverFiltering: true,
            //     serverSorting: true,
            //     saveState: {
            //         cookie: false,
            //         webstorage: false,
            //     },
            // },
            responsive: true,
            stateSave: false,
            ajax: {
                url: '../../Specialty/GetSpecialtyDirectoryData/',
                method: 'POST',
                // data: function ( d ) {
                //     d.params.query.SpecialtyId = $("#hiddenSpecialtyId").val()
                // }
                data: {
                    query: {
                        SpecialtyId: $("#hiddenSpecialtyId").val(),
                    },
                }
            },

            // layout definition
            layout: {
                scroll: true, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false, // display/hide footer
                customScrollbar: false //disable metronic custom small scrollbar for the browse's default
            },
            rows: {
                autoHide: false,
            },

            //scrollY: '50vh',
            //scrollX: true,
            //scrollCollapse: true,
            //column sorting
            sortable: false,
            pagination: true,

            search: {
                input: $('#SearchString'),
                delay: 1000,
                regex: true,
            },
            // columns definition
            columns: [
                {
                    data: 'SpecialtyDirectoryId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    render: function (data, type, row, meta) {
                        return '<a value="' + data + '"  id="' + data + '" class="actionEdit DirectorySectionId btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit details"> \
                        <i class="las la-edit" style="font-size:18px;"></i> \
                            </a>';
                    },
                },
                {
                    data: 'DirectorySectionDescription'
                },
                {
                    data: 'LineOfBusinessName'
                },
                {
                    data: 'Company'
                }, 
                {
                    data: 'DirectoryTypeId',
                    render: function (data) {
                        if (data === 1 )
                            return 'Online';
                        else
                            return 'Paper';
                    }
                },
                {
                    data: 'EffectiveDate',
                    type: 'date',
                    render: function (data) {
                        return moment(data).format('MM[/]DD[/]YYYY');
                    }

                },
                {
                    data: 'EndDate',
                    type: 'date',
                    render: function (data) {
                        return moment(data).format('MM[/]DD[/]YYYY');
                    }
                },
                {
                    data: 'DisplayFlag',
                    render: function (data) {
                        if (data === 'Y')
                            return '<span class="badge badge-success">Y</span>';
                        else
                            return '<span class="badge badge-danger">N</span>';
                    }
                }],
            translate: {
                records: {
                    processing: 'Please wait...',
                    noRecords: 'No records found'
                }
            }
        });



        datatable.on('click', '.DirectorySectionId', function () {
            //var id = $(this).find('.DirectorySectionId')[0].attributes[1].value; 
            var id = $(this)[0].attributes.value.value;

            $('#SpecialtyLanguageArea').hide();

            $.ajax({
                url: '../../Specialty/LoadSpecialtyDirectory/',
                data: { SpecialtyDirectoryId: id },
                type: 'POST',
                success: function (result) {
                    //Switch partial views.
                    $('#SpecialtyDirectoryList').hide();
                    $('#Partial').html(result);

                    //Hide 'Add New' Directory Section button when editing an existing one.
                    $('#btnAddDirectorySection').hide();

                    //Initialize save and cancel buttons
                    $('#btnCancelSpecialtyDirectory').click(function () {
                        $('#SpecialtyDirectory').hide();
                        $('#SpecialtyDirectoryList').show();
                        $('#SpecialtyLanguageArea').show();

                        //Show "Add new" Directory Section button on cancel.
                        $('#btnAddDirectorySection').show();
                    });

                    //Initialize controllers
                    $('#ddlDirectorySection').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    });
                    $('#ddlDirectorySectionCompany').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    });
                    $('#ddlDirectorySectionLob').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    }).on("change", function (e) {
                        //Enable Company dropdown.
                        $("#ddlDirectorySectionCompany").prop("disabled", false);
                        $("#ddlDirectorySectionCompany").val(null);
                        $('#ddlDirectorySectionCompany').select2({
                            placeholder: 'Select',
                            minimumResultsForSearch: -1,
                            ajax:
                            {
                                url: '../../Specialty/GetCompanyByLob/',
                                dataType: 'json',
                                //type: "GET",
                                //delay: 250,
                                //quietMillis: 100,
                                data: function (params) {
                                    return {
                                        lobId: $('#ddlDirectorySectionLob').val()
                                    };
                                },
                                processResults: function (data) {
                                    return {
                                        results: $.map(data, function (obj) {
                                            return {
                                                id: obj.companyId,
                                                text: obj.companyCode
                                            };
                                        })
                                    };
                                },
                                cache: false
                            }
                        });
                    });

                    $('#ddlDirectoryType').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    });

                    // $('#formDirectorySection').validate({
                    //     errorClass: "text-danger",
                    //     errorLabelContainer: "#errorNoteDirectorySection",
                    //     rules: {
                    //         DirectorySection: { required: true },
                    //         DirectorySectionLoB: { required: true },
                    //         DirectorySectionCompany: { required: true },
                    //         DirectorySectionEffectiveDate: { required: true },
                    //         DirectorySectionEndDate: { required: true }

                    //     },
                    //     messages: {
                    //         DirectorySection: { required: "Directory Section is required." },
                    //         DirectorySectionLoB: { required: "Line of Business is required." },
                    //         DirectorySectionCompany: { required: "Company is required." },
                    //         DirectorySectionEffectiveDate: { required: "Effective Date is required." },
                    //         DirectorySectionEndDate: { required: "End Date is required." },
                    //     }
                    // });

                    $("#btnSave").click(function () {  // capture the click
                        
                        if ($("#formDirectorySection").valid()) {   // test for validity
                            $("#btnSave").click(function () {
                            $.ajax({
                                url: '../../Specialty/UpdateSpecialtyDirectory/',
                                data: {
                                    specialtyId: $('#hiddenSpecialtyId').val(),
                                    specialtyDirectoryId: $('#hiddenSpecialtyDirectoryId').val(),
                                    directorySectionId: $('#ddlDirectorySection').val(),
                                    companyId: $('#ddlDirectorySectionCompany').val(),
                                    lineOfBusinessId: $('#ddlDirectorySectionLob').val(),
                                    endDate: $('#dateEnd').val()
                                },
                                type: 'POST',
                                success: function (result) {
                                    $('#SpecialtyDirectory').hide();
                                    $('#DirectorySectionSqlError').hide();
                                    $('#SpecialtyDirectoryList').show();
                                    $('#SpecialtyLanguageArea').show();
                                    //Show "Add New" Directory Section button after updating record.
                                    $('#btnAddDirectorySection').show();
                                    datatable.reload();
                                },
                                error: function (xhr) {
                                    $('#DirectorySectionSqlError').show();
                                    $("#DirectorySectionSqlError").html(''); //Clear existing Sql error messages.
                                    $("#DirectorySectionSqlError").append(xhr.responseText);
                                    console.log('error : ' + xhr.status + ' - ' + xhr.statusText + ' - ' + xhr.responseText);
                                }
                                });
                            });
                        }
                    });
                },
                error: function (xhr) {
                    console.log('error : ' + xhr.status + ' - ' + xhr.statusText + ' - ' + xhr.responseText);
                }
            });



        });

//Events for filter controllers on /Details page.

        $('#ddlCompany').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1
        }).on('change', function () {
            datatable.search($(this).val(), 'CompanyId').draw();
            });

        $('#ddlLob').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1
        }).on('change', function () {
            datatable.search($(this).val(), 'LineOfBusinessId').draw();
        });

        $('#ddlType').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1
        }).on('change', function () {
            //var value = $(this).val();
            //if (value === 'P')
            //    value = 'Paper';
            //else
            //    value = 'Online';
            //datatable.search(value, 'DirectoryType');
            datatable.search($('option:selected', this).text(), 'DirectoryType').draw();
        });

        $('#chkActive').click(function () {
            if ($(this).prop("checked") == true) {
                datatable.column(7).search('Y').draw();
            }
            else if ($(this).prop("checked") == false) {
                datatable.column(7).search('N').draw();

            }
        });

        function delay(fn, ms) {
            let timer = 0;
            return function (...args) {
                clearTimeout(timer);
                timer = setTimeout(fn.bind(this, ...args), ms || 0);
            };
        }
    };

    return {
        // public functions
        init: function () {
            List();
        },
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    SpecialtyDirectory.init();
});