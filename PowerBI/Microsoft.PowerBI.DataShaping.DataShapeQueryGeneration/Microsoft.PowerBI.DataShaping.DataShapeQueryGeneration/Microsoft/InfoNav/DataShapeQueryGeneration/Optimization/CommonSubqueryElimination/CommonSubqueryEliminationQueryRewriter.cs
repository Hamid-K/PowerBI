using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x020000FE RID: 254
	internal sealed class CommonSubqueryEliminationQueryRewriter : ResolvedQueryDefinitionRewriter
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x00021C32 File Offset: 0x0001FE32
		internal CommonSubqueryEliminationQueryRewriter(IReadOnlyList<ResolvedQueryLetBinding> newLetBindings, ExpressionRewriter expressionRewriter)
			: base(expressionRewriter.AsEnumerable<ExpressionRewriter>())
		{
			this._newLetBindings = newLetBindings;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00021C48 File Offset: 0x0001FE48
		public static ResolvedQueryDefinition Rewrite(ResolvedQueryDefinition query, IReadOnlyList<ResolvedQueryLetBinding> newLetBindings, IReadOnlyDictionary<ResolvedQueryDefinition, ResolvedQueryLetRefExpression> queryRewriteMapping)
		{
			ExpressionRewriter expressionRewriter = new ExpressionRewriter(queryRewriteMapping);
			CommonSubqueryEliminationQueryRewriter commonSubqueryEliminationQueryRewriter = new CommonSubqueryEliminationQueryRewriter(newLetBindings, expressionRewriter);
			expressionRewriter.SetDefinitionRewriter(commonSubqueryEliminationQueryRewriter);
			return commonSubqueryEliminationQueryRewriter.Rewrite(query);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00021C74 File Offset: 0x0001FE74
		protected override IReadOnlyList<ResolvedQueryLetBinding> RewriteLetBindings(IReadOnlyList<ResolvedQueryLetBinding> letBindings)
		{
			IReadOnlyList<ResolvedQueryLetBinding> newLetBindings = this._newLetBindings;
			this._newLetBindings = null;
			IReadOnlyList<ResolvedQueryLetBinding> readOnlyList = base.RewriteLetBindings(letBindings);
			if (newLetBindings != null)
			{
				List<ResolvedQueryLetBinding> list = new List<ResolvedQueryLetBinding>(newLetBindings.Count + readOnlyList.Count);
				list.AddRange(readOnlyList);
				list.AddRange(newLetBindings);
				return list;
			}
			return readOnlyList;
		}

		// Token: 0x04000456 RID: 1110
		private IReadOnlyList<ResolvedQueryLetBinding> _newLetBindings;
	}
}
