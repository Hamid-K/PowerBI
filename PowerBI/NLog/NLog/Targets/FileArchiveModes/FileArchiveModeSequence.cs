using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x02000080 RID: 128
	internal sealed class FileArchiveModeSequence : FileArchiveModeBase
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0001881A File Offset: 0x00016A1A
		public FileArchiveModeSequence(string archiveDateFormat)
		{
			this._archiveDateFormat = archiveDateFormat;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00018829 File Offset: 0x00016A29
		public override bool AttemptCleanupOnInitializeFile(string archiveFilePath, int maxArchiveFiles)
		{
			return false;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001882C File Offset: 0x00016A2C
		protected override DateAndSequenceArchive GenerateArchiveFileInfo(FileInfo archiveFile, FileArchiveModeBase.FileNameTemplate fileTemplate)
		{
			string text = Path.GetFileName(archiveFile.FullName) ?? "";
			int num = fileTemplate.Template.Length - fileTemplate.EndAt;
			string text2 = text.Substring(fileTemplate.BeginAt, text.Length - num - fileTemplate.BeginAt);
			int num2;
			try
			{
				num2 = Convert.ToInt32(text2, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				return null;
			}
			return new DateAndSequenceArchive(archiveFile.FullName, DateTime.MinValue, string.Empty, num2);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000188BC File Offset: 0x00016ABC
		public override DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles)
		{
			int num = 0;
			FileArchiveModeBase.FileNameTemplate fileNameTemplate = this.GenerateFileNameTemplate(archiveFilePath);
			foreach (DateAndSequenceArchive dateAndSequenceArchive in existingArchiveFiles)
			{
				num = Math.Max(num, dateAndSequenceArchive.Sequence + 1);
			}
			int num2 = fileNameTemplate.EndAt - fileNameTemplate.BeginAt - 2;
			string text = num.ToString().PadLeft(num2, '0');
			archiveFilePath = Path.Combine(Path.GetDirectoryName(archiveFilePath), fileNameTemplate.ReplacePattern("*").Replace("*", text));
			return new DateAndSequenceArchive(archiveFilePath, archiveDate, this._archiveDateFormat, num);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00018970 File Offset: 0x00016B70
		public override IEnumerable<DateAndSequenceArchive> CheckArchiveCleanup(string archiveFilePath, List<DateAndSequenceArchive> existingArchiveFiles, int maxArchiveFiles)
		{
			if (maxArchiveFiles <= 0 || existingArchiveFiles.Count == 0 || existingArchiveFiles.Count < maxArchiveFiles)
			{
				yield break;
			}
			int sequence = existingArchiveFiles[existingArchiveFiles.Count - 1].Sequence;
			int minNumberToKeep = sequence - maxArchiveFiles + 1;
			if (minNumberToKeep <= 0)
			{
				yield break;
			}
			foreach (DateAndSequenceArchive dateAndSequenceArchive in existingArchiveFiles)
			{
				if (dateAndSequenceArchive.Sequence < minNumberToKeep)
				{
					yield return dateAndSequenceArchive;
				}
			}
			List<DateAndSequenceArchive>.Enumerator enumerator = default(List<DateAndSequenceArchive>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000224 RID: 548
		private readonly string _archiveDateFormat;
	}
}
