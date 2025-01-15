using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000183 RID: 387
	public enum BinaryOperatorKind
	{
		// Token: 0x04000801 RID: 2049
		Or,
		// Token: 0x04000802 RID: 2050
		And,
		// Token: 0x04000803 RID: 2051
		Equal,
		// Token: 0x04000804 RID: 2052
		NotEqual,
		// Token: 0x04000805 RID: 2053
		GreaterThan,
		// Token: 0x04000806 RID: 2054
		GreaterThanOrEqual,
		// Token: 0x04000807 RID: 2055
		LessThan,
		// Token: 0x04000808 RID: 2056
		LessThanOrEqual,
		// Token: 0x04000809 RID: 2057
		Add,
		// Token: 0x0400080A RID: 2058
		Subtract,
		// Token: 0x0400080B RID: 2059
		Multiply,
		// Token: 0x0400080C RID: 2060
		Divide,
		// Token: 0x0400080D RID: 2061
		Modulo,
		// Token: 0x0400080E RID: 2062
		Has
	}
}
