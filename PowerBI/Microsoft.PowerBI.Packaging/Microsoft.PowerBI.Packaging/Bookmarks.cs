using System;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200000C RID: 12
	public class Bookmarks : ExplorationArtifact
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000027F8 File Offset: 0x000009F8
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002800 File Offset: 0x00000A00
		[JsonProperty("bookmarks", Required = Required.Always)]
		public NonNulls<ExplorationArtifact> BookmarksList { get; set; }
	}
}
