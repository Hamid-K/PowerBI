using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005B9 RID: 1465
	internal class OdbcCacheContext
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x0008C3D0 File Offset: 0x0008A5D0
		public OdbcCacheContext(string connectionString, string catalog, ResourceCredentialCollection credentials)
		{
			this.connectionString = connectionString;
			this.catalog = catalog;
			this.credentials = credentials;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0008C3ED File Offset: 0x0008A5ED
		public OdbcCacheContext WithCatalog(string catalog)
		{
			if (catalog == this.catalog)
			{
				return this;
			}
			return new OdbcCacheContext(this.connectionString, catalog, this.credentials);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0008C414 File Offset: 0x0008A614
		public StructuredCacheKey GetStructuredCacheKey(params string[] parts)
		{
			string[] array = new string[parts.Length + 3];
			array[0] = "OdbcCacheContext/7";
			array[1] = this.connectionString;
			array[2] = this.catalog;
			for (int i = 0; i < parts.Length; i++)
			{
				array[3 + i] = parts[i];
			}
			return new StructuredCacheKey(this.credentials, array);
		}

		// Token: 0x04001429 RID: 5161
		private readonly string connectionString;

		// Token: 0x0400142A RID: 5162
		private readonly string catalog;

		// Token: 0x0400142B RID: 5163
		private readonly ResourceCredentialCollection credentials;
	}
}
