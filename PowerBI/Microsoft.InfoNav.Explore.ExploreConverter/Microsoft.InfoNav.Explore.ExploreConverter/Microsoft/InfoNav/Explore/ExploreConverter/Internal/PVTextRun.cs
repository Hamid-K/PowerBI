using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000025 RID: 37
	[DataContract]
	internal sealed class PVTextRun
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003EBE File Offset: 0x000020BE
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003EC6 File Offset: 0x000020C6
		[DataMember]
		public PVTextStyle TextStyle { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003ECF File Offset: 0x000020CF
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003ED7 File Offset: 0x000020D7
		[DataMember]
		public string Value { get; set; }
	}
}
