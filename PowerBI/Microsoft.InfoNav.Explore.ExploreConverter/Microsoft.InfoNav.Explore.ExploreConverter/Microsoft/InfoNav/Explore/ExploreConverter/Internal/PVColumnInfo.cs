using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200001A RID: 26
	[DataContract]
	internal sealed class PVColumnInfo
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003CCE File Offset: 0x00001ECE
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003CD6 File Offset: 0x00001ED6
		[DataMember]
		public string CustomFormatString { get; set; }
	}
}
