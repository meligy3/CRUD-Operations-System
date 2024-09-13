using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static System.Net.WebRequestMethods;

namespace Company.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadImage(IFormFile file, string FolderName)
        {
            // 1. get loacted folder path
            // string FolderPath = "pc path"
            // string FolderPath = Directory.GetCurrentDirectory()+"\\wwwroot\\files\\"+FolderName;
           string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);   

            // 2. get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}"; 
            
            // 3. get file path 
            string FilePath = Path.Combine(FolderPath, fileName);

            var fs = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(fs);

            return fileName;
        }

       
        //public static void DeleteFile(string fileName, string FolderName)
        
        //{
        //    if(fileName is not null && FolderName is not null)
        //    {

        //        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, fileName);
        //        if (File.Exists(filePath))

        //        {
        //            File.Delete(filePath);
        //        }

        //    }
            

        //}
    }
}
