using System;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Index;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E5 RID: 485
	public class IndexConfiguration
	{
		// Token: 0x060019A4 RID: 6564 RVA: 0x0004595C File Offset: 0x00043B5C
		internal IndexConfiguration(IndexConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0004596B File Offset: 0x00043B6B
		public IndexConfiguration IsUnique()
		{
			return this.IsUnique(true);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00045974 File Offset: 0x00043B74
		public IndexConfiguration IsUnique(bool unique)
		{
			this._configuration.IsUnique = new bool?(unique);
			return this;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00045988 File Offset: 0x00043B88
		public IndexConfiguration IsClustered()
		{
			return this.IsClustered(true);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00045991 File Offset: 0x00043B91
		public IndexConfiguration IsClustered(bool clustered)
		{
			this._configuration.IsClustered = new bool?(clustered);
			return this;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000459A5 File Offset: 0x00043BA5
		public IndexConfiguration HasName(string name)
		{
			this._configuration.Name = name;
			return this;
		}

		// Token: 0x04000A81 RID: 2689
		private readonly IndexConfiguration _configuration;
	}
}
