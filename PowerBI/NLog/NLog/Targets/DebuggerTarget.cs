using System;
using System.Diagnostics;
using System.Text;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000035 RID: 53
	[Target("Debugger")]
	public sealed class DebuggerTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0000C860 File Offset: 0x0000AA60
		public DebuggerTarget()
		{
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000C86F File Offset: 0x0000AA6F
		public DebuggerTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000C880 File Offset: 0x0000AA80
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (!Debugger.IsLogging())
			{
				InternalLogger.Debug<string>("Debugger(Name={0}): System.Diagnostics.Debugger.IsLogging()==false. Output has been disabled.", base.Name);
			}
			if (base.Header != null)
			{
				Debugger.Log(LogLevel.Off.Ordinal, string.Empty, base.RenderLogEvent(base.Header, LogEventInfo.CreateNullEvent()) + "\n");
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		protected override void CloseTarget()
		{
			if (base.Footer != null)
			{
				Debugger.Log(LogLevel.Off.Ordinal, string.Empty, base.RenderLogEvent(base.Footer, LogEventInfo.CreateNullEvent()) + "\n");
			}
			base.CloseTarget();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000C920 File Offset: 0x0000AB20
		protected override void Write(LogEventInfo logEvent)
		{
			if (Debugger.IsLogging())
			{
				string text;
				if (base.OptimizeBufferReuse)
				{
					using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this.ReusableLayoutBuilder.Allocate())
					{
						this.Layout.RenderAppendBuilder(logEvent, lockOject.Result, false);
						lockOject.Result.Append('\n');
						text = lockOject.Result.ToString();
						goto IL_0073;
					}
				}
				text = base.RenderLogEvent(this.Layout, logEvent) + "\n";
				IL_0073:
				Debugger.Log(logEvent.Level.Ordinal, logEvent.LoggerName, text);
			}
		}
	}
}
