using System;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000118 RID: 280
	internal interface IInternalConnection : IDisposable
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001366 RID: 4966
		DbConnection Connection { get; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001367 RID: 4967
		string ConnectionKey { get; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001368 RID: 4968
		bool ConnectionHasModel { get; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001369 RID: 4969
		DbConnectionStringOrigin ConnectionStringOrigin { get; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600136A RID: 4970
		// (set) Token: 0x0600136B RID: 4971
		AppConfig AppConfig { get; set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600136C RID: 4972
		// (set) Token: 0x0600136D RID: 4973
		string ProviderName { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600136E RID: 4974
		string ConnectionStringName { get; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600136F RID: 4975
		string OriginalConnectionString { get; }

		// Token: 0x06001370 RID: 4976
		ObjectContext CreateObjectContextFromConnectionModel();
	}
}
