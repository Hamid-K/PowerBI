using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000048 RID: 72
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ContextEvent
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003F67 File Offset: 0x00002167
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00003F6F File Offset: 0x0000216F
		[DataMember(IsRequired = false, Order = 10)]
		public QueryDefinition QueryDefinition { get; set; }
	}
}
