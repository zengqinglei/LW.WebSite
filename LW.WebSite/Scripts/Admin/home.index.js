/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;
        this.controls = {
            aResetPW: $(selector + " .a-resetpw"),
            aLogout: $(selector + ' .a-logout'),
            navs: $(selector + ' .nav-list'),
            tabs: $(selector + ' .tab-list'),
            tabMenu: $('.tab-menu'),
            dialogResetPW: $('.dialog-resetpw'),
            formResetPW: $('.dialog-resetpw form'),
            txtOldPassword: $('.dialog-resetpw [name="OldPassword"]'),
            txtNewPassword: $('.dialog-resetpw [name="NewPassword"]'),
            txtConfirmPassword: $('.dialog-resetpw [name="ConfirmPassword"]')
        }

        this.init();
    }
    Page.prototype.init = function () {
        var self = this;

        self.controls.aResetPW.click(function () { self.initResetPW(); });
        self.controls.aLogout.click(function () { self.onLogout(); });
        self.initNavs();
    }
    Page.prototype.initResetPW = function () {
        var self = this;

        self.controls.dialogResetPW.show().dialog({
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
                handler: function () { self.controls.dialogResetPW.dialog("close"); }
            }],
            onOpen: function () {
                self.controls.txtOldPassword.validatebox({
                    required: true,
                    missingMessage: '请输入旧密码'
                });
                self.controls.txtNewPassword.validatebox({
                    required: true,
                    missingMessage: '请输入新密码'
                });
                self.controls.txtConfirmPassword.validatebox({
                    required: true,
                    missingMessage: '请再次输入新密码',
                    validType: "equals['" + self.controls.txtNewPassword.selector + "']",
                    invalidMessage: '两次新密码输入不一致'
                });
                self.controls.formResetPW.form("disableValidation");
            },
            onClose: function () {
                self.controls.formResetPW.form("reset").form("disableValidation");
            }
        });
    }
    Page.prototype.onLogout = function () {
        var self = this;

        $.messager.confirm("系统提示！", "您确定要退出本次登录吗？", function (r) {
            if (r) {
                location.href = "/admin/account/logout";
            }
        });
    }
    Page.prototype.onResetPW = function () {
        var self = this;

        self.controls.formResetPW.form("enableValidation").form('submit', {
            success: function (data) {
                var json = jQuery.parseJSON(data);
                if (json.Status == 1) {
                    showMsg({ msg: json.Msg, icon: "success" });
                    self.controls.dialogResetPW.dialog('close');
                } else {
                    showMsg({ msg: json.Msg, icon: "error" });
                }
            }
        });
    }
    Page.prototype.initNavs = function () {
        var self = this;

        self.initTabs();
        $.post('/admin/home/getnavs', {}, function (data) {
            self.controls.navs.accordion({
                fit: true,
                border: false
            });
            $(data).each(function (index, item) {
                self.controls.navs.accordion("add", {
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
    Page.prototype.initTabs = function () {
        var self = this;

        self.controls.tabs.tabs({
            fit: true,
            border: false,
            onContextMenu: function (e, title, index) {
                e.preventDefault();

                self.controls.tabs.tabs("select", title);

                var Tabs = $('.tabs-closable');
                var leftTabs = $('.tabs-selected').prevAll();
                var rightTabs = $('.tabs-selected').nextAll();
                self.controls.tabMenu.menu({
                    onHide: function () {
                        $(this).empty();
                    }
                }).menu('appendItem', {
                    text: '刷新',
                    iconCls: 'icon-reload',
                    onclick: function () {
                        self.controls.tabs.tabs('getTab', title).panel("refresh");
                    }
                }).menu('appendItem', {
                    separator: true
                }).menu('appendItem', {
                    text: '关闭',
                    disabled: !self.controls.tabs.tabs('getTab', title).panel("options").closable,
                    iconCls: 'icon-cancel',
                    onclick: function () {
                        self.controls.tabs.tabs('close', title);
                    }
                }).menu('appendItem', {
                    text: '全部关闭',
                    disabled: Tabs.length == 0,
                    onclick: function () {
                        Tabs.each(function (i, n) {
                            self.controls.tabs.tabs('close', $(n).text());
                        });
                    }
                }).menu('appendItem', {
                    text: '除此之外全部关闭',
                    disabled: Tabs.length <= 1,
                    onclick: function () {
                        Tabs.each(function (i, n) {
                            if ($(n).text() != title) {
                                self.controls.tabs.tabs('close', $(n).text());
                            }
                        });
                    }
                }).menu('appendItem', {
                    separator: true
                }).menu('appendItem', {
                    text: '左侧全部关闭',
                    disabled: leftTabs.length == 0,
                    onclick: function () {
                        prevall.each(function (i, n) {
                            var tmpTab = self.controls.tabs.tabs('getTab', $('.tabs-title', $(n)).text());
                            if (tmpTab.panel('options').closable) {
                                self.controls.tabs.tabs('close', tmpTab.panel('options').title);
                            }
                        });
                    }
                }).menu('appendItem', {
                    text: '右侧全部关闭',
                    disabled: rightTabs.length == 0,
                    onclick: function () {
                        nextall.each(function (i, n) {
                            self.controls.tabs.tabs('close', $('.tabs-title', $(n)).text());
                        });
                    }
                }).menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            },
            onSelect: function (title, index) {
                self.controls.navs.find('.tree-node').each(function (i, n) {
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

        if (self.controls.tabs.tabs("exists", node.text)) {
            self.controls.tabs.tabs("select", node.text);
        } else {
            self.controls.tabs.tabs("add", {
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

    new Page(".layout-home-index");
});