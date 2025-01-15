using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000112 RID: 274
	internal class BsonProperty
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00037057 File Offset: 0x00035257
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0003705F File Offset: 0x0003525F
		public BsonString Name { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00037068 File Offset: 0x00035268
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00037070 File Offset: 0x00035270
		public BsonToken Value { get; set; }
	}
}
