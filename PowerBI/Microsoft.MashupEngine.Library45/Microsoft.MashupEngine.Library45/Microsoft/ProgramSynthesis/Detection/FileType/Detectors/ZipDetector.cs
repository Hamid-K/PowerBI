using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AD0 RID: 2768
	internal class ZipDetector : BinaryFormatDetector
	{
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000D925A File Offset: 0x000D745A
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[]
				{
					FileType.ZipDeflateCompression,
					FileType.ZipOtherCompression
				});
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x000D9271 File Offset: 0x000D7471
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "zip" });
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x000D9110 File Offset: 0x000D7310
		internal override int Precedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x000D9286 File Offset: 0x000D7486
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			if (header.Length < 10)
			{
				return FileType.Unknown;
			}
			if (header[0] != 80 || header[1] != 75 || header[2] != 3 || header[3] != 4)
			{
				return FileType.Unknown;
			}
			if (header[8] == 8 && header[9] == 0)
			{
				return FileType.ZipDeflateCompression;
			}
			return FileType.ZipOtherCompression;
		}
	}
}
