if (!('arqWeb' in window)) window['arqWeb'] = {}

arqWeb.config = {
    cookie_expiry: 604800, //1 week duration for saved settings
    storage_method: 1 //2 means use cookies, 1 means localStorage, 0 means localStorage if available otherwise cookies
}

arqWeb.settings = {
    is: function (item, status) {
        //such as arqWeb.settings.is('navbar', 'fixed')
        return (arqWeb.data.get('settings', item + '-' + status) == 1)
    },
    exists: function (item, status) {
        return (arqWeb.data.get('settings', item + '-' + status) !== null)
    },
    set: function (item, status) {
        arqWeb.data.set('settings', item + '-' + status, 1)
    },
    unset: function (item, status) {
        arqWeb.data.set('settings', item + '-' + status, -1)
    },
    remove: function (item, status) {
        arqWeb.data.remove('settings', item + '-' + status)
    },

    navbar_fixed: function (fix) {
        fix = fix || false;
        if (!fix && arqWeb.settings.is('sidebar', 'fixed')) {
            arqWeb.settings.sidebar_fixed(false);
        }

        var navbar = document.getElementById('navbar');
        if (fix) {
            if (!arqWeb.hasClass(navbar, 'navbar-fixed-top')) arqWeb.addClass(navbar, 'navbar-fixed-top');
            if (!arqWeb.hasClass(document.body, 'navbar-fixed')) arqWeb.addClass(document.body, 'navbar-fixed');

            arqWeb.settings.set('navbar', 'fixed');
        } else {
            arqWeb.removeClass(navbar, 'navbar-fixed-top');
            arqWeb.removeClass(document.body, 'navbar-fixed');

            arqWeb.settings.unset('navbar', 'fixed');
        }

        document.getElementById('ace-settings-navbar').checked = fix;
    },


    breadcrumbs_fixed: function (fix) {
        fix = fix || false;
        if (fix && !arqWeb.settings.is('sidebar', 'fixed')) {
            arqWeb.settings.sidebar_fixed(true);
        }

        var breadcrumbs = document.getElementById('breadcrumbs');
        if (fix) {
            if (!arqWeb.hasClass(breadcrumbs, 'breadcrumbs-fixed')) arqWeb.addClass(breadcrumbs, 'breadcrumbs-fixed');
            if (!arqWeb.hasClass(document.body, 'breadcrumbs-fixed')) arqWeb.addClass(document.body, 'breadcrumbs-fixed');

            arqWeb.settings.set('breadcrumbs', 'fixed');
        } else {
            arqWeb.removeClass(breadcrumbs, 'breadcrumbs-fixed');
            arqWeb.removeClass(document.body, 'breadcrumbs-fixed');

            arqWeb.settings.unset('breadcrumbs', 'fixed');
        }
        document.getElementById('ace-settings-breadcrumbs').checked = fix;
    },


    sidebar_fixed: function (fix) {
        fix = fix || false;
        if (!fix && arqWeb.settings.is('breadcrumbs', 'fixed')) {
            arqWeb.settings.breadcrumbs_fixed(false);
        }

        if (fix && !arqWeb.settings.is('navbar', 'fixed')) {
            arqWeb.settings.navbar_fixed(true);
        }

        var sidebar = document.getElementById('sidebar');
        if (fix) {
            if (!arqWeb.hasClass(sidebar, 'sidebar-fixed')) arqWeb.addClass(sidebar, 'sidebar-fixed');
            arqWeb.settings.set('sidebar', 'fixed');
        } else {
            arqWeb.removeClass(sidebar, 'sidebar-fixed');
            arqWeb.settings.unset('sidebar', 'fixed');
        }
        document.getElementById('ace-settings-sidebar').checked = fix;
    },

    main_container_fixed: function (inside) {
        inside = inside || false;

        var main_container = document.getElementById('main-container');
        var navbar_container = document.getElementById('navbar-container');
        if (inside) {
            if (!arqWeb.hasClass(main_container, 'container')) arqWeb.addClass(main_container, 'container');
            if (!arqWeb.hasClass(navbar_container, 'container')) arqWeb.addClass(navbar_container, 'container');
            arqWeb.settings.set('main-container', 'fixed');
        } else {
            arqWeb.removeClass(main_container, 'container');
            arqWeb.removeClass(navbar_container, 'container');
            arqWeb.settings.unset('main-container', 'fixed');
        }
        document.getElementById('ace-settings-add-container').checked = inside;


        if (navigator.userAgent.match(/webkit/i)) {
            //webkit has a problem redrawing and moving around the sidebar background in realtime
            //so we do this, to force redraw
            //there will be no problems with webkit if the ".container" class is statically put inside HTML code.
            var sidebar = document.getElementById('sidebar')
            arqWeb.toggleClass(sidebar, 'menu-min')
            setTimeout(function () { arqWeb.toggleClass(sidebar, 'menu-min') }, 0)
        }
    },

    sidebar_collapsed: function (collpase) {
        collpase = collpase || false;

        var sidebar = document.getElementById('sidebar');
        var icon = document.getElementById('sidebar-collapse').querySelector('[class*="icon-"]');
        var $icon1 = icon.getAttribute('data-icon1');//the icon for expanded state
        var $icon2 = icon.getAttribute('data-icon2');//the icon for collapsed state

        if (collpase) {
            arqWeb.addClass(sidebar, 'menu-min');
            arqWeb.removeClass(icon, $icon1);
            arqWeb.addClass(icon, $icon2);

            arqWeb.settings.set('sidebar', 'collapsed');
        } else {
            arqWeb.removeClass(sidebar, 'menu-min');
            arqWeb.removeClass(icon, $icon2);
            arqWeb.addClass(icon, $icon1);

            arqWeb.settings.unset('sidebar', 'collapsed');
        }

    },

    set_skin: function (skin) {
        arqWeb.data.set('settings', 'main_skin', skin)        
    },
    select_skin: function () {
        type = arqWeb.data.get('settings', 'main_skin');
        $($(".colorpick-btn")[type]).trigger('click');
    },

    main_rtl: function (lado) {
        lado = lado || false;
        if (lado) {
            arqWeb.switch_direction(jQuery);
            arqWeb.settings.set('main_rtl', 'left');
        } else {
            arqWeb.switch_direction(jQuery);
            arqWeb.settings.unset('main_rtl', 'left');
        }
    }
}


