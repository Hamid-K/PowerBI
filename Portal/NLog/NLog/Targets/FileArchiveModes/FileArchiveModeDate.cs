using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x0200007A RID: 122
	internal sealed class FileArchiveModeDate : FileArchiveModeBase
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x00017D39 File Offset: 0x00015F39
		public FileArchiveModeDate(string archiveDateFormat, bool archiveCleanupEnabled)
		{
			this._archiveDateFormat = archiveDateFormat;
			this._archiveCleanupEnabled = archiveCleanupEnabled;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00017D4F File Offset: 0x00015F4F
		public override List<DateAndSequenceArchive> GetExistingArchiveFiles(string archiveFilePath)
		{
			if (this._archiveCleanupEnabled)
			{
				return base.GetExistingArchiveFiles(archiveFilePath);
			}
			return new List<DateAndSequenceArchive>();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00017D68 File Offset: 0x00015F68
		protected override DateAndSequenceArchive GenerateArchiveFileInfo(FileInfo archiveFile, FileArchiveModeBase.FileNameTemplate fileTemplate)
		{
			string text = Path.GetFileName(archiveFile.FullName) ?? "";
			int num = fileTemplate.ReplacePattern("*").LastIndexOf('*');
			DateTime dateTime;
			if (num + this._archiveDateFormat.Length <= text.Length && DateTime.TryParseExact(text.Substring(num, this._archiveDateFormat.Length), this._archiveDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				return new DateAndSequenceArchive(archiveFile.FullName, dateTime, this._archiveDateFormat, -1);
			}
			return null;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public override DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles)
		{
			FileArchiveModeBase.FileNameTemplate fileNameTemplate = this.GenerateFileNameTemplate(archiveFilePath);
			archiveFilePath = Path.Combine(Path.GetDirectoryName(archiveFilePath), fileNameTemplate.ReplacePattern("*").Replace("*", archiveDate.ToString(this._archiveDateFormat)));
			return new DateAndSequenceArchive(archiveFilePath, archiveDate, this._archiveDateFormat, 0);
		}

		// Token: 0x0400021D RID: 541
		private readonly string _archiveDateFormat;

		// Token: 0x0400021E RID: 542
		private readonly bool _archiveCleanupEnabled;
	}
}
