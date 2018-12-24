using System;
using System.Web.Mvc;
using LOB.BLL;
using LOB.Core;
using LOB.Web.Models;

namespace LOB.Web.Controllers
{
    public class ObjectsController : Controller
    {
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

        public ActionResult Index()
        {
            ViewBag.Title = "ElementTypes";
            DataModel model = new DataModel();
            string query = "";
            model.ElementTypes = ElementTypes.GetElementTypes(query);
            foreach (ElementType item in model.ElementTypes)
            {
                item.AttributesCount = ElementTypeAttributes.CountElementTypeAttributes(item.Id);
            }
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