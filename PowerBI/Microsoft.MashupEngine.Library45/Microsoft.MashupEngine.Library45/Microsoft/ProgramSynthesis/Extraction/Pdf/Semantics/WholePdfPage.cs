using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics
{
	// Token: 0x02000C11 RID: 3089
	[NullableContext(1)]
	[Nullable(0)]
	public class WholePdfPage : SinglePagePdfRegion
	{
		// Token: 0x06004FD7 RID: 20439 RVA: 0x000FB5A5 File Offset: 0x000F97A5
		internal WholePdfPage(ILoadedPdf pdf, DependencyGraph pageData)
			: base(pdf, pageData)
		{
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x000FB5AF File Offset: 0x000F97AF
		public override string ToString()
		{
			return string.Format("WholePdfPage(PageIndex={0})", base.PageIndex);
		}
	}
}
