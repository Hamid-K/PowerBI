using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000026 RID: 38
	[ImmutableObject(true)]
	internal sealed class EdmKpi
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00008538 File Offset: 0x00006738
		internal EdmKpi(string statusGraphic, string trendGraphic, string kpiGoal, string kpiStatus, string kpiTrend, string description)
		{
			this._trendGraphic = trendGraphic;
			this._statusGraphic = statusGraphic;
			this._kpiGoal = kpiGoal;
			this._kpiStatus = kpiStatus;
			this._kpiTrend = kpiTrend;
			this._description = description;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000856D File Offset: 0x0000676D
		internal string StatusGraphic
		{
			get
			{
				return this._statusGraphic;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008575 File Offset: 0x00006775
		internal string TrendGraphic
		{
			get
			{
				return this._trendGraphic;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000857D File Offset: 0x0000677D
		internal string KpiGoal
		{
			get
			{
				return this._kpiGoal;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008585 File Offset: 0x00006785
		internal string KpiStatus
		{
			get
			{
				return this._kpiStatus;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000858D File Offset: 0x0000678D
		internal string KpiTrend
		{
			get
			{
				return this._kpiTrend;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008595 File Offset: 0x00006795
		internal string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04000161 RID: 353
		private readonly string _statusGraphic;

		// Token: 0x04000162 RID: 354
		private readonly string _trendGraphic;

		// Token: 0x04000163 RID: 355
		private readonly string _kpiGoal;

		// Token: 0x04000164 RID: 356
		private readonly string _kpiStatus;

		// Token: 0x04000165 RID: 357
		private readonly string _kpiTrend;

		// Token: 0x04000166 RID: 358
		private readonly string _description;
	}
}
