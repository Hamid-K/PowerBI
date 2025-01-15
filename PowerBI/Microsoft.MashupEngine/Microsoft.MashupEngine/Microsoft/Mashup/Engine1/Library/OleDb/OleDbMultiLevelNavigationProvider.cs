using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Navigation;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x02000592 RID: 1426
	internal sealed class OleDbMultiLevelNavigationProvider : IMultiLevelNavigationProvider
	{
		// Token: 0x06002D10 RID: 11536 RVA: 0x000894E9 File Offset: 0x000876E9
		public OleDbMultiLevelNavigationProvider(OleDbDataSource dataSource)
		{
			this.dataSource = dataSource;
			this.cache = new Dictionary<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog>();
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x00089504 File Offset: 0x00087704
		public bool SupportsCatalogs
		{
			get
			{
				if (this.supportsCatalogs == null)
				{
					if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Catalogs, Array.Empty<int>()))
					{
						this.supportsCatalogs = new bool?(true);
					}
					else if (!this.dataSource.Info.SupportsCatalogNames)
					{
						this.supportsCatalogs = new bool?(false);
					}
					else
					{
						this.FindIfSupportsCatalogAndSchema();
					}
				}
				return this.supportsCatalogs.Value;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x0008957C File Offset: 0x0008777C
		public bool SupportsSchemas
		{
			get
			{
				if (this.supportsSchemas == null)
				{
					if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Schemata, Array.Empty<int>()))
					{
						this.supportsSchemas = new bool?(true);
					}
					else if (!this.dataSource.Info.SupportsSchemaNames)
					{
						this.supportsSchemas = new bool?(false);
					}
					else
					{
						this.FindIfSupportsCatalogAndSchema();
					}
				}
				return this.supportsSchemas.Value;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000895F1 File Offset: 0x000877F1
		public IEnumerable<string> NonParentalNames
		{
			get
			{
				return OleDbMultiLevelNavigationProvider.nonParentalNames;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000895F8 File Offset: 0x000877F8
		public IEnumerable<TableType> TableTypes
		{
			get
			{
				yield return new TableType("TABLE", "Table");
				yield return new TableType("VIEW", "View");
				yield break;
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x00089601 File Offset: 0x00087801
		public string GetQualifiedTableName(string catalog, string schema, string name)
		{
			return new OleDbIdentifier(catalog, schema, name).ToQualifiedName(this.dataSource.Info);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0008961B File Offset: 0x0008781B
		public IValueReference CreateDataTable(string catalog, string schema, string name, TableType tableType)
		{
			return new DelayedValue(delegate
			{
				DbEnvironment catalogEnvironemnt = this.dataSource.GetCatalogEnvironemnt(catalog);
				OleDbIdentifier oleDbIdentifier = new OleDbIdentifier(catalog, schema, name);
				return catalogEnvironemnt.NavigationPropertiesRecord[oleDbIdentifier.SchemaItem];
			});
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0008964E File Offset: 0x0008784E
		public Value NativeQuery(Value target, TextValue query, Value parameters, Value options, string catalog = null)
		{
			return this.dataSource.GetCatalogEnvironemnt(catalog).NativeQuery(target, query, parameters, options);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00089667 File Offset: 0x00087867
		public ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options, string catalog = null)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.Action_NativeStatementsNotSupported, target, null);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x00089678 File Offset: 0x00087878
		public IEnumerable<HierarchyCatalogItem> GetCatalogItems()
		{
			if (this.catalogs == null)
			{
				if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Catalogs, Array.Empty<int>()))
				{
					this.catalogs = this.LoadCatalogItems().ToList<HierarchyCatalogItem>();
				}
				else if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Schemata, Array.Empty<int>()))
				{
					this.catalogs = (from item in this.LoadSchemaItems(Restriction.Any)
						select item.CatalogItem).Distinct<HierarchyCatalogItem>().ToList<HierarchyCatalogItem>();
				}
				else
				{
					this.catalogs = (from item in this.LoadCatalogTableItems(Restriction.Any, Restriction.Any)
						select item.CatalogItem).Distinct<HierarchyCatalogItem>().ToList<HierarchyCatalogItem>();
				}
			}
			return this.catalogs;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0008976C File Offset: 0x0008796C
		public IEnumerable<HierarchySchemaItem> GetSchemaItems(Restriction selectedCatalog)
		{
			if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Schemata, Array.Empty<int>()))
			{
				return this.LoadSchemaItems(selectedCatalog);
			}
			return this.GetCatalogs(selectedCatalog).SelectMany((KeyValuePair<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> kvp) => kvp.Value.Schemas.Keys);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000897C8 File Offset: 0x000879C8
		public IEnumerable<HierarchyTableItem> GetTableItems(Restriction selectedCatalog, Restriction selectedSchema)
		{
			if (!this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Tables, Array.Empty<int>()))
			{
				return new HierarchyTableItem[0];
			}
			OleDbMultiLevelNavigationProvider.Catalog catalog;
			if (selectedCatalog.HasFilter && this.cache.TryGetValue(new HierarchyCatalogItem(selectedCatalog.Item, Value.Null), out catalog))
			{
				return catalog.Schemas.Where((KeyValuePair<HierarchySchemaItem, OleDbMultiLevelNavigationProvider.Schema> schema) => selectedSchema.Matches(schema.Key)).SelectMany((KeyValuePair<HierarchySchemaItem, OleDbMultiLevelNavigationProvider.Schema> schema) => schema.Value.Tables);
			}
			if (selectedCatalog.HasFilter || !this.SupportsCatalogs)
			{
				return this.LoadCatalogTableItems(selectedCatalog, selectedSchema);
			}
			return this.GetCatalogItems().SelectMany((HierarchyCatalogItem item) => this.GetTableItems(item.Restriction, selectedSchema));
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000898A8 File Offset: 0x00087AA8
		private IEnumerable<HierarchyCatalogItem> LoadCatalogItems()
		{
			return from DataRow r in this.dataSource.GetSchemaTableSafe(OleDbSchemaGuid.Catalogs, Array.Empty<object>()).Rows
				select new HierarchyCatalogItem((string)r["CATALOG_NAME"], TextValue.NewOrNull(r["DESCRIPTION"] as string));
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000898F8 File Offset: 0x00087AF8
		private IEnumerable<HierarchySchemaItem> LoadSchemaItems(Restriction selectedCatalog)
		{
			return from DataRow r in this.dataSource.GetSchemaTableSafe(OleDbSchemaGuid.Schemata, new object[] { this.ToOleDbRestriction(selectedCatalog) }).Rows
				select new HierarchySchemaItem(r["CATALOG_NAME"] as string, (string)r["SCHEMA_NAME"], Value.Null) into schema
				where selectedCatalog.Matches(schema.CatalogName)
				select schema;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x00089976 File Offset: 0x00087B76
		private IEnumerable<HierarchyTableItem> LoadCatalogTableItems(Restriction selectedCatalog, Restriction selectedSchema)
		{
			foreach (object obj in this.dataSource.GetSchemaTableSafe(OleDbSchemaGuid.Tables, new object[]
			{
				this.ToOleDbRestriction(selectedCatalog),
				this.ToOleDbRestriction(selectedSchema)
			}).Rows)
			{
				DataRow dataRow = (DataRow)obj;
				TableType tableType;
				if (this.TryLookupTableType(dataRow["TABLE_TYPE"] as string, out tableType) && selectedCatalog.Matches(dataRow["TABLE_CATALOG"] as string) && selectedSchema.Matches(dataRow["TABLE_SCHEMA"] as string))
				{
					yield return new HierarchyTableItem(dataRow["TABLE_CATALOG"] as string, dataRow["TABLE_SCHEMA"] as string, dataRow["TABLE_NAME"] as string, tableType, TextValue.NewOrNull(dataRow["DESCRIPTION"] as string));
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x00089994 File Offset: 0x00087B94
		private bool TryLookupTableType(string serverType, out TableType tableType)
		{
			tableType = this.TableTypes.FirstOrDefault((TableType t) => t.ServerType == serverType);
			return tableType != null;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000899CC File Offset: 0x00087BCC
		private IEnumerable<KeyValuePair<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog>> GetCatalogs(Restriction selectedCatalog)
		{
			if (!selectedCatalog.HasFilter)
			{
				return this.GetCatalogItems().SelectMany((HierarchyCatalogItem item) => this.GetCatalogs(item.Restriction));
			}
			if (!this.cache.ContainsKey(new HierarchyCatalogItem(selectedCatalog.Item, Value.Null)))
			{
				this.ExtendCache(this.GetCatalogs(this.LoadCatalogTableItems(selectedCatalog, Restriction.Any)));
			}
			return this.cache.Where((KeyValuePair<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> kvp) => selectedCatalog.Matches(kvp.Key));
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x00089A68 File Offset: 0x00087C68
		private void ExtendCache(Dictionary<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> catalogs)
		{
			foreach (KeyValuePair<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> keyValuePair in catalogs)
			{
				OleDbMultiLevelNavigationProvider.Catalog catalog;
				if (this.cache.TryGetValue(keyValuePair.Key, out catalog))
				{
					using (Dictionary<HierarchySchemaItem, OleDbMultiLevelNavigationProvider.Schema>.Enumerator enumerator2 = keyValuePair.Value.Schemas.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<HierarchySchemaItem, OleDbMultiLevelNavigationProvider.Schema> keyValuePair2 = enumerator2.Current;
							if (!catalog.Schemas.ContainsKey(keyValuePair2.Key))
							{
								catalog.Schemas.Add(keyValuePair2.Key, keyValuePair2.Value);
							}
						}
						continue;
					}
				}
				this.cache.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x00089B54 File Offset: 0x00087D54
		private Dictionary<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> GetCatalogs(IEnumerable<HierarchyTableItem> tables)
		{
			Dictionary<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> dictionary = new Dictionary<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog>();
			foreach (HierarchyTableItem hierarchyTableItem in tables)
			{
				OleDbMultiLevelNavigationProvider.Catalog catalog;
				if (!dictionary.TryGetValue(hierarchyTableItem.CatalogItem, out catalog))
				{
					catalog = new OleDbMultiLevelNavigationProvider.Catalog();
					dictionary.Add(hierarchyTableItem.CatalogItem, catalog);
				}
				OleDbMultiLevelNavigationProvider.Schema schema;
				if (!catalog.Schemas.TryGetValue(hierarchyTableItem.SchemaItem, out schema))
				{
					schema = new OleDbMultiLevelNavigationProvider.Schema();
					catalog.Schemas.Add(hierarchyTableItem.SchemaItem, schema);
				}
				schema.Tables.Add(hierarchyTableItem);
			}
			return dictionary;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x00089BFC File Offset: 0x00087DFC
		private void FindIfSupportsCatalogAndSchema()
		{
			if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Catalogs, Array.Empty<int>()))
			{
				this.supportsCatalogs = new bool?(this.supportsCatalogs.GetValueOrDefault(true));
			}
			if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Schemata, Array.Empty<int>()))
			{
				this.supportsSchemas = new bool?(this.supportsSchemas.GetValueOrDefault(true));
				HierarchySchemaItem hierarchySchemaItem = this.LoadSchemaItems(Restriction.Any).FirstOrDefault<HierarchySchemaItem>();
				if (hierarchySchemaItem != null)
				{
					this.supportsCatalogs = new bool?(this.supportsCatalogs ?? (hierarchySchemaItem.CatalogName != null));
					return;
				}
			}
			if (this.dataSource.Info.SupportsSchema(OleDbSchemaGuid.Tables, Array.Empty<int>()))
			{
				HierarchyTableItem hierarchyTableItem = this.LoadCatalogTableItems(Restriction.Any, Restriction.Any).FirstOrDefault<HierarchyTableItem>();
				if (hierarchyTableItem != null)
				{
					this.supportsCatalogs = new bool?(this.supportsCatalogs ?? (hierarchyTableItem.CatalogName != null));
					this.supportsSchemas = new bool?(this.supportsSchemas ?? (hierarchyTableItem.SchemaName != null));
					return;
				}
			}
			this.supportsCatalogs = new bool?(this.supportsCatalogs.GetValueOrDefault());
			this.supportsSchemas = new bool?(this.supportsSchemas.GetValueOrDefault());
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x00089D6D File Offset: 0x00087F6D
		private object ToOleDbRestriction(Restriction restriction)
		{
			if (!restriction.HasFilter)
			{
				return null;
			}
			if (restriction.Item == null)
			{
				return DBNull.Value;
			}
			return restriction.Item;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x00089D90 File Offset: 0x00087F90
		public void TestConnection()
		{
			this.dataSource.GetCatalogEnvironemnt(null).TestConnection();
		}

		// Token: 0x040013A7 RID: 5031
		private static readonly string[] nonParentalNames = new string[] { "" };

		// Token: 0x040013A8 RID: 5032
		private readonly OleDbDataSource dataSource;

		// Token: 0x040013A9 RID: 5033
		private readonly Dictionary<HierarchyCatalogItem, OleDbMultiLevelNavigationProvider.Catalog> cache;

		// Token: 0x040013AA RID: 5034
		private List<HierarchyCatalogItem> catalogs;

		// Token: 0x040013AB RID: 5035
		private bool? supportsCatalogs;

		// Token: 0x040013AC RID: 5036
		private bool? supportsSchemas;

		// Token: 0x02000593 RID: 1427
		private class Schema
		{
			// Token: 0x040013AD RID: 5037
			public readonly List<HierarchyTableItem> Tables = new List<HierarchyTableItem>();
		}

		// Token: 0x02000594 RID: 1428
		private class Catalog
		{
			// Token: 0x040013AE RID: 5038
			public readonly Dictionary<HierarchySchemaItem, OleDbMultiLevelNavigationProvider.Schema> Schemas = new Dictionary<HierarchySchemaItem, OleDbMultiLevelNavigationProvider.Schema>();
		}
	}
}
