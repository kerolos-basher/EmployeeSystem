#pragma checksum "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85fb35daf250db6b60e16aeec3cd9b872cef6b4c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\_ViewImports.cshtml"
using Idintitycorepro;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\_ViewImports.cshtml"
using Idintitycorepro.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\_ViewImports.cshtml"
using Idintitycorepro.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\_ViewImports.cshtml"
using Idintitycorepro.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85fb35daf250db6b60e16aeec3cd9b872cef6b4c", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"969a0baa0bb5828d9c3bb8671ac619e609ceb1e6", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\Shared\Error.cshtml"
 if (ViewBag.ErrorTitle == null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"text-danger\">Error</h1>\r\n    <h2 class=\"text-danger\">An error occurred while processing your request.</h2>\r\n");
#nullable restore
#line 9 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\Shared\Error.cshtml"

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"text-danger\">Error</h1>\r\n    <h2 class=\"text-danger\">");
#nullable restore
#line 14 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 15 "E:\silvia hard\Abdelrhman Core\kerolospasher\Idintitycorepro\Idintitycorepro\Views\Shared\Error.cshtml"

}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
