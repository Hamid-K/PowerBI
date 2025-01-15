using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010C RID: 268
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0003709E File Offset: 0x0003529E
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000370AD File Offset: 0x000352AD
		public override BsonType Type { get; }

		// Token: 0x0400043E RID: 1086
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x0400043F RID: 1087
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
