// 全局配置
var LW = {
    Url: {
        getValidCode: '/service/getvalidcode'
    },
    ajaxError: function (XMLHttpRequest, textStatus, errorThrown) {
        switch (XMLHttpRequest.status) {
            case 302:
                {
                    $("body").append(XMLHttpRequest.responseText);
                }
                break;
            case 404:
                {
                    showMsg({ icon: "error", msg: "很抱歉，您所访问的地址不存在！" });
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