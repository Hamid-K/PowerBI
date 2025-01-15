using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000024 RID: 36
	[DataContract]
	public class AnalysisServicesDataSourceAnnotation : DataSourceAnnotation<AnalysisServicesDataSourceAnnotation>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002D84 File Offset: 0x00000F84
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002D8C File Offset: 0x00000F8C
		[DataMember(Name = "dataSourceAnnotationUPNMapping", Order = 10)]
		public AnalysisServicesUpnMapping UpnMapping { get; set; }
	}
}
