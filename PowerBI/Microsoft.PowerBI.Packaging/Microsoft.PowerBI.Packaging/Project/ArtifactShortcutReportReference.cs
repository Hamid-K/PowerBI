using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000059 RID: 89
	public sealed class ArtifactShortcutReportReference
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00007EB8 File Offset: 0x000060B8
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00007EC0 File Offset: 0x000060C0
		[DisplayName("Path")]
		[Description("The path to the report item. This must be a relative path and ‘/’ must be used as the directory separator.")]
		[JsonProperty("path", Required = Required.Always)]
		public string Path { get; set; } = "";
	}
}
