using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T115891.Models;

namespace T115891.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            return PartialView("_GridViewPartial", BatchEditRepository.GridData);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchUpdatePartial(MVCxGridViewBatchUpdateValues<GridDataItem, int> batchValues)
        {
            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    BatchEditRepository.InsertNewItem(item, batchValues);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    BatchEditRepository.UpdateItem(item, batchValues);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                BatchEditRepository.DeleteItem(itemKey, batchValues);
            }
            return PartialView("_GridViewPartial", BatchEditRepository.GridData);
        }
        public ActionResult GridViewCustomActionPartial(string key)
        {
            Session["Mode"] = key;
            return PartialView("_GridViewPartial", BatchEditRepository.GridData);
        }
    }
}