using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B4 RID: 180
	internal readonly struct PropertyRef
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		public PropertyRef(ulong key, JsonPropertyInfo info, byte[] nameFromJson)
		{
			this.Key = key;
			this.Info = info;
			this.NameFromJson = nameFromJson;
		}

		// Token: 0x040003EB RID: 1003
		public readonly ulong Key;

		// Token: 0x040003EC RID: 1004
		public readonly JsonPropertyInfo Info;

		// Token: 0x040003ED RID: 1005
		public readonly byte[] NameFromJson;
	}
}
