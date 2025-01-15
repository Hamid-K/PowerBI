using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000274 RID: 628
	public sealed class ODataResourceCollectionInfo : ODataAnnotatable
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00049B3D File Offset: 0x00047D3D
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00049B45 File Offset: 0x00047D45
		public Uri Url { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00049B4E File Offset: 0x00047D4E
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00049B56 File Offset: 0x00047D56
		public string Name { get; set; }
	}
}
