using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.IObservable;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BB6 RID: 2998
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PdfAnalyzer : IDisposable
	{
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x000EFCAE File Offset: 0x000EDEAE
		public PdfAnalyzerOptions AnalyzerOptions { get; }

		// Token: 0x06004C35 RID: 19509 RVA: 0x000EFCB6 File Offset: 0x000EDEB6
		protected PdfAnalyzer(PdfAnalyzerOptions options)
		{
			this.AnalyzerOptions = options;
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06004C36 RID: 19510
		public abstract int PageCount { get; }

		// Token: 0x06004C37 RID: 19511 RVA: 0x000EFCC8 File Offset: 0x000EDEC8
		private static PdfAnalyzer BuildPdfAnalyzer(ILoadedPdf loadedPdf, [Nullable(2)] PdfAnalyzerOptions options)
		{
			PdfAnalyzerVersion pdfAnalyzerVersion = ((options != null) ? options.Version : PdfAnalyzerVersion.V1_4);
			if (pdfAnalyzerVersion - PdfAnalyzerVersion.V1 <= 4)
			{
				return new PdfAnalyzerV1(loadedPdf, options);
			}
			throw new ArgumentException("Invalid version: " + ((options != null) ? new PdfAnalyzerVersion?(options.Version) : null).ToString());
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x000EFD26 File Offset: 0x000EDF26
		public static PdfAnalyzer LoadPdf(FileInfo fileInfo, [Nullable(2)] string password = null, [Nullable(2)] PdfAnalyzerOptions options = null)
		{
			return PdfAnalyzer.LoadPdf(OfficePdfLoader.Instance, fileInfo, password, options);
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x000EFD35 File Offset: 0x000EDF35
		public static PdfAnalyzer LoadPdf(Stream pdfStream, [Nullable(2)] string password = null, [Nullable(2)] PdfAnalyzerOptions options = null)
		{
			return PdfAnalyzer.LoadPdf(OfficePdfLoader.Instance, pdfStream, password, options);
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x000EFD44 File Offset: 0x000EDF44
		public static PdfAnalyzer LoadPdf(IPdfLoader pdfLoader, FileInfo fileInfo, [Nullable(2)] string password = null, [Nullable(2)] PdfAnalyzerOptions options = null)
		{
			if (!fileInfo.Exists)
			{
				throw new ArgumentException("File " + fileInfo.FullName + " does not exist");
			}
			return PdfAnalyzer.BuildPdfAnalyzer(pdfLoader.LoadPdf(options, fileInfo, password), options);
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x000EFD78 File Offset: 0x000EDF78
		public static PdfAnalyzer LoadPdf(IPdfLoader pdfLoader, Stream pdfStream, [Nullable(2)] string password = null, [Nullable(2)] PdfAnalyzerOptions options = null)
		{
			return PdfAnalyzer.BuildPdfAnalyzer(((IPdfLoader)OfficePdfLoader.Instance).LoadPdf(options, pdfStream, password), options);
		}

		// Token: 0x06004C3C RID: 19516
		public abstract void RenderPage(int pageNumber, FileInfo fileInfo);

		// Token: 0x06004C3D RID: 19517
		public abstract Task<IReadOnlyList<IPdfTable>> AnalyzePageAsync(int pageNumber);

		// Token: 0x06004C3E RID: 19518 RVA: 0x000EFD8D File Offset: 0x000EDF8D
		public IObservable<IPdfTable> AnalyzeAllPagesObservable()
		{
			return this.AnalyzeAllPagesObservable(TableKind.All);
		}

		// Token: 0x06004C3F RID: 19519
		protected abstract IObservable<IPdfTable> AnalyzeAllPagesObservable(TableKind includedTableKinds);

		// Token: 0x06004C40 RID: 19520 RVA: 0x000EFD96 File Offset: 0x000EDF96
		public IEnumerable<IPdfTable> AnalyzeAllPages()
		{
			return this.AnalyzeAllPages(TableKind.All);
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x000EFD9F File Offset: 0x000EDF9F
		protected IEnumerable<IPdfTable> AnalyzeAllPages(TableKind includedTableKinds)
		{
			return this.AnalyzeAllPagesObservable(includedTableKinds).ToList<IPdfTable>().Result;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x000EFDB4 File Offset: 0x000EDFB4
		public async Task<IPdfTable> GetTableAsync(int index, CancellationToken cancel = default(CancellationToken))
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException("Negative indexes are not allowed.");
			}
			return (await this.AnalyzeAllPagesObservable().NthAsync(index)).OrElseCompute(delegate
			{
				throw new IndexOutOfRangeException("Index is greater than or equal to the number of tables in the document.");
			});
		}

		// Token: 0x06004C43 RID: 19523
		[return: Nullable(new byte[] { 1, 2 })]
		public abstract Task<IPdfTable> GetTableAsync(string identifier, CancellationToken cancel = default(CancellationToken));

		// Token: 0x06004C44 RID: 19524
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public abstract Task<IReadOnlyList<IPdfTable>> GetTablesByBoundsAsync(int pageIndex, [Nullable(new byte[] { 1, 0, 1 })] IEnumerable<Bounds<PixelUnit>> tableBounds);

		// Token: 0x06004C45 RID: 19525
		public abstract void Dispose();
	}
}
