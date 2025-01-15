using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000023 RID: 35
	[DataContract]
	public class ExportToExcelRequest
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000034C1 File Offset: 0x000016C1
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000034C9 File Offset: 0x000016C9
		[DataMember(IsRequired = true, Name = "exportDataType")]
		public ExportDataType ExportDataType { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000034D2 File Offset: 0x000016D2
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000034DA File Offset: 0x000016DA
		[DataMember(IsRequired = true, Name = "executeSemanticQueryRequest")]
		public QueryDataRequest ExecuteSemanticQueryRequest { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000034E3 File Offset: 0x000016E3
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000034EB File Offset: 0x000016EB
		[DataMember(IsRequired = false, Name = "artifactId")]
		public long? ArtifactId { get; set; }
	}
}
