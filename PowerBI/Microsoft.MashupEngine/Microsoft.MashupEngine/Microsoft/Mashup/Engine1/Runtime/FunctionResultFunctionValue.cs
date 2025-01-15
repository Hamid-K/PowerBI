using System;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200131C RID: 4892
	internal class FunctionResultFunctionValue : DelegatingFunctionValue, IFunctionIdentity, IEquatable<IFunctionIdentity>, IOptimizedValue
	{
		// Token: 0x06008128 RID: 33064 RVA: 0x001B80F1 File Offset: 0x001B62F1
		public FunctionResultFunctionValue(FunctionValue function, FunctionValue ctor, params Value[] args)
			: base(function)
		{
			this.ctor = ctor;
			this.args = args;
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x06008129 RID: 33065 RVA: 0x001B8108 File Offset: 0x001B6308
		public override IExpression Expression
		{
			get
			{
				IExpression expression = ConstantExpressionSyntaxNode.New(this.ctor);
				IExpression[] array = this.args.Select(new Func<Value, ConstantExpressionSyntaxNode>(ConstantExpressionSyntaxNode.New));
				return new InvocationExpressionSyntaxNodeN(expression, array);
			}
		}

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x0600812A RID: 33066 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override IFunctionIdentity FunctionIdentity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x001B813E File Offset: 0x001B633E
		protected override FunctionValue Wrap(FunctionValue function)
		{
			return new FunctionResultFunctionValue(function, this.ctor, this.args);
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x001B8152 File Offset: 0x001B6352
		int IFunctionIdentity.GetHashCode()
		{
			return this.args.Aggregate(this.ctor.FunctionIdentity.GetHashCode(), (int seed, Value value) => seed * 7 + value.GetHashCode());
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x001B8190 File Offset: 0x001B6390
		bool IEquatable<IFunctionIdentity>.Equals(IFunctionIdentity functionIdentity)
		{
			FunctionResultFunctionValue functionResultFunctionValue = functionIdentity as FunctionResultFunctionValue;
			return functionResultFunctionValue != null && this.ctor.FunctionIdentity.Equals(functionResultFunctionValue.ctor.FunctionIdentity) && this.args.AllEqual(functionResultFunctionValue.args);
		}

		// Token: 0x0400467B RID: 18043
		private readonly FunctionValue ctor;

		// Token: 0x0400467C RID: 18044
		private readonly Value[] args;
	}
}
