using LogisticApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticApp.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly LogisticsDbContext _dbContext;

        public DocumentsService(LogisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Document> GetDocumentContentById(Guid id)
        {
            return await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == id);
        }

        public Task UploadDocument(string fileName, string folderName)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Name = fileName,
                Folder = folderName
            };

            _dbContext.Documents.Add(document);
            return _dbContext.SaveChangesAsync();
        }
    }
}
