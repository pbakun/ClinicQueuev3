#pragma checksum "c:\_Kod\ClinicQueuev3\QueueSystem\WebApp\Areas\Main\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f7a8c8c65ba0cd719a490bec4faeca2c388d2343"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Main_Views_Home_Index), @"mvc.1.0.view", @"/Areas/Main/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Main/Views/Home/Index.cshtml", typeof(AspNetCore.Areas_Main_Views_Home_Index))]
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
#line 1 "c:\_Kod\ClinicQueuev3\QueueSystem\WebApp\Areas\Main\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#line 2 "c:\_Kod\ClinicQueuev3\QueueSystem\WebApp\Areas\Main\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7a8c8c65ba0cd719a490bec4faeca2c388d2343", @"/Areas/Main/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Areas/Main/Views/_ViewImports.cshtml")]
    public class Areas_Main_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "c:\_Kod\ClinicQueuev3\QueueSystem\WebApp\Areas\Main\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(88, 244, true);
            WriteLiteral("\r\n<br />\r\n<div class=\"container\">\r\n    <div class=\"col-3\">\r\n        <div class=\"btn btn-info btn-lg\">Doctor</div>\r\n    </div>\r\n    <div class=\"col-3\">\r\n        <a class=\"btn btn-dark btn-lg\" href=\"./patient/12\">Pok. 12</a>\r\n    </div>\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
