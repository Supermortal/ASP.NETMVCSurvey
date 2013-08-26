$(document).ready(function () {
  $("#btnSurveyCancel").click(function () {
    document.location.href = '@Url.Action("Index", "Survey")';
  });

  $("#btnRemove0").click(function () {
    var id = $(this).attr("id");
    var index = id.substring(9);

    $("#trSurveyParam" + index).remove();
  });
});

if (!_surveyParamsCount || _surveyParamsCount == null) {
  var _surveyParamsCount = 1;
}

$("#btnSurveyParamAdd").click(function () {
  var table = $("#tblSurveyParameters");

  //var tr = document.createElement("tr");
  //$(tr).attr("id", "trSurveyParamHead" + _surveyParamsCount);

  //var th = document.createElement("th");
  //$(th).html("Question");
  //$(tr).append(th);

  //th = document.createElement("th");
  //$(th).html("Type");
  //$(tr).append(th);

  //th = document.createElement("th");
  //$(th).html("Required");
  //$(tr).append(th);

  //table.append(tr);

  var tr = document.createElement("tr");
  $(tr).addClass("surveyParam");
  $(tr).attr("id", "trSurveyParam" + _surveyParamsCount);

  var td = document.createElement("td");
  var txtSurveyName = document.createElement("input");
  $(txtSurveyName).attr("type", "text");
  $(txtSurveyName).attr("id", "txtSurveyQuestion" + _surveyParamsCount);
  $(td).append(txtSurveyName);
  $(tr).append(td);

  td = document.createElement("td");
  var typeSel = document.createElement("select");
  $(typeSel).attr("id", "selSurveyParamType" + _surveyParamsCount);

  var option = document.createElement("option");
  $(option).html("Text");
  $(typeSel).append(option);

  option = document.createElement("option");
  $(option).html("Number");
  $(typeSel).append(option);

  option = document.createElement("option");
  $(option).html("Decimal Number");
  $(typeSel).append(option);

  option = document.createElement("option");
  $(option).html("Currency");
  $(typeSel).append(option);

  option = document.createElement("option");
  $(option).html("Date");
  $(typeSel).append(option);

  //option = document.createElement("option");
  //$(option).html("Large Number");
  //$(typeSel).append(option);

  $(td).append(typeSel);
  $(tr).append(td);

  td = document.createElement("td");
  var reqSel = document.createElement("select");
  $(reqSel).attr("id", "selSurveyParamRequired" + _surveyParamsCount);

  option = document.createElement("option");
  $(option).html("False");
  $(reqSel).append(option);

  option = document.createElement("option");
  $(option).html("True");
  $(reqSel).append(option);

  $(td).append(reqSel);
  $(tr).append(td);

  td = document.createElement("td");
  var btnRemove = document.createElement("input");
  $(btnRemove).attr("id", "btnRemove" + _surveyParamsCount);
  $(btnRemove).attr("type", "button");
  $(btnRemove).val("Remove");
  $(btnRemove).click(function () {
    var id = $(this).attr("id");
    var index = id.substring(9);

    $("#trSurveyParam" + index).remove();
    $("#trSurveyParamHead" + index).remove();
  });

  $(td).append(btnRemove);
  $(tr).append(td);

  $(table).append(tr);

  _surveyParamsCount++;
});

$("#btnSurveySubmit").click(function () {
  var postData = {};

  postData.surveyName = $("#txtSurveyName").val();

  postData.surveyParameters = [];

  $(".surveyParam").each(function () {
    var surveyParameter = {};
    var children = $(this).children();

    //$($(children... gets the td child of the row, and then the input/select child of the td
    surveyParameter.Key = $($(children[0]).children()[0]).val();
    surveyParameter.Type = $($(children[1]).children()[0]).val();
    surveyParameter.Required = $($(children[2]).children()[0]).val();

    postData.surveyParameters.push(surveyParameter);
  });

  $.ajax({
      url: _MVCSurvey_HelperObject.postUrl,
    type: 'POST',
    contentType: 'application/json',
    dataType: 'json',
    data: JSON.stringify(postData),

    success: function (d) {
        document.location.href = _MVCSurvey_HelperObject.redirectUrl;
    },

    error: function (error) {
        console.error(error);
    }
  });
});