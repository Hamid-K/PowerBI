using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A3 RID: 163
	internal sealed class TablixHeader
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		internal TablixHeader(ReportItem cellContents, ReportSize size)
		{
			this._cellContents = cellContents;
			this._size = size;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D20D File Offset: 0x0000B40D
		public ReportItem CellContents
		{
			get
			{
				return this._cellContents;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000D215 File Offset: 0x0000B415
		public ReportSize Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0400021B RID: 539
		private readonly ReportItem _cellContents;

		// Token: 0x0400021C RID: 540
		private readonly ReportSize _size;
	}
}
