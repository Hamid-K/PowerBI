using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics
{
	// Token: 0x02000C0E RID: 3086
	[NullableContext(1)]
	[Nullable(0)]
	public class WholePdf : PdfRegion
	{
		// Token: 0x06004FCB RID: 20427 RVA: 0x000FB3BC File Offset: 0x000F95BC
		internal WholePdf(ILoadedPdf pdf)
			: base(pdf)
		{
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x000FB3C8 File Offset: 0x000F95C8
		public async Task<WholePdfPage> GetPageAsync(int pageIndex)
		{
			ILoadedPdf pdf = base.Pdf;
			DependencyGraph dependencyGraph = await base.Pdf.ProcessPage(pageIndex);
			return new WholePdfPage(pdf, dependencyGraph);
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x000FB413 File Offset: 0x000F9613
		public override string ToString()
		{
			return "WholePdf";
		}
	}
}
