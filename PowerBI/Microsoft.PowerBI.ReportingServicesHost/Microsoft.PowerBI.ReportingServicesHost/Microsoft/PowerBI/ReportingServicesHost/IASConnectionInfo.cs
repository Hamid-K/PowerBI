using System;
using System.Security;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200003D RID: 61
	internal interface IASConnectionInfo
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000153 RID: 339
		string CubeName { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340
		string DatabaseName { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000155 RID: 341
		string DatabaseID { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000156 RID: 342
		string ModelMetadataCatalogName { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000157 RID: 343
		ModelLocation ModelLocation { get; }

		// Token: 0x06000158 RID: 344
		string GetConnectionString();

		// Token: 0x06000159 RID: 345
		SecureString GetSecureConnectionString();
	}
}
