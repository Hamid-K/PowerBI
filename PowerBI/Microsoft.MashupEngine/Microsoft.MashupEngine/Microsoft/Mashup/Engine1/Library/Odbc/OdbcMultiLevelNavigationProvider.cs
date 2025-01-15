using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Navigation;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000611 RID: 1553
	internal sealed class OdbcMultiLevelNavigationProvider : IMultiLevelNavigationProvider
	{
		// Token: 0x060030C8 RID: 12488 RVA: 0x00093D05 File Offset: 0x00091F05
		public OdbcMultiLevelNavigationProvider(OdbcQueryDomain queryDomain, OdbcTableTypes tableTypes, bool createNavigationProperties, bool supportsIncrementalNavigation, bool onlyCurrentCatalog = false)
		{
			this.queryDomain = queryDomain;
			this.tableTypes = tableTypes;
			this.createNavigationProperties = createNavigationProperties;
			this.supportsIncrementalNavigation = supportsIncrementalNavigation;
			this.cache = new Dictionary<Restriction, Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents>>();
			if (onlyCurrentCatalog)
			{
				this.supportsListingAllTables = new bool?(false);
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x00093D48 File Offset: 0x00091F48
		public bool SupportsCatalogs
		{
			get
			{
				if (this.supportsCatalogs == null)
				{
					this.supportsCatalogs = this.queryDomain.DataSource.Info.SupportsCatalogNames;
					if (this.supportsCatalogs == null)
					{
						this.FindIfSupportsCatalogAndSchema();
					}
				}
				return this.supportsCatalogs.Value;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x00093D9C File Offset: 0x00091F9C
		public bool SupportsSchemas
		{
			get
			{
				if (this.supportsSchemas == null)
				{
					this.supportsSchemas = this.queryDomain.DataSource.Info.SupportsSchemaNames;
					if (this.supportsSchemas == null)
					{
						this.FindIfSupportsCatalogAndSchema();
					}
				}
				return this.supportsSchemas.Value;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x00093DF0 File Offset: 0x00091FF0
		public bool SupportsNativeQuery
		{
			get
			{
				if (this.supportsNativeQuery == null)
				{
					this.supportsNativeQuery = new bool?(this.queryDomain.DataSource.Host.QueryService<IExtensibilityService>() == null || this.queryDomain.DataSource.Info.SupportsNativeQuery);
				}
				return this.supportsNativeQuery.Value;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x00093E4F File Offset: 0x0009204F
		public IEnumerable<string> NonParentalNames
		{
			get
			{
				return OdbcMultiLevelNavigationProvider.nonParentalNames;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x00093E58 File Offset: 0x00092058
		public IEnumerable<TableType> TableTypes
		{
			get
			{
				return this.tableTypes.Values;
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x00093E73 File Offset: 0x00092073
		public string GetQualifiedTableName(string catalog, string schema, string name)
		{
			return new OdbcIdentifier(catalog, schema, name).AsSqlReference(this.queryDomain.DataSource.Info).ToScript(this.queryDomain.DataSource.SqlSettings);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x00093EA8 File Offset: 0x000920A8
		public IValueReference CreateDataTable(string catalog, string schema, string name, TableType tableType)
		{
			OdbcIdentifier odbcIdentifier = new OdbcIdentifier(catalog, schema, name);
			OdbcTableInfo tableInfo = this.queryDomain.DataSource.GetOrCreateTableInfo(odbcIdentifier, tableType);
			return new DelayedValue(() => OdbcTableValue.New(this.queryDomain, tableInfo, this.createNavigationProperties));
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x00093EF4 File Offset: 0x000920F4
		public Value NativeQuery(Value target, TextValue query, Value parameters, Value options, string catalog = null)
		{
			if (!parameters.IsNull && ((parameters.IsList && parameters.AsList.Count > 0) || parameters.AsRecord.Count > 0))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NoParameters, parameters, null);
			}
			if (!this.SupportsNativeQuery)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NotSupported, target, null);
			}
			Value @null = Value.Null;
			if (!options.IsNull)
			{
				if (!options.AsRecord.IsEmpty)
				{
					Keys keys = options.AsRecord.Keys;
					if (!keys.Contains("EnableFolding") || keys.Length > 1)
					{
						throw ValueException.NewExpressionError<Message3>(Strings.InvalidOption(keys.ToString(), "Value.NativeQuery", "EnableFolding"), options, null);
					}
					if (keys.Contains("EnableFolding") && this.queryDomain.DataSource.Host.QueryService<IExtensibilityService>() == null)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionWithNoValidOptions("EnableFolding"), null, null);
					}
				}
				if (options.AsRecord.TryGetValue("EnableFolding", out @null) && !@null.IsLogical)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("EnableFolding"), options, null);
				}
			}
			if (catalog == null)
			{
				bool? supportsCatalogNames = this.queryDomain.DataSource.Info.SupportsCatalogNames;
				bool flag = true;
				if ((supportsCatalogNames.GetValueOrDefault() == flag) & (supportsCatalogNames != null))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NotSupported, target, null);
				}
			}
			if (@null.IsLogical && @null.AsBoolean)
			{
				return new OdbcFoldingNativeQueryTableValue("ODBC", this.queryDomain.DataSource.Host, this.queryDomain.DataSource, query.AsString, catalog, options, this.queryDomain, null).CreateOdbcQueryTableValue();
			}
			return new OdbcNativeQueryTableValue("ODBC", this.queryDomain.DataSource.Host, this.queryDomain.DataSource, query.AsString, catalog, false, null, null);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x00089667 File Offset: 0x00087867
		public ActionValue NativeStatement(Value target, TextValue query, Value parameters, Value options, string catalog = null)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.Action_NativeStatementsNotSupported, target, null);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000940D0 File Offset: 0x000922D0
		public IEnumerable<HierarchyCatalogItem> GetCatalogItems()
		{
			IList<HierarchyCatalogItem> list = null;
			if (this.queryDomain.DataSource.Info.IsDriverV3)
			{
				list = this.queryDomain.DataSource.ConnectForMetadata<List<HierarchyCatalogItem>>(delegate(IOdbcConnection connection)
				{
					List<HierarchyCatalogItem> list3;
					try
					{
						using (IDataReader tables = this.GetTables(connection, "%", string.Empty, string.Empty, string.Empty))
						{
							List<HierarchyCatalogItem> list2 = new List<HierarchyCatalogItem>();
							while (tables.Read() && !tables.IsDBNull(0))
							{
								list2.Add(new HierarchyCatalogItem(tables.GetString(0), TextValue.NewOrNull(tables.GetStringOrNull(4))));
							}
							list3 = list2;
						}
					}
					catch (OdbcException ex)
					{
						using (IHostTrace tracer = this.GetTracer("Engine/IO/Odbc/Navigation/CatalogItems", TraceEventType.Warning))
						{
							tracer.Add(ex, true);
						}
						if (!ex.IsSafe)
						{
							throw;
						}
						list3 = null;
					}
					return list3;
				});
			}
			if (list == null)
			{
				list = (from kvp in this.GetCatalogHierarchy(Restriction.Any)
					select kvp.Key).ToList<HierarchyCatalogItem>();
			}
			return list;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0009414C File Offset: 0x0009234C
		public IEnumerable<HierarchySchemaItem> GetSchemaItems(Restriction selectedCatalog)
		{
			IEnumerable<HierarchySchemaItem> enumerable = null;
			if (this.supportsIncrementalNavigation)
			{
				string selectedCatalogName = selectedCatalog.Item;
				enumerable = this.queryDomain.DataSource.ConnectForMetadata<List<HierarchySchemaItem>>(delegate(IOdbcConnection connection)
				{
					List<HierarchySchemaItem> list2;
					using (connection.UseCatalog(selectedCatalogName))
					{
						try
						{
							using (IDataReader tables = this.GetTables(connection, string.Empty, "%", string.Empty, string.Empty))
							{
								List<HierarchySchemaItem> list = new List<HierarchySchemaItem>();
								while (tables.Read() && !tables.IsDBNull(1))
								{
									list.Add(new HierarchySchemaItem(selectedCatalogName, tables.GetString(1), TextValue.NewOrNull(tables.GetStringOrNull(4))));
								}
								list2 = list;
							}
						}
						catch (OdbcException ex)
						{
							using (IHostTrace tracer = this.GetTracer("Engine/IO/Odbc/Navigation/SchemaItems", TraceEventType.Warning))
							{
								tracer.Add(ex, true);
							}
							if (!ex.IsSafe)
							{
								throw;
							}
							list2 = null;
						}
					}
					return list2;
				});
			}
			if (enumerable == null)
			{
				enumerable = this.GetCatalogHierarchy(selectedCatalog).SelectMany((KeyValuePair<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> kvp) => kvp.Value.Schemas.Keys);
			}
			return enumerable;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000941CC File Offset: 0x000923CC
		public IEnumerable<HierarchyTableItem> GetTableItems(Restriction selectedCatalog, Restriction selectedSchema)
		{
			Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> dictionary;
			if (this.cache.TryGetValue(selectedCatalog, out dictionary))
			{
				return (from schema in dictionary.Where((KeyValuePair<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> catalog) => selectedCatalog.Matches(catalog.Key)).SelectMany((KeyValuePair<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> catalog) => catalog.Value.Schemas)
					where selectedSchema.Matches(schema.Key)
					select schema).SelectMany((KeyValuePair<HierarchySchemaItem, OdbcMultiLevelNavigationProvider.SchemaContents> schema) => schema.Value.Tables);
			}
			if (this.supportsIncrementalNavigation)
			{
				return from item in this.GetAllTableItems(selectedCatalog.Item, selectedSchema.Item)
					where selectedCatalog.Matches(item.CatalogName) && selectedSchema.Matches(item.SchemaName)
					select item;
			}
			return from item in this.GetAllTableItems(selectedCatalog.Item)
				where selectedCatalog.Matches(item.CatalogName) && selectedSchema.Matches(item.SchemaName)
				select item;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000942C8 File Offset: 0x000924C8
		private IEnumerable<KeyValuePair<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents>> GetCatalogHierarchy(Restriction selectedCatalog)
		{
			Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> dictionary;
			if (this.cache.TryGetValue(selectedCatalog, out dictionary))
			{
				return dictionary.Where((KeyValuePair<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> kvp) => selectedCatalog.Matches(kvp.Key));
			}
			dictionary = this.CatalogHierarchyFromTables(this.GetTableItems(selectedCatalog, Restriction.Any));
			this.cache.Add(selectedCatalog, dictionary);
			if (!selectedCatalog.HasFilter)
			{
				foreach (KeyValuePair<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> keyValuePair in dictionary)
				{
					this.cache[keyValuePair.Key.Restriction] = new Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> { { keyValuePair.Key, keyValuePair.Value } };
				}
			}
			return dictionary;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000943AC File Offset: 0x000925AC
		private Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> CatalogHierarchyFromTables(IEnumerable<HierarchyTableItem> allTables)
		{
			Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents> dictionary = new Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents>();
			foreach (HierarchyTableItem hierarchyTableItem in allTables)
			{
				OdbcMultiLevelNavigationProvider.CatalogContents catalogContents;
				if (!dictionary.TryGetValue(hierarchyTableItem.CatalogItem, out catalogContents))
				{
					catalogContents = new OdbcMultiLevelNavigationProvider.CatalogContents();
					dictionary.Add(hierarchyTableItem.CatalogItem, catalogContents);
				}
				OdbcMultiLevelNavigationProvider.SchemaContents schemaContents;
				if (!catalogContents.Schemas.TryGetValue(hierarchyTableItem.SchemaItem, out schemaContents))
				{
					schemaContents = new OdbcMultiLevelNavigationProvider.SchemaContents();
					catalogContents.Schemas.Add(hierarchyTableItem.SchemaItem, schemaContents);
				}
				schemaContents.Tables.Add(hierarchyTableItem);
			}
			return dictionary;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x00094454 File Offset: 0x00092654
		private List<HierarchyTableItem> GetAllTableItems(string catalog)
		{
			OdbcDataSource dataSource = this.queryDomain.DataSource;
			if (!dataSource.Info.Supports(Odbc32.SQL_OIC.SQL_OIC_LEVEL2))
			{
				catalog = null;
			}
			string catalogQuery = ((this.SupportsCatalogs && catalog == null && dataSource.Info.IsDriverV3) ? "%" : null);
			return dataSource.ConnectForMetadata<List<HierarchyTableItem>>(catalog, delegate(IOdbcConnection connection)
			{
				bool? flag = this.supportsListingAllTables;
				bool flag2 = false;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					try
					{
						using (IDataReader tables = this.GetTables(connection, catalogQuery, null, null, dataSource.TableTypes.FilterString))
						{
							List<HierarchyTableItem> list;
							if (this.TryGetAllTableItems(tables, dataSource.TableTypes, out list))
							{
								return list;
							}
						}
					}
					catch (OdbcException ex)
					{
						if (!ex.IsNonTransient)
						{
							throw;
						}
					}
				}
				using (IDataReader tables2 = this.GetTables(connection, null, null, null, dataSource.TableTypes.FilterString))
				{
					List<HierarchyTableItem> list2;
					if (this.TryGetAllTableItems(tables2, dataSource.TableTypes, out list2))
					{
						return list2;
					}
				}
				return new List<HierarchyTableItem>();
			});
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000944D8 File Offset: 0x000926D8
		private List<HierarchyTableItem> GetAllTableItems(string catalog, string schema)
		{
			OdbcDataSource dataSource = this.queryDomain.DataSource;
			string catalogQuery = ((this.SupportsCatalogs && dataSource.Info.IsDriverV3) ? catalog : null);
			string schemaQuery = (this.SupportsSchemas ? schema : null);
			return dataSource.ConnectForMetadata<List<HierarchyTableItem>>(catalog, delegate(IOdbcConnection connection)
			{
				bool? flag = this.supportsListingAllTables;
				bool flag2 = false;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					try
					{
						string text = OdbcSearchPattern.EscapeSearchCharacters(dataSource.Info.SearchPatternEscapeCharacter, catalogQuery);
						string text2 = OdbcSearchPattern.EscapeSearchCharacters(dataSource.Info.SearchPatternEscapeCharacter, schemaQuery);
						using (IDataReader tables = this.GetTables(connection, text, text2, null, dataSource.TableTypes.FilterString))
						{
							List<HierarchyTableItem> list;
							if (this.TryGetAllTableItems(tables, dataSource.TableTypes, out list))
							{
								return list;
							}
						}
					}
					catch (OdbcException ex)
					{
						using (IHostTrace tracer = this.GetTracer("Engine/IO/Odbc/Navigation/TableItems", TraceEventType.Warning))
						{
							tracer.Add(ex, true);
						}
						if (ex.IsSafe)
						{
							return null;
						}
						throw;
					}
				}
				using (IDataReader tables2 = this.GetTables(connection, null, null, null, dataSource.TableTypes.FilterString))
				{
					List<HierarchyTableItem> list2;
					if (this.TryGetAllTableItems(tables2, dataSource.TableTypes, out list2))
					{
						return list2;
					}
				}
				return new List<HierarchyTableItem>();
			});
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x00094554 File Offset: 0x00092754
		private bool TryGetAllTableItems(IDataReader reader, OdbcTableTypes tableTypes, out List<HierarchyTableItem> tables)
		{
			tables = new List<HierarchyTableItem>();
			while (reader.Read())
			{
				string text = (this.SupportsCatalogs ? ((reader[0] as string) ?? string.Empty) : null);
				string text2 = (this.SupportsSchemas ? ((reader[1] as string) ?? string.Empty) : null);
				TableType tableType;
				if (tableTypes.TryGetValue((reader[3] as string) ?? string.Empty, out tableType))
				{
					object obj = reader[2];
					if (obj == DBNull.Value || obj == null)
					{
						if (this.supportsListingAllTables == null)
						{
							this.supportsListingAllTables = new bool?(false);
						}
						return false;
					}
					tables.Add(new HierarchyTableItem(text, text2, obj as string, tableType, TextValue.NewOrNull(reader[4] as string)));
				}
			}
			return true;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00094630 File Offset: 0x00092830
		private void FindIfSupportsCatalogAndSchema()
		{
			OdbcDataSource dataSource = this.queryDomain.DataSource;
			dataSource.ConnectForMetadata(delegate(IOdbcConnection connection)
			{
				using (IDataReader tables = this.GetTables(connection, "%", null, null, dataSource.TableTypes.FilterString))
				{
					this.supportsCatalogs = new bool?(dataSource.Info.SupportsCatalogNames.GetValueOrDefault());
					this.supportsSchemas = new bool?(dataSource.Info.SupportsSchemaNames.GetValueOrDefault());
					if (tables.Read())
					{
						try
						{
							object obj = tables[0];
							if (obj != DBNull.Value && obj != null && this.supportsCatalogs == null)
							{
								this.supportsCatalogs = new bool?(true);
							}
						}
						catch (OdbcException ex)
						{
							if (!ex.IsSafe)
							{
								throw;
							}
						}
						try
						{
							object obj2 = tables[1];
							if (obj2 != DBNull.Value && obj2 != null && this.supportsSchemas == null)
							{
								this.supportsSchemas = new bool?(true);
							}
						}
						catch (OdbcException ex2)
						{
							if (!ex2.IsSafe)
							{
								throw;
							}
						}
						try
						{
							object obj3 = tables[2];
							if (obj3 != DBNull.Value && obj3 != null && this.supportsListingAllTables == null)
							{
								this.supportsListingAllTables = new bool?(true);
							}
						}
						catch (OdbcException ex3)
						{
							if (!ex3.IsSafe)
							{
								throw;
							}
						}
					}
				}
			});
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x00094674 File Offset: 0x00092874
		private IDataReader GetTables(IOdbcConnection connection, string catalogName, string schemaName, string tableName, string tableType)
		{
			IDataReader tables;
			try
			{
				tables = connection.GetTables(catalogName, schemaName, tableName, tableType);
			}
			catch (OdbcException)
			{
				if (string.Equals(catalogName, "%", StringComparison.Ordinal))
				{
					string text = null;
					try
					{
						text = connection.GetConnectAttrString(Odbc32.SQL_ATTR.CURRENT_CATALOG);
					}
					catch (OdbcException)
					{
					}
					if (text != null)
					{
						return connection.GetTables(text, schemaName, tableName, tableType);
					}
				}
				throw;
			}
			return tables;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000946E0 File Offset: 0x000928E0
		public void TestConnection()
		{
			this.queryDomain.DataSource.TestConnectionAndGetVersion(null);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000946F4 File Offset: 0x000928F4
		private IHostTrace GetTracer(string entryname, TraceEventType severity)
		{
			return TracingService.CreateTrace(this.queryDomain.DataSource.Host, entryname, severity, this.queryDomain.DataSource.Resource);
		}

		// Token: 0x04001584 RID: 5508
		public const int tableCatalogOrdinal = 0;

		// Token: 0x04001585 RID: 5509
		public const int tableSchemaOrdinal = 1;

		// Token: 0x04001586 RID: 5510
		public const int tableNameOrdinal = 2;

		// Token: 0x04001587 RID: 5511
		private const int tableTypeOrdinal = 3;

		// Token: 0x04001588 RID: 5512
		private const int tableRemarksOrdinal = 4;

		// Token: 0x04001589 RID: 5513
		private const string ValueDotNativeQuery = "Value.NativeQuery";

		// Token: 0x0400158A RID: 5514
		private static readonly string[] nonParentalNames = new string[] { "" };

		// Token: 0x0400158B RID: 5515
		private readonly OdbcQueryDomain queryDomain;

		// Token: 0x0400158C RID: 5516
		private readonly bool createNavigationProperties;

		// Token: 0x0400158D RID: 5517
		private readonly bool supportsIncrementalNavigation;

		// Token: 0x0400158E RID: 5518
		private readonly Dictionary<Restriction, Dictionary<HierarchyCatalogItem, OdbcMultiLevelNavigationProvider.CatalogContents>> cache;

		// Token: 0x0400158F RID: 5519
		private readonly OdbcTableTypes tableTypes;

		// Token: 0x04001590 RID: 5520
		private bool? supportsCatalogs;

		// Token: 0x04001591 RID: 5521
		private bool? supportsSchemas;

		// Token: 0x04001592 RID: 5522
		private bool? supportsListingAllTables;

		// Token: 0x04001593 RID: 5523
		private bool? supportsNativeQuery;

		// Token: 0x02000612 RID: 1554
		private class SchemaContents
		{
			// Token: 0x04001594 RID: 5524
			public readonly List<HierarchyTableItem> Tables = new List<HierarchyTableItem>();
		}

		// Token: 0x02000613 RID: 1555
		private class CatalogContents
		{
			// Token: 0x04001595 RID: 5525
			public readonly Dictionary<HierarchySchemaItem, OdbcMultiLevelNavigationProvider.SchemaContents> Schemas = new Dictionary<HierarchySchemaItem, OdbcMultiLevelNavigationProvider.SchemaContents>();
		}
	}
}
