using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D6 RID: 470
	public sealed class AssertUIDisabler : IDisposable
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x0002AB85 File Offset: 0x00028D85
		public AssertUIDisabler()
			: this(BehaviorOnAssertionFailure.Ignore)
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002AB90 File Offset: 0x00028D90
		public AssertUIDisabler(BehaviorOnAssertionFailure behaviorOnAssertionFailure)
		{
			this.m_traceListeners = new List<TraceListener>();
			this.m_uiDisabledListener = new List<DefaultTraceListener>();
			foreach (object obj in Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				ExtendedTraceListener extendedTraceListener = traceListener as ExtendedTraceListener;
				if (extendedTraceListener != null)
				{
					this.m_traceListeners.Add(extendedTraceListener);
				}
				else
				{
					DefaultTraceListener defaultTraceListener = traceListener as DefaultTraceListener;
					if (defaultTraceListener != null && defaultTraceListener.AssertUiEnabled)
					{
						this.m_uiDisabledListener.Add(defaultTraceListener);
						defaultTraceListener.AssertUiEnabled = false;
					}
				}
			}
			foreach (TraceListener traceListener2 in this.m_traceListeners)
			{
				Trace.Listeners.Remove(traceListener2);
			}
			this.m_traceListener = new ExtendedTraceListener(behaviorOnAssertionFailure, "");
			Trace.Listeners.Add(this.m_traceListener);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002ACA8 File Offset: 0x00028EA8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				Trace.Listeners.Remove(this.m_traceListener);
				foreach (TraceListener traceListener in this.m_traceListeners)
				{
					Trace.Listeners.Add(traceListener);
				}
				foreach (DefaultTraceListener defaultTraceListener in this.m_uiDisabledListener)
				{
					defaultTraceListener.AssertUiEnabled = true;
				}
			}
		}

		// Token: 0x040004A7 RID: 1191
		private List<TraceListener> m_traceListeners;

		// Token: 0x040004A8 RID: 1192
		private ExtendedTraceListener m_traceListener;

		// Token: 0x040004A9 RID: 1193
		private List<DefaultTraceListener> m_uiDisabledListener;
	}
}
