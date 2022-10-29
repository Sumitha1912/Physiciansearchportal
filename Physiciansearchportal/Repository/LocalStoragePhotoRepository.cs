using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;

namespace Physiciansearchportal.Repository
{
    public class LocalStoragePhotoRepository : IPhotoRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Photos", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Photos", fileName);
        }
    }
}
