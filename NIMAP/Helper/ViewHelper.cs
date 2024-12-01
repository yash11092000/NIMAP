using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


public class ViewHelper : IViewHelper
{
    public async Task<string> RenderPartialViewToString(Controller controller, string viewName, object model)
    {
        if (controller == null)
            throw new ArgumentNullException(nameof(controller), "Controller cannot be null.");

        var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
        var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

        if (!viewResult.Success)
        {
            throw new Exception($"View '{viewName}' not found.");
        }

        controller.ViewData.Model = model;

        using (var writer = new StringWriter())
        {
            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }
    }
}
