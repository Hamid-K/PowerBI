using System;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D2 RID: 466
	public abstract class ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x00042033 File Offset: 0x00040233
		internal ConventionModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00042046 File Offset: 0x00040246
		internal ModificationStoredProcedureConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x04000A64 RID: 2660
		private readonly ModificationStoredProcedureConfiguration _configuration = new ModificationStoredProcedureConfiguration();
	}
}
