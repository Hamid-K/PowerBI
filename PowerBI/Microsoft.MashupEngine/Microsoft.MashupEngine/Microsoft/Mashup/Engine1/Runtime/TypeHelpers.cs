using System;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200167C RID: 5756
	internal static class TypeHelpers
	{
		// Token: 0x060091B7 RID: 37303 RVA: 0x001E4830 File Offset: 0x001E2A30
		public static bool IsListOf(this Value value, Func<Value, bool> predicate)
		{
			return value.IsList && value.AsList.All((IValueReference vr) => predicate(vr.Value));
		}

		// Token: 0x060091B8 RID: 37304 RVA: 0x001E486C File Offset: 0x001E2A6C
		public static Type GetUnderlyingClrType(this Value value)
		{
			object obj = value;
			FunctionValue functionValue;
			if (value.IsFunction && value.AsFunction.TryGetAs<FunctionValue>(out functionValue))
			{
				obj = functionValue;
			}
			return obj.GetType();
		}
	}
}
