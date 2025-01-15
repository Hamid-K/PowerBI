using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x02000119 RID: 281
	internal abstract class FileCharacteristicsHelper
	{
		// Token: 0x06000EB3 RID: 3763 RVA: 0x000246B8 File Offset: 0x000228B8
		public static FileCharacteristicsHelper CreateHelper(bool forcedManaged)
		{
			if (!forcedManaged && PlatformDetector.IsWin32 && !PlatformDetector.IsMono)
			{
				return new Win32FileCharacteristicsHelper();
			}
			return new PortableFileCharacteristicsHelper();
		}

		// Token: 0x06000EB4 RID: 3764
		public abstract FileCharacteristics GetFileCharacteristics(string fileName, FileStream fileStream);

		// Token: 0x06000EB5 RID: 3765 RVA: 0x000246D8 File Offset: 0x000228D8
		public static DateTime? ValidateFileCreationTime<T>(T fileInfo, Func<T, DateTime?> primary, Func<T, DateTime?> fallback, Func<T, DateTime?> finalFallback = null)
		{
			DateTime? dateTime = primary(fileInfo);
			if (dateTime != null && dateTime.Value.Year < 1980 && !PlatformDetector.IsWin32)
			{
				dateTime = fallback(fileInfo);
				if (finalFallback != null && (dateTime == null || dateTime.Value.Year < 1980))
				{
					dateTime = finalFallback(fileInfo);
				}
			}
			return dateTime;
		}
	}
}
