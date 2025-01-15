using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200007E RID: 126
	[DisplayName("ItemMetadata")]
	[Description("Holds the Type and DisplayName of the item. The DisplayName is what is shown in the title bar of Power BI Desktop.")]
	[DataContract]
	public sealed class ArtifactDetailsMetadata : ArtifactMetadataMinimum
	{
	}
}
