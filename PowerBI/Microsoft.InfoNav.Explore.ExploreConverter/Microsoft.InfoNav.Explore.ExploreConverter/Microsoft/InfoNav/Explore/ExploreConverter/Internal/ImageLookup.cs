using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000098 RID: 152
	internal sealed class ImageLookup
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000CF0E File Offset: 0x0000B10E
		internal ImageLookup(ReportImageSource? source, string value, string mimeType, string dataSetName)
		{
			this._source = new ImageSource(source, value, mimeType);
			this._dataSetName = dataSetName;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public ImageSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000CF34 File Offset: 0x0000B134
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
		}

		// Token: 0x040001F7 RID: 503
		private readonly ImageSource _source;

		// Token: 0x040001F8 RID: 504
		private readonly string _dataSetName;
	}
}
