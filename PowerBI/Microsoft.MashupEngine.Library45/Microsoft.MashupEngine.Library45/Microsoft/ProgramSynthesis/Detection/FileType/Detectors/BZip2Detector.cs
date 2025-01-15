using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC3 RID: 2755
	internal class BZip2Detector : BinaryFormatDetector
	{
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x000D8715 File Offset: 0x000D6915
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.BZip2 });
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x000D8727 File Offset: 0x000D6927
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "bz2" });
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x000D873C File Offset: 0x000D693C
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			if (header.Length < 3)
			{
				return FileType.Unknown;
			}
			if (header[0] != 66 || header[1] != 90 || header[2] != 104 || !char.IsDigit((char)header[3]))
			{
				return FileType.Unknown;
			}
			if (header.Length < 10)
			{
				return FileType.BZip2;
			}
			if (header[4] == 49 && header[5] == 65 && header[6] == 89 && header[7] == 38 && header[8] == 83 && header[9] == 89)
			{
				return FileType.BZip2;
			}
			return FileType.Unknown;
		}
	}
}
