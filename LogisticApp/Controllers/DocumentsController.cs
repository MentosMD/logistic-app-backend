using Confluent.Kafka;
using LogisticApp.Dto;
using LogisticApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LogisticApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService documentsService;
        private readonly IProducer<string, string> _producer;

        public DocumentsController(IDocumentsService documentsService, IProducer<string, string> producer) 
        {
            this.documentsService = documentsService;
            this._producer = producer;
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file");

            if (Path.GetExtension(file.FileName).ToLowerInvariant() != ".pdf")
                return BadRequest("Only PDFs allowed.");

            var fileName = $"{Guid.NewGuid()}.pdf";
            var folderName = "Plata";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", folderName, fileName);
            using (var stream = new FileStream(path, FileMode.Create)) await file.CopyToAsync(stream);

            await documentsService.UploadDocument(fileName, folderName);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetDocumentContentById(Guid id)
        {
            var document = await documentsService.GetDocumentContentById(id);
            if (document == null) return NotFound("Document not found.");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", document.Folder, document.Name);
            if (!System.IO.File.Exists(path)) return NotFound("File not found on server.");

            var content = await System.IO.File.ReadAllTextAsync(path);
            return content;
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishMessage()
        {
            try
            {
                var docEvent = new
                {
                    Id = 145,
                    Content = "Внимание: при перевозке химикатов..."
                };

                var message = new Message<string, string> { Value = JsonSerializer.Serialize(docEvent) };

                // Non-blocking asynchronous message publishing
                var deliveryReport = await _producer.ProduceAsync("queue.upload.documents", message);

                return Ok(new
                {
                    Status = deliveryReport.Status.ToString(),
                    Partition = deliveryReport.Partition.Value,
                    Offset = deliveryReport.Offset.Value
                });
            }
            catch (ProduceException<string, string> ex)
            {
                return StatusCode(500, $"Delivery failed: {ex.Error.Reason}");
            }
        }
    }
}
