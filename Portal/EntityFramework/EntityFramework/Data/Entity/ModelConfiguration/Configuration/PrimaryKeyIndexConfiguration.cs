using System;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Index;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E6 RID: 486
	public class PrimaryKeyIndexConfiguration
	{
		// Token: 0x060019AA RID: 6570 RVA: 0x000459B4 File Offset: 0x00043BB4
		internal PrimaryKeyIndexConfiguration(IndexConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000459C3 File Offset: 0x00043BC3
		public PrimaryKeyIndexConfiguration IsClustered()
		{
			return this.IsClustered(true);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000459CC File Offset: 0x00043BCC
		public PrimaryKeyIndexConfiguration IsClustered(bool clustered)
		{
			this._configuration.IsClustered = new bool?(clustered);
			return this;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000459E0 File Offset: 0x00043BE0
		public PrimaryKeyIndexConfiguration HasName(string name)
		{
			this._configuration.Name = name;
			return this;
		}

		// Token: 0x04000A82 RID: 2690
		private readonly IndexConfiguration _configuration;
	}
}
