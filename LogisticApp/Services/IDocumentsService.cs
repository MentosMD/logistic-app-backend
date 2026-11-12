using LogisticApp.Entities;

namespace LogisticApp.Services
{
    public interface IDocumentsService
    {
        Task UploadDocument(string fileName, string folderName);
        Task<Document> GetDocumentContentById(Guid id);
    }
}
