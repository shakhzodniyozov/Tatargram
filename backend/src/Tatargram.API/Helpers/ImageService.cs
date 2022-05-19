using Tatargram.Interfaces;
using Tatargram.Models;

namespace Tatargram.Helpers;

public class ImageService
{
    public ImageService(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    private readonly IWebHostEnvironment environment;
    private string ImagePath { get => environment.WebRootPath; }
    public async Task<List<Image>> SetImages(IEntity entity, string[] photos)
    {
        string type = entity.GetType().Name;
        Directory.CreateDirectory(Path.Combine(ImagePath, "images", type, entity.Id.ToString()));
        var images = new List<Image>();

        foreach (var p in photos)
        {
            string fileExtension = ParseFileExtension(p);
            string fileName = Path.Combine(ImagePath, "images", type, entity.Id.ToString(), $"{Guid.NewGuid().ToString()}.{fileExtension}");
            byte[] imageBytes = Convert.FromBase64String(p.Substring(p.IndexOf(',') + 1));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
                await stream.WriteAsync(imageBytes, 0, imageBytes.Count());

            var image = new Image();
            if (entity.GetType().Name == nameof(Post))
                image.PostId = entity.Id;
            else if (entity.GetType().Name == nameof(User))
                image.UserId = entity.Id;

            images.Add(new Image() { AbsolutePath = fileName });
        }

        return images;
    }

    private string ParseFileExtension(string base64)
    {
        string head = base64.Split(';')[0];
        string extension = head.Substring(base64.IndexOf('/') + 1);

        return extension;
    }

    public void DeletePostImages(Guid id)
    {
        Directory.Delete(Path.Combine(ImagePath, "images", "Post", id.ToString()), true);
    }

    public void DeleteImage(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch { }
    }
}