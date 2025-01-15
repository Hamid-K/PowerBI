using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000261 RID: 609
	internal sealed class BatchQueryDefinition
	{
		// Token: 0x06001A7E RID: 6782 RVA: 0x00049867 File Offset: 0x00047A67
		internal BatchQueryDefinition(QueryCommandTree commandTree)
		{
			this._commandTree = commandTree;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00049876 File Offset: 0x00047A76
		public QueryCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0004987E File Offset: 0x00047A7E
		public BatchQueryTranslationResult Translate(bool useConceptualSchema)
		{
			return this.Translate(CancellationToken.None, useConceptualSchema);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0004988C File Offset: 0x00047A8C
		public BatchQueryTranslationResult Translate(CancellationToken cancellationToken, bool useConceptualSchema)
		{
			return this.Translate(BatchQueryDefinition.Translator, cancellationToken, useConceptualSchema);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0004989C File Offset: 0x00047A9C
		internal BatchQueryTranslationResult Translate(CommandTreeTranslator translator, CancellationToken cancellationToken, bool useConceptualSchema)
		{
			BatchTranslationResult batchTranslationResult = translator.TranslateBatch(this._commandTree, cancellationToken, useConceptualSchema);
			IEnumerable<BatchQueryTranslationTableResult> enumerable = BatchQueryDefinition.TranslateTableResult(batchTranslationResult.Tables);
			return new BatchQueryTranslationResult(batchTranslationResult.CommandText, enumerable, batchTranslationResult.QuerySourceMap);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000498D6 File Offset: 0x00047AD6
		private static IEnumerable<BatchQueryTranslationTableResult> TranslateTableResult(IEnumerable<BatchTranslationTableResult> tableResults)
		{
			foreach (BatchTranslationTableResult batchTranslationTableResult in tableResults)
			{
				yield return new BatchQueryTranslationTableResult(BatchQueryDefinition.TranslateResultFields(batchTranslationTableResult.DataFields));
			}
			IEnumerator<BatchTranslationTableResult> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000498E6 File Offset: 0x00047AE6
		private static IEnumerable<QueryResultField> TranslateResultFields(IReadOnlyList<TranslationResultField> dataFields)
		{
			foreach (TranslationResultField translationResultField in dataFields)
			{
				yield return new QueryResultField(translationResultField.Field, translationResultField.ColumnName, translationResultField.RawUnqualifiedColumnName, null, false, translationResultField.SortInfo);
			}
			IEnumerator<TranslationResultField> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000EA4 RID: 3748
		private static readonly CommandTreeTranslator Translator = CommandTreeTranslatorFactory.CreateDaxTranslator();

		// Token: 0x04000EA5 RID: 3749
		private readonly QueryCommandTree _commandTree;
	}
}
