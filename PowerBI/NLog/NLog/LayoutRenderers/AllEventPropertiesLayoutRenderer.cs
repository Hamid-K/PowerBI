using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B0 RID: 176
	[LayoutRenderer("all-event-properties")]
	[ThreadAgnostic]
	[ThreadSafe]
	[MutableUnsafe]
	public class AllEventPropertiesLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x0001DEE1 File Offset: 0x0001C0E1
		public AllEventPropertiesLayoutRenderer()
		{
			this.Separator = ", ";
			this.Format = "[key]=[value]";
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001DEFF File Offset: 0x0001C0FF
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0001DF07 File Offset: 0x0001C107
		public string Separator { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001DF10 File Offset: 0x0001C110
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0001DF18 File Offset: 0x0001C118
		[DefaultValue(false)]
		public bool IncludeEmptyValues { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001DF21 File Offset: 0x0001C121
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0001DF29 File Offset: 0x0001C129
		[DefaultValue(false)]
		public bool IncludeCallerInformation { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001DF32 File Offset: 0x0001C132
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public string Format
		{
			get
			{
				return this._format;
			}
			set
			{
				if (!value.Contains("[key]"))
				{
					throw new ArgumentException("Invalid format: [key] placeholder is missing.");
				}
				if (!value.Contains("[value]"))
				{
					throw new ArgumentException("Invalid format: [value] placeholder is missing.");
				}
				this._format = value;
				string[] array = this._format.Split(new string[] { "[key]", "[value]" }, StringSplitOptions.None);
				if (array.Length == 3)
				{
					this._beforeKey = array[0];
					this._afterKey = array[1];
					this._afterValue = array[2];
					return;
				}
				this._beforeKey = null;
				this._afterKey = null;
				this._afterValue = null;
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (logEvent.HasProperties)
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				bool flag = true;
				IEnumerable<KeyValuePair<object, object>> enumerable = this.GetProperties(logEvent);
				if (!this.IncludeEmptyValues)
				{
					enumerable = enumerable.Where((KeyValuePair<object, object> p) => !AllEventPropertiesLayoutRenderer.IsEmptyPropertyValue(p.Value));
				}
				foreach (KeyValuePair<object, object> keyValuePair in enumerable)
				{
					if (!flag)
					{
						builder.Append(this.Separator);
					}
					flag = false;
					if (this._beforeKey == null || this._afterKey == null || this._afterValue == null)
					{
						string text = Convert.ToString(keyValuePair.Key, formatProvider);
						string text2 = Convert.ToString(keyValuePair.Value, formatProvider);
						string text3 = this.Format.Replace("[key]", text).Replace("[value]", text2);
						builder.Append(text3);
					}
					else
					{
						builder.Append(this._beforeKey);
						builder.AppendFormattedValue(keyValuePair.Key, null, formatProvider);
						builder.Append(this._afterKey);
						builder.AppendFormattedValue(keyValuePair.Value, null, formatProvider);
						builder.Append(this._afterValue);
					}
				}
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001E128 File Offset: 0x0001C328
		private static bool IsEmptyPropertyValue(object value)
		{
			string text;
			if ((text = value as string) != null)
			{
				return string.IsNullOrEmpty(text);
			}
			return value == null;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001E14C File Offset: 0x0001C34C
		private IDictionary<object, object> GetProperties(LogEventInfo logEvent)
		{
			IDictionary<object, object> properties = logEvent.Properties;
			if (this.IncludeCallerInformation)
			{
				return properties;
			}
			if (logEvent.CallSiteInformation != null)
			{
				foreach (string text in AllEventPropertiesLayoutRenderer.CallerInformationAttributeNames)
				{
					if (properties.ContainsKey(text))
					{
						return properties.Where((KeyValuePair<object, object> p) => !AllEventPropertiesLayoutRenderer.CallerInformationAttributeNames.Contains(p.Key)).ToDictionary((KeyValuePair<object, object> p) => p.Key, (KeyValuePair<object, object> p) => p.Value);
					}
				}
				return properties;
			}
			return properties;
		}

		// Token: 0x040002B8 RID: 696
		private string _format;

		// Token: 0x040002B9 RID: 697
		private string _beforeKey;

		// Token: 0x040002BA RID: 698
		private string _afterKey;

		// Token: 0x040002BB RID: 699
		private string _afterValue;

		// Token: 0x040002BF RID: 703
		private static List<string> CallerInformationAttributeNames = new List<string> { "CallerMemberName", "CallerFilePath", "CallerLineNumber" };
	}
}
