using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000149 RID: 329
	[DataContract(Name = "ChangeDetectionMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ChangeDetectionMetadata
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00011B47 File Offset: 0x0000FD47
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00011B4F File Offset: 0x0000FD4F
		[DataMember(Name = "version", IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00011B58 File Offset: 0x0000FD58
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00011B60 File Offset: 0x0000FD60
		[DataMember(Name = "refreshInterval", IsRequired = true, Order = 1)]
		public string RefreshInterval { get; set; }
	}
}
