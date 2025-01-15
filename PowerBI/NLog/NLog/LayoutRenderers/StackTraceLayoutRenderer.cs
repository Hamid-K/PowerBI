using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E9 RID: 233
	[LayoutRenderer("stacktrace")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class StackTraceLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x000222DA File Offset: 0x000204DA
		public StackTraceLayoutRenderer()
		{
			this.Separator = " => ";
			this.TopFrames = 3;
			this.Format = StackTraceFormat.Flat;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000222FB File Offset: 0x000204FB
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00022303 File Offset: 0x00020503
		[DefaultValue("Flat")]
		public StackTraceFormat Format { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002230C File Offset: 0x0002050C
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00022314 File Offset: 0x00020514
		[DefaultValue(3)]
		public int TopFrames { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0002231D File Offset: 0x0002051D
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00022325 File Offset: 0x00020525
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0002232E File Offset: 0x0002052E
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00022336 File Offset: 0x00020536
		[DefaultValue(" => ")]
		public string Separator { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0002233F File Offset: 0x0002053F
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.Format != StackTraceFormat.Raw)
				{
					return StackTraceUsage.WithoutSource;
				}
				return StackTraceUsage.WithSource;
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002234C File Offset: 0x0002054C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (logEvent.StackTrace == null)
			{
				return;
			}
			int num = logEvent.UserStackFrameNumber + this.TopFrames - 1;
			if (num >= logEvent.StackTrace.GetFrameCount())
			{
				num = logEvent.StackTrace.GetFrameCount() - 1;
			}
			int num2 = logEvent.UserStackFrameNumber + this.SkipFrames;
			switch (this.Format)
			{
			case StackTraceFormat.Raw:
				StackTraceLayoutRenderer.AppendRaw(builder, logEvent, num, num2);
				return;
			case StackTraceFormat.Flat:
				this.AppendFlat(builder, logEvent, num, num2);
				return;
			case StackTraceFormat.DetailedFlat:
				this.AppendDetailedFlat(builder, logEvent, num, num2);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000223D8 File Offset: 0x000205D8
		private static void AppendRaw(StringBuilder builder, LogEventInfo logEvent, int startingFrame, int endingFrame)
		{
			for (int i = startingFrame; i >= endingFrame; i--)
			{
				StackFrame frame = logEvent.StackTrace.GetFrame(i);
				builder.Append(frame.ToString());
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002240C File Offset: 0x0002060C
		private void AppendFlat(StringBuilder builder, LogEventInfo logEvent, int startingFrame, int endingFrame)
		{
			bool flag = true;
			for (int i = startingFrame; i >= endingFrame; i--)
			{
				StackFrame frame = logEvent.StackTrace.GetFrame(i);
				if (!flag)
				{
					builder.Append(this.Separator);
				}
				MethodBase method = frame.GetMethod();
				if (!(method == null))
				{
					Type declaringType = method.DeclaringType;
					if (declaringType != null)
					{
						builder.Append(declaringType.Name);
					}
					else
					{
						builder.Append("<no type>");
					}
					builder.Append(".");
					builder.Append(method.Name);
					flag = false;
				}
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002249C File Offset: 0x0002069C
		private void AppendDetailedFlat(StringBuilder builder, LogEventInfo logEvent, int startingFrame, int endingFrame)
		{
			bool flag = true;
			for (int i = startingFrame; i >= endingFrame; i--)
			{
				MethodBase method = logEvent.StackTrace.GetFrame(i).GetMethod();
				if (!(method == null))
				{
					if (!flag)
					{
						builder.Append(this.Separator);
					}
					builder.Append("[");
					builder.Append(method);
					builder.Append("]");
					flag = false;
				}
			}
		}
	}
}
