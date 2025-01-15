using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE4 RID: 3300
	internal sealed class IfCubeExpression : CubeExpression
	{
		// Token: 0x06005985 RID: 22917 RVA: 0x00139762 File Offset: 0x00137962
		public IfCubeExpression(CubeExpression condition, CubeExpression trueCase, CubeExpression falseCase)
		{
			this.condition = condition;
			this.trueCase = trueCase;
			this.falseCase = falseCase;
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06005986 RID: 22918 RVA: 0x00075E2C File Offset: 0x0007402C
		public override CubeExpressionKind Kind
		{
			get
			{
				return CubeExpressionKind.If;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x0013977F File Offset: 0x0013797F
		public CubeExpression Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06005988 RID: 22920 RVA: 0x00139787 File Offset: 0x00137987
		public CubeExpression TrueCase
		{
			get
			{
				return this.trueCase;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x0013978F File Offset: 0x0013798F
		public CubeExpression FalseCase
		{
			get
			{
				return this.falseCase;
			}
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x00139797 File Offset: 0x00137997
		public bool Equals(IfCubeExpression other)
		{
			return other != null && this.condition.Equals(other.condition) && this.trueCase.Equals(other.trueCase) && this.falseCase.Equals(other.falseCase);
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x001397D5 File Offset: 0x001379D5
		public override bool Equals(object other)
		{
			return this.Equals(other as IfCubeExpression);
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x001397E3 File Offset: 0x001379E3
		public override int GetHashCode()
		{
			return this.condition.GetHashCode() + 37 * this.trueCase.GetHashCode() + 5011 * this.falseCase.GetHashCode();
		}

		// Token: 0x0400321E RID: 12830
		private readonly CubeExpression condition;

		// Token: 0x0400321F RID: 12831
		private readonly CubeExpression trueCase;

		// Token: 0x04003220 RID: 12832
		private readonly CubeExpression falseCase;
	}
}
