using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DBD RID: 3517
	[NullableContext(1)]
	[Nullable(0)]
	internal class PdfParser : IDisposable
	{
		// Token: 0x0600595B RID: 22875 RVA: 0x0011C258 File Offset: 0x0011A458
		static PdfParser()
		{
			PdfParser.LoadLibrary(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), (IntPtr.Size == 8) ? "x64" : "x86", "pdf2text.dll"));
			int pdf2TextVersion = PdfParser.GetPdf2TextVersion();
			if (pdf2TextVersion != 20)
			{
				throw new Exception(string.Format("Unexpected version of pdf2text.dll: expected version={0}, found version={1}.", 20, pdf2TextVersion));
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00002130 File Offset: 0x00000330
		private PdfParser()
		{
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600595D RID: 22877 RVA: 0x0011C2CA File Offset: 0x0011A4CA
		public bool IsLoaded
		{
			get
			{
				return this._loaded;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x0600595E RID: 22878 RVA: 0x0011C2D4 File Offset: 0x0011A4D4
		public ulong NumPages
		{
			get
			{
				return this._pdfInfo.NumPages;
			}
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x0011C2E4 File Offset: 0x0011A4E4
		public void Dispose()
		{
			object parserLock = PdfParser.ParserLock;
			lock (parserLock)
			{
				this.ReleaseUnmanagedResources();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x0011C32C File Offset: 0x0011A52C
		public PdfParser.Page LoadPage(ulong pageNum, float dpi)
		{
			if (!this._loaded)
			{
				throw new ObjectDisposedException("Cannot load a page after the PDF has been unloaded.");
			}
			return PdfParser.Page.LoadPage(this, pageNum, dpi);
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x0011C34C File Offset: 0x0011A54C
		[NullableContext(2)]
		public static PdfLoadResult Load([Nullable(1)] FileInfo file, string password, out PdfParser pdf)
		{
			pdf = new PdfParser();
			object parserLock = PdfParser.ParserLock;
			PdfLoadResult pdfLoadResult;
			lock (parserLock)
			{
				pdfLoadResult = PdfParser.LoadPdfFromFile(file.FullName, password, out pdf._pdfInfo);
			}
			if (pdfLoadResult != PdfLoadResult.Success)
			{
				pdf = null;
			}
			else
			{
				pdf._loaded = true;
			}
			return pdfLoadResult;
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x0011C3B4 File Offset: 0x0011A5B4
		public static PdfLoadResult Load(FileInfo file, [Nullable(2)] out PdfParser pdf)
		{
			return PdfParser.Load(file, null, out pdf);
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x0011C3C0 File Offset: 0x0011A5C0
		[NullableContext(2)]
		public static PdfLoadResult Load([Nullable(1)] Stream stream, string password, out PdfParser pdf)
		{
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream must be readable.", "stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream must be seekable.", "stream");
			}
			pdf = new PdfParser();
			object parserLock = PdfParser.ParserLock;
			PdfLoadResult pdfLoadResult;
			lock (parserLock)
			{
				pdfLoadResult = PdfParser.LoadPdfFromStream(new StreamWrapper(stream), password, out pdf._pdfInfo);
			}
			if (pdfLoadResult != PdfLoadResult.Success)
			{
				pdf = null;
			}
			else
			{
				pdf._loaded = true;
			}
			return pdfLoadResult;
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x0011C458 File Offset: 0x0011A658
		public static PdfLoadResult Load(Stream stream, [Nullable(2)] out PdfParser pdf)
		{
			return PdfParser.Load(stream, null, out pdf);
		}

		// Token: 0x06005965 RID: 22885
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x06005966 RID: 22886
		[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern int GetPdf2TextVersion();

		// Token: 0x06005967 RID: 22887
		[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern PdfLoadResult LoadPdfFromFile([MarshalAs(UnmanagedType.LPWStr)] string filename, [Nullable(2)] [MarshalAs(UnmanagedType.LPWStr)] string password, out PdfParser.PdfInfo pdfInfo);

		// Token: 0x06005968 RID: 22888
		[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern PdfLoadResult LoadPdfFromStream(IStream stream, [Nullable(2)] [MarshalAs(UnmanagedType.LPWStr)] string password, out PdfParser.PdfInfo pdfInfo);

		// Token: 0x06005969 RID: 22889
		[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern void UnloadPdf(ref PdfParser.PdfInfo pdfInfo);

		// Token: 0x0600596A RID: 22890 RVA: 0x0011C462 File Offset: 0x0011A662
		private void ReleaseUnmanagedResources()
		{
			this._loaded = false;
			PdfParser.UnloadPdf(ref this._pdfInfo);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0011C478 File Offset: 0x0011A678
		~PdfParser()
		{
			this.ReleaseUnmanagedResources();
		}

		// Token: 0x040029B4 RID: 10676
		private volatile bool _loaded;

		// Token: 0x040029B5 RID: 10677
		private PdfParser.PdfInfo _pdfInfo;

		// Token: 0x040029B6 RID: 10678
		private const int Pdf2TextVersion = 20;

		// Token: 0x040029B7 RID: 10679
		private static readonly object ParserLock = new object();

		// Token: 0x02000DBE RID: 3518
		[NullableContext(0)]
		private struct PdfInfo
		{
			// Token: 0x17001042 RID: 4162
			// (get) Token: 0x0600596C RID: 22892 RVA: 0x0011C4A4 File Offset: 0x0011A6A4
			public ulong NumPages
			{
				get
				{
					return (ulong)this.numPages;
				}
			}

			// Token: 0x040029B8 RID: 10680
			private readonly IntPtr handle;

			// Token: 0x040029B9 RID: 10681
			private readonly UIntPtr numPages;
		}

		// Token: 0x02000DBF RID: 3519
		[Nullable(0)]
		internal class Page : IDisposable
		{
			// Token: 0x0600596D RID: 22893 RVA: 0x0011C4B1 File Offset: 0x0011A6B1
			private Page(PdfParser pdf)
			{
				this._pdf = pdf;
			}

			// Token: 0x17001043 RID: 4163
			// (get) Token: 0x0600596E RID: 22894 RVA: 0x0011C4C0 File Offset: 0x0011A6C0
			public bool IsLoaded
			{
				get
				{
					return this._loaded;
				}
			}

			// Token: 0x17001044 RID: 4164
			// (get) Token: 0x0600596F RID: 22895 RVA: 0x0011C4CA File Offset: 0x0011A6CA
			public float Width
			{
				get
				{
					return this._pageInfo._width;
				}
			}

			// Token: 0x17001045 RID: 4165
			// (get) Token: 0x06005970 RID: 22896 RVA: 0x0011C4D7 File Offset: 0x0011A6D7
			public float Height
			{
				get
				{
					return this._pageInfo._height;
				}
			}

			// Token: 0x17001046 RID: 4166
			// (get) Token: 0x06005971 RID: 22897 RVA: 0x0011C4E4 File Offset: 0x0011A6E4
			public ulong PageNum
			{
				get
				{
					return (ulong)this._pageInfo._pageNum;
				}
			}

			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06005972 RID: 22898 RVA: 0x0011C4F6 File Offset: 0x0011A6F6
			[Nullable(new byte[] { 2, 1 })]
			public IEnumerable<PdfParser.TextRun> TextRuns
			{
				[return: Nullable(new byte[] { 2, 1 })]
				get
				{
					if (!this._loaded)
					{
						throw new ObjectDisposedException("Cannot get text runs of a page after it has been unloaded; call PdfParser.LoadPage() again to load a new instance of this page.");
					}
					return PdfParser.TextRun.GetTextRuns(this._pageInfo);
				}
			}

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06005973 RID: 22899 RVA: 0x0011C518 File Offset: 0x0011A718
			[Nullable(new byte[] { 2, 1 })]
			public IEnumerable<PdfParser.PdfPath> PdfPaths
			{
				[return: Nullable(new byte[] { 2, 1 })]
				get
				{
					if (!this._loaded)
					{
						throw new ObjectDisposedException("Cannot get paths of a page after it has been unloaded; call PdfParser.LoadPage() again to load a new instance of this page.");
					}
					return PdfParser.PdfPath.GetPaths(this._pageInfo);
				}
			}

			// Token: 0x17001049 RID: 4169
			// (get) Token: 0x06005974 RID: 22900 RVA: 0x0011C53A File Offset: 0x0011A73A
			[Nullable(new byte[] { 2, 1 })]
			public IEnumerable<PdfParser.Image> Images
			{
				[return: Nullable(new byte[] { 2, 1 })]
				get
				{
					if (!this._loaded)
					{
						throw new ObjectDisposedException("Cannot get images of a page after it has been unloaded; call PdfParser.LoadPage() again to load a new instance of this page.");
					}
					return this._pageInfo._images.MarshalCClassArray(this._pageInfo._numImages);
				}
			}

			// Token: 0x06005975 RID: 22901 RVA: 0x0011C56C File Offset: 0x0011A76C
			public void Dispose()
			{
				object parserLock = PdfParser.ParserLock;
				lock (parserLock)
				{
					this.ReleaseUnmanagedResources();
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x06005976 RID: 22902 RVA: 0x0011C5B4 File Offset: 0x0011A7B4
			internal static PdfParser.Page LoadPage(PdfParser pdf, ulong pageNum, float dpi)
			{
				if (pageNum >= pdf.NumPages)
				{
					throw new ArgumentException("Page number exceeded number of pages.", "pageNum");
				}
				PdfParser.Page page = new PdfParser.Page(pdf);
				object parserLock = PdfParser.ParserLock;
				lock (parserLock)
				{
					PdfParser.Page.LoadPdfPage(ref pdf._pdfInfo, (UIntPtr)pageNum, dpi, out page._pageInfo);
					if (!pdf.IsLoaded)
					{
						throw new ObjectDisposedException("PDF was unloaded while trying to load page.");
					}
				}
				page._loaded = true;
				return page;
			}

			// Token: 0x06005977 RID: 22903 RVA: 0x0011C644 File Offset: 0x0011A844
			public void RenderPNG(FileInfo file, float dpi)
			{
				if (!this._loaded)
				{
					throw new ObjectDisposedException("Cannot render a page after it has been unloaded; call PdfParser.LoadPage() again to load a new instance of this page.");
				}
				object parserLock = PdfParser.ParserLock;
				lock (parserLock)
				{
					PdfParser.Page.RenderPdfPageToFile(ref this._pageInfo, dpi, file.FullName);
					if (!this.IsLoaded)
					{
						throw new ObjectDisposedException("Page was unloaded while trying to render page.");
					}
					if (!this._pdf.IsLoaded)
					{
						throw new ObjectDisposedException("PDF was unloaded while trying to render page.");
					}
				}
			}

			// Token: 0x06005978 RID: 22904
			[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
			private static extern void LoadPdfPage(ref PdfParser.PdfInfo pdfInfo, UIntPtr pageNum, float dpi, out PdfParser.Page.PdfPageInfo pageInfo);

			// Token: 0x06005979 RID: 22905
			[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
			private static extern void UnloadPdfPage(ref PdfParser.Page.PdfPageInfo pageInfo);

			// Token: 0x0600597A RID: 22906
			[DllImport("pdf2text.dll", CallingConvention = CallingConvention.StdCall)]
			private static extern void RenderPdfPageToFile(ref PdfParser.Page.PdfPageInfo pageInfo, float dpi, [MarshalAs(UnmanagedType.LPWStr)] string filename);

			// Token: 0x0600597B RID: 22907 RVA: 0x0011C6D0 File Offset: 0x0011A8D0
			private void ReleaseUnmanagedResources()
			{
				this._loaded = false;
				PdfParser.Page.UnloadPdfPage(ref this._pageInfo);
			}

			// Token: 0x0600597C RID: 22908 RVA: 0x0011C6E8 File Offset: 0x0011A8E8
			~Page()
			{
				this.ReleaseUnmanagedResources();
			}

			// Token: 0x040029BA RID: 10682
			private readonly PdfParser _pdf;

			// Token: 0x040029BB RID: 10683
			private volatile bool _loaded;

			// Token: 0x040029BC RID: 10684
			private PdfParser.Page.PdfPageInfo _pageInfo;

			// Token: 0x02000DC0 RID: 3520
			[NullableContext(0)]
			internal struct PdfPageInfo
			{
				// Token: 0x040029BD RID: 10685
				private readonly IntPtr _pdfInfo;

				// Token: 0x040029BE RID: 10686
				public readonly UIntPtr _pageNum;

				// Token: 0x040029BF RID: 10687
				private readonly IntPtr _handle;

				// Token: 0x040029C0 RID: 10688
				public readonly UIntPtr _numTextRuns;

				// Token: 0x040029C1 RID: 10689
				public readonly IntPtr _textRuns;

				// Token: 0x040029C2 RID: 10690
				public readonly UIntPtr _numPaths;

				// Token: 0x040029C3 RID: 10691
				public readonly IntPtr _paths;

				// Token: 0x040029C4 RID: 10692
				public readonly UIntPtr _numImages;

				// Token: 0x040029C5 RID: 10693
				public readonly IntPtr _images;

				// Token: 0x040029C6 RID: 10694
				public readonly float _width;

				// Token: 0x040029C7 RID: 10695
				public readonly float _height;
			}
		}

		// Token: 0x02000DC1 RID: 3521
		[NullableContext(0)]
		[StructLayout(LayoutKind.Sequential)]
		internal class Image
		{
			// Token: 0x040029C8 RID: 10696
			public readonly BoundingBox BoundingBox;

			// Token: 0x040029C9 RID: 10697
			public readonly int RenderingOrder;

			// Token: 0x040029CA RID: 10698
			public readonly TransformationMatrix Transformation;

			// Token: 0x040029CB RID: 10699
			public readonly BoundingPoints BoundingPoints;
		}

		// Token: 0x02000DC2 RID: 3522
		[Nullable(0)]
		internal class TextRun
		{
			// Token: 0x0600597E RID: 22910 RVA: 0x0011C714 File Offset: 0x0011A914
			private TextRun(PdfParser.TextRun.PdfTextRunInfo textRunInfo)
			{
				this._textRunInfo = textRunInfo;
				this.Glyphs = this._textRunInfo.glyphs.MarshalCStructArray(this._textRunInfo.numGlyphs);
			}

			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x0600597F RID: 22911 RVA: 0x0011C744 File Offset: 0x0011A944
			[Nullable(2)]
			public PdfParser.TextRun.Glyph[] Glyphs
			{
				[NullableContext(2)]
				get;
			}

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x06005980 RID: 22912 RVA: 0x0011C74C File Offset: 0x0011A94C
			public float BaseLine
			{
				get
				{
					return this._textRunInfo.baseLine;
				}
			}

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x06005981 RID: 22913 RVA: 0x0011C759 File Offset: 0x0011A959
			public string FontFamilyName
			{
				get
				{
					return Marshal.PtrToStringUni(this._textRunInfo._fontFamilyName);
				}
			}

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x06005982 RID: 22914 RVA: 0x0011C76B File Offset: 0x0011A96B
			public string FontBaseName
			{
				get
				{
					return Marshal.PtrToStringUni(this._textRunInfo._fontBaseName);
				}
			}

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x06005983 RID: 22915 RVA: 0x0011C77D File Offset: 0x0011A97D
			public float FontSize
			{
				get
				{
					return this._textRunInfo.fontSize;
				}
			}

			// Token: 0x1700104F RID: 4175
			// (get) Token: 0x06005984 RID: 22916 RVA: 0x0011C78A File Offset: 0x0011A98A
			public Color FontColor
			{
				get
				{
					return Color.FromArgb(this._textRunInfo.fontColor);
				}
			}

			// Token: 0x17001050 RID: 4176
			// (get) Token: 0x06005985 RID: 22917 RVA: 0x0011C79C File Offset: 0x0011A99C
			public bool IsFontBold
			{
				get
				{
					return this._textRunInfo.isFontBold;
				}
			}

			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06005986 RID: 22918 RVA: 0x0011C7A9 File Offset: 0x0011A9A9
			public bool IsFontItalic
			{
				get
				{
					return this._textRunInfo.isFontItalic;
				}
			}

			// Token: 0x17001052 RID: 4178
			// (get) Token: 0x06005987 RID: 22919 RVA: 0x0011C7B6 File Offset: 0x0011A9B6
			public int RenderingOrder
			{
				get
				{
					return this._textRunInfo.renderingOrder;
				}
			}

			// Token: 0x17001053 RID: 4179
			// (get) Token: 0x06005988 RID: 22920 RVA: 0x0011C7C3 File Offset: 0x0011A9C3
			public TransformationMatrix Transformation
			{
				get
				{
					return this._textRunInfo.transformation;
				}
			}

			// Token: 0x17001054 RID: 4180
			// (get) Token: 0x06005989 RID: 22921 RVA: 0x0011C7D0 File Offset: 0x0011A9D0
			public BoundingPoints BoundingPoints
			{
				get
				{
					return this._textRunInfo.boundingPoints;
				}
			}

			// Token: 0x0600598A RID: 22922 RVA: 0x0011C7DD File Offset: 0x0011A9DD
			[return: Nullable(new byte[] { 2, 1 })]
			internal static IReadOnlyList<PdfParser.TextRun> GetTextRuns(PdfParser.Page.PdfPageInfo pageInfo)
			{
				return pageInfo._textRuns.MarshalCArray(pageInfo._numTextRuns, (PdfParser.TextRun.PdfTextRunInfo textRunInfo) => new PdfParser.TextRun(textRunInfo));
			}

			// Token: 0x040029CC RID: 10700
			private readonly PdfParser.TextRun.PdfTextRunInfo _textRunInfo;

			// Token: 0x02000DC3 RID: 3523
			[Nullable(0)]
			public struct Glyph
			{
				// Token: 0x17001055 RID: 4181
				// (get) Token: 0x0600598B RID: 22923 RVA: 0x0011C80F File Offset: 0x0011AA0F
				public string Text
				{
					get
					{
						return Marshal.PtrToStringUni(this._text);
					}
				}

				// Token: 0x17001056 RID: 4182
				// (get) Token: 0x0600598C RID: 22924 RVA: 0x0011C81C File Offset: 0x0011AA1C
				public BidiUnicodeCategory BidiCategory
				{
					get
					{
						switch (this.Bidi)
						{
						case BidiKind.C2_NOTAPPLICABLE:
							return BidiUnicodeCategory.NotApplicable;
						case BidiKind.C2_LEFTTORIGHT:
							return BidiUnicodeCategory.LeftToRight;
						case BidiKind.C2_RIGHTTOLEFT:
							return BidiUnicodeCategory.RightToLeft;
						case BidiKind.C2_EUROPENUMBER:
							return BidiUnicodeCategory.EuropeanNumber;
						case BidiKind.C2_EUROPESEPARATOR:
							return BidiUnicodeCategory.EuropeanNumberSeparator;
						case BidiKind.C2_EUROPETERMINATOR:
							return BidiUnicodeCategory.EuropeanNumberTerminator;
						case BidiKind.C2_ARABICNUMBER:
							return BidiUnicodeCategory.ArabicNumber;
						case BidiKind.C2_COMMONSEPARATOR:
							return BidiUnicodeCategory.CommonNumberSeparator;
						case BidiKind.C2_BLOCKSEPARATOR:
							return BidiUnicodeCategory.ParagraphSeparator;
						case BidiKind.C2_SEGMENTSEPARATOR:
							return BidiUnicodeCategory.SegmentSeparator;
						case BidiKind.C2_WHITESPACE:
							return BidiUnicodeCategory.Whitespace;
						case BidiKind.C2_OTHERNEUTRAL:
							return BidiUnicodeCategory.OtherNeutral;
						default:
							return BidiUnicodeCategory.Unknown;
						}
					}
				}

				// Token: 0x040029CE RID: 10702
				private readonly IntPtr _text;

				// Token: 0x040029CF RID: 10703
				public readonly BoundingBox Location;

				// Token: 0x040029D0 RID: 10704
				public readonly BidiKind Bidi;
			}

			// Token: 0x02000DC4 RID: 3524
			[NullableContext(0)]
			private struct PdfTextRunInfo
			{
				// Token: 0x040029D1 RID: 10705
				public readonly float baseLine;

				// Token: 0x040029D2 RID: 10706
				public readonly UIntPtr numGlyphs;

				// Token: 0x040029D3 RID: 10707
				public readonly IntPtr glyphs;

				// Token: 0x040029D4 RID: 10708
				public readonly IntPtr _fontFamilyName;

				// Token: 0x040029D5 RID: 10709
				public readonly IntPtr _fontBaseName;

				// Token: 0x040029D6 RID: 10710
				public readonly float fontSize;

				// Token: 0x040029D7 RID: 10711
				public readonly int fontColor;

				// Token: 0x040029D8 RID: 10712
				[MarshalAs(UnmanagedType.I1)]
				public readonly bool isFontBold;

				// Token: 0x040029D9 RID: 10713
				[MarshalAs(UnmanagedType.I1)]
				public readonly bool isFontItalic;

				// Token: 0x040029DA RID: 10714
				public readonly int renderingOrder;

				// Token: 0x040029DB RID: 10715
				public readonly TransformationMatrix transformation;

				// Token: 0x040029DC RID: 10716
				public readonly BoundingPoints boundingPoints;

				// Token: 0x040029DD RID: 10717
				public readonly TextDirection textDirection;

				// Token: 0x040029DE RID: 10718
				public readonly TextDirection origTextDirection;

				// Token: 0x040029DF RID: 10719
				public readonly TextDirection lineOrientation;
			}
		}

		// Token: 0x02000DC6 RID: 3526
		[NullableContext(2)]
		[Nullable(0)]
		internal class PdfPath
		{
			// Token: 0x06005990 RID: 22928 RVA: 0x0011C89C File Offset: 0x0011AA9C
			private PdfPath(PdfParser.PdfPath.PdfPathInfo pdfPathInfo)
			{
				this._pathInfo = pdfPathInfo;
				this.Segments = this._pathInfo._segments.MarshalCStructArray(this._pathInfo._numSegments);
				this.DashArray = this._pathInfo._dashArray.MarshalCStructArray(this._pathInfo._numDashes);
			}

			// Token: 0x17001057 RID: 4183
			// (get) Token: 0x06005991 RID: 22929 RVA: 0x0011C8F8 File Offset: 0x0011AAF8
			internal PointInfo StartPoint
			{
				get
				{
					return this._pathInfo.startPoint;
				}
			}

			// Token: 0x17001058 RID: 4184
			// (get) Token: 0x06005992 RID: 22930 RVA: 0x0011C905 File Offset: 0x0011AB05
			public BoundingBox BoundingBox
			{
				get
				{
					return this._pathInfo.boundingBox;
				}
			}

			// Token: 0x17001059 RID: 4185
			// (get) Token: 0x06005993 RID: 22931 RVA: 0x0011C914 File Offset: 0x0011AB14
			public Color? FillColor
			{
				get
				{
					if (!this._pathInfo.isFilled)
					{
						return null;
					}
					return new Color?(Color.FromArgb(this._pathInfo.fillColor));
				}
			}

			// Token: 0x1700105A RID: 4186
			// (get) Token: 0x06005994 RID: 22932 RVA: 0x0011C950 File Offset: 0x0011AB50
			public Color? StrokingColor
			{
				get
				{
					if (!this._pathInfo.isStroked)
					{
						return null;
					}
					return new Color?(Color.FromArgb(this._pathInfo.strokingColor));
				}
			}

			// Token: 0x1700105B RID: 4187
			// (get) Token: 0x06005995 RID: 22933 RVA: 0x0011C989 File Offset: 0x0011AB89
			public bool IsClosed
			{
				get
				{
					return this._pathInfo.isClosed;
				}
			}

			// Token: 0x1700105C RID: 4188
			// (get) Token: 0x06005996 RID: 22934 RVA: 0x0011C996 File Offset: 0x0011AB96
			public float LineWidth
			{
				get
				{
					return this._pathInfo.lineWidth;
				}
			}

			// Token: 0x1700105D RID: 4189
			// (get) Token: 0x06005997 RID: 22935 RVA: 0x0011C9A3 File Offset: 0x0011ABA3
			internal IReadOnlyList<PdfParser.PdfPath.PdfPathSegmentInfo> Segments { get; }

			// Token: 0x1700105E RID: 4190
			// (get) Token: 0x06005998 RID: 22936 RVA: 0x0011C9AB File Offset: 0x0011ABAB
			public int RenderingOrder
			{
				get
				{
					return this._pathInfo.renderingOrder;
				}
			}

			// Token: 0x1700105F RID: 4191
			// (get) Token: 0x06005999 RID: 22937 RVA: 0x0011C9B8 File Offset: 0x0011ABB8
			public TransformationMatrix Transformation
			{
				get
				{
					return this._pathInfo.transformation;
				}
			}

			// Token: 0x17001060 RID: 4192
			// (get) Token: 0x0600599A RID: 22938 RVA: 0x0011C9C5 File Offset: 0x0011ABC5
			public float DashPhase
			{
				get
				{
					return this._pathInfo.dashPhase;
				}
			}

			// Token: 0x17001061 RID: 4193
			// (get) Token: 0x0600599B RID: 22939 RVA: 0x0011C9D2 File Offset: 0x0011ABD2
			public float[] DashArray { get; }

			// Token: 0x0600599C RID: 22940 RVA: 0x0011C9DA File Offset: 0x0011ABDA
			[return: Nullable(new byte[] { 2, 1 })]
			public static IReadOnlyList<PdfParser.PdfPath> GetPaths(PdfParser.Page.PdfPageInfo pageInfo)
			{
				return pageInfo._paths.MarshalCArray(pageInfo._numPaths, (PdfParser.PdfPath.PdfPathInfo pathInfo) => new PdfParser.PdfPath(pathInfo));
			}

			// Token: 0x0600599D RID: 22941 RVA: 0x0011CA0C File Offset: 0x0011AC0C
			[NullableContext(1)]
			public GraphicalPath AsPath(double featureScale)
			{
				return new GraphicalPath(this.BoundingBox.AsPixelBounds(featureScale), this.StartPoint.AsPixelVector(featureScale), this.Segments.Select((PdfParser.PdfPath.PdfPathSegmentInfo segment) => segment.AsSegment(featureScale)).ToList<GraphicalPath.AbstractSegment>(), this.IsClosed, (int)Math.Ceiling((double)this.LineWidth * featureScale), this.FillColor, this.StrokingColor, this.RenderingOrder);
			}

			// Token: 0x040029E2 RID: 10722
			private readonly PdfParser.PdfPath.PdfPathInfo _pathInfo;

			// Token: 0x02000DC7 RID: 3527
			[NullableContext(1)]
			[Nullable(0)]
			internal struct PdfPathSegmentInfo
			{
				// Token: 0x0600599E RID: 22942 RVA: 0x0011CA9C File Offset: 0x0011AC9C
				public GraphicalPath.AbstractSegment AsSegment(double featureScale)
				{
					PdfPathSegmentType pdfPathSegmentType = this.segmentType;
					if (pdfPathSegmentType == PdfPathSegmentType.LineSegment)
					{
						return new GraphicalPath.LineSegment(this.endPoint.AsPixelVector(featureScale));
					}
					if (pdfPathSegmentType != PdfPathSegmentType.BezierCurveSegment)
					{
						throw new Exception("Unexpected segment type: " + this.segmentType.ToString());
					}
					return new GraphicalPath.BezierSegment(this.endPoint.AsPixelVector(featureScale), this.controlPoint1.AsPixelVector(featureScale), this.controlPoint2.AsPixelVector(featureScale));
				}

				// Token: 0x0600599F RID: 22943 RVA: 0x0011CB24 File Offset: 0x0011AD24
				public override string ToString()
				{
					PdfPathSegmentType pdfPathSegmentType = this.segmentType;
					if (pdfPathSegmentType != PdfPathSegmentType.LineSegment)
					{
						if (pdfPathSegmentType != PdfPathSegmentType.BezierCurveSegment)
						{
						}
						return string.Format("{0} segment going through control points {1} and {2} and ending at {3}", new object[]
						{
							(this.segmentType == PdfPathSegmentType.BezierCurveSegment) ? "Bezier curve" : "Unknown",
							this.controlPoint1,
							this.controlPoint2,
							this.endPoint
						});
					}
					return string.Format("Line segment ending at {0}", this.endPoint);
				}

				// Token: 0x040029E5 RID: 10725
				public readonly PointInfo endPoint;

				// Token: 0x040029E6 RID: 10726
				public readonly PointInfo controlPoint1;

				// Token: 0x040029E7 RID: 10727
				public readonly PointInfo controlPoint2;

				// Token: 0x040029E8 RID: 10728
				public readonly PdfPathSegmentType segmentType;
			}

			// Token: 0x02000DC8 RID: 3528
			[NullableContext(0)]
			private struct PdfPathInfo
			{
				// Token: 0x040029E9 RID: 10729
				public readonly PointInfo startPoint;

				// Token: 0x040029EA RID: 10730
				public readonly UIntPtr _numSegments;

				// Token: 0x040029EB RID: 10731
				public readonly IntPtr _segments;

				// Token: 0x040029EC RID: 10732
				public readonly BoundingBox boundingBox;

				// Token: 0x040029ED RID: 10733
				[MarshalAs(UnmanagedType.I1)]
				public readonly bool isClosed;

				// Token: 0x040029EE RID: 10734
				[MarshalAs(UnmanagedType.I1)]
				public readonly bool isFilled;

				// Token: 0x040029EF RID: 10735
				[MarshalAs(UnmanagedType.I1)]
				public readonly bool isStroked;

				// Token: 0x040029F0 RID: 10736
				public readonly int fillColor;

				// Token: 0x040029F1 RID: 10737
				public readonly int strokingColor;

				// Token: 0x040029F2 RID: 10738
				public readonly float lineWidth;

				// Token: 0x040029F3 RID: 10739
				public readonly UIntPtr _numDashes;

				// Token: 0x040029F4 RID: 10740
				public readonly IntPtr _dashArray;

				// Token: 0x040029F5 RID: 10741
				public readonly float dashPhase;

				// Token: 0x040029F6 RID: 10742
				public readonly int renderingOrder;

				// Token: 0x040029F7 RID: 10743
				public readonly TransformationMatrix transformation;
			}
		}
	}
}
