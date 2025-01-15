using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000062 RID: 98
	internal sealed class DataSource
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000B791 File Offset: 0x00009991
		internal DataSource(string name, string dataSourceReference)
		{
			this._name = name;
			this._dataSourceReference = dataSourceReference;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000B7A7 File Offset: 0x000099A7
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000B7AF File Offset: 0x000099AF
		public string DataSourceReference
		{
			get
			{
				return this._dataSourceReference;
			}
		}

		// Token: 0x04000169 RID: 361
		private readonly string _name;

		// Token: 0x0400016A RID: 362
		private readonly string _dataSourceReference;
	}
}
