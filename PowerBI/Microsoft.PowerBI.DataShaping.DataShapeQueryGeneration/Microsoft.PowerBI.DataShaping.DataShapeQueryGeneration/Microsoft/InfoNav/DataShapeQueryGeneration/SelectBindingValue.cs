using System;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000014 RID: 20
	internal class SelectBindingValue
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005460 File Offset: 0x00003660
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00005468 File Offset: 0x00003668
		internal string Value { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005471 File Offset: 0x00003671
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00005479 File Offset: 0x00003679
		internal IntervalValue Interval { get; set; }
	}
}
