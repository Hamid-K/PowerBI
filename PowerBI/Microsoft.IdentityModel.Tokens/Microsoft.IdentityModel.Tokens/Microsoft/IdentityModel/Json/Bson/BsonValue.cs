using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x0200010D RID: 269
	internal class BsonValue : BsonToken
	{
		// Token: 0x06000DA6 RID: 3494 RVA: 0x00036F76 File Offset: 0x00035176
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00036F8C File Offset: 0x0003518C
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00036F94 File Offset: 0x00035194
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000440 RID: 1088
		private readonly object _value;

		// Token: 0x04000441 RID: 1089
		private readonly BsonType _type;
	}
}
