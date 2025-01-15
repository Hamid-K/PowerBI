using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.Win32;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000070 RID: 112
	internal sealed class MonitoredScope : IDisposable
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000AE0C File Offset: 0x0000900C
		private static bool TraceMonitoredScope
		{
			get
			{
				if (MonitoredScope.traceMonitoredScope == null)
				{
					bool flag = false;
					try
					{
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ReportServerTracing\\"))
						{
							if (registryKey != null)
							{
								object value = registryKey.GetValue("TraceMonitoredScope");
								flag = value is int && (int)value == 1;
							}
						}
					}
					catch (Exception)
					{
					}
					MonitoredScope.traceMonitoredScope = new bool?(flag);
				}
				return MonitoredScope.traceMonitoredScope.Value;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000AEA0 File Offset: 0x000090A0
		private MonitoredScope()
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000AEC0 File Offset: 0x000090C0
		internal static MonitoredScope New(string name)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return MonitoredScope.dummyInstance;
			}
			MonitoredScope monitoredScope = MonitoredScope.instance;
			if (monitoredScope == null)
			{
				monitoredScope = new MonitoredScope();
				MonitoredScope.instance = monitoredScope;
			}
			monitoredScope.Start(name);
			return monitoredScope;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000AEF7 File Offset: 0x000090F7
		internal static MonitoredScope NewFormat(string format, object arg0)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return MonitoredScope.dummyInstance;
			}
			return MonitoredScope.New(string.Format(CultureInfo.InvariantCulture, format, arg0));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000AF17 File Offset: 0x00009117
		internal static MonitoredScope NewFormat(string format, object arg0, object arg1)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return MonitoredScope.dummyInstance;
			}
			return MonitoredScope.New(string.Format(CultureInfo.InvariantCulture, format, arg0, arg1));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000AF38 File Offset: 0x00009138
		internal static MonitoredScope NewFormat(string format, object arg0, object arg1, object arg2)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return MonitoredScope.dummyInstance;
			}
			return MonitoredScope.New(string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000AF5A File Offset: 0x0000915A
		internal static MonitoredScope NewConcat(string arg0, object arg1)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return MonitoredScope.dummyInstance;
			}
			if (arg1 != null)
			{
				return MonitoredScope.New(arg0 + ((arg1 != null) ? arg1.ToString() : null));
			}
			return MonitoredScope.New(arg0);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000AF8C File Offset: 0x0000918C
		internal static void End(string name)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return;
			}
			MonitoredScope monitoredScope = MonitoredScope.instance;
			if (!string.Equals(monitoredScope.stack.Peek().Key, name, StringComparison.InvariantCulture))
			{
				throw new Exception("MonitoredScope cannot be ended because the start and end scope names do not match!");
			}
			monitoredScope.Dispose();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000AFD4 File Offset: 0x000091D4
		private void Start(string name)
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return;
			}
			this.stack.Push(new KeyValuePair<string, DateTime>(name, DateTime.UtcNow));
			string text = string.Format(CultureInfo.InvariantCulture, "< {0} {1}", this.indent, name);
			RSTrace.MonitoredScope.Trace(text);
			this.indent += "--";
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B038 File Offset: 0x00009238
		public void Dispose()
		{
			if (!MonitoredScope.TraceMonitoredScope)
			{
				return;
			}
			KeyValuePair<string, DateTime> keyValuePair = this.stack.Pop();
			this.indent = this.indent.Substring(0, this.indent.Length - 2);
			double totalMilliseconds = (DateTime.UtcNow - keyValuePair.Value).TotalMilliseconds;
			string text = string.Format(CultureInfo.InvariantCulture, "> {0} {1} - {2}", this.indent, keyValuePair.Key, totalMilliseconds);
			RSTrace.MonitoredScope.Trace(text);
		}

		// Token: 0x0400018F RID: 399
		private readonly Stack<KeyValuePair<string, DateTime>> stack = new Stack<KeyValuePair<string, DateTime>>();

		// Token: 0x04000190 RID: 400
		private const string IndentationTag = "--";

		// Token: 0x04000191 RID: 401
		private string indent = "--";

		// Token: 0x04000192 RID: 402
		private static bool? traceMonitoredScope = null;

		// Token: 0x04000193 RID: 403
		private static readonly MonitoredScope dummyInstance = new MonitoredScope();

		// Token: 0x04000194 RID: 404
		[ThreadStatic]
		private static MonitoredScope instance;
	}
}
