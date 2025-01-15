using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A7 RID: 2471
	internal sealed class UnaryMdxExpression : MdxExpression
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x000ECB6D File Offset: 0x000EAD6D
		public UnaryMdxExpression(MdxUnaryOperators op, MdxExpression operand)
		{
			this.operand = operand;
			this.op = op;
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x000ECB83 File Offset: 0x000EAD83
		public override bool IsComplex
		{
			get
			{
				return this.operand.IsComplex;
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06004692 RID: 18066 RVA: 0x000ECB90 File Offset: 0x000EAD90
		public MdxExpression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x000ECB98 File Offset: 0x000EAD98
		public MdxUnaryOperators Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000ECBA0 File Offset: 0x000EADA0
		public bool Equals(UnaryMdxExpression other)
		{
			return other != null && this.op == other.op && this.operand.Equals(other.operand);
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000ECBC6 File Offset: 0x000EADC6
		public override bool Equals(object other)
		{
			return this.Equals(other as UnaryMdxExpression);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000ECBD4 File Offset: 0x000EADD4
		public override int GetHashCode()
		{
			return this.op.GetHashCode() + 5011 * this.operand.GetHashCode();
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000ECC08 File Offset: 0x000EAE08
		public override void Write(MdxExpressionWriter writer)
		{
			if (this.Operator == MdxUnaryOperators.Not)
			{
				writer.Write("NOT");
				writer.Write("(");
				this.Operand.Write(writer);
				writer.Write(")");
				return;
			}
			throw new InvalidOperationException(this.Operator.ToString());
		}

		// Token: 0x04002570 RID: 9584
		private readonly MdxExpression operand;

		// Token: 0x04002571 RID: 9585
		private readonly MdxUnaryOperators op;
	}
}
