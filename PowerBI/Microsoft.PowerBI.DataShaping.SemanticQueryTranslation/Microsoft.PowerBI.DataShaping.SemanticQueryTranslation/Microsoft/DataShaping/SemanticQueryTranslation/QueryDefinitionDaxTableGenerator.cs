using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.SemanticQueryTranslation.SparklineData;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.InfoNav.Utils;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000011 RID: 17
	internal sealed class QueryDefinitionDaxTableGenerator
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002F60 File Offset: 0x00001160
		internal static QueryDefinitionDaxTableGeneratorResult Generate(ResolvedQueryDefinition queryDefinition, SemanticQueryTranslatorContext context, DaxTableGenerationOptions generationOptions)
		{
			QueryDefinitionDaxTableGenerationTelemetry queryDefinitionDaxTableGenerationTelemetry = new QueryDefinitionDaxTableGenerationTelemetry();
			QueryDefinitionDaxTableGeneratorResult queryDefinitionDaxTableGeneratorResult;
			try
			{
				queryDefinitionDaxTableGeneratorResult = QueryDefinitionDaxTableGenerator.Generate(queryDefinition, context, generationOptions, queryDefinitionDaxTableGenerationTelemetry);
			}
			catch (Exception ex)
			{
				queryDefinitionDaxTableGenerationTelemetry.RegisterException(ex);
				throw;
			}
			finally
			{
				queryDefinitionDaxTableGenerationTelemetry.Write(context.TelemetryService, context.Tracer);
			}
			return queryDefinitionDaxTableGeneratorResult;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FBC File Offset: 0x000011BC
		internal static QueryDefinitionDaxTableGeneratorResult Generate(SemanticQueryDataShapeCommand command, SemanticQueryTranslatorContext context, DaxTableGenerationOptions generationOptions, QueryDefinitionDaxTableGenerationTelemetry telemetry)
		{
			DataShapeGenerationResult dataShapeGenerationResult = QueryDefinitionDaxTableGenerator.GenerateDsqForCommand(command, context, telemetry);
			IReadOnlyList<string> readOnlyList = command.Query.Select.Select(delegate(QueryExpressionContainer s)
			{
				if (s == null)
				{
					return null;
				}
				return s.Name;
			}).ToReadOnlyList<string>();
			return QueryDefinitionDaxTableGenerator.Translate(dataShapeGenerationResult, readOnlyList, context, generationOptions.DsqtOptions, telemetry, true);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003018 File Offset: 0x00001218
		internal static QueryDefinitionDaxTableGeneratorResult Generate(ResolvedQueryDefinition queryDefinition, SemanticQueryTranslatorContext context, DaxTableGenerationOptions generationOptions, QueryDefinitionDaxTableGenerationTelemetry telemetry)
		{
			DataShapeGenerationResult dataShapeGenerationResult = QueryDefinitionDaxTableGenerator.GenerateDsqForQuery(context, queryDefinition, generationOptions.DsqGenOptions, telemetry);
			IReadOnlyList<string> readOnlyList = queryDefinition.Select.Select(delegate(ResolvedQuerySelect s)
			{
				if (s == null)
				{
					return null;
				}
				return s.Name;
			}).ToReadOnlyList<string>();
			return QueryDefinitionDaxTableGenerator.Translate(dataShapeGenerationResult, readOnlyList, context, generationOptions.DsqtOptions, telemetry, false);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003074 File Offset: 0x00001274
		private static QueryDefinitionDaxTableGeneratorResult Translate(DataShapeGenerationResult generationResult, IReadOnlyList<string> querySelectNames, SemanticQueryTranslatorContext context, DataShapeQueryTranslationOptions translationOptions, QueryDefinitionDaxTableGenerationTelemetry telemetry, bool includeTranslatedGroup)
		{
			DataShapeQueryTranslationResult dataShapeQueryTranslationResult = QueryDefinitionDaxTableGenerator.TranslateDsq(context, generationResult, translationOptions, telemetry);
			DataShapeDefinition unifiedDataShapeDefinition = dataShapeQueryTranslationResult.UnifiedDataShapeDefinition;
			DataSet dataSet = unifiedDataShapeDefinition.DataSets[0];
			string query = dataSet.Query;
			TranslatedQuerySchema translatedQuerySchema = QueryDefinitionDaxTableGenerator.BuildTranslatedQuerySchema(generationResult.BindingDescriptor, generationResult.InternalSchema, unifiedDataShapeDefinition.DataShape, querySelectNames, dataSet, includeTranslatedGroup);
			IReadOnlyList<EngineMessageBase> readOnlyList = QueryDefinitionDaxTableGenerator.CombineMessages(context.ErrorContext, generationResult.ErrorContext, dataShapeQueryTranslationResult.ErrorContext);
			return new QueryDefinitionDaxTableGeneratorResult(query, translatedQuerySchema, readOnlyList);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030E4 File Offset: 0x000012E4
		private static IReadOnlyList<EngineMessageBase> CombineMessages(SemanticQueryTranslationErrorContext errorContext, DataShapeGenerationErrorContext generationErrorContext, TranslationErrorContext translationErrorContext)
		{
			List<EngineMessageBase> list = null;
			QueryDefinitionDaxTableGenerator.AddMessages<SemanticQueryTranslationMessage>(ref list, errorContext);
			QueryDefinitionDaxTableGenerator.AddMessages<DataShapeGenerationMessage>(ref list, generationErrorContext);
			QueryDefinitionDaxTableGenerator.AddMessages<TranslationMessage>(ref list, translationErrorContext);
			return list;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000310C File Offset: 0x0000130C
		private static void AddMessages<T>(ref List<EngineMessageBase> messages, EngineErrorContextBase<T> errorContext) where T : EngineMessageBase
		{
			if (errorContext.HasMessage)
			{
				Util.AddToLazyList<EngineMessageBase>(ref messages, errorContext.Messages);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003124 File Offset: 0x00001324
		private static DataShapeGenerationResult GenerateDsqForQuery(SemanticQueryTranslatorContext context, ResolvedQueryDefinition queryDefinition, DataShapeGenerationOptions dsqGenOptions, QueryDefinitionDaxTableGenerationTelemetry telemetry)
		{
			telemetry.DataShapeGeneration = new DataShapeGenerationTelemetry();
			DataShapeGenerationContext dataShapeGenerationContext = QueryDefinitionDaxTableGenerator.CreateContextForDsqGeneration(context, telemetry, false);
			return DataShapeQueryGeneratorAdapter.Instance.GenerateDataShapeFromQuery(dataShapeGenerationContext, queryDefinition, dsqGenOptions);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003154 File Offset: 0x00001354
		private static DataShapeGenerationResult GenerateDsqForCommand(SemanticQueryDataShapeCommand command, SemanticQueryTranslatorContext context, QueryDefinitionDaxTableGenerationTelemetry telemetry)
		{
			telemetry.DataShapeGeneration = new DataShapeGenerationTelemetry();
			DataShapeGenerationContext dataShapeGenerationContext = QueryDefinitionDaxTableGenerator.CreateContextForDsqGeneration(context, telemetry, true);
			return DataShapeQueryGeneratorAdapter.Instance.GenerateDataShapeFromCommand(dataShapeGenerationContext, command, null, null);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003184 File Offset: 0x00001384
		private static DataShapeGenerationContext CreateContextForDsqGeneration(SemanticQueryTranslatorContext context, QueryDefinitionDaxTableGenerationTelemetry telemetry, bool allowQueryParameters = false)
		{
			return new DataShapeGenerationContext(context.Tracer, context.TelemetryService, context.FeatureSwitchProvider, context.Dumper, context.Schema, telemetry.DataShapeGeneration, new ExpressionToExtensionSchemaItemQueryRewriter(context), 0, true, true, allowQueryParameters, false);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031C8 File Offset: 0x000013C8
		private static DataShapeQueryTranslationResult TranslateDsq(SemanticQueryTranslatorContext context, DataShapeGenerationResult generationResult, DataShapeQueryTranslationOptions dsqtOptions, QueryDefinitionDaxTableGenerationTelemetry telemetry)
		{
			DataSourceContext dataSourceContext = new DataSourceContext(context.DataSourceName, context.Model, generationResult.FederatedConceptualSchema);
			generationResult.DataShape.DataSourceId = context.DataSourceName;
			telemetry.DataShapeQueryTranslation = new DataShapeQueryTranslationTelemetry();
			DataShapeQueryTranslationContext dataShapeQueryTranslationContext = new DataShapeQueryTranslationContext(generationResult.DataShape, context.Tracer, context.TelemetryService, context.FeatureSwitchProvider, context.Dumper, dataSourceContext, null, telemetry.DataShapeQueryTranslation, context.CancellationToken, dsqtOptions, context.TransformMetadataFactory);
			return new DataShapeQueryTranslator().Translate(dataShapeQueryTranslationContext);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000325C File Offset: 0x0000145C
		private static TranslatedQuerySchema BuildTranslatedQuerySchema(QueryBindingDescriptor queryBindingDescriptor, IntermediateDataShapeTableSchema intermediateTableSchema, DataShape dataShape, IReadOnlyList<string> querySelectNames, DataSet dataSet, bool includeTranslatedGroups)
		{
			Dictionary<string, string> dictionary;
			List<TranslatedSelect> list = QueryDefinitionDaxTableGenerator.TranslateSelects(queryBindingDescriptor, intermediateTableSchema, dataShape, querySelectNames, dataSet, out dictionary);
			List<TranslatedParameter> list2 = QueryDefinitionDaxTableGenerator.TranslateQueryParameters(dataSet);
			TranslatedGroups translatedGroups = (includeTranslatedGroups ? QueryDefinitionDaxTableGenerator.TranslateGroups(queryBindingDescriptor, dataShape, dictionary) : null);
			return new TranslatedQuerySchema
			{
				Selects = list,
				Parameters = list2,
				Groups = translatedGroups
			};
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000032AC File Offset: 0x000014AC
		private static List<TranslatedSelect> TranslateSelects(QueryBindingDescriptor queryBindingDescriptor, IntermediateDataShapeTableSchema intermediateTableSchema, DataShape dataShape, IReadOnlyList<string> querySelectNames, DataSet dataSet, out Dictionary<string, string> fieldToColumnMapping)
		{
			ResultTable resultTable = dataSet.ResultTables[0];
			Dictionary<string, ExpressionNode> dictionary = DataShapeDefinitionCalculationCollector.MapCalculations(dataShape);
			fieldToColumnMapping = QueryDefinitionDaxTableGenerator.BuildFieldToColumnMapping(resultTable);
			SelectBinding[] select = queryBindingDescriptor.Select;
			List<TranslatedSelect> list = new List<TranslatedSelect>(select.Length);
			for (int i = 0; i < select.Length; i++)
			{
				SelectBinding selectBinding = select[i];
				TranslatedSelect translatedSelect = new TranslatedSelect();
				if (selectBinding != null)
				{
					string value = selectBinding.Value;
					if (value != null)
					{
						translatedSelect.ColumnName = QueryDefinitionDaxTableGenerator.GetColumnName(dictionary, fieldToColumnMapping, value);
					}
					translatedSelect.GroupColumns = QueryDefinitionDaxTableGenerator.TranslateGroupKeys(selectBinding.GroupKeys, dictionary, fieldToColumnMapping);
					translatedSelect.SortColumns = QueryDefinitionDaxTableGenerator.TranslateSortKeys(intermediateTableSchema.Columns[i], dictionary, fieldToColumnMapping);
					translatedSelect.DynamicFormat = QueryDefinitionDaxTableGenerator.TranslateDynamicFormat(selectBinding.DynamicFormat, dictionary, fieldToColumnMapping);
				}
				if (i < querySelectNames.Count)
				{
					translatedSelect.Name = querySelectNames[i];
				}
				list.Add(translatedSelect);
			}
			return list;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000339C File Offset: 0x0000159C
		private static List<TranslatedColumn> TranslateGroupKeys(IReadOnlyList<SelectIdentityKey> groupKeys, Dictionary<string, ExpressionNode> calculationMapping, Dictionary<string, string> fieldToColumnMapping)
		{
			if (groupKeys.IsNullOrEmpty<SelectIdentityKey>())
			{
				return null;
			}
			List<TranslatedColumn> list = new List<TranslatedColumn>(groupKeys.Count);
			foreach (SelectIdentityKey selectIdentityKey in groupKeys)
			{
				string columnName = QueryDefinitionDaxTableGenerator.GetColumnName(calculationMapping, fieldToColumnMapping, selectIdentityKey.Calc);
				TranslatedColumn translatedColumn = new TranslatedColumn
				{
					ColumnName = columnName,
					Source = QueryDefinitionDaxTableGenerator.CreateExpressionFromOptionalPropertyReference(selectIdentityKey.Source)
				};
				list.Add(translatedColumn);
			}
			return list;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000342C File Offset: 0x0000162C
		private static List<TranslatedColumn> TranslateSortKeys(IntermediateTableSchemaColumn intermediateTableSchemaColumn, Dictionary<string, ExpressionNode> calculationMapping, Dictionary<string, string> fieldToColumnMapping)
		{
			if (intermediateTableSchemaColumn == null || intermediateTableSchemaColumn.SortKeys.IsNullOrEmpty<IntermediateTableSchemaKey>())
			{
				return null;
			}
			List<TranslatedColumn> list = new List<TranslatedColumn>(intermediateTableSchemaColumn.SortKeys.Count);
			foreach (IntermediateTableSchemaKey intermediateTableSchemaKey in intermediateTableSchemaColumn.SortKeys)
			{
				string columnName = QueryDefinitionDaxTableGenerator.GetColumnName(calculationMapping, fieldToColumnMapping, intermediateTableSchemaKey.ValueCalculationId);
				TranslatedColumn translatedColumn = new TranslatedColumn
				{
					ColumnName = columnName,
					Source = QueryDefinitionDaxTableGenerator.CreateExpressionFromOptionalColumn(intermediateTableSchemaKey.Source)
				};
				list.Add(translatedColumn);
			}
			return list;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000034CC File Offset: 0x000016CC
		private static TranslatedDynamicFormat TranslateDynamicFormat(DynamicFormatBinding dynamicFormatBinding, Dictionary<string, ExpressionNode> calculationMapping, Dictionary<string, string> fieldToColumnMapping)
		{
			if (dynamicFormatBinding == null)
			{
				return null;
			}
			TranslatedDynamicFormat translatedDynamicFormat = new TranslatedDynamicFormat();
			if (!string.IsNullOrEmpty(dynamicFormatBinding.Format))
			{
				translatedDynamicFormat.Format = QueryDefinitionDaxTableGenerator.GetColumnName(calculationMapping, fieldToColumnMapping, dynamicFormatBinding.Format);
			}
			if (!string.IsNullOrEmpty(dynamicFormatBinding.Culture))
			{
				translatedDynamicFormat.Culture = QueryDefinitionDaxTableGenerator.GetColumnName(calculationMapping, fieldToColumnMapping, dynamicFormatBinding.Culture);
			}
			return translatedDynamicFormat;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003525 File Offset: 0x00001725
		private static QueryExpressionContainer CreateExpressionFromOptionalPropertyReference(ConceptualPropertyReference propertyRef)
		{
			if (propertyRef == null)
			{
				return null;
			}
			return propertyRef.Schema.SourceRef(propertyRef.Entity).Column(propertyRef.Property);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000354D File Offset: 0x0000174D
		private static QueryExpressionContainer CreateExpressionFromOptionalColumn(IConceptualColumn conceptualColumn)
		{
			if (conceptualColumn == null)
			{
				return null;
			}
			return conceptualColumn.Entity.SourceRef().Column(conceptualColumn);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000356C File Offset: 0x0000176C
		private static string GetColumnName(Dictionary<string, ExpressionNode> calculationMapping, Dictionary<string, string> fieldToColumnMapping, string calculationId)
		{
			ExpressionNode expressionNode;
			if (!calculationMapping.TryGetValue(calculationId, out expressionNode))
			{
				Contract.RetailFail("Could not find calculation with Id {0}", calculationId);
			}
			FieldValueExpressionNode fieldValueExpressionNode = (FieldValueExpressionNode)expressionNode;
			string text;
			if (!fieldToColumnMapping.TryGetValue(fieldValueExpressionNode.FieldId, out text))
			{
				Contract.RetailFail("Could not find field with Id {0}", fieldValueExpressionNode.FieldId);
			}
			return text;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000035B8 File Offset: 0x000017B8
		private static Dictionary<string, string> BuildFieldToColumnMapping(ResultTable resultTable)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			foreach (Field field in resultTable.Fields)
			{
				if (field.DataField != null)
				{
					dictionary.Add(field.Id, field.DataField);
				}
			}
			return dictionary;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003624 File Offset: 0x00001824
		private static List<TranslatedParameter> TranslateQueryParameters(DataSet dataSet)
		{
			IList<QueryParameter> queryParameters = dataSet.QueryParameters;
			if (queryParameters.IsNullOrEmpty<QueryParameter>())
			{
				return null;
			}
			List<TranslatedParameter> list = new List<TranslatedParameter>(queryParameters.Count);
			foreach (QueryParameter queryParameter in queryParameters)
			{
				list.Add(new TranslatedParameter
				{
					Name = queryParameter.Name,
					TranslatedName = queryParameter.QueryName
				});
			}
			return list;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000036A8 File Offset: 0x000018A8
		private static TranslatedGroups TranslateGroups(QueryBindingDescriptor queryBindingDescriptor, DataShape dataShape, IDictionary<string, string> fieldToColumnMapping)
		{
			TranslatedGroups translatedGroups = null;
			DataShapeExpressions expressions = queryBindingDescriptor.Expressions;
			List<TranslatedGroup> list = QueryDefinitionDaxTableGenerator.TranslateGroupsOfHierarchy((expressions != null) ? expressions.Primary : null, dataShape.PrimaryHierarchy, fieldToColumnMapping).ToList<TranslatedGroup>();
			DataShapeExpressions expressions2 = queryBindingDescriptor.Expressions;
			List<TranslatedGroup> list2 = QueryDefinitionDaxTableGenerator.TranslateGroupsOfHierarchy((expressions2 != null) ? expressions2.Secondary : null, dataShape.SecondaryHierarchy, fieldToColumnMapping).ToList<TranslatedGroup>();
			if (list.Count > 0 || list2.Count > 0)
			{
				translatedGroups = new TranslatedGroups
				{
					Primary = ((list.Count == 0) ? null : list),
					Secondary = ((list2.Count == 0) ? null : list2)
				};
			}
			return translatedGroups;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000373C File Offset: 0x0000193C
		private static IEnumerable<TranslatedGroup> TranslateGroupsOfHierarchy(DataShapeExpressionsAxis axis, IEnumerable<DataMember> dataShapeHierarchy, IDictionary<string, string> fieldToColumnMapping)
		{
			if (((axis != null) ? axis.Groupings : null) != null)
			{
				Dictionary<string, DataMember> dataMemberDictionary = new Dictionary<string, DataMember>(StringComparer.Ordinal);
				QueryDefinitionDaxTableGenerator.CollectDataMembers(dataShapeHierarchy, dataMemberDictionary);
				foreach (DataShapeExpressionsAxisGrouping dataShapeExpressionsAxisGrouping in axis.Groupings)
				{
					TranslatedGroup translatedGroup = new TranslatedGroup();
					if (!string.IsNullOrEmpty(dataShapeExpressionsAxisGrouping.SubtotalMember))
					{
						DataMember dataMember;
						if (!dataMemberDictionary.TryGetValue(dataShapeExpressionsAxisGrouping.SubtotalMember, out dataMember))
						{
							Contract.RetailFail("Could not find subtotal member with Id {0}", dataShapeExpressionsAxisGrouping.SubtotalMember.MarkAsCustomerContent());
						}
						MatchCondition matchCondition = dataMember.MatchCondition;
						string text = ((matchCondition != null) ? matchCondition.Field.FieldId : null);
						if (text != null)
						{
							string text2;
							if (!fieldToColumnMapping.TryGetValue(text, out text2))
							{
								Contract.RetailFail("Could not find field with ID {0}", text.MarkAsCustomerContent());
							}
							translatedGroup.SubtotalIndicatorColumnName = text2;
						}
					}
					yield return translatedGroup;
				}
				IEnumerator<DataShapeExpressionsAxisGrouping> enumerator = null;
				dataMemberDictionary = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000375C File Offset: 0x0000195C
		private static void CollectDataMembers(IEnumerable<DataMember> dataMembers, IDictionary<string, DataMember> dataMemberDictionary)
		{
			if (dataMembers != null)
			{
				foreach (DataMember dataMember in dataMembers)
				{
					dataMemberDictionary.Add(dataMember.Id, dataMember);
					QueryDefinitionDaxTableGenerator.CollectDataMembers(dataMember.DataMembers, dataMemberDictionary);
				}
			}
		}
	}
}
