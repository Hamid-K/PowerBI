using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200158A RID: 5514
	public abstract class NativeFunctionValue2 : NativeFunctionValue
	{
		// Token: 0x06008952 RID: 35154 RVA: 0x001D0A96 File Offset: 0x001CEC96
		protected NativeFunctionValue2()
			: this("arg0", "arg1")
		{
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x001D0AA8 File Offset: 0x001CECA8
		protected NativeFunctionValue2(string param0, string param1)
			: this(2, param0, param1)
		{
		}

		// Token: 0x06008954 RID: 35156 RVA: 0x001D0AB3 File Offset: 0x001CECB3
		protected NativeFunctionValue2(int min, string param0, string param1)
		{
			this.min = min;
			this.param0 = param0;
			this.param1 = param1;
		}

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x06008955 RID: 35157 RVA: 0x001D0AD0 File Offset: 0x001CECD0
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.New(Keys.New(this.Param0, this.Param1), new Value[] { this.Type0, this.Type1 }), this.Min);
				}
				return this.functionType;
			}
		}

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x06008956 RID: 35158 RVA: 0x001D0B30 File Offset: 0x001CED30
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x06008957 RID: 35159 RVA: 0x001D0B38 File Offset: 0x001CED38
		protected string Param0
		{
			get
			{
				return this.param0;
			}
		}

		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x06008958 RID: 35160 RVA: 0x001D0B40 File Offset: 0x001CED40
		protected string Param1
		{
			get
			{
				return this.param1;
			}
		}

		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x06008959 RID: 35161 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue ReturnType
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x0600895A RID: 35162 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue Type0
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x0600895B RID: 35163 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue Type1
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x001D0B48 File Offset: 0x001CED48
		public sealed override Value Invoke()
		{
			if (this.min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null);
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x001D0B6F File Offset: 0x001CED6F
		public sealed override Value Invoke(Value arg0)
		{
			if (this.min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null);
		}

		// Token: 0x0600895E RID: 35166
		public abstract override Value Invoke(Value arg0, Value arg1);

		// Token: 0x04004BCC RID: 19404
		private readonly int min;

		// Token: 0x04004BCD RID: 19405
		private readonly string param0;

		// Token: 0x04004BCE RID: 19406
		private readonly string param1;

		// Token: 0x04004BCF RID: 19407
		private TypeValue functionType;
	}
}
