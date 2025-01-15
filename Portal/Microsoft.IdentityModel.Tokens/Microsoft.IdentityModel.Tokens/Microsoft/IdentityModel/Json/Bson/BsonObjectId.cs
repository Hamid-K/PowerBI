using System;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x02000107 RID: 263
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Microsoft.IdentityModel.Json.Bson for more details.")]
	internal class BsonObjectId
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0003621D File Offset: 0x0003441D
		public byte[] Value { get; }

		// Token: 0x06000D70 RID: 3440 RVA: 0x00036225 File Offset: 0x00034425
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}
