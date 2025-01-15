using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200007D RID: 125
	[DisplayName("ItemDetails")]
	[Description("Holds the LogicalId of the item and identifies this directory as a Fabric item.")]
	[DataContract]
	public sealed class ArtifactDetailsConfig : ArtifactConfigMinimum
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A408 File Offset: 0x00008608
		public static Version[] SupportedVersions
		{
			get
			{
				return new Version[]
				{
					new Version(2, 0)
				};
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000A41A File Offset: 0x0000861A
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000A422 File Offset: 0x00008622
		[DisplayName("Version")]
		[Description("The version of this item. The current latest version is 2.0")]
		[DataMember(Name = "version", EmitDefaultValue = true, IsRequired = true)]
		[EnumDataType(typeof(ArtifactDetailsConfig.ArtifactVersions))]
		public string Version { get; set; }

		// Token: 0x0200010B RID: 267
		private enum ArtifactVersions
		{
			// Token: 0x0400048E RID: 1166
			[EnumMember(Value = "2.0")]
			Version2_0
		}
	}
}
