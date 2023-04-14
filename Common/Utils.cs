using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Common
{
    public static class Utils
    {
        public static User ConvertUserToUserResponse(User user)
        {
            User userResponse = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl
            };
            return userResponse;
        }

        public static User ConvertUserResponseToUser(User userResponse)
        {
            User user = new User
            {
                Id = userResponse.Id,
                FirstName = userResponse.FirstName,
                Lastname = userResponse.Lastname,
                Email = userResponse.Email,
                Password = null,
                PhotoUrl = userResponse.PhotoUrl
            };

            return user;
        }

        public static ImageProperties ConvertImageBase64StringToByteArr(string base64ImageString)
        {
            byte[] imageBytes = new byte[] { 0 };
            string fileExtension = "";
            if (!string.IsNullOrEmpty(base64ImageString))
            {
                string[] imageBase64Splitted = base64ImageString.Split(',');
                fileExtension = imageBase64Splitted[0].Split(':')[1].Split(';')[0].Split('/')[1];
                string imageBase64 = imageBase64Splitted[1];
                imageBytes = Convert.FromBase64String(imageBase64);
            }

            return new ImageProperties { FileExtension = fileExtension, ImageBytes = imageBytes };
        }
    }


    public class ImageProperties
    {
        public string FileExtension { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}