//check the status of something
arqWeb.settings.check = function (item, val) {
    if (!arqWeb.settings.exists(item, val)) return;//no such setting specified
    var status = arqWeb.settings.is(item, val);//is breadcrumbs-fixed? or is sidebar-collapsed? etc

    var mustHaveClass = {
        'navbar-fixed': 'navbar-fixed-top',
        'sidebar-fixed': 'sidebar-fixed',
        'breadcrumbs-fixed': 'breadcrumbs-fixed',
        'sidebar-collapsed': 'menu-min',
        'main-container-fixed': 'container'
    }


    //if an element doesn't have a specified class, but saved settings say it should, then add it
    //for example, sidebar isn't .fixed, but user fixed it on a previous page
    //or if an element has a specified class, but saved settings say it shouldn't, then remove it
    //for example, sidebar by default is minimized (.menu-min hard coded), but user expanded it and now shouldn't have 'menu-min' class

    var target = document.getElementById(item);//#navbar, #sidebar, #breadcrumbs
    if (status != arqWeb.hasClass(target, mustHaveClass[item + '-' + val])) {
        arqWeb.settings[item.replace('-', '_') + '_' + val](status);//call the relevant function to mage the changes
    }
}




//save/retrieve data using localStorage or cookie
//method == 1, use localStorage
//method == 2, use cookies
//method not specified, use localStorage if available, otherwise cookies
arqWeb.data_storage = function (method, undefined) {
    var prefix = 'arquitetaWeb.';

    var storage = null;
    var type = 0;

    if ((method == 1 || method === undefined) && 'localStorage' in window && window['localStorage'] !== null) {
        storage = arqWeb.storage;
        type = 1;
    }
    else if (storage == null && (method == 2 || method === undefined) && 'cookie' in document && document['cookie'] !== null) {
        storage = arqWeb.cookie;
        type = 2;
    }

    //var data = {}
    this.set = function (namespace, key, value, undefined) {
        if (!storage) return;

        if (value === undefined) {//no namespace here?
            value = key;
            key = namespace;

            if (value == null) storage.remove(prefix + key)
            else {
                if (type == 1)
                    storage.set(prefix + key, value)
                else if (type == 2)
                    storage.set(prefix + key, value, arqWeb.config.cookie_expiry)
            }
        }
        else {
            if (type == 1) {//localStorage
                if (value == null) storage.remove(prefix + namespace + '.' + key)
                else storage.set(prefix + namespace + '.' + key, value);
            }
            else if (type == 2) {//cookie
                var val = storage.get(prefix + namespace);
                var tmp = val ? JSON.parse(val) : {};

                if (value == null) {
                    delete tmp[key];//remove
                    if (arqWeb.sizeof(tmp) == 0) {//no other elements in this cookie, so delete it
                        storage.remove(prefix + namespace);
                        return;
                    }
                }

                else {
                    tmp[key] = value;
                }

                storage.set(prefix + namespace, JSON.stringify(tmp), arqWeb.config.cookie_expiry)
            }
        }
    }

    this.get = function (namespace, key, undefined) {
        if (!storage) return null;

        if (key === undefined) {//no namespace here?
            key = namespace;
            return storage.get(prefix + key);
        }
        else {
            if (type == 1) {//localStorage
                return storage.get(prefix + namespace + '.' + key);
            }
            else if (type == 2) {//cookie
                var val = storage.get(prefix + namespace);
                var tmp = val ? JSON.parse(val) : {};
                return key in tmp ? tmp[key] : null;
            }
        }
    }


    this.remove = function (namespace, key, undefined) {
        if (!storage) return;

        if (key === undefined) {
            key = namespace
            this.set(key, null);
        }
        else {
            this.set(namespace, key, null);
        }
    }
}





