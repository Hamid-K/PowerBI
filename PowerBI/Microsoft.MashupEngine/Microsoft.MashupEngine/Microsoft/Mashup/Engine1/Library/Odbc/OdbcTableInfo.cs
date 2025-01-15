using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000669 RID: 1641
	internal class OdbcTableInfo
	{
		// Token: 0x060033BA RID: 13242 RVA: 0x000A56A0 File Offset: 0x000A38A0
		public OdbcTableInfo(IEngineHost host, OdbcDataSource dataSource, OdbcIdentifier identifier, TableType tableType)
		{
			this.host = host;
			this.dataSource = dataSource;
			this.reference = identifier;
			this.columns = new OdbcColumnInfoCollection(dataSource, identifier, host);
			this.tableType = tableType;
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000A56D3 File Offset: 0x000A38D3
		public OdbcIdentifier Identifier
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000A56DB File Offset: 0x000A38DB
		public OdbcColumnInfoCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000A56E3 File Offset: 0x000A38E3
		public int[] PrimaryKey
		{
			get
			{
				if (this.primaryKey == null)
				{
					this.primaryKey = this.dataSource.ConnectForMetadata<int[]>(delegate(IOdbcConnection connection)
					{
						TableType tableType = this.tableType;
						if ((tableType != null && !tableType.HasPrimaryKey) || !connection.GetFunctions(Odbc32.SQL_API.SQL_API_SQLPRIMARYKEYS))
						{
							return EmptyArray<int>.Instance;
						}
						return this.GetColumns(new Func<string, string, string, IDataReader>(connection.GetPrimaryKeys), "PrimaryKeys", 3);
					});
				}
				return this.primaryKey;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000A5710 File Offset: 0x000A3910
		public int[] RowId
		{
			get
			{
				if (this.rowId == null)
				{
					this.rowId = this.dataSource.ConnectForMetadata<int[]>(delegate(IOdbcConnection connection)
					{
						try
						{
							if (!connection.GetFunctions(Odbc32.SQL_API.SQL_API_SQLSPECIALCOLUMNS))
							{
								return EmptyArray<int>.Instance;
							}
						}
						catch (OdbcException ex)
						{
							if (ex.IsNonTransient)
							{
								return EmptyArray<int>.Instance;
							}
							throw;
						}
						return this.GetColumns(new Func<string, string, string, IDataReader>(connection.GetBestRowId), "SpecialColumns", 1);
					});
				}
				return this.rowId;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000A5740 File Offset: 0x000A3940
		public string UniqueIdentifier
		{
			get
			{
				if (this.relationshipIdentity == null)
				{
					this.relationshipIdentity = OdbcTableInfo.IdentityBase.Qualify(this.dataSource.UniqueIdentifier, this.Identifier.Catalog, this.Identifier.Schema, this.Identifier.Name);
				}
				return this.relationshipIdentity;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000A579C File Offset: 0x000A399C
		public IEnumerable<OdbcRelationship> GetReferencingTables()
		{
			return this.GetRelationships(this.Identifier.Catalog, this.Identifier.Schema, this.Identifier.Name, null, null, null, 4, 5, 6, 3, 7);
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000A57D8 File Offset: 0x000A39D8
		public IEnumerable<OdbcRelationship> GetReferencedTables()
		{
			return this.GetRelationships(null, null, null, this.Identifier.Catalog, this.Identifier.Schema, this.Identifier.Name, 0, 1, 2, 7, 3);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000A5814 File Offset: 0x000A3A14
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000A581D File Offset: 0x000A3A1D
		public bool Equals(OdbcTableInfo other)
		{
			return other != null && other.Identifier.Equals(this.Identifier);
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000A5835 File Offset: 0x000A3A35
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode();
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000A5844 File Offset: 0x000A3A44
		private int[] GetColumns(Func<string, string, string, IDataReader> getReader, string functionName, int columnNameOrdinal)
		{
			List<int> list = new List<int>();
			int[] array;
			try
			{
				using (IDataReader dataReader = getReader(this.reference.Catalog, this.reference.Schema, this.reference.Name))
				{
					while (dataReader.Read())
					{
						string text = (string)dataReader[columnNameOrdinal];
						OdbcColumnInfo odbcColumnInfo;
						if (!this.Columns.TryGetColumnInfo(text, out odbcColumnInfo))
						{
							using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, string.Format(CultureInfo.InvariantCulture, "Engine/IO/Odbc/OdbcTableInfo/{0}/NonExistentColumn", functionName), TraceEventType.Warning, this.dataSource.Resource))
							{
								hostTrace.Add("Message", "A non-existent column name was returned. Ignoring.", false);
								hostTrace.Add("ColumnName", text, true);
							}
							return EmptyArray<int>.Instance;
						}
						list.Add(odbcColumnInfo.Ordinal);
					}
					array = list.ToArray();
				}
			}
			catch (OdbcException ex)
			{
				if (!ex.IsNonTransient)
				{
					throw;
				}
				array = EmptyArray<int>.Instance;
			}
			return array;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000A5968 File Offset: 0x000A3B68
		private IEnumerable<OdbcRelationship> GetRelationships(string pkCatalog, string pkSchema, string pkTable, string fkCatalog, string fkSchema, string fkTable, int otherCatalogIndex, int otherSchemaIndex, int otherTableIndex, int sourceColumnIndex, int targetColumnIndex)
		{
			OdbcDataSourceInfo dataSourceInfo = this.dataSource.Info;
			return this.dataSource.ConnectForMetadata<IEnumerable<OdbcRelationship>>(delegate(IOdbcConnection connection)
			{
				TableType tableType = this.tableType;
				if ((tableType == null || tableType.HasForeignKeys) && connection.GetFunctions(Odbc32.SQL_API.SQL_API_SQLFOREIGNKEYS))
				{
					try
					{
						using (IDataReader foreignKeys = connection.GetForeignKeys(pkCatalog, pkSchema, pkTable, fkCatalog, fkSchema, fkTable))
						{
							List<OdbcRelationship> list = new List<OdbcRelationship>();
							OdbcIdentifier odbcIdentifier = null;
							List<string> list2 = new List<string>();
							List<string> list3 = new List<string>();
							int num = -1;
							while (foreignKeys.Read())
							{
								if (!foreignKeys.IsDBNull(otherTableIndex) && !foreignKeys.IsDBNull(sourceColumnIndex) && !foreignKeys.IsDBNull(targetColumnIndex))
								{
									bool? supportsCatalogNames = dataSourceInfo.SupportsCatalogNames;
									bool flag = false;
									OdbcIdentifier odbcIdentifier2 = new OdbcIdentifier((!((supportsCatalogNames.GetValueOrDefault() == flag) & (supportsCatalogNames != null))) ? (foreignKeys[otherCatalogIndex] as string) : null, foreignKeys[otherSchemaIndex] as string, foreignKeys.GetString(otherTableIndex));
									int num2 = Convert.ToInt32(foreignKeys[8], CultureInfo.InvariantCulture);
									if ((odbcIdentifier != null && !odbcIdentifier2.Equals(odbcIdentifier)) || num2 <= num)
									{
										list.Add(new OdbcRelationship(Keys.New(list3.ToArray()), odbcIdentifier, Keys.New(list2.ToArray())));
										list2.Clear();
										list3.Clear();
									}
									odbcIdentifier = odbcIdentifier2;
									list3.Add(foreignKeys.GetString(sourceColumnIndex));
									list2.Add(foreignKeys.GetString(targetColumnIndex));
									num = num2;
								}
							}
							if (odbcIdentifier != null)
							{
								list.Add(new OdbcRelationship(Keys.New(list3.ToArray()), odbcIdentifier, Keys.New(list2.ToArray())));
							}
							return list;
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
				return EmptyArray<OdbcRelationship>.Instance;
			});
		}

		// Token: 0x04001705 RID: 5893
		private static readonly PersistentCacheKey IdentityBase = new PersistentCacheKey("Odbc.DataSource/1");

		// Token: 0x04001706 RID: 5894
		private readonly IEngineHost host;

		// Token: 0x04001707 RID: 5895
		private readonly OdbcDataSource dataSource;

		// Token: 0x04001708 RID: 5896
		private readonly OdbcColumnInfoCollection columns;

		// Token: 0x04001709 RID: 5897
		private readonly OdbcIdentifier reference;

		// Token: 0x0400170A RID: 5898
		private readonly TableType tableType;

		// Token: 0x0400170B RID: 5899
		private int[] primaryKey;

		// Token: 0x0400170C RID: 5900
		private int[] rowId;

		// Token: 0x0400170D RID: 5901
		private string relationshipIdentity;
	}
}
