using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A23 RID: 2595
	internal class DrdaSchemaDataTypesInformation : DrdaSchemaInformation
	{
		// Token: 0x0600516C RID: 20844 RVA: 0x001487CF File Offset: 0x001469CF
		public DrdaSchemaDataTypesInformation()
			: base("DataTypes", DrdaSchemaDataTypesInformation.SchemaRestrictions, 1, DrdaSchemaDataTypesInformation.SchemaResultColumns)
		{
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x001487F4 File Offset: 0x001469F4
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			this._addedDrdaTypes.Clear();
			await statement.ExecuteAsync("CALL SYSIBM.SQLGETTYPEINFO (?, ?)", base.GetParameters(conn.Requester.HostType, parameters, -1, options), true, false, isAsync, cancellationToken);
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0014886C File Offset: 0x00146A6C
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
			string text = string.Empty;
			object obj = null;
			Tuple<DrdaType, string, bool, bool, bool> tuple = null;
			bool flag = false;
			object obj2 = null;
			object obj3 = null;
			for (int i = 0; i < resultSet.FieldCount; i++)
			{
				switch (i)
				{
				case 0:
				{
					int num = resultSet.GetColumnMappingIndex(0, "TYPE_NAME", 0);
					text = queryResultSet.GetString(num).ToUpperInvariant();
					bool flag2 = false;
					if (!base.GetDataTypeMapping(connection.Requester.Flavor).TryGetValue(text, out tuple))
					{
						flag2 = true;
					}
					else if (this._addedDrdaTypes.Contains(tuple.Item1))
					{
						flag2 = true;
					}
					if (flag2)
					{
						resultSet.GetColumn(0).Value = string.Empty;
						resultSet.GetColumn(1).Value = -1;
						return;
					}
					this._addedDrdaTypes.Add(tuple.Item1);
					obj = tuple.Item1.ToString();
					break;
				}
				case 1:
					obj = (int)tuple.Item1;
					break;
				case 2:
				{
					int num = resultSet.GetColumnMappingIndex(2, "COLUMN_SIZE", 2);
					obj = queryResultSet.GetValue(i);
					break;
				}
				case 3:
					obj = string.Empty;
					break;
				case 4:
				{
					int num = resultSet.GetColumnMappingIndex(4, "CREATE_PARAMS", 5);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 5:
					obj = tuple.Item2;
					break;
				case 6:
				{
					int num = resultSet.GetColumnMappingIndex(6, "AUTO_INCREMENT", 11);
					obj = queryResultSet.GetValue(num);
					obj = obj is short && (short)obj == 1;
					break;
				}
				case 7:
					obj = false;
					break;
				case 8:
				{
					int num = resultSet.GetColumnMappingIndex(8, "CASE_SENSITIVE", 7);
					obj = queryResultSet.GetValue(num);
					obj = obj is short && (short)obj == 1;
					break;
				}
				case 9:
					obj = tuple.Item3;
					break;
				case 10:
				{
					int num = resultSet.GetColumnMappingIndex(10, "FIXED_PREC_SCALE", 10);
					obj = queryResultSet.GetValue(num);
					obj = obj is short && (short)obj == 1;
					break;
				}
				case 11:
					obj = tuple.Item4;
					break;
				case 12:
				{
					int num = resultSet.GetColumnMappingIndex(12, "NULLABLE", 6);
					obj = queryResultSet.GetValue(num);
					obj = obj is short && (short)obj == 1;
					break;
				}
				case 13:
				{
					int num = resultSet.GetColumnMappingIndex(13, "SEARCHABLE", 8);
					obj = queryResultSet.GetValue(num);
					if (obj is short)
					{
						switch ((short)obj)
						{
						case 1:
						case 3:
							obj = true;
							flag = true;
							break;
						case 2:
							obj = true;
							flag = false;
							break;
						default:
							obj = false;
							flag = false;
							break;
						}
					}
					else
					{
						obj = false;
					}
					break;
				}
				case 14:
					obj = flag;
					break;
				case 15:
				{
					int num = resultSet.GetColumnMappingIndex(15, "UNSIGNED_ATTRIBUTE", 9);
					obj = queryResultSet.GetValue(num);
					obj = obj is short && (short)obj == 1;
					break;
				}
				case 16:
				{
					int num = resultSet.GetColumnMappingIndex(16, "MAXIMUM_SCALE", 14);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 17:
				{
					int num = resultSet.GetColumnMappingIndex(17, "MINIMUM_SCALE", 13);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 18:
					obj = tuple.Item5;
					break;
				case 19:
				{
					int num = resultSet.GetColumnMappingIndex(20, "LITERAL_PREFIX", 3);
					obj2 = queryResultSet.GetValue(num);
					num = resultSet.GetColumnMappingIndex(21, "LITERAL_SUFFIX", 4);
					obj3 = queryResultSet.GetValue(num);
					obj = obj3 != null && obj3 != DBNull.Value && obj2 != null && obj2 != DBNull.Value;
					break;
				}
				case 20:
					obj = obj2;
					break;
				case 21:
					obj = obj3;
					break;
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x0400401A RID: 16410
		internal const string CollectionName = "DataTypes";

		// Token: 0x0400401B RID: 16411
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("DATATYPE", typeof(short), null)
		};

		// Token: 0x0400401C RID: 16412
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.TypeName, typeof(string), null, 128),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.ProviderDbType, typeof(int), 0),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.ColumnSize, typeof(long), null),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.CreateFormat, typeof(string), null, 128),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.CreateParameters, typeof(string), null, 128),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.DataType, typeof(string), null, 128),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsAutoIncrementable, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsBestMatch, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsCaseSensitive, typeof(bool), true),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsFixedLength, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsFixedPrecisionScale, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsLong, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsNullable, typeof(bool), true),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsSearchable, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsSearchableWithLike, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsUnsigned, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.MaximumScale, typeof(short), 0),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.MinimumScale, typeof(short), 0),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsConcurrencyType, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.IsLiteralSupported, typeof(bool), false),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.LiteralPrefix, typeof(string), null, 128),
			new DrdaSchemaResultColumn(DbMetaDataColumnNames.LiteralSuffix, typeof(string), null, 128)
		};

		// Token: 0x0400401D RID: 16413
		private SortedSet<DrdaType> _addedDrdaTypes = new SortedSet<DrdaType>();
	}
}
