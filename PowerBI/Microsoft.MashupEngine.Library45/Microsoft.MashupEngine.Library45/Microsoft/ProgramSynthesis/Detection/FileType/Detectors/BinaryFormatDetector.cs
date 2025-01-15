using System;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC2 RID: 2754
	internal abstract class BinaryFormatDetector : FormatDetector
	{
		// Token: 0x06004522 RID: 17698 RVA: 0x000D8706 File Offset: 0x000D6906
		internal override FileType MatchFormat(FileTypeIdentifier caller, string header, string footer)
		{
			return FileType.Unknown;
		}
	}
}
