using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001FD RID: 509
	internal sealed class PlanOperationDataTransform : PlanOperation
	{
		// Token: 0x060011CA RID: 4554 RVA: 0x00047CA6 File Offset: 0x00045EA6
		internal PlanOperationDataTransform(PlanOperation input, DataTransform transform)
		{
			this.m_input = input;
			this.m_transform = transform;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00047CBC File Offset: 0x00045EBC
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00047CC4 File Offset: 0x00045EC4
		public DataTransform Transform
		{
			get
			{
				return this.m_transform;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00047CCC File Offset: 0x00045ECC
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00047CD8 File Offset: 0x00045ED8
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationDataTransform planOperationDataTransform;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationDataTransform>(this, other, out flag, out planOperationDataTransform))
			{
				return flag;
			}
			return this.Input.Equals(planOperationDataTransform.Input) && this.Transform == planOperationDataTransform.Transform;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00047D17 File Offset: 0x00045F17
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataTransform");
			builder.WriteProperty<DataTransform>("Transform", this.Transform, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x04000810 RID: 2064
		private readonly PlanOperation m_input;

		// Token: 0x04000811 RID: 2065
		private readonly DataTransform m_transform;
	}
}
