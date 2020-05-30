
    
function download_csv(array) {
    var obj = [];
    array.forEach(function(row) {
        obj.push(Object.values(row));
    });
    //need to loop 
    var csv = '';

    //Titles
    var keys = [];
    keys.push(Object.keys(array[0]));
    keys.forEach(function (row) {
        csv += row.join(',');
        csv += "\n";
    });

    //Values
    obj.forEach(function(row) {
        csv += row.join(',');
        csv += "\n";
    });

    //console.log(csv);
   
    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';
    hiddenElement.download = 'ZAX Results_' + GetTodaysDate()+'.csv';
    hiddenElement.click();
}


function GetTodaysDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = dd + '-' + mm + '-' + yyyy;
    return today;
}
