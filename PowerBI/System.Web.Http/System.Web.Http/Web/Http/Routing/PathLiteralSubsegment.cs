using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000167 RID: 359
	internal sealed class PathLiteralSubsegment : PathSubsegment
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x00018D26 File Offset: 0x00016F26
		public PathLiteralSubsegment(string literal)
		{
			this.Literal = literal;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00018D35 File Offset: 0x00016F35
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00018D3D File Offset: 0x00016F3D
		public string Literal { get; private set; }
	}
}
