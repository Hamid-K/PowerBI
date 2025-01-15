using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200158B RID: 5515
	public abstract class NativeFunctionValue2<TResult, T0, T1> : NativeFunctionValue2 where TResult : Value where T0 : Value where T1 : Value
	{
		// Token: 0x0600895F RID: 35167 RVA: 0x001D0B97 File Offset: 0x001CED97
		protected NativeFunctionValue2(TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1)
			: base(min, param0, param1)
		{
			this.returnType = returnType;
			this.type0 = type0;
			this.type1 = type1;
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x001D0BBA File Offset: 0x001CEDBA
		protected NativeFunctionValue2(TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1)
			: this(returnType, 2, param0, type0, param1, type1)
		{
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x06008961 RID: 35169 RVA: 0x001D0BCA File Offset: 0x001CEDCA
		protected override TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x06008962 RID: 35170 RVA: 0x001D0BD2 File Offset: 0x001CEDD2
		protected override TypeValue Type0
		{
			get
			{
				return this.type0;
			}
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x06008963 RID: 35171 RVA: 0x001D0BDA File Offset: 0x001CEDDA
		protected override TypeValue Type1
		{
			get
			{
				return this.type1;
			}
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x001D0BE4 File Offset: 0x001CEDE4
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			T0 t = arg0.As<T0>(this.type0);
			T1 t2 = arg1.As<T1>(this.type1);
			return this.TypedInvoke(t, t2).As<TResult>(this.returnType);
		}

		// Token: 0x06008965 RID: 35173
		public abstract TResult TypedInvoke(T0 arg0, T1 arg1);

		// Token: 0x04004BD0 RID: 19408
		private readonly TypeValue returnType;

		// Token: 0x04004BD1 RID: 19409
		private readonly TypeValue type0;

		// Token: 0x04004BD2 RID: 19410
		private readonly TypeValue type1;
	}
}
