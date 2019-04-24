@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)
                                              settings.Name = "GridView"
                                              settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartial"}
                                              settings.CustomActionRouteValues = New With {.Controller = "Home", .Action = "GridViewCustomActionPartial"}
                                              settings.SettingsEditing.BatchUpdateRouteValues = New With {Key .Controller = "Home", Key .Action = "BatchUpdatePartial"}
                                              settings.SettingsEditing.Mode = GridViewEditingMode.Batch

                                              settings.CommandColumn.Visible = True
                                              settings.CommandColumn.ShowDeleteButton = True
                                              settings.CommandColumn.ShowNewButtonInHeader = True
                                              settings.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "CopyButton", .Text = "Copy"})
                                              settings.CellEditorInitialize = Sub(s, e)
                                                                                  Dim editor As ASPxEdit = CType(e.Editor, ASPxEdit)
                                                                                  editor.ValidationSettings.Display = Display.Dynamic

                                                                              End Sub
                                              settings.KeyFieldName = "ID"
                                              settings.ClientSideEvents.BatchEditStartEditing = "OnStartEdit"
                                              settings.ClientSideEvents.CustomButtonClick = "OnCustomButtonClick"
                                              Dim mode As String = TryCast(Session("Mode"), String)
                                              If mode = "Row" Then
                                                  settings.SettingsEditing.BatchEditSettings.EditMode = GridViewBatchEditMode.Row
                                              Else
                                                  settings.SettingsEditing.BatchEditSettings.EditMode = GridViewBatchEditMode.Cell
                                              End If
                                              settings.Columns.Add("C1")
                                              settings.Columns.Add(Sub(column)
                                                                       column.FieldName = "C2"
                                                                       column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                                              settings.Columns.Add("C3")
                                              settings.Columns.Add(Sub(column)
                                                                       column.FieldName = "C4"
                                                                       column.ColumnType = MVCxGridViewColumnType.CheckBox
                                                                   End Sub)
                                              settings.Columns.Add(Sub(column)
                                                                       column.FieldName = "C5"
                                                                       column.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                   End Sub)
                                          End Sub)
End Code
@grid.Bind(Model).GetHtml()