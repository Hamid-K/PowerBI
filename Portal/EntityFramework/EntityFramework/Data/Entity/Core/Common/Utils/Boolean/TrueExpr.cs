using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000624 RID: 1572
	internal sealed class TrueExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x06004BF6 RID: 19446 RVA: 0x0010B919 File Offset: 0x00109B19
		private TrueExpr()
		{
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004BF7 RID: 19447 RVA: 0x0010B921 File Offset: 0x00109B21
		internal static TrueExpr<T_Identifier> Value
		{
			get
			{
				return TrueExpr<T_Identifier>._value;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0010B928 File Offset: 0x00109B28
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.True;
			}
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0010B92B File Offset: 0x00109B2B
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitTrue(this);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0010B934 File Offset: 0x00109B34
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return FalseExpr<T_Identifier>.Value;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0010B93B File Offset: 0x00109B3B
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this == other;
		}

		// Token: 0x04001A85 RID: 6789
		private static readonly TrueExpr<T_Identifier> _value = new TrueExpr<T_Identifier>();
	}
}
