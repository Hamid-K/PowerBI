using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x02000120 RID: 288
	internal abstract class DsdGeneratorBase
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x000299F4 File Offset: 0x00027BF4
		protected DsdGeneratorBase(DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, DataSourceContext dataSourceContext, bool applyTransformsInQuery, bool useConceptualSchema)
		{
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_applyTransformsInQuery = applyTransformsInQuery;
			this.m_useConceptualSchema = useConceptualSchema;
			this.m_idContext = new DsdGeneratorIdContext();
			this.m_segmentationItemsToTableMapping = new Dictionary<IContextItem, string>();
			this.m_limitMetadata = new Dictionary<Identifier, DsdLimitMetadata>();
			this.m_dataSource = this.BuildDataSource(dataSourceContext);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00029A5C File Offset: 0x00027C5C
		protected DataShapeDefinition Generate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			return new DataShapeDefinition
			{
				DataSource = this.m_dataSource,
				DataSets = this.BuildDataSets(dataShape.ExtensionSchema),
				DataTransforms = this.BuildDataTransforms(dataShape.Transforms),
				DataShape = this.BuildDataShape(dataShape),
				ResultEncodingHints = this.BuildResultEncodingHints()
			};
		}

		// Token: 0x06000AB3 RID: 2739
		protected abstract Collation BuildCollation(DataSourceContext dataSourceContext);

		// Token: 0x06000AB4 RID: 2740
		protected abstract IList<DataSet> BuildDataSets(ExtensionSchema extensionSchema);

		// Token: 0x06000AB5 RID: 2741
		protected abstract DataBinding BuildDataBinding(IDataBoundItem item);

		// Token: 0x06000AB6 RID: 2742
		protected abstract Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildExpressionForCalculation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation calculation);

		// Token: 0x06000AB7 RID: 2743
		protected abstract FieldValueExpressionNode BuildIntersectionCorrelation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape);

		// Token: 0x06000AB8 RID: 2744
		protected abstract MatchCondition BuildMatchCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember);

		// Token: 0x06000AB9 RID: 2745
		protected abstract DiscardCondition BuildDiscardCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember);

		// Token: 0x06000ABA RID: 2746
		protected abstract RestartKindDefinition BuildRestartKindDefinition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember);

		// Token: 0x06000ABB RID: 2747
		protected abstract ResultEncodingHints BuildResultEncodingHints();

		// Token: 0x06000ABC RID: 2748
		protected abstract ExpressionTable GetExpressionTableForLimits();

		// Token: 0x06000ABD RID: 2749
		protected abstract ExpressionTable GetExpressionTableForLimitOverrides();

		// Token: 0x06000ABE RID: 2750
		protected abstract IList<LimitOverride> GetLimitOverrides();

		// Token: 0x06000ABF RID: 2751
		protected abstract IList<LimitTelemetryItem> GetLimitTelemetry();

		// Token: 0x06000AC0 RID: 2752
		protected abstract ExpressionTable GetExpressionTableForMember(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember member);

		// Token: 0x06000AC1 RID: 2753
		protected abstract Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode GetRestartExpressionForStatic(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember, ExpressionTable expressionTable, ObjectType objectType);

		// Token: 0x06000AC2 RID: 2754
		protected abstract string GetDataSetTableId(DataSetFieldReferenceExpressionNode exprNode);

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00029AB8 File Offset: 0x00027CB8
		protected string GetTransformTableId(DataTransformTable table)
		{
			string text;
			if (!this.m_idContext.TryGetId(table, out text))
			{
				Microsoft.DataShaping.Contract.RetailFail("Missing Id for transform table");
			}
			return text;
		}

		// Token: 0x06000AC4 RID: 2756
		protected abstract bool HasReusableSecondary(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape);

		// Token: 0x06000AC5 RID: 2757
		protected abstract IList<string> BuildSegmentationTableIds(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape);

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00029AE0 File Offset: 0x00027CE0
		protected virtual CorrelationMode? BuildCorrelationMode(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			return null;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00029AF8 File Offset: 0x00027CF8
		private ScopeKey BuildScopeKey(ScopeValueDefinition value, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			ExpressionTable expressionTableForMember = this.GetExpressionTableForMember(dataMember);
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = this.BuildExpression(value.Value.ExpressionId, expressionTableForMember);
			return new ScopeKey
			{
				Value = expressionNode
			};
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00029B2C File Offset: 0x00027D2C
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey BuildGroupKey(Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			return this.BuildGroupKey(groupKey.Value, dataMember);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00029B3C File Offset: 0x00027D3C
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey BuildGroupKey(Expression expression, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			ExpressionTable expressionTableForMember = this.GetExpressionTableForMember(dataMember);
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = this.BuildExpression(expression.ExpressionId, expressionTableForMember);
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey
			{
				Value = expressionNode
			};
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00029B6C File Offset: 0x00027D6C
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataSource BuildDataSource(DataSourceContext dataSourceContext)
		{
			if (dataSourceContext == null)
			{
				return null;
			}
			string text = this.m_idContext.MakeUniqueId(dataSourceContext.DataSourceName, dataSourceContext);
			Collation collation = this.BuildCollation(dataSourceContext);
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataSource
			{
				Id = text,
				DataSourceName = dataSourceContext.DataSourceName,
				Collation = collation
			};
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00029BB8 File Offset: 0x00027DB8
		protected Collation BuildCollationBasicProperties(DataSourceContext dataSourceContext, out bool preferOrdinalStringEquality)
		{
			CompareOptions compareOptions;
			string text;
			if (!this.m_useConceptualSchema)
			{
				EntityDataModel model = dataSourceContext.Model;
				compareOptions = model.CompareOptions;
				text = model.Culture;
				preferOrdinalStringEquality = model.PreferOrdinalStringEquality;
			}
			else
			{
				ConceptualCollation conceptualCollation = dataSourceContext.FederatedConceptualSchema.GetDefaultSchema().ConceptualCollation;
				compareOptions = conceptualCollation.CompareOptions;
				text = conceptualCollation.Culture;
				preferOrdinalStringEquality = conceptualCollation.PreferOrdinalStringEquality;
			}
			return new Collation
			{
				Culture = text,
				IgnoreCase = compareOptions.HasFlag(CompareOptions.IgnoreCase),
				IgnoreNonSpace = compareOptions.HasFlag(CompareOptions.IgnoreNonSpace),
				IgnoreKanaType = compareOptions.HasFlag(CompareOptions.IgnoreKanaType),
				IgnoreWidth = compareOptions.HasFlag(CompareOptions.IgnoreWidth)
			};
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00029C7C File Offset: 0x00027E7C
		private IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform> BuildDataTransforms(List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransform> dsqTransforms)
		{
			if (this.m_applyTransformsInQuery)
			{
				return null;
			}
			return this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransform, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform>(dsqTransforms, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransform, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform>(this.BuildDataTransform));
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00029C9C File Offset: 0x00027E9C
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform BuildDataTransform(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransform dsqTransform)
		{
			TransformInputTableMapping transformInputTableMapping;
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformInput dataTransformInput = this.BuildDataTransformInput(dsqTransform.Input, out transformInputTableMapping);
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformOutput dataTransformOutput = this.BuildDataTransformOutput(dsqTransform.Output, transformInputTableMapping);
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform
			{
				Id = this.EnsureUniqueId(dsqTransform),
				Algorithm = dsqTransform.Algorithm.Value,
				Input = dataTransformInput,
				Output = dataTransformOutput
			};
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00029CF8 File Offset: 0x00027EF8
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformInput BuildDataTransformInput(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransformInput dsqInput, out TransformInputTableMapping inputTableMapping)
		{
			string text;
			DataTransformTableSchema dataTransformTableSchema = this.BuildDataTransformTableSchema(dsqInput.Table, out text, out inputTableMapping);
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformInput
			{
				TableId = text,
				Schema = dataTransformTableSchema,
				Parameters = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransformParameter, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformParameter>(dsqInput.Parameters, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransformParameter, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformParameter>(this.BuildDataTransformParameter))
			};
		}

		// Token: 0x06000ACF RID: 2767
		protected abstract ExpressionTable GetExpressionTableForTransforms();

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00029D48 File Offset: 0x00027F48
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformParameter BuildDataTransformParameter(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransformParameter dsqParam)
		{
			ExpressionTable expressionTableForTransforms = this.GetExpressionTableForTransforms();
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = this.BuildExpression(dsqParam.Value.ExpressionId, expressionTableForTransforms);
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformParameter
			{
				Name = dsqParam.Id.Value,
				Value = expressionNode
			};
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00029D8C File Offset: 0x00027F8C
		private DataTransformTableSchema BuildDataTransformTableSchema(DataTransformTable dsqTable, out string inputTableId, out TransformInputTableMapping transformInputTableMapping)
		{
			inputTableId = null;
			Dictionary<Identifier, string> dictionary = new Dictionary<Identifier, string>();
			List<DataTransformTableColumn> columns = dsqTable.Columns;
			List<DataTransformColumn> list = new List<DataTransformColumn>(columns.Count);
			foreach (DataTransformTableColumn dataTransformTableColumn in columns)
			{
				string text;
				string text2;
				DataTransformColumn dataTransformColumn = this.BuildDataTransformColumn(dataTransformTableColumn, out text, out text2);
				Microsoft.DataShaping.Contract.RetailAssert(inputTableId == null || inputTableId == text, "All columns must come from the same input table");
				inputTableId = text;
				dictionary.Add(dataTransformTableColumn.Id, text2);
				if (dataTransformColumn != null)
				{
					list.Add(dataTransformColumn);
				}
			}
			transformInputTableMapping = new TransformInputTableMapping(dsqTable, dictionary);
			return new DataTransformTableSchema
			{
				Columns = list
			};
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00029E48 File Offset: 0x00028048
		private DataTransformColumn BuildDataTransformColumn(DataTransformTableColumn dsqColumn, out string inputTableId, out string dsdColumnName)
		{
			ExpressionTable expressionTableForTransforms = this.GetExpressionTableForTransforms();
			FieldValueExpressionNode fieldValueExpressionNode = this.BuildExpression(dsqColumn.Value.ExpressionId, expressionTableForTransforms) as FieldValueExpressionNode;
			Microsoft.DataShaping.Contract.RetailAssert(fieldValueExpressionNode != null, "Column expression should have translated to a field reference.");
			inputTableId = fieldValueExpressionNode.TableId;
			dsdColumnName = fieldValueExpressionNode.FieldId;
			if (string.IsNullOrEmpty(dsqColumn.Role.GetValueOrDefault<string>()))
			{
				return null;
			}
			return new DataTransformColumn
			{
				Name = dsdColumnName,
				Role = dsqColumn.Role.GetValueOrDefault<string>()
			};
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00029EC4 File Offset: 0x000280C4
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformOutput BuildDataTransformOutput(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataTransformOutput dsqOutput, TransformInputTableMapping inputTableMapping)
		{
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformOutput
			{
				Table = this.BuildDataTransformResultTable(dsqOutput.Table, inputTableMapping)
			};
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00029EDE File Offset: 0x000280DE
		private ResultTable BuildDataTransformResultTable(DataTransformTable dsqTable, TransformInputTableMapping inputTableMapping)
		{
			return new ResultTable
			{
				Id = this.EnsureUniqueId(dsqTable),
				IsReusable = false,
				Fields = this.BuildDataTransformResultTableFields(dsqTable.Columns, inputTableMapping)
			};
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00029F0C File Offset: 0x0002810C
		private IList<Field> BuildDataTransformResultTableFields(List<DataTransformTableColumn> columns, TransformInputTableMapping inputTableMapping)
		{
			if (columns == null)
			{
				return null;
			}
			int num = -1;
			List<Field> list = new List<Field>(columns.Count);
			foreach (DataTransformTableColumn dataTransformTableColumn in columns)
			{
				Field field = this.BuildDataTransformResultTableField(dataTransformTableColumn, inputTableMapping);
				if (field != null)
				{
					if (field.TargetRole != null)
					{
						if (num == -1)
						{
							num = list.Count;
						}
						list.Add(field);
					}
					else if (num != -1)
					{
						list.Insert(num, field);
						num++;
					}
					else
					{
						list.Add(field);
					}
				}
			}
			return list;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00029FB0 File Offset: 0x000281B0
		private Field BuildDataTransformResultTableField(DataTransformTableColumn dsqColumn, TransformInputTableMapping inputTableMapping)
		{
			string text = null;
			string text2 = null;
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode node = this.GetExpressionTableForTransforms().GetNode(dsqColumn.Value);
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.FunctionCallExpressionNode functionCallExpressionNode = node as Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.BackingFunctionName == "TransformOutputRoleRef")
			{
				IComparable value = (functionCallExpressionNode.Arguments[0] as Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.LiteralExpressionNode).Value.Value;
				Microsoft.DataShaping.Contract.RetailAssert(value is string, "Accepting only string types for targetRole");
				text2 = value as string;
			}
			else
			{
				ResolvedDataTransformTableColumnReferenceExpressionNode resolvedDataTransformTableColumnReferenceExpressionNode = node as ResolvedDataTransformTableColumnReferenceExpressionNode;
				Microsoft.DataShaping.Contract.RetailAssert(resolvedDataTransformTableColumnReferenceExpressionNode != null, "Unexpected output column expression node type: {0}", resolvedDataTransformTableColumnReferenceExpressionNode.GetType().Name);
				Microsoft.DataShaping.Contract.RetailAssert(inputTableMapping != null && inputTableMapping.Table == resolvedDataTransformTableColumnReferenceExpressionNode.Table, "Output column expression referred to unexpected table: {0}", resolvedDataTransformTableColumnReferenceExpressionNode.Table.Id.Value);
				if (!inputTableMapping.TryGetDataFieldForColumn(resolvedDataTransformTableColumnReferenceExpressionNode.Column.Id, out text))
				{
					Microsoft.DataShaping.Contract.RetailFail("Output column expression referred to unknown input column: {0}", resolvedDataTransformTableColumnReferenceExpressionNode.Column.Id.Value);
				}
			}
			return new Field
			{
				Id = dsqColumn.Id.Value,
				DataField = text,
				TargetRole = text2
			};
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002A0D9 File Offset: 0x000282D9
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape BuildDataShape(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			if (dataShape.ContextOnly.GetValueOrDefault<bool>())
			{
				return null;
			}
			return this.BuildSingleDataShape(dataShape);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002A0F4 File Offset: 0x000282F4
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape BuildSingleDataShape(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			List<DataWindow> list;
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape dataShape2 = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape
			{
				Id = this.EnsureUniqueId(dataShape),
				HasReusableSecondary = this.HasReusableSecondary(dataShape),
				DataBinding = this.BuildDataBinding(dataShape),
				DataLimits = this.BuildDataLimits(dataShape, out list),
				CorrelationExpression = this.BuildIntersectionCorrelation(dataShape),
				Calculations = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation>(dataShape.Calculations, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation>(this.BuildCalculation)),
				SecondaryHierarchy = this.BuildHierarchy(dataShape.SecondaryHierarchy, null),
				PrimaryHierarchy = this.BuildHierarchy(dataShape.PrimaryHierarchy, dataShape.DataRows),
				DataShapes = this.BuildDataShapes(dataShape),
				DataWindows = this.BuildDataWindows(dataShape.Limits, dataShape.DynamicLimits, list),
				DataWindow = this.BuildLegacyDataWindow(dataShape),
				RestartDefinitions = this.BuildLegacyRestartDefinitions(dataShape),
				SegmentationTableIds = this.BuildSegmentationTableIds(dataShape),
				Messages = dataShape.Messages
			};
			CorrelationMode? correlationMode = this.BuildCorrelationMode(dataShape);
			if (correlationMode != null)
			{
				dataShape2.CorrelationMode = correlationMode.Value;
			}
			if (this.m_errorContext.HasMessage)
			{
				IEnumerable<Message> enumerable = this.BuildCollection<TranslationMessage, Message>(this.m_errorContext.Messages, new Func<TranslationMessage, Message>(this.BuildMessage));
				if (dataShape2.Messages == null)
				{
					dataShape2.Messages = new List<Message>();
				}
				foreach (Message message in enumerable)
				{
					dataShape2.Messages.Add(message);
				}
			}
			return dataShape2;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002A28C File Offset: 0x0002848C
		private IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember> BuildHierarchy(DataHierarchy dataHierarchy, List<DataRow> rows)
		{
			if (dataHierarchy == null)
			{
				return null;
			}
			return this.BuildDataMembers(dataHierarchy.DataMembers, rows);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002A2A0 File Offset: 0x000284A0
		private IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape> BuildDataShapes(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			string currentTableBinding = this.m_currentTableBinding;
			if (dataShape.DataShapes != null)
			{
				if (dataShape.DataShapes.Any((Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape ds) => !ds.ContextOnly.GetValueOrDefault<bool>()))
				{
					return this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape>(dataShape.DataShapes, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape>(this.BuildDataShape));
				}
			}
			this.m_currentTableBinding = currentTableBinding;
			return null;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002A30C File Offset: 0x0002850C
		private IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember> BuildDataMembers(IEnumerable<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> dsqMembers, IList<DataRow> dataRows)
		{
			if (dsqMembers == null)
			{
				return null;
			}
			List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in dsqMembers)
			{
				if (dataMember.ContextOnly)
				{
					IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember> list2 = this.BuildDataMembers(dataMember.DataMembers, dataRows);
					if (list2 != null)
					{
						list.AddRange(list2);
					}
				}
				else
				{
					Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember dataMember2 = this.BuildDataMember(dataMember, dataRows);
					if (dataMember2 != null)
					{
						list.Add(dataMember2);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002A39C File Offset: 0x0002859C
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember BuildDataMember(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember, IList<DataRow> dataRows)
		{
			if (dsqMember.ContextOnly)
			{
				return null;
			}
			DataBinding dataBinding = this.BuildDataBinding(dsqMember);
			if (dsqMember.ParticipatesInWindowing(this.m_annotations.SubtotalAnnotations) && this.m_currentTableBinding != null)
			{
				this.m_segmentationItemsToTableMapping.Add(dsqMember, this.m_currentTableBinding);
			}
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember childMemberToInheritFrom = this.GetChildMemberToInheritFrom(dsqMember);
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation> list = this.MergeCalculations(dsqMember, childMemberToInheritFrom);
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember dataMember = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember
			{
				Id = this.EnsureUniqueId(dsqMember),
				Calculations = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation>(list, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation>(this.BuildCalculation)),
				Group = this.BuildGroup(dsqMember),
				DataMembers = this.BuildDataMembers(dsqMember.DataMembers, dataRows),
				StartPosition = this.BuildStartPosition(dsqMember),
				MatchCondition = this.BuildMatchCondition(dsqMember),
				DiscardCondition = this.BuildDiscardCondition(dsqMember),
				DataBinding = dataBinding,
				RestartKindDefinition = this.BuildRestartKindDefinition(dsqMember)
			};
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember2 = childMemberToInheritFrom ?? dsqMember;
			if (dataRows != null && this.m_annotations.IsLeaf(dataMember2))
			{
				int leafIndex = this.m_annotations.GetLeafIndex(dataMember2);
				DataRow dataRow = dataRows[leafIndex];
				dataMember.Intersections = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataIntersection, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataIntersection>(dataRow.Intersections, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataIntersection, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataIntersection>(this.BuildIntersection));
			}
			return dataMember;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002A4D4 File Offset: 0x000286D4
		private Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember GetChildMemberToInheritFrom(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember)
		{
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> dataMembers = dsqMember.DataMembers;
			if (dataMembers == null || dataMembers.Count != 2)
			{
				return null;
			}
			if (dsqMember.DataMembers.Any((Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember d) => d.ContextOnly))
			{
				return dsqMember.StaticChildDataMemberOrDefault();
			}
			return null;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002A534 File Offset: 0x00028734
		private List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation> MergeCalculations(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember1, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember2)
		{
			if (dataMember1 == null || dataMember1.Calculations.IsNullOrEmpty<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation>())
			{
				if (dataMember2 == null)
				{
					return null;
				}
				return dataMember2.Calculations;
			}
			else
			{
				if (dataMember2 == null || dataMember2.Calculations.IsNullOrEmpty<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation>())
				{
					return dataMember1.Calculations;
				}
				return dataMember1.Calculations.Concat(dataMember2.Calculations).ToList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation>();
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002A58C File Offset: 0x0002878C
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Group BuildGroup(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			if (group == null)
			{
				return null;
			}
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Group group2 = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Group();
			group2.GroupKeys = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>(group.GroupKeys, dataMember, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey>(this.BuildGroupKey));
			if (group.ScopeIdDefinition != null)
			{
				group2.ScopeIdDefinition = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ScopeIdDefinition
				{
					ScopeKeys = this.BuildCollection<ScopeValueDefinition, ScopeKey, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>(group.ScopeIdDefinition.Values, dataMember, new Func<ScopeValueDefinition, Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember, ScopeKey>(this.BuildScopeKey))
				};
			}
			return group2;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002A604 File Offset: 0x00028804
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataIntersection BuildIntersection(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataIntersection dsqIntersection)
		{
			if (!this.m_annotations.AreContentsIncludedInOutput(dsqIntersection))
			{
				return null;
			}
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataIntersection
			{
				Id = this.EnsureUniqueId(dsqIntersection),
				Calculations = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation>(dsqIntersection.Calculations, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation>(this.BuildCalculation)),
				DataShapes = this.BuildCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape>(dsqIntersection.DataShapes, new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape>(this.BuildDataShape)),
				DataBinding = this.BuildDataBinding(dsqIntersection)
			};
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002A67C File Offset: 0x0002887C
		private IList<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>> BuildLegacyRestartDefinitions(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			IEnumerable<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> restartMembers = dataShape.GetRestartMembers(this.m_annotations.SubtotalAnnotations);
			if (restartMembers == null || !restartMembers.Any<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>())
			{
				return null;
			}
			List<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>> list = new List<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in restartMembers)
			{
				list.Add(this.BuildRestartDefinition(dataMember));
			}
			return list;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002A6F0 File Offset: 0x000288F0
		private IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode> BuildRestartDefinition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			ExpressionTable expressionTableForMember = this.GetExpressionTableForMember(dataMember);
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>();
			if (group != null)
			{
				Microsoft.DataShaping.Contract.RetailAssert(group.SortKeys != null && group.SortKeys.Count > 0, "Missing or empty SortKeys collection on group");
				for (int i = 0; i < group.SortKeys.Count; i++)
				{
					Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey sortKey = group.SortKeys[i];
					list.Add(this.BuildExpression(sortKey.Value.ExpressionId, expressionTableForMember));
				}
			}
			else
			{
				list.Add(this.GetRestartExpressionForStatic(dataMember, expressionTableForMember, ObjectType.RestartDefinition));
			}
			return list;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002A788 File Offset: 0x00028988
		private DataLimits BuildDataLimits(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape, out List<DataWindow> dsdDataWindows)
		{
			dsdDataWindows = null;
			if (dataShape.Limits.IsNullOrEmpty<Limit>())
			{
				return null;
			}
			List<DataLimit> list = null;
			foreach (Limit limit in dataShape.Limits)
			{
				if (limit.Operator is WindowLimitOperator)
				{
					Microsoft.DataShaping.Util.AddToLazyList<DataWindow>(ref dsdDataWindows, this.BuildDataWindow(limit));
				}
				else
				{
					DataLimit dataLimit = this.BuildDataLimit(limit);
					if (dataLimit != null)
					{
						Microsoft.DataShaping.Util.AddToLazyList<DataLimit>(ref list, dataLimit);
					}
				}
			}
			if (list.IsNullOrEmpty<DataLimit>())
			{
				return null;
			}
			string currentTableBinding = this.m_currentTableBinding;
			List<TelemetryItem> list2 = this.BuildLimitTelemetryItems();
			DataLimits dataLimits = new DataLimits();
			dataLimits.Limits = list;
			dataLimits.DataBinding = this.BuildDataBindingForLimits(dataShape.Limits, dataShape.DynamicLimits);
			dataLimits.TelemetryItems = list2;
			this.m_currentTableBinding = currentTableBinding;
			return dataLimits;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002A868 File Offset: 0x00028A68
		private List<TelemetryItem> BuildLimitTelemetryItems()
		{
			IList<LimitTelemetryItem> limitTelemetry = this.GetLimitTelemetry();
			if (limitTelemetry.IsNullOrEmpty<LimitTelemetryItem>())
			{
				return null;
			}
			List<TelemetryItem> list = new List<TelemetryItem>();
			ExpressionTable expressionTableForLimitOverrides = this.GetExpressionTableForLimitOverrides();
			foreach (LimitTelemetryItem limitTelemetryItem in limitTelemetry)
			{
				Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = DsdExpressionGenerator.Generate(expressionTableForLimitOverrides.GetNode(limitTelemetryItem.Value), expressionTableForLimitOverrides, new DsdExpressionGenerator.GetTableIdForFieldReference(this.GetDataSetTableId), new DsdExpressionGenerator.GetTableIdForTransformColumn(this.GetTransformTableId), null);
				list.Add(new TelemetryItem
				{
					Name = limitTelemetryItem.Name,
					Expression = expressionNode
				});
			}
			return list;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002A918 File Offset: 0x00028B18
		private DataLimit BuildDataLimit(Limit limit)
		{
			LimitOverride limitOverride = this.GetLimitOverrides().EmptyIfNull<LimitOverride>().FirstOrDefault((LimitOverride o) => o.LimitId == limit.Id);
			ExpressionTable expressionTableForLimits = this.GetExpressionTableForLimits();
			DataLimit dataLimit = DsdLimitGenerator.Generate(limit, expressionTableForLimits, this.GetExpressionTableForLimitOverrides(), this.m_annotations.SubtotalAnnotations, this.m_scopeTree, new DsdExpressionGenerator.GetTableIdForFieldReference(this.GetDataSetTableId), new DsdExpressionGenerator.GetTableIdForTransformColumn(this.GetTransformTableId), limitOverride);
			if (dataLimit == null)
			{
				Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode literalExpressionNode = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode
				{
					Value = -1
				};
				this.m_limitMetadata.Add(limit.Id.Value, new DsdLimitMetadata(literalExpressionNode, null));
			}
			else
			{
				Dictionary<string, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode> dictionary = null;
				if (limitOverride != null)
				{
					dictionary = new Dictionary<string, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>(LimitPropertyConstants.NameComparer);
					if (dataLimit.Operator.DbCount != null)
					{
						Microsoft.DataShaping.Util.AddToLazyDictionary<string, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>(ref dictionary, "DbCount", dataLimit.Operator.DbCount, null);
					}
					if (limitOverride.Properties != null)
					{
						foreach (KeyValuePair<string, ExpressionId> keyValuePair in limitOverride.Properties)
						{
							Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = this.BuildExpression(new ExpressionId?(keyValuePair.Value), this.GetExpressionTableForLimitOverrides());
							Microsoft.DataShaping.Util.AddToLazyDictionary<string, Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>(ref dictionary, keyValuePair.Key, expressionNode, null);
						}
					}
				}
				DsdLimitMetadata dsdLimitMetadata = new DsdLimitMetadata(dataLimit.Operator.Count, dictionary);
				this.m_limitMetadata.Add(limit.Id.Value, dsdLimitMetadata);
			}
			return dataLimit;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002AABC File Offset: 0x00028CBC
		private DataWindow BuildLegacyDataWindow(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			if (!dataShape.PrimaryHierarchy.GetAllDynamicMembers().Any<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>() || !dataShape.RequestedPrimaryLeafCount.IsSpecified<int>())
			{
				return null;
			}
			return new DataWindow
			{
				Count = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode
				{
					Value = dataShape.RequestedPrimaryLeafCount.Value
				}
			};
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002AB10 File Offset: 0x00028D10
		private DataWindows BuildDataWindows(List<Limit> limits, DynamicLimits dynamicLimits, List<DataWindow> dsdDataWindows)
		{
			if (dsdDataWindows == null)
			{
				return null;
			}
			return new DataWindows
			{
				Windows = dsdDataWindows,
				DataBinding = this.BuildDataBindingForLimits(limits, dynamicLimits)
			};
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002AB34 File Offset: 0x00028D34
		private DataWindow BuildDataWindow(Limit windowLimit)
		{
			LimitOverride limitOverride = this.GetLimitOverrides().EmptyIfNull<LimitOverride>().FirstOrDefault((LimitOverride o) => o.LimitId == windowLimit.Id);
			DataWindow dataWindow = DsdLimitGenerator.GenerateDataWindow(windowLimit, this.GetExpressionTableForLimits(), this.GetExpressionTableForLimitOverrides(), this.m_annotations.SubtotalAnnotations, this.m_scopeTree, new DsdExpressionGenerator.GetTableIdForFieldReference(this.GetDataSetTableId), new DsdExpressionGenerator.GetTableIdForTransformColumn(this.GetTransformTableId), limitOverride, new BuildRestartDefinition(this.BuildRestartDefinition));
			DsdLimitMetadata dsdLimitMetadata = new DsdLimitMetadata(dataWindow.Count, null);
			this.m_limitMetadata.Add(windowLimit.Id.Value, dsdLimitMetadata);
			return dataWindow;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		private DataBinding BuildDataBindingForLimits(List<Limit> limits, DynamicLimits dynamicLimits)
		{
			DataBinding dataBinding = null;
			if (dynamicLimits != null)
			{
				dataBinding = this.BuildDataBinding(dynamicLimits);
			}
			if (dataBinding == null)
			{
				foreach (Limit limit in limits)
				{
					dataBinding = this.BuildDataBinding(limit);
					if (dataBinding != null)
					{
						break;
					}
				}
			}
			return dataBinding;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002AC50 File Offset: 0x00028E50
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation BuildCalculation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation calculation)
		{
			if (calculation.IsContextOnly)
			{
				return null;
			}
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation
			{
				Id = this.EnsureUniqueId(calculation),
				Value = this.BuildExpressionForCalculation(calculation)
			};
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002AC7C File Offset: 0x00028E7C
		private StartPosition BuildStartPosition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			if (!dataMember.HasStartPosition())
			{
				return null;
			}
			ExpressionTable expressionTableForMember = this.GetExpressionTableForMember(dataMember);
			StartPosition startPosition = new StartPosition
			{
				Values = new List<object>(),
				Expressions = new List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>()
			};
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Group group = dataMember.Group;
			if (group != null)
			{
				Microsoft.DataShaping.Contract.RetailAssert(group.SortKeys != null && group.SortKeys.Count > 0, "Missing or empty SortKeys collection on group");
				for (int i = 0; i < group.SortKeys.Count; i++)
				{
					Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey sortKey = group.SortKeys[i];
					startPosition.Values.Add(dataMember.Group.StartPosition.Values[i].Value.Value.Value);
					startPosition.Expressions.Add(this.BuildExpression(sortKey.Value.ExpressionId, expressionTableForMember));
				}
			}
			else
			{
				Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode restartExpressionForStatic = this.GetRestartExpressionForStatic(dataMember, expressionTableForMember, ObjectType.StartPosition);
				if (restartExpressionForStatic == null)
				{
					return null;
				}
				startPosition.Values.Add(dataMember.SubtotalStartPosition.Value);
				startPosition.Expressions.Add(restartExpressionForStatic);
			}
			return startPosition;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002AD9C File Offset: 0x00028F9C
		private Message BuildMessage(TranslationMessage message)
		{
			return new Message
			{
				Code = message.ErrorCode.ToString(),
				Severity = message.Severity.ToString(),
				Text = message.Message,
				ObjectType = message.ObjectType.ToString(),
				ObjectName = message.ObjectId,
				PropertyName = message.PropertyName
			};
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002AE20 File Offset: 0x00029020
		protected FieldValueExpressionNode BuildField(ExpressionId? expressionId, ExpressionTable expressionTable)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = this.BuildExpression(expressionId, expressionTable);
			Microsoft.DataShaping.Contract.RetailAssert(expressionNode is FieldValueExpressionNode, "Expected a FieldValueExpressionNode");
			return (FieldValueExpressionNode)expressionNode;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002AE42 File Offset: 0x00029042
		protected Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildExpression(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode node, ExpressionTable expressionTable)
		{
			return DsdExpressionGenerator.Generate(node, expressionTable, new DsdExpressionGenerator.GetTableIdForFieldReference(this.GetDataSetTableId), new DsdExpressionGenerator.GetTableIdForTransformColumn(this.GetTransformTableId), this.m_limitMetadata);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002AE6C File Offset: 0x0002906C
		protected Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildExpression(ExpressionId? exprId, ExpressionTable expressionTable)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode node = expressionTable.GetNode(exprId.Value);
			return this.BuildExpression(node, expressionTable);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002AE8F File Offset: 0x0002908F
		protected string GetDataSourceId()
		{
			return this.m_dataSource.Id;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002AE9C File Offset: 0x0002909C
		protected Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection ConvertSortDirection(Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection sortDirection)
		{
			if (sortDirection == Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection.Ascending)
			{
				return Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection.Ascending;
			}
			if (sortDirection != Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection.Descending)
			{
				Microsoft.DataShaping.Contract.RetailFail("DSQ value type " + sortDirection.ToString() + " is not supported.");
				throw new InvalidOperationException();
			}
			return Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection.Descending;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002AED1 File Offset: 0x000290D1
		private string EnsureUniqueId(IIdentifiable item)
		{
			if (!this.m_idContext.RegisterUniqueId(item))
			{
				Microsoft.DataShaping.Contract.RetailFail("Id was not unique");
			}
			return item.Id.Value;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002AEF8 File Offset: 0x000290F8
		private IList<DSDT> BuildCollection<DSQT, DSDT>(IEnumerable<DSQT> collection, Func<DSQT, DSDT> buildAction)
		{
			return this.BuildCollection<DSQT, DSDT, object>(collection, null, (DSQT dsqt, object item) => buildAction(dsqt));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002AF28 File Offset: 0x00029128
		private IList<DSDT> BuildCollection<DSQT, DSDT, TItem>(IEnumerable<DSQT> collection, TItem item, Func<DSQT, TItem, DSDT> buildAction)
		{
			if (collection == null)
			{
				return null;
			}
			List<DSDT> list = new List<DSDT>();
			foreach (DSQT dsqt in collection)
			{
				DSDT dsdt = buildAction(dsqt, item);
				if (dsdt != null)
				{
					list.Add(dsdt);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x04000584 RID: 1412
		protected const string DataSetNameSuffix = "DataSet";

		// Token: 0x04000585 RID: 1413
		private const int LimitRemovedCountValue = -1;

		// Token: 0x04000586 RID: 1414
		protected static readonly StringComparer DataShapeDefinitionIdComparer = StringComparer.Ordinal;

		// Token: 0x04000587 RID: 1415
		protected readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000588 RID: 1416
		protected readonly ScopeTree m_scopeTree;

		// Token: 0x04000589 RID: 1417
		protected readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400058A RID: 1418
		protected readonly bool m_applyTransformsInQuery;

		// Token: 0x0400058B RID: 1419
		protected readonly bool m_useConceptualSchema;

		// Token: 0x0400058C RID: 1420
		protected readonly DsdGeneratorIdContext m_idContext;

		// Token: 0x0400058D RID: 1421
		protected readonly Dictionary<IContextItem, string> m_segmentationItemsToTableMapping;

		// Token: 0x0400058E RID: 1422
		private readonly Dictionary<Identifier, DsdLimitMetadata> m_limitMetadata;

		// Token: 0x0400058F RID: 1423
		protected Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataSource m_dataSource;

		// Token: 0x04000590 RID: 1424
		protected string m_currentTableBinding;
	}
}
