using System;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001590 RID: 5520
	public abstract class NativeFunctionValueN : NativeFunctionValue
	{
		// Token: 0x060089AF RID: 35247 RVA: 0x001D151E File Offset: 0x001CF71E
		protected NativeFunctionValueN(int min, params string[] parameters)
		{
			this.min = min;
			this.parameters = parameters;
		}

		// Token: 0x060089B0 RID: 35248 RVA: 0x001D1534 File Offset: 0x001CF734
		protected NativeFunctionValueN(FunctionTypeValue functionType)
		{
			this.min = functionType.Min;
			this.parameters = functionType.Parameters.Keys.ToArray<string>();
			this.functionType = functionType;
		}

		// Token: 0x17002461 RID: 9313
		// (get) Token: 0x060089B1 RID: 35249 RVA: 0x001D1568 File Offset: 0x001CF768
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.New(Keys.New(this.ParamNames), (int i) => this.ParamType(i)), this.Min);
				}
				return this.functionType;
			}
		}

		// Token: 0x17002462 RID: 9314
		// (get) Token: 0x060089B2 RID: 35250 RVA: 0x001D15B6 File Offset: 0x001CF7B6
		protected int Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17002463 RID: 9315
		// (get) Token: 0x060089B3 RID: 35251 RVA: 0x001D15BE File Offset: 0x001CF7BE
		protected string[] ParamNames
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002464 RID: 9316
		// (get) Token: 0x060089B4 RID: 35252 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue ReturnType
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x060089B5 RID: 35253 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue ParamType(int index)
		{
			return TypeValue.Any;
		}

		// Token: 0x060089B6 RID: 35254 RVA: 0x00189545 File Offset: 0x00187745
		public sealed override Value Invoke()
		{
			return this.Invoke(new Value[0]);
		}

		// Token: 0x060089B7 RID: 35255 RVA: 0x00189553 File Offset: 0x00187753
		public sealed override Value Invoke(Value arg0)
		{
			return this.Invoke(new Value[] { arg0 });
		}

		// Token: 0x060089B8 RID: 35256 RVA: 0x00189565 File Offset: 0x00187765
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			return this.Invoke(new Value[] { arg0, arg1 });
		}

		// Token: 0x060089B9 RID: 35257 RVA: 0x0018957B File Offset: 0x0018777B
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2 });
		}

		// Token: 0x060089BA RID: 35258 RVA: 0x00189595 File Offset: 0x00187795
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x060089BB RID: 35259 RVA: 0x001895B4 File Offset: 0x001877B4
		public sealed override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3, arg4 });
		}

		// Token: 0x060089BC RID: 35260 RVA: 0x001D15C8 File Offset: 0x001CF7C8
		public sealed override Value Invoke(params Value[] args)
		{
			if (args.Length < this.min || args.Length > this.parameters.Length)
			{
				throw ValueException.InvalidArguments(this, args);
			}
			if (args.Length != this.parameters.Length)
			{
				Value[] array = new Value[this.parameters.Length];
				Array.Copy(args, array, args.Length);
				for (int i = args.Length; i < this.parameters.Length; i++)
				{
					array[i] = Value.Null;
				}
				args = array;
			}
			return this.InvokeN(args);
		}

		// Token: 0x060089BD RID: 35261
		protected abstract Value InvokeN(Value[] args);

		// Token: 0x04004BF9 RID: 19449
		private readonly int min;

		// Token: 0x04004BFA RID: 19450
		private readonly string[] parameters;

		// Token: 0x04004BFB RID: 19451
		private TypeValue functionType;
	}
}
