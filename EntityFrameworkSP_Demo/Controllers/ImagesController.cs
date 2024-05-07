using EntityFrameworkSP_Demo.Constants;
using EntityFrameworkSP_Demo.Entities;
using EntityFrameworkSP_Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


namespace EntityFrameworkSP_Demo.Controllers
{
    [Authorize] // Secures all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageUploadDal _upload;

        public ImagesController(IImageUploadDal upload)
        {
            _upload = upload;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _upload.GetAll();
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _upload.Get(my => my.Id == id);
            return Ok(result);
        }

        [HttpPost("upload")]
        public IActionResult Upload([FromForm] ImageUpload imageUpload)
        {
            _upload.Add(imageUpload);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _upload.Delete(id);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] ImageUpload imageUpload)
        {
            _upload.Update(imageUpload);
            return Ok();
        }

        [HttpGet]
        [Route("DownloadFile")]
        private async Task<IActionResult> DownloadFile(string filename)
        {
            var Downloadfilepath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.Root, filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(Downloadfilepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(Downloadfilepath);
            return File(bytes, contenttype, Path.GetFileName(Downloadfilepath));
        }

        [HttpGet]
        [Route("GeneratePDFfromURL")]
        public async Task<IActionResult> GeneratePDFfromURL(string URLlink)
        {
            var renderer = new ChromePdfRenderer();

            var pdf = renderer.RenderUrlAsPdf(URLlink);

            string PDFfilename = DateTime.Now.Ticks.ToString() + ".pdf";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.Root, PDFfilename);

            try
            {
                pdf.SaveAs(filePath);
                return await DownloadFile(PDFfilename);
            }
            catch (IOException ex)
            {
                // Log the error
                Console.WriteLine("Error generating PDF: " + ex.Message);
                // Optionally, you can return a meaningful error response to the client
                return StatusCode(500, "Error generating PDF: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("GeneratePDFfromFile")]
        public async Task<IActionResult> GeneratePDFfromFile()
        {

            string PDFfilename = DateTime.Now.Ticks.ToString() + ".pdf";
            var renderer = new ChromePdfRenderer();
            var pdf = renderer.RenderHtmlFileAsPdf(@"c:\\Temp\\Uploads\\Upload\index.html");
            pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "FilePath.Root", PDFfilename));

            return await DownloadFile(PDFfilename);
        }

        [HttpGet]
        [Route("GeneratePDFfromhtml")]
        public async Task<IActionResult> GeneratePDFfromhtml(string URLlink)
        {


            string PDFfilename = DateTime.Now.Ticks.ToString() + ".pdf";
            var Renderer = new ChromePdfRenderer(); // Instantiates Chrome Renderer
            var pdf = Renderer.RenderHtmlAsPdf(" <h1> I am losing my interest in human beings; in the significance of their lives and their actions. Some one has said it is better to study one man than ten books. I want neither books nor men; they make me suffer. Can one of them talk to me like the night – the Summer night? Like the stars or the caressing wind?\r\n\r\nThe night came slowly, softly, as I lay out there under the maple tree. It came creeping, creeping stealthily out of the valley, thinking I did not notice. And the outlines of trees and foliage nearby blended in one black mass and the night came stealing out from them, too, and from the east and west, until the only light was in the sky, filtering through the maple leaves and a star looking down through every cranny.\r\n\r\nThe night is solemn and it means mystery.\r\n\r\nHuman shapes flitted by like intangible things. Some stole up like little mice to peep at me. I did not mind. My whole being was abandoned to the soothing and penetrating charm of the night.\r\n\r\nThe katydids began their slumber song: they are at it yet. How wise they are. They do not chatter like people. They tell me only: “sleep, sleep, sleep.” The wind rippled the maple leaves like little warm love thrills.\r\n\r\nWhy do fools cumber the Earth! It was a man’s voice that broke the necromancer’s spell. A man came to-day with his “Bible Class.” He is detestable with his red cheeks and bold eyes and coarse manner and speech. What does he know of Christ? Shall I ask a young fool who was born yesterday and will die tomorrow to tell me things of Christ? I would rather ask the stars: they have seen him.</h1> !");

            pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), FilePath.Root, PDFfilename));

            return await DownloadFile(PDFfilename);
        }
        [HttpGet]
        [Route("ConvertPdfToBase64")]
        public IActionResult ConvertPdfToBase64(string filename)
        {
            string pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.Root, filename);

            try
            {
                string base64String = PdfToImageConverter.ConvertPdfToBase64(pdfFilePath);
                return Ok(base64String);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine("Error converting PDF to base64: " + ex.Message);
                // Optionally, you can return a meaningful error response to the client
                return StatusCode(500, "Error converting PDF to base64: " + ex.Message);
            }
        }

    }


}


