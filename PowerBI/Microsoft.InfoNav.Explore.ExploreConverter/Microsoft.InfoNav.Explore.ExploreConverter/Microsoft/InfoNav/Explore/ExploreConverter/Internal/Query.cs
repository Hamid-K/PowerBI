using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000071 RID: 113
	internal sealed class Query
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000BA51 File Offset: 0x00009C51
		internal Query(string dataSourceName)
		{
			this._dataSourceName = dataSourceName;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000BA60 File Offset: 0x00009C60
		public string DataSourceName
		{
			get
			{
				return this._dataSourceName;
			}
		}

		// Token: 0x04000185 RID: 389
		private readonly string _dataSourceName;
	}
}
