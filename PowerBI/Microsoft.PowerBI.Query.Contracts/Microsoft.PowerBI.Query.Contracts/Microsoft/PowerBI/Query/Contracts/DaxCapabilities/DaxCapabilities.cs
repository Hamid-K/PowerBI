using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Query.Contracts.DaxCapabilities
{
	// Token: 0x0200000A RID: 10
	[DataContract(Name = "DaxCapabilities")]
	public class DaxCapabilities
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002294 File Offset: 0x00000494
		[DataMember(Name = "Functions", IsRequired = true, Order = 10)]
		public IList<DaxFunction> SupportedFunctions { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000229D File Offset: 0x0000049D
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000022A5 File Offset: 0x000004A5
		[DataMember(Name = "Operators", IsRequired = true, Order = 20)]
		public IList<DaxOperator> SupportedOperators { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000022AE File Offset: 0x000004AE
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000022B6 File Offset: 0x000004B6
		[DataMember(Name = "SupportsVariations", IsRequired = false, Order = 30)]
		public bool SupportsVariations { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000022BF File Offset: 0x000004BF
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000022C7 File Offset: 0x000004C7
		[DataMember(Name = "SupportsTableConstructor", IsRequired = false, Order = 40)]
		public bool SupportsTableConstructor { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000022D0 File Offset: 0x000004D0
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000022D8 File Offset: 0x000004D8
		[DataMember(Name = "SupportsVirtualColumns", IsRequired = false, Order = 50)]
		public bool SupportsVirtualColumns { get; set; }
	}
}
