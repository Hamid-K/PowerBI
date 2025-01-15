using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000ACC RID: 2764
	internal class PlainTextFormatDetector : TextualFormatDetector
	{
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000D90AD File Offset: 0x000D72AD
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return new FileType[1];
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x000D90B5 File Offset: 0x000D72B5
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return new string[] { "txt" };
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000D90C5 File Offset: 0x000D72C5
		internal override int Precedence
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override FileType MatchFormat(FileTypeIdentifier caller, string header, string footer)
		{
			return FileType.PlainText;
		}
	}
}
