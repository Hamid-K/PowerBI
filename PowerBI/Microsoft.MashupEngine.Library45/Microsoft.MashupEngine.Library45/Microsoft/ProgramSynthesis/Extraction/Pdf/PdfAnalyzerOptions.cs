using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BCE RID: 3022
	[NullableContext(1)]
	[Nullable(0)]
	public class PdfAnalyzerOptions
	{
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x000F4FB1 File Offset: 0x000F31B1
		public static PdfAnalyzerOptions Default { get; } = new PdfAnalyzerOptions(new PdfAnalyzerVersion?(PdfAnalyzerVersion.V1_4), null, null, null, false, true, true, true, null, null);

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x000F4FB8 File Offset: 0x000F31B8
		public PdfAnalyzerVersion Version { get; }

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x000F4FC0 File Offset: 0x000F31C0
		public int PageRangeStartIndex { get; }

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x000F4FC8 File Offset: 0x000F31C8
		public int? PageRangeEndIndex { get; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06004CA8 RID: 19624 RVA: 0x000F4FD0 File Offset: 0x000F31D0
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit>? ClippingBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06004CA9 RID: 19625 RVA: 0x000F4FD8 File Offset: 0x000F31D8
		public InferredTablesMode Mode { get; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06004CAA RID: 19626 RVA: 0x000F4FE0 File Offset: 0x000F31E0
		public bool ForceSeparatorGrids { get; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06004CAB RID: 19627 RVA: 0x000F4FE8 File Offset: 0x000F31E8
		public bool MergeMultiPageTables { get; }

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x000F4FF0 File Offset: 0x000F31F0
		public bool CombineCompatibleColumns { get; }

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x000F4FF8 File Offset: 0x000F31F8
		public bool CombineCompatibleRows { get; }

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x000F5000 File Offset: 0x000F3200
		public string ImageString { get; }

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004CAF RID: 19631 RVA: 0x000F5008 File Offset: 0x000F3208
		internal AxisAligned<bool> CombineCompatibleAlongAxis
		{
			get
			{
				return new AxisAligned<bool>(this.CombineCompatibleRows, this.CombineCompatibleColumns);
			}
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x000F501C File Offset: 0x000F321C
		[NullableContext(2)]
		public PdfAnalyzerOptions(PdfAnalyzerVersion? version = null, int? pageRangeStartIndex = null, int? pageRangeEndIndex = null, InferredTablesMode? mode = null, bool forceSeparatorGrids = false, bool mergeMultiPageTables = true, bool combineCompatibleColumns = true, bool combineCompatibleRows = true, string imageString = null, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit>? clippingBounds = null)
		{
			this.Version = version ?? PdfAnalyzerVersion.V1_4;
			this.PageRangeStartIndex = pageRangeStartIndex.GetValueOrDefault();
			this.PageRangeEndIndex = pageRangeEndIndex;
			this.Mode = mode ?? ((this.Version == PdfAnalyzerVersion.V1 || this.Version == PdfAnalyzerVersion.V1_1 || this.Version == PdfAnalyzerVersion.V1_2 || this.Version == PdfAnalyzerVersion.V1_3 || this.Version == PdfAnalyzerVersion.V1_4) ? InferredTablesMode.AllCandidates : InferredTablesMode.IdentifyTables);
			this.ForceSeparatorGrids = forceSeparatorGrids;
			this.MergeMultiPageTables = mergeMultiPageTables;
			this.CombineCompatibleColumns = combineCompatibleColumns;
			this.CombineCompatibleRows = combineCompatibleRows;
			this.ImageString = imageString ?? "[image]";
			this.ClippingBounds = clippingBounds;
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x000F50E3 File Offset: 0x000F32E3
		public static bool IsVersionSupported(PdfAnalyzerVersion version)
		{
			return version - PdfAnalyzerVersion.V1 <= 4;
		}

		// Token: 0x04002279 RID: 8825
		public const PdfAnalyzerVersion CurrentVersion = PdfAnalyzerVersion.V1_4;

		// Token: 0x04002285 RID: 8837
		public const string DefaultImageString = "[image]";
	}
}
