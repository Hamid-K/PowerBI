using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200007C RID: 124
	internal static class ExtensionMethods
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00008655 File Offset: 0x00006855
		public static void WriteRoundtrippable(this TextWriter writer, double d)
		{
			writer.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008670 File Offset: 0x00006870
		internal static TResult? IfValidReturningNullable<TArg, TResult>(this TArg arg, Func<TArg, TResult> op) where TArg : class where TResult : struct
		{
			if (arg != null)
			{
				return new TResult?(op.Invoke(arg));
			}
			return default(TResult?);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000869C File Offset: 0x0000689C
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
