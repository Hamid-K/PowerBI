using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.HtmlUtils;
using Microsoft.Mashup.WebBrowserContents;

namespace Microsoft.Mashup.Engine1.Library.WebBrowserContents
{
	// Token: 0x0200204B RID: 8267
	internal class WebBrowserContentsOptions
	{
		// Token: 0x0601136F RID: 70511 RVA: 0x003B4188 File Offset: 0x003B2388
		public WebBrowserContentsOptions(string dataSourceName, OptionsRecord options)
		{
			this.options = options;
			Value value;
			if (this.options.TryGetValue("ApiKeyName", out value))
			{
				this.apiKeyName = value.AsText.AsString;
			}
			Value value2;
			if (this.options.TryGetValue("WaitFor", out value2) && !value2.IsNull)
			{
				OptionsRecord optionsRecord = WebBrowserContentsOptions.WaitForOptionRecord.CreateOptions(dataSourceName, value2);
				Value value3;
				if (optionsRecord.TryGetValue("Selector", out value3) && !value3.IsNull)
				{
					this.waitForSelector = value3.AsText.AsString;
					if (!AngleSharpUtils.IsValidSelector(this.waitForSelector))
					{
						throw ValueException.NewDataFormatError<Message0>(Resources.InvalidSelector, TextValue.New(this.waitForSelector), null);
					}
				}
				Value value4;
				if (optionsRecord.TryGetValue("Timeout", out value4) && !value4.IsNull)
				{
					this.waitForTimeout = new TimeSpan?(value4.AsDuration.AsTimeSpan);
				}
			}
		}

		// Token: 0x17002DF7 RID: 11767
		// (get) Token: 0x06011370 RID: 70512 RVA: 0x003B426D File Offset: 0x003B246D
		public string ApiKeyName
		{
			get
			{
				return this.apiKeyName;
			}
		}

		// Token: 0x17002DF8 RID: 11768
		// (get) Token: 0x06011371 RID: 70513 RVA: 0x003B4275 File Offset: 0x003B2475
		public string WaitForSelector
		{
			get
			{
				return this.waitForSelector;
			}
		}

		// Token: 0x17002DF9 RID: 11769
		// (get) Token: 0x06011372 RID: 70514 RVA: 0x003B427D File Offset: 0x003B247D
		public TimeSpan? WaitForTimeout
		{
			get
			{
				return this.waitForTimeout;
			}
		}

		// Token: 0x0400686B RID: 26731
		public static readonly OptionRecordDefinition WaitForOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Selector", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("Timeout", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, null)
		});

		// Token: 0x0400686C RID: 26732
		private readonly OptionsRecord options;

		// Token: 0x0400686D RID: 26733
		private readonly string apiKeyName;

		// Token: 0x0400686E RID: 26734
		private readonly string waitForSelector;

		// Token: 0x0400686F RID: 26735
		private readonly TimeSpan? waitForTimeout;
	}
}
