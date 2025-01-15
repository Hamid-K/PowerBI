using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000AA RID: 170
	public abstract class ChartDerivedSeriesExprHost : StyleExprHost
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000037EE File Offset: 0x000019EE
		internal IList<ChartFormulaParameterExprHost> ChartFormulaParametersHostsRemotable
		{
			get
			{
				return this.m_formulaParametersHostsRemotable;
			}
		}

		// Token: 0x0400011D RID: 285
		public ChartSeriesExprHost ChartSeriesHost;

		// Token: 0x0400011E RID: 286
		[CLSCompliant(false)]
		protected IList<ChartFormulaParameterExprHost> m_formulaParametersHostsRemotable;
	}
}
