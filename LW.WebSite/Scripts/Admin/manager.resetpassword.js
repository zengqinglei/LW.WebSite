/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;

        this.controls = {
            frmResetpw: $('.form-' + selector),
            frmResetpw_txtOldpassword: $('.form-' + selector + ' [name="OldPassword"]'),
            frmResetpw_txtNewpassword: $('.form-' + selector + ' [name="NewPassword"]'),
            frmResetpw_txtConfirmpassword: $('.form-' + selector + ' [name="ConfirmPassword"]')
        }

        this.initForm();
    }
    Page.prototype.initForm = function () {
        var self = this;

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
    }
    Page.prototype.onSubmit = function (callback) {
        var self = this;

        self.controls.frmResetpw.form("enableValidation").form('submit', {
            success: function (data) {
                var data = jQuery.parseJSON(data);
                if (callback) callback(data);
            }
        });
    }

    LW.ManagerResetpw = new Page("manager-resetpassword");
});