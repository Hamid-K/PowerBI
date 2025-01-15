using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExplorationContracts
{
	// Token: 0x02000007 RID: 7
	[DataContract]
	public class ExplorationCompatibilityInfo
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020D5 File Offset: 0x000002D5
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020DD File Offset: 0x000002DD
		[DataMember(Name = "category", EmitDefaultValue = false)]
		public ExplorationCompatibilityCategory Category { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020E6 File Offset: 0x000002E6
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020EE File Offset: 0x000002EE
		[DataMember(Name = "details", EmitDefaultValue = false)]
		public string Details { get; set; }
	}
}
