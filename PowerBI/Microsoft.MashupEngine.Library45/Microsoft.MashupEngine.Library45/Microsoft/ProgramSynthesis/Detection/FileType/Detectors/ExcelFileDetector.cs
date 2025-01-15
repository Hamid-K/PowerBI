using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC4 RID: 2756
	internal class ExcelFileDetector : BinaryFormatDetector
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x000D87B7 File Offset: 0x000D69B7
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return this._supportedFileTypes;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x000D87BF File Offset: 0x000D69BF
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return this._supportedExtensions;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x000D87C8 File Offset: 0x000D69C8
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			FileType fileType;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(header))
				{
					using (IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(memoryStream, null))
					{
						fileType = ((excelDataReader.RowCount >= 0) ? FileType.Excel : FileType.Unknown);
					}
				}
			}
			catch (Exception)
			{
				fileType = FileType.Unknown;
			}
			return fileType;
		}

		// Token: 0x04001F9C RID: 8092
		private readonly IReadOnlyList<FileType> _supportedFileTypes = new List<FileType> { FileType.Excel };

		// Token: 0x04001F9D RID: 8093
		private readonly IReadOnlyList<string> _supportedExtensions = new List<string> { "xls", "xlsx" };
	}
}
