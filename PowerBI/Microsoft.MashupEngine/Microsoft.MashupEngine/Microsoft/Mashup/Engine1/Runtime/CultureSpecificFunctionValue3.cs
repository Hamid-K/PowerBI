using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012C0 RID: 4800
	public abstract class CultureSpecificFunctionValue3<TResult, T0, T1, T2> : NativeFunctionValue3<TResult, T0, T1, T2>, IInvocationRewriter where TResult : Value where T0 : Value where T1 : Value where T2 : Value
	{
		// Token: 0x06007E29 RID: 32297 RVA: 0x001B0558 File Offset: 0x001AE758
		protected CultureSpecificFunctionValue3(IEngineHost engineHost, ICulture boundCulture, TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2)
			: this(engineHost, boundCulture, returnType, 3, param0, type0, param1, type1, param2, type2)
		{
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x001B057C File Offset: 0x001AE77C
		protected CultureSpecificFunctionValue3(IEngineHost engineHost, ICulture boundCulture, TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2)
			: base(returnType, min, param0, type0, param1, type1, param2, type2)
		{
			this.engineHost = engineHost;
			this.boundCulture = boundCulture;
			this.defaultCulture = boundCulture ?? Culture.GetDefaultCulture(engineHost);
		}

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x06007E2B RID: 32299 RVA: 0x001B05BD File Offset: 0x001AE7BD
		protected ICulture BoundCulture
		{
			get
			{
				return this.boundCulture;
			}
		}

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x06007E2C RID: 32300 RVA: 0x001B05C5 File Offset: 0x001AE7C5
		public override IFunctionIdentity FunctionIdentity
		{
			get
			{
				return new TypeAndCultureIdentity(base.GetType(), this.boundCulture);
			}
		}

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x06007E2D RID: 32301 RVA: 0x000020FA File Offset: 0x000002FA
		protected virtual FunctionValue CultureNeutralFunction
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x06007E2E RID: 32302 RVA: 0x0017811C File Offset: 0x0017631C
		protected virtual int CultureArgumentPosition
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x001B05D8 File Offset: 0x001AE7D8
		public virtual bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression)
		{
			if (this.BoundCulture != null && invocation.Arguments.Count <= this.CultureArgumentPosition)
			{
				int cultureArgumentPosition = this.CultureArgumentPosition;
				if (cultureArgumentPosition != 1)
				{
					if (cultureArgumentPosition != 2)
					{
						throw new InvalidOperationException();
					}
					expression = new InvocationExpressionSyntaxNodeN(ConstantExpressionSyntaxNode.New(this.CultureNeutralFunction), new IExpression[]
					{
						invocation.Arguments[0],
						invocation.Arguments[1],
						ConstantExpressionSyntaxNode.New(this.BoundCulture.Name)
					});
				}
				else
				{
					expression = new InvocationExpressionSyntaxNode2(ConstantExpressionSyntaxNode.New(this.CultureNeutralFunction), invocation.Arguments[0], ConstantExpressionSyntaxNode.New(this.BoundCulture.Name));
				}
				environment.SetType(expression, base.ReturnType);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x001B06AD File Offset: 0x001AE8AD
		protected ICulture GetCulture(Value culture)
		{
			return Culture.GetCulture(this.engineHost, culture, this.defaultCulture);
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x001B06C1 File Offset: 0x001AE8C1
		protected T QueryService<T>() where T : class
		{
			return this.engineHost.QueryService<T>();
		}

		// Token: 0x0400454B RID: 17739
		private readonly IEngineHost engineHost;

		// Token: 0x0400454C RID: 17740
		private readonly ICulture boundCulture;

		// Token: 0x0400454D RID: 17741
		private readonly ICulture defaultCulture;
	}
}
