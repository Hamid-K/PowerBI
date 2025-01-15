using System;
using System.Globalization;
using System.IO;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000384 RID: 900
	public static class EventFilesNaming
	{
		// Token: 0x06001BDE RID: 7134 RVA: 0x0006A644 File Offset: 0x00068844
		public static string FormatTargetFileName(DateTime creationTime, DateTime modifiedTime, string elementId, int counter)
		{
			return "{0}_{1}_{2}_{3}_{4}.etl".FormatWithInvariantCulture(new object[]
			{
				creationTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
				elementId,
				creationTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture),
				modifiedTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture),
				counter
			});
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0006A6AA File Offset: 0x000688AA
		public static string GetCompressedEventFileName(string eventFileName)
		{
			return Path.ChangeExtension(eventFileName, "cmp");
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0006A6B7 File Offset: 0x000688B7
		public static bool IsCompressed(string eventFileName)
		{
			return eventFileName.EndsWith("cmp", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0006A6C5 File Offset: 0x000688C5
		public static string GetDecompressedEventFileName(string eventFileName)
		{
			ExtendedDiagnostics.EnsureOperation(EventFilesNaming.IsCompressed(eventFileName), "File is not compressed");
			return Path.ChangeExtension(eventFileName, "etl");
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0006A6E2 File Offset: 0x000688E2
		public static string GetFileNamePrefix(DateTime creationTime)
		{
			return creationTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0006A6F8 File Offset: 0x000688F8
		public static EventFileProperties GetEventFileProperties(string fileName)
		{
			fileName = Path.GetFileNameWithoutExtension(fileName);
			string[] array = fileName.Split(new char[] { '_' });
			if (array.Length != 5)
			{
				throw new ArgumentException("File name '" + fileName + "' is not a valid event file name (Day_ElementId_Creation_Modified_Counter)");
			}
			string text = array[1];
			DateTime dateTime = ExtendedDateTime.ParseExactUtc(array[2], "yyyyMMddHHmmss");
			DateTime dateTime2 = ExtendedDateTime.ParseExactUtc(array[3], "yyyyMMddHHmmss");
			int num = int.Parse(array[4], CultureInfo.InvariantCulture);
			return new EventFileProperties(dateTime, dateTime2, new ElementId(text), num);
		}

		// Token: 0x0400095D RID: 2397
		private const string c_resultFileFormat = "{0}_{1}_{2}_{3}_{4}.etl";

		// Token: 0x0400095E RID: 2398
		private const string c_dayFormat = "yyyyMMdd";

		// Token: 0x0400095F RID: 2399
		private const string c_fullDateFormat = "yyyyMMddHHmmss";

		// Token: 0x04000960 RID: 2400
		private const string c_cmpExtension = "cmp";

		// Token: 0x04000961 RID: 2401
		private const string c_etlExtension = "etl";

		// Token: 0x04000962 RID: 2402
		public static TimeSpan FileNamePrefixResolution = TimeSpan.FromDays(1.0);
	}
}
