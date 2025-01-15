using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200158E RID: 5518
	public abstract class NativeFunctionValue4<TResult, T0, T1, T2, T3> : NativeFunctionValue where TResult : Value where T0 : Value where T1 : Value where T2 : Value where T3 : Value
	{
		// Token: 0x06008986 RID: 35206 RVA: 0x001D0F78 File Offset: 0x001CF178
		protected NativeFunctionValue4(TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2, string param3, TypeValue type3)
		{
			this.min = min;
			this.param0 = param0;
			this.param1 = param1;
			this.param2 = param2;
			this.param3 = param3;
			this.returnType = returnType;
			this.type0 = type0;
			this.type1 = type1;
			this.type2 = type2;
			this.type3 = type3;
		}

		// Token: 0x06008987 RID: 35207 RVA: 0x001D0FD8 File Offset: 0x001CF1D8
		protected NativeFunctionValue4(TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2, string param3, TypeValue type3)
			: this(returnType, 4, param0, type0, param1, type1, param2, type2, param3, type3)
		{
		}

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x06008988 RID: 35208 RVA: 0x001D0FFC File Offset: 0x001CF1FC
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.New(Keys.New(this.Param0, this.Param1, this.Param2, this.Param3), new Value[] { this.Type0, this.Type1, this.Type2, this.Type3 }), this.Min);
				}
				return this.functionType;
			}
		}

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x06008989 RID: 35209 RVA: 0x001D107A File Offset: 0x001CF27A
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x0600898A RID: 35210 RVA: 0x001D1082 File Offset: 0x001CF282
		protected string Param0
		{
			get
			{
				return this.param0;
			}
		}

		// Token: 0x1700244C RID: 9292
		// (get) Token: 0x0600898B RID: 35211 RVA: 0x001D108A File Offset: 0x001CF28A
		protected string Param1
		{
			get
			{
				return this.param1;
			}
		}

		// Token: 0x1700244D RID: 9293
		// (get) Token: 0x0600898C RID: 35212 RVA: 0x001D1092 File Offset: 0x001CF292
		protected string Param2
		{
			get
			{
				return this.param2;
			}
		}

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x0600898D RID: 35213 RVA: 0x001D109A File Offset: 0x001CF29A
		protected string Param3
		{
			get
			{
				return this.param3;
			}
		}

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x0600898E RID: 35214 RVA: 0x001D10A2 File Offset: 0x001CF2A2
		protected virtual TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x0600898F RID: 35215 RVA: 0x001D10AA File Offset: 0x001CF2AA
		protected virtual TypeValue Type0
		{
			get
			{
				return this.type0;
			}
		}

		// Token: 0x17002451 RID: 9297
		// (get) Token: 0x06008990 RID: 35216 RVA: 0x001D10B2 File Offset: 0x001CF2B2
		protected virtual TypeValue Type1
		{
			get
			{
				return this.type1;
			}
		}

		// Token: 0x17002452 RID: 9298
		// (get) Token: 0x06008991 RID: 35217 RVA: 0x001D10BA File Offset: 0x001CF2BA
		protected virtual TypeValue Type2
		{
			get
			{
				return this.type2;
			}
		}

		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x06008992 RID: 35218 RVA: 0x001D10C2 File Offset: 0x001CF2C2
		protected virtual TypeValue Type3
		{
			get
			{
				return this.type3;
			}
		}

		// Token: 0x06008993 RID: 35219 RVA: 0x001D10CA File Offset: 0x001CF2CA
		public sealed override Value Invoke()
		{
			if (this.min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null, Value.Null, Value.Null);
		}

		// Token: 0x06008994 RID: 35220 RVA: 0x001D10FB File Offset: 0x001CF2FB
		public sealed override Value Invoke(Value arg0)
		{
			if (this.min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null, Value.Null, Value.Null);
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x001D112D File Offset: 0x001CF32D
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			if (this.min > 2)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1 });
			}
			return this.Invoke(arg0, arg1, Value.Null, Value.Null);
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x001D115F File Offset: 0x001CF35F
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			if (this.min > 3)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1, arg2 });
			}
			return this.Invoke(arg0, arg1, arg2, Value.Null);
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x001D1194 File Offset: 0x001CF394
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			T0 t = arg0.As<T0>(this.type0);
			T1 t2 = arg1.As<T1>(this.type1);
			T2 t3 = arg2.As<T2>(this.type2);
			T3 t4 = arg3.As<T3>(this.type3);
			return this.TypedInvoke(t, t2, t3, t4).As<TResult>(this.returnType);
		}

		// Token: 0x06008998 RID: 35224
		public abstract TResult TypedInvoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3);

		// Token: 0x04004BE1 RID: 19425
		private readonly int min;

		// Token: 0x04004BE2 RID: 19426
		private readonly string param0;

		// Token: 0x04004BE3 RID: 19427
		private readonly string param1;

		// Token: 0x04004BE4 RID: 19428
		private readonly string param2;

		// Token: 0x04004BE5 RID: 19429
		private readonly string param3;

		// Token: 0x04004BE6 RID: 19430
		private TypeValue functionType;

		// Token: 0x04004BE7 RID: 19431
		private readonly TypeValue returnType;

		// Token: 0x04004BE8 RID: 19432
		private readonly TypeValue type0;

		// Token: 0x04004BE9 RID: 19433
		private readonly TypeValue type1;

		// Token: 0x04004BEA RID: 19434
		private readonly TypeValue type2;

		// Token: 0x04004BEB RID: 19435
		private readonly TypeValue type3;
	}
}
