using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000088 RID: 136
	internal sealed class SqlMetaDataFactory : DbMetaDataFactory
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x000215AE File Offset: 0x0001F7AE
		public SqlMetaDataFactory(Stream XMLStream, string serverVersion, string serverVersionNormalized)
			: base(XMLStream, serverVersion, serverVersionNormalized)
		{
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000215BC File Offset: 0x0001F7BC
		private void addUDTsToDataTypesTable(DataTable dataTypesTable, SqlConnection connection, string ServerVersion)
		{
			if (0 > string.Compare(ServerVersion, "09.00.0000", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandText = "select assemblies.name, types.assembly_class, ASSEMBLYPROPERTY(assemblies.name, 'VersionMajor') as version_major, ASSEMBLYPROPERTY(assemblies.name, 'VersionMinor') as version_minor, ASSEMBLYPROPERTY(assemblies.name, 'VersionBuild') as version_build, ASSEMBLYPROPERTY(assemblies.name, 'VersionRevision') as version_revision, ASSEMBLYPROPERTY(assemblies.name, 'CultureInfo') as culture_info, ASSEMBLYPROPERTY(assemblies.name, 'PublicKey') as public_key, is_nullable, is_fixed_length, max_length from sys.assemblies as assemblies  join sys.assembly_types as types on assemblies.assembly_id = types.assembly_id ";
			DataColumn dataColumn = dataTypesTable.Columns[DbMetaDataColumnNames.ProviderDbType];
			DataColumn dataColumn2 = dataTypesTable.Columns[DbMetaDataColumnNames.ColumnSize];
			DataColumn dataColumn3 = dataTypesTable.Columns[DbMetaDataColumnNames.IsFixedLength];
			DataColumn dataColumn4 = dataTypesTable.Columns[DbMetaDataColumnNames.IsSearchable];
			DataColumn dataColumn5 = dataTypesTable.Columns[DbMetaDataColumnNames.IsLiteralSupported];
			DataColumn dataColumn6 = dataTypesTable.Columns[DbMetaDataColumnNames.TypeName];
			DataColumn dataColumn7 = dataTypesTable.Columns[DbMetaDataColumnNames.IsNullable];
			if (dataColumn == null || dataColumn2 == null || dataColumn3 == null || dataColumn4 == null || dataColumn5 == null || dataColumn6 == null || dataColumn7 == null)
			{
				throw ADP.InvalidXml();
			}
			using (IDataReader dataReader = sqlCommand.ExecuteReader())
			{
				object[] array = new object[11];
				while (dataReader.Read())
				{
					dataReader.GetValues(array);
					DataRow dataRow = dataTypesTable.NewRow();
					dataRow[dataColumn] = SqlDbType.Udt;
					if (array[10] != DBNull.Value)
					{
						dataRow[dataColumn2] = array[10];
					}
					if (array[9] != DBNull.Value)
					{
						dataRow[dataColumn3] = array[9];
					}
					dataRow[dataColumn4] = true;
					dataRow[dataColumn5] = false;
					if (array[8] != DBNull.Value)
					{
						dataRow[dataColumn7] = array[8];
					}
					if (array[0] != DBNull.Value && array[1] != DBNull.Value && array[2] != DBNull.Value && array[3] != DBNull.Value && array[4] != DBNull.Value && array[5] != DBNull.Value)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append(array[1].ToString());
						stringBuilder.Append(", ");
						stringBuilder.Append(array[0].ToString());
						stringBuilder.Append(", Version=");
						stringBuilder.Append(array[2].ToString());
						stringBuilder.Append(".");
						stringBuilder.Append(array[3].ToString());
						stringBuilder.Append(".");
						stringBuilder.Append(array[4].ToString());
						stringBuilder.Append(".");
						stringBuilder.Append(array[5].ToString());
						if (array[6] != DBNull.Value)
						{
							stringBuilder.Append(", Culture=");
							stringBuilder.Append(array[6].ToString());
						}
						if (array[7] != DBNull.Value)
						{
							stringBuilder.Append(", PublicKeyToken=");
							StringBuilder stringBuilder2 = new StringBuilder();
							byte[] array2 = (byte[])array[7];
							foreach (byte b in array2)
							{
								stringBuilder2.Append(string.Format("{0,-2:x2}", b));
							}
							stringBuilder.Append(stringBuilder2.ToString());
						}
						dataRow[dataColumn6] = stringBuilder.ToString();
						dataTypesTable.Rows.Add(dataRow);
						dataRow.AcceptChanges();
					}
				}
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002191C File Offset: 0x0001FB1C
		private void AddTVPsToDataTypesTable(DataTable dataTypesTable, SqlConnection connection, string ServerVersion)
		{
			if (0 > string.Compare(ServerVersion, "10.00.0000", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandText = "select name, is_nullable, max_length from sys.types where is_table_type = 1";
			DataColumn dataColumn = dataTypesTable.Columns[DbMetaDataColumnNames.ProviderDbType];
			DataColumn dataColumn2 = dataTypesTable.Columns[DbMetaDataColumnNames.ColumnSize];
			DataColumn dataColumn3 = dataTypesTable.Columns[DbMetaDataColumnNames.IsSearchable];
			DataColumn dataColumn4 = dataTypesTable.Columns[DbMetaDataColumnNames.IsLiteralSupported];
			DataColumn dataColumn5 = dataTypesTable.Columns[DbMetaDataColumnNames.TypeName];
			DataColumn dataColumn6 = dataTypesTable.Columns[DbMetaDataColumnNames.IsNullable];
			if (dataColumn == null || dataColumn2 == null || dataColumn3 == null || dataColumn4 == null || dataColumn5 == null || dataColumn6 == null)
			{
				throw ADP.InvalidXml();
			}
			using (IDataReader dataReader = sqlCommand.ExecuteReader())
			{
				object[] array = new object[11];
				while (dataReader.Read())
				{
					dataReader.GetValues(array);
					DataRow dataRow = dataTypesTable.NewRow();
					dataRow[dataColumn] = SqlDbType.Structured;
					if (array[2] != DBNull.Value)
					{
						dataRow[dataColumn2] = array[2];
					}
					dataRow[dataColumn3] = false;
					dataRow[dataColumn4] = false;
					if (array[1] != DBNull.Value)
					{
						dataRow[dataColumn6] = array[1];
					}
					if (array[0] != DBNull.Value)
					{
						dataRow[dataColumn5] = array[0];
						dataTypesTable.Rows.Add(dataRow);
						dataRow.AcceptChanges();
					}
				}
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		private DataTable GetDataTypesTable(SqlConnection connection)
		{
			if (base.CollectionDataSet.Tables[DbMetaDataCollectionNames.DataTypes] == null)
			{
				throw ADP.UnableToBuildCollection(DbMetaDataCollectionNames.DataTypes);
			}
			DataTable dataTable = base.CloneAndFilterCollection(DbMetaDataCollectionNames.DataTypes, null);
			this.addUDTsToDataTypesTable(dataTable, connection, base.ServerVersionNormalized);
			this.AddTVPsToDataTypesTable(dataTable, connection, base.ServerVersionNormalized);
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00021B0C File Offset: 0x0001FD0C
		protected override DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			SqlConnection sqlConnection = (SqlConnection)connection;
			DataTable dataTable = null;
			if (collectionName == DbMetaDataCollectionNames.DataTypes)
			{
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(DbMetaDataCollectionNames.DataTypes);
				}
				dataTable = this.GetDataTypesTable(sqlConnection);
			}
			if (dataTable == null)
			{
				throw ADP.UnableToBuildCollection(collectionName);
			}
			return dataTable;
		}

		// Token: 0x040002D3 RID: 723
		private const string ServerVersionNormalized90 = "09.00.0000";

		// Token: 0x040002D4 RID: 724
		private const string ServerVersionNormalized10 = "10.00.0000";
	}
}
