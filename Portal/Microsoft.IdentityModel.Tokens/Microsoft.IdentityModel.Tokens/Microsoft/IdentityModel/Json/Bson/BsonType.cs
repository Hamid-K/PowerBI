﻿using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000113 RID: 275
	internal enum BsonType : sbyte
	{
		// Token: 0x0400044C RID: 1100
		Number = 1,
		// Token: 0x0400044D RID: 1101
		String,
		// Token: 0x0400044E RID: 1102
		Object,
		// Token: 0x0400044F RID: 1103
		Array,
		// Token: 0x04000450 RID: 1104
		Binary,
		// Token: 0x04000451 RID: 1105
		Undefined,
		// Token: 0x04000452 RID: 1106
		Oid,
		// Token: 0x04000453 RID: 1107
		Boolean,
		// Token: 0x04000454 RID: 1108
		Date,
		// Token: 0x04000455 RID: 1109
		Null,
		// Token: 0x04000456 RID: 1110
		Regex,
		// Token: 0x04000457 RID: 1111
		Reference,
		// Token: 0x04000458 RID: 1112
		Code,
		// Token: 0x04000459 RID: 1113
		Symbol,
		// Token: 0x0400045A RID: 1114
		CodeWScope,
		// Token: 0x0400045B RID: 1115
		Integer,
		// Token: 0x0400045C RID: 1116
		TimeStamp,
		// Token: 0x0400045D RID: 1117
		Long,
		// Token: 0x0400045E RID: 1118
		MinKey = -1,
		// Token: 0x0400045F RID: 1119
		MaxKey = 127
	}
}
