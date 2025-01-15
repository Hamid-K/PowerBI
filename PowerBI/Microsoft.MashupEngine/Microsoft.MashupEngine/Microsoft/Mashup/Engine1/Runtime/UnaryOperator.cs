using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001686 RID: 5766
	public abstract class UnaryOperator : NativeFunctionValue1
	{
		// Token: 0x0600921E RID: 37406 RVA: 0x001D097A File Offset: 0x001CEB7A
		private UnaryOperator()
			: base("arg0")
		{
		}

		// Token: 0x0600921F RID: 37407 RVA: 0x001E5220 File Offset: 0x001E3420
		private UnaryOperator(string arg)
			: base(arg)
		{
		}

		// Token: 0x04004E6E RID: 20078
		public new static readonly UnaryOperator Identity = new UnaryOperator.IdentityUnaryOperator();

		// Token: 0x04004E6F RID: 20079
		public new static readonly UnaryOperator Negate = new UnaryOperator.NegateUnaryOperator();

		// Token: 0x04004E70 RID: 20080
		public new static readonly UnaryOperator Not = new UnaryOperator.NotUnaryOperator();

		// Token: 0x04004E71 RID: 20081
		public static readonly UnaryOperator Meta = new UnaryOperator.MetaUnaryOperator();

		// Token: 0x04004E72 RID: 20082
		public new static readonly UnaryOperator BitwiseNot = new UnaryOperator.BitwiseNotUnaryOperator();

		// Token: 0x04004E73 RID: 20083
		public new static readonly UnaryOperator SubtractMetaValue = new UnaryOperator.SubtractMetaValueUnaryOperator();

		// Token: 0x02001687 RID: 5767
		private class IdentityUnaryOperator : UnaryOperator
		{
			// Token: 0x06009221 RID: 37409 RVA: 0x001E5267 File Offset: 0x001E3467
			public override Value Invoke(Value value)
			{
				return value.Identity();
			}
		}

		// Token: 0x02001688 RID: 5768
		private class NegateUnaryOperator : UnaryOperator
		{
			// Token: 0x06009223 RID: 37411 RVA: 0x001E5277 File Offset: 0x001E3477
			public override Value Invoke(Value value)
			{
				return value.Negate();
			}
		}

		// Token: 0x02001689 RID: 5769
		private class NotUnaryOperator : UnaryOperator
		{
			// Token: 0x06009225 RID: 37413 RVA: 0x001E527F File Offset: 0x001E347F
			public override Value Invoke(Value value)
			{
				return value.Not();
			}
		}

		// Token: 0x0200168A RID: 5770
		private class MetaUnaryOperator : UnaryOperator
		{
			// Token: 0x06009227 RID: 37415 RVA: 0x001E5287 File Offset: 0x001E3487
			public override Value Invoke(Value value)
			{
				return value.MetaValue;
			}
		}

		// Token: 0x0200168B RID: 5771
		private class SubtractMetaValueUnaryOperator : UnaryOperator
		{
			// Token: 0x06009229 RID: 37417 RVA: 0x001E528F File Offset: 0x001E348F
			public override Value Invoke(Value value)
			{
				return value.SubtractMetaValue;
			}
		}

		// Token: 0x0200168C RID: 5772
		private class BitwiseNotUnaryOperator : UnaryOperator
		{
			// Token: 0x0600922B RID: 37419 RVA: 0x001E5297 File Offset: 0x001E3497
			public BitwiseNotUnaryOperator()
				: base("number")
			{
			}

			// Token: 0x0600922C RID: 37420 RVA: 0x001E52A4 File Offset: 0x001E34A4
			public override Value Invoke(Value value)
			{
				return value.BitwiseNot();
			}
		}
	}
}
