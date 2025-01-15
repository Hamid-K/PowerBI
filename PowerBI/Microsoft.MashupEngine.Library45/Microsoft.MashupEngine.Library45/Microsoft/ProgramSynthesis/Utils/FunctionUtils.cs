using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200048E RID: 1166
	public static class FunctionUtils
	{
		// Token: 0x06001A42 RID: 6722 RVA: 0x0004F45F File Offset: 0x0004D65F
		public static Func<T, T> IdentityIfNull<T>(this Func<T, T> fun)
		{
			Func<T, T> func = fun;
			if (fun == null && (func = FunctionUtils.<>c__0<T>.<>9__0_0) == null)
			{
				func = (FunctionUtils.<>c__0<T>.<>9__0_0 = (T v) => v);
			}
			return func;
		}
	}
}
