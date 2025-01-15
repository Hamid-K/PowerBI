using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000623 RID: 1571
	internal abstract class TreeExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x06004BF0 RID: 19440 RVA: 0x0010B889 File Offset: 0x00109A89
		protected TreeExpr(IEnumerable<BoolExpr<T_Identifier>> children)
		{
			this._children = new Set<BoolExpr<T_Identifier>>(children);
			this._children.MakeReadOnly();
			this._hashCode = this._children.GetElementsHashCode();
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x0010B8BA File Offset: 0x00109ABA
		internal Set<BoolExpr<T_Identifier>> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0010B8C2 File Offset: 0x00109AC2
		public override bool Equals(object obj)
		{
			return base.Equals(obj as BoolExpr<T_Identifier>);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0010B8D0 File Offset: 0x00109AD0
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x0010B8D8 File Offset: 0x00109AD8
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}({1})", new object[] { this.ExprType, this._children });
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0010B901 File Offset: 0x00109B01
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return ((TreeExpr<T_Identifier>)other).Children.SetEquals(this.Children);
		}

		// Token: 0x04001A83 RID: 6787
		private readonly Set<BoolExpr<T_Identifier>> _children;

		// Token: 0x04001A84 RID: 6788
		private readonly int _hashCode;
	}
}
