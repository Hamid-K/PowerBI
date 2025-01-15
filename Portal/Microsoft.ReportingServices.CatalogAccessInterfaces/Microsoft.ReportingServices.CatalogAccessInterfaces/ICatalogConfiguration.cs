using System;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200000A RID: 10
	public interface ICatalogConfiguration
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AD RID: 173
		string ConnectionString { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000AE RID: 174
		string WindowsUser { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000AF RID: 175
		string WindowsDomain { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B0 RID: 176
		string WindowsPassword { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000B1 RID: 177
		bool UseImpersonation { get; }
	}
}