//cookie storage
arqWeb.cookie = {
    // The following functions are from Cookie.js class in TinyMCE, Moxiecode, used under LGPL.

    /**
	 * Get a cookie.
	 */
    get: function (name) {
        var cookie = document.cookie, e, p = name + "=", b;

        if (!cookie)
            return;

        b = cookie.indexOf("; " + p);

        if (b == -1) {
            b = cookie.indexOf(p);

            if (b != 0)
                return null;

        } else {
            b += 2;
        }

        e = cookie.indexOf(";", b);

        if (e == -1)
            e = cookie.length;

        return decodeURIComponent(cookie.substring(b + p.length, e));
    },

    /**
	 * Set a cookie.
	 *
	 * The 'expires' arg can be either a JS Date() object set to the expiration date (back-compat)
	 * or the number of seconds until expiration
	 */
    set: function (name, value, expires, path, domain, secure) {
        var d = new Date();

        if (typeof (expires) == 'object' && expires.toGMTString) {
            expires = expires.toGMTString();
        } else if (parseInt(expires, 10)) {
            d.setTime(d.getTime() + (parseInt(expires, 10) * 1000)); // time must be in miliseconds
            expires = d.toGMTString();
        } else {
            expires = '';
        }

        document.cookie = name + "=" + encodeURIComponent(value) +
			((expires) ? "; expires=" + expires : "") +
			((path) ? "; path=" + path : "") +
			((domain) ? "; domain=" + domain : "") +
			((secure) ? "; secure" : "");
    },

    /**
	 * Remove a cookie.
	 *
	 * This is done by setting it to an empty value and setting the expiration time in the past.
	 */
    remove: function (name, path) {
        this.set(name, '', -1000, path);
    }
};


//local storage
arqWeb.storage = {
    get: function (key) {
        return window['localStorage'].getItem(key);
    },
    set: function (key, value) {
        window['localStorage'].setItem(key, value);
    },
    remove: function (key) {
        window['localStorage'].removeItem(key);
    }
};






//count the number of properties in an object
//useful for getting the number of elements in an associative array
arqWeb.sizeof = function (obj) {
    var size = 0;
    for (var key in obj) if (obj.hasOwnProperty(key)) size++;
    return size;
}

//because jQuery may not be loaded at this stage, we use our own toggleClass
arqWeb.hasClass = function (elem, className) {
    return (" " + elem.className + " ").indexOf(" " + className + " ") > -1;
}
arqWeb.addClass = function (elem, className) {
    if (!arqWeb.hasClass(elem, className)) {
        var currentClass = elem.className;
        elem.className = currentClass + (currentClass.length ? " " : "") + className;
    }
}
arqWeb.removeClass = function (elem, className) { arqWeb.replaceClass(elem, className); }
    
arqWeb.replaceClass = function (elem, className, newClass) {
    var classToRemove = new RegExp(("(^|\\s)" + className + "(\\s|$)"), "i");
    elem.className = elem.className.replace(classToRemove, function (match, p1, p2) {
        return newClass ? (p1 + newClass + p2) : " ";
    }).replace(/^\s+|\s+$/g, "");
}

arqWeb.toggleClass = function (elem, className) {
    if (arqWeb.hasClass(elem, className))
        arqWeb.removeClass(elem, className);
    else arqWeb.addClass(elem, className);
}




//data_storage instance used inside arqWeb.settings etc
arqWeb.data = new arqWeb.data_storage(arqWeb.config.storage_method);
