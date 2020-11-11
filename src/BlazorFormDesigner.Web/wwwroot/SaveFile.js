function FileSaveAs(url) {
    var link = document.createElement('a');
    link.download = "answers.xlsx";
    link.href = url;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    //location.reload();
}