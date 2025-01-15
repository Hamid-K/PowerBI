using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000269 RID: 617
	public sealed class ChartFormulaParameterInstance : BaseInstance
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x00062183 File Offset: 0x00060383
		internal ChartFormulaParameterInstance(ChartFormulaParameter chartFormulaParameterDef)
			: base(chartFormulaParameterDef.ReportScope)
		{
			this.m_chartFormulaParameterDef = chartFormulaParameterDef;
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00062198 File Offset: 0x00060398
		public object Value
		{
			get
			{
				if (this.m_value == null && !this.m_chartFormulaParameterDef.ChartDef.IsOldSnapshot)
				{
					this.m_value = this.m_chartFormulaParameterDef.ChartFormulaParameterDef.EvaluateValue(this.ReportScopeInstance, this.m_chartFormulaParameterDef.ChartDef.RenderingContext.OdpContext).Value;
				}
				return this.m_value;
			}
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000621FB File Offset: 0x000603FB
		protected override void ResetInstanceCache()
		{
			this.m_value = null;
		}

		// Token: 0x04000C0E RID: 3086
		private ChartFormulaParameter m_chartFormulaParameterDef;

		// Token: 0x04000C0F RID: 3087
		private object m_value;
	}
}
