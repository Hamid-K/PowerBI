using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AD1 RID: 2769
	internal class ZLibDetector : BinaryFormatDetector
	{
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004571 RID: 17777 RVA: 0x000D92C6 File Offset: 0x000D74C6
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.ZLibDeflate });
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x000D92D8 File Offset: 0x000D74D8
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "Z", "z" });
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000D9110 File Offset: 0x000D7310
		internal override int Precedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x000D92F8 File Offset: 0x000D74F8
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			if (header.Length < 2)
			{
				return FileType.Unknown;
			}
			int num = (int)(header[0] & 15);
			int num2 = (header[0] & 240) >> 4;
			int num3 = (int)header[0] * 256 + (int)header[1];
			if (num == 8 && num2 <= 7 && num3 % 31 == 0)
			{
				return FileType.ZLibDeflate;
			}
			return FileType.Unknown;
		}
	}
}
