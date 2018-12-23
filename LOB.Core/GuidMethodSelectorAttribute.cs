using System;
using System.Reflection;
using System.Web.Mvc;

namespace LOB.Core
{
    public class GuidSelectorAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            object idValue = controllerContext.RouteData.Values["id"];
            if (idValue == null)
            {
                return false;
            }
            Guid output;
            bool result = Guid.TryParse(idValue.ToString(), out output);
            return result;
        }
    }
}