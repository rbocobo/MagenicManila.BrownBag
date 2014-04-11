/// <reference path="../qunit.js" />
/// <reference path="../../../magenicmanila.brownbag.ui.web/scripts/_references.js" />

QUnit.module("Sampple.Tests", {
	setup: function () {
		$("#qunit-fixture").append("<div class='someclass'>Data</div>");
	}
});

QUnit.test("test1", function (assert) {
	var html = $('.someclass').html();
	QUnit.equal("Data", "Data", "Should be equal to Data");
});