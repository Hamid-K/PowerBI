using System;
using System.Collections.Generic;
using System.IO;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x02000079 RID: 121
	internal abstract class FileArchiveModeBase : IFileArchiveMode
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00017B50 File Offset: 0x00015D50
		public virtual bool AttemptCleanupOnInitializeFile(string archiveFilePath, int maxArchiveFiles)
		{
			int lastArchiveFileCount = this._lastArchiveFileCount;
			this._lastArchiveFileCount = lastArchiveFileCount + 1;
			return lastArchiveFileCount > maxArchiveFiles;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00017B74 File Offset: 0x00015D74
		public string GenerateFileNameMask(string archiveFilePath)
		{
			int lastArchiveFileCount = this._lastArchiveFileCount;
			string text = this.GenerateFileNameMask(archiveFilePath, this.GenerateFileNameTemplate(archiveFilePath));
			this._lastArchiveFileCount = lastArchiveFileCount;
			return text;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00017BA0 File Offset: 0x00015DA0
		public virtual List<DateAndSequenceArchive> GetExistingArchiveFiles(string archiveFilePath)
		{
			this._lastArchiveFileCount = 65534;
			string directoryName = Path.GetDirectoryName(archiveFilePath);
			FileArchiveModeBase.FileNameTemplate fileNameTemplate = this.GenerateFileNameTemplate(archiveFilePath);
			string text = this.GenerateFileNameMask(archiveFilePath, fileNameTemplate);
			List<DateAndSequenceArchive> list = new List<DateAndSequenceArchive>();
			if (string.IsNullOrEmpty(text))
			{
				return list;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			if (!directoryInfo.Exists)
			{
				return list;
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles(text))
			{
				DateAndSequenceArchive dateAndSequenceArchive = this.GenerateArchiveFileInfo(fileInfo, fileNameTemplate);
				if (dateAndSequenceArchive != null)
				{
					list.Add(dateAndSequenceArchive);
				}
			}
			if (list.Count > 1)
			{
				list.Sort(new Comparison<DateAndSequenceArchive>(FileArchiveModeBase.FileSortOrderComparison));
			}
			this._lastArchiveFileCount = list.Count;
			return list;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00017C58 File Offset: 0x00015E58
		private static int FileSortOrderComparison(DateAndSequenceArchive x, DateAndSequenceArchive y)
		{
			if (x.Date != y.Date && !x.HasSameFormattedDate(y.Date))
			{
				return x.Date.CompareTo(y.Date);
			}
			if (x.Sequence.CompareTo(y.Sequence) != 0)
			{
				return x.Sequence.CompareTo(y.Sequence);
			}
			return string.CompareOrdinal(x.FileName, y.FileName);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00017CD7 File Offset: 0x00015ED7
		protected virtual FileArchiveModeBase.FileNameTemplate GenerateFileNameTemplate(string archiveFilePath)
		{
			this._lastArchiveFileCount++;
			return new FileArchiveModeBase.FileNameTemplate(Path.GetFileName(archiveFilePath));
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00017CF2 File Offset: 0x00015EF2
		protected virtual string GenerateFileNameMask(string archiveFilePath, FileArchiveModeBase.FileNameTemplate fileTemplate)
		{
			if (fileTemplate != null)
			{
				return fileTemplate.ReplacePattern("*");
			}
			return string.Empty;
		}

		// Token: 0x0600093A RID: 2362
		protected abstract DateAndSequenceArchive GenerateArchiveFileInfo(FileInfo archiveFile, FileArchiveModeBase.FileNameTemplate fileTemplate);

		// Token: 0x0600093B RID: 2363
		public abstract DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles);

		// Token: 0x0600093C RID: 2364 RVA: 0x00017D08 File Offset: 0x00015F08
		public virtual IEnumerable<DateAndSequenceArchive> CheckArchiveCleanup(string archiveFilePath, List<DateAndSequenceArchive> existingArchiveFiles, int maxArchiveFiles)
		{
			if (maxArchiveFiles <= 0)
			{
				yield break;
			}
			this._lastArchiveFileCount = existingArchiveFiles.Count;
			if (existingArchiveFiles.Count == 0 || existingArchiveFiles.Count < maxArchiveFiles)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < existingArchiveFiles.Count - maxArchiveFiles; i = num + 1)
			{
				if (this._lastArchiveFileCount > 0)
				{
					this._lastArchiveFileCount--;
				}
				yield return existingArchiveFiles[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x0400021C RID: 540
		private int _lastArchiveFileCount = 65534;

		// Token: 0x02000249 RID: 585
		internal sealed class FileNameTemplate
		{
			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x06001598 RID: 5528 RVA: 0x00039334 File Offset: 0x00037534
			// (set) Token: 0x06001599 RID: 5529 RVA: 0x0003933C File Offset: 0x0003753C
			public string Template { get; private set; }

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x0600159A RID: 5530 RVA: 0x00039345 File Offset: 0x00037545
			// (set) Token: 0x0600159B RID: 5531 RVA: 0x0003934D File Offset: 0x0003754D
			public int BeginAt { get; private set; }

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x0600159C RID: 5532 RVA: 0x00039356 File Offset: 0x00037556
			// (set) Token: 0x0600159D RID: 5533 RVA: 0x0003935E File Offset: 0x0003755E
			public int EndAt { get; private set; }

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x0600159E RID: 5534 RVA: 0x00039367 File Offset: 0x00037567
			private bool FoundPattern
			{
				get
				{
					return this.BeginAt != -1 && this.EndAt != -1;
				}
			}

			// Token: 0x0600159F RID: 5535 RVA: 0x00039380 File Offset: 0x00037580
			public FileNameTemplate(string template)
			{
				this.Template = template;
				this.BeginAt = template.IndexOf("{#", StringComparison.Ordinal);
				if (this.BeginAt != -1)
				{
					this.EndAt = template.IndexOf("#}", StringComparison.Ordinal) + "#}".Length;
				}
			}

			// Token: 0x060015A0 RID: 5536 RVA: 0x000393D4 File Offset: 0x000375D4
			public string ReplacePattern(string replacementValue)
			{
				if (this.FoundPattern && !string.IsNullOrEmpty(replacementValue))
				{
					return this.Template.Substring(0, this.BeginAt) + replacementValue + this.Template.Substring(this.EndAt);
				}
				return this.Template;
			}

			// Token: 0x04000641 RID: 1601
			public const string PatternStartCharacters = "{#";

			// Token: 0x04000642 RID: 1602
			public const string PatternEndCharacters = "#}";
		}
	}
}
