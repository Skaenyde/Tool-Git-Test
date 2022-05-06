"use strict";
// Class definition

var SpecialtiesListPage = function () {
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        var datatable = $('#dtPharmacies').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '../Pharmacy/GetListData/',
                        method: 'POST',
                        //contentType: 'application/json',
                        params: {
                            query: {
                                //SearchString: $("#SearchString").val(),
                                //LineOfBusinessId: $('#ddlLoB').val(),
                                //CompanyId: $("#ddlCompany").val(),
                                //County: $("#ddlCounty").val(),
                                //City: $("#ddlCity").val(),
                                //StateCode: $("#ddlState").val()
                            },
                        },
                    },
                },
                pageSize: 10, // display 20 records per page
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
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
                    field: 'PharmacyStatusId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    template: function (data, type, row, meta) {
                        return '<a href="Details/' + data.PharmacyStatusId + '" class="btn btn-sm btn-clean btn-icon btn-icon-md" title="Edit details"> \
                            <i class="la la-edit"></i> \
                            </a>';
                    },
                },
                {
                    field: 'NPI',
                    title: 'NPI'
                },
                {
                    field: 'NCPDP',
                    title: 'NCPDP'
                },
                {
                    field: 'PharmacyName',
                    title: 'Pharmacy Name'
                },
                {
                    field: 'CompanyName',
                    title: 'Company Name'
                },
                {
                    field: 'Address',
                    title: 'Address'

                },
                {
                    field: 'CityName',
                    title: 'City'
                },
                {
                    field: 'County',
                    title: 'County'
                },
                {
                    field: 'StateCode',
                    title: 'State'
                }],
            translate: {
                records: {
                    processing: 'Please wait...',
                    noRecords: 'Please select filters'
                }
            }
        });

        //$('#ddlCompany, #ddlLoB').selectpicker();

        //$('#SearchString').keyup(delay(function (e) {
        //    //console.log('Time\'s up!', this.value);
        //    datatable.search($(this).text(), 'SearchString');
        //}, 1000));

        $('#ddlCompany').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1,
            allowClear: true
        }).on('change', function () {
            datatable.search($(this).val(), 'CompanyId');
            });

        $('#ddlLoB').select2({
            placeholder: 'Select',
            minimumResultsForSearch: -1,
            allowClear: true
        }).on('change', function () {
            datatable.search($(this).val(), 'LineOfBusinessId');
        });



        $('#ddlCounty').select2({
            placeholder: 'Select',
            multiple: true,
            minimumInputLength: 3,
            allowClear: true,
            ajax:
            {
                url: '../Pharmacy/GetCounty/',
                dataType: 'json',
                //type: "GET",
                //delay: 250,
                //quietMillis: 100,
                data: function (params) {
                    return {
                        county: params.term
                    };
                },
                processResults: function (data) {
                    return {
                        results: $.map(data, function (obj) {
                            return {
                                id: obj.text,
                                text: obj.text
                            };
                        })
                    };
                },
                cache: false
            }
        }).on("change", function (e) {
                datatable.search($('#ddlCounty').val(), 'County');
            });

        $('#ddlState').select2({
            placeholder: 'Select',
            multiple: true,
            allowClear: true
        }).on('change', function () {
            datatable.search($(this).val(), 'StateCode');
            });

        $('#ddlCity').select2({
            placeholder: 'Select',
            multiple: true,
            minimumInputLength: 3,
            allowClear: true,
            ajax:
            {
                url: '../Pharmacy/GetCity/',
                dataType: 'json',
                //type: "GET",
                //delay: 250,
                //quietMillis: 100,
                data: function (params) {
                    return {
                        city: params.term
                    };
                },
                processResults: function (data) {
                    return {
                        results: $.map(data, function (obj) {
                            return {
                                id: obj.cityName,
                                text: obj.cityName
                            };
                        })
                    };
                },
                cache: false
            }
        }).on("change", function (e) {
                datatable.search($('#ddlCity').val(), 'City');
            });

        $('#chkActive').click(function () {
            if ($(this).prop("checked") == true) {
                datatable.search('true', 'Active');
            }
            else if ($(this).prop("checked") == false) {
                datatable.search(false, 'Active');

            }
        });

        //Clear all filters.
        $('#btnClear').on('click', function () {

            //Reset headers before request.
            datatable.setDataSourceParam('query.County', null);
            datatable.setDataSourceParam('query.City', null);
            datatable.setDataSourceParam('query.StateCode', null);
            datatable.setDataSourceParam('query.CompanyId', null);
            datatable.setDataSourceParam('query.LineOfBusinessId', null);
            datatable.setDataSourceParam('query.SearchString', null);

            //Clear controls.
            $('#ddlCounty').val(null).text(null);
            $('#ddlCity').val(null).text(null);
            $('#SearchString').val(null);
            $('#ddlCompany').val(null).trigger('change');
            $('#ddlLoB').val(null).trigger('change');
            $('#ddlState').val(null).trigger('change');

            datatable.load();


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

//Initialize all JS related once page is fully loaded.
jQuery(document).ready(function () {
    SpecialtiesListPage.init();
});
