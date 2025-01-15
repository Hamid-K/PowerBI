using System;

namespace Model
{
	// Token: 0x0200002E RID: 46
	public class Telemetry
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000029B1 File Offset: 0x00000BB1
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000029B9 File Offset: 0x00000BB9
		public Guid Id { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000029C2 File Offset: 0x00000BC2
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000029CA File Offset: 0x00000BCA
		public TelemetryHostData Properties { get; set; }
	}
}
