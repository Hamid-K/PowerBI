using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.IObservable;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BB9 RID: 3001
	[NullableContext(1)]
	[Nullable(0)]
	internal class PdfAnalyzerV1 : PdfAnalyzer
	{
		// Token: 0x06004C4B RID: 19531 RVA: 0x000EFF24 File Offset: 0x000EE124
		internal PdfAnalyzerV1(ILoadedPdf pdf, [Nullable(2)] PdfAnalyzerOptions options = null)
			: base(options ?? new PdfAnalyzerOptions(new PdfAnalyzerVersion?(PdfAnalyzerVersion.V1), null, null, null, false, true, true, true, null, null))
		{
			this.LoadedPdf = pdf;
			if (!PdfAnalyzerV1.SupportedVersions.Contains(base.AnalyzerOptions.Version))
			{
				throw new Exception("Unsupported PdfAnalyzer version: " + base.AnalyzerOptions.Version.ToString());
			}
			if (!PdfAnalyzerV1.SupportedModes.Contains(base.AnalyzerOptions.Mode))
			{
				throw new Exception("Unsupported InferredTablesMode: " + base.AnalyzerOptions.Mode.ToString());
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06004C4C RID: 19532 RVA: 0x000EFFF8 File Offset: 0x000EE1F8
		public override int PageCount
		{
			get
			{
				return this.LoadedPdf.PageCount;
			}
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x000F0005 File Offset: 0x000EE205
		public override void RenderPage(int pageNumber, FileInfo fileInfo)
		{
			this.LoadedPdf.RenderPage(pageNumber, fileInfo);
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x000F0014 File Offset: 0x000EE214
		internal WholePdf AsWholePdf
		{
			get
			{
				return new WholePdf(this.LoadedPdf);
			}
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x000F0024 File Offset: 0x000EE224
		[return: Nullable(new byte[] { 1, 2 })]
		public override async Task<IPdfTable> GetTableAsync(string identifier, CancellationToken cancel = default(CancellationToken))
		{
			IPdfTable pdfTable;
			if (identifier.StartsWith("Page"))
			{
				int num;
				if (!int.TryParse(identifier.Substring("Page".Length), out num) || num < 1 || num > this.PageCount)
				{
					pdfTable = null;
				}
				else
				{
					int num2 = num - 1;
					IProsePdfTable<ICell> prosePdfTable = (await this.BuildGraphForPage(num2)).BuildFullTable();
					this.LabelPageTable(prosePdfTable);
					pdfTable = prosePdfTable;
				}
			}
			else if (identifier.StartsWith("Table"))
			{
				int num3;
				if (base.AnalyzerOptions.Mode == InferredTablesMode.None)
				{
					pdfTable = null;
				}
				else if (!int.TryParse(identifier.Substring("Table".Length), out num3) || num3 < 1)
				{
					pdfTable = null;
				}
				else
				{
					int num4 = num3 - 1;
					pdfTable = (await this.AnalyzeAllPagesObservable(TableKind.Inferred).NthAsync(num4)).OrElseDefault<IPdfTable>();
				}
			}
			else
			{
				pdfTable = null;
			}
			return pdfTable;
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x000F0070 File Offset: 0x000EE270
		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override async Task<IReadOnlyList<IPdfTable>> GetTablesByBoundsAsync(int pageIndex, [Nullable(new byte[] { 1, 0, 1 })] IEnumerable<Bounds<PixelUnit>> tableBounds)
		{
			DependencyGraph dependencyGraph = await this.BuildGraphForPage(pageIndex);
			DependencyGraph graph = dependencyGraph;
			IProsePdfTable<ICell> fullTable = graph.BuildFullTable();
			return tableBounds.Select(delegate(Bounds<PixelUnit> bounds)
			{
				Bounds<TableUnit>? bounds2 = fullTable.FindTableBounds(bounds);
				if (bounds2 == null)
				{
					return null;
				}
				IProsePdfTable<ICell> prosePdfTable = fullTable.CollapsedSection(bounds2.Value, graph.BuildSeparatorCollection(), graph.GetAnalyzerOptions(), true, null);
				PdfTable<ICell> pdfTable = prosePdfTable as PdfTable<ICell>;
				if (pdfTable != null)
				{
					PdfTable<ICell> pdfTable2 = pdfTable;
					PdfAnalyzerOptions analyzerOptions = graph.GetAnalyzerOptions();
					string text = "Section of ";
					TableIdentity tableIdentity = fullTable.TableIdentity;
					string text2 = ((tableIdentity != null) ? tableIdentity.Identifier : null);
					string text3 = "; bounds=";
					Bounds<PixelUnit> bounds3 = bounds;
					pdfTable2.TableIdentity = new TableIdentity(analyzerOptions, text + text2 + text3 + bounds3.ToString());
				}
				return prosePdfTable;
			}).ToList<IProsePdfTable<ICell>>();
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x000F00C3 File Offset: 0x000EE2C3
		protected override IObservable<IPdfTable> AnalyzeAllPagesObservable(TableKind includedTableKinds)
		{
			return this.AnalyzePagesObservable(includedTableKinds, this.GraphsInOptionsRange());
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x000F00D2 File Offset: 0x000EE2D2
		public override Task<IReadOnlyList<IPdfTable>> AnalyzePageAsync(int pageIndex)
		{
			return this.AnalyzePageAsync(pageIndex, TableKind.All);
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x000F00DC File Offset: 0x000EE2DC
		public async Task<IReadOnlyList<IPdfTable>> AnalyzePageAsync(int pageIndex, TableKind tableKind)
		{
			return await this.AnalyzePageObservable(pageIndex, tableKind).ToList<IPdfTable>().ConfigureAwait(false);
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x000F0130 File Offset: 0x000EE330
		private IObservable<IPdfTable> AnalyzePageObservable(int pageIndex, TableKind tableKind)
		{
			IObservable<DependencyGraph> observable;
			if (base.AnalyzerOptions.MergeMultiPageTables)
			{
				int num = Math.Max(pageIndex - 1, 0);
				int num2 = Math.Min(base.AnalyzerOptions.PageRangeEndIndex ?? (this.PageCount - 1), this.PageCount - 1);
				int num3 = Math.Max(0, num2 - num) + 1;
				observable = this.GraphsInRange(Enumerable.Range(num, num3));
			}
			else
			{
				observable = this.BuildGraphForPage(pageIndex).ToObservable<DependencyGraph>();
			}
			return from table in this.AnalyzePagesObservable(tableKind, observable).TakeWhile((IPdfTable table) => table.Kind == TableKind.Page || table.StartingPageIndex <= pageIndex)
				where table.TableRangeIncludes(pageIndex)
				select table;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x000F01F5 File Offset: 0x000EE3F5
		private IObservable<IPdfTable> AnalyzePagesObservable(TableKind includedTableKinds, IObservable<DependencyGraph> graphsObservable)
		{
			return Observable.Create<IPdfTable>(delegate([Nullable(new byte[] { 0, 1 })] IObserver<IPdfTable> observer)
			{
				List<IObservable<IPdfTable>> list = new List<IObservable<IPdfTable>>();
				IConnectableObservable<DependencyGraph> connectableObservable = graphsObservable.Publish<DependencyGraph>();
				if ((includedTableKinds & TableKind.Page) == TableKind.Page)
				{
					list.Add(this.BuildAndLabelPageTables(connectableObservable));
				}
				if ((includedTableKinds & TableKind.Inferred) == TableKind.Inferred)
				{
					IObservable<IPdfTable> observable;
					if (this.AnalyzerOptions.MergeMultiPageTables)
					{
						observable = MultiPageTableBuilder.MergeCrossPageTables(connectableObservable);
					}
					else
					{
						observable = connectableObservable.SelectMany((DependencyGraph graph) => graph.BuildSimpleTables());
					}
					list.Add(this.LabelInferredTables(observable));
				}
				IObservable<IPdfTable> observable2 = list.Merge<IPdfTable>();
				CompositeDisposable compositeDisposable = new CompositeDisposable();
				compositeDisposable.Add(observable2.Subscribe(observer));
				compositeDisposable.Add(connectableObservable.Connect());
				return compositeDisposable;
			});
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x000F0224 File Offset: 0x000EE424
		private IObservable<DependencyGraph> GraphsInOptionsRange()
		{
			int pageRangeStartIndex = base.AnalyzerOptions.PageRangeStartIndex;
			int num = Math.Min((base.AnalyzerOptions.PageRangeEndIndex + 1) ?? this.PageCount, this.PageCount);
			return this.GraphsInRange(Enumerable.Range(pageRangeStartIndex, Math.Max(0, num - pageRangeStartIndex)));
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x000F02A6 File Offset: 0x000EE4A6
		private IObservable<DependencyGraph> GraphsInRange(IEnumerable<int> range)
		{
			return range.Select(delegate(int pageIndex)
			{
				PdfAnalyzerV1.<>c__DisplayClass20_0 CS$<>8__locals1 = new PdfAnalyzerV1.<>c__DisplayClass20_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.pageIndex = pageIndex;
				return delegate
				{
					PdfAnalyzerV1.<>c__DisplayClass20_0.<<GraphsInRange>b__1>d <<GraphsInRange>b__1>d;
					<<GraphsInRange>b__1>d.<>t__builder = AsyncTaskMethodBuilder<DependencyGraph>.Create();
					<<GraphsInRange>b__1>d.<>4__this = CS$<>8__locals1;
					<<GraphsInRange>b__1>d.<>1__state = -1;
					<<GraphsInRange>b__1>d.<>t__builder.Start<PdfAnalyzerV1.<>c__DisplayClass20_0.<<GraphsInRange>b__1>d>(ref <<GraphsInRange>b__1>d);
					return <<GraphsInRange>b__1>d.<>t__builder.Task;
				};
			}).ToObservable<DependencyGraph>();
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x000F02C0 File Offset: 0x000EE4C0
		internal async Task<DependencyGraph> BuildGraphForPage(int pageIndex)
		{
			DependencyGraph dependencyGraph = await this.LoadedPdf.ProcessPage(pageIndex);
			Bounds<PixelUnit>? clippingBounds2 = base.AnalyzerOptions.ClippingBounds;
			DependencyGraph dependencyGraph2;
			if (clippingBounds2 != null)
			{
				Bounds<PixelUnit> clippingBounds = clippingBounds2.GetValueOrDefault();
				Func<Glyph, bool> <>9__4;
				dependencyGraph2 = new DependencyGraph(new PageData(pageIndex, (from gl in dependencyGraph.GetGlyphs().Select(delegate(IReadOnlyList<Glyph> gl)
					{
						Func<Glyph, bool> func;
						if ((func = <>9__4) == null)
						{
							func = (<>9__4 = (Glyph g) => clippingBounds.Overlaps(g.ApparentPixelBounds));
						}
						return gl.Where(func).ToList<Glyph>();
					})
					where gl.Count > 0
					select gl).ToList<List<Glyph>>(), dependencyGraph.GetPageBounds(), (from path in dependencyGraph.GetPaths()
					where clippingBounds.Overlaps(path.PixelBounds)
					select path).ToList<GraphicalPath>(), (from image in dependencyGraph.GetImages()
					where clippingBounds.Overlaps(image.PixelBounds)
					select image).ToList<Image>()), dependencyGraph.GetAnalyzerOptions());
			}
			else
			{
				dependencyGraph2 = dependencyGraph;
			}
			return dependencyGraph2;
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x000F030B File Offset: 0x000EE50B
		private void LabelPageTable(IProsePdfTable<ICell> pageTable)
		{
			((PdfTable<ICell>)pageTable).TableIdentity = new TableIdentity(base.AnalyzerOptions, string.Format("{0}{1:D3}", "Page", pageTable.StartingPageIndex + 1));
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x000F033F File Offset: 0x000EE53F
		private IObservable<IPdfTable> BuildAndLabelPageTables(IObservable<DependencyGraph> graphs)
		{
			return graphs.Select(delegate(DependencyGraph graph)
			{
				IProsePdfTable<ICell> prosePdfTable = graph.BuildFullTable();
				this.LabelPageTable(prosePdfTable);
				return prosePdfTable;
			});
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x000F0353 File Offset: 0x000EE553
		private IObservable<IPdfTable> LabelInferredTables(IObservable<IPdfTable> tableObservable)
		{
			return tableObservable.Select(delegate(IPdfTable table, int tableIndex)
			{
				TableIdentity tableIdentity = new TableIdentity(base.AnalyzerOptions, string.Format("{0}{1:D3}", "Table", tableIndex + 1));
				PdfTable<ICell> pdfTable = table as PdfTable<ICell>;
				if (pdfTable == null)
				{
					MultiPageTable multiPageTable = table as MultiPageTable;
					if (multiPageTable != null)
					{
						multiPageTable.TableIdentity = tableIdentity;
					}
				}
				else
				{
					pdfTable.TableIdentity = tableIdentity;
				}
				return table;
			});
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x000F0367 File Offset: 0x000EE567
		public override void Dispose()
		{
			this.LoadedPdf.Dispose();
		}

		// Token: 0x0400222D RID: 8749
		public const PdfAnalyzerVersion AnalyzerVersion = PdfAnalyzerVersion.V1;

		// Token: 0x0400222E RID: 8750
		private static readonly PdfAnalyzerVersion[] SupportedVersions = new PdfAnalyzerVersion[]
		{
			PdfAnalyzerVersion.V1,
			PdfAnalyzerVersion.V1_1,
			PdfAnalyzerVersion.V1_2,
			PdfAnalyzerVersion.V1_3,
			PdfAnalyzerVersion.V1_4
		};

		// Token: 0x0400222F RID: 8751
		private const string PageTablePrefix = "Page";

		// Token: 0x04002230 RID: 8752
		private const string InferredTablePrefix = "Table";

		// Token: 0x04002231 RID: 8753
		private static readonly InferredTablesMode[] SupportedModes = new InferredTablesMode[]
		{
			InferredTablesMode.IdentifyTables,
			InferredTablesMode.AllCandidates,
			InferredTablesMode.None
		};

		// Token: 0x04002232 RID: 8754
		internal readonly ILoadedPdf LoadedPdf;
	}
}
