using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CDF RID: 3295
	internal sealed class ConstantCubeExpression : CubeExpression
	{
		// Token: 0x06005964 RID: 22884 RVA: 0x00139445 File Offset: 0x00137645
		public ConstantCubeExpression(Value value)
		{
			this.value = value;
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06005965 RID: 22885 RVA: 0x00002105 File Offset: 0x00000305
		public override CubeExpressionKind Kind
		{
			get
			{
				return CubeExpressionKind.Constant;
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06005966 RID: 22886 RVA: 0x00139454 File Offset: 0x00137654
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x0013945C File Offset: 0x0013765C
		public bool Equals(ConstantCubeExpression other)
		{
			return other != null && this.value.Equals(other.value);
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00139474 File Offset: 0x00137674
		public override bool Equals(object other)
		{
			return this.Equals(other as ConstantCubeExpression);
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x00139482 File Offset: 0x00137682
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x04003213 RID: 12819
		public static readonly CubeExpression Null = new ConstantCubeExpression(Value.Null);

		// Token: 0x04003214 RID: 12820
		public static readonly CubeExpression True = new ConstantCubeExpression(LogicalValue.True);

		// Token: 0x04003215 RID: 12821
		public static readonly CubeExpression False = new ConstantCubeExpression(LogicalValue.False);

		// Token: 0x04003216 RID: 12822
		private readonly Value value;
	}
}
