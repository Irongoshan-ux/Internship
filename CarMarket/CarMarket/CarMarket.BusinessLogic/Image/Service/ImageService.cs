using CarMarket.Core.Image.Domain;
using System;
using System.Collections.Generic;

namespace CarMarket.BusinessLogic.Image.Service
{
    public class ImageService
    {
        private static readonly List<string> TYPES_OF_IMAGE_FILES = new() { "JPEG", "JPG", "PNG" };

        public static string ConvertImageToDisplay(ImageModel image)
        {
            if (image is null) return string.Empty;

            var base64 = Convert.ToBase64String(image.ImageData);

            var result = string.Format("data:image/jpg;base64,{0}", base64);

            return result;
        }

        public static bool IsImage(ImageModel image)
        {
            var imageType = GetFileExtension(image.ImageTitle);

            return IsImageType(imageType);
        }

        public static bool IsImage(string fileName)
        {
            var imageType = GetFileExtension(fileName);

            return IsImageType(imageType);
        }

        private static bool IsImageType(string fileExtension) => TYPES_OF_IMAGE_FILES.Contains(fileExtension);

        private static string GetFileExtension(string fileName) => fileName[(fileName.LastIndexOf('.') + 1)..].ToUpper();
    }
}
