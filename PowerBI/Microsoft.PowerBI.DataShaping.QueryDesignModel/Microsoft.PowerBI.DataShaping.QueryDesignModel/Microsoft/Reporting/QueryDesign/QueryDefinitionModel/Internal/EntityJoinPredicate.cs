using System;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000EA RID: 234
	internal sealed class EntityJoinPredicate : IJoinPredicate
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x000237F6 File Offset: 0x000219F6
		internal EntityJoinPredicate(EntitySet entitySet, IConceptualEntity entity = null)
		{
			this._entitySet = entitySet;
			this._entity = entity;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0002380C File Offset: 0x00021A0C
		public bool IsAnchored
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0002380F File Offset: 0x00021A0F
		internal EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00023817 File Offset: 0x00021A17
		internal IConceptualEntity Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0002381F File Offset: 0x00021A1F
		public QueryExpression ToPredicateExpression()
		{
			IConceptualEntity entity = this._entity;
			return (((entity != null) ? entity.Scan(true) : null) ?? this._entitySet.Scan(true)).HasAnyRows(false);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002384A File Offset: 0x00021A4A
		public IJoinPredicate ToPredicateWithCanonicalQueryExpressions()
		{
			return this;
		}

		// Token: 0x040009AD RID: 2477
		private readonly EntitySet _entitySet;

		// Token: 0x040009AE RID: 2478
		private readonly IConceptualEntity _entity;
	}
}
