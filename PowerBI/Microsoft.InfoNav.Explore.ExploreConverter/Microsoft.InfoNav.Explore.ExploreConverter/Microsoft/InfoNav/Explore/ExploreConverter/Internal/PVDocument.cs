using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200001B RID: 27
	[DataContract]
	internal sealed class PVDocument
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003CE7 File Offset: 0x00001EE7
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003CEF File Offset: 0x00001EEF
		[DataMember]
		public PVVisual RootVisual { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003CF8 File Offset: 0x00001EF8
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003D00 File Offset: 0x00001F00
		[DataMember]
		public PVDocumentContext Context { get; set; }
	}
}
