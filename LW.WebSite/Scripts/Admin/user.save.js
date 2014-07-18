/// <reference path="common.js" />

$(function () {
    function Page(selector) {
        this.selector = selector;
        this.controls = {
            frmAddUser: $(".form-" + selector),
            frmAddUser_txtUserid: $('.form-' + selector + ' [name="userid"]'),
            frmAddUser_txtNickname: $('.form-' + selector + ' [name="nickname"]'),
            frmAddUser_txtUsermail: $('.form-' + selector + ' [name="usermail"]'),
            frmAddUser_txtKnowway: $('.form-' + selector + ' [name="know_way"]'),
        }

        this.initForm(this.controls.frmAddUser_txtUserid.val().length == 0);
    }
    Page.prototype.initForm = function (isAdd) {
        var self = this;

        self.controls.frmAddUser_txtNickname.validatebox({
            required: true,
            missingMessage: '请填写客户昵称！',
            validType: isAdd ? {
                remote: ['/service/existaccount', 'nickname']
            } : null,
            invalidMessage: '该昵称已存在！'
        });
        self.controls.frmAddUser_txtUsermail.validatebox({
            required: true,
            missingMessage: '请填写客户邮箱！',
            validType: 'email',
            invalidMessage: '请填写正确的电子邮箱！'
        });
    }
    Page.prototype.onSubmit = function (callback) {
        var self = this;

        self.controls.frmAddUser.form('submit', {
            success: function (data) {
                var data = jQuery.parseJSON(data);
                if (callback) {
                    callback(data);
                }
            }
        });
    }

    LW.UserSave = new Page("user-save");
});