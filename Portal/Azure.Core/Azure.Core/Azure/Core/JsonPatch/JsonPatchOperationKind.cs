using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.JsonPatch
{
	// Token: 0x02000085 RID: 133
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct JsonPatchOperationKind
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		private JsonPatchOperationKind(string operation)
		{
			this._operation = operation;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000CADE File Offset: 0x0000ACDE
		public static JsonPatchOperationKind Add { get; } = new JsonPatchOperationKind("add");

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
		public static JsonPatchOperationKind Remove { get; } = new JsonPatchOperationKind("remove");

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public static JsonPatchOperationKind Replace { get; } = new JsonPatchOperationKind("replace");

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000CAF3 File Offset: 0x0000ACF3
		public static JsonPatchOperationKind Move { get; } = new JsonPatchOperationKind("move");

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000CAFA File Offset: 0x0000ACFA
		public static JsonPatchOperationKind Copy { get; } = new JsonPatchOperationKind("copy");

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000CB01 File Offset: 0x0000AD01
		public static JsonPatchOperationKind Test { get; } = new JsonPatchOperationKind("test");

		// Token: 0x06000441 RID: 1089 RVA: 0x0000CB08 File Offset: 0x0000AD08
		public override string ToString()
		{
			return this._operation;
		}

		// Token: 0x040001C0 RID: 448
		private readonly string _operation;
	}
}
