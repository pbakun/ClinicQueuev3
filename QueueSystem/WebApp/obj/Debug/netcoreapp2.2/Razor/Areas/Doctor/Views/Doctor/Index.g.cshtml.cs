#pragma checksum "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0775f4c161660aa1eada975c551368138469a801"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Doctor_Views_Doctor_Index), @"mvc.1.0.view", @"/Areas/Doctor/Views/Doctor/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Doctor/Views/Doctor/Index.cshtml", typeof(AspNetCore.Areas_Doctor_Views_Doctor_Index))]
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
#line 1 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#line 2 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0775f4c161660aa1eada975c551368138469a801", @"/Areas/Doctor/Views/Doctor/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Areas/Doctor/Views/_ViewImports.cshtml")]
    public class Areas_Doctor_Views_Doctor_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApp.Models.Queue>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return false"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/queueServiceMaster.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml"
  
    ViewData["Title"] = "System Kolejkowy";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(127, 210, true);
            WriteLiteral("<br />\r\n\r\n<div class=\"backgroundWhite\">\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n            <h2 class=\"text-info\">Kolejka</h2>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"text-hide\" id=\"RoomNo\">");
            EndContext();
            BeginContext(338, 12, false);
#line 15 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml"
                                  Write(Model.RoomNo);

#line default
#line hidden
            EndContext();
            BeginContext(350, 14, true);
            WriteLiteral("</div>\r\n\r\n    ");
            EndContext();
            BeginContext(364, 1325, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0775f4c161660aa1eada975c551368138469a8015794", async() => {
                BeginContext(408, 305, true);
                WriteLiteral(@"
        <div class=""border-top border-dark"">
            <br />
            <div class=""row"">
                <div class=""col-2 offset-1 text-nowrap"">
                    <a class=""text-secondary"">Obecny pacjent:</a>
                    <a class=""text-left text-dark font-weight-bold"" id=""QueueNo"">");
                EndContext();
                BeginContext(714, 38, false);
#line 23 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml"
                                                                            Write(Html.DisplayFor(m => m.QueueNoMessage));

#line default
#line hidden
                EndContext();
                BeginContext(752, 930, true);
                WriteLiteral(@"</a>

                </div>
                <div class=""col-1"">
                    <a class=""btn btn-dark form-control text-light"" id=""PrevNo"">Back</a>
                </div>
                <div class=""col-1"">
                    <a class=""btn btn-dark form-control text-light"" id=""NextNo"">Next</a>
                </div>

                <div class=""col-1"">
                    <a class=""btn btn-info"" id=""Break"">Break</a>
                </div>

            </div>

            <div class=""row"">
                <div class=""col-4 offset-1"">

                    
                        <a class=""text-secondary"">Nadpisz numer:</a>
                        <input type=""text"" class=""text-dark border-dark"" id=""NewQueueNoInputBox""  />
                        <a class=""btn btn-success"" id=""NewQueueNoSubmit"">New</a>
                    

                </div>
            </div>
        </div>
    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1689, 168, true);
            WriteLiteral("\r\n\r\n\r\n</div>\r\n\r\n<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js\"></script>\r\n\r\n<script type=\"text/javascript\">\r\n    var roomNo = parseInt(\'");
            EndContext();
            BeginContext(1858, 12, false);
#line 59 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml"
                      Write(Model.RoomNo);

#line default
#line hidden
            EndContext();
            BeginContext(1870, 33, true);
            WriteLiteral("\');\r\n    var queueNo = parseInt(\'");
            EndContext();
            BeginContext(1904, 13, false);
#line 60 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml"
                       Write(Model.QueueNo);

#line default
#line hidden
            EndContext();
            BeginContext(1917, 28, true);
            WriteLiteral("\');\r\n    var id = parseInt(\'");
            EndContext();
            BeginContext(1946, 8, false);
#line 61 "c:\_Moje\C#\Clinicv3\QueueSystem\WebApp\Areas\Doctor\Views\Doctor\Index.cshtml"
                  Write(Model.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1954, 452, true);
            WriteLiteral(@"');

    //if enter was clicked while focus in input box call submit button clicked event
    $(""#NewQueueNoInputBox"").keyup(function (event) {   
        if (event.keyCode === 13) {
            //function definition in queueServiceMaster.js
            ForceNewQueueNo(document.getElementById(""NewQueueNoInputBox"").value);
            console.log(""event fired"");
            $(""#NewQueueNoSubmit"").click();
        }
    });

</script>

");
            EndContext();
            BeginContext(2406, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0775f4c161660aa1eada975c551368138469a80110967", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2467, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2469, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0775f4c161660aa1eada975c551368138469a80112147", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2519, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApp.Models.Queue> Html { get; private set; }
    }
}
#pragma warning restore 1591
