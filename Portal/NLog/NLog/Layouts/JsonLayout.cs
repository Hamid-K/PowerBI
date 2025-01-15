using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;
using NLog.MessageTemplates;
using NLog.Targets;

namespace NLog.Layouts
{
	// Token: 0x020000A5 RID: 165
	[Layout("JsonLayout")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class JsonLayout : Layout
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001B43C File Offset: 0x0001963C
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0001B471 File Offset: 0x00019671
		private JsonLayout.LimitRecursionJsonConvert JsonConverter
		{
			get
			{
				JsonLayout.LimitRecursionJsonConvert limitRecursionJsonConvert;
				if ((limitRecursionJsonConvert = this._jsonConverter) == null)
				{
					limitRecursionJsonConvert = (this._jsonConverter = new JsonLayout.LimitRecursionJsonConvert(this.MaxRecursionLimit, ConfigurationItemFactory.Default.JsonConverter));
				}
				return limitRecursionJsonConvert;
			}
			set
			{
				this._jsonConverter = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0001B47C File Offset: 0x0001967C
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0001B4A6 File Offset: 0x000196A6
		private IValueFormatter ValueFormatter
		{
			get
			{
				IValueFormatter valueFormatter;
				if ((valueFormatter = this._valueFormatter) == null)
				{
					valueFormatter = (this._valueFormatter = ConfigurationItemFactory.Default.ValueFormatter);
				}
				return valueFormatter;
			}
			set
			{
				this._valueFormatter = value;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001B4AF File Offset: 0x000196AF
		public JsonLayout()
		{
			this.Attributes = new List<JsonAttribute>();
			this.RenderEmptyObject = true;
			this.IncludeAllProperties = false;
			this.ExcludeProperties = new HashSet<string>();
			this.MaxRecursionLimit = 0;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0001B4E2 File Offset: 0x000196E2
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0001B4EA File Offset: 0x000196EA
		[ArrayParameter(typeof(JsonAttribute), "attribute")]
		public IList<JsonAttribute> Attributes { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0001B4F3 File Offset: 0x000196F3
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0001B4FB File Offset: 0x000196FB
		public bool SuppressSpaces { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001B504 File Offset: 0x00019704
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0001B50C File Offset: 0x0001970C
		public bool RenderEmptyObject { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001B515 File Offset: 0x00019715
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0001B51D File Offset: 0x0001971D
		public bool IncludeGdc { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0001B526 File Offset: 0x00019726
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0001B52E File Offset: 0x0001972E
		public bool IncludeMdc { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001B537 File Offset: 0x00019737
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0001B53F File Offset: 0x0001973F
		public bool IncludeMdlc { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001B548 File Offset: 0x00019748
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0001B550 File Offset: 0x00019750
		public bool IncludeAllProperties { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0001B559 File Offset: 0x00019759
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0001B561 File Offset: 0x00019761
		public ISet<string> ExcludeProperties { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0001B56A File Offset: 0x0001976A
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0001B572 File Offset: 0x00019772
		public int MaxRecursionLimit { get; set; }

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001B57B File Offset: 0x0001977B
		protected override void InitializeLayout()
		{
			base.InitializeLayout();
			if (this.IncludeMdc)
			{
				base.ThreadAgnostic = false;
			}
			if (this.IncludeMdlc)
			{
				base.ThreadAgnostic = false;
			}
			if (this.IncludeAllProperties)
			{
				base.MutableUnsafe = true;
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001B5B0 File Offset: 0x000197B0
		protected override void CloseLayout()
		{
			this.JsonConverter = null;
			this.ValueFormatter = null;
			base.CloseLayout();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001B5C6 File Offset: 0x000197C6
		internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			base.PrecalculateBuilderInternal(logEvent, target);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001B5D0 File Offset: 0x000197D0
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			int length = target.Length;
			this.RenderJsonFormattedMessage(logEvent, target);
			if (target.Length == length && this.RenderEmptyObject)
			{
				target.Append(this.SuppressSpaces ? "{}" : "{  }");
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001B618 File Offset: 0x00019818
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return base.RenderAllocateBuilder(logEvent, null);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001B624 File Offset: 0x00019824
		private void RenderJsonFormattedMessage(LogEventInfo logEvent, StringBuilder sb)
		{
			int length = sb.Length;
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				JsonAttribute jsonAttribute = this.Attributes[i];
				int length2 = sb.Length;
				if (!this.RenderAppendJsonPropertyValue(jsonAttribute, logEvent, sb, sb.Length == length))
				{
					sb.Length = length2;
				}
			}
			if (this.IncludeGdc)
			{
				foreach (string text in GlobalDiagnosticsContext.GetNames())
				{
					if (!string.IsNullOrEmpty(text))
					{
						object @object = GlobalDiagnosticsContext.GetObject(text);
						this.AppendJsonPropertyValue(text, @object, null, null, CaptureType.Unknown, sb, sb.Length == length);
					}
				}
			}
			if (this.IncludeMdc)
			{
				foreach (string text2 in MappedDiagnosticsContext.GetNames())
				{
					if (!string.IsNullOrEmpty(text2))
					{
						object object2 = MappedDiagnosticsContext.GetObject(text2);
						this.AppendJsonPropertyValue(text2, object2, null, null, CaptureType.Unknown, sb, sb.Length == length);
					}
				}
			}
			if (this.IncludeMdlc)
			{
				foreach (string text3 in MappedDiagnosticsLogicalContext.GetNames())
				{
					if (!string.IsNullOrEmpty(text3))
					{
						object object3 = MappedDiagnosticsLogicalContext.GetObject(text3);
						this.AppendJsonPropertyValue(text3, object3, null, null, CaptureType.Unknown, sb, sb.Length == length);
					}
				}
			}
			if (this.IncludeAllProperties && logEvent.HasProperties)
			{
				foreach (MessageTemplateParameter messageTemplateParameter in ((IEnumerable<MessageTemplateParameter>)logEvent.CreateOrUpdatePropertiesInternal(true, null)))
				{
					if (!string.IsNullOrEmpty(messageTemplateParameter.Name) && !this.ExcludeProperties.Contains(messageTemplateParameter.Name))
					{
						this.AppendJsonPropertyValue(messageTemplateParameter.Name, messageTemplateParameter.Value, messageTemplateParameter.Format, logEvent.FormatProvider, messageTemplateParameter.CaptureType, sb, sb.Length == length);
					}
				}
			}
			if (sb.Length > length)
			{
				this.CompleteJsonMessage(sb);
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001B87C File Offset: 0x00019A7C
		private void BeginJsonProperty(StringBuilder sb, string propName, bool beginJsonMessage)
		{
			if (beginJsonMessage)
			{
				sb.Append(this.SuppressSpaces ? "{" : "{ ");
			}
			else
			{
				sb.Append(',');
				if (!this.SuppressSpaces)
				{
					sb.Append(' ');
				}
			}
			sb.Append('"');
			sb.Append(propName);
			sb.Append('"');
			sb.Append(':');
			if (!this.SuppressSpaces)
			{
				sb.Append(' ');
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001B8F7 File Offset: 0x00019AF7
		private void CompleteJsonMessage(StringBuilder sb)
		{
			sb.Append(this.SuppressSpaces ? "}" : " }");
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001B914 File Offset: 0x00019B14
		private void AppendJsonPropertyValue(string propName, object propertyValue, string format, IFormatProvider formatProvider, CaptureType captureType, StringBuilder sb, bool beginJsonMessage)
		{
			this.BeginJsonProperty(sb, propName, beginJsonMessage);
			if (this.MaxRecursionLimit <= 1 && captureType == CaptureType.Serialize)
			{
				this.JsonConverter.SerializeObjectNoLimit(propertyValue, sb);
				return;
			}
			if (captureType == CaptureType.Stringify)
			{
				int length = sb.Length;
				this.ValueFormatter.FormatValue(propertyValue, format, captureType, formatProvider, sb);
				JsonLayout.PerformJsonEscapeIfNeeded(sb, length);
				return;
			}
			this.JsonConverter.SerializeObject(propertyValue, sb);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001B984 File Offset: 0x00019B84
		private static void PerformJsonEscapeIfNeeded(StringBuilder sb, int valueStart)
		{
			if (sb.Length - valueStart <= 2)
			{
				return;
			}
			for (int i = valueStart + 1; i < sb.Length - 1; i++)
			{
				if (DefaultJsonSerializer.RequiresJsonEscape(sb[i], false))
				{
					string text = sb.ToString(valueStart + 1, sb.Length - valueStart - 2);
					sb.Length = valueStart;
					sb.Append('"');
					DefaultJsonSerializer.AppendStringEscape(sb, text, false);
					sb.Append('"');
					return;
				}
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001B9F8 File Offset: 0x00019BF8
		private bool RenderAppendJsonPropertyValue(JsonAttribute attrib, LogEventInfo logEvent, StringBuilder sb, bool beginJsonMessage)
		{
			this.BeginJsonProperty(sb, attrib.Name, beginJsonMessage);
			if (attrib.Encode)
			{
				sb.Append('"');
			}
			int length = sb.Length;
			attrib.LayoutWrapper.RenderAppendBuilder(logEvent, sb);
			if (!attrib.IncludeEmptyValue && length == sb.Length)
			{
				return false;
			}
			if (attrib.Encode)
			{
				sb.Append('"');
			}
			return true;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001BA5E File Offset: 0x00019C5E
		public override string ToString()
		{
			return base.ToStringWithNestedItems<JsonAttribute>(this.Attributes, delegate(JsonAttribute a)
			{
				string name = a.Name;
				string text = "-";
				Layout layout = a.Layout;
				return name + text + ((layout != null) ? layout.ToString() : null);
			});
		}

		// Token: 0x0400027E RID: 638
		private JsonLayout.LimitRecursionJsonConvert _jsonConverter;

		// Token: 0x0400027F RID: 639
		private IValueFormatter _valueFormatter;

		// Token: 0x02000253 RID: 595
		private class LimitRecursionJsonConvert : IJsonConverter
		{
			// Token: 0x060015D1 RID: 5585 RVA: 0x00039B3E File Offset: 0x00037D3E
			public LimitRecursionJsonConvert(int maxRecursionLimit, IJsonConverter converter)
			{
				this._converter = converter;
				this._serializer = converter as DefaultJsonSerializer;
				this._serializerOptions = new JsonSerializeOptions
				{
					MaxRecursionLimit = Math.Max(0, maxRecursionLimit)
				};
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x00039B71 File Offset: 0x00037D71
			public bool SerializeObject(object value, StringBuilder builder)
			{
				if (this._serializer != null)
				{
					return this._serializer.SerializeObject(value, builder, this._serializerOptions);
				}
				return this._converter.SerializeObject(value, builder);
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x00039B9C File Offset: 0x00037D9C
			public void SerializeObjectNoLimit(object value, StringBuilder builder)
			{
				this._converter.SerializeObject(value, builder);
			}

			// Token: 0x0400066D RID: 1645
			private readonly IJsonConverter _converter;

			// Token: 0x0400066E RID: 1646
			private readonly DefaultJsonSerializer _serializer;

			// Token: 0x0400066F RID: 1647
			private readonly JsonSerializeOptions _serializerOptions;
		}
	}
}
