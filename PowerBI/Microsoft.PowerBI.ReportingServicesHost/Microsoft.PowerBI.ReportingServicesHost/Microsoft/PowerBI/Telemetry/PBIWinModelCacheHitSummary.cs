using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000022 RID: 34
	public class PBIWinModelCacheHitSummary : BaseTelemetryEvent
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003738 File Offset: 0x00001938
		public PBIWinModelCacheHitSummary(int RequestCount, int CacheHitCount, int MaxSize, TraceType type, string message)
			: base("PBI.Win.ModelCacheHitSummary", TelemetryUse.Trace)
		{
			this.RequestCount = RequestCount;
			this.CacheHitCount = CacheHitCount;
			this.MaxSize = MaxSize;
			this.type = type;
			this.message = message;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000376B File Offset: 0x0000196B
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003773 File Offset: 0x00001973
		public int RequestCount { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000377C File Offset: 0x0000197C
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003784 File Offset: 0x00001984
		public int CacheHitCount { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003795 File Offset: 0x00001995
		public int MaxSize { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000379E File Offset: 0x0000199E
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000037A6 File Offset: 0x000019A6
		public TraceType type { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000037AF File Offset: 0x000019AF
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000037B7 File Offset: 0x000019B7
		public string message { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000037C0 File Offset: 0x000019C0
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				int requestCount = this.RequestCount;
				string text = ((requestCount == null) ? string.Empty : requestCount.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("RequestCount", text);
				int cacheHitCount = this.CacheHitCount;
				string text2 = ((cacheHitCount == null) ? string.Empty : cacheHitCount.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("CacheHitCount", text2);
				int maxSize = this.MaxSize;
				string text3 = ((maxSize == null) ? string.Empty : maxSize.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("MaxSize", text3);
				TraceType type = this.type;
				string text4 = ((type == null) ? string.Empty : type.ToString());
				dictionary.Add("type", text4);
				string message = this.message;
				string text5 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text5);
				return dictionary;
			}
		}
	}
}
