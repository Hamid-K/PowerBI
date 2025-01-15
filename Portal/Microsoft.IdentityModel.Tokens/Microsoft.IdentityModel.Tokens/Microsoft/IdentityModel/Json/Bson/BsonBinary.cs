using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000110 RID: 272
	internal class BsonBinary : BsonValue
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00036FED File Offset: 0x000351ED
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x00036FF5 File Offset: 0x000351F5
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00036FFE File Offset: 0x000351FE
		public BsonBinary(byte[] value, BsonBinaryType binaryType)
			: base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
