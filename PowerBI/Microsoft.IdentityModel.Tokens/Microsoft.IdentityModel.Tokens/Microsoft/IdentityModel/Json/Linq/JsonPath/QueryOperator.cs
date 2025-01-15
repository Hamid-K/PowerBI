using System;

namespace Microsoft.IdentityModel.Json.Linq.JsonPath
{
	// Token: 0x020000D7 RID: 215
	internal enum QueryOperator
	{
		// Token: 0x040003D6 RID: 982
		None,
		// Token: 0x040003D7 RID: 983
		Equals,
		// Token: 0x040003D8 RID: 984
		NotEquals,
		// Token: 0x040003D9 RID: 985
		Exists,
		// Token: 0x040003DA RID: 986
		LessThan,
		// Token: 0x040003DB RID: 987
		LessThanOrEquals,
		// Token: 0x040003DC RID: 988
		GreaterThan,
		// Token: 0x040003DD RID: 989
		GreaterThanOrEquals,
		// Token: 0x040003DE RID: 990
		And,
		// Token: 0x040003DF RID: 991
		Or,
		// Token: 0x040003E0 RID: 992
		RegexEquals,
		// Token: 0x040003E1 RID: 993
		StrictEquals,
		// Token: 0x040003E2 RID: 994
		StrictNotEquals
	}
}
