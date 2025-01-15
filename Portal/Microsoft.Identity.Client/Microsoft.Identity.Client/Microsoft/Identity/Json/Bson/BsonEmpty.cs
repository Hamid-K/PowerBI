using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x0200010B RID: 267
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x0003674A File Offset: 0x0003494A
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00036759 File Offset: 0x00034959
		public override BsonType Type { get; }

		// Token: 0x04000420 RID: 1056
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x04000421 RID: 1057
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
