/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.controls = {
            loginDialog: $(selector),
            loginForm: $(selector + ' form'),
            txtAccount: $(selector + ' form [name="Account"]'),
            txtPassword: $(selector + ' form [name="Password"]'),
            txtValidCode: $(selector + ' form [name="ValidCode"]'),
            imgValidCode: $(selector + ' form .img-validcode'),
            aRefreshValidCode: $(selector + ' form .a-refreshvalidcode')
        }

        this.init();
    }
    Page.prototype.init = function () {
        var self = this;

        self.controls.loginDialog.dialog({
            title: '罗莉盒微信--管理员登录',
            width: 400,
            closable: false,
            draggable: false,
            buttons: [{
                text: '登录',
                iconCls: 'icon-ok',
                handler: function () { self.onLogin(); }
            }, {
                text: '重置',
                iconCls: 'icon-undo',
                handler: function () { self.onReset(); }
            }],
            onOpen: function () {
                self.controls.txtAccount.validatebox({
                    required: true,
                    missingMessage: '请输入管理员帐号'
                });
                self.controls.txtPassword.validatebox({
                    required: true,
                    missingMessage: '请输入管理员密码',
                    validType: 'length[6,12]',
                    invalidMessage: '格式有误，请输入6~12位字符'
                });
                self.controls.txtValidCode.focus(function () {
                    self.controls.txtValidCode.validatebox("disableValidation");
                }).blur(function () {
                    self.controls.txtValidCode.validatebox("enableValidation");
                }).validatebox({
                    required: true,
                    missingMessage: '请输入4位验证码',
                    validType: 'length[4,4]',
                    invalidMessage: '格式有误，请输入4位验证码'
                });
                self.controls.loginForm.form('disableValidation').find('input').bind('keyup', function (event) {
                    if (event.keyCode == '13') {
                        self.onLogin();
                    }
                });
            },
            onClose: function () { $(this).dialog('distory'); }
        });

        self.refreshValidCode();
        self.controls.aRefreshValidCode.click(function () { self.refreshValidCode(); });
    }
    Page.prototype.onLogin = function () {
        var self = this;

        self.controls.loginForm.form('enableValidation').form('submit', {
            success: function (data) {
                var json = jQuery.parseJSON(data);
                if (json.status == 1) {
                    if (json.data) {
                        window.location = json.data;
                    } else {
                        showMsg({ msg: json.msg, icon: "success" });
                    }
                    self.controls.loginDialog.dialog('close');
                } else {
                    self.refreshValidCode();
                    showMsg({ msg: json.msg, icon: "error" });
                }
            }
        });
    }
    Page.prototype.onReset = function () {
        var self = this;

        self.controls.loginForm.form('reset');
        self.refreshValidCode();
        self.controls.loginForm.form('disableValidation');
    }
    Page.prototype.refreshValidCode = function () {
        var self = this;

        refreshValidCode(self.controls.imgValidCode);
    }

    new Page(".dialog-account-login");
});