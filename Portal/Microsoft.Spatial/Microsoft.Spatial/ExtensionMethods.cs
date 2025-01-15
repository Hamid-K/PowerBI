using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x02000068 RID: 104
	internal static class ExtensionMethods
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00006B3F File Offset: 0x00004D3F
		public static void WriteRoundtrippable(this TextWriter writer, double d)
		{
			writer.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00006B58 File Offset: 0x00004D58
		internal static TResult? IfValidReturningNullable<TArg, TResult>(this TArg arg, Func<TArg, TResult> op) where TArg : class where TResult : struct
		{
			if (arg != null)
			{
				return new TResult?(op(arg));
			}
			return null;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00006B84 File Offset: 0x00004D84
		internal static TResult IfValid<TArg, TResult>(this TArg arg, Func<TArg, TResult> op) where TArg : class where TResult : class
		{
			if (arg != null)
			{
				return op(arg);
			}
			return default(TResult);
		}
	}
}
