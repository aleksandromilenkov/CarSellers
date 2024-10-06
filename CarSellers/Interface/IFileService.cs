namespace CarSellers.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension);
}
