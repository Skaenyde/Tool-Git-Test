"use strict";
// Class definition

var SpecialtiesListPage = function () {
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        var datatable = $('#dtSpecialties').KTDatatable({
            reposive: true,
            dom: 'Bfrtip',
            buttons: [
                'excelHtml5'
            ],
            processing: true,
            serverSide: true,
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '../Specialty/GetListData/',
                        method: 'GET',
                        //contentType: 'application/json',
                        //params: {
                        //    query: {
                        //        SearchString: $("#txtSearch").val(),
                        //        LanguageName: $('#ddlLanguage').val(),
                        //        SearchCompany: $("#ddlCompany option:selected").text(),
                        //        SearchLoB: $("#ddlLoB option:selected").text(),
                        //        SearchSpecialty: $("#ddlSpecialty option:selected").text()
                        //    },
                        //},
                    },
                },
                pageSize: 10, // display 20 records per page
                serverPaging: false,
                serverFiltering: false,
                serverSorting: false,
                saveState: {
                    cookie: false,
                    webstorage: false,
                },
            },

            // layout definition
            layout: {
                scroll: true, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false, // display/hide footer
            },
            rows: {
                autoHide: false,
            },
            

            //column sorting
            sortable: true,
            pagination: true,

            search: {
                input: $('#txtSearch'),
                delay: 400,
            },  

            // columns definition
            columns: [
                {
                    field: 'SpecialtyId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    template: function (data, type, row, meta) {
                        return '<a href="Details/' + data.SpecialtyId + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit details"> \
                            <i class="la la-edit"></i> \
                            </a>';
                    },
                },
                {
                    field: 'SpecialtyName',
                    title: 'Specialty',
                },
                {
                    field: 'LanguageName',
                    title: 'Language',
                },
                {
                    field: 'LineOfBusinessName',
                    title: 'LoB'
                },
                {
                    field: 'CompanyName',
                    title: 'Company', 
                    
                },
                {
                    field: 'DisplayName',
                    title: 'Directory Display Name'
                },
                {
                    field: 'EffectiveDate',
                    title: 'Effective Date',
                    type: 'date',
                    sortable: false,
                    template: function (data) {
                        return moment(data.EffectiveDate).format('MM[/]DD[/]YYYY');
                    }
                },
                {
                    field: 'EndDate',
                    title: 'End Date',
                    type: 'date',
                    sortable: false,
                    template: function (data) {
                        return moment(data.EndDate).format('MM[/]DD[/]YYYY');
                    }
                },
                {
                    field: 'DisplayFlag',
                    title: 'Display Flag',
                    template: function (data) {
                        if (data.DisplayFlag === 'Y')
                            return '<span class="kt-badge kt-badge--success kt-badge--inline kt-badge--pill">Y</span>';
                        else
                            return '<span class="kt-badge kt-badge--danger kt-badge--inline kt-badge--pill">N</span>';
                    }
                }],

        });

        $('#export_excel').on('click', function (e) {
            e.preventDefault();
            datatable.button(0).trigger();
        });

        $('#ddlLanguage').on('change', function () {
            datatable.search(validateValue($(this)), 'LanguageName'); //SeachLanguage
        });

        $('#ddlCompany').on('change', function () {
            datatable.search(validateValue($(this)), 'CompanyName'); //SearchCompanyy
        }); 

        $('#ddlSpecialty').on('change', function () {
            datatable.search(validateValue($(this)), 'SpecialtyName');  //SearchSpecialty
        });

        $('#ddlLoB').on('change', function () {
            datatable.search(validateValue($(this)), 'LineOfBusinessName');
        });

        $('#ddlDisplayFlag').on('change', function () {
            datatable.search(validateValue($(this)), 'DisplayFlag');
        });

        $('#ddlStatus').on('change', function () {
            datatable.search(validateValue($(this)), 'Status');
        });

        function validateValue(element) {
            var value = $('option:selected', element).text().toLowerCase();
            return (value === "all") ? null : value;
        }   

        $('#ddlSpecialty, #ddlCompany, #ddlLoB, #ddlLanguage, #ddlStatus, #ddlDisplayFlag').selectpicker();
    };

    return {
        // public functions
        init: function () {
            List();
        },
    };
}();

//Initialize all JS related once page is fully loaded.
jQuery(document).ready(function () {
    SpecialtiesListPage.init();
});
