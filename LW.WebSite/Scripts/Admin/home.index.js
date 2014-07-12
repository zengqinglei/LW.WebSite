/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;

        this.controls = {
            btnResetpw: $(".layout-" + selector + " .a-resetpw"),
            btnLogout: $(".layout-" + selector + ' .a-logout'),
            navList: $(".layout-" + selector + ' .nav-list'),
            tabList: $(".layout-" + selector + ' .tab-list'),
            tabMenu: $('.tab-menu'),
            dlgResetpw: $('.dialog-resetpw'),
            frmResetpw: $('.dialog-resetpw form'),
            frmResetpw_txtOldpassword: $('.dialog-resetpw [name="OldPassword"]'),
            frmResetpw_txtNewpassword: $('.dialog-resetpw [name="NewPassword"]'),
            frmResetpw_txtConfirmpassword: $('.dialog-resetpw [name="ConfirmPassword"]')
        }

        this.init();
    }
    Page.prototype.init = function () {
        var self = this;

        self.controls.btnResetpw.click(function () { self.initResetpw(); });
        self.controls.btnLogout.click(function () { self.onLogout(); });
        self.initNavList();
    }
    Page.prototype.initResetpw = function () {
        var self = this;

        self.controls.dlgResetpw.show().dialog({
            title: '管理员密码修改',
            width: 400,
            modal: true,
            iconCls: "icon-01-12",
            buttons: [{
                text: "确定",
                iconCls: "icon-ok",
                handler: function () { self.onResetPW(); }
            }, {
                text: "取消",
                iconCls: "icon-cancel",
                handler: function () { self.controls.dlgResetpw.dialog("close"); }
            }],
            onOpen: function () {
                self.controls.frmResetpw_txtOldpassword.validatebox({
                    required: true,
                    missingMessage: '请输入旧密码'
                });
                self.controls.frmResetpw_txtNewpassword.validatebox({
                    required: true,
                    missingMessage: '请输入新密码'
                });
                self.controls.frmResetpw_txtConfirmpassword.validatebox({
                    required: true,
                    missingMessage: '请再次输入新密码',
                    validType: "equals['" + self.controls.frmResetpw_txtNewpassword.selector + "']",
                    invalidMessage: '两次新密码输入不一致'
                });
                self.controls.frmResetpw.form("disableValidation");
            },
            onClose: function () {
                self.controls.frmResetpw.form("reset").form("disableValidation");
            }
        });
    }
    Page.prototype.onLogout = function () {
        var self = this;

        $.messager.confirm("系统提示！", "您确定要退出本次登录吗？", function (r) {
            if (!r) return false;
            location.href = "/admin/account/logout";
        });
    }
    Page.prototype.onResetPW = function () {
        var self = this;

        self.controls.frmResetpw.form("enableValidation").form('submit', {
            success: function (data) {
                var json = jQuery.parseJSON(data);
                if (json.status == 1) {
                    showMsg({ msg: json.msg, icon: "success" });
                    self.controls.dlgResetpw.dialog('close');
                } else {
                    showMsg({ msg: json.msg, icon: "error" });
                }
            }
        });
    }
    Page.prototype.initNavList = function () {
        var self = this;

        self.initTabList();
        $.post('/admin/home/getnavs', {}, function (data) {
            self.controls.navList.accordion({
                fit: true,
                border: false
            });
            $(data).each(function (index, item) {
                self.controls.navList.accordion("add", {
                    title: item.text,
                    iconCls: item.iconCls,
                    selected: index == 0,
                    content: $("<ul class='nav-child'></ul>").tree({
                        data: item.children,
                        onClick: function (node) {
                            if (node.attributes) {
                                self.openTab(node, true);
                            }
                        }
                    })
                });
            });
        }, 'json').fail(LW.ajaxError);
    }
    Page.prototype.initTabList = function () {
        var self = this;

        self.controls.tabList.tabs({
            fit: true,
            border: false,
            onContextMenu: function (e, title, index) {
                e.preventDefault();

                self.controls.tabList.tabs("select", title);

                var tabList = $('.tabList-closable');
                var lefttabList = $('.tabList-selected').prevAll();
                var righttabList = $('.tabList-selected').nextAll();
                self.controls.tabMenu.menu({
                    onHide: function () {
                        $(this).empty();
                    }
                }).menu('appendItem', {
                    text: '刷新',
                    iconCls: 'icon-reload',
                    onclick: function () {
                        self.controls.tabList.tabs('getTab', title).panel("refresh");
                    }
                }).menu('appendItem', {
                    separator: true
                }).menu('appendItem', {
                    text: '关闭',
                    disabled: !self.controls.tabList.tabs('getTab', title).panel("options").closable,
                    iconCls: 'icon-cancel',
                    onclick: function () {
                        self.controls.tabList.tabs('close', title);
                    }
                }).menu('appendItem', {
                    text: '全部关闭',
                    disabled: tabList.length == 0,
                    onclick: function () {
                        tabList.each(function (i, n) {
                            self.controls.tabList.tabs('close', $(n).text());
                        });
                    }
                }).menu('appendItem', {
                    text: '除此之外全部关闭',
                    disabled: tabList.length <= 1,
                    onclick: function () {
                        tabList.each(function (i, n) {
                            if ($(n).text() != title) {
                                self.controls.tabList.tabs('close', $(n).text());
                            }
                        });
                    }
                }).menu('appendItem', {
                    separator: true
                }).menu('appendItem', {
                    text: '左侧全部关闭',
                    disabled: lefttabList.length == 0,
                    onclick: function () {
                        prevall.each(function (i, n) {
                            var tmpTab = self.controls.tabList.tabs('getTab', $('.tabList-title', $(n)).text());
                            if (tmpTab.panel('options').closable) {
                                self.controls.tabList.tabs('close', tmpTab.panel('options').title);
                            }
                        });
                    }
                }).menu('appendItem', {
                    text: '右侧全部关闭',
                    disabled: righttabList.length == 0,
                    onclick: function () {
                        nextall.each(function (i, n) {
                            self.controls.tabList.tabs('close', $('.tabList-title', $(n)).text());
                        });
                    }
                }).menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            },
            onSelect: function (title, index) {
                self.controls.navList.find('.tree-node').each(function (i, n) {
                    if ($(n).hasClass('tree-node-selected') && $('.tree-title', $(n)).text() == title) {
                    } else if ($(n).hasClass('tree-node-selected')) {
                        $(n).removeClass('tree-node-selected');
                    } else if ($('.tree-title', $(n)).text() == title) {
                        $(n).addClass('tree-node-selected');
                    }
                });
            }
        });
        self.openTab({
            text: "首 页",
            iconCls: "icon-12-11",
            attributes: {
                url: "/admin/home/welcome"
            }
        }, false);
    }
    Page.prototype.openTab = function (node, closable) {
        var self = this;

        if (self.controls.tabList.tabs("exists", node.text)) {
            self.controls.tabList.tabs("select", node.text);
        } else {
            self.controls.tabList.tabs("add", {
                title: node.text,
                closable: closable,
                iconCls: node.iconCls,
                href: node.attributes.url,
                loadingMessage: '数据加载中...',
                bodyCls: "tab-container",
                onLoadError: LW.ajaxError
            });
        }
    }

    new Page("home-index");
});