<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128550233/19.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T115891)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# GridView for MVC - How to implement clone functionality in batch edit mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t115891/)**
<!-- run online end -->

This example demonstrates how implement a custom **Copy** button that allows users to clone a row in [GridView](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.GridViewExtension) extension in batch edit mode. 

![Grid View - Clone a Row](clone-grid-line.png)

To implement this functionality, follow the steps below.

1. Create a [custom command button](https://docs.devexpress.com/AspNet/DevExpress.Web.GridViewCommandColumn.CustomButtons) and handle the client [CustomButtonClick](https://docs.devexpress.com/AspNet/js-ASPxClientGridView.CustomButtonClick) event.

```cshtmml
var grid = Html.DevExpress().GridView(settings => {
    settings.Name = "GridView";
    settings.SettingsEditing.Mode = DevExpress.Web.GridViewEditingMode.Batch;
    settings.CommandColumn.Visible = true;
    settings.CommandColumn.CustomButtons.Add(new DevExpress.Web.GridViewCommandColumnCustomButton() { ID = "CopyButton", Text = "Copy" });
    settings.ClientSideEvents.CustomButtonClick = "OnCustomButtonClick";
    @* ... *@
});
```

2. In the [CustomButtonClick](https://docs.devexpress.com/AspNet/js-ASPxClientGridView.CustomButtonClick) event handler, call the [AddNewRow](https://docs.devexpress.com/AspNet/js-ASPxClientGridView.AddNewRow) method to add a new row.

```js
function OnCustomButtonClick(s, e) {
    if (e.buttonID == "CopyButton") {
        // ...
        s.AddNewRow();
    }
}
```

3. Handle the client [BatchEditStartEditing](https://docs.devexpress.com/AspNet/js-ASPxClientGridView.BatchEditStartEditing) event to insert values of the previous row to the newly created row.

    Use the [rowValues](https://docs.devexpress.com/AspNet/js-ASPxClientGridViewBatchEditStartEditingEventArgs.rowValues) object to define a value for cells in edit mode (every cell in Row edit mode and the focused cell in Cell edit mode) and the client [SetCellValue](https://docs.devexpress.com/AspNet/js-ASPxClientGridViewBatchEditApi.SetCellValue(visibleIndex-columnFieldNameOrId-value)) method to assign values to cells that are not in edit mode (unfocused cells in Cell edit mode).

```cshtml
settings.ClientSideEvents.BatchEditStartEditing = "OnStartEdit";
```

```js
function OnStartEdit(s, e) {
  // ...
  for (var i = 0; i < s.GetColumnsCount() ; i++) {
      var column = s.GetColumn(i);
      if (column.visible == false || column.fieldName == undefined)
          continue;
      ProcessCells(rbl.GetSelectedIndex(), e, column, s);
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
```

## Files to Look At

* [_GridViewPartial.cshtml](./CS/T115891/Views/Home/_GridViewPartial.cshtml)
* [Index.cshtml](./CS/T115891/Views/Home/Index.cshtml)

## Documentation
- [Batch Edit Mode](https://docs.devexpress.com/AspNetMvc/16147/components/grid-view/concepts/data-editing-and-validation/batch-edit)

## More Examples
- [GridView for Web Forms - How to implement clone functionality in batch edit mode](https://github.com/DevExpress-Examples/asp-net-web-forms-gridview-clone-functionality-in-batch-edit-mode)
