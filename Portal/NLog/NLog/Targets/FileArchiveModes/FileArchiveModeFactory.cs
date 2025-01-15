using System;
using NLog.Common;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x0200007E RID: 126
	internal static class FileArchiveModeFactory
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x000185F8 File Offset: 0x000167F8
		public static IFileArchiveMode CreateArchiveStyle(string archiveFilePath, ArchiveNumberingMode archiveNumbering, string dateFormat, bool customArchiveFileName, int maxArchiveFiles)
		{
			if (FileArchiveModeFactory.ContainsFileNamePattern(archiveFilePath))
			{
				IFileArchiveMode fileArchiveMode = FileArchiveModeFactory.CreateStrictFileArchiveMode(archiveNumbering, dateFormat, maxArchiveFiles);
				if (fileArchiveMode != null)
				{
					return fileArchiveMode;
				}
			}
			if (archiveNumbering != ArchiveNumberingMode.Sequence)
			{
				if (!customArchiveFileName)
				{
					IFileArchiveMode fileArchiveMode2 = FileArchiveModeFactory.CreateStrictFileArchiveMode(archiveNumbering, dateFormat, maxArchiveFiles);
					if (fileArchiveMode2 != null)
					{
						return new FileArchiveModeDynamicTemplate(fileArchiveMode2);
					}
				}
				else
				{
					InternalLogger.Info<string>("FileTarget: Pattern {{#}} is missing in ArchiveFileName `{0}` (Fallback to dynamic wildcard)", archiveFilePath);
				}
			}
			return new FileArchiveModeDynamicSequence(archiveNumbering, dateFormat, customArchiveFileName);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00018649 File Offset: 0x00016849
		private static IFileArchiveMode CreateStrictFileArchiveMode(ArchiveNumberingMode archiveNumbering, string dateFormat, int maxArchiveFiles)
		{
			switch (archiveNumbering)
			{
			case ArchiveNumberingMode.Sequence:
				return new FileArchiveModeSequence(dateFormat);
			case ArchiveNumberingMode.Rolling:
				return new FileArchiveModeRolling();
			case ArchiveNumberingMode.Date:
				return new FileArchiveModeDate(dateFormat, FileArchiveModeFactory.ShouldDeleteOldArchives(maxArchiveFiles));
			case ArchiveNumberingMode.DateAndSequence:
				return new FileArchiveModeDateAndSequence(dateFormat);
			default:
				return null;
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00018688 File Offset: 0x00016888
		public static bool ContainsFileNamePattern(string fileName)
		{
			int num = fileName.IndexOf("{#", StringComparison.Ordinal);
			int num2 = fileName.IndexOf("#}", StringComparison.Ordinal);
			return num != -1 && num2 != -1 && num < num2;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000186BD File Offset: 0x000168BD
		public static bool ShouldDeleteOldArchives(int maxArchiveFiles)
		{
			return maxArchiveFiles > 0;
		}
	}
}
