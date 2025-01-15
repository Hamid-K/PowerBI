using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200158F RID: 5519
	public abstract class NativeFunctionValue5<TResult, T0, T1, T2, T3, T4> : NativeFunctionValue where TResult : Value where T0 : Value where T1 : Value where T2 : Value where T3 : Value where T4 : Value
	{
		// Token: 0x06008999 RID: 35225 RVA: 0x001D11F8 File Offset: 0x001CF3F8
		protected NativeFunctionValue5(TypeValue returnType, int min, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2, string param3, TypeValue type3, string param4, TypeValue type4)
		{
			this.min = min;
			this.param0 = param0;
			this.param1 = param1;
			this.param2 = param2;
			this.param3 = param3;
			this.param4 = param4;
			this.returnType = returnType;
			this.type0 = type0;
			this.type1 = type1;
			this.type2 = type2;
			this.type3 = type3;
			this.type4 = type4;
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x001D1268 File Offset: 0x001CF468
		protected NativeFunctionValue5(TypeValue returnType, string param0, TypeValue type0, string param1, TypeValue type1, string param2, TypeValue type2, string param3, TypeValue type3, string param4, TypeValue type4)
			: this(returnType, 5, param0, type0, param1, type1, param2, type2, param3, type3, param4, type4)
		{
		}

		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x0600899B RID: 35227 RVA: 0x001D1290 File Offset: 0x001CF490
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.New(Keys.New(new string[] { this.Param0, this.Param1, this.Param2, this.Param3, this.Param4 }), new Value[] { this.Type0, this.Type1, this.Type2, this.Type3, this.Type4 }), this.Min);
				}
				return this.functionType;
			}
		}

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x0600899C RID: 35228 RVA: 0x001D1335 File Offset: 0x001CF535
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x0600899D RID: 35229 RVA: 0x001D133D File Offset: 0x001CF53D
		protected string Param0
		{
			get
			{
				return this.param0;
			}
		}

		// Token: 0x17002457 RID: 9303
		// (get) Token: 0x0600899E RID: 35230 RVA: 0x001D1345 File Offset: 0x001CF545
		protected string Param1
		{
			get
			{
				return this.param1;
			}
		}

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x0600899F RID: 35231 RVA: 0x001D134D File Offset: 0x001CF54D
		protected string Param2
		{
			get
			{
				return this.param2;
			}
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x060089A0 RID: 35232 RVA: 0x001D1355 File Offset: 0x001CF555
		protected string Param3
		{
			get
			{
				return this.param3;
			}
		}

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x060089A1 RID: 35233 RVA: 0x001D135D File Offset: 0x001CF55D
		protected string Param4
		{
			get
			{
				return this.param4;
			}
		}

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x060089A2 RID: 35234 RVA: 0x001D1365 File Offset: 0x001CF565
		protected virtual TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x060089A3 RID: 35235 RVA: 0x001D136D File Offset: 0x001CF56D
		protected virtual TypeValue Type0
		{
			get
			{
				return this.type0;
			}
		}

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x060089A4 RID: 35236 RVA: 0x001D1375 File Offset: 0x001CF575
		protected virtual TypeValue Type1
		{
			get
			{
				return this.type1;
			}
		}

		// Token: 0x1700245E RID: 9310
		// (get) Token: 0x060089A5 RID: 35237 RVA: 0x001D137D File Offset: 0x001CF57D
		protected virtual TypeValue Type2
		{
			get
			{
				return this.type2;
			}
		}

		// Token: 0x1700245F RID: 9311
		// (get) Token: 0x060089A6 RID: 35238 RVA: 0x001D1385 File Offset: 0x001CF585
		protected virtual TypeValue Type3
		{
			get
			{
				return this.type3;
			}
		}

		// Token: 0x17002460 RID: 9312
		// (get) Token: 0x060089A7 RID: 35239 RVA: 0x001D138D File Offset: 0x001CF58D
		protected virtual TypeValue Type4
		{
			get
			{
				return this.type4;
			}
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x001D1395 File Offset: 0x001CF595
		public sealed override Value Invoke()
		{
			if (this.min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null, Value.Null, Value.Null, Value.Null);
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x001D13CB File Offset: 0x001CF5CB
		public sealed override Value Invoke(Value arg0)
		{
			if (this.min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null, Value.Null, Value.Null, Value.Null);
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x001D1402 File Offset: 0x001CF602
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			if (this.min > 2)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1 });
			}
			return this.Invoke(arg0, arg1, Value.Null, Value.Null, Value.Null);
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x001D1439 File Offset: 0x001CF639
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			if (this.min > 3)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1, arg2 });
			}
			return this.Invoke(arg0, arg1, arg2, Value.Null, Value.Null);
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x001D1470 File Offset: 0x001CF670
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			if (this.min > 4)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1, arg2, arg3 });
			}
			return this.Invoke(arg0, arg1, arg2, arg3, Value.Null);
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x001D14AC File Offset: 0x001CF6AC
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			T0 t = arg0.As<T0>(this.type0);
			T1 t2 = arg1.As<T1>(this.type1);
			T2 t3 = arg2.As<T2>(this.type2);
			T3 t4 = arg3.As<T3>(this.type3);
			T4 t5 = arg4.As<T4>(this.type4);
			return this.TypedInvoke(t, t2, t3, t4, t5).As<TResult>(this.returnType);
		}

		// Token: 0x060089AE RID: 35246
		public abstract TResult TypedInvoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

		// Token: 0x04004BEC RID: 19436
		private readonly int min;

		// Token: 0x04004BED RID: 19437
		private readonly string param0;

		// Token: 0x04004BEE RID: 19438
		private readonly string param1;

		// Token: 0x04004BEF RID: 19439
		private readonly string param2;

		// Token: 0x04004BF0 RID: 19440
		private readonly string param3;

		// Token: 0x04004BF1 RID: 19441
		private readonly string param4;

		// Token: 0x04004BF2 RID: 19442
		private TypeValue functionType;

		// Token: 0x04004BF3 RID: 19443
		private readonly TypeValue returnType;

		// Token: 0x04004BF4 RID: 19444
		private readonly TypeValue type0;

		// Token: 0x04004BF5 RID: 19445
		private readonly TypeValue type1;

		// Token: 0x04004BF6 RID: 19446
		private readonly TypeValue type2;

		// Token: 0x04004BF7 RID: 19447
		private readonly TypeValue type3;

		// Token: 0x04004BF8 RID: 19448
		private readonly TypeValue type4;
	}
}
