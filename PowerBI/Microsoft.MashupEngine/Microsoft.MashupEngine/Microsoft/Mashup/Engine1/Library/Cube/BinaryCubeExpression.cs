using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE2 RID: 3298
	internal sealed class BinaryCubeExpression : CubeExpression
	{
		// Token: 0x06005976 RID: 22902 RVA: 0x0013955B File Offset: 0x0013775B
		public BinaryCubeExpression(BinaryOperator2 op, CubeExpression left, CubeExpression right)
		{
			this.op = op;
			this.left = left;
			this.right = right;
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x000023C4 File Offset: 0x000005C4
		public override CubeExpressionKind Kind
		{
			get
			{
				return CubeExpressionKind.Binary;
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x06005978 RID: 22904 RVA: 0x00139578 File Offset: 0x00137778
		public BinaryOperator2 Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06005979 RID: 22905 RVA: 0x00139580 File Offset: 0x00137780
		public CubeExpression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x00139588 File Offset: 0x00137788
		public CubeExpression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x00139590 File Offset: 0x00137790
		public bool Equals(BinaryCubeExpression other)
		{
			if (other != null && this.op == other.op)
			{
				BinaryOperator2 binaryOperator = this.op;
				bool flag = binaryOperator - BinaryOperator2.Equals <= 3;
				return (this.left.Equals(other.left) && this.right.Equals(other.right)) || (flag && this.left.Equals(other.right) && this.right.Equals(other.left));
			}
			return false;
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x00139614 File Offset: 0x00137814
		public override bool Equals(object other)
		{
			return this.Equals(other as BinaryCubeExpression);
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x00139624 File Offset: 0x00137824
		public override int GetHashCode()
		{
			return this.op.GetHashCode() * 37 + this.left.GetHashCode() + this.right.GetHashCode();
		}

		// Token: 0x04003219 RID: 12825
		private readonly BinaryOperator2 op;

		// Token: 0x0400321A RID: 12826
		private readonly CubeExpression left;

		// Token: 0x0400321B RID: 12827
		private readonly CubeExpression right;
	}
}
