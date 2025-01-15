using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C8 RID: 200
	[LayoutRenderer("guid")]
	[ThreadSafe]
	[ThreadAgnostic]
	public class GuidLayoutRenderer : LayoutRenderer, IRawValue, IStringValueRenderer
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0001FC4B File Offset: 0x0001DE4B
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0001FC53 File Offset: 0x0001DE53
		[DefaultValue("N")]
		public string Format { get; set; } = "N";

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0001FC5C File Offset: 0x0001DE5C
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0001FC64 File Offset: 0x0001DE64
		[DefaultValue(false)]
		public bool GeneratedFromLogEvent { get; set; }

		// Token: 0x06000C61 RID: 3169 RVA: 0x0001FC6D File Offset: 0x0001DE6D
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.GetStringValue(logEvent));
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0001FC7D File Offset: 0x0001DE7D
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetValue(logEvent);
			return true;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0001FC8E File Offset: 0x0001DE8E
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001FC98 File Offset: 0x0001DE98
		private string GetStringValue(LogEventInfo logEvent)
		{
			return this.GetValue(logEvent).ToString(this.Format);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0001FCBC File Offset: 0x0001DEBC
		private Guid GetValue(LogEventInfo logEvent)
		{
			Guid guid;
			if (this.GeneratedFromLogEvent)
			{
				int hashCode = logEvent.GetHashCode();
				short num = (short)((hashCode >> 16) & 65535);
				short num2 = (short)(hashCode & 65535);
				long ticks = LogEventInfo.ZeroDate.Ticks;
				byte b = (byte)((ticks >> 56) & 255L);
				byte b2 = (byte)((ticks >> 48) & 255L);
				byte b3 = (byte)((ticks >> 40) & 255L);
				byte b4 = (byte)((ticks >> 32) & 255L);
				byte b5 = (byte)((ticks >> 24) & 255L);
				byte b6 = (byte)((ticks >> 16) & 255L);
				byte b7 = (byte)((ticks >> 8) & 255L);
				byte b8 = (byte)(ticks & 255L);
				guid = new Guid(logEvent.SequenceID, num, num2, b, b2, b3, b4, b5, b6, b7, b8);
			}
			else
			{
				guid = Guid.NewGuid();
			}
			return guid;
		}
	}
}
