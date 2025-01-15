using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200008C RID: 140
	[DataContract]
	public sealed class ReportDatasetReferenceByConnection
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000A895 File Offset: 0x00008A95
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000A89D File Offset: 0x00008A9D
		[DisplayName("ConnectionString")]
		[Description("The connection string referring to the remote semantic model.")]
		[DataMember(Name = "connectionString", EmitDefaultValue = true, IsRequired = true)]
		public string ConnectionString { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000A8AE File Offset: 0x00008AAE
		[DisplayName("PbiServiceModelId")]
		[Description("The unique identifier for the target semantic model in the Microsoft Fabric service.")]
		[DataMember(Name = "pbiServiceModelId", EmitDefaultValue = true, IsRequired = true)]
		public long? PbiServiceModelId { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000A8BF File Offset: 0x00008ABF
		[DisplayName("PbiModelVirtualServerName")]
		[Description("The virtual server name for the target semantic model in the Microsoft Fabric service.")]
		[DataMember(Name = "pbiModelVirtualServerName", EmitDefaultValue = true, IsRequired = true)]
		public string PbiModelVirtualServerName { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		[DisplayName("PbiModelDatabaseName")]
		[Description("The database name for the target semantic model in the Microsoft Fabric service.")]
		[DataMember(Name = "pbiModelDatabaseName", EmitDefaultValue = true, IsRequired = true)]
		public string PbiModelDatabaseName { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000A8D9 File Offset: 0x00008AD9
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		[DisplayName("Name")]
		[Description("The name of the connection. The value of this property must be 'EntityDataSource'. This property will be removed in a future release.")]
		[DataMember(Name = "name", EmitDefaultValue = true, IsRequired = true)]
		public string Name { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000A8EA File Offset: 0x00008AEA
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000A8F2 File Offset: 0x00008AF2
		[DisplayName("ConnectionType")]
		[Description("The type of connection.")]
		[DataMember(Name = "connectionType", EmitDefaultValue = true, IsRequired = true)]
		public string ConnectionType { get; set; }
	}
}
