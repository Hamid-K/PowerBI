using System;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E5 RID: 485
	internal sealed class EdmConceptualKpi : IConceptualKpi
	{
		// Token: 0x06001718 RID: 5912 RVA: 0x0003F788 File Offset: 0x0003D988
		internal EdmConceptualKpi(Kpi kpi)
		{
			this._kpi = kpi;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0003F797 File Offset: 0x0003D997
		public string StatusGraphic
		{
			get
			{
				return this._kpi.StatusGraphic;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0003F7A4 File Offset: 0x0003D9A4
		public string TrendGraphic
		{
			get
			{
				return this._kpi.TrendGraphic;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0003F7B1 File Offset: 0x0003D9B1
		public IConceptualMeasure Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0003F7B9 File Offset: 0x0003D9B9
		public IConceptualMeasure Goal
		{
			get
			{
				return this._goal;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0003F7C1 File Offset: 0x0003D9C1
		public IConceptualMeasure Trend
		{
			get
			{
				return this._trend;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x0003F7C9 File Offset: 0x0003D9C9
		public string Description
		{
			get
			{
				return this._kpi.Description;
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0003F7D8 File Offset: 0x0003D9D8
		internal void CompleteInitialization(EdmConceptualPropertyInitContext context)
		{
			if (this._kpi.Goal != null)
			{
				this._goal = context.GetProperty<IConceptualMeasure>(this._kpi.Goal);
			}
			if (this._kpi.Status != null)
			{
				this._status = context.GetProperty<IConceptualMeasure>(this._kpi.Status);
			}
			if (this._kpi.Trend != null)
			{
				this._trend = context.GetProperty<IConceptualMeasure>(this._kpi.Trend);
			}
		}

		// Token: 0x04000C55 RID: 3157
		private readonly Kpi _kpi;

		// Token: 0x04000C56 RID: 3158
		private IConceptualMeasure _status;

		// Token: 0x04000C57 RID: 3159
		private IConceptualMeasure _goal;

		// Token: 0x04000C58 RID: 3160
		private IConceptualMeasure _trend;
	}
}
