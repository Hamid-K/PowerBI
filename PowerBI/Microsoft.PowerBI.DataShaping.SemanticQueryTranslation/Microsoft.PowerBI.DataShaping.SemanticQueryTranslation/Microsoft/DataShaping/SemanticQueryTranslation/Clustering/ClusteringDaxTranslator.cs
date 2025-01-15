using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Clustering
{
	// Token: 0x02000028 RID: 40
	internal sealed class ClusteringDaxTranslator
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00006B1C File Offset: 0x00004D1C
		internal static bool TryTranslate(SemanticQueryTranslatorContext context, ClusteringTranslationRequest request, out ClusteringTranslationResult result)
		{
			ResolvedPartitionTable partitionTable = request.PartitionTable;
			ResolvedPartitionTableDefinition definition = partitionTable.Definition;
			ResolvedPartitionTableResult partitionResult = partitionTable.Result;
			if (!ClusteringDaxTranslator.ValidatePartitionTable(context.ErrorContext, partitionTable))
			{
				result = null;
				return false;
			}
			QueryDefinitionDaxTableGeneratorResult queryDefinitionDaxTableGeneratorResult = QueryDefinitionDaxTableGenerator.Generate(definition.TableDefinition, context, ClusteringDaxTranslator.CreateGenerationOptions(definition));
			string columnName = ClusteringDaxTranslator.GetSelect(definition.PartitionIdColumn, queryDefinitionDaxTableGeneratorResult.QuerySchema).ColumnName;
			IReadOnlyList<ClusteringLookupTuple> readOnlyList = ClusteringDaxTranslator.CreateGroupedColumnsLookupValues(queryDefinitionDaxTableGeneratorResult.QuerySchema, definition.ItemIdColumns, request.GroupedColumns, context.Schema);
			IReadOnlyList<KeyValuePair<ResolvedQueryExpression, string>> readOnlyList2 = SemanticQueryTranslationUtils.ConvertDisplayNames(definition.Partitions);
			List<PartitionTableIdentityMapping>[] array = readOnlyList.Select((ClusteringLookupTuple tuple) => ClusteringDaxTranslator.CreateIdentityMapping(tuple, partitionResult.TableName)).ToList<PartitionTableIdentityMapping>().ArrayWrap<List<PartitionTableIdentityMapping>>();
			string text;
			if (!ClusteringColumnGenerator.TryGenerate(context, partitionResult.TableName, columnName, readOnlyList, readOnlyList2, definition.DefaultPartitionPrefix, out text))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.ClusteringColumnGenerationError(EngineMessageSeverity.Error));
				result = null;
				return false;
			}
			string tableName = partitionResult.TableName;
			string daxQuery = queryDefinitionDaxTableGeneratorResult.DaxQuery;
			string text2 = text;
			IReadOnlyList<IReadOnlyList<PartitionTableIdentityMapping>> readOnlyList3 = array;
			result = new ClusteringTranslationResult(tableName, daxQuery, text2, columnName, readOnlyList3);
			return true;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006C2C File Offset: 0x00004E2C
		private static DaxTableGenerationOptions CreateGenerationOptions(ResolvedPartitionTableDefinition partitionDefinition)
		{
			DataShapeGenerationOptions dataShapeGenerationOptions = new DataShapeGenerationOptions(ClusteringDaxTranslator.GenerateSelectIndicesToPreserve(partitionDefinition), true, true, false, false, false, AllowedExpressionContent.TopLevelQuerySelect);
			bool? flag = new bool?(true);
			DataShapeQueryTranslationOptions dataShapeQueryTranslationOptions = new DataShapeQueryTranslationOptions(null, flag, true, true, true, false);
			return new DaxTableGenerationOptions(dataShapeGenerationOptions, dataShapeQueryTranslationOptions);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006C70 File Offset: 0x00004E70
		private static bool ValidatePartitionTable(SemanticQueryTranslationErrorContext errorContext, ResolvedPartitionTable partitionTable)
		{
			ResolvedPartitionTableDefinition definition = partitionTable.Definition;
			string text = definition.ItemIdColumns.Single<string>();
			bool flag = false;
			bool flag2 = false;
			foreach (ResolvedQuerySelect resolvedQuerySelect in definition.TableDefinition.Select)
			{
				flag = flag || QueryNameComparer.Instance.Equals(resolvedQuerySelect.Name, text);
				flag2 = flag2 || QueryNameComparer.Instance.Equals(resolvedQuerySelect.Name, definition.PartitionIdColumn);
				if (flag && flag2)
				{
					return true;
				}
			}
			if (!flag)
			{
				errorContext.Register(SemanticQueryTranslationMessages.InvalidPartitionTableError(EngineMessageSeverity.Error, "ItemIdColumn", text));
			}
			if (!flag2)
			{
				errorContext.Register(SemanticQueryTranslationMessages.InvalidPartitionTableError(EngineMessageSeverity.Error, "PartitionIdColumn", definition.PartitionIdColumn));
			}
			return false;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006D4C File Offset: 0x00004F4C
		private static TranslatedSelect GetSelect(string selectName, TranslatedQuerySchema querySchema)
		{
			foreach (TranslatedSelect translatedSelect in querySchema.Selects)
			{
				if (QueryNameComparer.Instance.Equals(translatedSelect.Name, selectName))
				{
					return translatedSelect;
				}
			}
			Contract.RetailFail("Could not find select '{0}' in TranslatedQuerySchema.", selectName);
			return null;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006DB8 File Offset: 0x00004FB8
		private static IReadOnlyList<int> GenerateSelectIndicesToPreserve(ResolvedPartitionTableDefinition definition)
		{
			ResolvedQueryDefinition tableDefinition = definition.TableDefinition;
			Dictionary<string, int> dictionary = new Dictionary<string, int>(ConceptualNameComparer.Instance);
			for (int i = 0; i < tableDefinition.Select.Count; i++)
			{
				ResolvedQuerySelect resolvedQuerySelect = tableDefinition.Select[i];
				if (!string.IsNullOrEmpty(resolvedQuerySelect.Name))
				{
					dictionary.Add(resolvedQuerySelect.Name, i);
				}
			}
			List<int> list = new List<int>();
			foreach (string text in definition.ItemIdColumns)
			{
				list.Add(dictionary[text]);
			}
			list.Add(dictionary[definition.PartitionIdColumn]);
			return list;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006E80 File Offset: 0x00005080
		private static IReadOnlyList<ClusteringLookupTuple> CreateGroupedColumnsLookupValues(TranslatedQuerySchema querySchema, IReadOnlyList<string> itemIdColumns, IReadOnlyList<ResolvedQueryExpression> groupedColumns, IConceptualSchema schema)
		{
			Contract.RetailAssert(groupedColumns.Count == 1, "We only support one grouped column");
			Contract.RetailAssert(itemIdColumns.Count == 1, "We only support one item id column");
			ResolvedQueryExpression resolvedQueryExpression = groupedColumns[0];
			string text = itemIdColumns[0];
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = resolvedQueryExpression as ResolvedQueryColumnExpression;
			TranslatedSelect select = ClusteringDaxTranslator.GetSelect(text, querySchema);
			List<ClusteringLookupTuple> list = new List<ClusteringLookupTuple>();
			foreach (TranslatedColumn translatedColumn in select.GroupColumns)
			{
				QueryColumnExpression column = translatedColumn.Source.Column;
				Contract.RetailAssert(column != null, "Expected identity to be a QueryColumnExpression but found: {0}", translatedColumn.Source.GetType().Name);
				QuerySourceRefExpression sourceRef = column.Expression.SourceRef;
				Contract.RetailAssert(sourceRef != null, "Expected column.Expression to be a QuerySourceRefExpression but found: {0}", column.Expression.GetType().Name);
				IConceptualEntity conceptualEntity;
				if (!schema.TryGetEntity(sourceRef.Entity, out conceptualEntity))
				{
					Contract.RetailFail("Missing expected entity '{0}'", sourceRef.Entity);
				}
				IConceptualProperty conceptualProperty;
				if (!conceptualEntity.TryGetProperty(column.Property, out conceptualProperty))
				{
					Contract.RetailFail("Missing expected property '{0}'", column.Property);
				}
				IConceptualColumn conceptualColumn = (IConceptualColumn)conceptualProperty;
				ResolvedQueryColumnExpression resolvedQueryColumnExpression2 = resolvedQueryColumnExpression.Expression.Column(conceptualColumn);
				list.Add(new ClusteringLookupTuple(translatedColumn.ColumnName, resolvedQueryColumnExpression2));
			}
			return list;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006FE8 File Offset: 0x000051E8
		private static PartitionTableIdentityMapping CreateIdentityMapping(ClusteringLookupTuple tuple, string mappingTableName)
		{
			IConceptualColumn column = ((ResolvedQueryColumnExpression)tuple.ColumnExpression).Column;
			return new PartitionTableIdentityMapping
			{
				PartitionTableColumn = ClusteringDaxTranslator.CreateColumnExpression(mappingTableName, tuple.DaxColumnName),
				SourceTableColumn = ClusteringDaxTranslator.CreateColumnExpression(column.Entity.Name, column.Name)
			};
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000703B File Offset: 0x0000523B
		private static QueryExpressionContainer CreateColumnExpression(string entityName, string columnName)
		{
			return new QuerySourceRefExpression
			{
				Entity = entityName
			}.Column(columnName);
		}
	}
}
