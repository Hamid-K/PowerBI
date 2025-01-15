using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001589 RID: 5513
	public abstract class NativeFunctionValue1<TResult, T0> : NativeFunctionValue1 where TResult : Value where T0 : Value
	{
		// Token: 0x0600894C RID: 35148 RVA: 0x001D0A2B File Offset: 0x001CEC2B
		protected NativeFunctionValue1(TypeValue returnType, int min, string param0, TypeValue type0)
			: base(min, param0)
		{
			this.returnType = returnType;
			this.type0 = type0;
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x001D0A44 File Offset: 0x001CEC44
		protected NativeFunctionValue1(TypeValue returnType, string param0, TypeValue type0)
			: this(returnType, 1, param0, type0)
		{
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x0600894E RID: 35150 RVA: 0x001D0A50 File Offset: 0x001CEC50
		protected override TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x0600894F RID: 35151 RVA: 0x001D0A58 File Offset: 0x001CEC58
		protected override TypeValue Type0
		{
			get
			{
				return this.type0;
			}
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x001D0A60 File Offset: 0x001CEC60
		public sealed override Value Invoke(Value arg0)
		{
			T0 t = arg0.As<T0>(this.type0);
			return this.TypedInvoke(t).As<TResult>(this.returnType);
		}

		// Token: 0x06008951 RID: 35153
		public abstract TResult TypedInvoke(T0 arg0);

		// Token: 0x04004BCA RID: 19402
		private readonly TypeValue returnType;

		// Token: 0x04004BCB RID: 19403
		private readonly TypeValue type0;
	}
}
