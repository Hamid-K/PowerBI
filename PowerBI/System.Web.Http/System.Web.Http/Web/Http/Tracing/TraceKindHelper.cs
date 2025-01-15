using System;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000116 RID: 278
	internal static class TraceKindHelper
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x0001279B File Offset: 0x0001099B
		public static bool IsDefined(TraceKind traceKind)
		{
			return traceKind == TraceKind.Trace || traceKind == TraceKind.Begin || traceKind == TraceKind.End;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000127AA File Offset: 0x000109AA
		public static void Validate(TraceKind value, string parameterValue)
		{
			if (!TraceKindHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterValue, (int)value, typeof(TraceKind));
			}
		}
	}
}
