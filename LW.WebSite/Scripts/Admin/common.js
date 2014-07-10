// 全局配置
var LW = {
    Url: {
        getValidCode: '/service/getvalidcode'
    }
};

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
        options.msg = "<div class='messager-icon messager-" + options.icon + "'></div><div>" + options.msg + "</div><div style='clear: both;'></div>";
    }
    $.messager.show(options);
}