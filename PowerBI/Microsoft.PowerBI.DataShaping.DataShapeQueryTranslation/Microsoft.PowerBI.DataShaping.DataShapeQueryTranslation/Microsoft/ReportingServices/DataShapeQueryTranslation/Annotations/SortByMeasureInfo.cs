using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000248 RID: 584
	internal sealed class SortByMeasureInfo
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x0004D3FE File Offset: 0x0004B5FE
		internal SortByMeasureInfo(string suggestedName, ExpressionId sortKeyPlanIdentity, ExpressionId? scopeValuePlanIdentity)
		{
			this.m_suggestedName = suggestedName;
			this.m_sortKeyPlanIdentity = sortKeyPlanIdentity;
			this.m_scopeValuePlanIdentity = scopeValuePlanIdentity;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0004D41B File Offset: 0x0004B61B
		public string SuggestedName
		{
			get
			{
				return this.m_suggestedName;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0004D423 File Offset: 0x0004B623
		public ExpressionId SortKeyPlanIdentity
		{
			get
			{
				return this.m_sortKeyPlanIdentity;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x0004D42B File Offset: 0x0004B62B
		public ExpressionId? ScopeValuePlanIdentity
		{
			get
			{
				return this.m_scopeValuePlanIdentity;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004D433 File Offset: 0x0004B633
		public IEnumerable<ExpressionId> GetEffectivePlanIdentities()
		{
			yield return this.m_sortKeyPlanIdentity;
			if (this.m_scopeValuePlanIdentity != null)
			{
				yield return this.m_scopeValuePlanIdentity.Value;
			}
			yield break;
		}

		// Token: 0x040008C8 RID: 2248
		private readonly string m_suggestedName;

		// Token: 0x040008C9 RID: 2249
		private readonly ExpressionId? m_scopeValuePlanIdentity;

		// Token: 0x040008CA RID: 2250
		private readonly ExpressionId m_sortKeyPlanIdentity;
	}
}
