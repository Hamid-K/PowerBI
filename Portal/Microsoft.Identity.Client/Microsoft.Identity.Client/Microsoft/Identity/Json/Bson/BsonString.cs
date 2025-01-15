using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x0200010E RID: 270
	internal class BsonString : BsonValue
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x000367C7 File Offset: 0x000349C7
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x000367CF File Offset: 0x000349CF
		public int ByteCount { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000367D8 File Offset: 0x000349D8
		public bool IncludeLength { get; }

		// Token: 0x06000D9E RID: 3486 RVA: 0x000367E0 File Offset: 0x000349E0
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
