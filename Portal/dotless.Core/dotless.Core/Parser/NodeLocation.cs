using System;

namespace dotless.Core.Parser
{
	// Token: 0x02000024 RID: 36
	public class NodeLocation
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004795 File Offset: 0x00002995
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000479D File Offset: 0x0000299D
		public int Index { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000047A6 File Offset: 0x000029A6
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000047AE File Offset: 0x000029AE
		public string Source { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000047B7 File Offset: 0x000029B7
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000047BF File Offset: 0x000029BF
		public string FileName { get; set; }

		// Token: 0x060000F5 RID: 245 RVA: 0x000047C8 File Offset: 0x000029C8
		public NodeLocation(int index, string source, string filename)
		{
			this.Index = index;
			this.Source = source;
			this.FileName = filename;
		}
	}
}
