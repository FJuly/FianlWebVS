using System.Web.Mvc;

namespace UI.Areas.PersonnalManger
{
    public class PersonnalMangerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PersonalManger";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PersonnalManger_default",
                "PersonalManger/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                //控制器所在命名空间
                new string[1] { "PersonalManger" }
            );
        }
    }
}
