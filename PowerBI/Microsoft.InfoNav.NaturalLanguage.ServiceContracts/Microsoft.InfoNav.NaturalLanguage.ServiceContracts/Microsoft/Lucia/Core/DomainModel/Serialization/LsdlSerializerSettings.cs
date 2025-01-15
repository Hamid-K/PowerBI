using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001DE RID: 478
	public sealed class LsdlSerializerSettings : ILsdlSerializerSettings
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00013677 File Offset: 0x00011877
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0001367F File Offset: 0x0001187F
		public bool CanonicalBindings { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00013688 File Offset: 0x00011888
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00013690 File Offset: 0x00011890
		public bool CanonicalForm { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00013699 File Offset: 0x00011899
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x000136A1 File Offset: 0x000118A1
		public IConceptualSchema ConceptualSchema { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000136AA File Offset: 0x000118AA
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x000136B2 File Offset: 0x000118B2
		public bool OmitVersion { get; set; }

		// Token: 0x040007F2 RID: 2034
		internal static readonly ILsdlSerializerSettings Default = new LsdlSerializerSettings();
	}
}
