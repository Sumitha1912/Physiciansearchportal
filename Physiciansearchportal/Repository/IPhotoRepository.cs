using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Physiciansearchportal.Repository
{
    public interface IPhotoRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
 