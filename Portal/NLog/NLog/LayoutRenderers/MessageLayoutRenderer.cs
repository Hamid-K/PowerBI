using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D7 RID: 215
	[LayoutRenderer("message")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class MessageLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x000210D0 File Offset: 0x0001F2D0
		public MessageLayoutRenderer()
		{
			this.ExceptionSeparator = EnvironmentHelper.NewLine;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000210E3 File Offset: 0x0001F2E3
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x000210EB File Offset: 0x0001F2EB
		public bool WithException { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000210F4 File Offset: 0x0001F2F4
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x000210FC File Offset: 0x0001F2FC
		public string ExceptionSeparator { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00021105 File Offset: 0x0001F305
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0002110D File Offset: 0x0001F30D
		public bool Raw { get; set; }

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00021118 File Offset: 0x0001F318
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			bool flag;
			if (logEvent.Exception != null && this.WithException)
			{
				object[] parameters = logEvent.Parameters;
				if (parameters != null && parameters.Length == 1 && logEvent.Parameters[0] == logEvent.Exception)
				{
					flag = logEvent.Message == "{0}";
					goto IL_0047;
				}
			}
			flag = false;
			IL_0047:
			bool flag2 = flag;
			if (this.Raw)
			{
				builder.Append(logEvent.Message);
			}
			else if (!flag2)
			{
				if (logEvent.MessageFormatter == LogMessageTemplateFormatter.DefaultAutoSingleTarget.MessageFormatter)
				{
					logEvent.AppendFormattedMessage(LogMessageTemplateFormatter.DefaultAutoSingleTarget, builder);
				}
				else
				{
					builder.Append(logEvent.FormattedMessage);
				}
			}
			if (this.WithException && logEvent.Exception != null)
			{
				Exception ex = logEvent.Exception;
				AggregateException ex2;
				if ((ex2 = logEvent.Exception as AggregateException) != null)
				{
					ex2 = ex2.Flatten();
					ex = ((ex2.InnerExceptions.Count == 1) ? ex2.InnerExceptions[0] : ex2);
				}
				if (!flag2)
				{
					builder.Append(this.ExceptionSeparator);
				}
				builder.Append(ex.ToString());
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002121C File Offset: 0x0001F41C
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			if (this.WithException)
			{
				return null;
			}
			return (this.Raw ? logEvent.Message : logEvent.FormattedMessage) ?? string.Empty;
		}
	}
}
