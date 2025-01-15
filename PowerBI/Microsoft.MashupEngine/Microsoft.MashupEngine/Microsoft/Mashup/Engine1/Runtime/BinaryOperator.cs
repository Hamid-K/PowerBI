using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001267 RID: 4711
	public abstract class BinaryOperator : NativeFunctionValue2
	{
		// Token: 0x06007C2E RID: 31790 RVA: 0x001AB700 File Offset: 0x001A9900
		protected BinaryOperator(TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1)
			: base(min, param0, param1)
		{
			this.returnType = returnType;
			this.type0 = type0;
			this.type1 = type1;
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x001AB723 File Offset: 0x001A9923
		protected BinaryOperator(TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1)
			: this(returnType, 2, param0, type0, param1, type1)
		{
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x001AB733 File Offset: 0x001A9933
		private BinaryOperator()
			: this(TypeValue.Any, 2, "number1", TypeValue.Any, "number2", TypeValue.Any)
		{
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06007C31 RID: 31793 RVA: 0x001AB755 File Offset: 0x001A9955
		protected override TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06007C32 RID: 31794 RVA: 0x001AB75D File Offset: 0x001A995D
		protected override TypeValue Type0
		{
			get
			{
				return this.type0;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x06007C33 RID: 31795 RVA: 0x001AB765 File Offset: 0x001A9965
		protected override TypeValue Type1
		{
			get
			{
				return this.type1;
			}
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x001AB770 File Offset: 0x001A9970
		private static bool? AsNullableBool(Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			if (!value.IsLogical)
			{
				throw ValueException.CastTypeMismatch(value, TypeValue.Logical);
			}
			return new bool?(value.AsBoolean);
		}

		// Token: 0x040044AB RID: 17579
		private TypeValue returnType;

		// Token: 0x040044AC RID: 17580
		private TypeValue type0;

		// Token: 0x040044AD RID: 17581
		private TypeValue type1;

		// Token: 0x040044AE RID: 17582
		public static readonly BinaryOperator LogicalOr = new BinaryOperator.LogicalOrBinaryOperator();

		// Token: 0x040044AF RID: 17583
		public static readonly BinaryOperator LogicalAnd = new BinaryOperator.LogicalAndBinaryOperator();

		// Token: 0x040044B0 RID: 17584
		public new static readonly BinaryOperator Add = new BinaryOperator.AddBinaryOperator();

		// Token: 0x040044B1 RID: 17585
		public new static readonly BinaryOperator Concatenate = new BinaryOperator.ConcatenateBinaryOperator();

		// Token: 0x040044B2 RID: 17586
		public new static readonly BinaryOperator Subtract = new BinaryOperator.SubtractBinaryOperator();

		// Token: 0x040044B3 RID: 17587
		public new static readonly BinaryOperator Multiply = new BinaryOperator.MultiplyBinaryOperator();

		// Token: 0x040044B4 RID: 17588
		public new static readonly BinaryOperator Divide = new BinaryOperator.DivideBinaryOperator();

		// Token: 0x040044B5 RID: 17589
		public new static readonly BinaryOperator Mod = new BinaryOperator.ModBinaryOperator();

		// Token: 0x040044B6 RID: 17590
		public static readonly BinaryOperator Range = new BinaryOperator.RangeBinaryOperator();

		// Token: 0x040044B7 RID: 17591
		public new static readonly BinaryOperator NewMeta = new BinaryOperator.NewMetaBinaryOperator();

		// Token: 0x040044B8 RID: 17592
		public static readonly BinaryOperator AddMeta = new BinaryOperator.AddMetaBinaryOperator();

		// Token: 0x040044B9 RID: 17593
		public static readonly BinaryOperator Equal = new BinaryOperator.EqualBinaryOperator();

		// Token: 0x040044BA RID: 17594
		public static readonly BinaryOperator NotEqual = new BinaryOperator.NotEqualBinaryOperator();

		// Token: 0x040044BB RID: 17595
		public new static readonly BinaryOperator LessThan = new BinaryOperator.LessThanBinaryOperator();

		// Token: 0x040044BC RID: 17596
		public new static readonly BinaryOperator LessThanOrEqual = new BinaryOperator.LessThanOrEqualBinaryOperator();

		// Token: 0x040044BD RID: 17597
		public new static readonly BinaryOperator GreaterThan = new BinaryOperator.GreaterThanBinaryOperator();

		// Token: 0x040044BE RID: 17598
		public new static readonly BinaryOperator GreaterThanOrEqual = new BinaryOperator.GreaterThanOrEqualBinaryOperator();

		// Token: 0x040044BF RID: 17599
		public new static readonly BinaryOperator BitwiseOr = new BinaryOperator.BitwiseOrBinaryOperator();

		// Token: 0x040044C0 RID: 17600
		public new static readonly BinaryOperator BitwiseAnd = new BinaryOperator.BitwiseAndBinaryOperator();

		// Token: 0x040044C1 RID: 17601
		public new static readonly BinaryOperator BitwiseXor = new BinaryOperator.BitwiseXorBinaryOperator();

		// Token: 0x040044C2 RID: 17602
		public new static readonly BinaryOperator ShiftLeft = new BinaryOperator.ShiftLeftBinaryOperator();

		// Token: 0x040044C3 RID: 17603
		public new static readonly BinaryOperator ShiftRight = new BinaryOperator.ShiftRightBinaryOperator();

		// Token: 0x02001268 RID: 4712
		private class LogicalOrBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C36 RID: 31798 RVA: 0x001AB89C File Offset: 0x001A9A9C
			public override Value Invoke(Value value1, Value value2)
			{
				bool? flag = BinaryOperator.AsNullableBool(value1);
				if (flag != null)
				{
					if (flag.GetValueOrDefault())
					{
						return LogicalValue.True;
					}
					if (value2.IsNull || value2.IsLogical)
					{
						return value2;
					}
					throw ValueException.CastTypeMismatch(value2, TypeValue.Logical);
				}
				else
				{
					bool? flag2 = BinaryOperator.AsNullableBool(value2);
					bool flag3 = true;
					if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
					{
						return LogicalValue.True;
					}
					return Value.Null;
				}
			}
		}

		// Token: 0x02001269 RID: 4713
		private class LogicalAndBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C38 RID: 31800 RVA: 0x001AB918 File Offset: 0x001A9B18
			public override Value Invoke(Value value1, Value value2)
			{
				bool? flag = BinaryOperator.AsNullableBool(value1);
				if (flag != null)
				{
					if (!flag.GetValueOrDefault())
					{
						return LogicalValue.False;
					}
					if (value2.IsNull || value2.IsLogical)
					{
						return value2;
					}
					throw ValueException.CastTypeMismatch(value2, TypeValue.Logical);
				}
				else
				{
					bool? flag2 = BinaryOperator.AsNullableBool(value2);
					bool flag3 = false;
					if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
					{
						return LogicalValue.False;
					}
					return Value.Null;
				}
			}
		}

		// Token: 0x0200126A RID: 4714
		private class AddBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C3A RID: 31802 RVA: 0x001AB98A File Offset: 0x001A9B8A
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.Add(value2);
			}
		}

		// Token: 0x0200126B RID: 4715
		private class ConcatenateBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C3C RID: 31804 RVA: 0x001AB993 File Offset: 0x001A9B93
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.Concatenate(value2);
			}
		}

		// Token: 0x0200126C RID: 4716
		private class SubtractBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C3E RID: 31806 RVA: 0x001AB99C File Offset: 0x001A9B9C
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.Subtract(value2);
			}
		}

		// Token: 0x0200126D RID: 4717
		private class MultiplyBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C40 RID: 31808 RVA: 0x001AB9A5 File Offset: 0x001A9BA5
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.Multiply(value2);
			}
		}

		// Token: 0x0200126E RID: 4718
		private class DivideBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C42 RID: 31810 RVA: 0x001AB9AE File Offset: 0x001A9BAE
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.Divide(value2);
			}
		}

		// Token: 0x0200126F RID: 4719
		private class IntegerDivideBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C44 RID: 31812 RVA: 0x001AB9B7 File Offset: 0x001A9BB7
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.IntegerDivide(value2);
			}
		}

		// Token: 0x02001270 RID: 4720
		private class ModBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C46 RID: 31814 RVA: 0x001AB9C0 File Offset: 0x001A9BC0
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.Mod(value2);
			}
		}

		// Token: 0x02001271 RID: 4721
		private class EqualBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C48 RID: 31816 RVA: 0x001AB9C9 File Offset: 0x001A9BC9
			public override Value Invoke(Value value1, Value value2)
			{
				if (!value1.Equals(value2))
				{
					return LogicalValue.False;
				}
				return LogicalValue.True;
			}
		}

		// Token: 0x02001272 RID: 4722
		private class NotEqualBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C4A RID: 31818 RVA: 0x001AB9DF File Offset: 0x001A9BDF
			public override Value Invoke(Value value1, Value value2)
			{
				if (value1.Equals(value2))
				{
					return LogicalValue.False;
				}
				return LogicalValue.True;
			}
		}

		// Token: 0x02001273 RID: 4723
		private class LessThanBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C4C RID: 31820 RVA: 0x001AB9F5 File Offset: 0x001A9BF5
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.NullableLessThan(value2);
			}
		}

		// Token: 0x02001274 RID: 4724
		private class LessThanOrEqualBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C4E RID: 31822 RVA: 0x001AB9FE File Offset: 0x001A9BFE
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.NullableLessThanOrEqual(value2);
			}
		}

		// Token: 0x02001275 RID: 4725
		private class GreaterThanBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C50 RID: 31824 RVA: 0x001ABA07 File Offset: 0x001A9C07
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.NullableGreaterThan(value2);
			}
		}

		// Token: 0x02001276 RID: 4726
		private class GreaterThanOrEqualBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C52 RID: 31826 RVA: 0x001ABA10 File Offset: 0x001A9C10
			public override Value Invoke(Value value1, Value value2)
			{
				return value1.NullableGreaterThanOrEqual(value2);
			}
		}

		// Token: 0x02001277 RID: 4727
		private class RangeBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C54 RID: 31828 RVA: 0x001ABA1C File Offset: 0x001A9C1C
			public override Value Invoke(Value lower, Value upper)
			{
				if (lower.Kind == upper.Kind)
				{
					ValueKind kind = lower.Kind;
					if (kind == ValueKind.Number)
					{
						return new RangeListValue(lower.AsInteger32, upper.AsInteger32 - lower.AsInteger32 + 1);
					}
					if (kind == ValueKind.Text)
					{
						return new CharacterRangeListValue(lower.AsCharacter, upper.AsCharacter);
					}
				}
				throw ValueException.BinaryOperatorTypeMismatch("..", lower, upper);
			}
		}

		// Token: 0x02001278 RID: 4728
		private class NewMetaBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C56 RID: 31830 RVA: 0x001ABA81 File Offset: 0x001A9C81
			public override Value Invoke(Value value, Value metaValue)
			{
				return value.NewMeta(metaValue.IsNull ? RecordValue.Empty : metaValue.AsRecord);
			}
		}

		// Token: 0x02001279 RID: 4729
		private class AddMetaBinaryOperator : BinaryOperator
		{
			// Token: 0x06007C58 RID: 31832 RVA: 0x001ABA9E File Offset: 0x001A9C9E
			public override Value Invoke(Value value, Value metaValue)
			{
				return value.NewMeta(value.MetaValue.Concatenate(metaValue).AsRecord);
			}
		}

		// Token: 0x0200127A RID: 4730
		private abstract class BitwiseOperator : BinaryOperator
		{
			// Token: 0x06007C5A RID: 31834 RVA: 0x001ABAB7 File Offset: 0x001A9CB7
			public BitwiseOperator()
				: base(NullableTypeValue.Number, 2, "number1", NullableTypeValue.Number, "number2", NullableTypeValue.Number)
			{
			}
		}

		// Token: 0x0200127B RID: 4731
		private class BitwiseOrBinaryOperator : BinaryOperator.BitwiseOperator
		{
			// Token: 0x06007C5B RID: 31835 RVA: 0x001ABAD9 File Offset: 0x001A9CD9
			public override Value Invoke(Value x, Value y)
			{
				return x.BitwiseOr(y);
			}
		}

		// Token: 0x0200127C RID: 4732
		private class BitwiseAndBinaryOperator : BinaryOperator.BitwiseOperator
		{
			// Token: 0x06007C5D RID: 31837 RVA: 0x001ABAEA File Offset: 0x001A9CEA
			public override Value Invoke(Value x, Value y)
			{
				return x.BitwiseAnd(y);
			}
		}

		// Token: 0x0200127D RID: 4733
		private class BitwiseXorBinaryOperator : BinaryOperator.BitwiseOperator
		{
			// Token: 0x06007C5F RID: 31839 RVA: 0x001ABAF3 File Offset: 0x001A9CF3
			public override Value Invoke(Value x, Value y)
			{
				return x.BitwiseXor(y);
			}
		}

		// Token: 0x0200127E RID: 4734
		private class ShiftLeftBinaryOperator : BinaryOperator.BitwiseOperator
		{
			// Token: 0x06007C61 RID: 31841 RVA: 0x001ABAFC File Offset: 0x001A9CFC
			public override Value Invoke(Value x, Value y)
			{
				return x.ShiftLeft(y);
			}
		}

		// Token: 0x0200127F RID: 4735
		private class ShiftRightBinaryOperator : BinaryOperator.BitwiseOperator
		{
			// Token: 0x06007C63 RID: 31843 RVA: 0x001ABB05 File Offset: 0x001A9D05
			public override Value Invoke(Value x, Value y)
			{
				return x.ShiftRight(y);
			}
		}
	}
}
