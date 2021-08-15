using Microsoft.AspNetCore.Http;

namespace WebDoctors.Contracts
{
    public interface IImageUpload : IDocumetUpload<IFormFile>
    {
    }
}
