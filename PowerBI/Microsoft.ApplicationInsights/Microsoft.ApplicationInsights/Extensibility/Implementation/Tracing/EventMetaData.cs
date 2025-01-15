using System;
using System.Diagnostics.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x0200009B RID: 155
	internal class EventMetaData
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00014E7C File Offset: 0x0001307C
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00014E84 File Offset: 0x00013084
		public string EventSourceName { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00014E8D File Offset: 0x0001308D
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00014E95 File Offset: 0x00013095
		public int EventId { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00014E9E File Offset: 0x0001309E
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00014EA6 File Offset: 0x000130A6
		public string MessageFormat { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00014EAF File Offset: 0x000130AF
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00014EB7 File Offset: 0x000130B7
		public long Keywords { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00014EC0 File Offset: 0x000130C0
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00014EC8 File Offset: 0x000130C8
		public EventLevel Level { get; set; }
	}
}
