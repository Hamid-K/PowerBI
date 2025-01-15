using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000007 RID: 7
	[DataContract]
	internal sealed class LocalizedMessage
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002231 File Offset: 0x00000431
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002239 File Offset: 0x00000439
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false)]
		internal string Value { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002242 File Offset: 0x00000442
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000224A File Offset: 0x0000044A
		[DataMember(Name = "lang", IsRequired = false, EmitDefaultValue = false)]
		internal string Lang { get; set; }
	}
}
