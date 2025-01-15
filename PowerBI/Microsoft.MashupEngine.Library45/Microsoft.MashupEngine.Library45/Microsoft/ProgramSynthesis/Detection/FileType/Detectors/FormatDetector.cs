using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC5 RID: 2757
	internal abstract class FormatDetector
	{
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600452E RID: 17710
		internal abstract IEnumerable<FileType> SupportedFileTypes { get; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600452F RID: 17711
		internal abstract IEnumerable<string> SupportedExtensions { get; }

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004530 RID: 17712
		internal abstract int Precedence { get; }

		// Token: 0x06004531 RID: 17713
		internal abstract FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer);

		// Token: 0x06004532 RID: 17714
		internal abstract FileType MatchFormat(FileTypeIdentifier caller, string header, string footer);
	}
}
