"use strict";
// Class definition

var RoleCompanyAccessPage = function () {
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        var datatable = $('#dtRoleCompanyAccess').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '../Permission/GetRoleCompanyAccessList/',
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
                    field: 'RoleCompanyAccessId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    template: function (data, type, row, meta) {
                        return '<a RoleCompanyAccessId="' + data.RoleCompanyAccessId + '" class="btn btn-sm btn-clean btn-icon btn-icon-md RoleCompanyAccessId" title="Edit details"> \
                            <i class="la la-edit"></i> \
                            </a>';
                    },
                },
                {
                    field: 'RoleAccessDesc',
                    title: 'Access Description',
                },
                {
                    field: 'RecordActionType',
                    title: 'Access',
                    template: function (data) {
                        if (data.RecordActionType === 'I')
                            return '<span class="kt-badge kt-badge--success kt-badge--inline kt-badge--pill">Y</span>';
                        else
                            return '<span class="kt-badge kt-badge--danger kt-badge--inline kt-badge--pill">N</span>';
                    }
                },
                {
                    field: 'CreateDate',
                    title: 'Created',
                    template: function (data) {
                        return moment(data.CreateDate).format('MM[/]DD[/]YYYY');
                    }
                },
                {
                    field: 'CreatedBy',
                    title: 'User'
                },
                {
                    field: 'UpdateDate',
                    title: 'Updated',
                    template: function (data) {
                        if ( data.UpdateDate == null) {
                            return '';
                        }
                        else {
                            return moment(data.UpdateDate).format('MM[/]DD[/]YYYY');
                        }
                    }
                },
                {
                    field: 'UpdatedBy',
                    title: 'User'
                }

            ],

        });


        datatable.on('click', '.RoleCompanyAccessId', function () {
            var id = this.attributes[0].value;
            var RoleAccessCompanyDesc = this.closest('tr').children[1].children[0].innerHTML; 
            var hasAccess = this.closest('tr').children[2].children[0].children[0].innerHTML;

            if (hasAccess == 'Y')
                $('#chkRoleCompanyAccessIsActive').prop("checked", true);
            else
                $('#chkRoleCompanyAccessIsActive').prop("checked", false);


            $('#txtRoleCompanyAccessDesc').val(RoleAccessCompanyDesc);
            $('#hidRoleCompanyAccessId').text(id);
            $('#modal_EditRoleCompanyAccess').modal();

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
    RoleCompanyAccessPage.init();
});
