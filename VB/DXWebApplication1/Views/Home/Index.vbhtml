<script type="text/javascript">
    var index;
    var copyFlag;
    function OnCustomButtonClick(s, e) {
        if (e.buttonID == "CopyButton") {
            index = e.visibleIndex;
            copyFlag = true;
            s.AddNewRow();
        }
    }
    function OnStartEdit(s, e) {
        if (copyFlag) {
            copyFlag = false;
            for (var i = 0; i < s.GetColumnsCount() ; i++) {
                var column = s.GetColumn(i);
                if (column.visible == false || column.fieldName == undefined)
                    continue;
                ProcessCells(rbl.GetSelectedIndex(), e, column, s);
            }
        }
    }
    function ProcessCells(selectedIndex, e, column, s) {
            var isCellEditMode = selectedIndex == 0;
            var cellValue = s.batchEditApi.GetCellValue(index, column.fieldName);
 
            if(isCellEditMode) {
                if(column.fieldName == e.focusedColumn.fieldName)
                    e.rowValues[column.index].value = cellValue;
                else
                    s.batchEditApi.SetCellValue(e.visibleIndex, column.fieldName, cellValue);
            } else {
                e.rowValues[column.index].value = cellValue;
            }
    }
    function OnSelectedIndexChanged(s, e) {
        GridView.PerformCallback({ key: s.GetSelectedItem().text })
    }
</script>
@Html.DevExpress().RadioButtonList(Sub(settings)
    settings.Name = "rbl"
    settings.Properties.Items.Add("Cell").Selected = True
    settings.Properties.Items.Add("Row")
    settings.Properties.ClientSideEvents.SelectedIndexChanged = "OnSelectedIndexChanged"
End Sub).GetHtml()
<form id="myForm">
    @Html.Action("GridViewPartial")
</form>
