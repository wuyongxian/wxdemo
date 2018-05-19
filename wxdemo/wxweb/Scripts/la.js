; (function () {
    window.LA = window.LA || {
        /*para*/
        /*end para*/
        /*function*/
        Load: function () {
            LA.loadMenu();//加载菜单
            LA.loadPageHeader();

            $('#li_loginout').click(function () {
                LA.loginOut();
            })
            //LA.setbreadcrumb();//设置导航路径
        },

        //用户推出操作
        loginOut: function () {
            var msg = '您确认要退出登陆吗？';
            $('.mesg-window').css('top', '0px');
            CM.showConfirm(msg, function (obj) {
                if (obj == 'yes') {

                    $.post("/Admin/Login/UserLoginOut", '',
                        function (r) {
                            if (r.state) {
                                localStorage.removeItem("user");
                                localStorage.removeItem("menuAuth"); 
                                window.location.href = '/Admin/Login/Index';
                            } else {
                                CM.showMsg(r.msg);
                            }
                        })
                } else {
                    //console.log('你点击了取消！');
                }

            },'right_top');//提示框出现的位置
          
        },

        //加载页面头部的个人信息
        loadPageHeader: function () {
            var user = JSON.parse(localStorage.getItem('user'));
            var sRealName = user.sRealName;
            var sPhoneNO = user.sPhoneNO;
            var sAddress = user.sAddress;
            var sGender = "男";
            if (user.sGender == 1) {
                sGender = '男';
            } else if (user.sGender == 2) {
                sGender = '女';
            } else {
                sGender = '未知';
            }
            var sHeaderPhoto = user.sHeaderPhoto;
            if (sHeaderPhoto != null && sHeaderPhoto != '') {
                $('.ace-nav .nav-user-photo').attr('src', sHeaderPhoto);
            }
            $('.ace-nav .user-info').html('<small>Welcome,</small>' + sRealName);
            $('#hspan_sRealName').html(sRealName);
            $('#hspan_sGender').html(sGender);
            $('#hspan_sPhoneNO').html(sPhoneNO);
            $('#hspan_sAddress').html(sAddress);
        },

        //设置页面打开和关闭状态
        setNavState: function () {
            var pathname = window.location.pathname + window.location.search;
            //console.log('pathname:' + pathname);
            $('#breadcrumbs .breadcrumbs').html($(this).attr("href"));
            $(".nav li a").each(function () {
                var href = $(this).attr("href");
                if (pathname == href) {
                    var UserMenuID = $(this).parent().attr('UserMenuID');
                    LA.setbreadcrumb(UserMenuID);
                    $(this).parents("ul").parent("li").addClass("active open");
                    $(this).parent("li").addClass("active");
                }
            });
        },

        //设置页面的面包屑导航
        setbreadcrumb: function (UserMenuID) {
            $.post('/Admin/UserMenu/getMenuPath', { id: UserMenuID },
                function (r) {
                    if (r.length == 1) {//当前目录为根目录
                        return false;
                    }
                    $('#breadcrumbs .breadcrumb .li_append').remove();
                    for (var i = 0; i < r.length; i++) {
                        if (i != r.length - 1) {
                            $('.breadcrumb').append('<li class="li_append"><a href="' + r[i].sUrl + '">' + r[i].sName + '</a></li>');
                        } else {
                            $('.breadcrumb').append('<li class="active li_append">' + r[i].sName + '</li>');
                        }

                    }

                });
        },
        //获取当前菜单的路径
        getmenupath: function (ID) {
            $.post('/Admin/UserMenu/getMenuPath', { id: ID },
                function (r) {
                    if (r.length == 1) {//当前目录为根目录
                        tag_ParentID = '';
                        tag_ID = r[0].ID;
                    } else {
                        tag_ParentID = r[r.length - 1].ParentID;
                        tag_ID = r[r.length - 1].ID;
                    }
                    $('.breadcrumb .active').remove();
                    for (var i = 0; i < r.length; i++) {
                        $('.breadcrumb').append('<li class="active">' + r[i].sName + '</li>');
                    }

                });

        },

        //加载菜单列表 ID:UserGroup表ID
        loadMenu: function () {
            var user = JSON.parse(localStorage.getItem('user'));
            var UserGroupID = user.UserGroupID;
            var url = '/Admin/UserMenu/getLAMenu';
            var data = { 'userGroupID': UserGroupID };
            $.post(url, data,
                function (r) {
                    LA.getMenuP(r);
                    LA.setNavState();
                });
        },

        getMenuP: function (itemsAll) {
            var strhtml = '';
            $('#sidebar_ul').html('');
            var items_P = LA.getMenuC_array("", itemsAll);//所有的一级菜单信息
            for (var i = 0; i < items_P.length; i++) {
                var item_P = items_P[i];
                var ParentID = item_P.UserMenuID;
                var items_C = LA.getMenuC_array(ParentID, itemsAll);//当前一级菜单下的二级菜单
                var isFold = false;
                if (items_C.length > 0) {//有子菜单
                    isFold = true;
                }
                strhtml += LA.getMenuItem(item_P, isFold);

                if (items_C.length > 0) {//有子菜单
                    //var item = items_C[0];
                    strhtml += LA.getMenuC_html(item_P.ID, items_C, itemsAll);
                } else {//无子菜单

                }
                if (isFold) {
                    strhtml+=' </li>'
                }
            }
            $('#sidebar_ul').html(strhtml);

        },

        //获取子菜单数据
        getMenuC_array: function (ParentID, items) {
            return items.filter(function (e) {
                return e.ParentID == ParentID;
            });
        },

        //展示子菜单页面
        getMenuC_html: function (ParentID, items, itemsAll) {
            var strhtml = ''
            strhtml += '<ul class="submenu">';
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                var ParentID = item.UserMenuID;
                var items_C = LA.getMenuC_array(ParentID, itemsAll);
                var isFold = false;
                if (items_C.length > 0) {//有子菜单
                    isFold = true;
                }
                strhtml += LA.getMenuItem(item, isFold);


                if (items_C.length > 0) {//有子菜单
                    //var item = items_C[0];
                    strhtml += LA.getMenuC_html(item.ID, items_C, itemsAll);
                } else {//无子菜单

                }
                if (isFold) {
                    strhtml += ' </li>'
                }
            }
            strhtml += '</ul>';
            return strhtml;

        },

        getMenuItem: function (item, isFold) {
            //获取菜单后添加当前用户的权限

            var strhtml = '';
            strhtml += '<li class="" UserMenuID="' + item.UserMenuID + '" sAuthority="' + item.sAuthority + '">';
            if (isFold) {//有子菜单的选项
                strhtml += '    <a href="' + item.sUrl + '" class="dropdown-toggle">'; 
                //strhtml += '         <i class="menu-icon fa ' + item.sIconUrl + '"></i>';
                strhtml += '         <span class="' + item.sIconUrl+'" aria-hidden="true"></span>';
                strhtml += '         <span class="menu-text"> ' + item.sName + ' </span>';
                strhtml += '        <b class="arrow fa fa-angle-down"></b>';
                strhtml += '    </a>';
                strhtml += '    <b class="arrow"></b>';
            } else {
                strhtml += '    <a href="' + item.sUrl + '">';
                strhtml += '          <i class="menu-icon fa fa-caret-right"></i>' + item.sName;
             
                strhtml += '    </a>';
                strhtml += '    <b class="arrow"></b>';
                strhtml += '</li>';
            }
           

            return strhtml;
        },



        //生成新的GUID
        getHtml: function () {
            //alert($('.breadcrumbs').html());
        }


    }
})();


