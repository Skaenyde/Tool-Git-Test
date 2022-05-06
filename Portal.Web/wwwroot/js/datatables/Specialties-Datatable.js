"use strict";

// Class definition
var SpecialtiesListPage = function () {
    // Shared variables
    var table;
    var datatable;

    // Private functions
    var initDatatable = function () {
        
        datatable = $(table).DataTable({
            responsive: true,
            processing: false,
            serverSide: false,
            stateSave: false,

            // datasource definition
            ajax: {
                url: '../Specialty/GetListData/',
                type: 'GET',
            },

            columns: [
                { 
                    data: 'SpecialtyId',
                    sortable: false,
                    selector: false,
                    render: function (data, type, row, meta) {
                        return '<a href="Details/' + data + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit details"> \
                        <i class="las la-edit" style="font-size:18px;"></i> \
                            </a>';
                    },
                },
                { 
                    data: 'SpecialtyName'
                },
                { data: 'LanguageName' },
                { data: 'LineOfBusinessName' },
                { data: 'CompanyName' },
                { data: 'DisplayName' },
                { 
                    data: 'EffectiveDate',
                    render: function (data) {
                        return moment(data).format('MM[/]DD[/]YYYY');
                    }
                },
                { 
                    data: 'EndDate',
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
                }
            ],

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


        });
    }

    //Filter events

    $('#ddlSpecialty').on('change', function () {
        datatable.search(validateValue($(this)), 'SpecialtyName').draw();  
    });

    $('#ddlLanguage').on('change', function () {
        datatable.search(validateValue($(this)), 'LanguageName').draw(); 
    });

    $('#ddlCompany').on('change', function () {
        datatable.search(validateValue($(this)), 'CompanyName').draw();
    }); 

    $('#ddlLoB').on('change', function () {
        datatable.search(validateValue($(this)), 'LineOfBusinessName').draw();
    });

    $('#ddlDisplayFlag').on('change', function () {
        // datatable.search(validateValue($(this)), 'DisplayFlag').draw();
        datatable.column(8).search(validateValue($(this))).draw();
    });

    function validateValue(element) {
        var value = $('option:selected', element).text().toLowerCase();
        if (value.length == 1) return value.toUpperCase();
        return (value === "all") ? null : value;
    }   



    // Handle specialty filter dropdown
    // var handleStatusFilter = () => {
    //     const filterStatus = document.querySelector('[data-SpecialtyList-filter="specialty"]');
    //     $(filterStatus).on('change', e => {
    //         let value = e.target.value;
    //         if (value === 'all') {
    //             value = '';
    //         }
    //         datatable.column(1).search(value).draw();
    //     });
    // }

    // // Hook export buttons
    var exportButtons = () => {
        const documentTitle = 'Provider Directory - Specialties';
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle
                }
            ]
        }).container().appendTo($('#dtSpecialties_export'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#dtSpecialties_export_menu [data-kt-export]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('#dtSpecialties');

            if ( !table ) {
                return;
            }

            initDatatable();
            exportButtons();
            handleSearchDatatable();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    SpecialtiesListPage.init();
    $('#ddlSpecialty').select2({
        closeOnSelect: true,
    });

});