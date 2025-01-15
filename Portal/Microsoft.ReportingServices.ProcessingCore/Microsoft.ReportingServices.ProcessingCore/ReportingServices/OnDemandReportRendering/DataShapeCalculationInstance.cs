using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200008C RID: 140
	internal sealed class DataShapeCalculationInstance : BaseInstance
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0002636A File Offset: 0x0002456A
		internal DataShapeCalculationInstance(IReportScope ownerReportScope, DataShapeCalculation dataShapeCalculation, RenderingContext renderingContext)
			: base(ownerReportScope)
		{
			this.m_dataShapeCalculation = dataShapeCalculation;
			this.m_renderingContext = renderingContext;
			this.m_isNewContext = true;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00026388 File Offset: 0x00024588
		internal override void SetNewContext()
		{
			if (this.m_isNewContext)
			{
				return;
			}
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000263A0 File Offset: 0x000245A0
		protected override void ResetInstanceCache()
		{
			this.m_calculationValue = null;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000263AC File Offset: 0x000245AC
		internal object CalculationValue
		{
			get
			{
				if (this.m_calculationValue == null)
				{
					this.m_calculationValue = this.m_dataShapeCalculation.RifCalculation.EvaluateCalculationValue(this.m_reportScope.ReportScopeInstance, this.m_renderingContext.OdpContext);
					this.m_isNewContext = false;
				}
				return this.m_calculationValue;
			}
		}

		// Token: 0x04000240 RID: 576
		private readonly DataShapeCalculation m_dataShapeCalculation;

		// Token: 0x04000241 RID: 577
		private readonly RenderingContext m_renderingContext;

		// Token: 0x04000242 RID: 578
		private bool m_isNewContext;

		// Token: 0x04000243 RID: 579
		private object m_calculationValue;
	}
}
