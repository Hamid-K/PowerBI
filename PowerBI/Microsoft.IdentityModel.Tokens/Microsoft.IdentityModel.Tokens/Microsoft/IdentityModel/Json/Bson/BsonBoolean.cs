using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x0200010E RID: 270
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00036F9C File Offset: 0x0003519C
		private BsonBoolean(bool value)
			: base(value, BsonType.Boolean)
		{
		}

		// Token: 0x04000442 RID: 1090
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x04000443 RID: 1091
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
