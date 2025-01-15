using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC6 RID: 2758
	internal class GZipDetector : BinaryFormatDetector
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x000D887B File Offset: 0x000D6A7B
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.GZip });
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x000D888D File Offset: 0x000D6A8D
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "gz" });
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x000D88A2 File Offset: 0x000D6AA2
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			if (header.Length < 3)
			{
				return FileType.Unknown;
			}
			if (header[0] == 31 && header[1] == 139 && header[2] == 8)
			{
				return FileType.GZip;
			}
			return FileType.Unknown;
		}
	}
}
