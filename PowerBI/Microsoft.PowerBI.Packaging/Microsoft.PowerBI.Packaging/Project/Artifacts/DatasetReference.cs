using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200008A RID: 138
	[DataContract]
	public sealed class DatasetReference
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000A852 File Offset: 0x00008A52
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000A85A File Offset: 0x00008A5A
		[DisplayName("ByPath")]
		[Description("Provides a reference to a local dataset artifact definition.")]
		[DataMember(Name = "byPath", EmitDefaultValue = true, IsRequired = false)]
		public ReportDatasetReferenceByPath ByPath { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000A863 File Offset: 0x00008A63
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000A86B File Offset: 0x00008A6B
		[DisplayName("ByConnection")]
		[Description("Provides a reference to a remote semantic model using a connection string. The connection string must point to a semantic model hosted in the Microsoft Fabric service.  Connections to other Analysis Services semantic models must use a byPath reference to a semantic model definition containing a modelReference.json file.")]
		[DataMember(Name = "byConnection", EmitDefaultValue = true, IsRequired = false)]
		public ReportDatasetReferenceByConnection ByConnection { get; set; }
	}
}
