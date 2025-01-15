using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B5 RID: 181
	[DataContract(Name = "PodParameter", Namespace = "http://schemas.microsoft.com/sqlbi/2015/09/ReportMetadataService")]
	public sealed class PodParameter
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00006D99 File Offset: 0x00004F99
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00006DA1 File Offset: 0x00004FA1
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00006DAA File Offset: 0x00004FAA
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00006DB2 File Offset: 0x00004FB2
		[DataMember(IsRequired = true, Order = 2)]
		public string TargetColumn { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00006DBB File Offset: 0x00004FBB
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00006DC3 File Offset: 0x00004FC3
		[DataMember(IsRequired = true, Order = 3)]
		public string TargetEntity { get; set; }
	}
}
