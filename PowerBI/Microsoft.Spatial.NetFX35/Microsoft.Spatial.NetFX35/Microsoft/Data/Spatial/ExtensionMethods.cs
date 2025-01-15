using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200007D RID: 125
	internal static class ExtensionMethods
	{
		// Token: 0x060002FD RID: 765 RVA: 0x000085C5 File Offset: 0x000067C5
		public static void WriteRoundtrippable(this TextWriter writer, double d)
		{
			writer.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000085E0 File Offset: 0x000067E0
		internal static TResult? IfValidReturningNullable<TArg, TResult>(this TArg arg, Func<TArg, TResult> op) where TArg : class where TResult : struct
		{
			if (arg != null)
			{
				return new TResult?(op.Invoke(arg));
			}
			return default(TResult?);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000860C File Offset: 0x0000680C
		internal static TResult IfValid<TArg, TResult>(this TArg arg, Func<TArg, TResult> op) where TArg : class where TResult : class
		{
			if (arg != null)
			{
				return op.Invoke(arg);
			}
			return default(TResult);
		}
	}
}
