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
	// Token: 0x02001662 RID: 5730
	internal class TakeWhileTableValue : OptimizableTableValue
	{
		// Token: 0x060090F2 RID: 37106 RVA: 0x001E1D6E File Offset: 0x001DFF6E
		public TakeWhileTableValue(TableValue table, FunctionValue condition)
		{
			this.table = table;
			this.condition = condition;
		}

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x060090F3 RID: 37107 RVA: 0x001E1D84 File Offset: 0x001DFF84
		public override TypeValue Type
		{
			get
			{
				return this.table.Type;
			}
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x060090F4 RID: 37108 RVA: 0x001E1D91 File Offset: 0x001DFF91
		public override Keys Columns
		{
			get
			{
				return this.table.Columns;
			}
		}

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x060090F5 RID: 37109 RVA: 0x001E1D9E File Offset: 0x001DFF9E
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.table.SortOrder;
			}
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x060090F6 RID: 37110 RVA: 0x001E1DAB File Offset: 0x001DFFAB
		public override IList<Relationship> Relationships
		{
			get
			{
				return this.table.Relationships;
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x060090F7 RID: 37111 RVA: 0x001E1DB8 File Offset: 0x001DFFB8
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					this.expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.FirstN), this.table.Expression, new ConstantExpressionSyntaxNode(this.condition));
				}
				return this.expression;
			}
		}

		// Token: 0x060090F8 RID: 37112 RVA: 0x001E1DF3 File Offset: 0x001DFFF3
		public override TableValue Optimize()
		{
			return new OptimizedTableValue(new TakeWhileTableValue(this.table.Optimize(), this.condition));
		}

		// Token: 0x060090F9 RID: 37113 RVA: 0x001E1E10 File Offset: 0x001E0010
		public override TableValue Unordered()
		{
			if (this.table.SortOrder == TableSortOrder.None)
			{
				return this;
			}
			return new TakeWhileTableValue(this.table.Unordered(), this.condition);
		}

		// Token: 0x060090FA RID: 37114 RVA: 0x001E1E3C File Offset: 0x001E003C
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new TakeWhileTableValue.TakeWhileEnumerator(this.table, this.condition);
		}

		// Token: 0x04004DD3 RID: 19923
		private readonly TableValue table;

		// Token: 0x04004DD4 RID: 19924
		private readonly FunctionValue condition;

		// Token: 0x04004DD5 RID: 19925
		private IExpression expression;

		// Token: 0x02001663 RID: 5731
		private class TakeWhileEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060090FB RID: 37115 RVA: 0x001E1E4F File Offset: 0x001E004F
			public TakeWhileEnumerator(TableValue table, FunctionValue condition)
			{
				this.enumerator = table.GetEnumerator();
				this.condition = condition;
			}

			// Token: 0x170025EA RID: 9706
			// (get) Token: 0x060090FC RID: 37116 RVA: 0x001E1E6A File Offset: 0x001E006A
			public IValueReference Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x170025EB RID: 9707
			// (get) Token: 0x060090FD RID: 37117 RVA: 0x001E1E77 File Offset: 0x001E0077
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060090FE RID: 37118 RVA: 0x001E1E7F File Offset: 0x001E007F
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x060090FF RID: 37119 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06009100 RID: 37120 RVA: 0x001E1E8C File Offset: 0x001E008C
			public bool MoveNext()
			{
				if (this.condition == null)
				{
					return false;
				}
				if (!this.enumerator.MoveNext())
				{
					return false;
				}
				if (this.condition.Invoke(this.enumerator.Current.Value).AsBoolean)
				{
					return true;
				}
				this.condition = null;
				return false;
			}

			// Token: 0x04004DD6 RID: 19926
			private readonly IEnumerator<IValueReference> enumerator;

			// Token: 0x04004DD7 RID: 19927
			private FunctionValue condition;
		}
	}
}
