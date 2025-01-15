using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200011A RID: 282
	[DataContract]
	public sealed class MoreInformation
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000F615 File Offset: 0x0000D815
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x0000F61D File Offset: 0x0000D81D
		[DataMember(Name = "odata.error", IsRequired = true, Order = 0)]
		public ODataError ODataError { get; set; }
	}
}
