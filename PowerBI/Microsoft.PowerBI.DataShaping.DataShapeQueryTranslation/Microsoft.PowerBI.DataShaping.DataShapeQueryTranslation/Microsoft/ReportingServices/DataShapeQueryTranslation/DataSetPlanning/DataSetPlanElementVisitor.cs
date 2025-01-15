using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E9 RID: 233
	internal abstract class DataSetPlanElementVisitor
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00023E48 File Offset: 0x00022048
		internal virtual void Visit(CalculationPlanElement planElement)
		{
			this.DefaultVisit(planElement);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00023E51 File Offset: 0x00022051
		internal virtual void Visit(DataMemberPlanElement planElement)
		{
			this.DefaultVisit(planElement);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00023E5A File Offset: 0x0002205A
		internal virtual void Visit(DataIntersectionPlanElement planElement)
		{
			this.DefaultVisit(planElement);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00023E63 File Offset: 0x00022063
		internal virtual void Visit(DataShapePlanElement planElement)
		{
			this.DefaultVisit(planElement);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00023E6C File Offset: 0x0002206C
		internal virtual void Visit(ExpressionPlanElement planElement)
		{
			this.DefaultVisit(planElement);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00023E75 File Offset: 0x00022075
		protected virtual void DefaultVisit(ScopePlanElement planElement)
		{
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00023E77 File Offset: 0x00022077
		protected virtual void DefaultVisit(NestedPlanElement planElement)
		{
		}
	}
}
