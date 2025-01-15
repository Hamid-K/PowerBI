using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000097 RID: 151
	internal sealed class Image : ReportItem
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000CE68 File Offset: 0x0000B068
		internal Image(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, ReportImageSource? source, string value, string mimeType, ReportImageSizing? sizing, string tag, string imageCacheScope, string dataSetName = null, ImageLookup imageLookup = null)
			: base("Image", name, rect, zIndex, diagnosticContext)
		{
			this._tag = tag;
			this._imageCacheScope = imageCacheScope;
			this._dataSetName = dataSetName;
			this._source = new ImageSource(source, value, mimeType);
			this._sizing = sizing;
			this._imageLookup = imageLookup;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000CEBE File Offset: 0x0000B0BE
		public string DataSetName
		{
			get
			{
				if (this._imageLookup != null)
				{
					return this._imageLookup.DataSetName;
				}
				return this._dataSetName;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		public ImageSource Source
		{
			get
			{
				if (this._imageLookup != null)
				{
					return this._imageLookup.Source;
				}
				return this._source;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000CEF6 File Offset: 0x0000B0F6
		public ReportImageSizing? Sizing
		{
			get
			{
				return this._sizing;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000CEFE File Offset: 0x0000B0FE
		public string Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000CF06 File Offset: 0x0000B106
		public string ImageCacheScope
		{
			get
			{
				return this._imageCacheScope;
			}
		}

		// Token: 0x040001F1 RID: 497
		private readonly ImageSource _source;

		// Token: 0x040001F2 RID: 498
		private readonly string _dataSetName;

		// Token: 0x040001F3 RID: 499
		private readonly ReportImageSizing? _sizing;

		// Token: 0x040001F4 RID: 500
		private readonly string _tag;

		// Token: 0x040001F5 RID: 501
		private readonly string _imageCacheScope;

		// Token: 0x040001F6 RID: 502
		private readonly ImageLookup _imageLookup;
	}
}
