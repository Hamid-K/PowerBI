using System;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Bson
{
	// Token: 0x02000106 RID: 262
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	internal class BsonObjectId
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00035A21 File Offset: 0x00033C21
		public byte[] Value { get; }

		// Token: 0x06000D60 RID: 3424 RVA: 0x00035A29 File Offset: 0x00033C29
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
