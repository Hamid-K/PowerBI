using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200004F RID: 79
	[DataContract]
	public sealed class RemoteArtifactPropertiesStorage
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00007644 File Offset: 0x00005844
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000764C File Offset: 0x0000584C
		[DataMember(Name = "DatasetId", IsRequired = true, Order = 10)]
		public string DatasetId { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00007655 File Offset: 0x00005855
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000765D File Offset: 0x0000585D
		[DataMember(Name = "ReportId", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string ReportId { get; set; }
	}
}
