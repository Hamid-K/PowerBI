using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200182E RID: 6190
	public class TableSortOrder
	{
		// Token: 0x06009CE9 RID: 40169 RVA: 0x00206DCD File Offset: 0x00204FCD
		public static bool IsKnown(TableSortOrder sortOrder)
		{
			return sortOrder == null || !sortOrder.IsEmpty;
		}

		// Token: 0x06009CEA RID: 40170 RVA: 0x00206DDD File Offset: 0x00204FDD
		public TableSortOrder(SortOrder[] sortOrders)
		{
			this.sortOrders = sortOrders;
		}

		// Token: 0x17002876 RID: 10358
		// (get) Token: 0x06009CEB RID: 40171 RVA: 0x00206DEC File Offset: 0x00204FEC
		public bool IsEmpty
		{
			get
			{
				return this.sortOrders.Length == 0;
			}
		}

		// Token: 0x17002877 RID: 10359
		// (get) Token: 0x06009CEC RID: 40172 RVA: 0x00206DF8 File Offset: 0x00204FF8
		public SortOrder[] SortOrders
		{
			get
			{
				return this.sortOrders;
			}
		}

		// Token: 0x06009CED RID: 40173 RVA: 0x00206E00 File Offset: 0x00205000
		public TableSortOrder Invert()
		{
			SortOrder[] array = new SortOrder[this.sortOrders.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SortOrder(this.sortOrders[i].Selector, this.sortOrders[i].Comparer, !this.sortOrders[i].Ascending);
			}
			return new TableSortOrder(array);
		}

		// Token: 0x06009CEE RID: 40174 RVA: 0x00206E74 File Offset: 0x00205074
		public TableSortOrder EliminateConstantSelectors(RecordTypeValue rowType)
		{
			QueryExpression[] array;
			bool[] array2;
			if (!this.IsEmpty && SortQuery.TryGetSelectors(this, rowType, out array, out array2))
			{
				ArrayBuilder<SortOrder> arrayBuilder = new ArrayBuilder<SortOrder>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					SortOrder sortOrder = this.sortOrders[i];
					if (array[i].Kind != QueryExpressionKind.Constant || sortOrder.Comparer != null)
					{
						arrayBuilder.Add(sortOrder);
					}
				}
				if (arrayBuilder.Count != this.sortOrders.Length)
				{
					return new TableSortOrder(arrayBuilder.ToArray());
				}
			}
			return this;
		}

		// Token: 0x06009CEF RID: 40175 RVA: 0x00206EF8 File Offset: 0x002050F8
		public IComparer<Value> ToComparer()
		{
			if (this.sortOrders.Length == 1)
			{
				return TableSortOrder.GetComparer(this.sortOrders[0]);
			}
			IComparer<Value>[] array = new IComparer<Value>[this.sortOrders.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TableSortOrder.GetComparer(this.sortOrders[i]);
			}
			return new TableSortOrder.ComparersComparer(array);
		}

		// Token: 0x06009CF0 RID: 40176 RVA: 0x00206F58 File Offset: 0x00205158
		public IExpression ToExpression()
		{
			IExpression[] array = new IExpression[this.sortOrders.Length];
			for (int i = 0; i < array.Length; i++)
			{
				SortOrder sortOrder = this.sortOrders[i];
				List<VariableInitializer> list = new List<VariableInitializer>();
				if (sortOrder.Selector != null)
				{
					if (sortOrder.Selector.Expression != null)
					{
						FunctionTypeValue asFunctionType = sortOrder.Selector.Type.AsFunctionType;
						IFunctionExpression functionExpression = NormalizationVisitor.Normalize(sortOrder.Selector.Expression, true) as IFunctionExpression;
						if (functionExpression != null)
						{
							list.Add(new VariableInitializer(Identifier.New("KeySelector"), new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
							{
								new ParameterSyntaxNode(Identifier.New(asFunctionType.ParameterName(0)), null)
							}, 1), functionExpression.Expression)));
						}
					}
					if (list.Count == 0)
					{
						return null;
					}
				}
				if (sortOrder.Comparer != null)
				{
					list.Add(new VariableInitializer(Identifier.New("KeyComparer"), new ConstantExpressionSyntaxNode(sortOrder.Comparer)));
				}
				if (!sortOrder.Ascending)
				{
					list.Add(new VariableInitializer(Identifier.New("Ascending"), ConstantExpressionSyntaxNode.False));
				}
				array[i] = new RecordExpressionSyntaxNode(list.ToArray());
			}
			return new ListExpressionSyntaxNode(array);
		}

		// Token: 0x06009CF1 RID: 40177 RVA: 0x00207094 File Offset: 0x00205294
		private static IComparer<Value> GetComparer(SortOrder columnSortOrder)
		{
			IComparer<Value> comparer = TableSortOrder.GetComparer(columnSortOrder.Selector, columnSortOrder.Comparer);
			if (!columnSortOrder.Ascending)
			{
				comparer = new TableSortOrder.DescendingComparer(comparer);
			}
			return comparer;
		}

		// Token: 0x06009CF2 RID: 40178 RVA: 0x002070C6 File Offset: 0x002052C6
		private static IComparer<Value> GetComparer(FunctionValue selector, FunctionValue comparer)
		{
			if (selector == null && comparer == null)
			{
				return _ValueComparer.LaxDefault;
			}
			if (selector == null)
			{
				return new TableSortOrder.ComparerComparer(comparer);
			}
			if (comparer == null)
			{
				return new TableSortOrder.SelectorComparer(selector);
			}
			return new TableSortOrder.SelectorComparerComparer(selector, comparer);
		}

		// Token: 0x04005285 RID: 21125
		private readonly SortOrder[] sortOrders;

		// Token: 0x04005286 RID: 21126
		public static readonly TableSortOrder None = null;

		// Token: 0x04005287 RID: 21127
		public static readonly TableSortOrder Unknown = new TableSortOrder(new SortOrder[0]);

		// Token: 0x0200182F RID: 6191
		private class SelectorComparer : IComparer<Value>
		{
			// Token: 0x06009CF4 RID: 40180 RVA: 0x00207107 File Offset: 0x00205307
			public SelectorComparer(FunctionValue selector)
			{
				this.selector = selector;
			}

			// Token: 0x06009CF5 RID: 40181 RVA: 0x00207116 File Offset: 0x00205316
			public int Compare(Value x, Value y)
			{
				return _ValueComparer.LaxDefault.Compare(this.selector.Invoke(x), this.selector.Invoke(y));
			}

			// Token: 0x04005288 RID: 21128
			private FunctionValue selector;
		}

		// Token: 0x02001830 RID: 6192
		private class ComparerComparer : IComparer<Value>
		{
			// Token: 0x06009CF6 RID: 40182 RVA: 0x0020713A File Offset: 0x0020533A
			public ComparerComparer(FunctionValue comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06009CF7 RID: 40183 RVA: 0x00207149 File Offset: 0x00205349
			public int Compare(Value x, Value y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return this.comparer.Invoke(x, y).AsInteger32;
			}

			// Token: 0x04005289 RID: 21129
			private FunctionValue comparer;
		}

		// Token: 0x02001831 RID: 6193
		private class SelectorComparerComparer : IComparer<Value>
		{
			// Token: 0x06009CF8 RID: 40184 RVA: 0x0020716F File Offset: 0x0020536F
			public SelectorComparerComparer(FunctionValue selector, FunctionValue comparer)
			{
				this.selector = selector;
				this.comparer = comparer;
			}

			// Token: 0x06009CF9 RID: 40185 RVA: 0x00207185 File Offset: 0x00205385
			public int Compare(Value x, Value y)
			{
				return this.comparer.Invoke(this.selector.Invoke(x), this.selector.Invoke(y)).AsInteger32;
			}

			// Token: 0x0400528A RID: 21130
			private FunctionValue selector;

			// Token: 0x0400528B RID: 21131
			private FunctionValue comparer;
		}

		// Token: 0x02001832 RID: 6194
		private class DescendingComparer : IComparer<Value>
		{
			// Token: 0x06009CFA RID: 40186 RVA: 0x002071AF File Offset: 0x002053AF
			public DescendingComparer(IComparer<Value> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06009CFB RID: 40187 RVA: 0x002071BE File Offset: 0x002053BE
			public int Compare(Value x, Value y)
			{
				return -this.comparer.Compare(x, y);
			}

			// Token: 0x0400528C RID: 21132
			private IComparer<Value> comparer;
		}

		// Token: 0x02001833 RID: 6195
		private class ComparersComparer : IComparer<Value>
		{
			// Token: 0x06009CFC RID: 40188 RVA: 0x002071CE File Offset: 0x002053CE
			public ComparersComparer(IComparer<Value>[] comparers)
			{
				this.comparers = comparers;
			}

			// Token: 0x06009CFD RID: 40189 RVA: 0x002071E0 File Offset: 0x002053E0
			public int Compare(Value x, Value y)
			{
				for (int i = 0; i < this.comparers.Length; i++)
				{
					int num = this.comparers[i].Compare(x, y);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x0400528D RID: 21133
			private IComparer<Value>[] comparers;
		}
	}
}
