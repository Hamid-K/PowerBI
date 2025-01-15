using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012A1 RID: 4769
	internal sealed class ColumnReferenceListValue : FoldableListValue
	{
		// Token: 0x06007D35 RID: 32053 RVA: 0x001AD630 File Offset: 0x001AB830
		public ColumnReferenceListValue(TableValue table, string column)
		{
			this.table = table;
			this.column = column;
			TypeValue typeValue = RecordTypeAlgebra.Field(table.Type.AsTableType.ItemType, column);
			this.type = ListTypeValue.New(typeValue);
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x06007D36 RID: 32054 RVA: 0x001AD674 File Offset: 0x001AB874
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x06007D37 RID: 32055 RVA: 0x001AD67C File Offset: 0x001AB87C
		public override long LargeCount
		{
			get
			{
				return this.table.LargeCount;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x06007D38 RID: 32056 RVA: 0x001AD689 File Offset: 0x001AB889
		public override int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x06007D39 RID: 32057 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsBuffered
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x001AD696 File Offset: 0x001AB896
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			foreach (IValueReference valueReference in this.table)
			{
				yield return new RequiredFieldAccessValueReference(valueReference, this.column);
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x001AD6A5 File Offset: 0x001AB8A5
		public override IValueReference GetReference(int index)
		{
			return this.table[index][this.column];
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x001AD6C0 File Offset: 0x001AB8C0
		public override Value Aggregate(FunctionValue aggregate)
		{
			Grouping grouping = new Grouping(false, Keys.New(this.column), Keys.Empty, EmptyArray<int>.Instance, new ColumnConstructor[]
			{
				new ColumnConstructor(this.column, this.ConvertToColumnReference(aggregate))
			}, true, null, this.table.Type.AsTableType);
			Value value;
			using (IEnumerator<IValueReference> enumerator = this.table.Group(grouping).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					value = enumerator.Current.Value[this.column];
				}
				else
				{
					value = Value.Null;
				}
			}
			return value;
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x001AD76C File Offset: 0x001AB96C
		public override ListValue Select(FunctionValue selection)
		{
			return new ColumnReferenceListValue(this.table.SelectRows(this.ConvertToColumnReference(selection)), this.column);
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x001AD78C File Offset: 0x001AB98C
		public override ListValue Distinct()
		{
			FunctionValue functionValue = new TableValue.ColumnSelectorFunctionValue(this.column, 0);
			return new ColumnReferenceListValue(this.table.Distinct(new TableDistinct(new Distinct[]
			{
				new Distinct(functionValue, null)
			})), this.column);
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x001AD7D8 File Offset: 0x001AB9D8
		public override ListValue Sort(bool ascending)
		{
			FunctionValue functionValue = new TableValue.ColumnSelectorFunctionValue(this.column, 0);
			return new ColumnReferenceListValue(this.table.Sort(new TableSortOrder(new SortOrder[]
			{
				new SortOrder(functionValue, null, ascending)
			})).AsTable, this.column);
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x001AD827 File Offset: 0x001ABA27
		public override ListValue Transform(FunctionValue transform)
		{
			return new ColumnReferenceListValue(this.table.TransformColumns(ListValue.Empty, transform, Value.Null).AsTable, this.column);
		}

		// Token: 0x06007D41 RID: 32065 RVA: 0x001AD84F File Offset: 0x001ABA4F
		public override ListValue Skip(RowCount count)
		{
			return new ColumnReferenceListValue(this.table.Skip(count), this.column);
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x001AD868 File Offset: 0x001ABA68
		public override ListValue Take(RowCount count)
		{
			return new ColumnReferenceListValue(this.table.Take(count), this.column);
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x001AD884 File Offset: 0x001ABA84
		private FunctionValue ConvertToColumnReference(FunctionValue function)
		{
			Compiler compiler = new Compiler(CompileOptions.None);
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(function), new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), this.column)));
			return compiler.ToFunction(functionExpression);
		}

		// Token: 0x040044FC RID: 17660
		private readonly TableValue table;

		// Token: 0x040044FD RID: 17661
		private readonly string column;

		// Token: 0x040044FE RID: 17662
		private readonly TypeValue type;
	}
}
