using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200007B RID: 123
	[DisplayName("ItemConfig")]
	[Description("The ItemConfig stored as item.config.json holds the LogicalId of the item and identifies this directory as a Fabric item. There is an ItemConfig file in both the Report and SemanticModel folders. This file is optional.")]
	[DataContract]
	public sealed class ArtifactConfig : ArtifactConfigMinimum, IArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000A3A2 File Offset: 0x000085A2
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000A3B4 File Offset: 0x000085B4
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000A3BC File Offset: 0x000085BC
		[DisplayName("Version")]
		[Description("The version of this item. The current latest version is 1.0")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(ArtifactConfig.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000A3C5 File Offset: 0x000085C5
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000A3CD File Offset: 0x000085CD
		public string DollarVeryUniqueSchemaProperty { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000A3D6 File Offset: 0x000085D6
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000A3DE File Offset: 0x000085DE
		public string FileName { get; set; }

		// Token: 0x0200010A RID: 266
		private enum ArtifactVersions
		{
			// Token: 0x0400048C RID: 1164
			[EnumMember(Value = "1.0")]
			Version1_0
		}
	}
}
