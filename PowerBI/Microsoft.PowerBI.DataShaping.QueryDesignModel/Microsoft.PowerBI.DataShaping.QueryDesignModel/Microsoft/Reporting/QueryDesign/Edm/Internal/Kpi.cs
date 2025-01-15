using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000248 RID: 584
	internal sealed class Kpi
	{
		// Token: 0x060019C5 RID: 6597 RVA: 0x00046D2B File Offset: 0x00044F2B
		internal Kpi(string statusGraphic, string trendGraphic, EdmMeasure value, EdmMeasure goal, EdmMeasure status, EdmMeasure trend, string description)
		{
			this._statusGraphic = statusGraphic;
			this._trendGraphic = trendGraphic;
			this._goal = goal;
			this._value = value;
			this._status = status;
			this._trend = trend;
			this._description = description;
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00046D68 File Offset: 0x00044F68
		public string StatusGraphic
		{
			get
			{
				return this._statusGraphic;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00046D70 File Offset: 0x00044F70
		public string TrendGraphic
		{
			get
			{
				return this._trendGraphic;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00046D78 File Offset: 0x00044F78
		public EdmMeasure Goal
		{
			get
			{
				return this._goal;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00046D80 File Offset: 0x00044F80
		public EdmMeasure Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00046D88 File Offset: 0x00044F88
		public EdmMeasure Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00046D90 File Offset: 0x00044F90
		public EdmMeasure Trend
		{
			get
			{
				return this._trend;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00046D98 File Offset: 0x00044F98
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04000E4B RID: 3659
		private readonly EdmMeasure _goal;

		// Token: 0x04000E4C RID: 3660
		private readonly EdmMeasure _status;

		// Token: 0x04000E4D RID: 3661
		private readonly EdmMeasure _value;

		// Token: 0x04000E4E RID: 3662
		private readonly EdmMeasure _trend;

		// Token: 0x04000E4F RID: 3663
		private readonly string _statusGraphic;

		// Token: 0x04000E50 RID: 3664
		private readonly string _trendGraphic;

		// Token: 0x04000E51 RID: 3665
		private readonly string _description;
	}
}
