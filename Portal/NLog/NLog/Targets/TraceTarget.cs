using System;
using System.ComponentModel;
using System.Diagnostics;

namespace NLog.Targets
{
	// Token: 0x02000059 RID: 89
	[Target("Trace")]
	public sealed class TraceTarget : TargetWithLayout
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00014B9A File Offset: 0x00012D9A
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00014BA2 File Offset: 0x00012DA2
		[DefaultValue(false)]
		public bool RawWrite { get; set; }

		// Token: 0x0600081E RID: 2078 RVA: 0x00014BAB File Offset: 0x00012DAB
		public TraceTarget()
		{
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00014BBA File Offset: 0x00012DBA
		public TraceTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00014BCC File Offset: 0x00012DCC
		protected override void Write(LogEventInfo logEvent)
		{
			string text = base.RenderLogEvent(this.Layout, logEvent);
			if (this.RawWrite || logEvent.Level <= LogLevel.Debug)
			{
				Trace.WriteLine(text);
				return;
			}
			if (logEvent.Level == LogLevel.Info)
			{
				Trace.TraceInformation(text);
				return;
			}
			if (logEvent.Level == LogLevel.Warn)
			{
				Trace.TraceWarning(text);
				return;
			}
			if (logEvent.Level == LogLevel.Error)
			{
				Trace.TraceError(text);
				return;
			}
			if (logEvent.Level >= LogLevel.Fatal)
			{
				Trace.Fail(text);
				return;
			}
			Trace.WriteLine(text);
		}
	}
}
