using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005CC RID: 1484
	internal class OdbcColumnInfoCollection : IEnumerable<OdbcColumnInfo>, IEnumerable
	{
		// Token: 0x06002E66 RID: 11878 RVA: 0x0008D595 File Offset: 0x0008B795
		public OdbcColumnInfoCollection(OdbcDataSource dataSource, OdbcIdentifier tableIdentifier, IEngineHost host = null)
		{
			this.dataSource = dataSource;
			this.tableIdentifier = tableIdentifier;
			this.host = host;
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x0008D5B2 File Offset: 0x0008B7B2
		public int Count
		{
			get
			{
				this.EnsureInitialized();
				return this.columns.Count;
			}
		}

		// Token: 0x170010FE RID: 4350
		public OdbcColumnInfo this[string name]
		{
			get
			{
				this.EnsureInitialized();
				return this.columnsByName[name];
			}
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x0008D5D9 File Offset: 0x0008B7D9
		public bool TryGetColumnInfo(string name, out OdbcColumnInfo columnInfo)
		{
			this.EnsureInitialized();
			return this.columnsByName.TryGetValue(name, out columnInfo);
		}

		// Token: 0x170010FF RID: 4351
		public OdbcColumnInfo this[int ordinal]
		{
			get
			{
				this.EnsureInitialized();
				return this.columns[ordinal];
			}
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x0008D602 File Offset: 0x0008B802
		public IEnumerator<OdbcColumnInfo> GetEnumerator()
		{
			this.EnsureInitialized();
			return this.columns.GetEnumerator();
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0008D615 File Offset: 0x0008B815
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x0008D61D File Offset: 0x0008B81D
		private void EnsureInitialized()
		{
			if (this.columns == null)
			{
				this.dataSource.ConnectForMetadata(delegate(IOdbcConnection connection)
				{
					using (IDataReader dataReader = connection.GetColumns(this.tableIdentifier.Catalog, OdbcSearchPattern.EscapeSearchCharacters(this.dataSource.Info.SearchPatternEscapeCharacter, this.tableIdentifier.Schema), OdbcSearchPattern.EscapeSearchCharacters(this.dataSource.Info.SearchPatternEscapeCharacter, this.tableIdentifier.Name)))
					{
						HashSet<string> hashSet = new HashSet<string>();
						List<OdbcColumnInfo> list = new List<OdbcColumnInfo>();
						while (dataReader.Read())
						{
							string @string = dataReader.GetString(3);
							if (hashSet.Add(@string))
							{
								OdbcColumnInfo odbcColumnInfo = new OdbcColumnInfo(this.dataSource.Types, @string, dataReader.GetNullableInt32(16).GetValueOrDefault(1), (Odbc32.SQL_TYPE)Convert.ToInt16(dataReader[4], CultureInfo.InvariantCulture), dataReader.GetStringOrNull(17) != "NO", dataReader.GetStringOrNull(5), dataReader.GetNumberPrecisionRadix(9), dataReader.GetNullableInt32(6), dataReader.GetNullableInt32(8), dataReader.GetStringOrNull(12), dataReader.GetStringOrNull(11), new OdbcIdentifier(dataReader[0] as string, dataReader[1] as string, dataReader[2] as string));
								list.Add(odbcColumnInfo);
							}
						}
						IEnumerable<OdbcColumnInfo> enumerable = list.OrderBy((OdbcColumnInfo c) => c.Ordinal).ThenBy((OdbcColumnInfo c) => c.Name, StringComparer.Ordinal);
						Dictionary<string, OdbcColumnInfo> dictionary = new Dictionary<string, OdbcColumnInfo>();
						List<OdbcColumnInfo> list2 = new List<OdbcColumnInfo>();
						foreach (OdbcColumnInfo odbcColumnInfo2 in enumerable)
						{
							odbcColumnInfo2.Ordinal = list2.Count;
							list2.Add(odbcColumnInfo2);
							DictionaryTracing.AddWithTracing<string, OdbcColumnInfo>(dictionary, odbcColumnInfo2.Name, odbcColumnInfo2, this.host, true, true);
						}
						this.columnsByName = dictionary;
						this.columns = list2;
					}
				});
			}
		}

		// Token: 0x0400146C RID: 5228
		private readonly OdbcDataSource dataSource;

		// Token: 0x0400146D RID: 5229
		private readonly OdbcIdentifier tableIdentifier;

		// Token: 0x0400146E RID: 5230
		private readonly IEngineHost host;

		// Token: 0x0400146F RID: 5231
		private Dictionary<string, OdbcColumnInfo> columnsByName;

		// Token: 0x04001470 RID: 5232
		private IList<OdbcColumnInfo> columns;
	}
}
