"use strict";

// Class definition
var SpecialtyLanguage = function () {
    // Shared variables
    var languageDatatable;
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        languageDatatable = $('#dtSpecialtyLanguage').DataTable({
            // datasource definition
            // data: {
            //     type: 'remote',
            //     source: {
            //         read: {
            //             url: '../../Specialty/GetSpecialtyLanguageData/',
            //             method: 'POST',
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
            serverSide: true,
            stateSave: false,
            ajax: {
                url: '../../Specialty/GetSpecialtyLanguageData/',
                method:'POST',
                data: {
                    query: {
                        SpecialtyId: $("#hiddenSpecialtyId").val()
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
                    data: 'SpecialtyLanguageId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    render: function (data, type, row, meta) {
                        return '<a id="' + data + '" value="' + data +'" class="SpecialtyLanguageId btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit details"> \
                            <i class="la la-edit" style="font-size:18px;"></i> \
                            </a>';
                    },
                },
                {
                    data: 'Language'
                },
                {
                    data: 'Lob'
                },
                {
                    data: 'Company'
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
                    data: 'DisplayName'
                },
                {
                    data: 'Active',
                    visible: false
                }
            ],
            translate: {
                records: {
                    processing: 'Please wait...',
                    noRecords: 'No records found'
                }
            }
        });


        languageDatatable.on('click', '.SpecialtyLanguageId', function () {
           // var id = $(this).find('.SpecialtyLanguageId')[0].attributes[1].value; //THIS IS IT!
            var id = $(this)[0].attributes.value.value;


            $('#DirectorySectionArea').hide();

            //Executes when row 'Edit Icon is clicked'
            $.ajax({
                url: '../../Specialty/LoadSpecialtyLanguage/', //Controller action which returns partial view
                data: { specialtyLanguageId: id },
                type: 'POST',
                success: function (result) {

                    //Switch partial views
                    $('#SpecialtyLanguageList').hide();
                    $('#PartialSpecialtyLanguageList').html(result);
                    //Hide "Add New" Specialty Language button when editing a existing one.
                    $('#btnAddNewSpecialtyLanguage').hide();
                    $('#btnSpecialtyLanguageCancel').click(function () {
                        $('#SpecialtyLanguage').hide();
                        $('#SpecialtyLanguageList').show();
                        $('#DirectorySectionArea').show();

                        //Show "Add new" Specialty Language button on cancel.
                        $('#btnAddNewSpecialtyLanguage').show();
                    });
                    $('#ddlSpecialtyLanguage').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    });
                    $('#ddlSpecialtyLanguageCompany').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    });
                    $('#ddlSpecialtyLanguageLob').select2({
                        placeholder: 'Select',
                        minimumResultsForSearch: -1
                    }).on("change", function (e) {
                        //Enable Company dropdown.
                        $("#ddlSpecialtyLanguageCompany").prop("disabled", false);
                        $("#ddlSpecialtyLanguageCompany").val(null);

                        $('#ddlSpecialtyLanguageCompany').select2({
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
                                        lobId: $('#ddlSpecialtyLanguageLob').val()
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

                   
                    $('#formSpecialtyLanguage').validate({
                        errorClass: "text-danger",
                        errorLabelContainer: "#errorNoteSpecialtyLanguage",
                        rules: {
                            SpecialtyLanguage: { required: true },
                            SpecialtyLoB: { required: true },
                            SpecialtyCompany: { required: true },
                            SpecialtyDisplayName: { required: true },
                            SpecialtyEffectiveDate: { required: true },
                            SpecialtyEndDate: { required: true },

                        },
                        messages: {
                            SpecialtyLanguage: { required: "Specialty Languagse is required." },
                            SpecialtyLoB: { required: "Line of Business is required." },
                            SpecialtyCompany: { required: "Company is required." },
                            SpecialtyDisplayName: { required: "Display Name is required." },
                            SpecialtyEffectiveDate: { required: "Effective Date is required." },
                            SpecialtyEndDate: { required: "End Date is required." },
                        }
                    });
                    $("#btnSaveSpecialtyLanguage").click(function () {  // capture the click
                        
                        if ($("#formSpecialtyLanguage").valid()) {   // test for validity
                            $('#btnSaveSpecialtyLanguage').click(function () {
                            $.ajax({
                                url: '../../Specialty/UpdateSpecialtyLanguage/', //Controller action for updating changes on DB.
                                data: {
                                    specialtyId: $('#hiddenSpecialtyId').val(),
                                    specialtyLanguageId: $('#hiddenSpecialtyLanguageId').val(),
                                    languageId: $('#ddlSpecialtyLanguage').val(),
                                    companyId: $('#ddlSpecialtyLanguageCompany').val(),
                                    lineOfBusinessId: $('#ddlSpecialtyLanguageLob').val(),
                                    effectiveDate: $('#dateSpecialtyLanguageEffective').val(),
                                    endDate: $('#dateSpecialtyLanguageEnd').val(),
                                    specialtyDisplayName: $('#txtSpecialtyLanguageDisplayName').val()
                                },
                                type: 'POST',
                                success: function (result) {
                                    $('#SpecialtyLanguage').hide();
                                    $('#SpecialtyLanguageSqlError').hide();
                                    $('#SpecialtyLanguageList').show();
                                    $('#DirectorySectionArea').show();
                                    //Show "Add new" Specialty Language button after updating record.
                                    $('#btnAddNewSpecialtyLanguage').show();
                                    languageDatatable.reload();

                                },
                                error: function (xhr) {
                                    $('#SpecialtyLanguageSqlError').show();
                                    $("#SpecialtyLanguageSqlError").html(''); //Clear existing Sql error messages.
                                    $("#SpecialtyLanguageSqlError").append(xhr.responseText);
                                    console.log(xhr.responseText);
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

        $('#ddlCompany').on('change', function () {
            languageDatatable.column(3).search($(this).val()).draw();
            languageDatatable.draw();
            //Client Side solution
            // let textValue = $('option:selected', this).text();
            // if (textValue != ''){
            //     textValue = '^' + textValue +'$';
            //     languageDatatable.column(3).search(textValue, true, false).draw();
            //     return;
            // }
            // languageDatatable.column(3).search(textValue).draw();
            
        });

        $('#ddlLob').on('change', function () {
            // languageDatatable.search($(this).val(), 'LineOfBusinessId').draw();
            languageDatatable.search($('option:selected', this).text(), 'LineOfBusinessId').draw();

        });

        $('#ddlLanguage').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1
        }).on('change', function () {
            // languageDatatable.column(1).search($(this).val(), 'LanguageId').draw();
            languageDatatable.search($('option:selected', this).text(), 'LanguageId').draw();

        });

        $('#ddlLob').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1
        }).on('change', function () {
            // languageDatatable.search($(this).val(), 'LineOfBusinessId').draw();
            languageDatatable.search($('option:selected', this).text(), 'LineOfBusinessId').draw();

            });

        $('#chkActive').click(function () {
            if ($(this).prop("checked") == true) {
                languageDatatable.column(7).search('true').draw();
            }
            else if ($(this).prop("checked") == false) {
                languageDatatable.column(7).search('false').draw();
            }
        });

        // languageDatatable.on('draw', function () {
        //     KTMenu.createInstances();
        // });
    };

    return {
        // public functions
        init: function () {
            List();
        },
    };
}();

//Initialize all JS related once page is fully loaded.
// jQuery(document).ready(function () {
//     SpecialtyLanguage.init();
// });
KTUtil.onDOMContentLoaded(function () {
    SpecialtyLanguage.init();
});