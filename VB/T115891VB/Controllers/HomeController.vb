Imports DevExpress.Web.Mvc
Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    <ValidateInput(False)>
    Public Function GridViewPartial() As ActionResult
        Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
    End Function

    <HttpPost, ValidateInput(False)>
    Public Function BatchUpdatePartial(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer)) As ActionResult
        For Each item In batchValues.Insert
            If batchValues.IsValid(item) Then
                BatchEditRepository.InsertNewItem(item, batchValues)
            Else
                batchValues.SetErrorText(item, "Correct validation errors")
            End If
        Next item
        For Each item In batchValues.Update
            If batchValues.IsValid(item) Then
                BatchEditRepository.UpdateItem(item, batchValues)
            Else
                batchValues.SetErrorText(item, "Correct validation errors")
            End If
        Next item
        For Each itemKey In batchValues.DeleteKeys
            BatchEditRepository.DeleteItem(itemKey, batchValues)
        Next itemKey
        Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
    End Function
    Public Function GridViewCustomActionPartial(ByVal key As String) As ActionResult
        Session("Mode") = key
        Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
    End Function
End Class