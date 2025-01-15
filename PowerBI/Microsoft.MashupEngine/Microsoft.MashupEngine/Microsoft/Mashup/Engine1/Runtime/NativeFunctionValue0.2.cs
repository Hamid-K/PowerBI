using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001587 RID: 5511
	public abstract class NativeFunctionValue0<TResult> : NativeFunctionValue0 where TResult : Value
	{
		// Token: 0x0600893E RID: 35134 RVA: 0x001D0946 File Offset: 0x001CEB46
		protected NativeFunctionValue0(TypeValue returnType)
		{
			this.returnType = returnType;
		}

		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x0600893F RID: 35135 RVA: 0x001D0955 File Offset: 0x001CEB55
		protected override TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x06008940 RID: 35136 RVA: 0x001D095D File Offset: 0x001CEB5D
		public sealed override Value Invoke()
		{
			return this.TypedInvoke().As<TResult>(this.returnType);
		}

		// Token: 0x06008941 RID: 35137
		public abstract TResult TypedInvoke();

		// Token: 0x04004BC6 RID: 19398
		private readonly TypeValue returnType;
	}
}
