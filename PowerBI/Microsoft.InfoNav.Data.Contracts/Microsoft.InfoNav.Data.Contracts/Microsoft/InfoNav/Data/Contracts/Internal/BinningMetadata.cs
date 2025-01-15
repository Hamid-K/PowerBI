using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001DB RID: 475
	[DataContract(Name = "binningMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class BinningMetadata
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00019121 File Offset: 0x00017321
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00019129 File Offset: 0x00017329
		[DataMember(Name = "binSize", IsRequired = true, Order = 0)]
		public BinSize BinSize { get; set; }
	}
}
