#pragma checksum "/Users/Guest/Desktop/HairSalon.Solution/HairSalon/Views/Stylists/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c1eebc48f20440358114c6122db074dfdb6096c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Stylists_Index), @"mvc.1.0.view", @"/Views/Stylists/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Stylists/Index.cshtml", typeof(AspNetCore.Views_Stylists_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1eebc48f20440358114c6122db074dfdb6096c3", @"/Views/Stylists/Index.cshtml")]
    public class Views_Stylists_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 19, true);
            WriteLiteral("<h1>Stylists</h1>\n\n");
            EndContext();
#line 3 "/Users/Guest/Desktop/HairSalon.Solution/HairSalon/Views/Stylists/Index.cshtml"
 foreach (var stylist in Model)
{

#line default
#line hidden
            BeginContext(53, 8, true);
            WriteLiteral("  <h3><a");
            EndContext();
            BeginWriteAttribute("href", " href=\'", 61, "\'", 94, 2);
            WriteAttributeValue("", 68, "/stylists/", 68, 10, true);
#line 5 "/Users/Guest/Desktop/HairSalon.Solution/HairSalon/Views/Stylists/Index.cshtml"
WriteAttributeValue("", 78, stylist.GetId(), 78, 16, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(95, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(97, 17, false);
#line 5 "/Users/Guest/Desktop/HairSalon.Solution/HairSalon/Views/Stylists/Index.cshtml"
                                      Write(stylist.GetName());

#line default
#line hidden
            EndContext();
            BeginContext(114, 10, true);
            WriteLiteral("</a></h3>\n");
            EndContext();
#line 6 "/Users/Guest/Desktop/HairSalon.Solution/HairSalon/Views/Stylists/Index.cshtml"
}

#line default
#line hidden
            BeginContext(126, 37, true);
            WriteLiteral("\n<p><a href=\'/\'>Back To Home</a></p>\n");
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
