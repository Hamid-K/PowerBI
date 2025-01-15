using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B7 RID: 183
	[LayoutRenderer("callsite-filename")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class CallSiteFileNameLayoutRenderer : LayoutRenderer, IUsesStackTrace, IStringValueRenderer
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0001E769 File Offset: 0x0001C969
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0001E771 File Offset: 0x0001C971
		[DefaultValue(true)]
		public bool IncludeSourcePath { get; set; } = true;

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0001E77A File Offset: 0x0001C97A
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0001E782 File Offset: 0x0001C982
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0001E78B File Offset: 0x0001C98B
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				return StackTraceUsage.WithSource;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001E78E File Offset: 0x0001C98E
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.GetStringValue(logEvent));
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E79E File Offset: 0x0001C99E
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		private string GetStringValue(LogEventInfo logEvent)
		{
			if (logEvent.CallSiteInformation != null)
			{
				string callerFilePath = logEvent.CallSiteInformation.GetCallerFilePath(this.SkipFrames);
				if (!string.IsNullOrEmpty(callerFilePath))
				{
					if (!this.IncludeSourcePath)
					{
						return Path.GetFileName(callerFilePath);
					}
					return callerFilePath;
				}
			}
			return string.Empty;
		}
	}
}
