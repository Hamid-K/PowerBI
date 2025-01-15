using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.SparklineData
{
	// Token: 0x0200001E RID: 30
	internal sealed class ExpressionToExtensionSchemaItemQueryRewriter : IExpressionToExtensionSchemaItemQueryRewriter
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x0000554A File Offset: 0x0000374A
		internal ExpressionToExtensionSchemaItemQueryRewriter(SemanticQueryTranslatorContext context)
		{
			this._context = context;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000555C File Offset: 0x0000375C
		public bool TryRewrite(ResolvedQueryDefinition query, IQuerySchemaExtender querySchemaExtender, out ResolvedQueryDefinition newQuery, out IReadOnlyList<EngineMessageBase> errorMessages, out SparklineDataStatistics sparklineStatistics)
		{
			GuidSourceNameGenerator guidSourceNameGenerator = new GuidSourceNameGenerator();
			newQuery = SparklineDataQueryDefinitionRewriter.RewriteQuery(query, querySchemaExtender, guidSourceNameGenerator, this._context, out sparklineStatistics);
			if (this._context.ErrorContext.HasError)
			{
				errorMessages = this._context.ErrorContext.Messages;
				return false;
			}
			errorMessages = Util.EmptyReadOnlyList<EngineMessageBase>();
			return true;
		}

		// Token: 0x04000060 RID: 96
		private readonly SemanticQueryTranslatorContext _context;
	}
}
