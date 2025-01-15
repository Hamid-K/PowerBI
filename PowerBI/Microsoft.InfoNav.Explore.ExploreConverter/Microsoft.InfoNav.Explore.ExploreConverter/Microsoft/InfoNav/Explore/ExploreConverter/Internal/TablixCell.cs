using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A2 RID: 162
	internal sealed class TablixCell
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		internal TablixCell(ReportItem reportItem, string dataSetName)
		{
			this._reportItem = reportItem;
			this._dataSetName = dataSetName;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000D1E7 File Offset: 0x0000B3E7
		public ReportItem ReportItem
		{
			get
			{
				return this._reportItem;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
		}

		// Token: 0x04000219 RID: 537
		private readonly string _dataSetName;

		// Token: 0x0400021A RID: 538
		private readonly ReportItem _reportItem;
	}
}
