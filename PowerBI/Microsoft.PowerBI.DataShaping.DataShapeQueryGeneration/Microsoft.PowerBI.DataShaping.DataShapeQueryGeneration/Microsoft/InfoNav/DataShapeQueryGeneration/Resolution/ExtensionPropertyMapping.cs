using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Resolution
{
	// Token: 0x020000F8 RID: 248
	internal sealed class ExtensionPropertyMapping
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x00021596 File Offset: 0x0001F796
		internal ExtensionPropertyMapping(string originalName, string resolvedName)
		{
			this.OriginalName = originalName;
			this.ResolvedName = resolvedName;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000215AC File Offset: 0x0001F7AC
		internal string OriginalName { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000215B4 File Offset: 0x0001F7B4
		internal string ResolvedName { get; }
	}
}
