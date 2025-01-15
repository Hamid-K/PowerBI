using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000ACA RID: 2762
	internal class OrcDetector : BinaryFormatDetector
	{
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x000D8F87 File Offset: 0x000D7187
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.Orc });
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x000D8F98 File Offset: 0x000D7198
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "orc" });
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000D8FB0 File Offset: 0x000D71B0
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			if (footer != null && footer.Length >= 4)
			{
				int num = footer.Length;
				if (footer[num - 4] == 79 && footer[num - 3] == 82 && footer[num - 2] == 67)
				{
					return FileType.Orc;
				}
			}
			if (header != null && header.Length >= 3 && header[0] == 79 && header[1] == 82 && header[2] == 67)
			{
				return FileType.Orc;
			}
			return FileType.Unknown;
		}
	}
}
