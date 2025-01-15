using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000DD RID: 221
	internal sealed class Relationship : IStructuredToString
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x000232D6 File Offset: 0x000214D6
		internal Relationship(int targetDataSetPlanIndex, IScope parentScope, List<JoinCondition> joinConditions = null)
		{
			this.m_targetDataSetPlanIndex = targetDataSetPlanIndex;
			this.m_parentScope = parentScope;
			if (joinConditions != null)
			{
				this.m_joinConditions = joinConditions.AsReadOnly();
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000232FB File Offset: 0x000214FB
		public int TargetDataSetPlanIndex
		{
			get
			{
				return this.m_targetDataSetPlanIndex;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00023303 File Offset: 0x00021503
		public ReadOnlyCollection<JoinCondition> JoinConditions
		{
			get
			{
				return this.m_joinConditions;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0002330B File Offset: 0x0002150B
		public IScope ParentScope
		{
			get
			{
				return this.m_parentScope;
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00023314 File Offset: 0x00021514
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("Relationship");
			builder.WriteAttribute<IScope>("ParentScope", this.m_parentScope, false, false);
			builder.WriteAttribute<int>("TargetPlanIndex", this.m_targetDataSetPlanIndex, true, false);
			builder.WriteProperty<ReadOnlyCollection<JoinCondition>>("JoinConditions", this.m_joinConditions, false);
			builder.EndObject();
		}

		// Token: 0x0400044F RID: 1103
		private readonly IScope m_parentScope;

		// Token: 0x04000450 RID: 1104
		private readonly int m_targetDataSetPlanIndex;

		// Token: 0x04000451 RID: 1105
		private readonly ReadOnlyCollection<JoinCondition> m_joinConditions;
	}
}
