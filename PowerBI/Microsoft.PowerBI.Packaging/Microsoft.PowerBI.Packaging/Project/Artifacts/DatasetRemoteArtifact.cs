using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x02000086 RID: 134
	[DataContract]
	public sealed class DatasetRemoteArtifact
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000A6F1 File Offset: 0x000088F1
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000A6F9 File Offset: 0x000088F9
		[DisplayName("DatasetId")]
		[Description("The ID of a published semantic model created from this definition.")]
		[DataMember(Name = "datasetId", EmitDefaultValue = true, IsRequired = true)]
		public string DatasetId { get; set; }
	}
}
