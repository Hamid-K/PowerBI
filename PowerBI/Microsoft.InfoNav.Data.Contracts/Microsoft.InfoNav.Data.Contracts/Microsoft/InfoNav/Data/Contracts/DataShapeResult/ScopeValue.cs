using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200011F RID: 287
	[DataContract]
	public sealed class ScopeValue
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0000F700 File Offset: 0x0000D900
		[DataMember(Name = "Value", IsRequired = true, Order = 0)]
		public object Value { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000F709 File Offset: 0x0000D909
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0000F711 File Offset: 0x0000D911
		[DataMember(Name = "Key", IsRequired = true, Order = 10)]
		public string Key { get; set; }
	}
}
