using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DAD RID: 3501
	[NullableContext(1)]
	[Nullable(0)]
	internal class OfficeLoadedPdf : ILoadedPdf, IDisposable
	{
		// Token: 0x0600592E RID: 22830 RVA: 0x0011B927 File Offset: 0x00119B27
		private static double DpiToFeatureScale(double dpi)
		{
			return dpi / 72.0;
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0011B934 File Offset: 0x00119B34
		public OfficeLoadedPdf(PdfParser parser, [Nullable(2)] PdfAnalyzerOptions options)
		{
			this._parser = parser;
			this._options = options;
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06005930 RID: 22832 RVA: 0x0011B94A File Offset: 0x00119B4A
		public int PageCount
		{
			get
			{
				return (int)this._parser.NumPages;
			}
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x0011B958 File Offset: 0x00119B58
		async Task<DependencyGraph> ILoadedPdf.ProcessPage(int i)
		{
			TaskAwaiter<PageData> taskAwaiter = this.ProcessPage(i, 300.0).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<PageData> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<PageData>);
			}
			return new DependencyGraph(taskAwaiter.GetResult(), this._options);
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0011B9A4 File Offset: 0x00119BA4
		public Task<PageData> ProcessPage(int i, double dpi)
		{
			double featureScale = OfficeLoadedPdf.DpiToFeatureScale(dpi);
			Task<PageData> task;
			using (PdfParser.Page page = this._parser.LoadPage((ulong)((long)i), (float)dpi))
			{
				IReadOnlyList<IReadOnlyList<Glyph>> readOnlyList = this.SplitTextRuns(page.TextRuns ?? Enumerable.Empty<PdfParser.TextRun>(), featureScale).ToList<IReadOnlyList<Glyph>>();
				IReadOnlyList<Image> readOnlyList2 = page.Images.Select((PdfParser.Image image) => new Image(image.BoundingBox.AsPixelBounds(featureScale), image.RenderingOrder, image.Transformation.AsTransformationMatrix(featureScale))).ToList<Image>();
				Bounds<PixelUnit> bounds = new Bounds<PixelUnit>(0, (int)((double)page.Width * featureScale), 0, (int)((double)page.Height * featureScale));
				IReadOnlyList<GraphicalPath> readOnlyList3 = page.PdfPaths.Select((PdfParser.PdfPath path) => path.AsPath(featureScale)).ToList<GraphicalPath>();
				task = Task.FromResult<PageData>(new PageData(i, readOnlyList, bounds, readOnlyList3, readOnlyList2));
			}
			return task;
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x0011BA88 File Offset: 0x00119C88
		public void RenderPage(int i, FileInfo file, float dpi)
		{
			using (PdfParser.Page page = this._parser.LoadPage((ulong)((long)i), dpi))
			{
				page.RenderPNG(file, dpi);
			}
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x0011BAC8 File Offset: 0x00119CC8
		void ILoadedPdf.RenderPage(int i, FileInfo file)
		{
			this.RenderPage(i, file, 300f);
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x0011BAD7 File Offset: 0x00119CD7
		public void Dispose()
		{
			this._parser.Dispose();
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0011BAE4 File Offset: 0x00119CE4
		private IEnumerable<IReadOnlyList<Glyph>> SplitTextRuns(IEnumerable<PdfParser.TextRun> textRuns, double featureScale)
		{
			foreach (PdfParser.TextRun textRun in textRuns)
			{
				FontCharacteristics fontCharacteristics = new FontCharacteristics(textRun.IsFontBold, textRun.IsFontItalic, textRun.FontSize, textRun.FontBaseName, textRun.FontColor);
				double num = textRun.BoundingPoints.TopLeft.AsPixelDoubleVector(featureScale).DistanceTo(textRun.BoundingPoints.BottomLeft.AsPixelDoubleVector(featureScale));
				TransformationMatrix transformationMatrix = textRun.Transformation.AsTransformationMatrix(featureScale);
				Glyph[] array;
				if (textRun.Glyphs == null)
				{
					array = new Glyph[0];
				}
				else
				{
					array = new Glyph[textRun.Glyphs.Length];
					for (int i = 0; i < array.Length; i++)
					{
						PdfParser.TextRun.Glyph glyph = textRun.Glyphs[i];
						Bounds<PixelUnit> bounds = glyph.Location.AsPixelBounds(featureScale);
						array[i] = new Glyph(bounds, glyph.Text, fontCharacteristics, transformationMatrix, textRun.RenderingOrder, new double?(num), glyph.BidiCategory);
					}
				}
				yield return array;
			}
			IEnumerator<PdfParser.TextRun> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04002973 RID: 10611
		internal const double Dpi = 300.0;

		// Token: 0x04002974 RID: 10612
		private readonly PdfParser _parser;

		// Token: 0x04002975 RID: 10613
		[Nullable(2)]
		private readonly PdfAnalyzerOptions _options;
	}
}
