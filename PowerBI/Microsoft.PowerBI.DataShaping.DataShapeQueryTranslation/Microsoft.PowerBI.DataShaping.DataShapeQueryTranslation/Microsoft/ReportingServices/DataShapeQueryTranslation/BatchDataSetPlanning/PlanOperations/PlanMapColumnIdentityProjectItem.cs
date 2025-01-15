using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000223 RID: 547
	internal sealed class PlanMapColumnIdentityProjectItem : PlanProjectItem
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00049964 File Offset: 0x00047B64
		internal PlanMapColumnIdentityProjectItem(ExpressionId sourceIdentity, IReadOnlyList<ExpressionId> targetIdentities)
		{
			this.SourceIdentity = sourceIdentity;
			this.TargetIdentities = targetIdentities;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004997A File Offset: 0x00047B7A
		public ExpressionId SourceIdentity { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00049982 File Offset: 0x00047B82
		public IReadOnlyList<ExpressionId> TargetIdentities { get; }

		// Token: 0x060012DB RID: 4827 RVA: 0x0004998A File Offset: 0x00047B8A
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00049993 File Offset: 0x00047B93
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<ExpressionId>(this.SourceIdentity, null), Hashing.GetHashCode<IReadOnlyList<ExpressionId>>(this.TargetIdentities, null));
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000499B4 File Offset: 0x00047BB4
		public override bool Equals(PlanProjectItem other)
		{
			PlanMapColumnIdentityProjectItem planMapColumnIdentityProjectItem = other as PlanMapColumnIdentityProjectItem;
			return planMapColumnIdentityProjectItem != null && this.SourceIdentity == planMapColumnIdentityProjectItem.SourceIdentity && this.TargetIdentities.SequenceEqual(planMapColumnIdentityProjectItem.TargetIdentities);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000499F1 File Offset: 0x00047BF1
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("MapColumnIdentityProjectItem");
			builder.WriteAttribute<ExpressionId>("SourceIdentity", this.SourceIdentity, false, true);
			builder.WriteAttribute<IReadOnlyList<ExpressionId>>("TargetIdentities", this.TargetIdentities, false, true);
			builder.EndObject();
		}
	}
}
