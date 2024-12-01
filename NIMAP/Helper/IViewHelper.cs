using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public interface IViewHelper
{
    Task<string> RenderPartialViewToString(Controller controller, string viewName, object model);
}
