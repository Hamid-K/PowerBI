using System;

namespace Microsoft.IdentityModel.Json.Bson
{
	// Token: 0x0200010F RID: 271
	internal class BsonString : BsonValue
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00036FC3 File Offset: 0x000351C3
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00036FCB File Offset: 0x000351CB
		public int ByteCount { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00036FD4 File Offset: 0x000351D4
		public bool IncludeLength { get; }

		// Token: 0x06000DAE RID: 3502 RVA: 0x00036FDC File Offset: 0x000351DC
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
