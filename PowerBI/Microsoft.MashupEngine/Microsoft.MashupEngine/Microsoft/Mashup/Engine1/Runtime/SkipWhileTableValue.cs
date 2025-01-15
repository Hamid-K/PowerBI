using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001660 RID: 5728
	internal class SkipWhileTableValue : OptimizableTableValue
	{
		// Token: 0x060090E3 RID: 37091 RVA: 0x001E1BF6 File Offset: 0x001DFDF6
		public SkipWhileTableValue(TableValue table, FunctionValue condition)
		{
			this.table = table;
			this.condition = condition;
		}

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x060090E4 RID: 37092 RVA: 0x001E1C0C File Offset: 0x001DFE0C
		public override TypeValue Type
		{
			get
			{
				return this.table.Type;
			}
		}

		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x060090E5 RID: 37093 RVA: 0x001E1C19 File Offset: 0x001DFE19
		public override Keys Columns
		{
			get
			{
				return this.table.Columns;
			}
		}

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x060090E6 RID: 37094 RVA: 0x001E1C26 File Offset: 0x001DFE26
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.table.SortOrder;
			}
		}

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x060090E7 RID: 37095 RVA: 0x001E1C33 File Offset: 0x001DFE33
		public override IList<Relationship> Relationships
		{
			get
			{
				return this.table.Relationships;
			}
		}

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x060090E8 RID: 37096 RVA: 0x001E1C40 File Offset: 0x001DFE40
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					this.expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.Skip), this.table.Expression, new ConstantExpressionSyntaxNode(this.condition));
				}
				return this.expression;
			}
		}

		// Token: 0x060090E9 RID: 37097 RVA: 0x001E1C7B File Offset: 0x001DFE7B
		public override TableValue Optimize()
		{
			return new OptimizedTableValue(new SkipWhileTableValue(this.table.Optimize(), this.condition));
		}

		// Token: 0x060090EA RID: 37098 RVA: 0x001E1C98 File Offset: 0x001DFE98
		public override TableValue Unordered()
		{
			if (this.table.SortOrder == TableSortOrder.None)
			{
				return this;
			}
			return new SkipWhileTableValue(this.table.Unordered(), this.condition);
		}

		// Token: 0x060090EB RID: 37099 RVA: 0x001E1CC4 File Offset: 0x001DFEC4
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new SkipWhileTableValue.SkipWhileEnumerator(this.table, this.condition);
		}

		// Token: 0x04004DCE RID: 19918
		private readonly TableValue table;

		// Token: 0x04004DCF RID: 19919
		private readonly FunctionValue condition;

		// Token: 0x04004DD0 RID: 19920
		private IExpression expression;

		// Token: 0x02001661 RID: 5729
		private class SkipWhileEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060090EC RID: 37100 RVA: 0x001E1CD7 File Offset: 0x001DFED7
			public SkipWhileEnumerator(TableValue table, FunctionValue condition)
			{
				this.enumerator = table.GetEnumerator();
				this.condition = condition;
			}

			// Token: 0x170025E3 RID: 9699
			// (get) Token: 0x060090ED RID: 37101 RVA: 0x001E1CF2 File Offset: 0x001DFEF2
			public IValueReference Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x170025E4 RID: 9700
			// (get) Token: 0x060090EE RID: 37102 RVA: 0x001E1CFF File Offset: 0x001DFEFF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060090EF RID: 37103 RVA: 0x001E1D07 File Offset: 0x001DFF07
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x060090F0 RID: 37104 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060090F1 RID: 37105 RVA: 0x001E1D14 File Offset: 0x001DFF14
			public bool MoveNext()
			{
				if (this.condition == null)
				{
					return this.enumerator.MoveNext();
				}
				while (this.enumerator.MoveNext())
				{
					if (!this.condition.Invoke(this.enumerator.Current.Value).AsBoolean)
					{
						this.condition = null;
						return true;
					}
				}
				return false;
			}

			// Token: 0x04004DD1 RID: 19921
			private readonly IEnumerator<IValueReference> enumerator;

			// Token: 0x04004DD2 RID: 19922
			private FunctionValue condition;
		}
	}
}
