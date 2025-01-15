using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Microsoft.Owin.Logging
{
	// Token: 0x0200002B RID: 43
	public class DiagnosticsLoggerFactory : ILoggerFactory
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00004C5C File Offset: 0x00002E5C
		public DiagnosticsLoggerFactory()
		{
			this._rootSourceSwitch = new SourceSwitch("Microsoft.Owin");
			this._rootTraceListener = null;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00004C8B File Offset: 0x00002E8B
		public DiagnosticsLoggerFactory(SourceSwitch rootSourceSwitch, TraceListener rootTraceListener)
		{
			this._rootSourceSwitch = rootSourceSwitch ?? new SourceSwitch("Microsoft.Owin");
			this._rootTraceListener = rootTraceListener;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00004CBF File Offset: 0x00002EBF
		public ILogger Create(string name)
		{
			return new DiagnosticsLogger(this.GetOrAddTraceSource(name));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004CCD File Offset: 0x00002ECD
		private TraceSource GetOrAddTraceSource(string name)
		{
			return this._sources.GetOrAdd(name, new Func<string, TraceSource>(this.InitializeTraceSource));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private TraceSource InitializeTraceSource(string traceSourceName)
		{
			TraceSource traceSource = new TraceSource(traceSourceName);
			if (traceSourceName == "Microsoft.Owin")
			{
				if (DiagnosticsLoggerFactory.HasDefaultSwitch(traceSource))
				{
					traceSource.Switch = this._rootSourceSwitch;
				}
				if (this._rootTraceListener != null)
				{
					traceSource.Listeners.Add(this._rootTraceListener);
				}
			}
			else
			{
				string parentSourceName = DiagnosticsLoggerFactory.ParentSourceName(traceSourceName);
				if (DiagnosticsLoggerFactory.HasDefaultListeners(traceSource))
				{
					TraceSource parentTraceSource = this.GetOrAddTraceSource(parentSourceName);
					traceSource.Listeners.Clear();
					traceSource.Listeners.AddRange(parentTraceSource.Listeners);
				}
				if (DiagnosticsLoggerFactory.HasDefaultSwitch(traceSource))
				{
					TraceSource parentTraceSource2 = this.GetOrAddTraceSource(parentSourceName);
					traceSource.Switch = parentTraceSource2.Switch;
				}
			}
			return traceSource;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004D8C File Offset: 0x00002F8C
		private static string ParentSourceName(string traceSourceName)
		{
			int indexOfLastDot = traceSourceName.LastIndexOf('.');
			if (indexOfLastDot != -1)
			{
				return traceSourceName.Substring(0, indexOfLastDot);
			}
			return "Microsoft.Owin";
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00004DB4 File Offset: 0x00002FB4
		private static bool HasDefaultListeners(TraceSource traceSource)
		{
			return traceSource.Listeners.Count == 1 && traceSource.Listeners[0] is DefaultTraceListener;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00004DDA File Offset: 0x00002FDA
		private static bool HasDefaultSwitch(TraceSource traceSource)
		{
			return string.IsNullOrEmpty(traceSource.Switch.DisplayName) == string.IsNullOrEmpty(traceSource.Name) && traceSource.Switch.Level == SourceLevels.Off;
		}

		// Token: 0x0400005A RID: 90
		private const string RootTraceName = "Microsoft.Owin";

		// Token: 0x0400005B RID: 91
		private readonly SourceSwitch _rootSourceSwitch;

		// Token: 0x0400005C RID: 92
		private readonly TraceListener _rootTraceListener;

		// Token: 0x0400005D RID: 93
		private readonly ConcurrentDictionary<string, TraceSource> _sources = new ConcurrentDictionary<string, TraceSource>(StringComparer.OrdinalIgnoreCase);
	}
}
