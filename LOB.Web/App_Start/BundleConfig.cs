using System.Web;
using System.Web.Optimization;

namespace LOB.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/js/bootstrap.bundle.min.js", "~/js/popper.min.js", "~/js/bootstrap-toggle.min.js", "~/js/default.js"));

            bundles.Add(new StyleBundle("~/css/default").Include("~/css/bootstrap.min.css", "~/css/bootstrap-reboot.min.css", "~/css/bootstrap-grid.min.css", "~/css/bootstrap-toggle.min.css", "~/css/default.css"));
        }
    }
}
