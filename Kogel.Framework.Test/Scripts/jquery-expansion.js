/*jQuery扩展方法*/
var jQueryExpansion = {};
//多事件绑定(需要配合adonis.full.js一起使用)
$.fn.addBind = function (obj, callback) {
    //绑定标识
    var bindId = Common.getGuid().replace(/-/g, "");
    if (jQueryExpansion[bindId] == undefined) {
        jQueryExpansion[bindId] = [];
        $(this).attr("bind-id", bindId);
    }
    jQueryExpansion[bindId].push(callback);
    $(this).unbind();
    $(this).bind(obj, function (e) {
        var bindId = $(this).attr("bind-id");
        for (var i = 0; i < jQueryExpansion[bindId].length; i++) {
            jQueryExpansion[bindId][i].call(this, e);
        }
    });
};
//根据model写入form值
$.fn.setForm = function (jsonValue, callback) {
    var obj = this;
    for (var name in jsonValue) {
        var ival = jsonValue[name];
        var $oinput = obj.find("input[name=" + name + "]");
        if ($oinput.attr("type") == "checkbox") {
            if (ival !== null) {
                var checkboxObj = $("[name=" + name + "]");
                var checkArray = ival.split(";");
                for (var i = 0; i < checkboxObj.length; i++) {
                    for (var j = 0; j < checkArray.length; j++) {
                        if (checkboxObj[i].value == checkArray[j]) {
                            checkboxObj[i].click();
                        }
                    }
                }
            }
        }
        else if ($oinput.attr("type") == "radio") {
            $oinput.each(function () {
                var radioObj = $("[name=" + name + "]");
                for (var i = 0; i < radioObj.length; i++) {
                    if (radioObj[i].value == ival) {
                        radioObj[i].click();
                    }
                }
            });
        }
        else if ($oinput.attr("type") == "textarea") {
            obj.find("[name=" + name + "]").html(ival);
        }
        else {
            obj.find("[name=" + name + "]").val(ival);
        }
        if (typeof callback === "function")
            callback.call($oinput != undefined && $oinput.length >= 1 ? $oinput[0] : undefined, name, ival);
    }
}
//生成动态表条件
$.fn.serializeQuery = function () {
    debugger;
    var dynamicWhere = {};
    var inputArr = $(this).find("input");
    for (var i = 0; i < inputArr.length; i++) {
        var item = $(inputArr[i]);
        //生成一个唯一键
        var key = 'xxxxxxxxxxxx4xxxyxxxxxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
        //值
        var value =
        {
            Table: item.attr("data-table"),//查询的表(默认为后台设置)
            Field: item.attr("name"),//字段名(必填)
            Operators: item.attr("data-operator"),//运算符(默认为13，Equal)
            Value: item.val(),//值
            ValueType: item.attr("data-type"),//值类型(默认为16,string)
        };
        dynamicWhere[key] = value;
    }
    return dynamicWhere;
}