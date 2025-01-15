using System;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C1 RID: 193
	[LayoutRenderer("event-properties")]
	[ThreadAgnostic]
	[ThreadSafe]
	[MutableUnsafe]
	public class EventPropertiesLayoutRenderer : LayoutRenderer, IRawValue, IStringValueRenderer
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001F0FD File Offset: 0x0001D2FD
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0001F105 File Offset: 0x0001D305
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001F10E File Offset: 0x0001D30E
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0001F116 File Offset: 0x0001D316
		public string Format { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0001F11F File Offset: 0x0001D31F
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x0001F127 File Offset: 0x0001D327
		public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0001F130 File Offset: 0x0001D330
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x0001F157 File Offset: 0x0001D357
		public string ObjectPath
		{
			get
			{
				string[] objectPropertyPath = this._objectPropertyPath;
				if (objectPropertyPath == null || objectPropertyPath.Length == 0)
				{
					return null;
				}
				return string.Join(".", this._objectPropertyPath);
			}
			set
			{
				this._objectPropertyPath = (StringHelpers.IsNullOrWhiteSpace(value) ? null : value.SplitAndTrimTokens('.'));
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0001F174 File Offset: 0x0001D374
		private ObjectReflectionCache ObjectReflectionCache
		{
			get
			{
				ObjectReflectionCache objectReflectionCache;
				if ((objectReflectionCache = this._objectReflectionCache) == null)
				{
					objectReflectionCache = (this._objectReflectionCache = new ObjectReflectionCache());
				}
				return objectReflectionCache;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0001F19C File Offset: 0x0001D39C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object obj;
			if (this.TryGetValue(logEvent, out obj))
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, this.Culture);
				builder.AppendFormattedValue(obj, this.Format, formatProvider);
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001F1D0 File Offset: 0x0001D3D0
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			this.TryGetValue(logEvent, out value);
			return true;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0001F1E5 File Offset: 0x0001D3E5
		private bool TryGetValue(LogEventInfo logEvent, out object value)
		{
			value = null;
			return logEvent.HasProperties && logEvent.Properties.TryGetValue(this.Item, out value) && (this._objectPropertyPath == null || this.TryGetObjectProperty(ref value));
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0001F220 File Offset: 0x0001D420
		private bool TryGetObjectProperty(ref object value)
		{
			ObjectReflectionCache objectReflectionCache = this.ObjectReflectionCache;
			for (int i = 0; i < this._objectPropertyPath.Length; i++)
			{
				if (value == null)
				{
					return false;
				}
				ObjectReflectionCache.ObjectPropertyList.PropertyValue propertyValue;
				if (!objectReflectionCache.LookupObjectProperties(value).TryGetPropertyValue(this._objectPropertyPath[i], out propertyValue))
				{
					return false;
				}
				value = propertyValue.Value;
			}
			return true;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0001F278 File Offset: 0x0001D478
		private string GetStringValue(LogEventInfo logEvent)
		{
			if (!(this.Format != "@"))
			{
				return null;
			}
			object obj;
			if (this.TryGetValue(logEvent, out obj))
			{
				return FormatHelper.TryFormatToString(obj, this.Format, base.GetFormatProvider(logEvent, this.Culture));
			}
			return string.Empty;
		}

		// Token: 0x040002FA RID: 762
		private string[] _objectPropertyPath;

		// Token: 0x040002FB RID: 763
		private ObjectReflectionCache _objectReflectionCache;
	}
}
