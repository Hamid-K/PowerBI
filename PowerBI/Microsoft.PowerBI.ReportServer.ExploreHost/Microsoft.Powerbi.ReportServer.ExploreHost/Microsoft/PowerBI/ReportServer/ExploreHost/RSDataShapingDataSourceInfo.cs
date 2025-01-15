using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000009 RID: 9
	internal sealed class RSDataShapingDataSourceInfo : IDataShapingDataSourceInfo, IDataSourceInfo
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002806 File Offset: 0x00000A06
		internal RSDataShapingDataSourceInfo(string name, string extension, string connectionString, ConnectionSecurity connectionSecurity, string category, string userName)
		{
			this.Name = name;
			this.Extension = extension;
			this.ConnectionString = connectionString;
			this.ConnectionSecurity = connectionSecurity;
			this.Category = category;
			this.UserName = userName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000283B File Offset: 0x00000A3B
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002843 File Offset: 0x00000A43
		public string UserName { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000284C File Offset: 0x00000A4C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002854 File Offset: 0x00000A54
		public ConnectionSecurity ConnectionSecurity { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000285D File Offset: 0x00000A5D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002865 File Offset: 0x00000A65
		public string Name { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000286E File Offset: 0x00000A6E
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002876 File Offset: 0x00000A76
		public string Extension { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000287F File Offset: 0x00000A7F
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002887 File Offset: 0x00000A87
		public string ConnectionString { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002890 File Offset: 0x00000A90
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002898 File Offset: 0x00000A98
		public string Category { get; private set; }
	}
}
