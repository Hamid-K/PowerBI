using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B2 RID: 178
	internal readonly struct ParameterRef
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x0002C79C File Offset: 0x0002A99C
		public ParameterRef(ulong key, JsonParameterInfo info, byte[] nameFromJson)
		{
			this.Key = key;
			this.Info = info;
			this.NameFromJson = nameFromJson;
		}

		// Token: 0x040003DE RID: 990
		public readonly ulong Key;

		// Token: 0x040003DF RID: 991
		public readonly JsonParameterInfo Info;

		// Token: 0x040003E0 RID: 992
		public readonly byte[] NameFromJson;
	}
}
