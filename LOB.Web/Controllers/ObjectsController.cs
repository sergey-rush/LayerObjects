using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LOB.BLL;
using LOB.Core;
using LOB.Web.Code;
using LOB.Web.Models;

namespace LOB.Web.Controllers
{
    public class ObjectsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Con";
            DataModel model = new DataModel();
            List<SelectListItem> keys = Settings.GetConnectionStrings();
            model.ConnectionStrings = keys;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DataModel model)
        {
            ViewBag.Title = "Con";
            Settings.SetConnectionString(model.SelectedConnectionString);
            List<SelectListItem> keys = Settings.GetConnectionStrings();
            model.ConnectionStrings = keys;

            return View(model);
        }

        [HttpGet]
        public ActionResult ElementTypeList()
        {
            ViewBag.Title = "ElementTypeList";
            DataModel model = new DataModel();
            string query = "";
            model.ElementTypes = ElementTypes.GetElementTypes(query);
            foreach (ElementType item in model.ElementTypes)
            {
                item.AttributesCount = ElementTypeAttributes.CountElementTypeAttributes(item.Id);
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult ElementTypeList(DataModel model)
        {
            ViewBag.Title = "ElementTypeList";
            model.ElementTypes = ElementTypes.GetElementTypes(model.Query);
            foreach (ElementType item in model.ElementTypes)
            {
                item.AttributesCount = ElementTypeAttributes.CountElementTypeAttributes(item.Id);
            }
            return View(model);
        }
        
        [GuidSelector]
        public ActionResult DeleteElementType(Guid id)
        {
            bool result = ElementTypes.DeleteElementTypeByElementTypeId(id);
            return RedirectToLocal("");
        }

        [HttpGet]
        public ActionResult AddElementType()
        {
            DataModel model = new DataModel();
            model.DrawingTypes = DrawingTypes.GetDrawingTypes();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddElementType(DataModel model)
        {
            Guid id = ElementTypes.InsertElementType(model.SelectedElementType);
            model.DrawingTypes = DrawingTypes.GetDrawingTypes();
            return View(model);
        }

        [GuidSelector]
        public ActionResult ElementTypeAttributeList(Guid id)
        {
            ViewBag.Title = "ElementTypeAttributes";
            DataModel model = new DataModel();
            model.ElementTypeAttributes = ElementTypeAttributes.GetAttributesByElementTypeId(id);
            model.SelectedElementType = ElementTypes.GetElementTypeByElementTypeId(id);

            foreach (ElementTypeAttribute item in model.ElementTypeAttributes)
            {
                item.AttrValueCount = Attributes.CountAttributeValues(item.AttributeId);
            }

            return View(model);
        }

        [GuidSelector]
        public ActionResult AttributeValueList(Guid id, int? page)
        {
            ViewBag.Title = "AttributeValues";

            int pageNum = 1;
            if (page != null)
            {
                pageNum = page.Value;
                ViewBag.Title = String.Format("AttributeValues page {0}", pageNum);
            }

            DataModel model = new DataModel();
            model.SelectedAttributeValue = new AttributeValue();
            int pageSize = 100;
            model.AttributeValues = Attributes.GetPagedAttributeValues(pageNum, pageSize, id);
            int itemsCount = Attributes.CountAttributeValues(id);
            model.Pager = new Pager(itemsCount, pageNum, pageSize);
            return View(model);
        }

        [GuidSelector]
        public ActionResult AttributeValueMap(Guid id)
        {
            ViewBag.Title = "Element Map";
            DataModel model = new DataModel();
            model.SelectedAttributeValue = Attributes.GetAvgAttributeValueByAttributeId(id);
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Objects");
            }
        }
    }
}