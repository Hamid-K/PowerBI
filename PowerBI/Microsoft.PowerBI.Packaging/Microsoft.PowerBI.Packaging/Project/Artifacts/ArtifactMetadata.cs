using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000080 RID: 128
	[DataContract]
	[DisplayName("ItemMetadata")]
	[Description("The ItemMetadata stored as item.metadata.json holds the Type and DisplayName of the item. The DisplayName is what is shown in the title bar of Power BI Desktop. There is an ItemMetadata file in both the Report and SemanticModel folders. This file is optional.")]
	public sealed class ArtifactMetadata : ArtifactMetadataMinimum, IArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000A476 File Offset: 0x00008676
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000A47E File Offset: 0x0000867E
		public string DollarVeryUniqueSchemaProperty { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000A487 File Offset: 0x00008687
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000A48F File Offset: 0x0000868F
		public string FileName { get; set; }
	}
}
