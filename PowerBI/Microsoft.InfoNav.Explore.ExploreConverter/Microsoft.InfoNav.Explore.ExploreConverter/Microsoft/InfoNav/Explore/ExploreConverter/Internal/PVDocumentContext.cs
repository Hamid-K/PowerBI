using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200001C RID: 28
	[DataContract]
	internal sealed class PVDocumentContext
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003D11 File Offset: 0x00001F11
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003D19 File Offset: 0x00001F19
		[DataMember]
		public List<PVResourceEntry> ImageResourceMap { get; set; }
	}
}
