using System;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing
{
	// Token: 0x020000D1 RID: 209
	public static class EngineTracer
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000133D8 File Offset: 0x000115D8
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001343C File Offset: 0x0001163C
		public static IEngineTracerDestination InternalTrace
		{
			get
			{
				IEngineTracerDestination engineTracerDestination = EngineTracer._traceDestination;
				if (engineTracerDestination == null)
				{
					object tracerWLock = EngineTracer._tracerWLock;
					lock (tracerWLock)
					{
						if (EngineTracer._traceDestination == null)
						{
							EngineTracer._traceDestination = new NullTracerDestination();
						}
						engineTracerDestination = EngineTracer._traceDestination;
					}
				}
				return engineTracerDestination;
			}
			set
			{
				object tracerWLock = EngineTracer._tracerWLock;
				lock (tracerWLock)
				{
					EngineTracer._traceDestination = value;
					EngineTracer.Info("EngineTracer set to " + value.ToString());
				}
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00013494 File Offset: 0x00011694
		public static void Log(EngineTracer.Level level, Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, level, messageFormat, parameters);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000134A4 File Offset: 0x000116A4
		public static void Log(EngineTracer.Level level, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(level, messageFormat, parameters);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000134B3 File Offset: 0x000116B3
		public static void Log(EngineTracer.Level level, string message)
		{
			EngineTracer.InternalTrace.Log(level, message);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000134C1 File Offset: 0x000116C1
		public static void Trace(Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, EngineTracer.Level.Trace, messageFormat, parameters);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000134D1 File Offset: 0x000116D1
		public static void Trace(string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Trace, messageFormat, parameters);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000134E0 File Offset: 0x000116E0
		public static void Trace(string message)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Trace, message);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000134EE File Offset: 0x000116EE
		public static void Debug(Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, EngineTracer.Level.Debug, messageFormat, parameters);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000134FE File Offset: 0x000116FE
		public static void Debug(string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Debug, messageFormat, parameters);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001350D File Offset: 0x0001170D
		public static void Debug(string message)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Debug, message);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001351B File Offset: 0x0001171B
		public static void Info(Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, EngineTracer.Level.Info, messageFormat, parameters);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001352B File Offset: 0x0001172B
		public static void Info(string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Info, messageFormat, parameters);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001353A File Offset: 0x0001173A
		public static void Info(string message)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Info, message);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00013548 File Offset: 0x00011748
		public static void Warn(Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, EngineTracer.Level.Warn, messageFormat, parameters);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00013558 File Offset: 0x00011758
		public static void Warn(string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Warn, messageFormat, parameters);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00013567 File Offset: 0x00011767
		public static void Warn(string message)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Warn, message);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00013575 File Offset: 0x00011775
		public static void Error(Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, EngineTracer.Level.Error, messageFormat, parameters);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00013585 File Offset: 0x00011785
		public static void Error(string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Error, messageFormat, parameters);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00013594 File Offset: 0x00011794
		public static void Error(string message)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Error, message);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000135A2 File Offset: 0x000117A2
		public static void Fatal(Exception exception, string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(exception, EngineTracer.Level.Fatal, messageFormat, parameters);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000135B2 File Offset: 0x000117B2
		public static void Fatal(string messageFormat, params object[] parameters)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Fatal, messageFormat, parameters);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000135C1 File Offset: 0x000117C1
		public static void Fatal(string message)
		{
			EngineTracer.InternalTrace.Log(EngineTracer.Level.Fatal, message);
		}

		// Token: 0x04000435 RID: 1077
		private static volatile IEngineTracerDestination _traceDestination;

		// Token: 0x04000436 RID: 1078
		private static object _tracerWLock = new object();

		// Token: 0x02000109 RID: 265
		public enum Level
		{
			// Token: 0x04000555 RID: 1365
			Trace,
			// Token: 0x04000556 RID: 1366
			Debug,
			// Token: 0x04000557 RID: 1367
			Info,
			// Token: 0x04000558 RID: 1368
			Warn,
			// Token: 0x04000559 RID: 1369
			Error,
			// Token: 0x0400055A RID: 1370
			Fatal,
			// Token: 0x0400055B RID: 1371
			Off
		}
	}
}
