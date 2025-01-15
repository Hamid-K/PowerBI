using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000026 RID: 38
	[DataContract]
	public sealed class AnalysisServicesUpnMappingRule
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002DC7 File Offset: 0x00000FC7
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002DCF File Offset: 0x00000FCF
		[DataMember(Name = "algorithm", Order = 10)]
		public MappingAlgorithmKind Algorithm { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002DD8 File Offset: 0x00000FD8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002DE0 File Offset: 0x00000FE0
		[DataMember(Name = "searchString", Order = 20)]
		public string SearchString { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002DE9 File Offset: 0x00000FE9
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002DF1 File Offset: 0x00000FF1
		[DataMember(Name = "replacementString", Order = 30)]
		public string ReplacementString { get; set; }
	}
}
