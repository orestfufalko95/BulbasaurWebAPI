using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.utils;
using BulbasaurWebAPI.exception.exceptions;
using Microsoft.AspNetCore.Http;

namespace BulbasaurWebAPI.bl.Service
{
    public class MediaService : IMediaService

    {
        private readonly string[] _imageExtensions = {".jpg", ".jpeg", ".png", ".bmp"};
        private readonly string[] _videExtensions = {".mp4", ".avi", ".mkv" };
        private readonly string[] _audioExtensions = {".mp3", ".wav" };
        public async Task<string> SavePersonImage(IFormFile photo, int userId, string currentUrl)
        {
            Console.WriteLine("===============================\n\n\n\n");

            var extension = Path.GetExtension(photo.FileName).ToLower();
            Console.WriteLine(extension);
            if (!_imageExtensions.Contains(extension)) throw new InvalidImageException();

            var fileName = "User" + userId + extension;
            var photoUrl = MediaPath.ProfilePhotoPath + fileName;
            Console.WriteLine(fileName);
            Console.WriteLine(photoUrl);
          
            if (currentUrl != null)
            {
                Console.WriteLine($"currentURL After != null - {currentUrl}");
                File.Delete(currentUrl);
            }
            await WriteFile(photo, photoUrl);
            Console.WriteLine(RefactorPath(MediaPath.ProfilePhotoUrl + fileName));
            return RefactorPath(MediaPath.ProfilePhotoUrl + fileName);
        }

        public async Task<string> SaveMedia(IFormFile mediaFile, int messageId)
        {
            var extension = Path.GetExtension(mediaFile.FileName).ToLower();
            var fileName = "Message" + messageId + extension;
            string url;
            string resultMediaUrl;
            if (_imageExtensions.Contains(extension))
            {
                url = MediaPath.MessagePhotoPath + fileName;
                resultMediaUrl = MediaPath.MessagePhotoUrl + fileName;
            }
            else if (_videExtensions.Contains(extension))
            {
                url = MediaPath.MessageVideoPath + fileName;
                resultMediaUrl = MediaPath.MessageVideoUrl + fileName;
            }
            else if (_audioExtensions.Contains(extension))
            {
                url = MediaPath.MessageAudioPath + fileName;
                resultMediaUrl = MediaPath.MessageAudioUrl + fileName;
            }
            else 
            {
                url = MediaPath.MessageOtherPath + fileName;
                resultMediaUrl = MediaPath.MessageOtherUrl + fileName;
            }

            await WriteFile(mediaFile, url);
            return RefactorPath(resultMediaUrl);
        }

        private async Task WriteFile(IFormFile fileToWrite, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await fileToWrite.CopyToAsync(fileStream);
            }
        }

        private string RefactorPath(string url)
        {
           return  url.Replace('\\', '/');
        }
            
    }
}