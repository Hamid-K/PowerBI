using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000161 RID: 353
	public static class RuntimeTraceSourceConfiguration
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		public static void IgnoreTraceListener(string name)
		{
			object obj = RuntimeTraceSourceConfiguration.s_traceListenersToIgnoreLock;
			lock (obj)
			{
				RuntimeTraceSourceConfiguration.s_traceListenersToIgnore.Add(name);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001FE34 File Offset: 0x0001E034
		public static bool IsIgnored(string name)
		{
			object obj = RuntimeTraceSourceConfiguration.s_traceListenersToIgnoreLock;
			bool flag2;
			lock (obj)
			{
				flag2 = RuntimeTraceSourceConfiguration.s_traceListenersToIgnore.Contains(name);
			}
			return flag2;
		}

		// Token: 0x0400037A RID: 890
		private static readonly HashSet<string> s_traceListenersToIgnore = new HashSet<string>();

		// Token: 0x0400037B RID: 891
		private static readonly object s_traceListenersToIgnoreLock = new object();
	}
}
