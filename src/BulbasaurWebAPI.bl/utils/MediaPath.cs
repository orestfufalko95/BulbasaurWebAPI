using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BulbasaurWebAPI.bl.utils
{
    public class MediaPath
    {

        private static readonly string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        private static readonly string RootFolder = ProjectPath.Substring(0, ProjectPath.LastIndexOf('\\'));

        public static string ProfilePhotoUrl = "\\Media\\Image\\Profile\\";
        public static string MessagePhotoUrl = "\\Media\\Image\\Message\\";
        public static string MessageVideoUrl = "\\Media\\Video\\";
        public static string MessageAudioUrl = "\\Media\\Audio\\";
        public static string MessageOtherUrl = "\\Media\\Other\\";

        public static string ProfilePhotoPath = RootFolder + ProfilePhotoUrl;
        public static string MessagePhotoPath = RootFolder + MessagePhotoUrl;
        public static string MessageVideoPath = RootFolder + MessageVideoUrl;
        public static string MessageAudioPath = RootFolder + MessageAudioUrl;
        public static string MessageOtherPath = RootFolder + MessageOtherUrl;

        public static string GetFolderPath(string url)
        {
            if (url == null)
            {
                return null;
            }
            url = RootFolder + url;
            return url.Replace('/', '\\');
        }

        public static string GetFileName(string fileUrl)
        {
            return fileUrl.Substring(fileUrl.LastIndexOf('\\') + 1);
        }
    }
}
