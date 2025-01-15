using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000107 RID: 263
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00036375 File Offset: 0x00034575
		public byte[] Value { get; }

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003637D File Offset: 0x0003457D
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
