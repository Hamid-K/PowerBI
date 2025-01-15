using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonNamingPolicy
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000053A3 File Offset: 0x000035A3
		public static JsonNamingPolicy CamelCase { get; } = new JsonCamelCaseNamingPolicy();

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000053AA File Offset: 0x000035AA
		public static JsonNamingPolicy SnakeCaseLower { get; } = new JsonSnakeCaseLowerNamingPolicy();

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000053B1 File Offset: 0x000035B1
		public static JsonNamingPolicy SnakeCaseUpper { get; } = new JsonSnakeCaseUpperNamingPolicy();

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000053B8 File Offset: 0x000035B8
		public static JsonNamingPolicy KebabCaseLower { get; } = new JsonKebabCaseLowerNamingPolicy();

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000053BF File Offset: 0x000035BF
		public static JsonNamingPolicy KebabCaseUpper { get; } = new JsonKebabCaseUpperNamingPolicy();

		// Token: 0x06000225 RID: 549
		public abstract string ConvertName(string name);
	}
}
