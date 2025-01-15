using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001174 RID: 4468
	public interface IMultiLevelNavigationProvider
	{
		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x06007524 RID: 29988
		bool SupportsCatalogs { get; }

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x06007525 RID: 29989
		bool SupportsSchemas { get; }

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x06007526 RID: 29990
		IEnumerable<string> NonParentalNames { get; }

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x06007527 RID: 29991
		IEnumerable<TableType> TableTypes { get; }

		// Token: 0x06007528 RID: 29992
		string GetQualifiedTableName(string catalog, string schema, string name);

		// Token: 0x06007529 RID: 29993
		IValueReference CreateDataTable(string catalog, string schema, string name, TableType tableType);

		// Token: 0x0600752A RID: 29994
		Value NativeQuery(Value target, TextValue query, Value parameters, Value options, string catalog = null);

		// Token: 0x0600752B RID: 29995
		ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options, string catalog = null);

		// Token: 0x0600752C RID: 29996
		IEnumerable<HierarchyCatalogItem> GetCatalogItems();

		// Token: 0x0600752D RID: 29997
		IEnumerable<HierarchySchemaItem> GetSchemaItems(Restriction selectedCatalog);

		// Token: 0x0600752E RID: 29998
		IEnumerable<HierarchyTableItem> GetTableItems(Restriction selectedCatalog, Restriction selectedSchema);

		// Token: 0x0600752F RID: 29999
		void TestConnection();
	}
}
