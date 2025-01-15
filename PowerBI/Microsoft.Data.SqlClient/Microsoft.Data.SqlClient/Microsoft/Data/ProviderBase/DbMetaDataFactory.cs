using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000165 RID: 357
	internal class DbMetaDataFactory
	{
		// Token: 0x06001A8D RID: 6797 RVA: 0x0006C6E2 File Offset: 0x0006A8E2
		public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
		{
			ADP.CheckArgumentNull(xmlStream, "xmlStream");
			ADP.CheckArgumentNull(serverVersion, "serverVersion");
			ADP.CheckArgumentNull(normalizedServerVersion, "normalizedServerVersion");
			this.LoadDataSetFromXml(xmlStream);
			this._serverVersionString = serverVersion;
			this._normalizedServerVersion = normalizedServerVersion;
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0006C720 File Offset: 0x0006A920
		protected DataSet CollectionDataSet
		{
			get
			{
				return this._metaDataCollectionsDataSet;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0006C728 File Offset: 0x0006A928
		protected string ServerVersion
		{
			get
			{
				return this._serverVersionString;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0006C730 File Offset: 0x0006A930
		protected string ServerVersionNormalized
		{
			get
			{
				return this._normalizedServerVersion;
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0006C738 File Offset: 0x0006A938
		protected DataTable CloneAndFilterCollection(string collectionName, string[] hiddenColumnNames)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[collectionName];
			if (dataTable == null || collectionName != dataTable.TableName)
			{
				throw ADP.DataTableDoesNotExist(collectionName);
			}
			DataTable dataTable2 = new DataTable(collectionName)
			{
				Locale = CultureInfo.InvariantCulture
			};
			DataColumnCollection columns = dataTable2.Columns;
			DataColumn[] array = this.FilterColumns(dataTable, hiddenColumnNames, columns);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedByCurrentVersion(dataRow))
				{
					DataRow dataRow2 = dataTable2.NewRow();
					for (int i = 0; i < columns.Count; i++)
					{
						dataRow2[columns[i]] = dataRow[array[i], DataRowVersion.Current];
					}
					dataTable2.Rows.Add(dataRow2);
					dataRow2.AcceptChanges();
				}
			}
			return dataTable2;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0006C83C File Offset: 0x0006AA3C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0006C845 File Offset: 0x0006AA45
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._normalizedServerVersion = null;
				this._serverVersionString = null;
				this._metaDataCollectionsDataSet.Dispose();
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0006C864 File Offset: 0x0006AA64
		private DataTable ExecuteCommand(DataRow requestedCollectionRow, string[] restrictions, DbConnection connection)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn dataColumn = dataTable.Columns["PopulationString"];
			DataColumn dataColumn2 = dataTable.Columns["NumberOfRestrictions"];
			DataColumn dataColumn3 = dataTable.Columns["CollectionName"];
			DataTable dataTable2 = null;
			string text = requestedCollectionRow[dataColumn, DataRowVersion.Current] as string;
			int num = (int)requestedCollectionRow[dataColumn2, DataRowVersion.Current];
			string text2 = requestedCollectionRow[dataColumn3, DataRowVersion.Current] as string;
			if (restrictions != null && restrictions.Length > num)
			{
				throw ADP.TooManyRestrictions(text2);
			}
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = text;
			dbCommand.CommandTimeout = Math.Max(dbCommand.CommandTimeout, 180);
			for (int i = 0; i < num; i++)
			{
				DbParameter dbParameter = dbCommand.CreateParameter();
				if (restrictions != null && restrictions.Length > i && restrictions[i] != null)
				{
					dbParameter.Value = restrictions[i];
				}
				else
				{
					dbParameter.Value = DBNull.Value;
				}
				dbParameter.ParameterName = this.GetParameterName(text2, i + 1);
				dbParameter.Direction = ParameterDirection.Input;
				dbCommand.Parameters.Add(dbParameter);
			}
			DbDataReader dbDataReader = null;
			try
			{
				try
				{
					dbDataReader = dbCommand.ExecuteReader();
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.QueryFailed(text2, ex);
				}
				dataTable2 = new DataTable(text2)
				{
					Locale = CultureInfo.InvariantCulture
				};
				DataTable schemaTable = dbDataReader.GetSchemaTable();
				foreach (object obj in schemaTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable2.Columns.Add(dataRow["ColumnName"] as string, (Type)dataRow["DataType"]);
				}
				object[] array = new object[dataTable2.Columns.Count];
				while (dbDataReader.Read())
				{
					dbDataReader.GetValues(array);
					dataTable2.Rows.Add(array);
				}
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
				}
			}
			return dataTable2;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0006CABC File Offset: 0x0006ACBC
		private DataColumn[] FilterColumns(DataTable sourceTable, string[] hiddenColumnNames, DataColumnCollection destinationColumns)
		{
			int num = 0;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (this.IncludeThisColumn(dataColumn, hiddenColumnNames))
				{
					num++;
				}
			}
			if (num == 0)
			{
				throw ADP.NoColumns();
			}
			int num2 = 0;
			DataColumn[] array = new DataColumn[num];
			foreach (object obj2 in sourceTable.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (this.IncludeThisColumn(dataColumn2, hiddenColumnNames))
				{
					DataColumn dataColumn3 = new DataColumn(dataColumn2.ColumnName, dataColumn2.DataType);
					destinationColumns.Add(dataColumn3);
					array[num2] = dataColumn2;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0006CBB0 File Offset: 0x0006ADB0
		internal DataRow FindMetaDataCollectionRow(string collectionName)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			if (dataTable == null)
			{
				throw ADP.InvalidXml();
			}
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			if (dataColumn == null || typeof(string) != dataColumn.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
			}
			DataRow dataRow = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				string text2 = dataRow2[dataColumn, DataRowVersion.Current] as string;
				if (string.IsNullOrEmpty(text2))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
				}
				if (ADP.CompareInsensitiveInvariant(text2, collectionName))
				{
					if (!this.SupportedByCurrentVersion(dataRow2))
					{
						flag = true;
					}
					else if (collectionName == text2)
					{
						if (flag2)
						{
							throw ADP.CollectionNameIsNotUnique(collectionName);
						}
						dataRow = dataRow2;
						text = text2;
						flag2 = true;
					}
					else if (!flag2)
					{
						if (text != null)
						{
							flag3 = true;
						}
						dataRow = dataRow2;
						text = text2;
					}
				}
			}
			if (dataRow == null)
			{
				if (!flag)
				{
					throw ADP.UndefinedCollection(collectionName);
				}
				throw ADP.UnsupportedVersion(collectionName);
			}
			else
			{
				if (!flag2 && flag3)
				{
					throw ADP.AmbiguousCollectionName(collectionName);
				}
				return dataRow;
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0006CD14 File Offset: 0x0006AF14
		private void FixUpVersion(DataTable dataSourceInfoTable)
		{
			DataColumn dataColumn = dataSourceInfoTable.Columns["DataSourceProductVersion"];
			DataColumn dataColumn2 = dataSourceInfoTable.Columns["DataSourceProductVersionNormalized"];
			if (dataColumn == null || dataColumn2 == null)
			{
				throw ADP.MissingDataSourceInformationColumn();
			}
			if (dataSourceInfoTable.Rows.Count != 1)
			{
				throw ADP.IncorrectNumberOfDataSourceInformationRows();
			}
			DataRow dataRow = dataSourceInfoTable.Rows[0];
			dataRow[dataColumn] = this._serverVersionString;
			dataRow[dataColumn2] = this._normalizedServerVersion;
			dataRow.AcceptChanges();
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0006CD90 File Offset: 0x0006AF90
		private string GetParameterName(string neededCollectionName, int neededRestrictionNumber)
		{
			DataColumn dataColumn = null;
			DataColumn dataColumn2 = null;
			DataColumn dataColumn3 = null;
			DataColumn dataColumn4 = null;
			string text = null;
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.Restrictions];
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				if (columns != null)
				{
					dataColumn = columns["CollectionName"];
					dataColumn2 = columns["ParameterName"];
					dataColumn3 = columns["RestrictionName"];
					dataColumn4 = columns["RestrictionNumber"];
				}
			}
			if (dataColumn2 == null || dataColumn == null || dataColumn3 == null || dataColumn4 == null)
			{
				throw ADP.MissingRestrictionColumn();
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((string)dataRow[dataColumn] == neededCollectionName && (int)dataRow[dataColumn4] == neededRestrictionNumber && this.SupportedByCurrentVersion(dataRow))
				{
					text = (string)dataRow[dataColumn2];
					break;
				}
			}
			if (text == null)
			{
				throw ADP.MissingRestrictionRow();
			}
			return text;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0006CEB0 File Offset: 0x0006B0B0
		public virtual DataTable GetSchema(DbConnection connection, string collectionName, string[] restrictions)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn dataColumn = dataTable.Columns["PopulationMechanism"];
			DataColumn dataColumn2 = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			DataRow dataRow = this.FindMetaDataCollectionRow(collectionName);
			string text = dataRow[dataColumn2, DataRowVersion.Current] as string;
			if (!ADP.IsEmptyArray(restrictions))
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					if (restrictions[i] != null && restrictions[i].Length > 4096)
					{
						throw ADP.NotSupported();
					}
				}
			}
			string text2 = dataRow[dataColumn, DataRowVersion.Current] as string;
			DataTable dataTable2;
			if (!(text2 == "DataTable"))
			{
				if (!(text2 == "SQLCommand"))
				{
					if (!(text2 == "PrepareCollection"))
					{
						throw ADP.UndefinedPopulationMechanism(text2);
					}
					dataTable2 = this.PrepareCollection(text, restrictions, connection);
				}
				else
				{
					dataTable2 = this.ExecuteCommand(dataRow, restrictions, connection);
				}
			}
			else
			{
				string[] array;
				if (text == DbMetaDataCollectionNames.MetaDataCollections)
				{
					array = new string[] { "PopulationMechanism", "PopulationString" };
				}
				else
				{
					array = null;
				}
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(text);
				}
				dataTable2 = this.CloneAndFilterCollection(text, array);
				if (text == DbMetaDataCollectionNames.DataSourceInformation)
				{
					this.FixUpVersion(dataTable2);
				}
			}
			return dataTable2;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0006D00C File Offset: 0x0006B20C
		private bool IncludeThisColumn(DataColumn sourceColumn, string[] hiddenColumnNames)
		{
			bool flag = true;
			string columnName = sourceColumn.ColumnName;
			if (columnName == "MinimumVersion" || columnName == "MaximumVersion")
			{
				flag = false;
			}
			else if (hiddenColumnNames != null)
			{
				for (int i = 0; i < hiddenColumnNames.Length; i++)
				{
					if (hiddenColumnNames[i] == columnName)
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0006D061 File Offset: 0x0006B261
		private void LoadDataSetFromXml(Stream XmlStream)
		{
			this._metaDataCollectionsDataSet = new DataSet
			{
				Locale = CultureInfo.InvariantCulture
			};
			this._metaDataCollectionsDataSet.ReadXml(XmlStream);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00025577 File Offset: 0x00023777
		protected virtual DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0006D088 File Offset: 0x0006B288
		private bool SupportedByCurrentVersion(DataRow requestedCollectionRow)
		{
			bool flag = true;
			DataColumnCollection columns = requestedCollectionRow.Table.Columns;
			DataColumn dataColumn = columns["MinimumVersion"];
			if (dataColumn != null)
			{
				object obj = requestedCollectionRow[dataColumn];
				if (obj != null && obj != DBNull.Value && 0 > string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
				}
			}
			if (flag)
			{
				dataColumn = columns["MaximumVersion"];
				if (dataColumn != null)
				{
					object obj = requestedCollectionRow[dataColumn];
					if (obj != null && obj != DBNull.Value && 0 < string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x04000AD0 RID: 2768
		private DataSet _metaDataCollectionsDataSet;

		// Token: 0x04000AD1 RID: 2769
		private string _normalizedServerVersion;

		// Token: 0x04000AD2 RID: 2770
		private string _serverVersionString;

		// Token: 0x04000AD3 RID: 2771
		private const string CollectionNameKey = "CollectionName";

		// Token: 0x04000AD4 RID: 2772
		private const string PopulationMechanismKey = "PopulationMechanism";

		// Token: 0x04000AD5 RID: 2773
		private const string PopulationStringKey = "PopulationString";

		// Token: 0x04000AD6 RID: 2774
		private const string MaximumVersionKey = "MaximumVersion";

		// Token: 0x04000AD7 RID: 2775
		private const string MinimumVersionKey = "MinimumVersion";

		// Token: 0x04000AD8 RID: 2776
		private const string DataSourceProductVersionNormalizedKey = "DataSourceProductVersionNormalized";

		// Token: 0x04000AD9 RID: 2777
		private const string DataSourceProductVersionKey = "DataSourceProductVersion";

		// Token: 0x04000ADA RID: 2778
		private const string RestrictionNumberKey = "RestrictionNumber";

		// Token: 0x04000ADB RID: 2779
		private const string NumberOfRestrictionsKey = "NumberOfRestrictions";

		// Token: 0x04000ADC RID: 2780
		private const string RestrictionNameKey = "RestrictionName";

		// Token: 0x04000ADD RID: 2781
		private const string ParameterNameKey = "ParameterName";

		// Token: 0x04000ADE RID: 2782
		private const string DataTableKey = "DataTable";

		// Token: 0x04000ADF RID: 2783
		private const string SqlCommandKey = "SQLCommand";

		// Token: 0x04000AE0 RID: 2784
		private const string PrepareCollectionKey = "PrepareCollection";
	}
}
