/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;
        this.controls = {
            gridList: $(".grid-" + selector + " .grid-table")
        }

        this.initTreeGrid();
    }
    Page.prototype.initTreeGrid = function () {
        var self = this;

        self.controls.gridList.treegrid({
            title: "产品分类列表",
            url: "/admin/category/list",
            fit: true,
            rownumbers: true,
            animate: true,
            singleSelect: true,
            idField: "cid",
            treeField: "cname",
            columns: [
                [
                    { field: "cname", title: "分类名称", width: 200 },
                    { field: "cpath", title: "分类层级", width: 100 },
                    { field: "ctype", title: "分类类型", width: 100 },
                    { field: "sortid", title: "排序ID", width: 100 },
                    { field: "cstatu", title: "分类状态", width: 100 },
                    { field: "imgurl", title: "配图地址", width: 200 },
                    { field: "description", title: "描述", width: 300 }
                ]
            ],
            toolbar: [
                {
                    id: "btnGetCategory-" + self.selector,
                    text: '详细',
                    disabled: true,
                    iconCls: 'icon-09-08',
                    handler: function () { self.getCategory(); }
                }, '-', {
                    id: "btnAddCategory-" + self.selector,
                    text: '新增',
                    iconCls: 'icon-01-02',
                    handler: function () { showMsg({ icon: 'info', msg: '该功能尚在开发中...' }); }
                }, '-', {
                    id: "btnUdpCategory-" + self.selector,
                    text: '修改',
                    disabled: true,
                    iconCls: 'icon-17-20',
                    handler: function () { showMsg({ icon: 'info', msg: '该功能尚在开发中...' }); }
                }, '-', {
                    id: "btnDelCategory-" + self.selector,
                    text: '删除',
                    disabled: true,
                    iconCls: 'icon-07-08',
                    handler: function () { self.delCategory(); }
                }, '-'
            ],
            onSelect: function (rowIndex, rowData) { self.setToolState(); },
            onUnselect: function (rowIndex, rowData) { self.setToolState(); },
            onSelectAll: function (rows) { self.setToolState(); },
            onUnselectAll: function (rows) { self.setToolState(); }
        });
        $.extend(self.controls, {
            gridList_btnGetCategory: $("#btnGetCategory-" + self.selector),
            gridList_btnAddCategory: $("#btnAddCategory-" + self.selector),
            gridList_btnUdpCategory: $("#btnUdpCategory-" + self.selector),
            gridList_btnDelCategory: $("#btnDelCategory-" + self.selector)
        });
    }
    Page.prototype.setToolState = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        if (row) {
            self.controls.gridList_btnGetCategory.linkbutton("enable");
            self.controls.gridList_btnUdpCategory.linkbutton("enable");
            self.controls.gridList_btnDelCategory.linkbutton("enable");
        } else {
            self.controls.gridList_btnGetCategory.linkbutton("disable");
            self.controls.gridList_btnUdpCategory.linkbutton("disable");
            self.controls.gridList_btnDelCategory.linkbutton("disable");
        }
    }
    Page.prototype.getCategory = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        $('<div></div>').dialog({
            href: "/admin/category/detail?cid=" + row.cid,
            title: "分类信息--" + row.cname,
            iconCls: 'icon-09-08',
            width: 680,
            top: 100,
            modal: true,
            onClose: function () { $(this).dialog("destroy"); }
        });
    }
    Page.prototype.delCategory = function () {
        var self = this;

        var row = self.controls.gridList.datagrid("getSelected");
        $.messager.confirm("系统提示！", "您确定要删除 " + row.cname + " 类别么？", function (r) {
            if (!r) return false;
            $.post("/admin/category/delete", { cid: row.cid }, function (data) {
                if (data.status == 1) {
                    self.controls.gridList.treegrid("remove", row.cid);
                    self.setToolState();
                    showMsg({ msg: data.msg, icon: "success" });
                } else {
                    showMsg({ msg: data.msg, icon: "error" });
                }
            }, "json");
        });
    }

    new Page("category-list");
});