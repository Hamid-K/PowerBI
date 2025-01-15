using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200009D RID: 157
	internal sealed class BandLayoutOptions
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000D04C File Offset: 0x0000B24C
		internal BandLayoutOptions(Navigation navigation, int rowCount, int columnCount, bool automaticColumnAndRowCount)
		{
			this._navigation = navigation;
			this._rowCount = rowCount;
			this._columnCount = columnCount;
			this._automaticColumnAndRowCount = automaticColumnAndRowCount;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000D071 File Offset: 0x0000B271
		public Navigation Navigation
		{
			get
			{
				return this._navigation;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000D079 File Offset: 0x0000B279
		public int RowCount
		{
			get
			{
				return this._rowCount;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000D081 File Offset: 0x0000B281
		public int ColumnCount
		{
			get
			{
				return this._columnCount;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000D089 File Offset: 0x0000B289
		public bool AutomaticColumnAndRowCount
		{
			get
			{
				return this._automaticColumnAndRowCount;
			}
		}

		// Token: 0x04000207 RID: 519
		private readonly Navigation _navigation;

		// Token: 0x04000208 RID: 520
		private readonly int _rowCount;

		// Token: 0x04000209 RID: 521
		private readonly int _columnCount;

		// Token: 0x0400020A RID: 522
		private readonly bool _automaticColumnAndRowCount;
	}
}
