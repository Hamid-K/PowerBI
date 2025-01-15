using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000004 RID: 4
	[DataContract]
	internal sealed class LocalizedMessage
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000221D File Offset: 0x0000041D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002225 File Offset: 0x00000425
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false)]
		internal string Value { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000222E File Offset: 0x0000042E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002236 File Offset: 0x00000436
		[DataMember(Name = "lang", IsRequired = false, EmitDefaultValue = false)]
		internal string Lang { get; set; }
	}
}
