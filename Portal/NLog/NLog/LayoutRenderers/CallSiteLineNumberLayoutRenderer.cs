using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B9 RID: 185
	[LayoutRenderer("callsite-linenumber")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class CallSiteLineNumberLayoutRenderer : LayoutRenderer, IUsesStackTrace, IRawValue
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001EA23 File Offset: 0x0001CC23
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x0001EA2B File Offset: 0x0001CC2B
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0001EA34 File Offset: 0x0001CC34
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				return StackTraceUsage.WithSource;
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001EA38 File Offset: 0x0001CC38
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			int? lineNumber = this.GetLineNumber(logEvent);
			if (lineNumber != null)
			{
				builder.AppendInvariant(lineNumber.Value);
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001EA63 File Offset: 0x0001CC63
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetLineNumber(logEvent);
			return true;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001EA74 File Offset: 0x0001CC74
		private int? GetLineNumber(LogEventInfo logEvent)
		{
			if (logEvent.CallSiteInformation == null)
			{
				return null;
			}
			return new int?(logEvent.CallSiteInformation.GetCallerLineNumber(this.SkipFrames));
		}
	}
}
