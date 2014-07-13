/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;
        this.controls = {
            seaList_dateAddbegintime: $(".search-" + selector + " .date-addBeginTime"),
            seaList_dateAddendtime: $(".search-" + selector + " .date-addEndTime"),
            seaList_txtNickname: $(".search-" + selector + " .text-nickname"),
            seaList_txtUsermail: $(".search-" + selector + " .text-usermail"),
            seaList_btnSearch: $(".search-" + selector + " .button-search"),
            seaList_btnClear: $(".search-" + selector + " .button-clear"),
            gridList: $(".grid-" + selector + " .grid-table")
        }

        this.initSearch();
        this.initGrid();
    }
    Page.prototype.initSearch = function () {
        var self = this;

        self.controls.seaList_dateAddbegintime.datetimebox({ value: new Date().toDateString() });
        self.controls.seaList_dateAddendtime.datetimebox({ value: new Date().getDate().toString() });
        self.controls.seaList_txtNickname.validatebox();
        self.controls.seaList_txtUsermail.validatebox();

        self.controls.seaList_btnSearch.linkbutton({ iconCls: "icon-search" }).click(function () {
            self.controls.gridList.datagrid("load", {
                addBegintime: self.controls.seaList_dateAddbegintime.datetimebox("getValue"),
                addEndtime: self.controls.seaList_dateAddendtime.datetimebox("getValue"),
                nickname: self.controls.seaList_txtNickname.val(),
                usermail: self.controls.seaList_txtUsermail.val(),
            });
        });
        self.controls.seaList_btnClear.linkbutton({ iconCls: "icon-reload" }).click(function () {
            self.controls.seaList_dateAddbegintime.datetimebox("setValue", "");
            self.controls.seaList_dateAddendtime.datetimebox("setValue", "");
            self.controls.seaList_txtNickname.val("");
            self.controls.seaList_txtUsermail.val("");
        });
    }
    Page.prototype.initGrid = function () {
        var self = this;

        self.controls.gridList.datagrid({
            title: "客户账户信息列表",
            url: "/admin/user/list",
            fit: true,
            rownumbers: true,
            pagination: true,
            queryParams: {
                addBeginTime: self.controls.seaList_dateAddbegintime.datetimebox("getValue"),
                addEndTime: self.controls.seaList_dateAddendtime.datetimebox("getValue")
            },
            singleSelect: true,
            pageSize: 20,
            columns: [
                [
                    {
                        field: "addtime", title: "注册时间", width: 130, sortable: true,
                        formatter: function (value) {
                            if (value) {
                                var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                                return date.format("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    },
                    { field: "nickname", title: "昵称", width: 100, sortable: true },
                    { field: "usermail", title: "邮箱", width: 150, sortable: true },
                    {
                        field: "state", title: "启用", width: 60, sortable: true,
                        formatter: function (value) {
                            if (value == true) {
                                return "是";
                            } else if (value == false) {
                                return "否";
                            }
                        }
                    },
                    { field: "score", title: "积分", width: 60, sortable: true },
                    { field: "experience", title: "经验值", width: 60, sortable: true },
                    { field: "order_num", title: "订单总数", width: 80, sortable: true },
                    { field: "evaluate_num", title: "评测数", width: 60, sortable: true },
                    { field: "collect_num", title: "收藏数", width: 60, sortable: true },
                    { field: "follow_num", title: "关注数", width: 60, sortable: true },
                    { field: "fans_num", title: "粉丝数", width: 60, sortable: true },
                    { field: "blog_num", title: "日志数", width: 60, sortable: true },
                    { field: "coupon_num", title: "优惠券", width: 60, sortable: true },
                    {
                        field: "if_super", title: "达人会员", width: 80, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    }
                ]
            ],
            toolbar: [
                {
                    id: "btnGetUser-" + self.selector,
                    text: '详细',
                    disabled: true,
                    iconCls: 'icon-09-08',
                    handler: function () { self.getUser(); }
                }, '-', {
                    id: "btnAddUser-" + self.selector,
                    text: '新增',
                    iconCls: 'icon-01-02',
                    handler: function () { self.savUser(true); }
                }, '-', {
                    id: "btnUdpUser-" + self.selector,
                    text: '修改',
                    disabled: true,
                    iconCls: 'icon-17-20',
                    handler: function () { self.savUser(false); }
                }, '-', {
                    id: "btnDelUser-" + self.selector,
                    text: '删除',
                    disabled: true,
                    iconCls: 'icon-07-08',
                    handler: function () { self.delUser(); }
                }, '-'
            ],
            onSelect: function (rowIndex, rowData) { self.setToolState(); },
            onUnselect: function (rowIndex, rowData) { self.setToolState(); },
            onSelectAll: function (rows) { self.setToolState(); },
            onUnselectAll: function (rows) { self.setToolState(); }
        });
        $.extend(self.controls, {
            gridList_btnGetUser: $("#btnGetUser-" + self.selector),
            gridList_btnAddUser: $("#btnAddUser-" + self.selector),
            gridList_btnUdpUser: $("#btnUdpUser-" + self.selector),
            gridList_btnDelUser: $("#btnDelUser-" + self.selector)
        });
    }
    Page.prototype.setToolState = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        if (row) {
            self.controls.gridList_btnGetUser.linkbutton("enable");
            self.controls.gridList_btnUdpUser.linkbutton("enable");
            self.controls.gridList_btnDelUser.linkbutton("enable");
        } else {
            self.controls.gridList_btnGetUser.linkbutton("disable");
            self.controls.gridList_btnUdpUser.linkbutton("disable");
            self.controls.gridList_btnDelUser.linkbutton("disable");
        }
    }
    Page.prototype.getUser = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        $('<div></div>').dialog({
            href: "/admin/user/detail?userid=" + row.userid,
            title: "客户详细资料--" + row.nickname,
            iconCls: 'icon-09-08',
            width: 680,
            top: 100,
            modal: true,
            onClose: function () { $(this).dialog("destroy"); }
        });
    }
    Page.prototype.savUser = function (isAdd) {
        var self = this;

        var row = isAdd ? null : self.controls.gridList.datagrid("getSelected");
        var dlgSaveUser = $('<div></div>').dialog({
            href: "/admin/user/save?userid=" + (isAdd ? "" : row.userid),
            title: (isAdd ? "新增" : "修改") + "客户" + (isAdd ? "" : ("--" + row.nickname)),
            iconCls: (isAdd ? 'icon-01-02' : 'icon-17-20'),
            width: 470,
            top: 100,
            modal: true,
            buttons: [
                {
                    iconCls: "icon-ok",
                    text: "保存",
                    handler: function () {
                        LW.UserSave.onSubmit(function (data) {
                            if (data.status == 1) {
                                if (isAdd) {
                                    self.controls.gridList.datagrid("insertRow", { index: 0, row: data.data });
                                } else {
                                    var index = self.controls.gridList.datagrid("getRowIndex", row);
                                    self.controls.gridList.datagrid("updateRow", { index: index, row: data.data });
                                    self.controls.gridList.datagrid("selectRow", index);
                                }
                                dlgSaveUser.dialog("close");
                                showMsg({ msg: data.msg, icon: "success" });
                            } else {
                                showMsg({ msg: data.msg, icon: "error" });
                            }
                        });
                    }
                }
            ],
            onClose: function () { $(this).dialog("destroy"); }
        });
    }
    Page.prototype.delUser = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        $.messager.confirm("系统提示！", "您确定要删除 " + row.nickname + " 的账号么？", function (r) {
            if (!r) return false;
            $.post("/admin/user/delete", { userid: row.userid }, function (data) {
                if (data.status == 1) {
                    var index = self.controls.gridList.datagrid("getRowIndex", row);
                    self.controls.gridList.datagrid("deleteRow", index);
                    self.setToolState();
                    showMsg({ msg: data.msg, icon: "success" });
                } else {
                    showMsg({ msg: data.msg, icon: "error" });
                }
            }, "json");
        });
    }

    new Page("user-list");
});