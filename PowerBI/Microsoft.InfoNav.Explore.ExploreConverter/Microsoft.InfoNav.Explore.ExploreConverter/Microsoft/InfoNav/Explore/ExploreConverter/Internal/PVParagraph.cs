using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000020 RID: 32
	[DataContract]
	internal sealed class PVParagraph
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003DEC File Offset: 0x00001FEC
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003DF4 File Offset: 0x00001FF4
		[DataMember]
		public string HorizontalTextAlignment { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003DFD File Offset: 0x00001FFD
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003E05 File Offset: 0x00002005
		[DataMember]
		public List<PVTextRun> TextRuns { get; set; }
	}
}
