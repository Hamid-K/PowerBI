using System;

namespace Model
{
	// Token: 0x0200002F RID: 47
	public class UrlDrillthroughTarget : DrillthroughTarget
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000029D3 File Offset: 0x00000BD3
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000029DB File Offset: 0x00000BDB
		public string Url { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000029E4 File Offset: 0x00000BE4
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000029EC File Offset: 0x00000BEC
		public bool DirectNavigation { get; set; }
	}
}
