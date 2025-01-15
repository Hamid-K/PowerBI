using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines
{
	// Token: 0x02000DA9 RID: 3497
	[NullableContext(1)]
	public interface IPdfLoader
	{
		// Token: 0x06005917 RID: 22807
		ILoadedPdf LoadPdf([Nullable(2)] PdfAnalyzerOptions options, FileInfo path, [Nullable(2)] string password = null);

		// Token: 0x06005918 RID: 22808
		ILoadedPdf LoadPdf([Nullable(2)] PdfAnalyzerOptions options, Stream stream, [Nullable(2)] string password = null);
	}
}
