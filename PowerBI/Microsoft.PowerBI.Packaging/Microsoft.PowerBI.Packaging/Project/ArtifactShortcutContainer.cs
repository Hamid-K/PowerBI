using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000058 RID: 88
	public sealed class ArtifactShortcutContainer
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007E94 File Offset: 0x00006094
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00007E9C File Offset: 0x0000609C
		[DisplayName("Report")]
		[Description("Describes a reference to a report item.")]
		[JsonProperty("report", Required = Required.Always)]
		public ArtifactShortcutReportReference Report { get; set; } = new ArtifactShortcutReportReference();
	}
}
