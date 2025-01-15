using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000611 RID: 1553
	internal sealed class FalseExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x06004B7B RID: 19323 RVA: 0x0010A94D File Offset: 0x00108B4D
		private FalseExpr()
		{
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004B7C RID: 19324 RVA: 0x0010A955 File Offset: 0x00108B55
		internal static FalseExpr<T_Identifier> Value
		{
			get
			{
				return FalseExpr<T_Identifier>._value;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x0010A95C File Offset: 0x00108B5C
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.False;
			}
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x0010A95F File Offset: 0x00108B5F
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitFalse(this);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x0010A968 File Offset: 0x00108B68
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return TrueExpr<T_Identifier>.Value;
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x0010A96F File Offset: 0x00108B6F
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this == other;
		}

		// Token: 0x04001A69 RID: 6761
		private static readonly FalseExpr<T_Identifier> _value = new FalseExpr<T_Identifier>();
	}
}
