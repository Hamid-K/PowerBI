using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C2 RID: 194
	[DataContract]
	internal sealed class QuerySourceExpressionReferenceContext
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x0001ADB1 File Offset: 0x00018FB1
		internal QuerySourceExpressionReferenceContext(Dictionary<string, IIntermediateTableSchema> sourcesByName)
		{
			this._sourcesByName = sourcesByName;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		internal int Count
		{
			get
			{
				return this._sourcesByName.Count;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001ADCD File Offset: 0x00018FCD
		[DataMember(Name = "Sources", Order = 1)]
		private IReadOnlyList<KeyValuePair<string, IIntermediateTableSchema>> SourcesForSerialization
		{
			get
			{
				return this._sourcesByName.OrderBy((KeyValuePair<string, IIntermediateTableSchema> pair) => pair.Key).ToList<KeyValuePair<string, IIntermediateTableSchema>>();
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001ADFE File Offset: 0x00018FFE
		internal bool TryGetSourceSchema(string sourceName, out IIntermediateTableSchema schema)
		{
			return this._sourcesByName.TryGetValue(sourceName, out schema);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001AE10 File Offset: 0x00019010
		internal bool TryGetColumnInSource(ResolvedQueryExpression expression, DataShapeGenerationErrorContext errorContext, out IntermediateTableSchemaColumn columnSchema)
		{
			columnSchema = null;
			ResolvedQueryColumnReferenceExpression resolvedQueryColumnReferenceExpression = expression as ResolvedQueryColumnReferenceExpression;
			if (resolvedQueryColumnReferenceExpression == null)
			{
				return false;
			}
			ResolvedQueryExpressionSourceRefExpression resolvedQueryExpressionSourceRefExpression = resolvedQueryColumnReferenceExpression.Source as ResolvedQueryExpressionSourceRefExpression;
			if (resolvedQueryExpressionSourceRefExpression == null)
			{
				return false;
			}
			IIntermediateTableSchema intermediateTableSchema;
			if (!this.TryGetSourceSchema(resolvedQueryExpressionSourceRefExpression.SourceName, out intermediateTableSchema))
			{
				errorContext.Register(DataShapeGenerationMessages.CouldNotResolveSourceReference(EngineMessageSeverity.Error, resolvedQueryExpressionSourceRefExpression.SourceName));
				return false;
			}
			IntermediateDataShapeTableSchema intermediateDataShapeTableSchema = intermediateTableSchema as IntermediateDataShapeTableSchema;
			if (intermediateDataShapeTableSchema == null || !intermediateDataShapeTableSchema.TryGetColumn(resolvedQueryColumnReferenceExpression.SelectName, out columnSchema))
			{
				errorContext.Register(DataShapeGenerationMessages.CouldNotResolveQuerySelectName(EngineMessageSeverity.Error, resolvedQueryColumnReferenceExpression.SelectName, resolvedQueryExpressionSourceRefExpression.SourceName));
				return false;
			}
			return true;
		}

		// Token: 0x040003B4 RID: 948
		internal static readonly QuerySourceExpressionReferenceContext Empty = new QuerySourceExpressionReferenceContext(new Dictionary<string, IIntermediateTableSchema>(QueryNameComparer.Instance));

		// Token: 0x040003B5 RID: 949
		private readonly Dictionary<string, IIntermediateTableSchema> _sourcesByName;
	}
}
