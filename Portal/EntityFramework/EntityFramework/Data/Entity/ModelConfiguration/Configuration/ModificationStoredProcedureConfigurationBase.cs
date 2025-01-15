using System;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001DA RID: 474
	public abstract class ModificationStoredProcedureConfigurationBase
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x00043370 File Offset: 0x00041570
		internal ModificationStoredProcedureConfigurationBase()
		{
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x00043383 File Offset: 0x00041583
		internal ModificationStoredProcedureConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x04000A6F RID: 2671
		private readonly ModificationStoredProcedureConfiguration _configuration = new ModificationStoredProcedureConfiguration();
	}
}
