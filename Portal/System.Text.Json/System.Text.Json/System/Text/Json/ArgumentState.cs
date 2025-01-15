using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json
{
	// Token: 0x0200004A RID: 74
	internal sealed class ArgumentState
	{
		// Token: 0x04000187 RID: 391
		public object Arguments;

		// Token: 0x04000188 RID: 392
		public global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>[] FoundProperties;

		// Token: 0x04000189 RID: 393
		public global::System.ValueTuple<JsonPropertyInfo, object, string>[] FoundPropertiesAsync;

		// Token: 0x0400018A RID: 394
		public int FoundPropertyCount;

		// Token: 0x0400018B RID: 395
		public JsonParameterInfo JsonParameterInfo;

		// Token: 0x0400018C RID: 396
		public int ParameterIndex;

		// Token: 0x0400018D RID: 397
		public List<ParameterRef> ParameterRefCache;
	}
}
