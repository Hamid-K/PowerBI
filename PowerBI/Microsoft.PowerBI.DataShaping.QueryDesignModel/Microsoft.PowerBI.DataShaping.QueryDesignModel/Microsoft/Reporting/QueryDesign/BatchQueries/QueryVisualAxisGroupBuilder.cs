using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.QueryDesignModel.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000276 RID: 630
	internal sealed class QueryVisualAxisGroupBuilder : IGroupBuilder<QueryExpression>, IGroupBuilder
	{
		// Token: 0x06001B32 RID: 6962 RVA: 0x0004C6EC File Offset: 0x0004A8EC
		public QueryVisualAxisGroupBuilder()
		{
			this._keys = new List<QueryExpression>();
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0004C6FF File Offset: 0x0004A8FF
		public bool IsEmpty
		{
			get
			{
				return this._keys.Count == 0;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0004C70F File Offset: 0x0004A90F
		public void AddKey(QueryExpression key)
		{
			this._keys.Add(key);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0004C71D File Offset: 0x0004A91D
		public void SetSubtotalIndicator(QueryExpression subtotalIndicator)
		{
			this._subtotalIndicator = subtotalIndicator;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0004C728 File Offset: 0x0004A928
		public QueryVisualAxisGroup ToVisualAxisGroup(Func<QueryExpression, QueryExpression> rewriteExpression)
		{
			IReadOnlyList<QueryExpression> readOnlyList = this._keys.CreateList(rewriteExpression);
			QueryExpression queryExpression = rewriteExpression(this._subtotalIndicator);
			return new QueryVisualAxisGroup(readOnlyList, queryExpression);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0004C754 File Offset: 0x0004A954
		public void RemoveDuplicateKeys(HashSet<QueryExpression> existingKeys)
		{
			QueryTableUtils.RemoveDuplicateKeys<QueryExpression>(this._keys, existingKeys);
		}

		// Token: 0x04000EEC RID: 3820
		private readonly List<QueryExpression> _keys;

		// Token: 0x04000EED RID: 3821
		private QueryExpression _subtotalIndicator;
	}
}
