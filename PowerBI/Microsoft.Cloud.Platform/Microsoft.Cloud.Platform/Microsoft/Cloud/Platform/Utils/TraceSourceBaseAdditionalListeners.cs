using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000160 RID: 352
	internal static class TraceSourceBaseAdditionalListeners
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0001FD48 File Offset: 0x0001DF48
		internal static void Add(TraceListener listener)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TraceListener>(listener, "listener");
			List<TraceListener> additionalListeners = TraceSourceBaseAdditionalListeners.m_additionalListeners;
			lock (additionalListeners)
			{
				TraceSourceBaseAdditionalListeners.m_additionalListeners.Add(listener);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001FD98 File Offset: 0x0001DF98
		internal static IEnumerable<TraceListener> Listeners
		{
			get
			{
				List<TraceListener> additionalListeners = TraceSourceBaseAdditionalListeners.m_additionalListeners;
				IEnumerable<TraceListener> enumerable;
				lock (additionalListeners)
				{
					enumerable = TraceSourceBaseAdditionalListeners.m_additionalListeners.ToArray();
				}
				return enumerable;
			}
		}

		// Token: 0x04000379 RID: 889
		private static List<TraceListener> m_additionalListeners = new List<TraceListener>();
	}
}
