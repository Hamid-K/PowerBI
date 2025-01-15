using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Typeflow
{
	// Token: 0x020017B6 RID: 6070
	internal static class PartialEvaluationAlgebra
	{
		// Token: 0x0600997A RID: 39290 RVA: 0x001FBDC4 File Offset: 0x001F9FC4
		public static Value Binary(BinaryOperator2 op, Value left, Value right)
		{
			Value value = null;
			if (left != null)
			{
				if (right != null)
				{
					try
					{
						switch (op)
						{
						case BinaryOperator2.Add:
							value = left.Add(right);
							break;
						case BinaryOperator2.Subtract:
							value = left.Subtract(right);
							break;
						case BinaryOperator2.Multiply:
							value = left.Multiply(right);
							break;
						case BinaryOperator2.Divide:
							value = left.Divide(right);
							break;
						case BinaryOperator2.GreaterThan:
							value = left.NullableGreaterThan(right);
							break;
						case BinaryOperator2.LessThan:
							value = left.NullableLessThan(right);
							break;
						case BinaryOperator2.GreaterThanOrEquals:
							value = left.NullableGreaterThanOrEqual(right);
							break;
						case BinaryOperator2.LessThanOrEquals:
							value = left.NullableLessThanOrEqual(right);
							break;
						case BinaryOperator2.Equals:
							value = LogicalValue.New(left.Equals(right));
							break;
						case BinaryOperator2.NotEquals:
							value = LogicalValue.New(!left.Equals(right));
							break;
						case BinaryOperator2.And:
							value = BinaryOperator.LogicalAnd.Invoke(left, right);
							break;
						case BinaryOperator2.Or:
							value = BinaryOperator.LogicalOr.Invoke(left, right);
							break;
						case BinaryOperator2.MetadataAdd:
							value = BinaryOperator.AddMeta.Invoke(left, right);
							break;
						case BinaryOperator2.Range:
							value = BinaryOperator.Range.Invoke(left, right);
							break;
						case BinaryOperator2.Concatenate:
							value = left.Concatenate(right);
							break;
						case BinaryOperator2.As:
							value = Library._Value.As.Invoke(left, right);
							break;
						case BinaryOperator2.Is:
							value = Library._Value.Is.Invoke(left, right);
							break;
						case BinaryOperator2.Coalesce:
							value = (left.IsNull ? right : left);
							break;
						default:
							throw new NotSupportedException();
						}
						return value;
					}
					catch (ValueException)
					{
						return value;
					}
				}
				if (op != BinaryOperator2.And)
				{
					if (op != BinaryOperator2.Or)
					{
						if (op == BinaryOperator2.Coalesce)
						{
							if (!left.IsNull)
							{
								value = left;
							}
						}
					}
					else if (left.Equals(LogicalValue.True))
					{
						value = left;
					}
				}
				else if (left.Equals(LogicalValue.False))
				{
					value = left;
				}
			}
			return value;
		}

		// Token: 0x0600997B RID: 39291 RVA: 0x001FBF98 File Offset: 0x001FA198
		public static Value Unary(UnaryOperator2 op, Value operand)
		{
			Value value = null;
			if (operand != null)
			{
				try
				{
					switch (op)
					{
					case UnaryOperator2.Not:
						value = operand.Not();
						break;
					case UnaryOperator2.Negative:
						value = operand.Negate();
						break;
					case UnaryOperator2.Positive:
						value = operand.Identity();
						break;
					default:
						throw new NotSupportedException();
					}
				}
				catch (ValueException)
				{
				}
			}
			return value;
		}

		// Token: 0x0600997C RID: 39292 RVA: 0x001FBFF8 File Offset: 0x001FA1F8
		public static BinaryOperator2 Negate(this BinaryOperator2 binaryOperator)
		{
			BinaryOperator2 binaryOperator2;
			if (binaryOperator.TrySwapOperands(out binaryOperator2))
			{
				return binaryOperator2;
			}
			throw new InvalidOperationException("PartialEvaluationAlgebra.Negate");
		}

		// Token: 0x0600997D RID: 39293 RVA: 0x001FC01C File Offset: 0x001FA21C
		public static BinaryOperator2 SwapOperands(this BinaryOperator2 binaryOperator)
		{
			BinaryOperator2 binaryOperator2;
			if (!binaryOperator.TrySwapOperands(out binaryOperator2))
			{
				binaryOperator2 = binaryOperator;
			}
			return binaryOperator2;
		}

		// Token: 0x0600997E RID: 39294 RVA: 0x001FC038 File Offset: 0x001FA238
		public static bool TrySwapOperands(this BinaryOperator2 binaryOperator, out BinaryOperator2 swappedOperator)
		{
			switch (binaryOperator)
			{
			case BinaryOperator2.GreaterThan:
				swappedOperator = BinaryOperator2.LessThan;
				return true;
			case BinaryOperator2.LessThan:
				swappedOperator = BinaryOperator2.GreaterThan;
				return true;
			case BinaryOperator2.GreaterThanOrEquals:
				swappedOperator = BinaryOperator2.LessThanOrEquals;
				return true;
			case BinaryOperator2.LessThanOrEquals:
				swappedOperator = BinaryOperator2.GreaterThanOrEquals;
				return true;
			case BinaryOperator2.Equals:
				swappedOperator = BinaryOperator2.Equals;
				return true;
			case BinaryOperator2.NotEquals:
				swappedOperator = BinaryOperator2.NotEquals;
				return true;
			default:
				swappedOperator = BinaryOperator2.Add;
				return false;
			}
		}

		// Token: 0x0400514A RID: 20810
		private static readonly FunctionValue trueConstraintFunction = new PartialEvaluationAlgebra.TrueConstraintFunction();

		// Token: 0x020017B7 RID: 6071
		private class TrueConstraintFunction : NativeFunctionValue1<LogicalValue, Value>
		{
			// Token: 0x06009980 RID: 39296 RVA: 0x001FC096 File Offset: 0x001FA296
			public TrueConstraintFunction()
				: base(TypeValue.Logical, 1, "param", TypeValue.Any)
			{
			}

			// Token: 0x06009981 RID: 39297 RVA: 0x001D0382 File Offset: 0x001CE582
			public override LogicalValue TypedInvoke(Value arg0)
			{
				return LogicalValue.True;
			}
		}
	}
}
