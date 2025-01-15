using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x02000063 RID: 99
	internal static class ExtensionMethods
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00005E77 File Offset: 0x00004077
		public static void WriteRoundtrippable(this TextWriter writer, double d)
		{
			writer.Write(d.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005E90 File Offset: 0x00004090
		internal static TResult? IfValidReturningNullable<TArg, TResult>(this TArg arg, Func<TArg, TResult> op) where TArg : class where TResult : struct
		{
			if (arg != null)
			{
				return new TResult?(op.Invoke(arg));
			}
			return default(TResult?);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00005EBC File Offset: 0x000040BC
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
