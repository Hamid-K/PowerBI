using System;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x0200010F RID: 271
	internal class BsonBinary : BsonValue
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x000367F1 File Offset: 0x000349F1
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x000367F9 File Offset: 0x000349F9
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00036802 File Offset: 0x00034A02
		public BsonBinary(byte[] value, BsonBinaryType binaryType)
			: base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
