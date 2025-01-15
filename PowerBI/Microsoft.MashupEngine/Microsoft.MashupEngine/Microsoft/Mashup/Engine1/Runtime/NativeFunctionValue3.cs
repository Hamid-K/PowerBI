using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200158C RID: 5516
	public abstract class NativeFunctionValue3 : NativeFunctionValue
	{
		// Token: 0x06008966 RID: 35174 RVA: 0x001D0C28 File Offset: 0x001CEE28
		protected NativeFunctionValue3()
			: this("arg0", "arg1", "arg2")
		{
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x001D0C3F File Offset: 0x001CEE3F
		protected NativeFunctionValue3(string param0, string param1, string param2)
			: this(3, param0, param1, param2)
		{
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x001D0C4B File Offset: 0x001CEE4B
		protected NativeFunctionValue3(int min, string param0, string param1, string param2)
		{
			this.min = min;
			this.param0 = param0;
			this.param1 = param1;
			this.param2 = param2;
		}

		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x06008969 RID: 35177 RVA: 0x001D0C70 File Offset: 0x001CEE70
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

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x0600896A RID: 35178 RVA: 0x001D0CDF File Offset: 0x001CEEDF
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x0600896B RID: 35179 RVA: 0x001D0CE7 File Offset: 0x001CEEE7
		protected string Param0
		{
			get
			{
				return this.param0;
			}
		}

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x0600896C RID: 35180 RVA: 0x001D0CEF File Offset: 0x001CEEEF
		protected string Param1
		{
			get
			{
				return this.param1;
			}
		}

		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x0600896D RID: 35181 RVA: 0x001D0CF7 File Offset: 0x001CEEF7
		protected string Param2
		{
			get
			{
				return this.param2;
			}
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x0600896E RID: 35182 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue ReturnType
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x0600896F RID: 35183 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue Type0
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x06008970 RID: 35184 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue Type1
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x1700243F RID: 9279
		// (get) Token: 0x06008971 RID: 35185 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue Type2
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x06008972 RID: 35186 RVA: 0x001D0CFF File Offset: 0x001CEEFF
		public sealed override Value Invoke()
		{
			if (this.min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null);
		}

		// Token: 0x06008973 RID: 35187 RVA: 0x001D0D26 File Offset: 0x001CEF26
		public sealed override Value Invoke(Value arg0)
		{
			if (this.min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null);
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x001D0D4E File Offset: 0x001CEF4E
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			if (this.min > 2)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0, arg1 });
			}
			return this.Invoke(arg0, arg1, Value.Null);
		}

		// Token: 0x06008975 RID: 35189
		public abstract override Value Invoke(Value arg0, Value arg1, Value arg2);

		// Token: 0x04004BD3 RID: 19411
		private readonly int min;

		// Token: 0x04004BD4 RID: 19412
		private readonly string param0;

		// Token: 0x04004BD5 RID: 19413
		private readonly string param1;

		// Token: 0x04004BD6 RID: 19414
		private readonly string param2;

		// Token: 0x04004BD7 RID: 19415
		private TypeValue functionType;
	}
}
