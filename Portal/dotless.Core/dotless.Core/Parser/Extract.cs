using System;

namespace dotless.Core.Parser
{
	// Token: 0x02000029 RID: 41
	public class Extract
	{
		// Token: 0x06000176 RID: 374 RVA: 0x000084F0 File Offset: 0x000066F0
		public Extract(string[] lines, int line)
		{
			this.Before = ((line > 0) ? lines[line - 1] : "/beginning of file");
			this.Line = lines[line];
			this.After = ((line + 1 < lines.Length) ? lines[line + 1] : "/end of file");
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000853C File Offset: 0x0000673C
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00008544 File Offset: 0x00006744
		public string After { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000854D File Offset: 0x0000674D
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00008555 File Offset: 0x00006755
		public string Before { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000855E File Offset: 0x0000675E
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00008566 File Offset: 0x00006766
		public string Line { get; set; }
	}
}
