using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001304 RID: 4868
	public sealed class FoldableFunctionValue : DelegatingFunctionValue, IOpaqueFunctionValue, IFunctionValue, IValue
	{
		// Token: 0x060080A2 RID: 32930 RVA: 0x001B7179 File Offset: 0x001B5379
		public static FoldableFunctionValue New(FunctionValue function)
		{
			return new FoldableFunctionValue(function);
		}

		// Token: 0x060080A3 RID: 32931 RVA: 0x001B7181 File Offset: 0x001B5381
		private FoldableFunctionValue(FunctionValue function)
			: base(function)
		{
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x060080A4 RID: 32932 RVA: 0x001B718A File Offset: 0x001B538A
		public new FunctionValue Function
		{
			get
			{
				return base.Function;
			}
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x001B7192 File Offset: 0x001B5392
		protected override FunctionValue Wrap(FunctionValue function)
		{
			return new FoldableFunctionValue(function);
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x001B719A File Offset: 0x001B539A
		public override Value Invoke()
		{
			return this.Invoke(EmptyArray<Value>.Instance);
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x00189553 File Offset: 0x00187753
		public override Value Invoke(Value arg0)
		{
			return this.Invoke(new Value[] { arg0 });
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x00189565 File Offset: 0x00187765
		public override Value Invoke(Value arg0, Value arg1)
		{
			return this.Invoke(new Value[] { arg0, arg1 });
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x0018957B File Offset: 0x0018777B
		public override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2 });
		}

		// Token: 0x060080AA RID: 32938 RVA: 0x00189595 File Offset: 0x00187795
		public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x001895B4 File Offset: 0x001877B4
		public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3, arg4 });
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x001B71A7 File Offset: 0x001B53A7
		public override Value Invoke(params Value[] args)
		{
			return Value.Invoke(this, args);
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x001B71B0 File Offset: 0x001B53B0
		public override bool TryGetAs<T>(out T contract)
		{
			if (typeof(T) == typeof(IOpaqueFunctionValue))
			{
				contract = (T)((object)this);
				return true;
			}
			return base.TryGetAs<T>(out contract);
		}
	}
}
