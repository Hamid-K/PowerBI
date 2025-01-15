using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000215 RID: 533
	internal sealed class PlanOperationOverlappingPointsSample : PlanOperation
	{
		// Token: 0x06001277 RID: 4727 RVA: 0x00048F2A File Offset: 0x0004712A
		internal PlanOperationOverlappingPointsSample(PlanOperation input, PlanExpression x, PlanExpression y, PlanExpression targetPointCount)
		{
			this.m_input = input;
			this.m_x = x;
			this.m_y = y;
			this.m_targetPointCount = targetPointCount;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00048F4F File Offset: 0x0004714F
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00048F58 File Offset: 0x00047158
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00048F60 File Offset: 0x00047160
		internal PlanExpression X
		{
			get
			{
				return this.m_x;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00048F68 File Offset: 0x00047168
		internal PlanExpression Y
		{
			get
			{
				return this.m_y;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00048F70 File Offset: 0x00047170
		public PlanExpression TargetPointCount
		{
			get
			{
				return this.m_targetPointCount;
			}
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00048F78 File Offset: 0x00047178
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationOverlappingPointsSample planOperationOverlappingPointsSample;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationOverlappingPointsSample>(this, other, out flag, out planOperationOverlappingPointsSample))
			{
				return flag;
			}
			return this.Input.Equals(planOperationOverlappingPointsSample.Input) && object.Equals(this.X, planOperationOverlappingPointsSample.X) && object.Equals(this.Y, planOperationOverlappingPointsSample.Y) && this.TargetPointCount.Equals(planOperationOverlappingPointsSample.TargetPointCount);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00048FE0 File Offset: 0x000471E0
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("OverlappingPointsSample");
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.WriteProperty<PlanExpression>("X", this.X, false);
			builder.WriteProperty<PlanExpression>("Y", this.Y, false);
			builder.WriteProperty<PlanExpression>("TargetPointCount", this.TargetPointCount, false);
			builder.EndObject();
		}

		// Token: 0x04000845 RID: 2117
		private readonly PlanOperation m_input;

		// Token: 0x04000846 RID: 2118
		private readonly PlanExpression m_x;

		// Token: 0x04000847 RID: 2119
		private readonly PlanExpression m_y;

		// Token: 0x04000848 RID: 2120
		private readonly PlanExpression m_targetPointCount;
	}
}
