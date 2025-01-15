using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BE RID: 190
	[LayoutRenderer("environment")]
	[ThreadSafe]
	public class EnvironmentLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0001EE27 File Offset: 0x0001D027
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0001EE2F File Offset: 0x0001D02F
		[RequiredParameter]
		[DefaultParameter]
		public string Variable { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001EE38 File Offset: 0x0001D038
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0001EE40 File Offset: 0x0001D040
		public string Default { get; set; }

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001EE49 File Offset: 0x0001D049
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			SimpleLayout simpleLayout = this.GetSimpleLayout();
			if (simpleLayout == null)
			{
				return;
			}
			simpleLayout.RenderAppendBuilder(logEvent, builder, false);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001EE60 File Offset: 0x0001D060
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			SimpleLayout simpleLayout = this.GetSimpleLayout();
			if (simpleLayout == null)
			{
				return string.Empty;
			}
			if (simpleLayout.IsFixedText || simpleLayout.IsSimpleStringText)
			{
				return simpleLayout.Render(logEvent);
			}
			return null;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001EE98 File Offset: 0x0001D098
		private SimpleLayout GetSimpleLayout()
		{
			if (this.Variable != null)
			{
				string text = EnvironmentHelper.GetSafeEnvironmentVariable(this.Variable);
				if (string.IsNullOrEmpty(text))
				{
					text = this.Default;
				}
				if (!string.IsNullOrEmpty(text))
				{
					KeyValuePair<string, SimpleLayout> cachedValue = this._cachedValue;
					if (string.CompareOrdinal(cachedValue.Key, text) != 0)
					{
						cachedValue = new KeyValuePair<string, SimpleLayout>(text, new SimpleLayout(text));
						this._cachedValue = cachedValue;
					}
					return cachedValue.Value;
				}
			}
			return null;
		}

		// Token: 0x040002F1 RID: 753
		private KeyValuePair<string, SimpleLayout> _cachedValue;
	}
}
