using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200007C RID: 124
	[DataContract]
	public class ArtifactConfigMinimum
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A3EF File Offset: 0x000085EF
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000A3F7 File Offset: 0x000085F7
		[DisplayName("LogicalId")]
		[Description("The logicalId of the item is a workspace-unique immutable identifier representing a logical item and its source control representation. There is a different logicalId in the Report and SemanticModel folder.")]
		[DataMember(Name = "logicalId", EmitDefaultValue = true, IsRequired = true)]
		public Guid LogicalId { get; set; }
	}
}
