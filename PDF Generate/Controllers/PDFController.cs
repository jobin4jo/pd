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
        private readonly IWebHostEnvironment env;
        public PDFController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet("PDFGenerate")]

        public async  Task<ActionResult> GeneratePdf(string number)
        {
            var data = new PdfDocument();
            string htmlContent = " <div style='border:soild 10x black'>";
            htmlContent += "<div>";
            htmlContent += "<div style = 'text-align: center;'>";
            htmlContent += "<h3> SemiOffice.Com Pharmacy </h3>";
            htmlContent += "<h3> Model Town, Lahore </h3>";
            htmlContent += "<h3> Ph:1234467987 </h3>";
            htmlContent += "</div>";
            htmlContent += "<div style = 'display: flex;'>";
            htmlContent += "<h4 style = 'text-align: left;' > Bill#</h4>";
            htmlContent += "<h4 style = 'padding-left: 80%;text-align: right;' > 45087 Lic.#</h4>";

            htmlContent += "</div>";
            htmlContent += "<div style = 'display: flex;'>";
            htmlContent += "<h4> Counter Sale </ h4 >";
            htmlContent += "<h4 style = 'padding-left: 80%;' > Date:</ h4 >";
            htmlContent += "</div>";
            htmlContent += "<table style = 'border: 2px solid black; border-collapse: collapse;'>";
            htmlContent += "<thead >";
            htmlContent += "<tr>";
            htmlContent += "<th style = 'border: 2px solid black; width: 200px;' > Qty </th>";
            htmlContent += "<th style = 'border: 2px solid black; width: 200px;' > Products </th>";
            htmlContent += "<th style = 'border: 2px solid black; width: 200px;' > Rate </th>";
            htmlContent += "<th style = 'border: 2px solid black; width: 200px;' > Amount </th>";
            htmlContent += "</tr>";
            htmlContent += "</thead>";
            htmlContent += "<tbody>";
            htmlContent += "<tr>";
            htmlContent += "<td style = 'border: 2px solid black;' > Dummy </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > fgetgfgvf </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > JDBS </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > IJBUH </td>";
            htmlContent += "</tr>";
            htmlContent += "</tbody>";
            htmlContent += "<tfoot>";
            htmlContent += "<tr>";
            htmlContent += "<td style = 'border: 2px solid black;' > T - items </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > 1 </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > Gross Total </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > ---</td>";
            htmlContent += "</tr >";
            htmlContent += "<tr >";
            htmlContent +="<td colspan = '2' rowspan = '2' > Return will be acctable with in 7 days </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > Discount </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > 3 </td>";
            htmlContent += "</tr>";
            htmlContent += "<tr>";


            htmlContent += "<td style = 'border: 2px solid black;' > Net Amount </td>";
            htmlContent += "<td style = 'border: 2px solid black;' > 105 </td>";
            htmlContent += "</tr>";
            htmlContent += "</tfoot>";
            htmlContent +=  "</table>";
       
            htmlContent += "  </div>";
            htmlContent +="</div>";
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



        [HttpPost("ImageUpload")]

        public async Task< ActionResult> imageUpload( )
        {
            bool result = false;
            try
            {
                var requestFiles = Request.Form.Files;
                foreach(IFormFile file in requestFiles)
                {
                    string fileName = file.FileName;
                    string FilePath = GetFilePath(fileName);
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }

                    string imagepath = FilePath + "\\image.png";

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        result = true;
                    }
                }

            }
            catch(Exception ex)
            {
                Ok(ex); 
            }
            return null;
        }

        [NonAction]
        private string GetFilePath(string ProductCode)
        {
            return this.env.WebRootPath + "\\Uploads\\Product\\" + ProductCode;
        }
    }
}
