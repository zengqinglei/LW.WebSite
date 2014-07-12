/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.controls = {
            dlgLogin: $(".dialog-" + selector),
            frmLogin: $(".dialog-" + selector + ' form'),
            frmLogin_txtAccount: $(".dialog-" + selector + ' form [name="Account"]'),
            frmLogin_pwdPassword: $(".dialog-" + selector + ' form [name="Password"]'),
            frmLogin_txtValidcode: $(".dialog-" + selector + ' form [name="ValidCode"]'),
            frmLogin_imgValidcode: $(".dialog-" + selector + ' form .img-validcode'),
            frmLogin_btnRefvalidcode: $(".dialog-" + selector + ' form .a-refreshvalidcode')
        }

        this.init();
    }
    Page.prototype.init = function () {
        var self = this;

        self.controls.dlgLogin.dialog({
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
                self.controls.frmLogin_txtAccount.validatebox({
                    required: true,
                    missingMessage: '请输入管理员帐号'
                });
                self.controls.frmLogin_pwdPassword.validatebox({
                    required: true,
                    missingMessage: '请输入管理员密码',
                    validType: 'length[6,12]',
                    invalidMessage: '格式有误，请输入6~12位字符'
                });
                self.controls.frmLogin_txtValidcode.focus(function () {
                    self.controls.frmLogin_txtValidcode.validatebox("disableValidation");
                }).blur(function () {
                    self.controls.frmLogin_txtValidcode.validatebox("enableValidation");
                }).validatebox({
                    required: true,
                    missingMessage: '请输入4位验证码',
                    validType: 'length[4,4]',
                    invalidMessage: '格式有误，请输入4位验证码'
                });
                self.controls.frmLogin.form('disableValidation').find('input').bind('keyup', function (event) {
                    if (event.keyCode == '13') {
                        self.onLogin();
                    }
                });
            },
            onClose: function () { $(this).dialog('distory'); }
        });

        self.refreshValidCode();
        self.controls.frmLogin_btnRefvalidcode.click(function () { self.refreshValidCode(); });
    }
    Page.prototype.onLogin = function () {
        var self = this;

        self.controls.frmLogin.form('enableValidation').form('submit', {
            success: function (data) {
                var json = jQuery.parseJSON(data);
                if (json.status == 1) {
                    if (json.data) {
                        window.location = json.data;
                    } else {
                        showMsg({ msg: json.msg, icon: "success" });
                    }
                    self.controls.dlgLogin.dialog('close');
                } else {
                    self.refreshValidCode();
                    showMsg({ msg: json.msg, icon: "error" });
                }
            }
        });
    }
    Page.prototype.onReset = function () {
        var self = this;

        self.controls.frmLogin.form('reset');
        self.refreshValidCode();
        self.controls.frmLogin.form('disableValidation');
    }
    Page.prototype.refreshValidCode = function () {
        var self = this;

        refreshValidCode(self.controls.frmLogin_imgValidcode);
    }

    new Page("account-login");
});