#pragma checksum "D:\Programming\ATL\Task\JqueryImprovement\OracleTaskWithDataTables\OracleTask\Views\Location\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "983c7e47359eea5e5199705f0142a6e5137c999e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Location_Index), @"mvc.1.0.view", @"/Views/Location/Index.cshtml")]
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
#line 1 "D:\Programming\ATL\Task\JqueryImprovement\OracleTaskWithDataTables\OracleTask\Views\_ViewImports.cshtml"
using OracleTask;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Programming\ATL\Task\JqueryImprovement\OracleTaskWithDataTables\OracleTask\Views\_ViewImports.cshtml"
using OracleTask.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Programming\ATL\Task\JqueryImprovement\OracleTaskWithDataTables\OracleTask\Views\_ViewImports.cshtml"
using OracleTask.Entity.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"983c7e47359eea5e5199705f0142a6e5137c999e", @"/Views/Location/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf40e8f225a770016627452d4e9db52044f87f80", @"/Views/_ViewImports.cshtml")]
    public class Views_Location_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LocationDTO>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/css/dataTables.bootstrap4.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/js/jquery.dataTables.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/js/dataTables.bootstrap4.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Programming\ATL\Task\JqueryImprovement\OracleTaskWithDataTables\OracleTask\Views\Location\Index.cshtml"
  
	ViewData["Title"] = "Locations";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Locations</h1>\r\n\r\n<hr />\r\n");
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "983c7e47359eea5e5199705f0142a6e5137c999e5239", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<div class=\"container\">\r\n\t<br />\r\n\t<div");
            BeginWriteAttribute("style", " style=\"", 418, "\"", 426, 0);
            EndWriteAttribute();
            WriteLiteral(@">
		<table id=""locationTable"" class=""table table-striped table-bordered dt-responsive nowrap"" width=""100%"" cellspacing=""0"">
			<thead>
				<tr>
					<th>
						Id
					</th>
					<th>
						Latitude
					</th>
					<th>
						Longitude
					</th>
					<th>
						Mark As
					</th>
					<th>
						User Username
					</th>
					<th>
						Edit
					</th>
					<th>
						Delete
					</th>
				</tr>
			</thead>
		</table>
	</div>
</div>

<div class=""modal fade"" id=""add-location-modal"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
	<div class=""modal-dialog"" role=""document"">
		<div class=""modal-content"">
			<div class=""modal-header"">
				<h5 class=""modal-title"" id=""exampleModalLabel""></h5>
				<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
					<span aria-hidden=""true"">&times;</span>
				</button>
			</div>
			<div class=""modal-body"">
			</div>
		</div>
	</div>
</div>

<div class=""modal fade"" id=""edit-location-modal"" tabindex=""-1"" role=""d");
            WriteLiteral(@"ialog"" aria-hidden=""true"">
	<div class=""modal-dialog"" role=""document"">
		<div class=""modal-content"">
			<div class=""modal-header"">
				<h5 class=""modal-title"" id=""exampleModalLabel""></h5>
				<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
					<span aria-hidden=""true"">&times;</span>
				</button>
			</div>
			<div class=""modal-body"">
			</div>
		</div>
	</div>
</div>



<div class=""modal fade"" id=""show-map-modal"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
	<div class=""modal-dialog modal-lg"" role=""document"">
		<div class=""modal-content"">
			<div class=""modal-header"">
				<h5 class=""modal-title"" id=""exampleModalLabel""></h5>
				<button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
					<span aria-hidden=""true"">&times;</span>
				</button>
			</div>
			<div class=""modal-body"">
			</div>
		</div>
	</div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "983c7e47359eea5e5199705f0142a6e5137c999e8715", async() => {
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
                WriteLiteral("</script>\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "983c7e47359eea5e5199705f0142a6e5137c999e9813", async() => {
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
                WriteLiteral("</script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LocationDTO>> Html { get; private set; }
    }
}
#pragma warning restore 1591
