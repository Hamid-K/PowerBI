using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B4 RID: 180
	[DataContract(Name = "Pod", Namespace = "http://schemas.microsoft.com/sqlbi/2015/09/ReportMetadataService")]
	public sealed class Pod
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00006D5E File Offset: 0x00004F5E
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00006D66 File Offset: 0x00004F66
		[DataMember(IsRequired = true, Order = 10)]
		public string Name { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00006D6F File Offset: 0x00004F6F
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00006D77 File Offset: 0x00004F77
		[DataMember(IsRequired = true, Order = 20)]
		public IList<PodParameter> Parameters { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00006D80 File Offset: 0x00004F80
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00006D88 File Offset: 0x00004F88
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool CortanaEnabled { get; set; }
	}
}
