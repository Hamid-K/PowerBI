using System;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200041B RID: 1051
	internal class PermissionsAwareMetadataQuery : MetadataQueryBase
	{
		// Token: 0x060023E0 RID: 9184 RVA: 0x000652B0 File Offset: 0x000634B0
		public PermissionsAwareMetadataQuery(MetadataQuery undocumentedQuery, MetadataQuery permissionsAwareQuery)
		{
			this.undocumentedQuery = undocumentedQuery;
			this.permissionsAwareQuery = permissionsAwareQuery;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000652C6 File Offset: 0x000634C6
		public override string[] GetColumnNames(SapHanaOdbcDataSource dataSource)
		{
			if (dataSource.SapHanaFlavor != SapHanaFlavor.Datasphere)
			{
				return this.undocumentedQuery.GetColumnNames(dataSource);
			}
			return this.permissionsAwareQuery.GetColumnNames(dataSource);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000652EA File Offset: 0x000634EA
		public override string GetQuery(SapHanaOdbcDataSource dataSource)
		{
			if (dataSource.SapHanaFlavor != SapHanaFlavor.Datasphere)
			{
				return this.undocumentedQuery.GetQuery(dataSource);
			}
			return this.permissionsAwareQuery.GetQuery(dataSource);
		}

		// Token: 0x04000E69 RID: 3689
		private readonly MetadataQuery undocumentedQuery;

		// Token: 0x04000E6A RID: 3690
		private readonly MetadataQuery permissionsAwareQuery;
	}
}
