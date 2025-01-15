using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200022F RID: 559
	internal sealed class PlanOperationTableScan : PlanOperation
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x0004A107 File Offset: 0x00048307
		internal PlanOperationTableScan(IConceptualEntity entity, IReadOnlyList<ExpressionId> expectedProjections)
		{
			this.TargetEntity = entity;
			this.ExpectedProjections = expectedProjections;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004A11D File Offset: 0x0004831D
		internal PlanOperationTableScan(string targetEntityPlanName, IReadOnlyList<ExpressionId> expectedProjections)
		{
			this.TargetEntityPlanName = targetEntityPlanName;
			this.ExpectedProjections = expectedProjections;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0004A133 File Offset: 0x00048333
		internal IConceptualEntity TargetEntity { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0004A13B File Offset: 0x0004833B
		internal string TargetEntityPlanName { get; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0004A143 File Offset: 0x00048343
		internal IReadOnlyList<ExpressionId> ExpectedProjections { get; }

		// Token: 0x06001330 RID: 4912 RVA: 0x0004A14B File Offset: 0x0004834B
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004A154 File Offset: 0x00048354
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationTableScan planOperationTableScan;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationTableScan>(this, other, out flag, out planOperationTableScan))
			{
				return flag;
			}
			return this.ExpectedProjections.SequenceEqualReadOnly(planOperationTableScan.ExpectedProjections) && object.Equals(this.TargetEntity, planOperationTableScan.TargetEntity) && this.TargetEntityPlanName == planOperationTableScan.TargetEntityPlanName;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004A1AC File Offset: 0x000483AC
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("TableScan");
			string text = "TargetEntity";
			IConceptualEntity targetEntity = this.TargetEntity;
			builder.WriteAttribute<string>(text, (targetEntity != null) ? targetEntity.EdmName : null, false, false);
			builder.WriteAttribute<string>("TargetEntityPlanName", this.TargetEntityPlanName, false, false);
			builder.WriteProperty<IEnumerable<IStructuredToString>>("ExpectedProjections", this.ExpectedProjections.Cast<IStructuredToString>(), false);
			builder.EndObject();
		}
	}
}
