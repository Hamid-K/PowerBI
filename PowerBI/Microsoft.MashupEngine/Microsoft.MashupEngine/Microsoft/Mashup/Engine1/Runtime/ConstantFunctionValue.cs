using System;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012B1 RID: 4785
	internal sealed class ConstantFunctionValue : NativeFunctionValueN
	{
		// Token: 0x06007DA3 RID: 32163 RVA: 0x001AF5E2 File Offset: 0x001AD7E2
		public ConstantFunctionValue(Value value)
			: base(0, EmptyArray<string>.Instance)
		{
			this.value = value;
			this.minArgs = 0;
			this.maxArgs = 0;
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x001AF605 File Offset: 0x001AD805
		public ConstantFunctionValue(Value value, int minArgs, int maxArgs)
			: base(minArgs, ConstantFunctionValue.MakeParameters(maxArgs))
		{
			this.value = value;
			this.minArgs = minArgs;
			this.maxArgs = maxArgs;
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x001AF629 File Offset: 0x001AD829
		public ConstantFunctionValue(Value value, IFunctionTypeExpression functionTypeExpression)
			: this(value, functionTypeExpression.Min, functionTypeExpression.Parameters.Count)
		{
			this.functionTypeExpression = functionTypeExpression;
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x001AF64A File Offset: 0x001AD84A
		public static ConstantFunctionValue Each(Value value)
		{
			return new ConstantFunctionValue(value, Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType);
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x001AF658 File Offset: 0x001AD858
		private static string[] MakeParameters(int n)
		{
			string[] array = new string[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = string.Format(CultureInfo.InvariantCulture, "arg{0}", i);
			}
			return array;
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x06007DA8 RID: 32168 RVA: 0x001AF691 File Offset: 0x001AD891
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					this.expression = new FunctionExpressionSyntaxNode(this.FunctionTypeExpression, new ConstantExpressionSyntaxNode(this.value));
				}
				return this.expression;
			}
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x06007DA9 RID: 32169 RVA: 0x001AF6BD File Offset: 0x001AD8BD
		private IFunctionTypeExpression FunctionTypeExpression
		{
			get
			{
				if (this.functionTypeExpression == null)
				{
					this.functionTypeExpression = this.Type.AsFunctionType.ToFunctionTypeExpression();
				}
				return this.functionTypeExpression;
			}
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x001AF6E3 File Offset: 0x001AD8E3
		protected override Value InvokeN(Value[] args)
		{
			return this.value;
		}

		// Token: 0x04004523 RID: 17699
		public static readonly ConstantFunctionValue EachNull = ConstantFunctionValue.Each(Value.Null);

		// Token: 0x04004524 RID: 17700
		public static readonly ConstantFunctionValue EachTrue = ConstantFunctionValue.Each(LogicalValue.True);

		// Token: 0x04004525 RID: 17701
		public static readonly ConstantFunctionValue EachFalse = ConstantFunctionValue.Each(LogicalValue.False);

		// Token: 0x04004526 RID: 17702
		private readonly Value value;

		// Token: 0x04004527 RID: 17703
		private readonly int minArgs;

		// Token: 0x04004528 RID: 17704
		private readonly int maxArgs;

		// Token: 0x04004529 RID: 17705
		private IFunctionTypeExpression functionTypeExpression;

		// Token: 0x0400452A RID: 17706
		private IExpression expression;
	}
}
