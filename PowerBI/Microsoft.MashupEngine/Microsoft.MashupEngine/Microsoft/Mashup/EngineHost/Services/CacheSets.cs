using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001992 RID: 6546
	public sealed class CacheSets : ICacheSets, IDisposable
	{
		// Token: 0x17002A66 RID: 10854
		// (get) Token: 0x0600A61B RID: 42523 RVA: 0x00225B1B File Offset: 0x00223D1B
		// (set) Token: 0x0600A61C RID: 42524 RVA: 0x00225B23 File Offset: 0x00223D23
		public ICacheSet Metadata { get; set; }

		// Token: 0x17002A67 RID: 10855
		// (get) Token: 0x0600A61D RID: 42525 RVA: 0x00225B2C File Offset: 0x00223D2C
		// (set) Token: 0x0600A61E RID: 42526 RVA: 0x00225B34 File Offset: 0x00223D34
		public ICacheSet Data { get; set; }

		// Token: 0x0600A61F RID: 42527 RVA: 0x00225B3D File Offset: 0x00223D3D
		public void Dispose()
		{
			this.Data.Dispose();
			this.Metadata.Dispose();
		}
	}
}
