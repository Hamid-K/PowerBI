using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000169 RID: 361
	[DataContract(Name = "DataView", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public class DataViewCapabilities
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001335D File Offset: 0x0001155D
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00013365 File Offset: 0x00011565
		[DataMember(Name = "DataViewUnsupportedReasons", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public DataViewUnsupportedReasons DataViewUnsupportedReasons { get; set; }
	}
}
