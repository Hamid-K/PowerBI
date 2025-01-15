using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NLog.MessageTemplates;

namespace NLog.Internal
{
	// Token: 0x02000128 RID: 296
	internal sealed class LogMessageTemplateFormatter : ILogMessageFormatter
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x00024E2C File Offset: 0x0002302C
		private LogMessageTemplateFormatter(bool forceTemplateRenderer, bool singleTargetOnly)
		{
			this._forceTemplateRenderer = forceTemplateRenderer;
			this._singleTargetOnly = singleTargetOnly;
			this.MessageFormatter = new LogMessageFormatter(this.FormatMessage);
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00024E55 File Offset: 0x00023055
		public LogMessageFormatter MessageFormatter { get; }

		// Token: 0x06000EED RID: 3821 RVA: 0x00024E60 File Offset: 0x00023060
		public bool HasProperties(LogEventInfo logEvent)
		{
			if (!this.HasParameters(logEvent))
			{
				return false;
			}
			if (this._singleTargetOnly)
			{
				TemplateEnumerator templateEnumerator = new TemplateEnumerator(logEvent.Message);
				if (templateEnumerator.MoveNext())
				{
					LiteralHole literalHole = templateEnumerator.Current;
					if (literalHole.MaybePositionalTemplate)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00024EAA File Offset: 0x000230AA
		private bool HasParameters(LogEventInfo logEvent)
		{
			return logEvent.Parameters != null && !string.IsNullOrEmpty(logEvent.Message) && logEvent.Parameters.Length != 0;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00024ED0 File Offset: 0x000230D0
		public void AppendFormattedMessage(LogEventInfo logEvent, StringBuilder builder)
		{
			if (!this.HasParameters(logEvent))
			{
				builder.Append(logEvent.Message ?? string.Empty);
				return;
			}
			IList<MessageTemplateParameter> list;
			logEvent.Message.Render(logEvent.FormatProvider ?? CultureInfo.CurrentCulture, logEvent.Parameters, this._forceTemplateRenderer, builder, out list);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00024F28 File Offset: 0x00023128
		public string FormatMessage(LogEventInfo logEvent)
		{
			if (!this.HasParameters(logEvent))
			{
				return logEvent.Message;
			}
			string text;
			using (StringBuilderPool.ItemHolder itemHolder = LogMessageTemplateFormatter._builderPool.Acquire())
			{
				this.AppendToBuilder(logEvent, itemHolder.Item);
				text = itemHolder.Item.ToString();
			}
			return text;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00024F8C File Offset: 0x0002318C
		private void AppendToBuilder(LogEventInfo logEvent, StringBuilder builder)
		{
			IList<MessageTemplateParameter> list;
			logEvent.Message.Render(logEvent.FormatProvider ?? CultureInfo.CurrentCulture, logEvent.Parameters, this._forceTemplateRenderer, builder, out list);
			logEvent.CreateOrUpdatePropertiesInternal(false, list ?? ArrayHelper.Empty<MessageTemplateParameter>());
		}

		// Token: 0x040003FB RID: 1019
		public static readonly LogMessageTemplateFormatter DefaultAuto = new LogMessageTemplateFormatter(false, false);

		// Token: 0x040003FC RID: 1020
		public static readonly LogMessageTemplateFormatter Default = new LogMessageTemplateFormatter(true, false);

		// Token: 0x040003FD RID: 1021
		public static readonly LogMessageTemplateFormatter DefaultAutoSingleTarget = new LogMessageTemplateFormatter(false, true);

		// Token: 0x040003FE RID: 1022
		private static readonly StringBuilderPool _builderPool = new StringBuilderPool(Environment.ProcessorCount * 2, 1024, 524288);

		// Token: 0x040003FF RID: 1023
		private readonly bool _forceTemplateRenderer;

		// Token: 0x04000400 RID: 1024
		private readonly bool _singleTargetOnly;
	}
}
