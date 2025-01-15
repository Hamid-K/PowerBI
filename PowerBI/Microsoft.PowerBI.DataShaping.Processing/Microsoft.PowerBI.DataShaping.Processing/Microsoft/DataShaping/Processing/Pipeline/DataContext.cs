using System;
using Microsoft.DataShaping.Common;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200008C RID: 140
	internal sealed class DataContext
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0000BED6 File Offset: 0x0000A0D6
		internal DataContext(IDataComparer comparer)
		{
			this._matchConditions = new MatchConditionGovernor(comparer);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000BEEA File Offset: 0x0000A0EA
		internal MatchConditionGovernor MatchConditions
		{
			get
			{
				return this._matchConditions;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000BEF2 File Offset: 0x0000A0F2
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000BEFA File Offset: 0x0000A0FA
		internal IDataRow ActiveRow
		{
			get
			{
				return this._activeRow;
			}
			set
			{
				this._activeRow = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000BF03 File Offset: 0x0000A103
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000BF0B File Offset: 0x0000A10B
		internal IDataRow PendingRow
		{
			get
			{
				return this._pendingRow;
			}
			set
			{
				this._pendingRow = value;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000BF14 File Offset: 0x0000A114
		internal void ClearActiveRow()
		{
			this._activeRow = null;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000BF1D File Offset: 0x0000A11D
		internal void ClearPendingRow()
		{
			this._pendingRow = null;
		}

		// Token: 0x04000200 RID: 512
		private readonly MatchConditionGovernor _matchConditions;

		// Token: 0x04000201 RID: 513
		private IDataRow _activeRow;

		// Token: 0x04000202 RID: 514
		private IDataRow _pendingRow;
	}
}
