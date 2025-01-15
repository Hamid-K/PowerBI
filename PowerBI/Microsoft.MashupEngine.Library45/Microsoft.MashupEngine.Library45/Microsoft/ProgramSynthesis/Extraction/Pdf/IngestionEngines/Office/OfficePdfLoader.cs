using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DB1 RID: 3505
	[NullableContext(1)]
	[Nullable(0)]
	internal class OfficePdfLoader : IPdfLoader
	{
		// Token: 0x06005945 RID: 22853 RVA: 0x00002130 File Offset: 0x00000330
		private OfficePdfLoader()
		{
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x0011BEAB File Offset: 0x0011A0AB
		public static OfficePdfLoader Instance { get; } = new OfficePdfLoader();

		// Token: 0x06005947 RID: 22855 RVA: 0x0011BEB2 File Offset: 0x0011A0B2
		private static LoadResult AsLoadResult(PdfLoadResult loadResult)
		{
			switch (loadResult)
			{
			case PdfLoadResult.Success:
				return LoadResult.Success;
			case PdfLoadResult.Corrupted:
				return LoadResult.Corrupted;
			case PdfLoadResult.PasswordRequired:
				return LoadResult.PasswordRequired;
			default:
				return LoadResult.Unknown;
			}
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x0011BED0 File Offset: 0x0011A0D0
		public ILoadedPdf LoadPdf([Nullable(2)] PdfAnalyzerOptions options, FileInfo file, [Nullable(2)] string password = null)
		{
			PdfParser pdfParser;
			PdfLoadResult pdfLoadResult = PdfParser.Load(file, password, out pdfParser);
			if (pdfLoadResult != PdfLoadResult.Success || pdfParser == null)
			{
				throw new PdfLoadException(OfficePdfLoader.AsLoadResult(pdfLoadResult));
			}
			return new OfficeLoadedPdf(pdfParser, options);
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x0011BF00 File Offset: 0x0011A100
		public ILoadedPdf LoadPdf([Nullable(2)] PdfAnalyzerOptions options, Stream stream, [Nullable(2)] string password = null)
		{
			PdfParser pdfParser;
			PdfLoadResult pdfLoadResult = PdfParser.Load(stream, password, out pdfParser);
			if (pdfLoadResult != PdfLoadResult.Success || pdfParser == null)
			{
				throw new PdfLoadException(OfficePdfLoader.AsLoadResult(pdfLoadResult));
			}
			return new OfficeLoadedPdf(pdfParser, options);
		}
	}
}
