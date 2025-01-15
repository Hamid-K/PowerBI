using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012BF RID: 4799
	public abstract class CultureSpecificFunctionValue2<TResult, T0, T1> : NativeFunctionValue2<TResult, T0, T1>, IInvocationRewriter where TResult : Value where T0 : Value where T1 : Value
	{
		// Token: 0x06007E21 RID: 32289 RVA: 0x001B045C File Offset: 0x001AE65C
		protected CultureSpecificFunctionValue2(IEngineHost engineHost, ICulture boundCulture, TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1)
			: this(engineHost, boundCulture, returnType, 2, param0, type0, param1, type1)
		{
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x001B047B File Offset: 0x001AE67B
		protected CultureSpecificFunctionValue2(IEngineHost engineHost, ICulture boundCulture, TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1)
			: base(returnType, min, param0, type0, param1, type1)
		{
			this.engineHost = engineHost;
			this.boundCulture = boundCulture;
			this.defaultCulture = boundCulture ?? Culture.GetDefaultCulture(engineHost);
		}

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x06007E23 RID: 32291 RVA: 0x001B04AD File Offset: 0x001AE6AD
		protected ICulture BoundCulture
		{
			get
			{
				return this.boundCulture;
			}
		}

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x06007E24 RID: 32292 RVA: 0x001B04B5 File Offset: 0x001AE6B5
		public override IFunctionIdentity FunctionIdentity
		{
			get
			{
				return new TypeAndCultureIdentity(base.GetType(), this.boundCulture);
			}
		}

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x06007E25 RID: 32293 RVA: 0x000020FA File Offset: 0x000002FA
		protected virtual FunctionValue CultureNeutralFunction
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x001B04C8 File Offset: 0x001AE6C8
		public virtual bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
		{
			if (this.BoundCulture != null && invocation.Arguments.Count == 1 && this.CultureNeutralFunction != null)
			{
				expression = new InvocationExpressionSyntaxNode2(ConstantExpressionSyntaxNode.New(this.CultureNeutralFunction), invocation.Arguments[0], ConstantExpressionSyntaxNode.New(this.BoundCulture.Name));
				environment.SetType(expression, this.ReturnType);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x001B0536 File Offset: 0x001AE736
		protected ICulture GetCulture(Value culture)
		{
			return Culture.GetCulture(this.engineHost, culture, this.defaultCulture);
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x001B054A File Offset: 0x001AE74A
		protected T QueryService<T>() where T : class
		{
			return this.engineHost.QueryService<T>();
		}

		// Token: 0x04004548 RID: 17736
		private readonly IEngineHost engineHost;

		// Token: 0x04004549 RID: 17737
		private readonly ICulture boundCulture;

		// Token: 0x0400454A RID: 17738
		private readonly ICulture defaultCulture;
	}
}
