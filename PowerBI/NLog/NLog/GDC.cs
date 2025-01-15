using System;

namespace NLog
{
	// Token: 0x02000002 RID: 2
	[Obsolete("Use GlobalDiagnosticsContext class instead. Marked obsolete on NLog 2.0")]
	public static class GDC
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void Set(string item, string value)
		{
			GlobalDiagnosticsContext.Set(item, value);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		public static string Get(string item)
		{
			return GlobalDiagnosticsContext.Get(item);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return GlobalDiagnosticsContext.Get(item, formatProvider);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206A File Offset: 0x0000026A
		public static object GetObject(string item)
		{
			return GlobalDiagnosticsContext.GetObject(item);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public static bool Contains(string item)
		{
			return GlobalDiagnosticsContext.Contains(item);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public static void Remove(string item)
		{
			GlobalDiagnosticsContext.Remove(item);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002082 File Offset: 0x00000282
		public static void Clear()
		{
			GlobalDiagnosticsContext.Clear();
		}
	}
}
