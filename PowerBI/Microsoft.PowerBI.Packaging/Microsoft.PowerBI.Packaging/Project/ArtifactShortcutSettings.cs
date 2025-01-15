using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200005A RID: 90
	public sealed class ArtifactShortcutSettings
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00007EDC File Offset: 0x000060DC
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00007EE4 File Offset: 0x000060E4
		[DisplayName("EnableAutoRecovery")]
		[Description("Whether Power BI Desktop should periodically save a copy of the open report and semantic model to guard against the program closing unexpectedly.")]
		[JsonProperty("enableAutoRecovery", Required = Required.Default)]
		public bool EnableAutoRecovery { get; set; }
	}
}
