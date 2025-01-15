using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A6 RID: 166
	internal class TraceEvent
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000151D5 File Offset: 0x000133D5
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x000151DD File Offset: 0x000133DD
		public EventMetaData MetaData { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000151E6 File Offset: 0x000133E6
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x000151EE File Offset: 0x000133EE
		public object[] Payload { get; set; }
	}
}
