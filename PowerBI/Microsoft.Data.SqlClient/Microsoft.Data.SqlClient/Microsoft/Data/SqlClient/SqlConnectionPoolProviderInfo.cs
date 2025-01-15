using System;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200006B RID: 107
	internal sealed class SqlConnectionPoolProviderInfo : DbConnectionPoolProviderInfo
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000181B8 File Offset: 0x000163B8
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x000181C0 File Offset: 0x000163C0
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
			set
			{
				this._instanceName = value;
			}
		}

		// Token: 0x0400019A RID: 410
		private string _instanceName;
	}
}
