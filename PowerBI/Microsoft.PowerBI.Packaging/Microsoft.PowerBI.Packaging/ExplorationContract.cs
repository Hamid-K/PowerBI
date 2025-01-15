using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200000A RID: 10
	public class ExplorationContract
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002771 File Offset: 0x00000971
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002779 File Offset: 0x00000979
		[JsonProperty("version", Required = Required.Always)]
		public ExplorationArtifact Version { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002782 File Offset: 0x00000982
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000278A File Offset: 0x0000098A
		[JsonProperty("report", Required = Required.Always)]
		public ExplorationArtifact Report { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002793 File Offset: 0x00000993
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000279B File Offset: 0x0000099B
		[JsonProperty("reportExtensions", Required = Required.Default)]
		public ExplorationArtifact ReportExtensions { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000027A4 File Offset: 0x000009A4
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000027AC File Offset: 0x000009AC
		[JsonProperty("pages", Required = Required.Always)]
		public Pages Pages { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000027B5 File Offset: 0x000009B5
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000027BD File Offset: 0x000009BD
		[JsonProperty("bookmarks", Required = Required.Default)]
		public Bookmarks Bookmarks { get; set; }
	}
}
