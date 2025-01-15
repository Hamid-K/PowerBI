using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000117 RID: 279
	[DataContract]
	public sealed class ErrorMessage
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0000F5B9 File Offset: 0x0000D7B9
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0000F5C1 File Offset: 0x0000D7C1
		[DataMember(Name = "lang", IsRequired = true, Order = 0)]
		public string Language { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0000F5CA File Offset: 0x0000D7CA
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0000F5D2 File Offset: 0x0000D7D2
		[DataMember(Name = "value", IsRequired = true, Order = 10)]
		public string Value { get; set; }
	}
}
