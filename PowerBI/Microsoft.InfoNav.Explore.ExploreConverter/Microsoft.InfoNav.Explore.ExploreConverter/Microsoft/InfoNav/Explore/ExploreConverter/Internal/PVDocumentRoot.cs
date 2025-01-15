using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200001D RID: 29
	[DataContract]
	internal sealed class PVDocumentRoot
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003D2A File Offset: 0x00001F2A
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003D32 File Offset: 0x00001F32
		[DataMember(Name = "microsoft.powerbi-rev1.Document", EmitDefaultValue = false)]
		public PVDocument Document { get; set; }

		// Token: 0x04000083 RID: 131
		internal const string DocumentName = "microsoft.powerbi-rev1.Document";
	}
}
