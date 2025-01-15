using System;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200007E RID: 126
	public static class ODataSegmentKinds
	{
		// Token: 0x040000FF RID: 255
		public const string ServiceBase = "~";

		// Token: 0x04000100 RID: 256
		public const string Batch = "$batch";

		// Token: 0x04000101 RID: 257
		public const string Ref = "$ref";

		// Token: 0x04000102 RID: 258
		public const string Metadata = "$metadata";

		// Token: 0x04000103 RID: 259
		public const string Value = "$value";

		// Token: 0x04000104 RID: 260
		public const string Count = "$count";

		// Token: 0x04000105 RID: 261
		public const string Action = "action";

		// Token: 0x04000106 RID: 262
		public const string Function = "function";

		// Token: 0x04000107 RID: 263
		public const string UnboundAction = "unboundaction";

		// Token: 0x04000108 RID: 264
		public const string UnboundFunction = "unboundfunction";

		// Token: 0x04000109 RID: 265
		public const string Cast = "cast";

		// Token: 0x0400010A RID: 266
		public const string EntitySet = "entityset";

		// Token: 0x0400010B RID: 267
		public const string Singleton = "singleton";

		// Token: 0x0400010C RID: 268
		public const string Key = "key";

		// Token: 0x0400010D RID: 269
		public const string Navigation = "navigation";

		// Token: 0x0400010E RID: 270
		public const string PathTemplate = "template";

		// Token: 0x0400010F RID: 271
		public const string Property = "property";

		// Token: 0x04000110 RID: 272
		public const string DynamicProperty = "dynamicproperty";

		// Token: 0x04000111 RID: 273
		public const string Unresolved = "unresolved";
	}
}
