using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Utils
{
	public static class TimeZoneUtil
	{
		private static readonly string _timeZoneId;

		static TimeZoneUtil()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			_timeZoneId = configuration["TimeZoneSettings:TimeZoneId"] ?? "UTC";
		}

		public static DateTime GetCurrentTime()
		{
			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(_timeZoneId);
			return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
		}
	}
}
