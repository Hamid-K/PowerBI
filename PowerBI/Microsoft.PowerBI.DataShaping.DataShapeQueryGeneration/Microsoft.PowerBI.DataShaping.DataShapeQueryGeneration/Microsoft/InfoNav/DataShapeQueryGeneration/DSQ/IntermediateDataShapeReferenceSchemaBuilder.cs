using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x0200010E RID: 270
	internal static class IntermediateDataShapeReferenceSchemaBuilder
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x000234AC File Offset: 0x000216AC
		internal static IntermediateDataShapeReferenceSchema BuildSchema(SelectBindingsBuilder selectBindingsBuilder, Identifier dataShapeId, DataShapeExpressionsAxisBuilder primaryAxisBuilder, DataShapeExpressionsAxisBuilder secondaryAxisBuilder)
		{
			List<IntermediateGroupSchema> list = IntermediateDataShapeReferenceSchemaBuilder.CreateGroupingSchemas(primaryAxisBuilder, selectBindingsBuilder);
			List<IntermediateGroupSchema> list2 = IntermediateDataShapeReferenceSchemaBuilder.CreateGroupingSchemas(secondaryAxisBuilder, selectBindingsBuilder);
			return new IntermediateDataShapeReferenceSchema(dataShapeId.StructureReference(), list, list2);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000234D8 File Offset: 0x000216D8
		private static List<IntermediateGroupSchema> CreateGroupingSchemas(DataShapeExpressionsAxisBuilder axisBuilder, SelectBindingsBuilder selectBindingsBuilder)
		{
			List<IntermediateGroupSchema> list = null;
			if (axisBuilder.Groupings != null)
			{
				list = new List<IntermediateGroupSchema>(axisBuilder.Groupings.Count);
				foreach (DataShapeExpressionsAxisGroupingBuilder dataShapeExpressionsAxisGroupingBuilder in axisBuilder.Groupings)
				{
					list.Add(IntermediateDataShapeReferenceSchemaBuilder.CreateGroupSchema(dataShapeExpressionsAxisGroupingBuilder, selectBindingsBuilder));
				}
			}
			return list;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00023548 File Offset: 0x00021748
		internal static IntermediateGroupSchema CreateGroupSchema(DataShapeExpressionsAxisGroupingBuilder groupingBuilder, SelectBindingsBuilder selectBindingsBuilder)
		{
			List<IntermediateGroupingKey> list = null;
			HashSet<int> hashSet = new HashSet<int>();
			if (!groupingBuilder.Keys.IsNullOrEmpty<DataShapeExpressionsAxisGroupingKeyBuilder>())
			{
				list = new List<IntermediateGroupingKey>(groupingBuilder.Keys.Count);
				foreach (DataShapeExpressionsAxisGroupingKeyBuilder dataShapeExpressionsAxisGroupingKeyBuilder in groupingBuilder.Keys)
				{
					string text = dataShapeExpressionsAxisGroupingKeyBuilder.ResolveCalcId(new Func<int, string>(selectBindingsBuilder.GetCalcIdForSelect));
					list.Add(new IntermediateGroupingKey(text.StructureReference(), dataShapeExpressionsAxisGroupingKeyBuilder.SourceField, dataShapeExpressionsAxisGroupingKeyBuilder.Select, dataShapeExpressionsAxisGroupingKeyBuilder.SelectIndicesWithThisIdentity, selectBindingsBuilder.GetFormatStringForSelect(dataShapeExpressionsAxisGroupingKeyBuilder.Select), dataShapeExpressionsAxisGroupingKeyBuilder.IsIdentityKey));
					if (dataShapeExpressionsAxisGroupingKeyBuilder.SelectIndicesWithThisIdentity != null)
					{
						hashSet.UnionWith(dataShapeExpressionsAxisGroupingKeyBuilder.SelectIndicesWithThisIdentity);
					}
				}
			}
			List<IntermediateSortingKey> list2 = null;
			if (!groupingBuilder.SortKeys.IsNullOrEmpty<DataShapeExpressionsAxisGroupingSortKey>())
			{
				list2 = new List<IntermediateSortingKey>(groupingBuilder.SortKeys.Count);
				foreach (DataShapeExpressionsAxisGroupingSortKey dataShapeExpressionsAxisGroupingSortKey in groupingBuilder.SortKeys)
				{
					list2.Add(new IntermediateSortingKey(dataShapeExpressionsAxisGroupingSortKey.Calc.StructureReference(), dataShapeExpressionsAxisGroupingSortKey.SortDirection, dataShapeExpressionsAxisGroupingSortKey.SourceField, dataShapeExpressionsAxisGroupingSortKey.Select));
				}
			}
			List<IntermediateGroupingDetailValue> list3 = null;
			if (hashSet.Count > 0)
			{
				list3 = new List<IntermediateGroupingDetailValue>(hashSet.Count);
				foreach (int num in hashSet)
				{
					string calcIdForSelect = selectBindingsBuilder.GetCalcIdForSelect(num);
					IConceptualColumn conceptualColumn = selectBindingsBuilder.GetLineageProperty(num) as IConceptualColumn;
					string formatStringForSelect = selectBindingsBuilder.GetFormatStringForSelect(new int?(num));
					IntermediateGroupingDetailValue intermediateGroupingDetailValue = new IntermediateGroupingDetailValue(calcIdForSelect.StructureReference(), num, conceptualColumn, formatStringForSelect);
					list3.Add(intermediateGroupingDetailValue);
				}
			}
			ExpressionNode expressionNode = groupingBuilder.Member.StructureReference();
			string subtotalMember = groupingBuilder.SubtotalMember;
			return new IntermediateGroupSchema(expressionNode, (subtotalMember != null) ? subtotalMember.StructureReference() : null, groupingBuilder.SubtotalType, list, list2, list3);
		}
	}
}
