using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200007F RID: 127
	[DisplayName("ItemDetails")]
	[Description("The ItemDetails stored as '.platform' identifies this directory as a Fabric item. There is a file in both the Report and SemanticModel folders. This file is optional.")]
	[DataContract]
	public sealed class ArtifactDetails : ArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000A43B File Offset: 0x0000863B
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000A443 File Offset: 0x00008643
		public string FileName { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000A44C File Offset: 0x0000864C
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000A454 File Offset: 0x00008654
		[DisplayName("Metadata")]
		[DataMember(Name = "metadata", EmitDefaultValue = false, IsRequired = false)]
		public ArtifactDetailsMetadata Metadata { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000A45D File Offset: 0x0000865D
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000A465 File Offset: 0x00008665
		[DisplayName("Config")]
		[DataMember(Name = "config", EmitDefaultValue = false, IsRequired = false)]
		public ArtifactDetailsConfig Config { get; set; }

		// Token: 0x040001E0 RID: 480
		public const string SchemaV2Uri = "https://developer.microsoft.com/json-schemas/fabric/gitIntegration/platformProperties/2.0.0/schema.json";
	}
}
