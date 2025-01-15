using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A1 RID: 161
	internal sealed class TablixBody
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000D18F File Offset: 0x0000B38F
		internal TablixBody(List<ReportSize> tablixColumns, List<TablixRow> tablixRows)
		{
			Contract.Check(tablixColumns != null, "Expect tablixColumns to not be null");
			Contract.Check(tablixRows != null, "Expect tablixRows to not be null");
			this._tablixColumns = tablixColumns;
			this._tablixRows = tablixRows;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000D1C1 File Offset: 0x0000B3C1
		public List<ReportSize> TablixColumns
		{
			get
			{
				return this._tablixColumns;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000D1C9 File Offset: 0x0000B3C9
		public List<TablixRow> TablixRows
		{
			get
			{
				return this._tablixRows;
			}
		}

		// Token: 0x04000217 RID: 535
		private readonly List<ReportSize> _tablixColumns;

		// Token: 0x04000218 RID: 536
		private readonly List<TablixRow> _tablixRows;
	}
}
