using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000ACB RID: 2763
	internal class ParquetDetector : BinaryFormatDetector
	{
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004554 RID: 17748 RVA: 0x000D900C File Offset: 0x000D720C
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.Parquet });
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x000D901D File Offset: 0x000D721D
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "parquet" });
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x000D9034 File Offset: 0x000D7234
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			bool flag = header != null && header.Length >= 4 && header[0] == 80 && header[1] == 65 && header[2] == 82 && header[3] == 49;
			bool flag2 = footer != null && footer.Length >= 4 && footer[footer.Length - 4] == 80 && footer[footer.Length - 3] == 65 && footer[footer.Length - 2] == 82 && footer[footer.Length - 1] == 49;
			if (!flag && !flag2)
			{
				return FileType.Unknown;
			}
			return FileType.Parquet;
		}
	}
}
