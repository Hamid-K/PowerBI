using System;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000115 RID: 277
	internal static class TraceLevelHelper
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x00012764 File Offset: 0x00010964
		public static bool IsDefined(TraceLevel traceLevel)
		{
			return traceLevel == TraceLevel.Off || traceLevel == TraceLevel.Debug || traceLevel == TraceLevel.Info || traceLevel == TraceLevel.Warn || traceLevel == TraceLevel.Error || traceLevel == TraceLevel.Fatal;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001277F File Offset: 0x0001097F
		public static void Validate(TraceLevel value, string parameterValue)
		{
			if (!TraceLevelHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterValue, (int)value, typeof(TraceLevel));
			}
		}
	}
}
