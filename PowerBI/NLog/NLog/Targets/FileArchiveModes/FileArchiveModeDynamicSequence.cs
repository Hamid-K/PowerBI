using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x0200007C RID: 124
	internal sealed class FileArchiveModeDynamicSequence : FileArchiveModeBase
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x0001804B File Offset: 0x0001624B
		public FileArchiveModeDynamicSequence(ArchiveNumberingMode archiveNumbering, string archiveDateFormat, bool customArchiveFileName)
		{
			this._archiveNumbering = archiveNumbering;
			this._archiveDateFormat = archiveDateFormat;
			this._customArchiveFileName = customArchiveFileName;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00018068 File Offset: 0x00016268
		private static bool RemoveNonLetters(string fileName, int startPosition, StringBuilder sb, out int digitsRemoved)
		{
			digitsRemoved = 0;
			sb.ClearBuilder();
			for (int i = 0; i < startPosition; i++)
			{
				sb.Append(fileName[i]);
			}
			bool? flag = null;
			for (int j = startPosition; j < fileName.Length; j++)
			{
				char c = fileName[j];
				if (char.IsDigit(c))
				{
					if (flag == null)
					{
						flag = new bool?(true);
						digitsRemoved = 1;
						sb.Append('{');
						sb.Append('#');
						sb.Append('}');
					}
					else if (!flag.Value)
					{
						sb.Append(c);
					}
					else
					{
						digitsRemoved++;
					}
				}
				else if (!char.IsLetter(c))
				{
					if (flag == null || !flag.Value)
					{
						sb.Append(c);
					}
				}
				else
				{
					if (flag != null)
					{
						flag = new bool?(false);
					}
					sb.Append(c);
				}
			}
			return flag != null;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00018158 File Offset: 0x00016358
		protected override FileArchiveModeBase.FileNameTemplate GenerateFileNameTemplate(string archiveFilePath)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(archiveFilePath);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = int.MaxValue;
			int num3 = 0;
			int num4;
			while (num3 < fileNameWithoutExtension.Length && (FileArchiveModeDynamicSequence.RemoveNonLetters(fileNameWithoutExtension, num3, stringBuilder, out num4) || num3 != 0))
			{
				if (num4 > 1 && stringBuilder.Length < num2)
				{
					num = num3;
					num2 = stringBuilder.Length;
				}
				num3++;
			}
			FileArchiveModeDynamicSequence.RemoveNonLetters(fileNameWithoutExtension, num, stringBuilder, out num4);
			if (num4 <= 1)
			{
				stringBuilder.ClearBuilder();
				stringBuilder.Append(fileNameWithoutExtension);
			}
			ArchiveNumberingMode archiveNumbering = this._archiveNumbering;
			if ((archiveNumbering <= ArchiveNumberingMode.Rolling || archiveNumbering == ArchiveNumberingMode.DateAndSequence) && stringBuilder.Length > 3 && stringBuilder[stringBuilder.Length - 3] != '{' && stringBuilder[stringBuilder.Length - 2] != '#' && stringBuilder[stringBuilder.Length - 1] != '}')
			{
				if (num4 <= 1)
				{
					stringBuilder.Append("{");
					stringBuilder.Append("#");
					stringBuilder.Append("}");
				}
				else
				{
					stringBuilder.Append('*');
				}
			}
			stringBuilder.Append(Path.GetExtension(archiveFilePath));
			return base.GenerateFileNameTemplate(stringBuilder.ToString());
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00018274 File Offset: 0x00016474
		protected override DateAndSequenceArchive GenerateArchiveFileInfo(FileInfo archiveFile, FileArchiveModeBase.FileNameTemplate fileTemplate)
		{
			if (fileTemplate != null && fileTemplate.EndAt > 0)
			{
				string name = archiveFile.Name;
				int i = 0;
				int j = 0;
				while (j < name.Length)
				{
					char c = name[j];
					if (i >= fileTemplate.Template.Length)
					{
						if (char.IsLetter(c))
						{
							return null;
						}
						break;
					}
					else
					{
						char c2;
						if (i >= fileTemplate.EndAt || j < fileTemplate.BeginAt)
						{
							c2 = fileTemplate.Template[i];
							i++;
							goto IL_00AB;
						}
						if (char.IsLetter(c))
						{
							i = fileTemplate.EndAt;
							while (i < fileTemplate.Template.Length)
							{
								c2 = fileTemplate.Template[i];
								i++;
								if (char.IsLetter(c2))
								{
									goto IL_00AB;
								}
							}
							return null;
						}
						IL_00D2:
						j++;
						continue;
						IL_00AB:
						if (c == c2 || char.ToLowerInvariant(c) == char.ToLowerInvariant(c2))
						{
							goto IL_00D2;
						}
						if (c2 != '*' || char.IsLetter(c))
						{
							return null;
						}
						break;
					}
				}
			}
			int num = FileArchiveModeDynamicSequence.ExtractArchiveNumberFromFileName(archiveFile.FullName);
			InternalLogger.Trace<int, string>("FileTarget: extracted sequenceNumber: {0} from file '{1}'", num, archiveFile.FullName);
			DateTime value = FileCharacteristicsHelper.ValidateFileCreationTime<FileInfo>(archiveFile, (FileInfo f) => new DateTime?(f.GetCreationTimeUtc()), (FileInfo f) => new DateTime?(f.GetLastWriteTimeUtc()), null).Value;
			return new DateAndSequenceArchive(archiveFile.FullName, value, string.Empty, (num > 0) ? num : 0);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000183EC File Offset: 0x000165EC
		private static int ExtractArchiveNumberFromFileName(string archiveFileName)
		{
			archiveFileName = Path.GetFileName(archiveFileName);
			int num = archiveFileName.LastIndexOf('.');
			if (num == -1)
			{
				return 0;
			}
			int num2 = archiveFileName.LastIndexOf('.', num - 1);
			int num3;
			if (!int.TryParse((num2 == -1) ? archiveFileName.Substring(num + 1) : archiveFileName.Substring(num2 + 1, num - num2 - 1), out num3))
			{
				return 0;
			}
			return num3;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00018448 File Offset: 0x00016648
		public override DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles)
		{
			int num = this.GetStartSequenceNo() - 1;
			string fileName = Path.GetFileName(archiveFilePath);
			foreach (DateAndSequenceArchive dateAndSequenceArchive in existingArchiveFiles)
			{
				string text = Path.GetFileName(dateAndSequenceArchive.FileName);
				if (string.Equals(text, fileName, StringComparison.OrdinalIgnoreCase))
				{
					num = Math.Max(num, dateAndSequenceArchive.Sequence + this.GetStartSequenceNo());
				}
				else
				{
					string extension = Path.GetExtension(text);
					text = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(text)) + extension;
					if (string.Equals(text, fileName, StringComparison.OrdinalIgnoreCase))
					{
						num = Math.Max(num, dateAndSequenceArchive.Sequence + 1);
					}
				}
			}
			archiveFilePath = Path.GetFullPath(archiveFilePath);
			if (num >= this.GetStartSequenceNo())
			{
				archiveFilePath = Path.Combine(Path.GetDirectoryName(archiveFilePath), Path.GetFileNameWithoutExtension(fileName) + "." + num.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(fileName));
			}
			return new DateAndSequenceArchive(archiveFilePath, archiveDate, this._archiveDateFormat, num);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00018554 File Offset: 0x00016754
		private int GetStartSequenceNo()
		{
			if (!this._customArchiveFileName)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x04000220 RID: 544
		private readonly ArchiveNumberingMode _archiveNumbering;

		// Token: 0x04000221 RID: 545
		private readonly string _archiveDateFormat;

		// Token: 0x04000222 RID: 546
		private readonly bool _customArchiveFileName;
	}
}
