using System;
using System.Collections.Generic;
using System.IO;
using NLog.Common;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x0200007F RID: 127
	internal sealed class FileArchiveModeRolling : IFileArchiveMode
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x000186C3 File Offset: 0x000168C3
		public bool AttemptCleanupOnInitializeFile(string archiveFilePath, int maxArchiveFiles)
		{
			return false;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000186C6 File Offset: 0x000168C6
		public string GenerateFileNameMask(string archiveFilePath)
		{
			return new FileArchiveModeBase.FileNameTemplate(Path.GetFileName(archiveFilePath)).ReplacePattern("*");
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000186E0 File Offset: 0x000168E0
		public List<DateAndSequenceArchive> GetExistingArchiveFiles(string archiveFilePath)
		{
			List<DateAndSequenceArchive> list = new List<DateAndSequenceArchive>();
			for (int i = 0; i < 2147483647; i++)
			{
				FileInfo fileInfo = new FileInfo(FileArchiveModeRolling.ReplaceNumberPattern(archiveFilePath, i));
				if (!fileInfo.Exists)
				{
					break;
				}
				list.Add(new DateAndSequenceArchive(fileInfo.FullName, DateTime.MinValue, string.Empty, i));
			}
			return list;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00018738 File Offset: 0x00016938
		private static string ReplaceNumberPattern(string pattern, int value)
		{
			int num = pattern.IndexOf("{#", StringComparison.Ordinal);
			int num2 = pattern.IndexOf("#}", StringComparison.Ordinal) + 2;
			int num3 = num2 - num - 2;
			return pattern.Substring(0, num) + Convert.ToString(value, 10).PadLeft(num3, '0') + pattern.Substring(num2);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001878C File Offset: 0x0001698C
		public DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles)
		{
			if (existingArchiveFiles.Count > 0)
			{
				int num = existingArchiveFiles[existingArchiveFiles.Count - 1].Sequence + 1;
				string text = FileArchiveModeRolling.ReplaceNumberPattern(archiveFilePath, num);
				existingArchiveFiles.Add(new DateAndSequenceArchive(text, DateTime.MinValue, string.Empty, int.MaxValue));
			}
			return new DateAndSequenceArchive(Path.GetFullPath(FileArchiveModeRolling.ReplaceNumberPattern(archiveFilePath, 0)), DateTime.MinValue, string.Empty, int.MinValue);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000187FB File Offset: 0x000169FB
		public IEnumerable<DateAndSequenceArchive> CheckArchiveCleanup(string archiveFilePath, List<DateAndSequenceArchive> existingArchiveFiles, int maxArchiveFiles)
		{
			if (existingArchiveFiles.Count <= 1)
			{
				yield break;
			}
			existingArchiveFiles.Sort((DateAndSequenceArchive x, DateAndSequenceArchive y) => x.Sequence.CompareTo(y.Sequence));
			if (maxArchiveFiles > 0 && existingArchiveFiles.Count > maxArchiveFiles)
			{
				int num;
				for (int i = 0; i < existingArchiveFiles.Count; i = num + 1)
				{
					if (existingArchiveFiles[i].Sequence != -2147483648 && existingArchiveFiles[i].Sequence != 2147483647 && i + 1 > maxArchiveFiles)
					{
						yield return existingArchiveFiles[i];
					}
					num = i;
				}
			}
			if (existingArchiveFiles.Count > 1 && existingArchiveFiles[0].Sequence == -2147483648)
			{
				string text = string.Empty;
				int num2 = existingArchiveFiles.Count - 1;
				if (maxArchiveFiles > 0 && num2 > maxArchiveFiles)
				{
					num2 = maxArchiveFiles;
				}
				for (int j = num2; j >= 1; j--)
				{
					string fileName = existingArchiveFiles[j].FileName;
					if (!string.IsNullOrEmpty(text))
					{
						InternalLogger.Info<string, string>("Roll archive {0} to {1}", fileName, text);
						File.Move(fileName, text);
					}
					text = fileName;
				}
			}
			yield break;
		}
	}
}
