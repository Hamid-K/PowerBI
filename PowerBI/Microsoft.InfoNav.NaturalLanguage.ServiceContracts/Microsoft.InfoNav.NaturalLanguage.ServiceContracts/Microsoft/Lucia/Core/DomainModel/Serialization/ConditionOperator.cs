using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001CA RID: 458
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum ConditionOperator
	{
		// Token: 0x040007C6 RID: 1990
		Equals,
		// Token: 0x040007C7 RID: 1991
		NotEquals,
		// Token: 0x040007C8 RID: 1992
		GreaterThan,
		// Token: 0x040007C9 RID: 1993
		LessThan,
		// Token: 0x040007CA RID: 1994
		GreaterThanOrEquals,
		// Token: 0x040007CB RID: 1995
		LessThanOrEquals,
		// Token: 0x040007CC RID: 1996
		Contains,
		// Token: 0x040007CD RID: 1997
		NotContains,
		// Token: 0x040007CE RID: 1998
		StartsWith,
		// Token: 0x040007CF RID: 1999
		NotStartsWith
	}
}
