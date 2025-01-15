using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A8 RID: 2472
	internal sealed class BinaryMdxExpression : MdxExpression
	{
		// Token: 0x06004698 RID: 18072 RVA: 0x000ECC64 File Offset: 0x000EAE64
		public BinaryMdxExpression(BinaryOperator2 op, MdxExpression left, MdxExpression right)
		{
			this.left = left;
			this.right = right;
			this.op = op;
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x000ECC81 File Offset: 0x000EAE81
		public override bool IsComplex
		{
			get
			{
				return this.left.IsComplex || this.right.IsComplex;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x000ECC9D File Offset: 0x000EAE9D
		public MdxExpression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x000ECCA5 File Offset: 0x000EAEA5
		public MdxExpression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x000ECCAD File Offset: 0x000EAEAD
		public BinaryOperator2 Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000ECCB8 File Offset: 0x000EAEB8
		public bool Equals(BinaryMdxExpression other)
		{
			if (other != null && this.op == other.op)
			{
				BinaryOperator2 binaryOperator = this.op;
				bool flag = binaryOperator - BinaryOperator2.Equals <= 3;
				return (this.left.Equals(other.left) && this.right.Equals(other.right)) || (flag && this.left.Equals(other.right) && this.right.Equals(other.left));
			}
			return false;
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x000ECD3C File Offset: 0x000EAF3C
		public override bool Equals(object other)
		{
			return this.Equals(other as BinaryMdxExpression);
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x000ECD4C File Offset: 0x000EAF4C
		public override int GetHashCode()
		{
			return this.op.GetHashCode() * 37 + this.left.GetHashCode() + this.right.GetHashCode();
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x000ECD88 File Offset: 0x000EAF88
		public override void Write(MdxExpressionWriter writer)
		{
			writer.Write("(");
			if (this.IsComplex)
			{
				using (writer.NewScope())
				{
					this.Left.Write(writer);
				}
				writer.Write(BinaryMdxExpression.ToString(this.op));
				using (writer.NewScope())
				{
					this.Right.Write(writer);
					goto IL_0093;
				}
			}
			this.Left.Write(writer);
			writer.Write(BinaryMdxExpression.ToString(this.op));
			this.Right.Write(writer);
			IL_0093:
			writer.Write(")");
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x000ECE50 File Offset: 0x000EB050
		private static string ToString(BinaryOperator2 op)
		{
			switch (op)
			{
			case BinaryOperator2.Multiply:
				return "*";
			case BinaryOperator2.GreaterThan:
				return ">";
			case BinaryOperator2.LessThan:
				return "<";
			case BinaryOperator2.GreaterThanOrEquals:
				return ">=";
			case BinaryOperator2.LessThanOrEquals:
				return "<=";
			case BinaryOperator2.Equals:
				return "=";
			case BinaryOperator2.NotEquals:
				return "<>";
			case BinaryOperator2.And:
				return "AND";
			case BinaryOperator2.Or:
				return "OR";
			}
			throw new InvalidOperationException("Unexpected operator " + op.ToString() + ".");
		}

		// Token: 0x04002572 RID: 9586
		private readonly MdxExpression left;

		// Token: 0x04002573 RID: 9587
		private readonly MdxExpression right;

		// Token: 0x04002574 RID: 9588
		private readonly BinaryOperator2 op;
	}
}
