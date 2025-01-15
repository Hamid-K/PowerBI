using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Query.Contracts.DaxCapabilities
{
	// Token: 0x0200000E RID: 14
	[DataContract(Name = "DaxOperator")]
	public class DaxOperator
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000271A File Offset: 0x0000091A
		[DataMember(Name = "Name", IsRequired = true, Order = 10)]
		public string Name { get; set; }
	}
}
