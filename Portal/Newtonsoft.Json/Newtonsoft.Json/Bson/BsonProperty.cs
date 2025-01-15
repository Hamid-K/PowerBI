using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000112 RID: 274
	internal class BsonProperty
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000371AF File Offset: 0x000353AF
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x000371B7 File Offset: 0x000353B7
		public BsonString Name { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000371C0 File Offset: 0x000353C0
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x000371C8 File Offset: 0x000353C8
		public BsonToken Value { get; set; }
	}
}
