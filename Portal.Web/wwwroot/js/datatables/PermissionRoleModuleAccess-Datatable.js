"use strict";
// Class definition

var RoleModuleAccessPage = function () {
    // Private functions

    // This is encapsulates everything inside the portlet including controls relevant 
    // to the datatable which are apart from it.
    var List = function () {

        var datatable = $('#dtRoleModuleAccess').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '../Permission/GetRoleModuleAccessList/',
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
                    field: 'RolePermissionId',
                    title: '',
                    sortable: false,
                    width: 30,
                    selector: false,
                    textAlign: 'center',
                    template: function (data, type, row, meta) {
                        return '<a RolePermissionId="' + data.RolePermissionId + '" class="btn btn-sm btn-clean btn-icon btn-icon-md RolePermissionId" title="Edit details"> \
                            <i class="la la-edit"></i> \
                            </a>';
                    },
                },
                {
                    field: 'RoleDesc',
                    title: 'Role',
                },
                {
                    field: 'ModuleDesc',
                    title: 'Module',
                },
                {
                    field: 'PermissionTypeDesc',
                    title: 'Permission',
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
                    field: 'UpdateBy',
                    title: 'User'
                }

            ],

        });


        datatable.on('click', '.RolePermissionId', function () {
            var id = this.attributes[0].value;
            var roleModuleAccessRoleDesc = this.closest('tr').children[1].children[0].innerHTML;
            var roleModuleAccessModuleDesc = this.closest('tr').children[2].children[0].innerHTML;
            var roleModuleAccess = this.closest('tr').children[3].children[0].innerHTML;

            $('#txtRoleModuleAccessRoleDesc').val(roleModuleAccessRoleDesc);
            $('#txtRoleModuleAccessModuleDesc').val(roleModuleAccessModuleDesc);
            //$('#ddlRoleModuleAccess').val(???);
            $('#hidRolePermissionId').text(id);

            if (roleModuleAccess == 'Read')
                $('#ddlRoleModuleAccess').val(1).trigger('change');
            if (roleModuleAccess == 'Edit')
                $('#ddlRoleModuleAccess').val(2).trigger('change');
            if (roleModuleAccess == 'None')
                $('#ddlRoleModuleAccess').val(3).trigger('change');

            $('#modal_EditRoleModuleAccess').modal();
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
    RoleModuleAccessPage.init();
});
