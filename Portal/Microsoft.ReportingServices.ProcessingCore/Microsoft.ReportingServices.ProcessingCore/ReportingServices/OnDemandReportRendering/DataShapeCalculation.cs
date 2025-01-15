using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200008A RID: 138
	internal sealed class DataShapeCalculation
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x000261F3 File Offset: 0x000243F3
		internal DataShapeCalculation(IReportScope ownerReportScope, Calculation rifCalculation, RenderingContext renderingContext)
		{
			this.m_ownerReportScope = ownerReportScope;
			this.m_rifCalculation = rifCalculation;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00026210 File Offset: 0x00024410
		public string ClientID
		{
			get
			{
				return this.m_rifCalculation.Name;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002621D File Offset: 0x0002441D
		internal Calculation RifCalculation
		{
			get
			{
				return this.m_rifCalculation;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00026225 File Offset: 0x00024425
		internal DataShapeCalculationInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new DataShapeCalculationInstance(this.m_ownerReportScope, this, this.m_renderingContext);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002624D File Offset: 0x0002444D
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000238 RID: 568
		private readonly IReportScope m_ownerReportScope;

		// Token: 0x04000239 RID: 569
		private readonly Calculation m_rifCalculation;

		// Token: 0x0400023A RID: 570
		private readonly RenderingContext m_renderingContext;

		// Token: 0x0400023B RID: 571
		private DataShapeCalculationInstance m_instance;
	}
}
