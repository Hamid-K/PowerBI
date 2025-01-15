using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.JsonPatch
{
	// Token: 0x02000084 RID: 132
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct JsonPatchOperation
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000CA96 File Offset: 0x0000AC96
		public JsonPatchOperation(JsonPatchOperationKind kind, [Nullable(1)] string path, string from, string rawJsonValue)
		{
			this.Kind = kind;
			this.Path = path;
			this.From = from;
			this.RawJsonValue = rawJsonValue;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000CAB5 File Offset: 0x0000ACB5
		public JsonPatchOperationKind Kind { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000CABD File Offset: 0x0000ACBD
		[Nullable(1)]
		public string Path
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000CAC5 File Offset: 0x0000ACC5
		public string From { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000CACD File Offset: 0x0000ACCD
		public string RawJsonValue { get; }
	}
}
