using System;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BC6 RID: 3014
	public class PdfLoadException : Exception
	{
		// Token: 0x06004C7D RID: 19581 RVA: 0x000F0D6A File Offset: 0x000EEF6A
		public PdfLoadException(LoadResult loadResult)
			: base(string.Format("Pdf failed to load with result: {0}", loadResult))
		{
			this.LoadResult = loadResult;
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x000F0D89 File Offset: 0x000EEF89
		public LoadResult LoadResult { get; }
	}
}
