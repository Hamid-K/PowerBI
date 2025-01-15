using System;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200061A RID: 1562
	internal sealed class NotExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x06004BAA RID: 19370 RVA: 0x0010AE86 File Offset: 0x00109086
		internal NotExpr(BoolExpr<T_Identifier> child)
			: base(new BoolExpr<T_Identifier>[] { child })
		{
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06004BAB RID: 19371 RVA: 0x0010AE98 File Offset: 0x00109098
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Not;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x0010AE9B File Offset: 0x0010909B
		internal BoolExpr<T_Identifier> Child
		{
			get
			{
				return base.Children.First<BoolExpr<T_Identifier>>();
			}
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0010AEA8 File Offset: 0x001090A8
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitNot(this);
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0010AEB1 File Offset: 0x001090B1
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "!{0}", new object[] { this.Child });
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0010AED1 File Offset: 0x001090D1
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return this.Child;
		}
	}
}
