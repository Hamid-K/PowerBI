using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A7 RID: 167
	internal sealed class TablixRow
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000D2F7 File Offset: 0x0000B4F7
		internal TablixRow(ReportSize height, List<TablixCell> tablixCells)
		{
			Contract.Check(tablixCells != null, "Expect tablixCells to not be null");
			this._height = height;
			this._tablixCells = tablixCells;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000D31B File Offset: 0x0000B51B
		public ReportSize Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000D323 File Offset: 0x0000B523
		public List<TablixCell> TablixCells
		{
			get
			{
				return this._tablixCells;
			}
		}

		// Token: 0x04000227 RID: 551
		private readonly ReportSize _height;

		// Token: 0x04000228 RID: 552
		private readonly List<TablixCell> _tablixCells;
	}
}
