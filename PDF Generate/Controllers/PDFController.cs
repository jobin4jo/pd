using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;

using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace PDF_Generate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {

        [HttpGet("PDFGenerate")]

        public async  Task<ActionResult> GeneratePdf(string number)
        {
            var data = new PdfDocument();
            string htmlContent = " <Div><h1>Hello Iam .NET Developers</h1>";
            htmlContent += "<p>Demo Content</p>";
            htmlContent +="</Div >";
            PdfGenerator.AddPdfPages(data, htmlContent,PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                data.Save(ms);
                response = ms.ToArray();
            }
            string fileName = "number" + number + ".pdf";
            return File(response, "application/pdf", fileName);


            
        }
    }
}
