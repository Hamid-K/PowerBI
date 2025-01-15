using System;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000ACF RID: 2767
	internal abstract class TextualFormatDetector : FormatDetector
	{
		// Token: 0x0600456A RID: 17770 RVA: 0x000D8706 File Offset: 0x000D6906
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			return FileType.Unknown;
		}
	}
}
