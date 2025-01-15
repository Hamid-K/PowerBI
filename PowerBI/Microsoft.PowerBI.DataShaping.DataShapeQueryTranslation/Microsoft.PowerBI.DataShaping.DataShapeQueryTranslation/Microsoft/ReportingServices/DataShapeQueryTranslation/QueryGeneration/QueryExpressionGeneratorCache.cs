using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000081 RID: 129
	internal sealed class QueryExpressionGeneratorCache
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00016121 File Offset: 0x00014321
		internal QueryExpressionGeneratorCache()
		{
			this.m_generatedExpressionsCache = new Dictionary<ExpressionId, QueryExpressionContext>();
			this.m_generatedCompoundExpressionsCache = new Dictionary<ExpressionId, List<ExpressionId>>();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001613F File Offset: 0x0001433F
		internal void PutExpression(ExpressionId key, QueryExpressionContext context)
		{
			this.m_generatedExpressionsCache[key] = context;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001614E File Offset: 0x0001434E
		internal bool TryGetExpression(ExpressionId key, out QueryExpressionContext context)
		{
			return this.m_generatedExpressionsCache.TryGetValue(key, out context);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00016160 File Offset: 0x00014360
		internal void PutCompoundExpression(ExpressionId expressionId, List<KeyValuePair<ExpressionId, QueryExpressionContext>> generatedExpressions)
		{
			if (generatedExpressions == null || generatedExpressions.Count == 0)
			{
				return;
			}
			List<ExpressionId> list = new List<ExpressionId>(generatedExpressions.Count);
			foreach (KeyValuePair<ExpressionId, QueryExpressionContext> keyValuePair in generatedExpressions)
			{
				this.PutExpression(keyValuePair.Key, keyValuePair.Value);
				list.Add(keyValuePair.Key);
			}
			this.m_generatedCompoundExpressionsCache.Add(expressionId, list);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000161F0 File Offset: 0x000143F0
		internal bool TryGetCompoundExpression(ExpressionId expressionId, out List<KeyValuePair<ExpressionId, QueryExpressionContext>> generatedExpressions)
		{
			List<ExpressionId> list;
			if (!this.m_generatedCompoundExpressionsCache.TryGetValue(expressionId, out list))
			{
				generatedExpressions = null;
				return false;
			}
			generatedExpressions = new List<KeyValuePair<ExpressionId, QueryExpressionContext>>(list.Count);
			foreach (ExpressionId expressionId2 in list)
			{
				QueryExpressionContext queryExpressionContext;
				if (!this.TryGetExpression(expressionId2, out queryExpressionContext))
				{
					Contract.RetailFail("Expected QueryExpressionContext to be in the cache");
				}
				generatedExpressions.Add(Util.ToKeyValuePair<ExpressionId, QueryExpressionContext>(expressionId2, queryExpressionContext));
			}
			return true;
		}

		// Token: 0x04000311 RID: 785
		private readonly Dictionary<ExpressionId, QueryExpressionContext> m_generatedExpressionsCache;

		// Token: 0x04000312 RID: 786
		private readonly Dictionary<ExpressionId, List<ExpressionId>> m_generatedCompoundExpressionsCache;
	}
}
