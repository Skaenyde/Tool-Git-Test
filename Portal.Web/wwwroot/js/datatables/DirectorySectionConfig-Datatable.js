"use strict";
// Class definition

var DirectorySectionConfigPage = function () {
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        var datatable = $('#dtDirectorySectionConfig').DataTable({
            // datasource definition
            // data: {
            //     type: 'remote',
            //     source: {
            //         read: {
            //             url: '../DirectorySectionConfig/GetDirectorySectionConfigData/',
            //             method: 'GET',
            //             //contentType: 'application/json',
            //             //params: {
            //             //    query: {
            //             //        directorySectionName: $("#txtSearch").val(),
            //             //        directorySectionDescription: $('#ddlLanguage').val(),
            //             //        SearchCompany: $("#ddlCompany option:selected").text(),
            //             //        SearchLoB: $("#ddlLoB option:selected").text(),
            //             //        SearchSpecialty: $("#ddlSpecialty option:selected").text()
            //             //    },
            //             //},
            //         },
            //     },
            //     pageSize: 10, // display 20 records per page
            //     serverPaging: false,
            //     serverFiltering: false,
            //     serverSorting: false,
            //     saveState: {
            //         cookie: false,
            //         webstorage: false,
            //     },
            // },

            ajax: {
                url: '../DirectorySectionConfig/GetDirectorySectionConfigData/',
                method: 'POST'
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

            //search: {
            //    input: $('#txtSearch'),
            //    delay: 400,
            //},  
            // columns definition
            columns: [
                //Directory Section Id
                {
                    data: 'DirectorySectionId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    render: function (data, type, row, meta) {
                        return '<a DirectorySectionId="'+ data +'" class="btn btn-sm btn-clean btn-icon btn-icon-md DirectorySectionId" title="Edit details"> \
                            <i class="la la-edit"></i> \
                            </a>';
                    }
                },
                //Directory Section Name
                {
                    data: 'DirectorySectionName'
                },
                //Directory Section Description
                {
                    data: 'DirectorySectionDescription'
                },
                //Directory section Code
                {
                    data: 'DirectorySectionCode'
                },
                //Online
                {
                    data: 'Online',
                    render: function (data) {
                        if (data === 'Y')
                            return '<span class="badge badge-success">Y</span>';
                        else
                            return '<span class="badge badge-danger">N</span>';
                    }
                },
                //Paper
                {
                    data: 'Paper',
                    render: function (data) {
                        if (data === 'Y')
                            return '<span class="badge badge-success">Y</span>';
                        else
                            return '<span class="badge badge-danger">N</span>';
                    }
                },
                //Update Date
                {
                    data: 'UpdateDate',
                    type: 'date',
                    sortable: false,
                    render: function (data) {
                        if (data == null) {
                            return ''; 
                        }
                        return moment(data).format('MM[/]DD[/]YYYY');
                    }
                },
                //Update By
                {
                    data: 'UpdatedBy'
                },
            ]
        });

        datatable.on('click', '.DirectorySectionId', function () {

            var id = this.attributes[0].value;
            var directorySectionName = this.closest('tr').children[1].innerHTML;
            var directorySectionDesc = this.closest('tr').children[2].innerHTML;
            var directorySectionCode = this.closest('tr').children[3].innerHTML;
            var directorySectionOnline = this.closest('tr').children[4].children[0].innerHTML;
            var directorySectionPaper = this.closest('tr').children[5].children[0].innerHTML; 
            var directorySectionStart = this.closest('tr').children[6].innerHTML;
            var directorySectionEnd = this.closest('tr').children[7].innerHTML;


            // Show Edit modal.
            $("#modal_EditDirectorySection").modal('show');


            //Initialize DatePickers
            // $('#dateEditEffectiveDate').datepicker({
            //     rtl: KTUtil.isRTL(),
            //     todayHighlight: true,
            //     orientation: "bottom left"
            // });
            // $('#dateEditEndDate').datepicker({
            //     rtl: KTUtil.isRTL(),
            //     todayHighlight: true,
            //     orientation: "bottom left"
            // });

            //Initialize Flatpickrs
            $('#dateEditEffectiveDate').flatpickr();
            $('#dateEditEndDate').flatpickr();

            // Set values to controllers
            $('#txtEditDirectorySectionName').val(directorySectionName);
            $('#txtEditDirectorySectionDescription').val(directorySectionDesc);
            $('#txtEditDirectorySectionCode').val(directorySectionCode);
            // $('#dateEditEffectiveDate').datepicker("setDate", directorySectionStart);
            // $('#dateEditEndDate').datepicker("setDate", directorySectionEnd);
            $('#dateEditEffectiveDate').flatpickr("defaultDate",directorySectionStart );
            $('#dateEditEndDate').flatpickr("defaultDate", directorySectionEnd);
            
            $('#hidDirectorySectionId').val(id);
            if (directorySectionOnline === 'Y')
                $('#chkEditOnline').prop("checked", true);
            else
                $('#chkEditOnline').prop("checked", false);
            if (directorySectionPaper === 'Y')
                $('#chkEditPaper').prop("checked", true);
            else
                $('#chkEditPaper').prop("checked", false); 
        });

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
    DirectorySectionConfigPage.init();
});
