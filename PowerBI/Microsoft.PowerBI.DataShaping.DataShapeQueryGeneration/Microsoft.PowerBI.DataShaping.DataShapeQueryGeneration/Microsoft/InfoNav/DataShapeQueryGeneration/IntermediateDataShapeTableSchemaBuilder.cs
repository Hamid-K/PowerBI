using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D1 RID: 209
	internal static class IntermediateDataShapeTableSchemaBuilder
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		internal static IntermediateDataShapeTableSchema BuildTableSchema(SelectBindingsBuilder selectBindingsBuilder, Identifier dataShapeId)
		{
			int selectCount = selectBindingsBuilder.SelectCount;
			List<IntermediateTableSchemaColumn> list = new List<IntermediateTableSchemaColumn>(selectCount);
			for (int i = 0; i < selectCount; i++)
			{
				list.Add(IntermediateDataShapeTableSchemaBuilder.CreateSchemaColumn(selectBindingsBuilder, i));
			}
			return new IntermediateDataShapeTableSchema(dataShapeId.StructureReference(), list);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001C210 File Offset: 0x0001A410
		private static IntermediateTableSchemaColumn CreateSchemaColumn(SelectBindingsBuilder selectBindingsBuilder, int selectIndex)
		{
			SelectBinding selectBinding = selectBindingsBuilder.GetSelectBinding(selectIndex);
			if (selectBinding == null)
			{
				return null;
			}
			if (selectBinding.Value == null)
			{
				return null;
			}
			List<IntermediateTableSchemaKey> list = null;
			IReadOnlyList<SelectIdentityKeyBuilder> selectGroupKeys = selectBindingsBuilder.GetSelectGroupKeys(selectIndex);
			if (!selectGroupKeys.IsNullOrEmpty<SelectIdentityKeyBuilder>())
			{
				list = new List<IntermediateTableSchemaKey>(selectGroupKeys.Count);
				foreach (SelectIdentityKeyBuilder selectIdentityKeyBuilder in selectGroupKeys)
				{
					string text = selectIdentityKeyBuilder.ResolveCalcId(new Func<int, string>(selectBindingsBuilder.GetCalcIdForSelect));
					list.Add(new IntermediateTableSchemaKey(text, selectIdentityKeyBuilder.InternalLineageColumn));
				}
			}
			List<IntermediateTableSchemaKey> list2 = null;
			IReadOnlyList<SelectSortIdentityKeyBuilder> selectSortKeys = selectBindingsBuilder.GetSelectSortKeys(selectIndex);
			if (!selectSortKeys.IsNullOrEmpty<SelectSortIdentityKeyBuilder>())
			{
				list2 = new List<IntermediateTableSchemaKey>(selectSortKeys.Count);
				foreach (SelectSortIdentityKeyBuilder selectSortIdentityKeyBuilder in selectSortKeys)
				{
					string calc = selectSortIdentityKeyBuilder.Calc;
					list2.Add(new IntermediateTableSchemaKey(calc, selectSortIdentityKeyBuilder.InternalLineageColumn));
				}
			}
			return new IntermediateTableSchemaColumn(selectBinding.Name, selectBinding.Value, selectBinding.Format, selectBindingsBuilder.GetLineageProperty(selectIndex), list, list2);
		}
	}
}
