using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x0200010D RID: 269
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x000367A0 File Offset: 0x000349A0
		private BsonBoolean(bool value)
			: base(value, BsonType.Boolean)
		{
		}

		// Token: 0x04000425 RID: 1061
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x04000426 RID: 1062
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
