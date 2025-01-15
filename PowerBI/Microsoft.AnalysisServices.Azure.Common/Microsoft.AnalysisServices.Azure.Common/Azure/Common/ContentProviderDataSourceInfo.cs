using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000053 RID: 83
	[DataContract]
	public sealed class ContentProviderDataSourceInfo
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000FE35 File Offset: 0x0000E035
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000FE3D File Offset: 0x0000E03D
		[DataMember(Name = "serverName")]
		public string ServerName { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000FE46 File Offset: 0x0000E046
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000FE4E File Offset: 0x0000E04E
		[DataMember(Name = "databaseName")]
		public string DatabaseName { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000FE57 File Offset: 0x0000E057
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000FE5F File Offset: 0x0000E05F
		[DataMember(Name = "userName")]
		public string UserName { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000FE68 File Offset: 0x0000E068
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000FE70 File Offset: 0x0000E070
		[DataMember(Name = "password")]
		public string Password { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000FE79 File Offset: 0x0000E079
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000FE81 File Offset: 0x0000E081
		[DataMember(Name = "isLiveConnectEnabled")]
		public bool IsLiveConnectEnabled { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000FE8A File Offset: 0x0000E08A
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000FE92 File Offset: 0x0000E092
		[DataMember(Name = "sqlStatement")]
		public string SQLStatement { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000FE9B File Offset: 0x0000E09B
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000FEA3 File Offset: 0x0000E0A3
		[DataMember(Name = "refreshFrequencyInMinutes")]
		public int RefreshFrequencyInMinutes { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		[DataMember(Name = "port")]
		public string Port { get; set; }
	}
}
