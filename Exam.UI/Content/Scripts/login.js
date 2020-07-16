layui.use(['jquery', 'layer', 'form', 'larryMenu'], function () {
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form,
        common = layui.common;
    // 页面上下文菜单
    var larryMenu = layui.larryMenu();

    var mar_top = ($(document).height() - $('#larry_login').height()) / 2.5;
    $('#larry_login').css({ 'margin-top': mar_top });
    //common.larryCmsSuccess('用户名：larry 密码：larry 无须输入验证码，输入正确后直接登录后台!','larryMS后台帐号登录提示',20);
    var placeholder = '';
    $("#larry_form input[type='text'],#larry_form input[type='password']").on('focus', function () {
        placeholder = $(this).attr('placeholder');
        $(this).attr('placeholder', '');
    });
    $("#larry_form input[type='text'],#larry_form input[type='password']").on('blur', function () {
        $(this).attr('placeholder', placeholder);
    });

    common.larryCmsLoadJq('../Content/Common/plus/jquery.supersized.min.js', function () {
        $.supersized({
            // 功能
            slide_interval: 3000,
            transition: 1,
            transition_speed: 1000,
            performance: 1,
            // 大小和位置
            min_width: 0,
            min_height: 0,
            vertical_center: 1,
            horizontal_center: 1,
            fit_always: 0,
            fit_portrait: 1,
            fit_landscape: 0,
            // 组件
            slide_links: 'blank',
            slides: [{

                image: '../Content/images/login/1.jpg'
            }, {
                    image: '../Content/images/login/2.jpg'
            }, {
                    image: '../Content/images/login/3.jpg'
            }]
        });
    });

    form.on('submit(submit)', function (data) {
        var userName = data.field.user_name;
        var userPwd = data.field.password;
        var ipaddress = data.field.Ip;
        var token = $('input[name=__RequestVerificationToken]').val();

        $.post("/Login/UserLogin", { "userName": userName, "userPwd": userPwd, "ipaddress": ipaddress, '__RequestVerificationToken': token }, function (data) {
            if (data.success == true) {
                layer.msg(data.msg, { icon: 1, time: 500 });
                
                if (data.Role == 1) {
                    window.location.href = '/Admin/Index';
                }
                else {
                    window.location.href = '/Student/Index';
                }
                //layer.msg('登录成功', { icon: 1, time: 500 });
                //window.location.href = '/Backstage/Home/Index';
            }
            else {
                layer.msg(data.msg, { icon: 2, time: 500 });
            }
        }, "json");
    
        return false;
    });

    // 右键菜单控制
    var larrycmsMenuData = [
        [{
            text: "刷新页面",
            func: function () {
                top.document.location.reload();
            }
        }]
    ];
    larryMenu.ContentMenu(larrycmsMenuData, {
        name: "html"
    }, $('html'));
});
