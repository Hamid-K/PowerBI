using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000109 RID: 265
	internal abstract class BsonToken
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D9D RID: 3485
		public abstract BsonType Type { get; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00036FD2 File Offset: 0x000351D2
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x00036FDA File Offset: 0x000351DA
		public BsonToken Parent { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00036FE3 File Offset: 0x000351E3
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x00036FEB File Offset: 0x000351EB
		public int CalculatedSize { get; set; }
	}
}
