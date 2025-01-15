using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000057 RID: 87
	[DisplayName("ItemShortcut")]
	[Description("The ItemShortcut is a file that ends in a .pbip extension. It is the default shortcut for opening up pbip based items and holds a link to the report path as well as other settings. This file is optional.")]
	public sealed class ArtifactShortcut : ArtifactBase
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00007E3C File Offset: 0x0000603C
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					new Version(1, 0)
				};
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00007E4E File Offset: 0x0000604E
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00007E56 File Offset: 0x00006056
		[DisplayName("Version")]
		[Description("The version of the item shortcut file format.")]
		[JsonProperty("version", Required = Required.Always)]
		[EnumDataType(typeof(ArtifactShortcut.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00007E5F File Offset: 0x0000605F
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00007E67 File Offset: 0x00006067
		[DisplayName("Artifacts")]
		[Description("The items referenced by this shortcut file.")]
		[JsonProperty("artifacts", Required = Required.Always)]
		public NonNulls<ArtifactShortcutContainer> Artifacts { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00007E70 File Offset: 0x00006070
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00007E78 File Offset: 0x00006078
		[DisplayName("Settings")]
		[Description("Holds settings applied when opening this item shortcut.")]
		[JsonProperty("settings", Required = Required.Default)]
		public ArtifactShortcutSettings Settings { get; set; } = new ArtifactShortcutSettings();

		// Token: 0x020000D1 RID: 209
		private enum ArtifactVersions
		{
			// Token: 0x0400033D RID: 829
			[EnumMember(Value = "1.0")]
			Version1_0
		}
	}
}
