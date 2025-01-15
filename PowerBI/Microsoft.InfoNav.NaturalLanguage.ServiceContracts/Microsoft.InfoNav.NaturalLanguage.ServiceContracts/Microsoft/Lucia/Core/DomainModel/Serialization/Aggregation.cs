using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001CB RID: 459
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum Aggregation
	{
		// Token: 0x040007D1 RID: 2001
		None,
		// Token: 0x040007D2 RID: 2002
		Sum,
		// Token: 0x040007D3 RID: 2003
		Average,
		// Token: 0x040007D4 RID: 2004
		Count,
		// Token: 0x040007D5 RID: 2005
		Min,
		// Token: 0x040007D6 RID: 2006
		Max,
		// Token: 0x040007D7 RID: 2007
		Median,
		// Token: 0x040007D8 RID: 2008
		Variance,
		// Token: 0x040007D9 RID: 2009
		StandardDeviation
	}
}
