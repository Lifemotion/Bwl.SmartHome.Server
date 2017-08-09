var _apiUrl = "/api/default.ashx";
var _lastState = "0";

function reloadObjects() {
	$.post(_apiUrl, {
		act: "get-obj-all"
	},
         function (data) {
         	objects = eval(data);
         	var tmpl = $(".objectItem.created").remove();

         	for (var i = 0; i < objects.length; i++) {
         		var obj = objects[i];
         		var guid = obj.Guid;
         		var caption = obj.Scheme.DefaultCaption;

         		var tmpl = $(".objectItem.template").clone().show();         		
         		tmpl.removeClass("template").addClass("created").appendTo("#objectsList");
         		tmpl.find(".objectText").html(caption);
         		tmpl.addClass("id_" + guid)
         		if (caption.includes("online")) {
         		    tmpl.css("background-color", "#E7FFE9");
         		}
         		if (caption.includes("offline")) {
         		    tmpl.css("background-color", "#FFDEDE");
         		}
         		for (var k = 0; k < obj.Scheme.States.length; k++) {
         			var state = obj.Scheme.States[k];
         			var stateId = state.ID;
         			var stateCaption = state.DefaultCaption;
         			var stateType = state.Type;
         			var stateValue = "";
         			for (var m = 0; m < obj.StateValues.length; m++)
         			{
         				if (obj.StateValues[m].ID == stateId) { stateValue = obj.StateValues[m].Value}
         			}
                        switch (stateType) {
                            case 101: //stateString
                                var stt = $(".textboxState.template").clone();
                                stt.removeClass("template").addClass("created").addClass("stateid_" + stateId).appendTo(".id_" + guid + " .states");
                                stt.find(".text").html(stateCaption);
                                var textbox = stt.find(".textbox.inactive");
                                textbox.prop('value', stateValue);
                                textbox.removeAttr("data-role").removeClass("inactive").textinput();
                                textbox.textinput('disable');
                                break;
                            case 102: //stateYesNo
                                var stt = $(".onOffState.template").clone();
                                stt.removeClass("template").addClass("created").addClass("stateid_" + stateId).appendTo(".id_" + guid + " .states");
                                stt.find(".text").html(stateCaption);
                                var checkbox = stt.find(".checkbox.inactive");
                                if (stateValue == "yes") {
                                    checkbox.prop('checked', true);
                                }

                                checkbox.removeAttr("data-role").removeClass("inactive").checkboxradio();
                                checkbox.checkboxradio('disable');
                                break;
                            case 1: //actionString
                                var stt = $(".textboxState.template").clone();
                                stt.removeClass("template").addClass("created").addClass("stateid_" + stateId).appendTo(".id_" + guid + " .states");
                                stt.find(".text").html(stateCaption);
                                var textbox = stt.find(".textbox.inactive");
                                textbox.prop('value', stateValue);
                                textbox.removeAttr("data-role").removeClass("inactive").textinput();
                                //not working yet
                              /*  (function (_guid, _stateId, _selector) {
                                    _selector.on("input", function (event) {
                                        var value = textbox.prop('value');
                                        textbox.prop('value', "tyete");
                                        $.post(_apiUrl, {
                                            act: "set-obj-state", oid: _guid, sid: _stateId, val: value
                                        }, function (data) {
                                        });
                                    });
                                })(guid, stateId, textbox);*/
                                break;
                            case 3: //actionOnOff
         					var stt = $(".onOffState.template").clone();
         					stt.removeClass("template").addClass("created").addClass("stateid_"+stateId).appendTo(".id_" + guid + " .states");
         					stt.find(".text").html(stateCaption);
         					var checkbox = stt.find(".checkbox.inactive");
         					if (stateValue == "on") {
         						checkbox.prop('checked', true);
         					}
         					checkbox.removeAttr("data-role").removeClass("inactive").checkboxradio();
         					(function (_guid, _stateId, _selector) {
         						_selector.on("click", function (event) {
         							var value = "off";
         							if (_selector.prop("checked") == true) {
         								value = "on";
         							}
         							$.post(_apiUrl, {
         								act: "set-obj-state", oid: _guid, sid: _stateId, val: value
         							}, function (data) {
         							});
         						});
         					})(guid, stateId, checkbox);
         					break;
                            case 2: //actionButton
         					var stt = $(".buttonState.template").clone();
         					stt.removeClass("template").addClass("created").addClass("stateid_" + stateId).appendTo(".id_" + guid + " .states");
         					var button = stt.find(".button.inactive");
         					button.val(stateCaption).removeAttr("data-role").removeClass("inactive").button();
         					(function (_guid, _stateId) {
         						button.on("vclick", function (event) {
         							$.post(_apiUrl, {
         								act: "set-obj-state", oid: _guid, sid: _stateId, val: "1"
         							}, function (data) {
         							});
         						});
         					})(guid, stateId);

         			}
         		}
         	}
         	var tmpl = $(".objectItem.template").hide();
         	$("#trackingResultList").listview('refresh');
         });
}

function startChangesMonitor() {
	setInterval(function () {
		$.post(_apiUrl, {
			act: "get-obj-all-hash"
		},
	 function (data) {
	 	if (data != _lastState) {
	 		_lastState=data;
	 		reloadObjects();
	 	}
	 });
	}, 1000);
}

$("document").ready(function () {
	$(".objectItem.template").hide();
	reloadObjects();
	startChangesMonitor();
});


$(document).bind("pagechange", function (event, data) {
	var pageid = data.toPage[0]["id"];
	if (pageid == "tracking") {

	}
});
