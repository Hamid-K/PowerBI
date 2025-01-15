using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x0200010C RID: 268
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00036F46 File Offset: 0x00035146
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00036F55 File Offset: 0x00035155
		public override BsonType Type { get; }

		// Token: 0x0400043D RID: 1085
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x0400043E RID: 1086
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
