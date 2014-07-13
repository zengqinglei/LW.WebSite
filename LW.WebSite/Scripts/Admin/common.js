/// <reference path="../jquery-easyui/jquery.easyui.js" />

// 全局配置
var LW = {
    Url: {
        getValidCode: '/service/getvalidcode'
    },
    cbLogin: function (data) {
        window.location = data.data;
    },
    ajaxError: function (XMLHttpRequest, callback) {
        switch (XMLHttpRequest.status) {
            case 401:
                {
                    // show login dialog
                    if (XMLHttpRequest.responseText) {
                        var data = $.parseJSON(XMLHttpRequest.responseText);
                        if (data.url) {
                            $.get(data.url, null, function (html) {
                                LW.cbLogin = function (data) {
                                    if (callback) callback();
                                    showMsg({ icon: 'success', msg: '登录成功,如未自动操作，请重新尝试！' });
                                };
                                $("body").append(html);
                            }, 'html');
                        }
                    }
                }
                break;
            case 403:
                {
                    showMsg({ icon: "error", msg: "很抱歉，无权限执行此操作！" });
                }
                break;
            case 404:
                {
                    showMsg({ icon: "error", msg: "很抱歉，您所访问的地址不存在！" });
                }
                break;
            case 408:
                {
                    showMsg({ icon: "error", msg: "很抱歉，请求超时！" });
                }
                break;
            case 500:
                {
                    showMsg({ icon: "error", msg: "很抱歉，服务器错误，请稍后重试！" });
                }
                break;
            default:
                {
                    showMsg({ icon: "error", msg: "很抱歉，数据加载失败，请稍后重试！" });
                }
                break;
        }
    }
};

$.fn.ajaxError = function () {
    LW.ajaxError(XMLHttpRequest);
};
$.fn.dialog.defaults.onLoadError = function () {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).dialog("refresh");
    });
};
$.fn.form.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).form("submit");
    });
};
// tabs and dialog public
$.fn.panel.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).panel("refresh");
    });
}
$.fn.datagrid.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).datagrid("reload");
    });
}
$.fn.treegrid.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).treegrid("reload");
    });
}
$.fn.combobox.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).combobox("reload");
    });
}
$.fn.combotree.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).combotree("reload");
    });
}
$.fn.combogrid.defaults.onLoadError = function (XMLHttpRequest) {
    var self = this;

    LW.ajaxError(XMLHttpRequest, function () {
        $(self).combogrid("reload");
    });
}

Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "H+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

// 刷新验证码
function refreshValidCode(control) {
    $(control).attr('src', LW.Url.getValidCode + "?random=" + new Date());
}

// 提示信息
function showMsg(options) {
    if (!options.title) {
        options.title = "提示信息";
    }
    if (!options.timeout) {
        options.timeout = 2000;
    }
    if (!options.showType) {
        options.showType = "slide";
    }
    if (options.icon) {
        options.msg = "<div class='messager-icon icon-" + options.icon + "'></div><div>" + options.msg + "</div><div style='clear: both;'></div>";
    }
    $.messager.show(options);
}