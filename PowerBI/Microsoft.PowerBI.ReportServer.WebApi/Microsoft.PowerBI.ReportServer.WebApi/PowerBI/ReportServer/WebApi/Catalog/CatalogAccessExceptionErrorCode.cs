using System;

namespace Microsoft.PowerBI.ReportServer.WebApi.Catalog
{
	// Token: 0x0200003D RID: 61
	public enum CatalogAccessExceptionErrorCode
	{
		// Token: 0x040000B1 RID: 177
		General,
		// Token: 0x040000B2 RID: 178
		StoredCredentialsIncorrect,
		// Token: 0x040000B3 RID: 179
		CannotRetrievePBIX,
		// Token: 0x040000B4 RID: 180
		CannotRetrieveDataSource,
		// Token: 0x040000B5 RID: 181
		StoredConnectionStringIncorrect,
		// Token: 0x040000B6 RID: 182
		UnsupportedCredentialsType
	}
}
