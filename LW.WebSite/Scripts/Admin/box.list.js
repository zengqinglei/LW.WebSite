/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;
        this.controls = {
            seaList_dateAddbegintime: $(".search-" + selector + " .date-addBeginTime"),
            seaList_dateAddendtime: $(".search-" + selector + " .date-addEndTime"),
            seaList_txtName: $(".search-" + selector + " .text-name"),
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
        self.controls.seaList_txtName.validatebox();

        self.controls.seaList_btnSearch.linkbutton({ iconCls: "icon-search" }).click(function () {
            self.controls.gridList.datagrid("load", {
                addBegintime: self.controls.seaList_dateAddbegintime.datetimebox("getValue"),
                addEndtime: self.controls.seaList_dateAddendtime.datetimebox("getValue"),
                name: self.controls.seaList_txtName.val()
            });
        });
        self.controls.seaList_btnClear.linkbutton({ iconCls: "icon-reload" }).click(function () {
            self.controls.seaList_dateAddbegintime.datetimebox("setValue", "");
            self.controls.seaList_dateAddendtime.datetimebox("setValue", "");
            self.controls.seaList_txtName.val('');
        });
    }
    Page.prototype.initGrid = function () {
        var self = this;

        self.controls.gridList.datagrid({
            title: "产品盒子列表",
            url: "/admin/box/list",
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
                        field: "addtime", title: "添加时间", width: 130, sortable: true,
                        formatter: function (value) {
                            if (value) {
                                var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                                return date.format("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    },
                    { field: "name", title: "盒子名称", width: 100, sortable: true },
                    { field: "category", title: "商品分类", width: 150, sortable: true },
                    { field: "quantity", title: "数量", width: 60, sortable: true },
                    {
                        field: "starttime", title: "开始时间", width: 130, sortable: true,
                        formatter: function (value) {
                            if (value) {
                                var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                                return date.format("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    },
                    {
                        field: "endtime", title: "结束时间", width: 130, sortable: true,
                        formatter: function (value) {
                            if (value) {
                                var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                                return date.format("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    },
                    { field: "member_price", title: "特权会员价格", width: 60, sortable: true },
                    { field: "box_intro", title: "盒子描述", width: 80, sortable: true },
                    { field: "box_remark", title: "盒子发送信息备注", width: 60, sortable: true },
                    { field: "box_senddate", title: "盒子发送日期", width: 130, sortable: true },
                    { field: "state", title: "盒子状态", width: 60, sortable: true },
                    {
                        field: "only_newuser", title: "允许无订单购买", width: 60, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    },
                    {
                        field: "if_repeat", title: "可重复购买", width: 60, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    },
                    {
                        field: "if_use_coupon", title: "可使用优惠券", width: 60, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    },
                    {
                        field: "if_give_coupon", title: "是否送优惠券", width: 80, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    },
                    {
                        field: "if_give_member", title: "是否送特权会员", width: 80, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    },
                    {
                        field: "coupon_valid_date", title: "购买盒子发送有效期", width: 130, sortable: true,
                        formatter: function (value) {
                            if (value) {
                                var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                                return date.format("yyyy-MM-dd");
                            }
                        }
                    },
                    { field: "boxcost", title: "盒子描述", width: 120, sortable: true },
                    { field: "icontype", title: "UI小图显示", width: 120, sortable: true },
                    {
                        field: "ifshowtime", title: "显示倒计时", width: 120, sortable: true,
                        formatter: function (value) {
                            if (value == 0) {
                                return "否";
                            } else if (value == 1) {
                                return "是";
                            }
                        }
                    },
                    { field: "toptime", title: "推荐置顶顺序", width: 120, sortable: true },
                    {
                        field: "if_hidden", title: "是否隐藏", width: 60, sortable: true,
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
                    id: "btnGetBox-" + self.selector,
                    text: '详细',
                    disabled: true,
                    iconCls: 'icon-09-08',
                    handler: function () { self.getBox(); }
                }, '-', {
                    id: "btnAddBox-" + self.selector,
                    text: '新增',
                    iconCls: 'icon-01-02',
                    handler: function () { showMsg({ icon: 'info', msg: '该功能尚在开发中...' }); }
                }, '-', {
                    id: "btnUdpBox-" + self.selector,
                    text: '修改',
                    disabled: true,
                    iconCls: 'icon-17-20',
                    handler: function () { showMsg({ icon: 'info', msg: '该功能尚在开发中...' }); }
                }, '-', {
                    id: "btnDelBox-" + self.selector,
                    text: '删除',
                    disabled: true,
                    iconCls: 'icon-07-08',
                    handler: function () { self.delBox(); }
                }, '-'
            ],
            onSelect: function (rowIndex, rowData) { self.setToolState(); },
            onUnselect: function (rowIndex, rowData) { self.setToolState(); },
            onSelectAll: function (rows) { self.setToolState(); },
            onUnselectAll: function (rows) { self.setToolState(); }
        });
        $.extend(self.controls, {
            gridList_btnGetBox: $("#btnGetBox-" + self.selector),
            gridList_btnAddBox: $("#btnAddBox-" + self.selector),
            gridList_btnUdpBox: $("#btnUdpBox-" + self.selector),
            gridList_btnDelBox: $("#btnDelBox-" + self.selector)
        });
    }
    Page.prototype.setToolState = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        if (row) {
            self.controls.gridList_btnGetBox.linkbutton("enable");
            self.controls.gridList_btnUdpBox.linkbutton("enable");
            self.controls.gridList_btnDelBox.linkbutton("enable");
        } else {
            self.controls.gridList_btnGetBox.linkbutton("disable");
            self.controls.gridList_btnUdpBox.linkbutton("disable");
            self.controls.gridList_btnDelBox.linkbutton("disable");
        }
    }
    Page.prototype.getBox = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        $('<div></div>').dialog({
            href: "/admin/box/detail?boxid=" + row.boxid,
            title: "产品盒子资料--" + row.name,
            iconCls: 'icon-09-08',
            width: 680,
            top:100,
            modal: true,
            onClose: function () { $(this).dialog("destroy"); }
        });
    }
    Page.prototype.delBox = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        $.messager.confirm("系统提示！", "您确定要删除 " + row.name + " 盒子么？", function (r) {
            if (!r) return false;
            $.post("/admin/box/delete", { boxid: row.boxid }, function (data) {
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

    new Page("box-list");
});