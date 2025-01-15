using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000FE RID: 254
	public sealed class InferredTermMetadata
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00009193 File Offset: 0x00007393
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0000919B File Offset: 0x0000739B
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string DefinedTermBaseForm { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000091A4 File Offset: 0x000073A4
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x000091AC File Offset: 0x000073AC
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public AdjectiveForm? AdjectiveForm { get; set; }
	}
}
