using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Common
{
    public static class Utils
    {
        public static UserResponse ConvertUserToUserResponse(User user)
        {
            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Lastname = user.Lastname,
                Email = user.Email,
                Status = user.Status,
                PhotoURL = user.PhotoURL
            };
            return userResponse;
        }

        public static User ConvertUserResponseToUser(UserResponse userResponse)
        {
            User user = new User
            {
                Id = userResponse.Id,
                FirstName = userResponse.FirstName,
                Lastname = userResponse.Lastname,
                Email = userResponse.Email,
                Password = null,
                Status = userResponse.Status,
                PhotoURL = userResponse.PhotoURL
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

