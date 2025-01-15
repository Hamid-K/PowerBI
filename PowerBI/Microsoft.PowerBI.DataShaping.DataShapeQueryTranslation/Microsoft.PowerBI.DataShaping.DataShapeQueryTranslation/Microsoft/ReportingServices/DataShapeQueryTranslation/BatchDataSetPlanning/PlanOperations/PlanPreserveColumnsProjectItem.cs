using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000219 RID: 537
	internal class PlanPreserveColumnsProjectItem : PlanProjectItem
	{
		// Token: 0x06001296 RID: 4758 RVA: 0x00049208 File Offset: 0x00047408
		internal PlanPreserveColumnsProjectItem(params ExpressionId[] planIdentities)
		{
			this.PlanIdentities = planIdentities;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00049217 File Offset: 0x00047417
		internal PlanPreserveColumnsProjectItem(IEnumerable<ExpressionId> planIdentities)
		{
			this.PlanIdentities = planIdentities;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00049226 File Offset: 0x00047426
		public IEnumerable<ExpressionId> PlanIdentities { get; }

		// Token: 0x06001299 RID: 4761 RVA: 0x0004922E File Offset: 0x0004742E
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00049237 File Offset: 0x00047437
		protected override int GetHashCodeInternal()
		{
			return this.PlanIdentities.GetHashCode();
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00049244 File Offset: 0x00047444
		public override bool Equals(PlanProjectItem other)
		{
			PlanPreserveColumnsProjectItem planPreserveColumnsProjectItem = other as PlanPreserveColumnsProjectItem;
			return planPreserveColumnsProjectItem != null && this.PlanIdentities.SequenceEqual(planPreserveColumnsProjectItem.PlanIdentities);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004926E File Offset: 0x0004746E
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PreserveColumnProjectItem");
			builder.WriteProperty<IEnumerable<IStructuredToString>>("PlanIdentities", this.PlanIdentities.Cast<IStructuredToString>(), false);
			builder.EndObject();
		}
	}
}
