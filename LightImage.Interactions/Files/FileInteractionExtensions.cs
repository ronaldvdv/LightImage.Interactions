using LightImage.Interactions.Files;
using System.IO;
using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public static class FileInteractionExtensions
    {
        public static async Task<FileInfo[]> SelectFiles(this IInteractionService service)
        {
            var result = await service.Show<SelectFileInput, SelectFileOutput>(new SelectFileInput());
            return result?.Files;
        }
    }
}