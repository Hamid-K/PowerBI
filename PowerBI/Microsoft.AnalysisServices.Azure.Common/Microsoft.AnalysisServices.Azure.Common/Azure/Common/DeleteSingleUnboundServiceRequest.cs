using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000058 RID: 88
	[DataContract]
	public class DeleteSingleUnboundServiceRequest : IFabricIntegratorProvisioningServiceUnboundServiceRoutable
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001001D File Offset: 0x0000E21D
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00010025 File Offset: 0x0000E225
		[DataMember]
		public DatabaseType DatabaseType { get; set; }
	}
}
