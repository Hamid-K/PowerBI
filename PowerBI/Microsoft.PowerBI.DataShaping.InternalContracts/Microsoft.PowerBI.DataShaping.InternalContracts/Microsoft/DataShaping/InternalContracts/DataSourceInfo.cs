using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200000D RID: 13
	internal sealed class DataSourceInfo : IDataShapingDataSourceInfo, IDataSourceInfo
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002298 File Offset: 0x00000498
		internal DataSourceInfo(string name, string extension, string connectionString, string category)
		{
			this.Name = name;
			this.Extension = extension;
			this.ConnectionString = connectionString;
			this.Category = category;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022BD File Offset: 0x000004BD
		public string Name { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C5 File Offset: 0x000004C5
		public string Extension { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022CD File Offset: 0x000004CD
		public string ConnectionString { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022D5 File Offset: 0x000004D5
		public string Category { get; }
	}
}
