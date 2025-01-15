using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines
{
	// Token: 0x02000DAA RID: 3498
	[NullableContext(1)]
	public interface ILoadedPdf : IDisposable
	{
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06005919 RID: 22809
		int PageCount { get; }

		// Token: 0x0600591A RID: 22810
		Task<DependencyGraph> ProcessPage(int i);

		// Token: 0x0600591B RID: 22811
		void RenderPage(int i, FileInfo file);
	}
}
