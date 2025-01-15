using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x02000108 RID: 264
	internal abstract class BsonToken
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D83 RID: 3459
		public abstract BsonType Type { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0003667E File Offset: 0x0003487E
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00036686 File Offset: 0x00034886
		public BsonToken Parent { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0003668F File Offset: 0x0003488F
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00036697 File Offset: 0x00034897
		public int CalculatedSize { get; set; }
	}
}
