using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	internal sealed class PVFrame
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003DB1 File Offset: 0x00001FB1
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003DB9 File Offset: 0x00001FB9
		[DataMember]
		public PVPosition Position { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003DC2 File Offset: 0x00001FC2
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003DCA File Offset: 0x00001FCA
		[DataMember]
		public decimal Width { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003DD3 File Offset: 0x00001FD3
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003DDB File Offset: 0x00001FDB
		[DataMember]
		public decimal Height { get; set; }
	}
}
