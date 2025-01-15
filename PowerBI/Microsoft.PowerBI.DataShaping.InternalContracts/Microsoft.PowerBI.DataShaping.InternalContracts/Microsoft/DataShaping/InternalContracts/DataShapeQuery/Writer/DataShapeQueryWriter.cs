using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Common.Json;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer
{
	// Token: 0x020000BE RID: 190
	internal abstract class DataShapeQueryWriter : IDisposable
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00008744 File Offset: 0x00006944
		public static void WriteJson(DataShapeQuery dataShapeQuery, Stream stream)
		{
			using (DataShapeQueryWriter dataShapeQueryWriter = new JsonDataShapeQueryWriter(stream))
			{
				dataShapeQueryWriter.WriteDataShapeQuery(dataShapeQuery);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000877C File Offset: 0x0000697C
		public static void WriteJson(DataShapeQuery dataShapeQuery, JsonWriter jsonWriter)
		{
			using (DataShapeQueryWriter dataShapeQueryWriter = new JsonDataShapeQueryWriter(jsonWriter))
			{
				dataShapeQueryWriter.WriteDataShapeQuery(dataShapeQuery);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000087B4 File Offset: 0x000069B4
		public static void WriteJson(DataShape dataShape, Stream stream)
		{
			using (DataShapeQueryWriter dataShapeQueryWriter = new JsonDataShapeQueryWriter(stream))
			{
				dataShapeQueryWriter.WriteDataShape(dataShape, true);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000087EC File Offset: 0x000069EC
		public static void WriteJson(DataShape dataShape, JsonWriter jsonWriter)
		{
			using (DataShapeQueryWriter dataShapeQueryWriter = new JsonDataShapeQueryWriter(jsonWriter))
			{
				dataShapeQueryWriter.WriteDataShape(dataShape, true);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00008824 File Offset: 0x00006A24
		public static string ToJson(DataShapeQuery dataShapeQuery)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				DataShapeQueryWriter.WriteJson(dataShapeQuery, memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000888C File Offset: 0x00006A8C
		public static string ToJson(DataShape dataShape)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				DataShapeQueryWriter.WriteJson(dataShape, memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000088F4 File Offset: 0x00006AF4
		private void WriteDataShapeQuery(DataShapeQuery dataShapeQuery)
		{
			this.WriteStartDocument();
			this.WriteList<DataSource>("DataSources", dataShapeQuery.DataSources, new Action<DataSource, bool>(this.WriteDataSource), false);
			this.WriteDataShapes(dataShapeQuery.DataShapes);
			this.WriteEndDocument();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000892C File Offset: 0x00006B2C
		private void WriteList<T>(string collectionLocalName, List<T> list, Action<T, bool> writeItem, bool isisInCollection = false)
		{
			if (list == null)
			{
				return;
			}
			if (isisInCollection)
			{
				this.WriteStartArray();
			}
			else
			{
				this.WriteStartArray(collectionLocalName);
			}
			foreach (T t in list)
			{
				writeItem(t, true);
			}
			this.WriteEndArray();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00008998 File Offset: 0x00006B98
		private void WriteDataSource(DataSource dataSource, bool isisInCollection)
		{
			if (dataSource == null)
			{
				return;
			}
			this.WriteStartObject("DataSource", isisInCollection);
			this.WriteIdentifier("Id", dataSource.Id);
			this.WriteDataSourceReference(dataSource.DataSourceReference);
			this.WriteEndObject();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000089CD File Offset: 0x00006BCD
		private void WriteDataSourceReference(DataSourceReference reference)
		{
			if (reference == null)
			{
				return;
			}
			this.WriteStartObject("DataSourceReference", false);
			this.WriteString("DataSourceName", reference.DataSourceName);
			this.WriteString("ItemPath", reference.ItemPath);
			this.WriteEndObject();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00008A07 File Offset: 0x00006C07
		private void WriteDataShape(DataShape dataShape, bool isisInCollection)
		{
			this.WriteDataShape(dataShape, isisInCollection, "DataShape");
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00008A18 File Offset: 0x00006C18
		private void WriteDataShape(DataShape dataShape, bool isInCollection, string startObjectConstant)
		{
			if (dataShape == null)
			{
				return;
			}
			this.WriteStartObject(startObjectConstant, isInCollection);
			this.WriteIdentifier("Id", dataShape.Id);
			this.WriteIdentifier("DataSourceId", dataShape.DataSourceId);
			if (dataShape.IsIndependent)
			{
				this.WriteValue("IsIndependent", dataShape.IsIndependent);
			}
			this.WriteValue("ContextOnly", dataShape.ContextOnly);
			if (dataShape.Usage != DataShapeUsage.Query)
			{
				this.WriteValue("Usage", dataShape.Usage.ToString());
			}
			this.WriteExtensionSchema(dataShape.ExtensionSchema);
			this.WriteList<QueryParameterDeclaration>("QueryParameters", dataShape.QueryParameters, new Action<QueryParameterDeclaration, bool>(this.WriteQueryParameter), false);
			this.WriteString("DataSourceVariables", dataShape.DataSourceVariables);
			this.WriteCalculations(dataShape.Calculations);
			this.WriteDataHierarchy("PrimaryHierarchy", dataShape.PrimaryHierarchy);
			this.WriteDataHierarchy("SecondaryHierarchy", dataShape.SecondaryHierarchy);
			this.WriteList<DataRow>("DataRows", dataShape.DataRows, new Action<DataRow, bool>(this.WriteDataRow), false);
			this.WriteList<VisualAxis>("VisualCalculationMetadata", dataShape.VisualCalculationMetadata, new Action<VisualAxis, bool>(this.WriteVisualAxis), false);
			this.WriteDataShapes(dataShape.DataShapes);
			this.WriteList<Filter>("Filters", dataShape.Filters, new Action<Filter, bool>(this.WriteFilter), false);
			this.WriteList<Limit>("Limits", dataShape.Limits, new Action<Limit, bool>(this.WriteLimit), false);
			this.WriteDynamicLimits(dataShape.DynamicLimits);
			this.WriteList<DataTransform>("Transforms", dataShape.Transforms, new Action<DataTransform, bool>(this.WriteDataTransform), false);
			this.WriteIntValue("RequestedPrimaryLeafCount", dataShape.RequestedPrimaryLeafCount);
			this.WriteList<RestartToken>("RestartTokens", dataShape.RestartTokens, new Action<RestartToken, bool>(this.WriteRestartToken), false);
			this.WriteMessages(dataShape.Messages);
			if (dataShape.IncludeRestartToken != null && dataShape.IncludeRestartToken.Value)
			{
				this.WriteValue("IncludeRestartTokens", true);
			}
			if (dataShape.RestartMatchingBehavior != null && dataShape.RestartMatchingBehavior.Value != RestartMatchingBehavior.IsOnOrAfter)
			{
				this.WriteValue("RestartMatchingBehavior", dataShape.RestartMatchingBehavior.ToString());
			}
			this.WriteEndObject();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00008C76 File Offset: 0x00006E76
		private void WriteQueryParameter(QueryParameterDeclaration parameter, bool isInCollection)
		{
			if (parameter == null)
			{
				return;
			}
			this.WriteStartObject("QueryParameter", isInCollection);
			this.WriteString("Name", parameter.Name);
			this.WriteString("Type", parameter.Type.ToString());
			this.WriteEndObject();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00008CB8 File Offset: 0x00006EB8
		private void WriteVisualAxis(VisualAxis visualAxis, bool isInCollection)
		{
			if (visualAxis == null)
			{
				return;
			}
			this.WriteStartObject("VisualAxis", isInCollection);
			this.WriteString("Name", visualAxis.Name);
			this.WriteList<VisualAxisGroup>("Groups", visualAxis.Groups, new Action<VisualAxisGroup, bool>(this.WriteVisualAxisGroup), false);
			this.WriteEndObject();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00008D0A File Offset: 0x00006F0A
		private void WriteVisualAxisGroup(VisualAxisGroup visualAxisGroup, bool isInCollection)
		{
			if (visualAxisGroup == null)
			{
				return;
			}
			this.WriteStartObject("Groups", isInCollection);
			this.WriteExpression("Member", visualAxisGroup.Member, false);
			this.WriteEndObject();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00008D34 File Offset: 0x00006F34
		private void WriteExtensionSchema(ExtensionSchema schema)
		{
			if (schema == null)
			{
				return;
			}
			this.WriteStartObject("ExtensionSchema", false);
			this.WriteString("Name", schema.Name);
			this.WriteList<ExtensionEntity>("Entities", schema.Entities, new Action<ExtensionEntity, bool>(this.WriteExtensionEntity), false);
			this.WriteEndObject();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00008D88 File Offset: 0x00006F88
		private void WriteExtensionEntity(ExtensionEntity entity, bool isInCollection)
		{
			if (entity == null)
			{
				return;
			}
			this.WriteStartObject("ExtensionEntity", isInCollection);
			this.WriteString("Name", entity.Name);
			this.WriteString("Extends", entity.Extends);
			if (!entity.Measures.IsNullOrEmpty<ExtensionMeasure>())
			{
				this.WriteList<ExtensionMeasure>("Measures", entity.Measures, new Action<ExtensionMeasure, bool>(this.WriteExtensionProperty), false);
			}
			if (!entity.Columns.IsNullOrEmpty<ExtensionColumn>())
			{
				this.WriteList<ExtensionColumn>("Columns", entity.Columns, new Action<ExtensionColumn, bool>(this.WriteExtensionProperty), false);
			}
			this.WriteEndObject();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00008E24 File Offset: 0x00007024
		private void WriteExtensionProperty(ExtensionProperty property, bool isInCollection)
		{
			if (property == null)
			{
				return;
			}
			this.WriteStartObject((property is ExtensionMeasure) ? "ExtensionMeasure" : "ExtensionColumn", isInCollection);
			this.WriteString("Name", property.Name);
			this.WriteExpression("Expression", property.Expression, false);
			this.WriteEnum<ConceptualPrimitiveType>("DataType", property.DataType);
			this.WriteEndObject();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00008E8C File Offset: 0x0000708C
		private void WriteDataTransform(DataTransform transform, bool isInCollection)
		{
			this.WriteStartObject("DataTransform", isInCollection);
			this.WriteIdentifier("Id", transform.Id);
			this.WriteValue("Algorithm", transform.Algorithm);
			this.WriteDataTransformInput(transform.Input);
			this.WriteDataTransformOutput(transform.Output);
			this.WriteEndObject();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00008EE5 File Offset: 0x000070E5
		private void WriteDataTransformInput(DataTransformInput input)
		{
			this.WriteStartObject("Input", false);
			this.WriteList<DataTransformParameter>("Parameters", input.Parameters, new Action<DataTransformParameter, bool>(this.WriteDataTransformParameter), false);
			this.WriteDataTransformTable(input.Table);
			this.WriteEndObject();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00008F23 File Offset: 0x00007123
		private void WriteDataTransformOutput(DataTransformOutput output)
		{
			this.WriteStartObject("Output", false);
			this.WriteDataTransformTable(output.Table);
			this.WriteEndObject();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00008F43 File Offset: 0x00007143
		private void WriteDataTransformParameter(DataTransformParameter param, bool isInCollection)
		{
			this.WriteStartObject("DataTransformParamter", isInCollection);
			this.WriteIdentifier("Id", param.Id);
			this.WriteExpression("Value", param.Value, false);
			this.WriteEndObject();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00008F7C File Offset: 0x0000717C
		private void WriteDataTransformTable(DataTransformTable table)
		{
			this.WriteStartObject("Table", false);
			this.WriteIdentifier("Id", table.Id);
			this.WriteList<DataTransformTableColumn>("Columns", table.Columns, new Action<DataTransformTableColumn, bool>(this.WriteDataTransformTableColumn), false);
			this.WriteEndObject();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00008FCC File Offset: 0x000071CC
		private void WriteDataTransformTableColumn(DataTransformTableColumn column, bool isInCollection)
		{
			this.WriteStartObject("DataTransformTableColumn", isInCollection);
			this.WriteIdentifier("Id", column.Id);
			this.WriteValue("Role", column.Role);
			this.WriteExpression("Value", column.Value, false);
			this.WriteEndObject();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00009020 File Offset: 0x00007220
		private void WriteDynamicLimits(DynamicLimits dynamicLimits)
		{
			if (dynamicLimits == null)
			{
				return;
			}
			this.WriteStartObject("DynamicLimits", false);
			this.WriteIntValue("TargetIntersectionCount", dynamicLimits.TargetIntersectionCount);
			this.WriteExpression("IntersectionLimit", dynamicLimits.IntersectionLimit, false);
			this.WriteDynamicLimitRecommendation("Primary", dynamicLimits.Primary);
			this.WriteDynamicLimitRecommendation("Secondary", dynamicLimits.Secondary);
			this.WriteList<DynamicLimitBlock>("Blocks", dynamicLimits.Blocks, new Action<DynamicLimitBlock, bool>(this.WriteDynamicLimitBlock), false);
			this.WriteEndObject();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000090A8 File Offset: 0x000072A8
		private void WriteDynamicLimitBlock(DynamicLimitBlock block, bool isInCollection)
		{
			DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = block as DynamicLimitEvenDistributionBlock;
			if (dynamicLimitEvenDistributionBlock != null)
			{
				this.WriteDynamicLimitEvenDistributionBlock(dynamicLimitEvenDistributionBlock, isInCollection);
				return;
			}
			DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = block as DynamicLimitPrimarySecondaryBlock;
			if (dynamicLimitPrimarySecondaryBlock == null)
			{
				throw new InvalidOperationException("Unknown DynamicLimitBlock type: " + block.GetType().Name);
			}
			this.WriteDynamicLimitPrimarySecondaryBlock(dynamicLimitPrimarySecondaryBlock, isInCollection);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000090F8 File Offset: 0x000072F8
		private void WriteDynamicLimitEvenDistributionBlock(DynamicLimitEvenDistributionBlock block, bool isInCollection)
		{
			if (block == null)
			{
				return;
			}
			this.WriteStartObject("Block", isInCollection);
			this.WriteEnum<ObjectType>("Type", ObjectType.DynamicLimitEvenDistributionBlock);
			this.WriteDynamicLimitRecommendation("Count", block.Count);
			this.WriteList<DynamicLimit>("Limits", block.Limits, new Action<DynamicLimit, bool>(this.WriteDynamicLimit), false);
			this.WriteEndObject();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00009158 File Offset: 0x00007358
		private void WriteDynamicLimitPrimarySecondaryBlock(DynamicLimitPrimarySecondaryBlock block, bool isInCollection)
		{
			if (block == null)
			{
				return;
			}
			this.WriteStartObject("Block", isInCollection);
			this.WriteEnum<ObjectType>("Type", ObjectType.DynamicLimitPrimarySecondaryBlock);
			this.WriteDynamicLimitRecommendation("Count", block.Count);
			this.WriteDynamicLimit("Primary", block.Primary, false);
			this.WriteDynamicLimit("Secondary", block.Secondary, false);
			this.WriteEndObject();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000091BD File Offset: 0x000073BD
		private void WriteDynamicLimit(DynamicLimit limit, bool isInCollection)
		{
			this.WriteDynamicLimit("DynamicLimit", limit, isInCollection);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000091CC File Offset: 0x000073CC
		private void WriteDynamicLimit(string name, DynamicLimit limit, bool isInCollection = false)
		{
			if (limit == null)
			{
				return;
			}
			this.WriteStartObject(name, isInCollection);
			this.WriteExpression("LimitRef", limit.LimitRef, false);
			this.WriteDynamicLimitRecommendation("Count", limit.Count);
			this.WriteEndObject();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00009204 File Offset: 0x00007404
		private void WriteDynamicLimitRecommendation(string name, DynamicLimitRecommendation recommendation)
		{
			if (recommendation == null)
			{
				return;
			}
			this.WriteStartObject(name, false);
			this.WriteIntValue("Min", recommendation.Min);
			this.WriteIntValue("Max", recommendation.Max);
			if (recommendation.IsMandatoryConstraint)
			{
				this.WriteValue("IsMandatoryConstraint", recommendation.IsMandatoryConstraint);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00009264 File Offset: 0x00007464
		private void WriteFilter(Filter filter, bool isInCollection)
		{
			if (filter == null)
			{
				return;
			}
			this.WriteStartObject("Filter", isInCollection);
			if (filter.UsageKind != FilterUsageKind.Default)
			{
				this.WriteEnum<FilterUsageKind>("UsageKind", filter.UsageKind);
			}
			this.WriteExpression("Target", filter.Target, false);
			this.WriteFilterCondition(filter.Condition, false);
			this.WriteEndObject();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000092C0 File Offset: 0x000074C0
		private void WriteFilterCondition(FilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			if (condition is UnaryFilterCondition)
			{
				this.WriteUnaryFilterCondition((UnaryFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is BinaryFilterCondition)
			{
				this.WriteBinaryFilterCondition((BinaryFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is CompoundFilterCondition)
			{
				this.WriteCompoundFilterCondition((CompoundFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is FilterEmptyGroupsCondition)
			{
				this.WriteFilterEmptyGroupsCondition((FilterEmptyGroupsCondition)condition, isInCollection);
				return;
			}
			if (condition is ContextFilterCondition)
			{
				this.WriteContextFilterCondition((ContextFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is ApplyFilterCondition)
			{
				this.WriteApplyFilterCondition((ApplyFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is ExistsFilterCondition)
			{
				this.WriteExistsFilterCondition((ExistsFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is AnyValueFilterCondition)
			{
				this.WriteAnyValueFilterCondition((AnyValueFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is DefaultValueFilterCondition)
			{
				this.WriteDefaultValueFilterCondition((DefaultValueFilterCondition)condition, isInCollection);
				return;
			}
			if (condition is InFilterCondition)
			{
				this.WriteInFilterCondition((InFilterCondition)condition, isInCollection);
				return;
			}
			throw new InvalidOperationException("Unknown FilterCondition type: " + condition.ObjectType.ToString());
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000093D0 File Offset: 0x000075D0
		private void WriteInFilterCondition(InFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "In");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteList<Expression>("Expressions", condition.Expressions, new Action<Expression, bool>(this.WriteExpression), false);
			if (condition.Values != null)
			{
				this.WriteList<List<Expression>>("Values", condition.Values, new Action<List<Expression>, bool>(this.WriteNestedExpressionList), false);
			}
			if (condition.IdentityComparison)
			{
				this.WriteValue("IdentityComparison", condition.IdentityComparison);
			}
			if (condition.Table != null)
			{
				this.WriteExpression("Table", condition.Table, false);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00009490 File Offset: 0x00007690
		private void WriteUnaryFilterCondition(UnaryFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "Unary");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteExpression("Expression", condition.Expression, false);
			this.WriteValue("Not", condition.Not);
			this.WriteEndObject();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000094F8 File Offset: 0x000076F8
		private void WriteBinaryFilterCondition(BinaryFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "Binary");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteExpression("LeftExpression", condition.LeftExpression, false);
			this.WriteEnum<BinaryFilterOperator>("Operator", condition.Operator);
			this.WriteExpression("RightExpression", condition.RightExpression, false);
			this.WriteValue("Not", condition.Not);
			this.WriteEndObject();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00009584 File Offset: 0x00007784
		private void WriteCompoundFilterCondition(CompoundFilterCondition condition, bool isisInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isisInCollection);
			this.WriteString("Type", "Compound");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteEnum<CompoundFilterOperator>("Operator", condition.Operator);
			this.WriteList<FilterCondition>("Conditions", condition.Conditions, new Action<FilterCondition, bool>(this.WriteFilterCondition), false);
			this.WriteEndObject();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000095F7 File Offset: 0x000077F7
		private void WriteFilterEmptyGroupsCondition(FilterEmptyGroupsCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "FilterEmptyGroups");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteEndObject();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00009630 File Offset: 0x00007830
		private void WriteContextFilterCondition(ContextFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "Context");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteDataShape(condition.DataShape, false);
			this.WriteEndObject();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00009684 File Offset: 0x00007884
		private void WriteApplyFilterCondition(ApplyFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "Apply");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteExpression("DataShapeReference", condition.DataShapeReference, false);
			this.WriteEndObject();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000096DC File Offset: 0x000078DC
		private void WriteExistsFilterCondition(ExistsFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "Exists");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteList<ExistsFilterItem>("Items", condition.Items, new Action<ExistsFilterItem, bool>(this.WriteExistsFilterItem), false);
			this.WriteEndObject();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00009740 File Offset: 0x00007940
		private void WriteExistsFilterItem(ExistsFilterItem item, bool isInCollection)
		{
			if (item == null)
			{
				return;
			}
			this.WriteStartObject("ExistsFilterItem", isInCollection);
			this.WriteList<Expression>("Targets", item.Targets, new Action<Expression, bool>(this.WriteExpression), false);
			this.WriteExpression("Exists", item.Exists, false);
			this.WriteEndObject();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00009794 File Offset: 0x00007994
		private void WriteAnyValueFilterCondition(AnyValueFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "AnyValue");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteList<Expression>("Items", condition.Targets, new Action<Expression, bool>(this.WriteExpression), false);
			if (condition.DefaultValueOverridesAncestors)
			{
				this.WriteValue("DefaultValueOverridesAncestors", true);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00009810 File Offset: 0x00007A10
		private void WriteDefaultValueFilterCondition(DefaultValueFilterCondition condition, bool isInCollection)
		{
			if (condition == null)
			{
				return;
			}
			this.WriteStartObject("Condition", isInCollection);
			this.WriteString("Type", "DefaultValue");
			this.WriteIdentifier("Id", condition.Id);
			this.WriteList<Expression>("Items", condition.Targets, new Action<Expression, bool>(this.WriteExpression), false);
			this.WriteEndObject();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00009874 File Offset: 0x00007A74
		private void WriteLimit(Limit limit, bool isInCollection)
		{
			if (limit == null)
			{
				return;
			}
			this.WriteStartObject("Limit", isInCollection);
			this.WriteIdentifier("Id", limit.Id);
			this.WriteLimitOperator(limit.Operator);
			this.WriteList<Expression>("Targets", limit.Targets, new Action<Expression, bool>(this.WriteExpression), false);
			this.WriteExpression("Within", limit.Within, false);
			if (limit.TelemetryId != null)
			{
				string text = "TelemetryId";
				int? telemetryId = limit.TelemetryId;
				this.WriteIntValue(text, (telemetryId != null) ? telemetryId.GetValueOrDefault() : null);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00009920 File Offset: 0x00007B20
		private void WriteLimitOperator(LimitOperator limitOperator)
		{
			if (limitOperator == null)
			{
				return;
			}
			TopLimitOperator topLimitOperator = limitOperator as TopLimitOperator;
			if (topLimitOperator != null)
			{
				this.WriteTopLimitOperator(topLimitOperator);
				return;
			}
			SampleLimitOperator sampleLimitOperator = limitOperator as SampleLimitOperator;
			if (sampleLimitOperator != null)
			{
				this.WriteSampleLimitOperator(sampleLimitOperator);
				return;
			}
			FirstLimitOperator firstLimitOperator = limitOperator as FirstLimitOperator;
			if (firstLimitOperator != null)
			{
				this.WriteFirstLimitOperator(firstLimitOperator);
				return;
			}
			LastLimitOperator lastLimitOperator = limitOperator as LastLimitOperator;
			if (lastLimitOperator != null)
			{
				this.WriteLastLimitOperator(lastLimitOperator);
				return;
			}
			BottomLimitOperator bottomLimitOperator = limitOperator as BottomLimitOperator;
			if (bottomLimitOperator != null)
			{
				this.WriteBottomLimitOperator(bottomLimitOperator);
				return;
			}
			BinnedLineSampleLimitOperator binnedLineSampleLimitOperator = limitOperator as BinnedLineSampleLimitOperator;
			if (binnedLineSampleLimitOperator != null)
			{
				this.WriteBinnedLineSampleLimitOperator(binnedLineSampleLimitOperator);
				return;
			}
			OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator = limitOperator as OverlappingPointsSampleLimitOperator;
			if (overlappingPointsSampleLimitOperator != null)
			{
				this.WriteOverlappingPointsSampleLimitOperator(overlappingPointsSampleLimitOperator);
				return;
			}
			TopNPerLevelLimitOperator topNPerLevelLimitOperator = limitOperator as TopNPerLevelLimitOperator;
			if (topNPerLevelLimitOperator != null)
			{
				this.WriteTopNPerLevelLimitOperator(topNPerLevelLimitOperator);
				return;
			}
			WindowLimitOperator windowLimitOperator = limitOperator as WindowLimitOperator;
			if (windowLimitOperator == null)
			{
				throw new InvalidOperationException("Unknown LimitOperator type: " + limitOperator.ObjectType.ToString());
			}
			this.WriteWindowLimitOperator(windowLimitOperator);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00009A08 File Offset: 0x00007C08
		private void WriteTopLimitOperator(TopLimitOperator topLimitOperator)
		{
			if (topLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "Top");
			this.WriteIntValue("Count", topLimitOperator.Count.Value);
			this.WriteLongValue("Skip", topLimitOperator.Skip);
			this.WriteValue("IsStrict", topLimitOperator.IsStrict);
			this.WriteEndObject();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00009A78 File Offset: 0x00007C78
		private void WriteSampleLimitOperator(SampleLimitOperator sampleLimitOperator)
		{
			if (sampleLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "Sample");
			this.WriteIntValue("Count", sampleLimitOperator.Count.Value);
			this.WriteValue("PreserveKeyPoints", sampleLimitOperator.PreserveKeyPoints);
			this.WriteEndObject();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00009AD8 File Offset: 0x00007CD8
		private void WriteFirstLimitOperator(FirstLimitOperator firstLimitOperator)
		{
			if (firstLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "First");
			this.WriteIntValue("Count", firstLimitOperator.Count.Value);
			this.WriteEndObject();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00009B28 File Offset: 0x00007D28
		private void WriteLastLimitOperator(LastLimitOperator lastLimitOperator)
		{
			if (lastLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "Last");
			this.WriteIntValue("Count", lastLimitOperator.Count.Value);
			this.WriteEndObject();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00009B78 File Offset: 0x00007D78
		private void WriteBottomLimitOperator(BottomLimitOperator bottomLimitOperator)
		{
			if (bottomLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "Bottom");
			this.WriteIntValue("Count", bottomLimitOperator.Count.Value);
			this.WriteEndObject();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00009BC8 File Offset: 0x00007DC8
		private void WriteBinnedLineSampleLimitOperator(BinnedLineSampleLimitOperator binnedLineSampleLimitOperator)
		{
			if (binnedLineSampleLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "BinnedLineSample");
			this.WriteIntValue("Count", binnedLineSampleLimitOperator.Count.Value);
			this.WriteIntValue("MinPointsPerSeries", binnedLineSampleLimitOperator.MinPointsPerSeries);
			this.WriteIntValue("MaxPointsPerSeries", binnedLineSampleLimitOperator.MaxPointsPerSeries);
			this.WriteIntValue("MaxDynamicSeriesCount", binnedLineSampleLimitOperator.MaxDynamicSeriesCount);
			this.WriteList<Expression>("Measures", binnedLineSampleLimitOperator.Measures, new Action<Expression, bool>(this.WriteExpression), false);
			this.WriteExpression("PrimaryScalarKey", binnedLineSampleLimitOperator.PrimaryScalarKey, false);
			this.WriteEndObject();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00009C7C File Offset: 0x00007E7C
		private void WriteOverlappingPointsSampleLimitOperator(OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator)
		{
			if (overlappingPointsSampleLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "OverlappingPointsSample");
			this.WriteIntValue("Count", overlappingPointsSampleLimitOperator.Count.Value);
			if (overlappingPointsSampleLimitOperator.X != null)
			{
				this.WriteStartObject("X", false);
				this.WritePlotAxis(overlappingPointsSampleLimitOperator.X);
				this.WriteEndObject();
			}
			if (overlappingPointsSampleLimitOperator.Y != null)
			{
				this.WriteStartObject("Y", false);
				this.WritePlotAxis(overlappingPointsSampleLimitOperator.Y);
				this.WriteEndObject();
			}
			this.WriteEndObject();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00009D18 File Offset: 0x00007F18
		private void WriteTopNPerLevelLimitOperator(TopNPerLevelLimitOperator topNPerLevelLimitOperator)
		{
			if (topNPerLevelLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "TopNPerLevel");
			this.WriteIntValue("Count", topNPerLevelLimitOperator.Count.Value);
			this.WriteList<List<Expression>>("Levels", topNPerLevelLimitOperator.Levels, new Action<List<Expression>, bool>(this.WriteNestedExpressionList), false);
			this.WriteWindowExpansionInstance(topNPerLevelLimitOperator.WindowExpansionInstance, false);
			this.WriteEndObject();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00009D94 File Offset: 0x00007F94
		private void WriteWindowLimitOperator(WindowLimitOperator windowLimitOperator)
		{
			if (windowLimitOperator == null)
			{
				return;
			}
			this.WriteStartObject("Operator", false);
			this.WriteString("Type", "Window");
			this.WriteIntValue("Count", windowLimitOperator.Count.Value);
			this.WriteList<RestartToken>("RestartTokens", windowLimitOperator.RestartTokens, new Action<RestartToken, bool>(this.WriteRestartToken), false);
			if (windowLimitOperator.RestartMatchingBehavior != null && windowLimitOperator.RestartMatchingBehavior.Value != RestartMatchingBehavior.IsOnOrAfter)
			{
				this.WriteValue("RestartMatchingBehavior", windowLimitOperator.RestartMatchingBehavior.ToString());
			}
			this.WriteEndObject();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00009E44 File Offset: 0x00008044
		private void WriteWindowExpansionInstance(LimitWindowExpansionInstance windowExpansionInstance, bool isInCollection = false)
		{
			if (windowExpansionInstance == null)
			{
				return;
			}
			this.WriteStartObject("WindowExpansionStates", isInCollection);
			this.WriteList<Expression>("Values", windowExpansionInstance.Values, new Action<Expression, bool>(this.WriteExpression), false);
			this.WriteList<LimitWindowExpansionValue>("WindowExpansionValues", windowExpansionInstance.WindowValues, new Action<LimitWindowExpansionValue, bool>(this.WriteWindowExpansionValue), false);
			this.WriteList<LimitWindowExpansionInstance>("Children", windowExpansionInstance.Children, new Action<LimitWindowExpansionInstance, bool>(this.WriteWindowExpansionInstance), false);
			this.WriteEndObject();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00009EC4 File Offset: 0x000080C4
		private void WriteWindowExpansionValue(LimitWindowExpansionValue windowValue, bool isInCollection = false)
		{
			if (windowValue == null)
			{
				return;
			}
			this.WriteStartObject("WindowExpansionValue", isInCollection);
			this.WriteList<Expression>("Values", windowValue.Values, new Action<Expression, bool>(this.WriteExpression), false);
			if (windowValue.WindowKind != WindowKind.None)
			{
				this.WriteEnum<WindowKind>("WindowKind", windowValue.WindowKind);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00009F1E File Offset: 0x0000811E
		private void WritePlotAxis(LimitPlotAxis axis)
		{
			this.WriteExpression("Key", axis.Key, false);
			this.WriteEnum<DataReductionPlotAxisTransform>("Transform", axis.Transform);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00009F43 File Offset: 0x00008143
		private void WriteDataShapes(List<DataShape> dataShapes)
		{
			this.WriteList<DataShape>("DataShapes", dataShapes, new Action<DataShape, bool>(this.WriteDataShape), false);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00009F5E File Offset: 0x0000815E
		private void WriteCalculations(List<Calculation> calcs)
		{
			this.WriteList<Calculation>("Calculations", calcs, new Action<Calculation, bool>(this.WriteCalculation), false);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00009F79 File Offset: 0x00008179
		private void WriteMessages(List<Message> msgs)
		{
			this.WriteList<Message>("Messages", msgs, new Action<Message, bool>(this.WriteMessage), false);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00009F94 File Offset: 0x00008194
		private void WriteDataHierarchy(string hierarchyName, DataHierarchy hierarchy)
		{
			if (hierarchy == null)
			{
				return;
			}
			this.WriteStartObject(hierarchyName, false);
			this.WriteDataMembers(hierarchy.DataMembers);
			this.WriteEndObject();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00009FB4 File Offset: 0x000081B4
		private void WriteDataMembers(List<DataMember> members)
		{
			this.WriteList<DataMember>("DataMembers", members, new Action<DataMember, bool>(this.WriteDataMember), false);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00009FD0 File Offset: 0x000081D0
		private void WriteDataMember(DataMember member, bool isInCollection)
		{
			if (member == null)
			{
				return;
			}
			this.WriteStartObject("DataMember", isInCollection);
			this.WriteIdentifier("Id", member.Id);
			if (member.ContextOnly)
			{
				this.WriteValue("ContextOnly", member.ContextOnly);
			}
			this.WriteGroup(member.Group);
			this.WriteCalculations(member.Calculations);
			this.WriteDataMembers(member.DataMembers);
			this.WriteDataShapes(member.DataShapes);
			this.WriteList<FilterCondition>("InstanceFilters", member.InstanceFilters, new Action<FilterCondition, bool>(this.WriteFilterCondition), false);
			if (member.HasExplicitSubtotal)
			{
				this.WriteValue("HasExplicitSubtotal", member.HasExplicitSubtotal);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000A090 File Offset: 0x00008290
		private void WriteGroup(Group group)
		{
			if (group == null)
			{
				return;
			}
			this.WriteStartObject("Group", false);
			this.WriteDetailGroupIdentity(group.DetailGroupIdentity);
			this.WriteGroupKeys(group.GroupKeys);
			this.WriteSortKeys(group.SortKeys);
			this.WriteScopeIdDefinition(group.ScopeIdDefinition);
			this.WriteStartPosition(group.StartPosition);
			if (group.SuppressSortByMeasureRollup)
			{
				this.WriteValue("SuppressSortByMeasureRollup", group.SuppressSortByMeasureRollup);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000A10D File Offset: 0x0000830D
		private void WriteDetailGroupIdentity(DetailGroupIdentity identity)
		{
			if (identity == null)
			{
				return;
			}
			this.WriteStartObject("DetailGroupIdentity", false);
			this.WriteIdentifier("Id", identity.Id);
			this.WriteExpression("Value", identity.Value, false);
			this.WriteEndObject();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000A148 File Offset: 0x00008348
		private void WriteGroupKeys(List<GroupKey> groupKeys)
		{
			if (groupKeys == null)
			{
				return;
			}
			this.WriteList<GroupKey>("GroupKeys", groupKeys, new Action<GroupKey, bool>(this.WriteGroupKey), false);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000A167 File Offset: 0x00008367
		private void WriteGroupKey(GroupKey groupKey, bool isisInCollection)
		{
			if (groupKey == null)
			{
				return;
			}
			this.WriteStartObject("Value", isisInCollection);
			this.WriteExpression("Value", groupKey.Value, false);
			this.WriteValue("ShowItemsWithNoData", groupKey.ShowItemsWithNoData);
			this.WriteEndObject();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000A1A2 File Offset: 0x000083A2
		private void WriteSortKeys(List<SortKey> sortKeys)
		{
			if (sortKeys == null)
			{
				return;
			}
			this.WriteList<SortKey>("SortKeys", sortKeys, new Action<SortKey, bool>(this.WriteSortKey), false);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000A1C4 File Offset: 0x000083C4
		private void WriteSortKey(SortKey sortKey, bool isInCollection)
		{
			if (sortKey == null)
			{
				return;
			}
			this.WriteStartObject("Value", isInCollection);
			this.WriteExpression("Value", sortKey.Value, false);
			this.WriteString("SortDirection", Enum.GetName(typeof(SortDirection), sortKey.SortDirection.Value));
			this.WriteEndObject();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000A223 File Offset: 0x00008423
		private void WriteScopeIdDefinition(ScopeIdDefinition scopeIdDef)
		{
			if (scopeIdDef == null)
			{
				return;
			}
			this.WriteStartObject("ScopeIdDefinition", false);
			if (scopeIdDef.Values != null)
			{
				this.WriteList<ScopeValueDefinition>("Values", scopeIdDef.Values, new Action<ScopeValueDefinition, bool>(this.WriteScopeValueDefinition), false);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000A261 File Offset: 0x00008461
		private void WriteScopeValueDefinition(ScopeValueDefinition scopeValueDef, bool isInCollection)
		{
			if (scopeValueDef == null)
			{
				return;
			}
			this.WriteStartObject("Value", isInCollection);
			this.WriteExpression("Value", scopeValueDef.Value, false);
			this.WriteEndObject();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000A28B File Offset: 0x0000848B
		private void WriteStartPosition(ScopeId startPosition)
		{
			if (startPosition == null)
			{
				return;
			}
			this.WriteStartObject("StartPosition", false);
			if (startPosition.Values != null)
			{
				this.WriteList<ScopeValue>("Values", startPosition.Values, new Action<ScopeValue, bool>(this.WriteScopeValue), false);
			}
			this.WriteEndObject();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000A2C9 File Offset: 0x000084C9
		private void WriteScopeValue(ScopeValue scopeValue, bool isInCollection)
		{
			if (scopeValue == null)
			{
				return;
			}
			this.WriteStartObject("Value", isInCollection);
			this.WriteValue("Value", scopeValue.Value, false);
			this.WriteEndObject();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000A2F4 File Offset: 0x000084F4
		private void WriteRestartToken(RestartToken token, bool isInCollection)
		{
			this.WriteStartArray();
			foreach (Candidate<ScalarValue> candidate in token)
			{
				this.WriteValue(null, candidate, true);
			}
			this.WriteEndArray();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000A350 File Offset: 0x00008550
		private void WriteDataRow(DataRow row, bool isInCollection)
		{
			if (row == null)
			{
				return;
			}
			this.WriteStartObject("DataRow", isInCollection);
			this.WriteList<DataIntersection>("DataIntersections", row.Intersections, new Action<DataIntersection, bool>(this.WriteDataIntersection), false);
			this.WriteEndObject();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000A388 File Offset: 0x00008588
		private void WriteDataIntersection(DataIntersection intersection, bool isInCollection)
		{
			if (intersection == null)
			{
				return;
			}
			this.WriteStartObject("DataIntersection", isInCollection);
			this.WriteIdentifier("Id", intersection.Id);
			this.WriteCalculations(intersection.Calculations);
			this.WriteDataShapes(intersection.DataShapes);
			this.WriteEndObject();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000A3D4 File Offset: 0x000085D4
		private void WriteCalculation(Calculation calculation, bool isInCollection)
		{
			if (calculation == null)
			{
				return;
			}
			this.WriteStartObject("Calculation", isInCollection);
			this.WriteIdentifier("Id", calculation.Id);
			if (calculation.IsContextOnly)
			{
				this.WriteValue("ContextOnly", calculation.IsContextOnly);
			}
			this.WriteExpression("Value", calculation.Value, false);
			if (calculation.SuppressJoinPredicate != null && calculation.SuppressJoinPredicate.IsValid && calculation.SuppressJoinPredicate.Value)
			{
				this.WriteValue("SuppressJoinPredicate", calculation.SuppressJoinPredicate);
			}
			if (calculation.RespectInstanceFilters != null && calculation.RespectInstanceFilters.Value)
			{
				string text = "RespectInstanceFilters";
				bool? respectInstanceFilters = calculation.RespectInstanceFilters;
				this.WriteValue(text, (respectInstanceFilters != null) ? respectInstanceFilters.GetValueOrDefault() : null);
			}
			this.WriteString("NativeReferenceName", calculation.NativeReferenceName);
			this.WriteEndObject();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000A4CC File Offset: 0x000086CC
		private void WriteMessage(Message message, bool isInCollection)
		{
			if (message == null)
			{
				return;
			}
			this.WriteStartObject("Message", isInCollection);
			this.WriteString("Code", message.Code);
			this.WriteString("Severity", message.Severity);
			this.WriteString("Text", message.Text);
			this.WriteString("ObjectType", message.ObjectType);
			this.WriteString("ObjectName", message.ObjectName);
			this.WriteString("PropertyName", message.PropertyName);
			this.WriteEndObject();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000A555 File Offset: 0x00008755
		private void WriteExpression(string localName, Expression expr, bool isInCollection)
		{
			if (expr == null)
			{
				return;
			}
			this.WriteValue(localName, expr.OriginalNode.ToString(), isInCollection);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000A56E File Offset: 0x0000876E
		private void WriteExpression(Expression expr, bool isInCollection)
		{
			if (expr == null)
			{
				return;
			}
			if (!isInCollection)
			{
				throw new InvalidOperationException("This method may only be used inside a collection");
			}
			this.WriteValue(string.Empty, expr.OriginalNode.ToString(), isInCollection);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000A599 File Offset: 0x00008799
		private void WriteNestedExpressionList(List<Expression> list, bool isInCollection)
		{
			if (list == null)
			{
				return;
			}
			if (!isInCollection)
			{
				throw new InvalidOperationException("This method may only be used inside a collection");
			}
			this.WriteList<Expression>(string.Empty, list, new Action<Expression, bool>(this.WriteExpression), true);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000A5C6 File Offset: 0x000087C6
		private void WriteIdentifier(string localName, Identifier id)
		{
			if (id == null)
			{
				return;
			}
			this.WriteString(localName, id.Value);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000A5DF File Offset: 0x000087DF
		private void WriteEnum<T>(string localName, T value)
		{
			this.WriteString(localName, value.ToString());
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000A5F5 File Offset: 0x000087F5
		private void WriteEnum<T>(string localName, Candidate<T> value)
		{
			if (value == null || !value.IsValid)
			{
				return;
			}
			this.WriteEnum<T>(localName, value.Value);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000A616 File Offset: 0x00008816
		private void WriteString(string localName, string value)
		{
			if (value == null)
			{
				return;
			}
			this.WriteValue(localName, value, false);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000A625 File Offset: 0x00008825
		private void WriteValue(string localName, Candidate<string> candidate)
		{
			if (candidate == null || !candidate.IsValid)
			{
				return;
			}
			this.WriteValue(localName, candidate.Value, false);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000A647 File Offset: 0x00008847
		private void WriteValue(string localName, Candidate<bool> candidate)
		{
			if (candidate == null || !candidate.IsValid)
			{
				return;
			}
			this.WriteValue(localName, candidate.Value, false);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000A669 File Offset: 0x00008869
		private void WriteIntValue(string localName, Candidate<int> candidate)
		{
			if (candidate == null || !candidate.IsValid)
			{
				return;
			}
			this.WriteValue(localName, candidate.Value, false);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000A68B File Offset: 0x0000888B
		private void WriteLongValue(string localName, long? val)
		{
			if (val == null)
			{
				return;
			}
			this.WriteValue(localName, val.Value, false);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000A6A6 File Offset: 0x000088A6
		private void WriteValue(string localName, Candidate<ScalarValue> candidate)
		{
			this.WriteValue(localName, candidate, false);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000A6B4 File Offset: 0x000088B4
		private void WriteValue(string localName, Candidate<ScalarValue> candidate, bool isInCollection)
		{
			if (candidate == null || !candidate.IsValid)
			{
				return;
			}
			object value = candidate.Value.Value;
			this.WriteVariantValue(localName, value, isInCollection);
		}

		// Token: 0x060004E9 RID: 1257
		protected abstract void WriteStartDocument();

		// Token: 0x060004EA RID: 1258
		protected abstract void WriteEndDocument();

		// Token: 0x060004EB RID: 1259
		protected abstract void WriteStartObject(string localName, bool isInCollection);

		// Token: 0x060004EC RID: 1260
		protected abstract void WriteEndObject();

		// Token: 0x060004ED RID: 1261
		protected abstract void WriteStartArray(string localName);

		// Token: 0x060004EE RID: 1262
		protected abstract void WriteStartArray();

		// Token: 0x060004EF RID: 1263
		protected abstract void WriteEndArray();

		// Token: 0x060004F0 RID: 1264
		protected abstract void WriteVariantValue(string localName, object value, bool isInCollection);

		// Token: 0x060004F1 RID: 1265
		protected abstract void WriteValue(string localName, bool value, bool isInCollection);

		// Token: 0x060004F2 RID: 1266
		protected abstract void WriteValue(string localName, int value, bool isInCollection);

		// Token: 0x060004F3 RID: 1267
		protected abstract void WriteValue(string localName, long value, bool isInCollection);

		// Token: 0x060004F4 RID: 1268
		protected abstract void WriteValue(string localName, string value, bool isInCollection);

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000A6EB File Offset: 0x000088EB
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060004F6 RID: 1270
		protected abstract void Dispose(bool disposing);

		// Token: 0x02000148 RID: 328
		protected static class WriterConstants
		{
			// Token: 0x04000378 RID: 888
			internal const string Aggregated = "Aggregated";

			// Token: 0x04000379 RID: 889
			internal const string Algorithm = "Algorithm";

			// Token: 0x0400037A RID: 890
			internal const string AnyValue = "AnyValue";

			// Token: 0x0400037B RID: 891
			internal const string Apply = "Apply";

			// Token: 0x0400037C RID: 892
			internal const string Binary = "Binary";

			// Token: 0x0400037D RID: 893
			internal const string Bin = "Bin";

			// Token: 0x0400037E RID: 894
			internal const string BinnedLineSample = "BinnedLineSample";

			// Token: 0x0400037F RID: 895
			internal const string Binning = "Binning";

			// Token: 0x04000380 RID: 896
			internal const string Block = "Block";

			// Token: 0x04000381 RID: 897
			internal const string Blocks = "Blocks";

			// Token: 0x04000382 RID: 898
			internal const string Bottom = "Bottom";

			// Token: 0x04000383 RID: 899
			internal const string Calculation = "Calculation";

			// Token: 0x04000384 RID: 900
			internal const string Children = "Children";

			// Token: 0x04000385 RID: 901
			internal const string Code = "Code";

			// Token: 0x04000386 RID: 902
			internal const string Columns = "Columns";

			// Token: 0x04000387 RID: 903
			internal const string Condition = "Condition";

			// Token: 0x04000388 RID: 904
			internal const string Calculations = "Calculations";

			// Token: 0x04000389 RID: 905
			internal const string Compound = "Compound";

			// Token: 0x0400038A RID: 906
			internal const string Conditions = "Conditions";

			// Token: 0x0400038B RID: 907
			internal const string Context = "Context";

			// Token: 0x0400038C RID: 908
			internal const string ContextOnly = "ContextOnly";

			// Token: 0x0400038D RID: 909
			internal const string Count = "Count";

			// Token: 0x0400038E RID: 910
			internal const string DataHierarchy = "DataHierarchy";

			// Token: 0x0400038F RID: 911
			internal const string DataIntersection = "DataIntersection";

			// Token: 0x04000390 RID: 912
			internal const string DataIntersections = "DataIntersections";

			// Token: 0x04000391 RID: 913
			internal const string DataMember = "DataMember";

			// Token: 0x04000392 RID: 914
			internal const string DataMembers = "DataMembers";

			// Token: 0x04000393 RID: 915
			internal const string DataTransform = "DataTransform";

			// Token: 0x04000394 RID: 916
			internal const string DataTransformInput = "DataTransformInput";

			// Token: 0x04000395 RID: 917
			internal const string DataTransformOutput = "DataTransformOutput";

			// Token: 0x04000396 RID: 918
			internal const string DataTransformParameter = "DataTransformParamter";

			// Token: 0x04000397 RID: 919
			internal const string DataTransformTable = "DataTransformTable";

			// Token: 0x04000398 RID: 920
			internal const string DataTransformTableColumn = "DataTransformTableColumn";

			// Token: 0x04000399 RID: 921
			internal const string DataRow = "DataRow";

			// Token: 0x0400039A RID: 922
			internal const string DataRows = "DataRows";

			// Token: 0x0400039B RID: 923
			internal const string DataShape = "DataShape";

			// Token: 0x0400039C RID: 924
			internal const string DataShapeQuery = "DataShapeQuery";

			// Token: 0x0400039D RID: 925
			internal const string DataShapeReference = "DataShapeReference";

			// Token: 0x0400039E RID: 926
			internal const string DataShapes = "DataShapes";

			// Token: 0x0400039F RID: 927
			internal const string DataSourceId = "DataSourceId";

			// Token: 0x040003A0 RID: 928
			internal const string DataSourceName = "DataSourceName";

			// Token: 0x040003A1 RID: 929
			internal const string DataSourceReference = "DataSourceReference";

			// Token: 0x040003A2 RID: 930
			internal const string DataSource = "DataSource";

			// Token: 0x040003A3 RID: 931
			internal const string DataSources = "DataSources";

			// Token: 0x040003A4 RID: 932
			internal const string DataType = "DataType";

			// Token: 0x040003A5 RID: 933
			internal const string DefaultValue = "DefaultValue";

			// Token: 0x040003A6 RID: 934
			internal const string DefaultValueOverridesAncestors = "DefaultValueOverridesAncestors";

			// Token: 0x040003A7 RID: 935
			internal const string DetailGroupIdentity = "DetailGroupIdentity";

			// Token: 0x040003A8 RID: 936
			internal const string Discrete = "Discrete";

			// Token: 0x040003A9 RID: 937
			internal const string DynamicLimit = "DynamicLimit";

			// Token: 0x040003AA RID: 938
			internal const string DynamicLimits = "DynamicLimits";

			// Token: 0x040003AB RID: 939
			internal const string Entities = "Entities";

			// Token: 0x040003AC RID: 940
			internal const string Exists = "Exists";

			// Token: 0x040003AD RID: 941
			internal const string ExistsFilterItem = "ExistsFilterItem";

			// Token: 0x040003AE RID: 942
			internal const string Expression = "Expression";

			// Token: 0x040003AF RID: 943
			internal const string Expressions = "Expressions";

			// Token: 0x040003B0 RID: 944
			internal const string Extends = "Extends";

			// Token: 0x040003B1 RID: 945
			internal const string ExtensionColumn = "ExtensionColumn";

			// Token: 0x040003B2 RID: 946
			internal const string ExtensionEntity = "ExtensionEntity";

			// Token: 0x040003B3 RID: 947
			internal const string ExtensionMeasure = "ExtensionMeasure";

			// Token: 0x040003B4 RID: 948
			internal const string ExtensionSchema = "ExtensionSchema";

			// Token: 0x040003B5 RID: 949
			internal const string DataSourceVariables = "DataSourceVariables";

			// Token: 0x040003B6 RID: 950
			internal const string Filter = "Filter";

			// Token: 0x040003B7 RID: 951
			internal const string FilterEmptyGroups = "FilterEmptyGroups";

			// Token: 0x040003B8 RID: 952
			internal const string Filters = "Filters";

			// Token: 0x040003B9 RID: 953
			internal const string First = "First";

			// Token: 0x040003BA RID: 954
			internal const string Group = "Group";

			// Token: 0x040003BB RID: 955
			internal const string GroupKeys = "GroupKeys";

			// Token: 0x040003BC RID: 956
			internal const string Key = "Key";

			// Token: 0x040003BD RID: 957
			internal const string HasExplicitSubtotal = "HasExplicitSubtotal";

			// Token: 0x040003BE RID: 958
			internal const string Id = "Id";

			// Token: 0x040003BF RID: 959
			internal const string IdentityComparison = "IdentityComparison";

			// Token: 0x040003C0 RID: 960
			internal const string In = "In";

			// Token: 0x040003C1 RID: 961
			internal const string IncludeRestartToken = "IncludeRestartTokens";

			// Token: 0x040003C2 RID: 962
			internal const string InnermostTarget = "InnermostTarget";

			// Token: 0x040003C3 RID: 963
			internal const string Input = "Input";

			// Token: 0x040003C4 RID: 964
			internal const string IntersectionLimit = "IntersectionLimit";

			// Token: 0x040003C5 RID: 965
			internal const string IsIndependent = "IsIndependent";

			// Token: 0x040003C6 RID: 966
			internal const string IsMandatoryConstraint = "IsMandatoryConstraint";

			// Token: 0x040003C7 RID: 967
			internal const string IsStrict = "IsStrict";

			// Token: 0x040003C8 RID: 968
			internal const string InstanceFilters = "InstanceFilters";

			// Token: 0x040003C9 RID: 969
			internal const string ItemPath = "ItemPath";

			// Token: 0x040003CA RID: 970
			internal const string Items = "Items";

			// Token: 0x040003CB RID: 971
			internal const string LeftExpression = "LeftExpression";

			// Token: 0x040003CC RID: 972
			internal const string Levels = "Levels";

			// Token: 0x040003CD RID: 973
			internal const string Limits = "Limits";

			// Token: 0x040003CE RID: 974
			internal const string Limit = "Limit";

			// Token: 0x040003CF RID: 975
			internal const string LimitRef = "LimitRef";

			// Token: 0x040003D0 RID: 976
			internal const string Last = "Last";

			// Token: 0x040003D1 RID: 977
			internal const string Max = "Max";

			// Token: 0x040003D2 RID: 978
			internal const string Measures = "Measures";

			// Token: 0x040003D3 RID: 979
			internal const string Min = "Min";

			// Token: 0x040003D4 RID: 980
			internal const string Name = "Name";

			// Token: 0x040003D5 RID: 981
			internal const string Not = "Not";

			// Token: 0x040003D6 RID: 982
			internal const string ObjectName = "ObjectName";

			// Token: 0x040003D7 RID: 983
			internal const string ObjectType = "ObjectType";

			// Token: 0x040003D8 RID: 984
			internal const string Operator = "Operator";

			// Token: 0x040003D9 RID: 985
			internal const string Output = "Output";

			// Token: 0x040003DA RID: 986
			internal const string OverlappingPointsSample = "OverlappingPointsSample";

			// Token: 0x040003DB RID: 987
			internal const string Parameters = "Parameters";

			// Token: 0x040003DC RID: 988
			internal const string Message = "Message";

			// Token: 0x040003DD RID: 989
			internal const string Messages = "Messages";

			// Token: 0x040003DE RID: 990
			internal const string MaxDynamicSeriesCount = "MaxDynamicSeriesCount";

			// Token: 0x040003DF RID: 991
			internal const string MaxPointsPerSeries = "MaxPointsPerSeries";

			// Token: 0x040003E0 RID: 992
			internal const string MinPointsPerSeries = "MinPointsPerSeries";

			// Token: 0x040003E1 RID: 993
			internal const string NativeReferenceName = "NativeReferenceName";

			// Token: 0x040003E2 RID: 994
			internal const string PreserveKeyPoints = "PreserveKeyPoints";

			// Token: 0x040003E3 RID: 995
			internal const string Primary = "Primary";

			// Token: 0x040003E4 RID: 996
			internal const string PrimaryHierarchy = "PrimaryHierarchy";

			// Token: 0x040003E5 RID: 997
			internal const string PrimaryScalarKey = "PrimaryScalarKey";

			// Token: 0x040003E6 RID: 998
			internal const string PropertyName = "PropertyName";

			// Token: 0x040003E7 RID: 999
			internal const string QueryParameter = "QueryParameter";

			// Token: 0x040003E8 RID: 1000
			internal const string QueryParameters = "QueryParameters";

			// Token: 0x040003E9 RID: 1001
			internal const string RequestedPrimaryLeafCount = "RequestedPrimaryLeafCount";

			// Token: 0x040003EA RID: 1002
			internal const string RestartTokens = "RestartTokens";

			// Token: 0x040003EB RID: 1003
			internal const string RestartMatchingBehavior = "RestartMatchingBehavior";

			// Token: 0x040003EC RID: 1004
			internal const string RespectInstanceFilters = "RespectInstanceFilters";

			// Token: 0x040003ED RID: 1005
			internal const string RightExpression = "RightExpression";

			// Token: 0x040003EE RID: 1006
			internal const string Role = "Role";

			// Token: 0x040003EF RID: 1007
			internal const string Sample = "Sample";

			// Token: 0x040003F0 RID: 1008
			internal const string Secondary = "Secondary";

			// Token: 0x040003F1 RID: 1009
			internal const string SecondaryHierarchy = "SecondaryHierarchy";

			// Token: 0x040003F2 RID: 1010
			internal const string Severity = "Severity";

			// Token: 0x040003F3 RID: 1011
			internal const string ScopeIdDefinition = "ScopeIdDefinition";

			// Token: 0x040003F4 RID: 1012
			internal const string ShowItemsWithNoData = "ShowItemsWithNoData";

			// Token: 0x040003F5 RID: 1013
			internal const string Skip = "Skip";

			// Token: 0x040003F6 RID: 1014
			internal const string SortDirection = "SortDirection";

			// Token: 0x040003F7 RID: 1015
			internal const string SortKeys = "SortKeys";

			// Token: 0x040003F8 RID: 1016
			internal const string StartPosition = "StartPosition";

			// Token: 0x040003F9 RID: 1017
			internal const string SuppressJoinPredicate = "SuppressJoinPredicate";

			// Token: 0x040003FA RID: 1018
			internal const string SuppressSortByMeasureRollup = "SuppressSortByMeasureRollup";

			// Token: 0x040003FB RID: 1019
			internal const string SyncTarget = "SyncTarget";

			// Token: 0x040003FC RID: 1020
			internal const string Table = "Table";

			// Token: 0x040003FD RID: 1021
			internal const string Target = "Target";

			// Token: 0x040003FE RID: 1022
			internal const string TargetIntersectionCount = "TargetIntersectionCount";

			// Token: 0x040003FF RID: 1023
			internal const string Targets = "Targets";

			// Token: 0x04000400 RID: 1024
			internal const string Text = "Text";

			// Token: 0x04000401 RID: 1025
			internal const string Top = "Top";

			// Token: 0x04000402 RID: 1026
			internal const string TelemetryId = "TelemetryId";

			// Token: 0x04000403 RID: 1027
			internal const string TopNPerLevel = "TopNPerLevel";

			// Token: 0x04000404 RID: 1028
			internal const string Transform = "Transform";

			// Token: 0x04000405 RID: 1029
			internal const string Transforms = "Transforms";

			// Token: 0x04000406 RID: 1030
			internal const string Type = "Type";

			// Token: 0x04000407 RID: 1031
			internal const string Unary = "Unary";

			// Token: 0x04000408 RID: 1032
			internal const string Usage = "Usage";

			// Token: 0x04000409 RID: 1033
			internal const string UsageKind = "UsageKind";

			// Token: 0x0400040A RID: 1034
			internal const string Value = "Value";

			// Token: 0x0400040B RID: 1035
			internal const string Values = "Values";

			// Token: 0x0400040C RID: 1036
			internal const string ValueType = "ValueType";

			// Token: 0x0400040D RID: 1037
			internal const string VisualAxis = "VisualAxis";

			// Token: 0x0400040E RID: 1038
			internal const string VisualAxisGroups = "Groups";

			// Token: 0x0400040F RID: 1039
			internal const string VisualAxisGroupMember = "Member";

			// Token: 0x04000410 RID: 1040
			internal const string VisualCalculationMetadata = "VisualCalculationMetadata";

			// Token: 0x04000411 RID: 1041
			internal const string Within = "Within";

			// Token: 0x04000412 RID: 1042
			internal const string Window = "Window";

			// Token: 0x04000413 RID: 1043
			internal const string WindowExpansionState = "WindowExpansionState";

			// Token: 0x04000414 RID: 1044
			internal const string WindowExpansionStates = "WindowExpansionStates";

			// Token: 0x04000415 RID: 1045
			internal const string WindowExpansionValue = "WindowExpansionValue";

			// Token: 0x04000416 RID: 1046
			internal const string WindowExpansionValues = "WindowExpansionValues";

			// Token: 0x04000417 RID: 1047
			internal const string WindowKind = "WindowKind";

			// Token: 0x04000418 RID: 1048
			internal const string X = "X";

			// Token: 0x04000419 RID: 1049
			internal const string Y = "Y";
		}
	}
}
