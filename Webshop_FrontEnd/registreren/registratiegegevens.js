async function GetJson(url) {
    let response = await fetch(url);
    let data = await response.json()
    return data;
}
async function buildHtmlTable(selector) {
    let jsondata = '';

    //44340
    let apiUrl = 'https://localhost:44340/Userdata_1';
    jsondata = await GetJson(apiUrl)
    console.log(jsondata);
    var countRows = Object.keys(jsondata).length;
    if(countRows>0) {
        var colHeaders=addAllColumnHeaders(jsondata[0], selector);
        for (var key in jsondata) {
            addRowData(jsondata[key], colHeaders, selector);
        }
    }
}

function addAllColumnHeaders(rowData, selector) {
    var columnSet = [];
    var headerTr$ = $('<tr/>');
    for(var key in rowData)
    {
        columnSet.push(key);
        headerTr$.append($('<th/>').html(key));
    }
    $(selector).append(headerTr$);
    return columnSet;
}
function addRowData(rowData, columns, selector){
    var row$ = $('<tr/>');
    for (var key in columns)
    {
        var cellValue = rowData[columns[key]];
        if (cellValue == null)
            cellValue = "";
        row$.append($('<td/>').html(cellValue));
    }
    $(selector).append(row$);
}