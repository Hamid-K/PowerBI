using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200132D RID: 4909
	public static class GroupHelpers
	{
		// Token: 0x060081BC RID: 33212 RVA: 0x001B8BD0 File Offset: 0x001B6DD0
		public static TableValue EnsureTableKey(TableValue table, out int[] key, out bool generatedKey)
		{
			generatedKey = !GroupHelpers.TryGetKey(table.Type.AsTableType, out key);
			if (generatedKey)
			{
				Keys keys = Keys.New(JoinQuery.EnsureUniqueKey("row_index", table.Columns));
				table = TableModule.Table.AddIndexColumn.Invoke(table, TextValue.New(keys[0])).AsTable;
				key = new int[] { table.Columns.Length - 1 };
			}
			return table;
		}

		// Token: 0x060081BD RID: 33213 RVA: 0x001B8C44 File Offset: 0x001B6E44
		public static TableValue GroupWithPassthrough(TableValue table, int[] keyColumns, ColumnSelection passthroughColumns, ListValue aggregations)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < passthroughColumns.Keys.Length; i++)
			{
				int column = passthroughColumns.GetColumn(i);
				if (Array.IndexOf<int>(keyColumns, column) != -1 || GroupHelpers.IsComparable(table.GetColumnType(column)))
				{
					list.Add(column);
				}
				else
				{
					list2.Add(column);
				}
			}
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < passthroughColumns.Keys.Length; j++)
			{
				int column2 = passthroughColumns.GetColumn(j);
				string text = passthroughColumns.Keys[j];
				if (Array.IndexOf<int>(keyColumns, column2) != -1 || GroupHelpers.IsComparable(table.GetColumnType(column2)))
				{
					columnSelectionBuilder.Add(text, num);
					num++;
				}
				else
				{
					columnSelectionBuilder.Add(text, list.Count + num2);
					num2++;
				}
			}
			for (int k = 0; k < aggregations.Count; k++)
			{
				columnSelectionBuilder.Add(aggregations[k].AsList.Item0.AsString);
			}
			TextValue[] array = new TextValue[list.Count];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = TextValue.New(table.Columns[list[l]]);
			}
			Value[] array2 = array;
			ListValue listValue = ListValue.New(array2);
			Compiler compiler = new Compiler(CompileOptions.None);
			Value[] array3 = new Value[list2.Count + aggregations.Count];
			for (int m = 0; m < list2.Count; m++)
			{
				string text2 = table.Columns[list2[m]];
				IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.List.First), new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), text2)));
				array3[m] = ListValue.New(new Value[]
				{
					TextValue.New(text2),
					compiler.ToFunction(functionExpression)
				});
			}
			for (int n = 0; n < aggregations.Count; n++)
			{
				array3[list2.Count + n] = aggregations[n];
			}
			ListValue listValue2 = ListValue.New(array3);
			return table.Group(listValue, listValue2, Library.GroupKind.Global, Value.Null).SelectColumns(columnSelectionBuilder.ToColumnSelection());
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x001B8E9B File Offset: 0x001B709B
		private static bool IsComparable(TypeValue type)
		{
			return TypeServices.IsScalar(type) && type.TypeKind != ValueKind.Binary;
		}

		// Token: 0x060081BF RID: 33215 RVA: 0x001B8EB4 File Offset: 0x001B70B4
		private static bool TryGetKey(TableTypeValue tableType, out int[] key)
		{
			key = null;
			foreach (TableKey tableKey in tableType.TableKeys)
			{
				bool primary = tableKey.Primary;
				if (key == null || primary)
				{
					key = tableKey.Columns;
					if (primary)
					{
						break;
					}
				}
			}
			return key != null;
		}

		// Token: 0x040046A0 RID: 18080
		private const string rowCountCol = "row_index";
	}
}
