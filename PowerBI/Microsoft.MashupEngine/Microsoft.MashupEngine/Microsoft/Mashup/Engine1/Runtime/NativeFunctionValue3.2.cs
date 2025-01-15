using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200158D RID: 5517
	public abstract class NativeFunctionValue3<TResult, T0, T1, T2> : NativeFunctionValue where TResult : Value where T0 : Value where T1 : Value where T2 : Value
	{
		// Token: 0x06008976 RID: 35190 RVA: 0x001D0D7C File Offset: 0x001CEF7C
		protected NativeFunctionValue3(TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2)
		{
			this.min = min;
			this.param0 = param0;
			this.param1 = param1;
			this.param2 = param2;
			this.returnType = returnType;
			this.type0 = type0;
			this.type1 = type1;
			this.type2 = type2;
		}

		// Token: 0x06008977 RID: 35191 RVA: 0x001D0DCC File Offset: 0x001CEFCC
		protected NativeFunctionValue3(TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2)
			: this(returnType, 3, param0, type0, param1, type1, param2, type2)
		{
		}

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x06008978 RID: 35192 RVA: 0x001D0DEC File Offset: 0x001CEFEC
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.New(Keys.New(this.Param0, this.Param1, this.Param2), new Value[] { this.Type0, this.Type1, this.Type2 }), this.Min);
				}
				return this.functionType;
			}
		}

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x06008979 RID: 35193 RVA: 0x001D0E5B File Offset: 0x001CF05B
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x0600897A RID: 35194 RVA: 0x001D0E63 File Offset: 0x001CF063
		protected string Param0
		{
			get
			{
				return this.param0;
			}
		}

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x0600897B RID: 35195 RVA: 0x001D0E6B File Offset: 0x001CF06B
		protected string Param1
		{
			get
			{
				return this.param1;
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x0600897C RID: 35196 RVA: 0x001D0E73 File Offset: 0x001CF073
		protected string Param2
		{
			get
			{
				return this.param2;
			}
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x0600897D RID: 35197 RVA: 0x001D0E7B File Offset: 0x001CF07B
		protected TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x0600897E RID: 35198 RVA: 0x001D0E83 File Offset: 0x001CF083
		protected TypeValue Type0
		{
			get
			{
				return this.type0;
			}
		}

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x0600897F RID: 35199 RVA: 0x001D0E8B File Offset: 0x001CF08B
		protected TypeValue Type1
		{
			get
			{
				return this.type1;
			}
		}

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x06008980 RID: 35200 RVA: 0x001D0E93 File Offset: 0x001CF093
		protected TypeValue Type2
		{
			get
			{
				return this.type2;
			}
		}

		// Token: 0x06008981 RID: 35201 RVA: 0x001D0E9B File Offset: 0x001CF09B
		public sealed override Value Invoke()
		{
			if (this.min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null, Value.Null);
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x001D0EC7 File Offset: 0x001CF0C7
		public sealed override Value Invoke(Value arg0)
		{
			if (this.min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null, Value.Null);
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x001D0EF4 File Offset: 0x001CF0F4
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			if (this.min > 2)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1 });
			}
			return this.Invoke(arg0, arg1, Value.Null);
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x001D0F24 File Offset: 0x001CF124
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			T0 t = arg0.As<T0>(this.type0);
			T1 t2 = arg1.As<T1>(this.type1);
			T2 t3 = arg2.As<T2>(this.type2);
			return this.TypedInvoke(t, t2, t3).As<TResult>(this.returnType);
		}

		// Token: 0x06008985 RID: 35205
		public abstract TResult TypedInvoke(T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x04004BD8 RID: 19416
		private readonly int min;

		// Token: 0x04004BD9 RID: 19417
		private readonly string param0;

		// Token: 0x04004BDA RID: 19418
		private readonly string param1;

		// Token: 0x04004BDB RID: 19419
		private readonly string param2;

		// Token: 0x04004BDC RID: 19420
		private readonly TypeValue returnType;

		// Token: 0x04004BDD RID: 19421
		private readonly TypeValue type0;

		// Token: 0x04004BDE RID: 19422
		private readonly TypeValue type1;

		// Token: 0x04004BDF RID: 19423
		private readonly TypeValue type2;

		// Token: 0x04004BE0 RID: 19424
		private TypeValue functionType;
	}
}
