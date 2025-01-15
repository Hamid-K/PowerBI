using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200159F RID: 5535
	internal sealed class OpaqueProxyFunctionValue<T> : NativeFunctionValue0<Value>
	{
		// Token: 0x06008ADB RID: 35547 RVA: 0x001D38D3 File Offset: 0x001D1AD3
		public OpaqueProxyFunctionValue(T value)
			: base(TypeValue.Any)
		{
			this.value = value;
		}

		// Token: 0x06008ADC RID: 35548 RVA: 0x0004F290 File Offset: 0x0004D490
		public override Value TypedInvoke()
		{
			throw ValueException.NewExpressionError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x001D38E8 File Offset: 0x001D1AE8
		public static bool TryGet(FunctionValue function, out T value)
		{
			OpaqueProxyFunctionValue<T> opaqueProxyFunctionValue = function as OpaqueProxyFunctionValue<T>;
			if (opaqueProxyFunctionValue != null)
			{
				value = opaqueProxyFunctionValue.value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x04004C21 RID: 19489
		private readonly T value;
	}
}
