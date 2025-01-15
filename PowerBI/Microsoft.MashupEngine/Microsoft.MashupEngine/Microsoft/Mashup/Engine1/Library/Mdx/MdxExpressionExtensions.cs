using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009AC RID: 2476
	internal static class MdxExpressionExtensions
	{
		// Token: 0x060046BA RID: 18106 RVA: 0x000ED52C File Offset: 0x000EB72C
		public static MdxExpression Not(this MdxExpression operand)
		{
			if (ConstantMdxExpression.True.Equals(operand))
			{
				return ConstantMdxExpression.False;
			}
			if (ConstantMdxExpression.False.Equals(operand))
			{
				return ConstantMdxExpression.True;
			}
			UnaryMdxExpression unaryMdxExpression = operand as UnaryMdxExpression;
			if (unaryMdxExpression == null || unaryMdxExpression.Operator != MdxUnaryOperators.Not)
			{
				return new UnaryMdxExpression(MdxUnaryOperators.Not, operand);
			}
			return unaryMdxExpression.Operand;
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x000ED580 File Offset: 0x000EB780
		public static MdxExpression And(this MdxExpression left, MdxExpression right)
		{
			if (left == null || ConstantMdxExpression.True.Equals(left))
			{
				return right;
			}
			if (right == null || ConstantMdxExpression.True.Equals(right))
			{
				return left;
			}
			if (ConstantMdxExpression.False.Equals(left) || ConstantMdxExpression.False.Equals(right))
			{
				return ConstantMdxExpression.False;
			}
			if (left.Equals(right))
			{
				return left;
			}
			return new BinaryMdxExpression(BinaryOperator2.And, left, right);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x000ED5E8 File Offset: 0x000EB7E8
		public static MdxExpression Or(this MdxExpression left, MdxExpression right)
		{
			if (left == null || ConstantMdxExpression.False.Equals(left))
			{
				return right;
			}
			if (right == null || ConstantMdxExpression.False.Equals(right))
			{
				return left;
			}
			if (ConstantMdxExpression.True.Equals(left) || ConstantMdxExpression.True.Equals(right))
			{
				return ConstantMdxExpression.True;
			}
			if (left.Equals(right))
			{
				return left;
			}
			return new BinaryMdxExpression(BinaryOperator2.Or, left, right);
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x000ED650 File Offset: 0x000EB850
		public static MdxExpression IsEmpty(this MdxExpression operand)
		{
			ConstantMdxExpression constantMdxExpression = operand as ConstantMdxExpression;
			InvocationMdxExpression invocationMdxExpression = operand as InvocationMdxExpression;
			if (invocationMdxExpression != null)
			{
				MdxFunction function = invocationMdxExpression.Function;
				if (function != MdxFunction.VbaCDate)
				{
					if (function == MdxFunction.IIf)
					{
						if (invocationMdxExpression.Arguments.Length == 3)
						{
							ConstantMdxExpression constantMdxExpression2 = invocationMdxExpression.Arguments[1].IsEmpty() as ConstantMdxExpression;
							ConstantMdxExpression constantMdxExpression3 = invocationMdxExpression.Arguments[2].IsEmpty() as ConstantMdxExpression;
							if (constantMdxExpression2 != null && !ConstantMdxExpression.Null.Equals(constantMdxExpression2) && constantMdxExpression3 != null && !ConstantMdxExpression.Null.Equals(constantMdxExpression3))
							{
								return ConstantMdxExpression.False;
							}
						}
					}
				}
				else if (invocationMdxExpression.Arguments.Length == 1)
				{
					constantMdxExpression = invocationMdxExpression.Arguments[0] as ConstantMdxExpression;
				}
			}
			if (constantMdxExpression != null)
			{
				return ConstantMdxExpression.New(ConstantMdxExpression.Null.Equals(constantMdxExpression));
			}
			return new InvocationMdxExpression(MdxFunction.IsEmpty, new MdxExpression[] { operand });
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000ED71C File Offset: 0x000EB91C
		public static MdxExpression Equals(this MdxExpression left, MdxExpression right)
		{
			ConstantMdxExpression constantMdxExpression = left as ConstantMdxExpression;
			ConstantMdxExpression constantMdxExpression2 = right as ConstantMdxExpression;
			if (constantMdxExpression != null && constantMdxExpression.Type == MdxConstantType.Boolean)
			{
				if (!(bool)constantMdxExpression.Value)
				{
					return right.Not();
				}
				return right;
			}
			else
			{
				if (constantMdxExpression2 == null || constantMdxExpression2.Type != MdxConstantType.Boolean)
				{
					return new BinaryMdxExpression(BinaryOperator2.Equals, left, right);
				}
				if (!(bool)constantMdxExpression2.Value)
				{
					return left.Not();
				}
				return left;
			}
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000ED784 File Offset: 0x000EB984
		public static MdxExpression IsValid(this MdxExpression operand)
		{
			ConstantMdxExpression constantMdxExpression = operand as ConstantMdxExpression;
			InvocationMdxExpression invocationMdxExpression = operand as InvocationMdxExpression;
			if (invocationMdxExpression != null)
			{
				MdxFunction function = invocationMdxExpression.Function;
				if (function != MdxFunction.VbaCDate)
				{
					if (function == MdxFunction.IIf)
					{
						if (invocationMdxExpression.Arguments.Length == 3)
						{
							ConstantMdxExpression constantMdxExpression2 = invocationMdxExpression.Arguments[1].IsValid() as ConstantMdxExpression;
							ConstantMdxExpression constantMdxExpression3 = invocationMdxExpression.Arguments[2].IsValid() as ConstantMdxExpression;
							if (constantMdxExpression2 != null && !ConstantMdxExpression.Null.Equals(constantMdxExpression2) && constantMdxExpression3 != null && !ConstantMdxExpression.Null.Equals(constantMdxExpression3))
							{
								return ConstantMdxExpression.True;
							}
						}
					}
				}
				else if (invocationMdxExpression.Arguments.Length == 1)
				{
					constantMdxExpression = invocationMdxExpression.Arguments[0] as ConstantMdxExpression;
				}
			}
			if (constantMdxExpression != null)
			{
				return ConstantMdxExpression.New(!ConstantMdxExpression.Null.Equals(constantMdxExpression));
			}
			return new InvocationMdxExpression(MdxFunction.IsValid, new MdxExpression[] { operand });
		}
	}
}
