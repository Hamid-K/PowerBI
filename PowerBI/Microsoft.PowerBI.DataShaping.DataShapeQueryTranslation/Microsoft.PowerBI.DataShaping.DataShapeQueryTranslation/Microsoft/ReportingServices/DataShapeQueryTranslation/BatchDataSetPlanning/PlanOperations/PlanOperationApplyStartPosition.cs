using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F1 RID: 497
	internal sealed class PlanOperationApplyStartPosition : PlanOperation
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x00046B56 File Offset: 0x00044D56
		internal PlanOperationApplyStartPosition(PlanOperation input, IEnumerable<DataMember> members, RestartMatchingBehavior? restartMatchingBehavior)
		{
			this.Input = input;
			this.Members = members.ToReadOnlyCollection<DataMember>();
			this.RestartMatchingBehavior = restartMatchingBehavior;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00046B78 File Offset: 0x00044D78
		public PlanOperation Input { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00046B80 File Offset: 0x00044D80
		public ReadOnlyCollection<DataMember> Members { get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00046B88 File Offset: 0x00044D88
		public RestartMatchingBehavior? RestartMatchingBehavior { get; }

		// Token: 0x06001136 RID: 4406 RVA: 0x00046B90 File Offset: 0x00044D90
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00046B9C File Offset: 0x00044D9C
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationApplyStartPosition planOperationApplyStartPosition;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationApplyStartPosition>(this, other, out flag, out planOperationApplyStartPosition))
			{
				return flag;
			}
			if (this.Input.Equals(planOperationApplyStartPosition.Input) && this.Members.SequenceEqual(planOperationApplyStartPosition.Members))
			{
				RestartMatchingBehavior? restartMatchingBehavior = this.RestartMatchingBehavior;
				RestartMatchingBehavior? restartMatchingBehavior2 = planOperationApplyStartPosition.RestartMatchingBehavior;
				return (restartMatchingBehavior.GetValueOrDefault() == restartMatchingBehavior2.GetValueOrDefault()) & (restartMatchingBehavior != null == (restartMatchingBehavior2 != null));
			}
			return false;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00046C10 File Offset: 0x00044E10
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ApplyStartPosition");
			builder.WriteProperty<ReadOnlyCollection<DataMember>>("Members", this.Members, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.WriteProperty<RestartMatchingBehavior?>("RestartMatchingBehavior", this.RestartMatchingBehavior, false);
			builder.EndObject();
		}
	}
}
