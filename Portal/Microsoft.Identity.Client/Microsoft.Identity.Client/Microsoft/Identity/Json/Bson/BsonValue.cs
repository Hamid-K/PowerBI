using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x0200010C RID: 268
	internal class BsonValue : BsonToken
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003677A File Offset: 0x0003497A
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00036790 File Offset: 0x00034990
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00036798 File Offset: 0x00034998
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000423 RID: 1059
		private readonly object _value;

		// Token: 0x04000424 RID: 1060
		private readonly BsonType _type;
	}
}
