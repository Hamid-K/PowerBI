using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NLog.Common;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x0200007B RID: 123
	internal sealed class FileArchiveModeDateAndSequence : FileArchiveModeBase
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x00017E42 File Offset: 0x00016042
		public FileArchiveModeDateAndSequence(string archiveDateFormat)
		{
			this._archiveDateFormat = archiveDateFormat;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00017E51 File Offset: 0x00016051
		public override bool AttemptCleanupOnInitializeFile(string archiveFilePath, int maxArchiveFiles)
		{
			return false;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00017E54 File Offset: 0x00016054
		protected override DateAndSequenceArchive GenerateArchiveFileInfo(FileInfo archiveFile, FileArchiveModeBase.FileNameTemplate fileTemplate)
		{
			DateTime dateTime;
			int num;
			if (!FileArchiveModeDateAndSequence.TryParseDateAndSequence(Path.GetFileName(archiveFile.FullName) ?? string.Empty, this._archiveDateFormat, fileTemplate, out dateTime, out num))
			{
				return null;
			}
			return new DateAndSequenceArchive(archiveFile.FullName, dateTime, this._archiveDateFormat, num);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00017E9C File Offset: 0x0001609C
		public override DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles)
		{
			int num = 0;
			FileArchiveModeBase.FileNameTemplate fileNameTemplate = this.GenerateFileNameTemplate(archiveFilePath);
			foreach (DateAndSequenceArchive dateAndSequenceArchive in existingArchiveFiles)
			{
				if (dateAndSequenceArchive.HasSameFormattedDate(archiveDate))
				{
					num = Math.Max(num, dateAndSequenceArchive.Sequence + 1);
				}
			}
			int num2 = fileNameTemplate.EndAt - fileNameTemplate.BeginAt - 2;
			string text = num.ToString().PadLeft(num2, '0');
			string text2 = fileNameTemplate.ReplacePattern("*").Replace("*", archiveDate.ToString(this._archiveDateFormat) + "." + text);
			archiveFilePath = Path.Combine(Path.GetDirectoryName(archiveFilePath), text2);
			return new DateAndSequenceArchive(archiveFilePath, archiveDate, this._archiveDateFormat, num);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00017F78 File Offset: 0x00016178
		private static bool TryParseDateAndSequence(string archiveFileNameWithoutPath, string dateFormat, FileArchiveModeBase.FileNameTemplate fileTemplate, out DateTime date, out int sequence)
		{
			int num = fileTemplate.Template.Length - fileTemplate.EndAt;
			int beginAt = fileTemplate.BeginAt;
			int num2 = archiveFileNameWithoutPath.Length - num - beginAt;
			if (num2 < 0)
			{
				date = default(DateTime);
				sequence = 0;
				return false;
			}
			string text = archiveFileNameWithoutPath.Substring(beginAt, num2);
			int num3 = text.LastIndexOf('.') + 1;
			string text2 = text.Substring(num3);
			if (!int.TryParse(text2, NumberStyles.None, CultureInfo.CurrentCulture, out sequence))
			{
				date = default(DateTime);
				return false;
			}
			int num4 = text.Length - text2.Length - 1;
			if (num4 < 0)
			{
				date = default(DateTime);
				return false;
			}
			string text3 = text.Substring(0, num4);
			if (!DateTime.TryParseExact(text3, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
			{
				return false;
			}
			InternalLogger.Trace<string, string>("FileTarget: parsed date '{0}' from file-template '{1}'", text3, (fileTemplate != null) ? fileTemplate.Template : null);
			return true;
		}

		// Token: 0x0400021F RID: 543
		private readonly string _archiveDateFormat;
	}
}
