using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200017F RID: 383
	[DataContract(Name = "ApplicationContext", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ApplicationContext
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00014385 File Offset: 0x00012585
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0001438D File Offset: 0x0001258D
		[DataMember(Name = "DatasetId", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string DatasetId { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00014396 File Offset: 0x00012596
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0001439E File Offset: 0x0001259E
		[DataMember(Name = "Sources", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<ApplicationContextSource> Sources { get; set; }
	}
}
