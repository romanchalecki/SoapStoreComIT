#pragma checksum "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c08ec122941a65ec3951fafcbf3c97e817a0ba2b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__StoreItemsAreaPartial), @"mvc.1.0.view", @"/Views/Shared/_StoreItemsAreaPartial.cshtml")]
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
#line 1 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/_ViewImports.cshtml"
using SoapStoreComIT;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/_ViewImports.cshtml"
using SoapStoreComIT.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c08ec122941a65ec3951fafcbf3c97e817a0ba2b", @"/Views/Shared/_StoreItemsAreaPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e0cda0e799b5c1eca3b9fc4cd36e36d7b9869f", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__StoreItemsAreaPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<StoreItem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MoreStoreItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 5 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
 if (Model.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 71, "\"", 163, 4);
            WriteAttributeValue("", 79, "col-12", 79, 6, true);
            WriteAttributeValue(" ", 85, "post", 86, 5, true);
#nullable restore
#line 7 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
WriteAttributeValue(" ", 90, Model.FirstOrDefault().Category.Name.Replace(" ",string.Empty), 91, 63, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 154, "show-all", 155, 9, true);
            EndWriteAttribute();
            WriteLiteral(">\n        <div class=\"row\">\n            <h3 class=\"text-right\" style=\"color:lightblue\">");
#nullable restore
#line 9 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                                      Write(Model.FirstOrDefault().Category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\n        </div>\n\n");
#nullable restore
#line 12 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
         foreach(var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"border border-info rounded col-12\" style=\"margin-bottom: 10px;margin-top: 10px;padding: 10px;\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-md-3 col-sm-12\">\r\n");
#nullable restore
#line 17 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                         if(item.Image == null)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <img srcset=\"/images/Noimageavailable.png\" width=\"99%\" style=\"border-radius:5px;border:1px solid #bbb9b9\" />\n");
#nullable restore
#line 20 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <img");
            BeginWriteAttribute("src", " src=\"", 832, "\"", 849, 1);
#nullable restore
#line 21 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
WriteAttributeValue("", 838, item.Image, 838, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" width=""99%"" style=""border-radius:5px;border:1px solid #bbb9b9"" />
                        
                    </div>

                        <div class=""col-md-9 col-sm-12"">
                            <div class=""row pr-3"">
                                <div class=""col-8"">
                                    <label class=""text-right"" style=""font-size:20px; color:steelblue"">");
#nullable restore
#line 28 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                                                                                 Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                                </div>\n");
#nullable restore
#line 30 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                 if (item.Quantity > 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"col-4 text-right\" style=\"color:slateblue\">\n                                        <h4>CA$");
#nullable restore
#line 33 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                          Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\n                                    </div>\n");
#nullable restore
#line 35 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"col-4 text-right\" style=\"color:red\">\n                                        <h4>Sold Out</h4>\n                                    </div>\n");
#nullable restore
#line 41 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </div>\r\n\r\n                            <div class=\"row col-12 text-justify d-none d-md-block\">\r\n                                <p>");
#nullable restore
#line 45 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                              Write(Html.Raw(item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </div>\r\n\r\n                            <div class=\"col-md-3 col-sm-12 offset-md-9 text-center\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c08ec122941a65ec3951fafcbf3c97e817a0ba2b10070", async() => {
                WriteLiteral("More");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 49 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
                                                                                                                           WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n                </div>\n");
#nullable restore
#line 55 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"p-4\"></div>\n    </div> \n");
#nullable restore
#line 58 "/Users/Raman/Documents/GitHub/SoapStoreComIT/SoapStoreComIT/SoapStoreComIT/Views/Shared/_StoreItemsAreaPartial.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<StoreItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591
