using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001591 RID: 5521
	public abstract class NativeFunctionValueN<TResult> : NativeFunctionValueN where TResult : Value
	{
		// Token: 0x060089BF RID: 35263 RVA: 0x001D164A File Offset: 0x001CF84A
		protected NativeFunctionValueN(TypeValue returnType, int min, string[] parameters, TypeValue[] types)
			: base(min, parameters)
		{
			this.returnType = returnType;
			this.types = types;
		}

		// Token: 0x060089C0 RID: 35264 RVA: 0x001D1663 File Offset: 0x001CF863
		protected NativeFunctionValueN(TypeValue returnType, string[] parameters, TypeValue[] types)
			: this(returnType, parameters.Length, parameters, types)
		{
		}

		// Token: 0x17002465 RID: 9317
		// (get) Token: 0x060089C1 RID: 35265 RVA: 0x001D1671 File Offset: 0x001CF871
		protected override TypeValue ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x060089C2 RID: 35266 RVA: 0x001D1679 File Offset: 0x001CF879
		protected override TypeValue ParamType(int index)
		{
			return this.types[index];
		}

		// Token: 0x060089C3 RID: 35267 RVA: 0x001D1684 File Offset: 0x001CF884
		protected sealed override Value InvokeN(Value[] args)
		{
			Value[] array = new Value[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = args[i].As<Value>(this.types[i]);
			}
			return this.TypedInvokeN(array).As<TResult>(this.returnType);
		}

		// Token: 0x060089C4 RID: 35268
		protected abstract TResult TypedInvokeN(Value[] args);

		// Token: 0x04004BFC RID: 19452
		private readonly TypeValue returnType;

		// Token: 0x04004BFD RID: 19453
		private readonly TypeValue[] types;
	}
}
