using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000BB RID: 187
	[DataContract]
	internal sealed class Theme
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00014420 File Offset: 0x00012620
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00014428 File Offset: 0x00012628
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00014431 File Offset: 0x00012631
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00014439 File Offset: 0x00012639
		[DataMember(Name = "accent1")]
		public string Accent1 { get; set; }
	}
}
