using System;
using AngleSharp.Dom;

namespace AngleSharp.Services.Styling
{
	// Token: 0x0200003C RID: 60
	public sealed class StyleOptions
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000072D6 File Offset: 0x000054D6
		public StyleOptions(IBrowsingContext context)
		{
			this.Context = context;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000072E5 File Offset: 0x000054E5
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000072ED File Offset: 0x000054ED
		public IElement Element { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000072F6 File Offset: 0x000054F6
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000072FE File Offset: 0x000054FE
		public bool IsDisabled { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00007307 File Offset: 0x00005507
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000730F File Offset: 0x0000550F
		public bool IsAlternate { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007318 File Offset: 0x00005518
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00007320 File Offset: 0x00005520
		public IBrowsingContext Context { get; private set; }
	}
}
