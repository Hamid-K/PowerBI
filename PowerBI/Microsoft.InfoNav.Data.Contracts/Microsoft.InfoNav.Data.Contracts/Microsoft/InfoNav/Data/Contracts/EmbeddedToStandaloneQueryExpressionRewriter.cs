using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000092 RID: 146
	internal sealed class EmbeddedToStandaloneQueryExpressionRewriter : QueryExpressionRewriter
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00009657 File Offset: 0x00007857
		private EmbeddedToStandaloneQueryExpressionRewriter(IReadOnlyList<EntitySource> sources)
		{
			this._sourceNameToSource = sources.Select((EntitySource s) => Util.ToKeyValuePair<string, EntitySource>(s.Name, s)).ToDictionary(QueryNameComparer.Instance);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00009694 File Offset: 0x00007894
		internal static EmbeddedToStandaloneQueryExpressionRewriter Create(IReadOnlyList<EntitySource> sources)
		{
			return new EmbeddedToStandaloneQueryExpressionRewriter(sources);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000969C File Offset: 0x0000789C
		protected internal override QueryExpression Visit(QuerySourceRefExpression expression)
		{
			if (expression.Entity != null)
			{
				return expression;
			}
			EntitySource entitySource;
			if (string.IsNullOrEmpty(expression.Source) || !this._sourceNameToSource.TryGetValue(expression.Source, out entitySource) || string.IsNullOrEmpty(entitySource.Entity))
			{
				throw Contract.ExceptNotSupported("Non-schema-bound sources cannot be made standalone");
			}
			return new QuerySourceRefExpression
			{
				Entity = entitySource.Entity
			};
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000096FE File Offset: 0x000078FE
		protected internal override QueryExpression Visit(QuerySubqueryExpression expression)
		{
			throw Contract.ExceptNotSupported("Subquery expressions are not supported by EmbeddedToStandaloneQueryExpressionRewriter");
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000970A File Offset: 0x0000790A
		protected internal override QueryExpression Visit(QueryTransformTableRefExpression expression)
		{
			throw Contract.ExceptNotSupported("QueryTransformTableRefExpression cannot be made standalone");
		}

		// Token: 0x040001CA RID: 458
		private readonly IReadOnlyDictionary<string, EntitySource> _sourceNameToSource;
	}
}
