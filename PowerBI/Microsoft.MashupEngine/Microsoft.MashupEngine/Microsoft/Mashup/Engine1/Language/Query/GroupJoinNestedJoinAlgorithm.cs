using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017F8 RID: 6136
	public class GroupJoinNestedJoinAlgorithm : NestedJoinAlgorithm
	{
		// Token: 0x06009AEC RID: 39660 RVA: 0x00200DD4 File Offset: 0x001FEFD4
		public override IEnumerable<IValueReference> NestedJoin(NestedJoinParameters parameters)
		{
			TableValue tableValue = new QueryTableValue(parameters.LeftQuery);
			TextValue textValue = TextValue.New(parameters.NewColumnName);
			TableValue asTable = parameters.RightTable.AsTable;
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(QueryHelpers.EachFunctionType, new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), GroupJoinNestedJoinAlgorithm.IdentifiersFromKeys(asTable.Columns)));
			return this.Invoke(tableValue, parameters.LeftKeyColumns, asTable, TableValue.GetColumns(parameters.RightTable.AsTable.Columns, parameters.RightKey), textValue, functionExpression, parameters);
		}

		// Token: 0x06009AED RID: 39661 RVA: 0x00200E58 File Offset: 0x001FF058
		protected TableValue Invoke(TableValue leftTable, int[] leftKeyColumns, TableValue rightTable, int[] rightKeyColumns, TextValue newColumnName, IFunctionExpression newColumnExpression, NestedJoinParameters parameters)
		{
			Keys columns = leftTable.Columns;
			leftTable = leftTable.SelectColumns(new ColumnSelection(JoinQuery.EnsureUniqueKeys(leftTable.Columns, rightTable.Columns)));
			int[] array;
			bool flag;
			leftTable = GroupHelpers.EnsureTableKey(leftTable, out array, out flag);
			TableValue tableValue = this.GetJoinTableValue(leftTable, leftKeyColumns, rightTable, rightKeyColumns, parameters);
			Compiler compiler = new Compiler(CompileOptions.None);
			string text = Guid.NewGuid().ToString();
			ListValue listValue = ListValue.New(new Value[]
			{
				TextValue.New(text),
				compiler.ToFunction(newColumnExpression),
				rightTable.Type
			});
			tableValue = GroupHelpers.GroupWithPassthrough(tableValue, array, new ColumnSelection(leftTable.Columns), ListValue.New(new Value[] { listValue }));
			ColumnSelection columnSelection = new ColumnSelection(columns).Add(newColumnName.AsString, tableValue.Columns.Length - 1);
			return tableValue.SelectColumns(columnSelection);
		}

		// Token: 0x06009AEE RID: 39662 RVA: 0x00200F3A File Offset: 0x001FF13A
		protected virtual TableValue GetJoinTableValue(TableValue leftTable, int[] leftKeyColumns, TableValue rightTable, int[] rightKeyColumns, NestedJoinParameters parameters)
		{
			return TableValue.Join(leftTable, leftKeyColumns, rightTable, rightKeyColumns, parameters.JoinKind, JoinAlgorithm.Dynamic, null);
		}

		// Token: 0x06009AEF RID: 39663 RVA: 0x00200F54 File Offset: 0x001FF154
		protected static Identifier[] IdentifiersFromKeys(Keys keys)
		{
			Identifier[] array = new Identifier[keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Identifier.New(keys[i]);
			}
			return array;
		}
	}
}
