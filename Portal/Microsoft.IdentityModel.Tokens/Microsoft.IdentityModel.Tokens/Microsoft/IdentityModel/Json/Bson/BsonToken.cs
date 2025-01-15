using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000109 RID: 265
	internal abstract class BsonToken
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D93 RID: 3475
		public abstract BsonType Type { get; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00036E7A File Offset: 0x0003507A
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00036E82 File Offset: 0x00035082
		public BsonToken Parent { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00036E8B File Offset: 0x0003508B
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x00036E93 File Offset: 0x00035093
		public int CalculatedSize { get; set; }
	}
}
