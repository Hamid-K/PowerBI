using System;

namespace NLog
{
	// Token: 0x02000016 RID: 22
	[Obsolete("Use MappedDiagnosticsContext class instead. Marked obsolete on NLog 2.0")]
	public static class MDC
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00008580 File Offset: 0x00006780
		public static void Set(string item, string value)
		{
			MappedDiagnosticsContext.Set(item, value);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00008589 File Offset: 0x00006789
		public static string Get(string item)
		{
			return MappedDiagnosticsContext.Get(item);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00008591 File Offset: 0x00006791
		public static object GetObject(string item)
		{
			return MappedDiagnosticsContext.GetObject(item);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00008599 File Offset: 0x00006799
		public static bool Contains(string item)
		{
			return MappedDiagnosticsContext.Contains(item);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000085A1 File Offset: 0x000067A1
		public static void Remove(string item)
		{
			MappedDiagnosticsContext.Remove(item);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000085A9 File Offset: 0x000067A9
		public static void Clear()
		{
			MappedDiagnosticsContext.Clear();
		}
	}
}
