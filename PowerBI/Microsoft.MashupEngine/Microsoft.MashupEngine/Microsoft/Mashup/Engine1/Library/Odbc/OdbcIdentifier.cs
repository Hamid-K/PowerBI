using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005FA RID: 1530
	internal sealed class OdbcIdentifier : DbIdentifier
	{
		// Token: 0x06003049 RID: 12361 RVA: 0x00088CB4 File Offset: 0x00086EB4
		public OdbcIdentifier(string catalog, string schema, string name)
			: base(catalog, schema, name)
		{
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x00092201 File Offset: 0x00090401
		public OdbcIdentifier(TableReference tableReference)
			: base(tableReference.Catalog.Name, tableReference.Schema.Name, tableReference.Name.Name)
		{
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0009222C File Offset: 0x0009042C
		public TableReference AsSqlReference(OdbcDataSourceInfo info)
		{
			if (this.sqlReference == null)
			{
				this.sqlReference = new TableReference(info.UseSchemaInDmlStatements ? this.NewAlias(base.Schema) : null, this.NewAlias(base.Name), info.UseCatalogInDmlStatements ? this.NewAlias(base.Catalog) : null);
			}
			return this.sqlReference;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x00088D2B File Offset: 0x00086F2B
		private Alias NewAlias(string name)
		{
			if (name == null)
			{
				return null;
			}
			return Alias.NewNativeAlias(name);
		}

		// Token: 0x04001539 RID: 5433
		private TableReference sqlReference;
	}
}
