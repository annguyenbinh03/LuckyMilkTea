using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services
{
	public class CloudinaryService
	{
		private readonly Cloudinary _cloudinary;

		public CloudinaryService(IConfiguration configuration)
		{
			var account = new Account(
				configuration["Cloudinary:CloudName"],
				configuration["Cloudinary:ApiKey"],
				configuration["Cloudinary:ApiSecret"]
			);

			_cloudinary = new Cloudinary(account);
		}

		public async Task<string?> UploadImageAsync(Stream fileStream, string fileName)
		{
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(fileName, fileStream),
				PublicId = $"uploads/{Guid.NewGuid()}",
				Overwrite = true,
				Folder = "MilkTeaShop"
			};

			var uploadResult = await _cloudinary.UploadAsync(uploadParams);
			return uploadResult?.SecureUrl?.ToString();
		}
	}
}
