using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001588 RID: 5512
	public abstract class NativeFunctionValue1 : NativeFunctionValue
	{
		// Token: 0x06008942 RID: 35138 RVA: 0x001D097A File Offset: 0x001CEB7A
		protected NativeFunctionValue1()
			: this("arg0")
		{
		}

		// Token: 0x06008943 RID: 35139 RVA: 0x001D0987 File Offset: 0x001CEB87
		protected NativeFunctionValue1(string param0)
			: this(1, param0)
		{
		}

		// Token: 0x06008944 RID: 35140 RVA: 0x001D0991 File Offset: 0x001CEB91
		protected NativeFunctionValue1(int min, string param0)
		{
			this.min = min;
			this.param0 = param0;
		}

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x06008945 RID: 35141 RVA: 0x001D09A8 File Offset: 0x001CEBA8
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.New(Keys.New(this.Param0), new Value[] { this.Type0 }), this.Min);
				}
				return this.functionType;
			}
		}

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x06008946 RID: 35142 RVA: 0x001D09F9 File Offset: 0x001CEBF9
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x06008947 RID: 35143 RVA: 0x001D0A01 File Offset: 0x001CEC01
		protected string Param0
		{
			get
			{
				return this.param0;
			}
		}

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06008948 RID: 35144 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue ReturnType
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x06008949 RID: 35145 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue Type0
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x001D0A09 File Offset: 0x001CEC09
		public sealed override Value Invoke()
		{
			if (this.min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null);
		}

		// Token: 0x0600894B RID: 35147
		public abstract override Value Invoke(Value arg0);

		// Token: 0x04004BC7 RID: 19399
		private readonly int min;

		// Token: 0x04004BC8 RID: 19400
		private readonly string param0;

		// Token: 0x04004BC9 RID: 19401
		private TypeValue functionType;
	}
}
