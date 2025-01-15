using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200003C RID: 60
	internal sealed class ExploreHostDataSourceInfo : IDataShapingDataSourceInfo, IDataSourceInfo
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004C24 File Offset: 0x00002E24
		internal ExploreHostDataSourceInfo(string name, string extension, string connectionString, string databaseName, string cubeName, ModelLocation modelLocation)
		{
			this.Name = name;
			this.Extension = extension;
			this.ConnectionString = connectionString;
			this.DatabaseName = databaseName;
			this.CubeName = cubeName;
			this.ModelLocation = modelLocation;
			this.Category = modelLocation.ToString();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004C77 File Offset: 0x00002E77
		public string Name { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004C7F File Offset: 0x00002E7F
		public string Extension { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004C87 File Offset: 0x00002E87
		public string ConnectionString { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004C8F File Offset: 0x00002E8F
		public string Category { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004C97 File Offset: 0x00002E97
		public string CubeName { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004C9F File Offset: 0x00002E9F
		public string DatabaseName { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004CA7 File Offset: 0x00002EA7
		public ModelLocation ModelLocation { get; }
	}
}
