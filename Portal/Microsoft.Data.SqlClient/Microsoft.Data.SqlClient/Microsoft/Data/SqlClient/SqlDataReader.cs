using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000EA RID: 234
	public class SqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x0003FCC4 File Offset: 0x0003DEC4
		internal SqlDataReader(SqlCommand command, CommandBehavior behavior)
		{
			this._command = command;
			this._commandBehavior = behavior;
			if (this._command != null)
			{
				this._defaultTimeoutMilliseconds = (long)command.CommandTimeout * 1000L;
				this._connection = command.Connection;
				if (this._connection != null)
				{
					this._statistics = this._connection.Statistics;
					this._typeSystem = this._connection.TypeSystem;
				}
			}
			this._sharedState._dataReady = false;
			this._metaDataConsumed = false;
			this._hasRows = false;
			this._browseModeInfoConsumed = false;
			this._currentStream = null;
			this._currentTextReader = null;
			this._cancelAsyncOnCloseTokenSource = new CancellationTokenSource();
			this._cancelAsyncOnCloseToken = this._cancelAsyncOnCloseTokenSource.Token;
			this._columnDataCharsIndex = -1;
		}

		// Token: 0x17000870 RID: 2160
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x0003FDAB File Offset: 0x0003DFAB
		internal bool BrowseModeInfoConsumed
		{
			set
			{
				this._browseModeInfoConsumed = value;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
		internal SqlCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0003FDBC File Offset: 0x0003DFBC
		protected SqlConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0003FDCC File Offset: 0x0003DFCC
		public SensitivityClassification SensitivityClassification { get; internal set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0003FDD5 File Offset: 0x0003DFD5
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0003FDEB File Offset: 0x0003DFEB
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				if (this.MetaData == null)
				{
					return 0;
				}
				return this._metaData.Length;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0003FE23 File Offset: 0x0003E023
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				return this._hasRows;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0003FE4C File Offset: 0x0003E04C
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0003FE54 File Offset: 0x0003E054
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003FE5C File Offset: 0x0003E05C
		internal bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			set
			{
				this._isInitialized = value;
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003FE65 File Offset: 0x0003E065
		internal long ColumnDataBytesRemaining()
		{
			if (-1L == this._sharedState._columnDataBytesRemaining)
			{
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
			}
			return this._sharedState._columnDataBytesRemaining;
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0003FEA0 File Offset: 0x0003E0A0
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("MetaData");
				}
				if (this._metaData == null && !this._metaDataConsumed)
				{
					if (this._currentTask != null)
					{
						throw SQL.PendingBeginXXXExists();
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						if (!this.TryConsumeMetaData())
						{
							throw SQL.SynchronousCallMayNotPend();
						}
					}
					catch (OutOfMemoryException ex)
					{
						this._isClosed = true;
						if (this._connection != null)
						{
							this._connection.Abort(ex);
						}
						throw;
					}
					catch (StackOverflowException ex2)
					{
						this._isClosed = true;
						if (this._connection != null)
						{
							this._connection.Abort(ex2);
						}
						throw;
					}
					catch (ThreadAbortException ex3)
					{
						this._isClosed = true;
						if (this._connection != null)
						{
							this._connection.Abort(ex3);
						}
						throw;
					}
				}
				return this._metaData;
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003FF80 File Offset: 0x0003E180
		internal virtual SmiExtendedMetaData[] GetInternalSmiMetaData()
		{
			SmiExtendedMetaData[] array = null;
			_SqlMetaDataSet metaData = this.MetaData;
			if (metaData != null && 0 < metaData.Length)
			{
				array = new SmiExtendedMetaData[metaData.VisibleColumnCount];
				int num = 0;
				for (int i = 0; i < metaData.Length; i++)
				{
					_SqlMetaData sqlMetaData = metaData[i];
					if (!sqlMetaData.IsHidden)
					{
						SqlCollation collation = sqlMetaData.collation;
						string text = null;
						string text2 = null;
						string text3 = null;
						if (SqlDbType.Xml == sqlMetaData.type)
						{
							SqlMetaDataXmlSchemaCollection xmlSchemaCollection = sqlMetaData.xmlSchemaCollection;
							text = ((xmlSchemaCollection != null) ? xmlSchemaCollection.Database : null);
							SqlMetaDataXmlSchemaCollection xmlSchemaCollection2 = sqlMetaData.xmlSchemaCollection;
							text2 = ((xmlSchemaCollection2 != null) ? xmlSchemaCollection2.OwningSchema : null);
							SqlMetaDataXmlSchemaCollection xmlSchemaCollection3 = sqlMetaData.xmlSchemaCollection;
							text3 = ((xmlSchemaCollection3 != null) ? xmlSchemaCollection3.Name : null);
						}
						else if (SqlDbType.Udt == sqlMetaData.type)
						{
							this.Connection.CheckGetExtendedUDTInfo(sqlMetaData, true);
							SqlMetaDataUdt udt = sqlMetaData.udt;
							text = ((udt != null) ? udt.DatabaseName : null);
							SqlMetaDataUdt udt2 = sqlMetaData.udt;
							text2 = ((udt2 != null) ? udt2.SchemaName : null);
							SqlMetaDataUdt udt3 = sqlMetaData.udt;
							text3 = ((udt3 != null) ? udt3.TypeName : null);
						}
						int num2 = sqlMetaData.length;
						if (num2 > 8000)
						{
							num2 = -1;
						}
						else if (SqlDbType.NChar == sqlMetaData.type || SqlDbType.NVarChar == sqlMetaData.type)
						{
							num2 /= 2;
						}
						SmiExtendedMetaData[] array2 = array;
						int num3 = num;
						SqlDbType type = sqlMetaData.type;
						long num4 = (long)num2;
						byte precision = sqlMetaData.precision;
						byte scale = sqlMetaData.scale;
						long num5 = (long)((collation != null) ? collation.LCID : this._defaultLCID);
						SqlCompareOptions sqlCompareOptions = ((collation != null) ? collation.SqlCompareOptions : SqlCompareOptions.None);
						SqlMetaDataUdt udt4 = sqlMetaData.udt;
						array2[num3] = new SmiQueryMetaData(type, num4, precision, scale, num5, sqlCompareOptions, (udt4 != null) ? udt4.Type : null, false, null, null, sqlMetaData.column, text, text2, text3, sqlMetaData.IsNullable, sqlMetaData.serverName, sqlMetaData.catalogName, sqlMetaData.schemaName, sqlMetaData.tableName, sqlMetaData.baseColumn, sqlMetaData.IsKey, sqlMetaData.IsIdentity, sqlMetaData.IsReadOnly, sqlMetaData.IsExpression, sqlMetaData.IsDifferentName, sqlMetaData.IsHidden);
						num++;
					}
				}
			}
			return array;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0004019D File Offset: 0x0003E39D
		public override int RecordsAffected
		{
			get
			{
				if (this._command != null)
				{
					return this._command.InternalRecordsAffected;
				}
				return this._recordsAffected;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x000401B9 File Offset: 0x0003E3B9
		internal string ResetOptionsString
		{
			set
			{
				this._resetOptionsString = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000401C2 File Offset: 0x0003E3C2
		private SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000401CA File Offset: 0x0003E3CA
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x000401D2 File Offset: 0x0003E3D2
		internal MultiPartTableName[] TableNames
		{
			get
			{
				return this._tableNames;
			}
			set
			{
				this._tableNames = value;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000401DC File Offset: 0x0003E3DC
		public override int VisibleFieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("VisibleFieldCount");
				}
				_SqlMetaDataSet metaData = this.MetaData;
				if (metaData == null)
				{
					return 0;
				}
				return metaData.VisibleColumnCount;
			}
		}

		// Token: 0x1700087F RID: 2175
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000880 RID: 2176
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00040226 File Offset: 0x0003E426
		internal void Bind(TdsParserStateObject stateObj)
		{
			stateObj.Owner = this;
			this._stateObj = stateObj;
			this._parser = stateObj.Parser;
			this._defaultLCID = this._parser.DefaultLCID;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00040254 File Offset: 0x0003E454
		internal DataTable BuildSchemaTable()
		{
			_SqlMetaDataSet metaData = this.MetaData;
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = metaData.Length;
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn dataColumn3 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn dataColumn4 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn dataColumn6 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn dataColumn7 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
			DataColumn dataColumn9 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn10 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn dataColumn11 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn dataColumn12 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn dataColumn13 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn dataColumn14 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn dataColumn15 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn dataColumn16 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn dataColumn17 = new DataColumn(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			DataColumn dataColumn18 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn dataColumn19 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn dataColumn20 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn dataColumn21 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn dataColumn22 = new DataColumn(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			DataColumn dataColumn23 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
			DataColumn dataColumn24 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
			DataColumn dataColumn25 = new DataColumn("IsIdentity", typeof(bool));
			DataColumn dataColumn26 = new DataColumn("DataTypeName", typeof(string));
			DataColumn dataColumn27 = new DataColumn("UdtAssemblyQualifiedName", typeof(string));
			DataColumn dataColumn28 = new DataColumn("XmlSchemaCollectionDatabase", typeof(string));
			DataColumn dataColumn29 = new DataColumn("XmlSchemaCollectionOwningSchema", typeof(string));
			DataColumn dataColumn30 = new DataColumn("XmlSchemaCollectionName", typeof(string));
			DataColumn dataColumn31 = new DataColumn("IsColumnSet", typeof(bool));
			dataColumn2.DefaultValue = 0;
			dataColumn10.DefaultValue = false;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(dataColumn);
			columns.Add(dataColumn2);
			columns.Add(dataColumn3);
			columns.Add(dataColumn4);
			columns.Add(dataColumn5);
			columns.Add(dataColumn14);
			columns.Add(dataColumn15);
			columns.Add(dataColumn22);
			columns.Add(dataColumn18);
			columns.Add(dataColumn21);
			columns.Add(dataColumn19);
			columns.Add(dataColumn20);
			columns.Add(dataColumn6);
			columns.Add(dataColumn11);
			columns.Add(dataColumn9);
			columns.Add(dataColumn23);
			columns.Add(dataColumn24);
			columns.Add(dataColumn25);
			columns.Add(dataColumn16);
			columns.Add(dataColumn13);
			columns.Add(dataColumn17);
			columns.Add(dataColumn10);
			columns.Add(dataColumn12);
			columns.Add(dataColumn7);
			columns.Add(dataColumn26);
			columns.Add(dataColumn28);
			columns.Add(dataColumn29);
			columns.Add(dataColumn30);
			columns.Add(dataColumn27);
			columns.Add(dataColumn8);
			columns.Add(dataColumn31);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow[dataColumn] = sqlMetaData.column;
				dataRow[dataColumn2] = sqlMetaData.ordinal;
				if (sqlMetaData.cipherMD != null)
				{
					dataRow[dataColumn3] = ((sqlMetaData.baseTI.metaType.IsSizeInCharacters && sqlMetaData.baseTI.length != int.MaxValue) ? (sqlMetaData.baseTI.length / 2) : sqlMetaData.baseTI.length);
				}
				else
				{
					dataRow[dataColumn3] = ((sqlMetaData.metaType.IsSizeInCharacters && sqlMetaData.length != int.MaxValue) ? (sqlMetaData.length / 2) : sqlMetaData.length);
				}
				dataRow[dataColumn6] = this.GetFieldTypeInternal(sqlMetaData);
				dataRow[dataColumn7] = this.GetProviderSpecificFieldTypeInternal(sqlMetaData);
				dataRow[dataColumn8] = (int)((sqlMetaData.cipherMD != null) ? sqlMetaData.baseTI.type : sqlMetaData.type);
				dataRow[dataColumn26] = this.GetDataTypeNameInternal(sqlMetaData);
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.Is2008DateTimeType)
				{
					dataRow[dataColumn9] = SqlDbType.NVarChar;
					switch (sqlMetaData.type)
					{
					case SqlDbType.Date:
						dataRow[dataColumn3] = 10;
						break;
					case SqlDbType.Time:
						dataRow[dataColumn3] = TdsEnums.WHIDBEY_TIME_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTime2:
						dataRow[dataColumn3] = TdsEnums.WHIDBEY_DATETIME2_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTimeOffset:
						dataRow[dataColumn3] = TdsEnums.WHIDBEY_DATETIMEOFFSET_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					}
				}
				else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsLargeUdt)
				{
					if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
					{
						dataRow[dataColumn9] = SqlDbType.VarBinary;
					}
					else
					{
						dataRow[dataColumn9] = SqlDbType.Image;
					}
				}
				else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
				{
					dataRow[dataColumn9] = (int)((sqlMetaData.cipherMD != null) ? sqlMetaData.baseTI.type : sqlMetaData.type);
					if (sqlMetaData.type == SqlDbType.Udt)
					{
						DataRow dataRow2 = dataRow;
						DataColumn dataColumn32 = dataColumn27;
						SqlMetaDataUdt udt = sqlMetaData.udt;
						dataRow2[dataColumn32] = ((udt != null) ? udt.AssemblyQualifiedName : null);
					}
					else if (sqlMetaData.type == SqlDbType.Xml)
					{
						DataRow dataRow3 = dataRow;
						DataColumn dataColumn33 = dataColumn28;
						SqlMetaDataXmlSchemaCollection xmlSchemaCollection = sqlMetaData.xmlSchemaCollection;
						dataRow3[dataColumn33] = ((xmlSchemaCollection != null) ? xmlSchemaCollection.Database : null);
						DataRow dataRow4 = dataRow;
						DataColumn dataColumn34 = dataColumn29;
						SqlMetaDataXmlSchemaCollection xmlSchemaCollection2 = sqlMetaData.xmlSchemaCollection;
						dataRow4[dataColumn34] = ((xmlSchemaCollection2 != null) ? xmlSchemaCollection2.OwningSchema : null);
						DataRow dataRow5 = dataRow;
						DataColumn dataColumn35 = dataColumn30;
						SqlMetaDataXmlSchemaCollection xmlSchemaCollection3 = sqlMetaData.xmlSchemaCollection;
						dataRow5[dataColumn35] = ((xmlSchemaCollection3 != null) ? xmlSchemaCollection3.Name : null);
					}
				}
				else
				{
					dataRow[dataColumn9] = this.GetVersionedMetaType(sqlMetaData.metaType).SqlDbType;
				}
				if (sqlMetaData.cipherMD != null)
				{
					if (255 != sqlMetaData.baseTI.precision)
					{
						dataRow[dataColumn4] = sqlMetaData.baseTI.precision;
					}
					else
					{
						dataRow[dataColumn4] = sqlMetaData.baseTI.metaType.Precision;
					}
				}
				else if (255 != sqlMetaData.precision)
				{
					dataRow[dataColumn4] = sqlMetaData.precision;
				}
				else
				{
					dataRow[dataColumn4] = sqlMetaData.metaType.Precision;
				}
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.Is2008DateTimeType)
				{
					dataRow[dataColumn5] = MetaType.MetaNVarChar.Scale;
				}
				else if (sqlMetaData.cipherMD != null)
				{
					if (255 != sqlMetaData.baseTI.scale)
					{
						dataRow[dataColumn5] = sqlMetaData.baseTI.scale;
					}
					else
					{
						dataRow[dataColumn5] = sqlMetaData.baseTI.metaType.Scale;
					}
				}
				else if (255 != sqlMetaData.scale)
				{
					dataRow[dataColumn5] = sqlMetaData.scale;
				}
				else
				{
					dataRow[dataColumn5] = sqlMetaData.metaType.Scale;
				}
				dataRow[dataColumn11] = sqlMetaData.IsNullable;
				if (this._browseModeInfoConsumed)
				{
					dataRow[dataColumn23] = sqlMetaData.IsDifferentName;
					dataRow[dataColumn15] = sqlMetaData.IsKey;
					dataRow[dataColumn17] = sqlMetaData.IsHidden;
					dataRow[dataColumn24] = sqlMetaData.IsExpression;
				}
				dataRow[dataColumn25] = sqlMetaData.IsIdentity;
				dataRow[dataColumn16] = sqlMetaData.IsIdentity;
				if (sqlMetaData.cipherMD != null)
				{
					dataRow[dataColumn10] = sqlMetaData.baseTI.metaType.IsLong;
				}
				else
				{
					dataRow[dataColumn10] = sqlMetaData.metaType.IsLong;
				}
				if (SqlDbType.Timestamp == sqlMetaData.type)
				{
					dataRow[dataColumn14] = true;
					dataRow[dataColumn13] = true;
				}
				else
				{
					dataRow[dataColumn14] = false;
					dataRow[dataColumn13] = false;
				}
				dataRow[dataColumn12] = sqlMetaData.IsReadOnly;
				dataRow[dataColumn31] = sqlMetaData.IsColumnSet;
				if (!ADP.IsEmpty(sqlMetaData.serverName))
				{
					dataRow[dataColumn22] = sqlMetaData.serverName;
				}
				if (!ADP.IsEmpty(sqlMetaData.catalogName))
				{
					dataRow[dataColumn18] = sqlMetaData.catalogName;
				}
				if (!ADP.IsEmpty(sqlMetaData.schemaName))
				{
					dataRow[dataColumn19] = sqlMetaData.schemaName;
				}
				if (!ADP.IsEmpty(sqlMetaData.tableName))
				{
					dataRow[dataColumn20] = sqlMetaData.tableName;
				}
				if (!ADP.IsEmpty(sqlMetaData.baseColumn))
				{
					dataRow[dataColumn21] = sqlMetaData.baseColumn;
				}
				else if (!ADP.IsEmpty(sqlMetaData.column))
				{
					dataRow[dataColumn21] = sqlMetaData.column;
				}
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			foreach (object obj in columns)
			{
				DataColumn dataColumn36 = (DataColumn)obj;
				dataColumn36.ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00040DD4 File Offset: 0x0003EFD4
		internal void Cancel(int objectID)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.Cancel(objectID);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		private bool TryCleanPartialRead()
		{
			if (this._stateObj._partialHeaderBytesRead > 0 && !this._stateObj.TryProcessHeader())
			{
				return false;
			}
			if (-1 != this._lastColumnWithDataChunkRead)
			{
				this.CloseActiveSequentialStreamAndTextReader();
			}
			if (this._sharedState._nextColumnHeaderToRead == 0)
			{
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._stateObj))
				{
					return false;
				}
			}
			else
			{
				if (!this.TryResetBlobState())
				{
					return false;
				}
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
				{
					return false;
				}
			}
			this._sharedState._dataReady = false;
			return true;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00040E9C File Offset: 0x0003F09C
		private void CleanPartialReadReliable()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				bool flag = this.TryCleanPartialRead();
			}
			catch (OutOfMemoryException ex)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex);
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00040F3C File Offset: 0x0003F13C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				base.Dispose(disposing);
			}
			catch (SqlException ex)
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string>("SqlDataReader.Dispose | ERR | Error Message: {0}, Stack Trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00040F8C File Offset: 0x0003F18C
		public override void Close()
		{
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int>("<sc.SqlDataReader.Close|API> {0}", this.ObjectID))
			{
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					TdsParserStateObject stateObj = this._stateObj;
					this._cancelAsyncOnCloseTokenSource.Cancel();
					Task currentTask = this._currentTask;
					if (currentTask != null && !currentTask.IsCompleted)
					{
						try
						{
							((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
							TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
							if (networkPacketTaskSource != null)
							{
								((IAsyncResult)networkPacketTaskSource.Task).AsyncWaitHandle.WaitOne();
							}
						}
						catch (Exception)
						{
							this._connection.InnerConnection.DoomThisConnection();
							this._isClosed = true;
							if (stateObj != null)
							{
								TdsParserStateObject tdsParserStateObject = stateObj;
								lock (tdsParserStateObject)
								{
									this._stateObj = null;
									this._command = null;
									this._connection = null;
								}
							}
							throw;
						}
					}
					this.CloseActiveSequentialStreamAndTextReader();
					if (stateObj != null)
					{
						TdsParserStateObject tdsParserStateObject2 = stateObj;
						lock (tdsParserStateObject2)
						{
							if (this._stateObj != null)
							{
								if (this._snapshot != null)
								{
									this.PrepareForAsyncContinuation();
								}
								this.SetTimeout(this._defaultTimeoutMilliseconds);
								stateObj._syncOverAsync = true;
								if (!this.TryCloseInternal(true))
								{
									throw SQL.SynchronousCallMayNotPend();
								}
							}
						}
					}
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00041148 File Offset: 0x0003F348
		private bool TryCloseInternal(bool closeReader)
		{
			TdsParser parser = this._parser;
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = this.IsCommandBehavior(CommandBehavior.CloseConnection);
			bool flag2 = false;
			bool flag3 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag5;
			try
			{
				if (!this._isClosed && parser != null && stateObj != null && stateObj._pendingData && parser.State == TdsParserState.OpenLoggedIn)
				{
					if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
					{
						this._sharedState._dataReady = true;
					}
					this._stateObj.SetTimeoutStateStopped();
					if (this._sharedState._dataReady)
					{
						flag3 = true;
						if (!this.TryCleanPartialRead())
						{
							return false;
						}
						flag3 = false;
					}
					bool flag4;
					if (!parser.TryRun(RunBehavior.Clean, this._command, this, null, stateObj, out flag4))
					{
						return false;
					}
				}
				this.RestoreServerSettings(parser, stateObj);
				flag5 = true;
			}
			catch (OutOfMemoryException ex)
			{
				this._isClosed = true;
				flag2 = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex);
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._isClosed = true;
				flag2 = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._isClosed = true;
				flag2 = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
			finally
			{
				if (flag2)
				{
					this._isClosed = true;
					this._command = null;
					this._connection = null;
					this._statistics = null;
					this._stateObj = null;
					this._parser = null;
				}
				else if (closeReader)
				{
					bool isClosed = this._isClosed;
					this._isClosed = true;
					this._parser = null;
					this._stateObj = null;
					this._data = null;
					if (this._snapshot != null)
					{
						this.CleanupAfterAsyncInvocationInternal(stateObj, true);
					}
					if (this.Connection != null)
					{
						this.Connection.RemoveWeakReference(this);
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						if (!isClosed && stateObj != null)
						{
							if (!flag3)
							{
								stateObj.CloseSession();
							}
							else if (parser != null)
							{
								parser.State = TdsParserState.Broken;
								parser.PutSession(stateObj);
								parser.Connection.BreakConnection();
							}
						}
					}
					catch (OutOfMemoryException ex4)
					{
						if (this._connection != null)
						{
							this._connection.Abort(ex4);
						}
						throw;
					}
					catch (StackOverflowException ex5)
					{
						if (this._connection != null)
						{
							this._connection.Abort(ex5);
						}
						throw;
					}
					catch (ThreadAbortException ex6)
					{
						if (this._connection != null)
						{
							this._connection.Abort(ex6);
						}
						throw;
					}
					bool flag6 = this.TrySetMetaData(null, false);
					this._fieldNameLookup = null;
					if (flag && this.Connection != null)
					{
						this.Connection.Close();
					}
					if (this._command != null)
					{
						this._recordsAffected = this._command.InternalRecordsAffected;
					}
					this._command = null;
					this._connection = null;
					this._statistics = null;
				}
			}
			return flag5;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00041474 File Offset: 0x0003F674
		internal virtual void CloseReaderFromConnection()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State == TdsParserState.OpenLoggedIn)
			{
				this.Close();
				return;
			}
			TdsParserStateObject stateObj = this._stateObj;
			this._isClosed = true;
			this._cancelAsyncOnCloseTokenSource.Cancel();
			if (stateObj != null)
			{
				TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
				if (networkPacketTaskSource != null)
				{
					networkPacketTaskSource.TrySetException(ADP.ClosedConnectionError());
				}
				if (this._snapshot != null)
				{
					this.CleanupAfterAsyncInvocationInternal(stateObj, false);
				}
				stateObj._syncOverAsync = true;
				stateObj.RemoveOwner();
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000414EC File Offset: 0x0003F6EC
		private bool TryConsumeMetaData()
		{
			while (this._parser != null && this._stateObj != null && this._stateObj._pendingData && !this._metaDataConsumed)
			{
				if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					if (this._parser.Connection != null)
					{
						this._parser.Connection.DoomThisConnection();
					}
					throw SQL.ConnectionDoomed();
				}
				bool flag;
				if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00041584 File Offset: 0x0003F784
		public override string GetDataTypeName(int i)
		{
			SqlStatistics sqlStatistics = null;
			string dataTypeNameInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				dataTypeNameInternal = this.GetDataTypeNameInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return dataTypeNameInternal;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000415D4 File Offset: 0x0003F7D4
		private string GetDataTypeNameInternal(_SqlMetaData metaData)
		{
			string text;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				text = MetaType.MetaNVarChar.TypeName;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					text = MetaType.MetaMaxVarBinary.TypeName;
				}
				else
				{
					text = MetaType.MetaImage.TypeName;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					string[] array = new string[5];
					int num = 0;
					SqlMetaDataUdt udt = metaData.udt;
					array[num] = ((udt != null) ? udt.DatabaseName : null);
					array[1] = ".";
					int num2 = 2;
					SqlMetaDataUdt udt2 = metaData.udt;
					array[num2] = ((udt2 != null) ? udt2.SchemaName : null);
					array[3] = ".";
					int num3 = 4;
					SqlMetaDataUdt udt3 = metaData.udt;
					array[num3] = ((udt3 != null) ? udt3.TypeName : null);
					text = string.Concat(array);
				}
				else if (metaData.cipherMD != null)
				{
					text = metaData.baseTI.metaType.TypeName;
				}
				else
				{
					text = metaData.metaType.TypeName;
				}
			}
			else
			{
				text = this.GetVersionedMetaType(metaData.metaType).TypeName;
			}
			return text;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000416FD File Offset: 0x0003F8FD
		internal virtual SqlBuffer.StorageType GetVariantInternalStorageType(int i)
		{
			return this._data[i].VariantInternalStorageType;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0004170C File Offset: 0x0003F90C
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0004171C File Offset: 0x0003F91C
		public override Type GetFieldType(int i)
		{
			SqlStatistics sqlStatistics = null;
			Type fieldTypeInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				fieldTypeInternal = this.GetFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return fieldTypeInternal;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0004176C File Offset: 0x0003F96C
		private Type GetFieldTypeInternal(_SqlMetaData metaData)
		{
			Type type;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				type = MetaType.MetaNVarChar.ClassType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					type = MetaType.MetaMaxVarBinary.ClassType;
				}
				else
				{
					type = MetaType.MetaImage.ClassType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					this.Connection.CheckGetExtendedUDTInfo(metaData, false);
					SqlMetaDataUdt udt = metaData.udt;
					type = ((udt != null) ? udt.Type : null);
				}
				else if (metaData.cipherMD != null)
				{
					type = metaData.baseTI.metaType.ClassType;
				}
				else
				{
					type = metaData.metaType.ClassType;
				}
			}
			else
			{
				type = this.GetVersionedMetaType(metaData.metaType).ClassType;
			}
			return type;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00041854 File Offset: 0x0003FA54
		internal virtual int GetLocaleId(int i)
		{
			_SqlMetaData sqlMetaData = this.MetaData[i];
			int num;
			if (sqlMetaData.cipherMD != null)
			{
				if (sqlMetaData.baseTI.collation != null)
				{
					num = sqlMetaData.baseTI.collation.LCID;
				}
				else
				{
					num = 0;
				}
			}
			else if (sqlMetaData.collation != null)
			{
				num = sqlMetaData.collation.LCID;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000418B3 File Offset: 0x0003FAB3
		public override string GetName(int i)
		{
			this.CheckMetaDataIsReady(i, false);
			return this._metaData[i].column;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000418D0 File Offset: 0x0003FAD0
		public override Type GetProviderSpecificFieldType(int i)
		{
			SqlStatistics sqlStatistics = null;
			Type providerSpecificFieldTypeInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				providerSpecificFieldTypeInternal = this.GetProviderSpecificFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return providerSpecificFieldTypeInternal;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00041920 File Offset: 0x0003FB20
		private Type GetProviderSpecificFieldTypeInternal(_SqlMetaData metaData)
		{
			Type type;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				type = MetaType.MetaNVarChar.SqlType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					type = MetaType.MetaMaxVarBinary.SqlType;
				}
				else
				{
					type = MetaType.MetaImage.SqlType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					this.Connection.CheckGetExtendedUDTInfo(metaData, false);
					SqlMetaDataUdt udt = metaData.udt;
					type = ((udt != null) ? udt.Type : null);
				}
				else if (metaData.cipherMD != null)
				{
					type = metaData.baseTI.metaType.SqlType;
				}
				else
				{
					type = metaData.metaType.SqlType;
				}
			}
			else
			{
				type = this.GetVersionedMetaType(metaData.metaType).SqlType;
			}
			return type;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00041A08 File Offset: 0x0003FC08
		public override int GetOrdinal(string name)
		{
			SqlStatistics sqlStatistics = null;
			int ordinal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._fieldNameLookup == null)
				{
					this.CheckMetaDataIsReady();
					this._fieldNameLookup = new FieldNameLookup(this, this._defaultLCID);
				}
				ordinal = this._fieldNameLookup.GetOrdinal(name);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return ordinal;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00041A6C File Offset: 0x0003FC6C
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetSqlValue(i);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00041A75 File Offset: 0x0003FC75
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetSqlValues(values);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00041A80 File Offset: 0x0003FC80
		public override DataTable GetSchemaTable()
		{
			SqlStatistics sqlStatistics = null;
			DataTable dataTable;
			using (TryEventScope.Create<int>("<sc.SqlDataReader.GetSchemaTable|API> {0}", this.ObjectID))
			{
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					if ((this._metaData == null || this._metaData._schemaTable == null) && this.MetaData != null)
					{
						this._metaData._schemaTable = this.BuildSchemaTable();
					}
					_SqlMetaDataSet metaData = this._metaData;
					dataTable = ((metaData != null) ? metaData._schemaTable : null);
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
			return dataTable;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00041B1C File Offset: 0x0003FD1C
		public override bool GetBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Boolean;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00041B34 File Offset: 0x0003FD34
		public virtual XmlReader GetXmlReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetXmlReader");
			MetaType metaType = this._metaData[i].metaType;
			if (metaType.SqlDbType != SqlDbType.Xml)
			{
				throw SQL.XmlReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this._currentStream, true, false);
			}
			this.ReadColumn(i, true, false);
			if (this._data[i].IsNull)
			{
				return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(new MemoryStream(new byte[0], false), true, false);
			}
			return this._data[i].SqlXml.CreateReader();
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00041BEC File Offset: 0x0003FDEC
		public override Stream GetStream(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetStream");
			if (this._metaData[i] != null && this._metaData[i].cipherMD != null)
			{
				throw SQL.StreamNotSupportOnEncryptedColumn(this._metaData[i].column);
			}
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsBinType || metaType.SqlDbType == SqlDbType.Timestamp) && metaType.SqlDbType != SqlDbType.Variant)
			{
				throw SQL.StreamNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return this._currentStream;
			}
			this.ReadColumn(i, true, false);
			byte[] array;
			if (this._data[i].IsNull)
			{
				array = new byte[0];
			}
			else
			{
				array = this._data[i].SqlBinary.Value;
			}
			return new MemoryStream(array, false);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00041CF1 File Offset: 0x0003FEF1
		public override byte GetByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Byte;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00041D0C File Offset: 0x0003FF0C
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			SqlStatistics sqlStatistics = null;
			long num = 0L;
			this.CheckDataIsReady(i, true, false, "GetBytes");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsLong && !metaType.IsBinType) || SqlDbType.Xml == metaType.SqlDbType)
			{
				throw SQL.NonBlobColumn(this._metaData[i].column);
			}
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				num = this.GetBytesInternal(i, dataIndex, buffer, bufferIndex, length);
				this._lastColumnWithDataChunkRead = i;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00041DB8 File Offset: 0x0003FFB8
		internal virtual long GetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			long num;
			if (!this.TryGetBytesInternal(i, dataIndex, buffer, bufferIndex, length, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00041DF0 File Offset: 0x0003FFF0
		private bool TryGetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length, out long remaining)
		{
			remaining = 0L;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag;
			try
			{
				int num = 0;
				if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (this._metaData[i] != null && this._metaData[i].cipherMD != null)
					{
						throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(this._metaData[i].column);
					}
					if (this._sharedState._nextColumnHeaderToRead <= i && !this.TryReadColumnHeader(i))
					{
						flag = false;
					}
					else
					{
						if (this._data[i] != null && this._data[i].IsNull)
						{
							throw new SqlNullValueException();
						}
						if (-1L == this._sharedState._columnDataBytesRemaining && this._metaData[i].metaType.IsPlp)
						{
							ulong num2;
							if (!this._parser.TryPlpBytesLeft(this._stateObj, out num2))
							{
								return false;
							}
							this._sharedState._columnDataBytesRemaining = (long)num2;
						}
						if (this._sharedState._columnDataBytesRemaining == 0L)
						{
							flag = true;
						}
						else if (buffer == null)
						{
							if (this._metaData[i].metaType.IsPlp)
							{
								remaining = (long)this._parser.PlpBytesTotalLength(this._stateObj);
								flag = true;
							}
							else
							{
								remaining = this._sharedState._columnDataBytesRemaining;
								flag = true;
							}
						}
						else
						{
							if (dataIndex < 0L)
							{
								throw ADP.NegativeParameter("dataIndex");
							}
							if (dataIndex < this._columnDataBytesRead)
							{
								throw ADP.NonSeqByteAccess(dataIndex, this._columnDataBytesRead, "GetBytes");
							}
							long num3 = dataIndex - this._columnDataBytesRead;
							if (num3 > this._sharedState._columnDataBytesRemaining && !this._metaData[i].metaType.IsPlp)
							{
								flag = true;
							}
							else
							{
								if (bufferIndex < 0 || bufferIndex >= buffer.Length)
								{
									throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
								}
								if (length + bufferIndex > buffer.Length)
								{
									throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
								}
								if (length < 0)
								{
									throw ADP.InvalidDataLength((long)length);
								}
								if (num3 > 0L)
								{
									if (this._metaData[i].metaType.IsPlp)
									{
										ulong num4;
										if (!this._parser.TrySkipPlpValue((ulong)num3, this._stateObj, out num4))
										{
											return false;
										}
										this._columnDataBytesRead += (long)num4;
									}
									else
									{
										if (!this._stateObj.TrySkipLongBytes(num3))
										{
											return false;
										}
										this._columnDataBytesRead += num3;
										this._sharedState._columnDataBytesRemaining -= num3;
									}
								}
								int num5;
								bool flag2 = this.TryGetBytesInternalSequential(i, buffer, bufferIndex, length, out num5);
								remaining = (long)num5;
								flag = flag2;
							}
						}
					}
				}
				else
				{
					if (dataIndex < 0L)
					{
						throw ADP.NegativeParameter("dataIndex");
					}
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
					}
					int num6 = (int)dataIndex;
					byte[] array;
					if (this._metaData[i].metaType.IsBinType)
					{
						array = this.GetSqlBinary(i).Value;
					}
					else
					{
						SqlString sqlString = this.GetSqlString(i);
						if (this._metaData[i].metaType.IsNCharType)
						{
							array = sqlString.GetUnicodeBytes();
						}
						else
						{
							array = sqlString.GetNonUnicodeBytes();
						}
					}
					num = array.Length;
					if (buffer == null)
					{
						remaining = (long)num;
						flag = true;
					}
					else if (num6 < 0 || num6 >= num)
					{
						flag = true;
					}
					else
					{
						try
						{
							if (num6 < num)
							{
								if (num6 + length > num)
								{
									num -= num6;
								}
								else
								{
									num = length;
								}
							}
							Buffer.BlockCopy(array, num6, buffer, bufferIndex, num);
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							num = array.Length;
							if (length < 0)
							{
								throw ADP.InvalidDataLength((long)length);
							}
							if (bufferIndex < 0 || bufferIndex >= buffer.Length)
							{
								throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
							}
							if (num + bufferIndex > buffer.Length)
							{
								throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
							}
							throw;
						}
						remaining = (long)num;
						flag = true;
					}
				}
			}
			catch (OutOfMemoryException ex2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (StackOverflowException ex3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
			catch (ThreadAbortException ex4)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex4);
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00042270 File Offset: 0x00040470
		internal int GetBytesInternalSequential(int i, byte[] buffer, int index, int length, long? timeoutMilliseconds = null)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			SqlStatistics sqlStatistics = null;
			int num;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(timeoutMilliseconds ?? this._defaultTimeoutMilliseconds);
				if (!this.TryReadColumnHeader(i))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				if (!this.TryGetBytesInternalSequential(i, buffer, index, length, out num))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000422FC File Offset: 0x000404FC
		internal bool TryGetBytesInternalSequential(int i, byte[] buffer, int index, int length, out int bytesRead)
		{
			bytesRead = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag;
			try
			{
				if (this._sharedState._columnDataBytesRemaining == 0L || length == 0)
				{
					bytesRead = 0;
					flag = true;
				}
				else if (this._metaData[i].metaType.IsPlp)
				{
					bool flag2 = this._stateObj.TryReadPlpBytes(ref buffer, index, length, out bytesRead);
					this._columnDataBytesRead += (long)bytesRead;
					ulong num;
					if (!flag2)
					{
						flag = false;
					}
					else if (!this._parser.TryPlpBytesLeft(this._stateObj, out num))
					{
						this._sharedState._columnDataBytesRemaining = -1L;
						flag = false;
					}
					else
					{
						this._sharedState._columnDataBytesRemaining = (long)num;
						flag = true;
					}
				}
				else
				{
					int num2 = (int)Math.Min((long)length, this._sharedState._columnDataBytesRemaining);
					bool flag3 = this._stateObj.TryReadByteArray(MemoryExtensions.AsSpan<byte>(buffer, index), num2, out bytesRead);
					this._columnDataBytesRead += (long)bytesRead;
					this._sharedState._columnDataBytesRemaining -= (long)bytesRead;
					flag = flag3;
				}
			}
			catch (OutOfMemoryException ex)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex);
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00042494 File Offset: 0x00040694
		public override TextReader GetTextReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetTextReader");
			MetaType metaType;
			if (this._metaData[i].cipherMD != null)
			{
				metaType = this._metaData[i].baseTI.metaType;
			}
			else
			{
				metaType = this._metaData[i].metaType;
			}
			if ((!metaType.IsCharType && metaType.SqlDbType != SqlDbType.Variant) || metaType.SqlDbType == SqlDbType.Xml)
			{
				throw SQL.TextReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType == SqlDbType.Variant || !this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this.ReadColumn(i, true, false);
				string text;
				if (this._data[i].IsNull)
				{
					text = string.Empty;
				}
				else
				{
					text = this._data[i].SqlString.Value;
				}
				return new StringReader(text);
			}
			if (this._metaData[i].cipherMD != null)
			{
				throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(this._metaData[i].column);
			}
			Encoding encoding;
			if (metaType.IsNCharType)
			{
				encoding = SqlUnicodeEncoding.SqlUnicodeEncodingInstance;
			}
			else
			{
				encoding = this._metaData[i].encoding;
			}
			this._currentTextReader = new SqlSequentialTextReader(this, i, encoding);
			this._lastColumnWithDataChunkRead = i;
			return this._currentTextReader;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00025577 File Offset: 0x00023777
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000425DC File Offset: 0x000407DC
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			SqlStatistics sqlStatistics = null;
			this.CheckMetaDataIsReady(i, false);
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			MetaType metaType;
			if (this._metaData[i].cipherMD != null)
			{
				metaType = this._metaData[i].baseTI.metaType;
			}
			else
			{
				metaType = this._metaData[i].metaType;
			}
			SqlDbType sqlDbType;
			if (this._metaData[i].cipherMD != null)
			{
				sqlDbType = this._metaData[i].baseTI.type;
			}
			else
			{
				sqlDbType = this._metaData[i].type;
			}
			long num2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (metaType.IsPlp && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (this._metaData[i].cipherMD != null)
					{
						throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(this._metaData[i].column);
					}
					if (bufferIndex < 0 || (buffer != null && bufferIndex >= buffer.Length))
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (buffer != null && length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					long num;
					if (sqlDbType == SqlDbType.Xml)
					{
						try
						{
							this.CheckDataIsReady(i, true, false, "GetChars");
						}
						catch (Exception ex)
						{
							if (ADP.IsCatchableExceptionType(ex))
							{
								throw new TargetInvocationException(ex);
							}
							throw;
						}
						num = this.GetStreamingXmlChars(i, dataIndex, buffer, bufferIndex, length);
					}
					else
					{
						this.CheckDataIsReady(i, true, false, "GetChars");
						num = this.GetCharsFromPlpData(i, dataIndex, buffer, bufferIndex, length);
					}
					this._lastColumnWithDataChunkRead = i;
					num2 = num;
				}
				else
				{
					if (this._sharedState._nextColumnDataToRead == i + 1 && this._sharedState._nextColumnHeaderToRead == i + 1 && this._columnDataChars != null && this.IsCommandBehavior(CommandBehavior.SequentialAccess) && dataIndex < this._columnDataCharsRead)
					{
						throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
					}
					if (this._columnDataCharsIndex != i)
					{
						string value = this.GetSqlString(i).Value;
						this._columnDataChars = value.ToCharArray();
						this._columnDataCharsRead = 0L;
						this._columnDataCharsIndex = i;
					}
					int num3 = this._columnDataChars.Length;
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num3, dataIndex, "dataIndex");
					}
					int num4 = (int)dataIndex;
					if (buffer == null)
					{
						num2 = (long)num3;
					}
					else if (num4 < 0 || num4 >= num3)
					{
						num2 = 0L;
					}
					else
					{
						try
						{
							if (num4 < num3)
							{
								if (num4 + length > num3)
								{
									num3 -= num4;
								}
								else
								{
									num3 = length;
								}
							}
							Array.Copy(this._columnDataChars, num4, buffer, bufferIndex, num3);
							this._columnDataCharsRead += (long)num3;
						}
						catch (Exception ex2)
						{
							if (!ADP.IsCatchableExceptionType(ex2))
							{
								throw;
							}
							num3 = this._columnDataChars.Length;
							if (length < 0)
							{
								throw ADP.InvalidDataLength((long)length);
							}
							if (bufferIndex < 0 || bufferIndex >= buffer.Length)
							{
								throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
							}
							if (num3 + bufferIndex > buffer.Length)
							{
								throw ADP.InvalidBufferSizeOrIndex(num3, bufferIndex);
							}
							throw;
						}
						num2 = (long)num3;
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num2;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0004293C File Offset: 0x00040B3C
		private long GetCharsFromPlpData(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			long num;
			try
			{
				if (!this._metaData[i].metaType.IsCharType)
				{
					throw SQL.NonCharColumn(this._metaData[i].column);
				}
				if (this._sharedState._nextColumnHeaderToRead <= i)
				{
					this.ReadColumnHeader(i);
				}
				if (this._data[i] != null && this._data[i].IsNull)
				{
					throw new SqlNullValueException();
				}
				if (dataIndex < this._columnDataCharsRead)
				{
					throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
				}
				if (dataIndex == 0L)
				{
					this._stateObj._plpdecoder = null;
				}
				bool isNCharType = this._metaData[i].metaType.IsNCharType;
				if (-1L == this._sharedState._columnDataBytesRemaining)
				{
					this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
				}
				if (this._sharedState._columnDataBytesRemaining == 0L)
				{
					this._stateObj._plpdecoder = null;
					num = 0L;
				}
				else if (buffer == null)
				{
					long num2 = (long)this._parser.PlpBytesTotalLength(this._stateObj);
					num = ((isNCharType && num2 > 0L) ? (num2 >> 1) : num2);
				}
				else
				{
					long num2;
					if (dataIndex > this._columnDataCharsRead)
					{
						this._stateObj._plpdecoder = null;
						num2 = dataIndex - this._columnDataCharsRead;
						num2 = (isNCharType ? (num2 << 1) : num2);
						num2 = (long)this._parser.SkipPlpValue((ulong)num2, this._stateObj);
						this._columnDataBytesRead += num2;
						this._columnDataCharsRead += ((isNCharType && num2 > 0L) ? (num2 >> 1) : num2);
					}
					num2 = (long)length;
					if (isNCharType)
					{
						num2 = (long)this._parser.ReadPlpUnicodeChars(ref buffer, bufferIndex, length, this._stateObj);
						this._columnDataBytesRead += num2 << 1;
					}
					else
					{
						num2 = (long)this._parser.ReadPlpAnsiChars(ref buffer, bufferIndex, length, this._metaData[i], this._stateObj);
						this._columnDataBytesRead += num2 << 1;
					}
					this._columnDataCharsRead += num2;
					this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
					num = num2;
				}
			}
			catch (OutOfMemoryException ex)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex);
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
			return num;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00042C0C File Offset: 0x00040E0C
		internal long GetStreamingXmlChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._streamingXml != null && this._streamingXml.ColumnOrdinal != i)
			{
				this._streamingXml.Close();
				this._streamingXml = null;
			}
			SqlStreamingXml sqlStreamingXml;
			if (this._streamingXml == null)
			{
				sqlStreamingXml = new SqlStreamingXml(i, this);
			}
			else
			{
				sqlStreamingXml = this._streamingXml;
			}
			long chars = sqlStreamingXml.GetChars(dataIndex, buffer, bufferIndex, length);
			if (this._streamingXml == null)
			{
				this._streamingXml = sqlStreamingXml;
			}
			return chars;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00025577 File Offset: 0x00023777
		[EditorBrowsable(EditorBrowsableState.Never)]
		IDataReader IDataRecord.GetData(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00042C7C File Offset: 0x00040E7C
		public override DateTime GetDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			DateTime dateTime = this._data[i].DateTime;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].Is2008DateTimeType)
			{
				object @string = this._data[i].String;
				dateTime = (DateTime)@string;
			}
			return dateTime;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00042CD6 File Offset: 0x00040ED6
		public override decimal GetDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Decimal;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00042CEE File Offset: 0x00040EEE
		public override double GetDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Double;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00042D06 File Offset: 0x00040F06
		public override float GetFloat(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Single;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00042D1E File Offset: 0x00040F1E
		public override Guid GetGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Guid;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00042D36 File Offset: 0x00040F36
		public override short GetInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int16;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00042D4E File Offset: 0x00040F4E
		public override int GetInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int32;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00042D66 File Offset: 0x00040F66
		public override long GetInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int64;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00042D7E File Offset: 0x00040F7E
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlBoolean;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00042D96 File Offset: 0x00040F96
		public virtual SqlBinary GetSqlBinary(int i)
		{
			this.ReadColumn(i, true, true);
			return this._data[i].SqlBinary;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00042DAE File Offset: 0x00040FAE
		public virtual SqlByte GetSqlByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlByte;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00042DC8 File Offset: 0x00040FC8
		public virtual SqlBytes GetSqlBytes(int i)
		{
			this.ReadColumn(i, true, false);
			SqlBinary sqlBinary = this._data[i].SqlBinary;
			return new SqlBytes(sqlBinary);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00042DF4 File Offset: 0x00040FF4
		public virtual SqlChars GetSqlChars(int i)
		{
			this.ReadColumn(i, true, false);
			SqlString sqlString;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].Is2008DateTimeType)
			{
				sqlString = this._data[i].Sql2008DateTimeSqlString;
			}
			else
			{
				sqlString = this._data[i].SqlString;
			}
			return new SqlChars(sqlString);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00042E4E File Offset: 0x0004104E
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDateTime;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00042E66 File Offset: 0x00041066
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDecimal;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00042E7E File Offset: 0x0004107E
		public virtual SqlGuid GetSqlGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00042E96 File Offset: 0x00041096
		public virtual SqlDouble GetSqlDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDouble;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00042EAE File Offset: 0x000410AE
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt16;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00042EC6 File Offset: 0x000410C6
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt32;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00042EDE File Offset: 0x000410DE
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt64;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00042EF6 File Offset: 0x000410F6
		public virtual SqlMoney GetSqlMoney(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlMoney;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00042F0E File Offset: 0x0004110E
		public virtual SqlSingle GetSqlSingle(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlSingle;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00042F28 File Offset: 0x00041128
		public virtual SqlString GetSqlString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].Is2008DateTimeType)
			{
				return this._data[i].Sql2008DateTimeSqlString;
			}
			return this._data[i].SqlString;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00042F7C File Offset: 0x0004117C
		public virtual SqlXml GetSqlXml(int i)
		{
			this.ReadColumn(i, true, false);
			SqlXml sqlXml;
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				sqlXml = (this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml());
			}
			else
			{
				SqlXml sqlXml2 = (this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml());
				object @string = this._data[i].String;
				sqlXml = (SqlXml)@string;
			}
			return sqlXml;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004300C File Offset: 0x0004120C
		public virtual object GetSqlValue(int i)
		{
			SqlStatistics sqlStatistics = null;
			object sqlValueInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				sqlValueInternal = this.GetSqlValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlValueInternal;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00043058 File Offset: 0x00041258
		private object GetSqlValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetSqlValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000430A4 File Offset: 0x000412A4
		private object GetSqlValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				return data.Sql2008DateTimeSqlString;
			}
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				return data.SqlValue;
			}
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type != SqlDbType.Udt)
				{
					return data.SqlValue;
				}
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.CheckGetExtendedUDTInfo(metaData, true);
					return connection.GetUdtValue(data.Value, metaData, false);
				}
				throw ADP.DataReaderClosed("GetSqlValueFromSqlBufferInternal");
			}
			else
			{
				if (metaData.type == SqlDbType.Xml)
				{
					return data.SqlString;
				}
				return data.SqlValue;
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00043154 File Offset: 0x00041354
		public virtual int GetSqlValues(object[] values)
		{
			SqlStatistics sqlStatistics = null;
			int num2;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckDataIsReady();
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				int num = ((values.Length < this._metaData.VisibleColumnCount) ? values.Length : this._metaData.VisibleColumnCount);
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.GetVisibleColumnIndex(i)] = this.GetSqlValueInternal(i);
				}
				num2 = num;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num2;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000431F0 File Offset: 0x000413F0
		public override string GetString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].Is2008DateTimeType)
			{
				return this._data[i].Sql2008DateTimeString;
			}
			return this._data[i].String;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00043244 File Offset: 0x00041444
		public override T GetFieldValue<T>(int i)
		{
			SqlStatistics sqlStatistics = null;
			T fieldValueInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				fieldValueInternal = this.GetFieldValueInternal<T>(i, false);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return fieldValueInternal;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00043290 File Offset: 0x00041490
		public override object GetValue(int i)
		{
			SqlStatistics sqlStatistics = null;
			object valueInternal;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				valueInternal = this.GetValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return valueInternal;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000432DC File Offset: 0x000414DC
		public virtual TimeSpan GetTimeSpan(int i)
		{
			this.ReadColumn(i, true, false);
			TimeSpan timeSpan = this._data[i].Time;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				object @string = this._data[i].String;
				timeSpan = (TimeSpan)@string;
			}
			return timeSpan;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00043324 File Offset: 0x00041524
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			this.ReadColumn(i, true, false);
			DateTimeOffset dateTimeOffset = this._data[i].DateTimeOffset;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				object @string = this._data[i].String;
				dateTimeOffset = (DateTimeOffset)@string;
			}
			return dateTimeOffset;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004336C File Offset: 0x0004156C
		private object GetValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000433B8 File Offset: 0x000415B8
		private object GetValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				if (data.IsNull)
				{
					return DBNull.Value;
				}
				return data.Sql2008DateTimeString;
			}
			else
			{
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
				{
					return data.Value;
				}
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2000)
				{
					return data.Value;
				}
				if (metaData.type != SqlDbType.Udt)
				{
					return data.Value;
				}
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.CheckGetExtendedUDTInfo(metaData, true);
					return connection.GetUdtValue(data.Value, metaData, true);
				}
				throw ADP.DataReaderClosed("GetValueFromSqlBufferInternal");
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004345C File Offset: 0x0004165C
		private T GetFieldValueInternal<T>(int i, bool isAsync)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			bool flag = typeof(T) == typeof(XmlReader) || typeof(T) == typeof(TextReader) || typeof(T) == typeof(Stream);
			if (!this.TryReadColumn(i, false, false, flag))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i], isAsync);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000434FC File Offset: 0x000416FC
		private T GetFieldValueFromSqlBufferInternal<T>(SqlBuffer data, _SqlMetaData metaData, bool isAsync)
		{
			Type typeFromStorageType = data.GetTypeFromStorageType(false);
			if (typeof(T) == typeof(int) && typeFromStorageType == typeof(int))
			{
				return data.Int32As<T>();
			}
			if (typeof(T) == typeof(byte) && typeFromStorageType == typeof(byte))
			{
				return data.ByteAs<T>();
			}
			if (typeof(T) == typeof(short) && typeFromStorageType == typeof(short))
			{
				return data.Int16As<T>();
			}
			if (typeof(T) == typeof(long) && typeFromStorageType == typeof(long))
			{
				return data.Int64As<T>();
			}
			if (typeof(T) == typeof(bool) && typeFromStorageType == typeof(bool))
			{
				return data.BooleanAs<T>();
			}
			if (typeof(T) == typeof(double) && typeFromStorageType == typeof(double))
			{
				return data.DoubleAs<T>();
			}
			if (typeof(T) == typeof(float) && typeFromStorageType == typeof(float))
			{
				return data.SingleAs<T>();
			}
			if (typeof(T) == typeof(Guid) && typeFromStorageType == typeof(Guid))
			{
				return (T)((object)data.Guid);
			}
			if (typeof(T) == typeof(decimal) && typeFromStorageType == typeof(decimal))
			{
				return (T)((object)data.Decimal);
			}
			if (typeof(T) == typeof(DateTimeOffset) && typeFromStorageType == typeof(DateTimeOffset) && this._typeSystem > SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				return (T)((object)data.DateTimeOffset);
			}
			if (typeof(T) == typeof(DateTime) && typeFromStorageType == typeof(DateTime) && this._typeSystem > SqlConnectionString.TypeSystem.SQLServer2005 && metaData.Is2008DateTimeType)
			{
				return (T)((object)data.DateTime);
			}
			if (typeof(T) == typeof(XmlReader))
			{
				if (metaData.metaType.SqlDbType != SqlDbType.Xml)
				{
					throw SQL.XmlReaderNotSupportOnColumnType(metaData.column);
				}
				if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					this._currentStream = new SqlSequentialStream(this, metaData.ordinal);
					this._lastColumnWithDataChunkRead = metaData.ordinal;
					return (T)((object)SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this._currentStream, true, isAsync));
				}
				if (data.IsNull)
				{
					return (T)((object)SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(new MemoryStream(Array.Empty<byte>(), false), true, isAsync));
				}
				return (T)((object)data.SqlXml.CreateReader());
			}
			else if (typeof(T) == typeof(TextReader))
			{
				MetaType metaType = metaData.metaType;
				if (metaData.cipherMD != null)
				{
					metaType = metaData.baseTI.metaType;
				}
				if ((!metaType.IsCharType && metaType.SqlDbType != SqlDbType.Variant) || metaType.SqlDbType == SqlDbType.Xml)
				{
					throw SQL.TextReaderNotSupportOnColumnType(metaData.column);
				}
				if (metaType.SqlDbType == SqlDbType.Variant || !this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					string text = (data.IsNull ? string.Empty : data.SqlString.Value);
					return (T)((object)new StringReader(text));
				}
				if (metaData.cipherMD != null)
				{
					throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(metaData.column);
				}
				Encoding encoding = SqlUnicodeEncoding.SqlUnicodeEncodingInstance;
				if (!metaType.IsNCharType)
				{
					encoding = metaData.encoding;
				}
				this._currentTextReader = new SqlSequentialTextReader(this, metaData.ordinal, encoding);
				this._lastColumnWithDataChunkRead = metaData.ordinal;
				return (T)((object)this._currentTextReader);
			}
			else if (typeof(T) == typeof(Stream))
			{
				if (metaData != null && metaData.cipherMD != null)
				{
					throw SQL.StreamNotSupportOnEncryptedColumn(metaData.column);
				}
				MetaType metaType2 = metaData.metaType;
				if ((!metaType2.IsBinType || metaType2.SqlDbType == SqlDbType.Timestamp) && metaType2.SqlDbType != SqlDbType.Variant)
				{
					throw SQL.StreamNotSupportOnColumnType(metaData.column);
				}
				if (metaType2.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					this._currentStream = new SqlSequentialStream(this, metaData.ordinal);
					this._lastColumnWithDataChunkRead = metaData.ordinal;
					return (T)((object)this._currentStream);
				}
				byte[] array = (data.IsNull ? Array.Empty<byte>() : data.SqlBinary.Value);
				return (T)((object)new MemoryStream(array, false));
			}
			else
			{
				if (typeof(INullable).IsAssignableFrom(typeof(T)))
				{
					object obj = this.GetSqlValueFromSqlBufferInternal(data, metaData);
					if (typeof(T) == typeof(SqlString))
					{
						SqlXml sqlXml = obj as SqlXml;
						if (sqlXml != null)
						{
							if (sqlXml.IsNull)
							{
								obj = SqlString.Null;
							}
							else
							{
								obj = new SqlString(sqlXml.Value);
							}
						}
					}
					return (T)((object)obj);
				}
				T t;
				try
				{
					t = (T)((object)this.GetValueFromSqlBufferInternal(data, metaData));
				}
				catch (InvalidCastException obj2) when (data.IsNull)
				{
					throw SQL.SqlNullValue();
				}
				return t;
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00043AD0 File Offset: 0x00041CD0
		public override int GetValues(object[] values)
		{
			SqlStatistics sqlStatistics = null;
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			int num3;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.CheckMetaDataIsReady();
				int num = ((values.Length < this._metaData.VisibleColumnCount) ? values.Length : this._metaData.VisibleColumnCount);
				int num2 = num - 1;
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				this._commandBehavior &= ~CommandBehavior.SequentialAccess;
				if (!this.TryReadColumn(num2, false, false, false))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				for (int i = 0; i < num; i++)
				{
					int visibleColumnIndex = this._metaData.GetVisibleColumnIndex(i);
					values[i] = this.GetValueFromSqlBufferInternal(this._data[visibleColumnIndex], this._metaData[visibleColumnIndex]);
					if (flag && i < num2)
					{
						this._data[i].Clear();
						if (visibleColumnIndex > i && visibleColumnIndex > 0)
						{
							this._data[visibleColumnIndex - 1].Clear();
						}
					}
				}
				num3 = num;
			}
			finally
			{
				if (flag)
				{
					this._commandBehavior |= CommandBehavior.SequentialAccess;
				}
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return num3;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00043C00 File Offset: 0x00041E00
		private MetaType GetVersionedMetaType(MetaType actualMetaType)
		{
			MetaType metaType;
			if (actualMetaType == MetaType.MetaUdt)
			{
				metaType = MetaType.MetaVarBinary;
			}
			else if (actualMetaType == MetaType.MetaXml)
			{
				metaType = MetaType.MetaNText;
			}
			else if (actualMetaType == MetaType.MetaMaxVarBinary)
			{
				metaType = MetaType.MetaImage;
			}
			else if (actualMetaType == MetaType.MetaMaxVarChar)
			{
				metaType = MetaType.MetaText;
			}
			else if (actualMetaType == MetaType.MetaMaxNVarChar)
			{
				metaType = MetaType.MetaNText;
			}
			else
			{
				metaType = actualMetaType;
			}
			return metaType;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00043C64 File Offset: 0x00041E64
		private bool TryHasMoreResults(out bool moreResults)
		{
			if (this._parser != null)
			{
				bool flag;
				if (!this.TryHasMoreRows(out flag))
				{
					moreResults = false;
					return false;
				}
				if (flag)
				{
					moreResults = false;
					return true;
				}
				while (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreResults = false;
						return false;
					}
					if (b <= 210)
					{
						if (b == 129)
						{
							moreResults = true;
							return true;
						}
						if (b - 209 <= 1)
						{
							moreResults = true;
							return true;
						}
					}
					else
					{
						if (b == 211)
						{
							if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.Null)
							{
								this._altMetaDataSetCollection.metaDataSet = this._metaData;
								this._metaData = null;
							}
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.AltRow;
							this._hasRows = true;
							moreResults = true;
							return true;
						}
						if (b == 253)
						{
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
							this._metaData = null;
							this._altMetaDataSetCollection = null;
							moreResults = true;
							return true;
						}
					}
					if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					bool flag2;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
					{
						moreResults = false;
						return false;
					}
				}
			}
			moreResults = false;
			return true;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00043D88 File Offset: 0x00041F88
		private bool TryHasMoreRows(out bool moreRows)
		{
			if (this._parser != null)
			{
				if (this._sharedState._dataReady)
				{
					moreRows = true;
					return true;
				}
				SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
				{
					moreRows = true;
					return true;
				}
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.Done)
				{
					moreRows = false;
					return true;
				}
				if (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreRows = false;
						return false;
					}
					bool flag = false;
					while (b == 253 || b == 254 || b == 255 || (!flag && (b == 228 || b == 227 || b == 169 || b == 170 || b == 171)))
					{
						if (b == 253 || b == 254 || b == 255)
						{
							flag = true;
						}
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							throw ADP.ClosedConnectionError();
						}
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
						{
							moreRows = false;
							return false;
						}
						if (!this._stateObj._pendingData)
						{
							break;
						}
						if (!this._stateObj.TryPeekByte(out b))
						{
							moreRows = false;
							return false;
						}
					}
					if (this.IsRowToken(b))
					{
						moreRows = true;
						return true;
					}
				}
			}
			moreRows = false;
			return true;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00043EE1 File Offset: 0x000420E1
		private bool IsRowToken(byte token)
		{
			return 209 == token || 210 == token;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00043EF8 File Offset: 0x000420F8
		public override bool IsDBNull(int i)
		{
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnHeaderToRead > i + 1 || this._lastColumnWithDataChunkRead > i))
			{
				this.CheckMetaDataIsReady(i, false);
			}
			else
			{
				this.CheckHeaderIsReady(i, false, "IsDBNull");
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				this.ReadColumnHeader(i);
			}
			return this._data[i].IsNull;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00043F5F File Offset: 0x0004215F
		protected internal bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00043F6C File Offset: 0x0004216C
		public override bool NextResult()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool flag;
			if (!this.TryNextResult(out flag))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return flag;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00043F9C File Offset: 0x0004219C
		private bool TryNextResult(out bool more)
		{
			SqlStatistics sqlStatistics = null;
			bool flag2;
			using (TryEventScope.Create<int>("<sc.SqlDataReader.NextResult|API> {0}", this.ObjectID))
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this.SetTimeout(this._defaultTimeoutMilliseconds);
					if (this.IsClosed)
					{
						throw ADP.DataReaderClosed("NextResult");
					}
					this._fieldNameLookup = null;
					bool flag = false;
					this._hasRows = false;
					if (this.IsCommandBehavior(CommandBehavior.SingleResult))
					{
						if (!this.TryCloseInternal(false))
						{
							more = false;
							flag2 = false;
						}
						else
						{
							this.ClearMetaData();
							more = flag;
							flag2 = true;
						}
					}
					else
					{
						if (this._parser != null)
						{
							bool flag3 = true;
							while (flag3)
							{
								if (!this.TryReadInternal(false, out flag3))
								{
									more = false;
									return false;
								}
							}
						}
						if (this._parser != null)
						{
							bool flag4;
							if (!this.TryHasMoreResults(out flag4))
							{
								more = false;
								return false;
							}
							if (flag4)
							{
								this._metaDataConsumed = false;
								this._browseModeInfoConsumed = false;
								SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
								if (altRowStatus != SqlDataReader.ALTROWSTATUS.AltRow)
								{
									if (altRowStatus != SqlDataReader.ALTROWSTATUS.Done)
									{
										if (!this.TryConsumeMetaData())
										{
											more = false;
											return false;
										}
										if (this._metaData == null)
										{
											more = false;
											return true;
										}
									}
									else
									{
										this._metaData = this._altMetaDataSetCollection.metaDataSet;
										this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
									}
								}
								else
								{
									int num;
									if (!this._parser.TryGetAltRowId(this._stateObj, out num))
									{
										more = false;
										return false;
									}
									_SqlMetaDataSet altMetaData = this._altMetaDataSetCollection.GetAltMetaData(num);
									if (altMetaData != null)
									{
										this._metaData = altMetaData;
									}
								}
								flag = true;
							}
							else
							{
								if (!this.TryCloseInternal(false))
								{
									more = false;
									return false;
								}
								if (!this.TrySetMetaData(null, false))
								{
									more = false;
									return false;
								}
							}
						}
						else
						{
							this.ClearMetaData();
						}
						more = flag;
						flag2 = true;
					}
				}
				catch (OutOfMemoryException ex)
				{
					this._isClosed = true;
					if (this._connection != null)
					{
						this._connection.Abort(ex);
					}
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._isClosed = true;
					if (this._connection != null)
					{
						this._connection.Abort(ex2);
					}
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._isClosed = true;
					if (this._connection != null)
					{
						this._connection.Abort(ex3);
					}
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
			return flag2;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00044234 File Offset: 0x00042434
		public override bool Read()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool flag;
			if (!this.TryReadInternal(true, out flag))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return flag;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00044264 File Offset: 0x00042464
		private bool TryReadInternal(bool setTimeout, out bool more)
		{
			SqlStatistics sqlStatistics = null;
			bool flag3;
			using (TryEventScope.Create<int>("<sc.SqlDataReader.Read|API> {0}", this.ObjectID))
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					if (this._parser != null)
					{
						if (setTimeout)
						{
							this.SetTimeout(this._defaultTimeoutMilliseconds);
						}
						if (this._sharedState._dataReady && !this.TryCleanPartialRead())
						{
							more = false;
							return false;
						}
						SqlBuffer.Clear(this._data);
						this._sharedState._nextColumnHeaderToRead = 0;
						this._sharedState._nextColumnDataToRead = 0;
						this._sharedState._columnDataBytesRemaining = -1L;
						this._lastColumnWithDataChunkRead = -1;
						if (!this._haltRead)
						{
							bool flag;
							if (!this.TryHasMoreRows(out flag))
							{
								more = false;
								return false;
							}
							if (flag)
							{
								while (this._stateObj._pendingData)
								{
									if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
									{
										this._altRowStatus = SqlDataReader.ALTROWSTATUS.Done;
										this._sharedState._dataReady = true;
										break;
									}
									if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
									{
										more = false;
										return false;
									}
									if (this._sharedState._dataReady)
									{
										break;
									}
								}
								if (this._sharedState._dataReady)
								{
									this._haltRead = this.IsCommandBehavior(CommandBehavior.SingleRow);
									more = true;
									return true;
								}
							}
							if (!this._stateObj._pendingData && !this.TryCloseInternal(false))
							{
								more = false;
								return false;
							}
						}
						else
						{
							bool flag2;
							if (!this.TryHasMoreRows(out flag2))
							{
								more = false;
								return false;
							}
							while (flag2)
							{
								while (this._stateObj._pendingData && !this._sharedState._dataReady)
								{
									if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
									{
										more = false;
										return false;
									}
								}
								if (this._sharedState._dataReady && !this.TryCleanPartialRead())
								{
									more = false;
									return false;
								}
								SqlBuffer.Clear(this._data);
								this._sharedState._nextColumnHeaderToRead = 0;
								if (!this.TryHasMoreRows(out flag2))
								{
									more = false;
									return false;
								}
							}
							this._haltRead = false;
						}
					}
					else if (this.IsClosed)
					{
						throw ADP.DataReaderClosed("Read");
					}
					more = false;
					flag3 = true;
				}
				catch (OutOfMemoryException ex)
				{
					this._isClosed = true;
					SqlConnection connection = this._connection;
					if (connection != null)
					{
						connection.Abort(ex);
					}
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._isClosed = true;
					SqlConnection connection2 = this._connection;
					if (connection2 != null)
					{
						connection2.Abort(ex2);
					}
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._isClosed = true;
					SqlConnection connection3 = this._connection;
					if (connection3 != null)
					{
						connection3.Abort(ex3);
					}
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
			return flag3;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000445A0 File Offset: 0x000427A0
		private void ReadColumn(int i, bool setTimeout = true, bool allowPartiallyReadColumn = false)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, setTimeout, allowPartiallyReadColumn, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000445D0 File Offset: 0x000427D0
		private bool TryReadColumn(int i, bool setTimeout, bool allowPartiallyReadColumn = false, bool forStreaming = false)
		{
			this.CheckDataIsReady(i, allowPartiallyReadColumn, true, null);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (setTimeout)
				{
					this.SetTimeout(this._defaultTimeoutMilliseconds);
				}
				if (!this.TryReadColumnInternal(i, false, forStreaming))
				{
					return false;
				}
			}
			catch (OutOfMemoryException ex)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex);
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
			return true;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00044694 File Offset: 0x00042894
		private bool TryReadColumnData()
		{
			if (!this._data[this._sharedState._nextColumnDataToRead].IsNull)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnDataToRead];
				if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)this._sharedState._columnDataBytesRemaining, this._stateObj, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, sqlMetaData.column, this._command))
				{
					return false;
				}
				this._sharedState._columnDataBytesRemaining = 0L;
			}
			this._sharedState._nextColumnDataToRead++;
			return true;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00044748 File Offset: 0x00042948
		private void ReadColumnHeader(int i)
		{
			if (!this.TryReadColumnHeader(i))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00044768 File Offset: 0x00042968
		private bool TryReadColumnHeader(int i)
		{
			if (!this._sharedState._dataReady)
			{
				throw SQL.InvalidRead();
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag;
			try
			{
				flag = this.TryReadColumnInternal(i, true, false);
			}
			catch (OutOfMemoryException ex)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex);
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex2);
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(ex3);
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004481C File Offset: 0x00042A1C
		internal bool TryReadColumnInternal(int i, bool readHeaderOnly, bool forStreaming)
		{
			if (i < this._sharedState._nextColumnHeaderToRead)
			{
				return i != this._sharedState._nextColumnDataToRead || readHeaderOnly || this.TryReadColumnData();
			}
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			if (flag)
			{
				if (0 < this._sharedState._nextColumnDataToRead)
				{
					this._data[this._sharedState._nextColumnDataToRead - 1].Clear();
				}
				if (this._lastColumnWithDataChunkRead > -1 && i > this._lastColumnWithDataChunkRead)
				{
					this.CloseActiveSequentialStreamAndTextReader();
				}
			}
			else if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead && !this.TryReadColumnData())
			{
				return false;
			}
			if (!this.TryResetBlobState())
			{
				return false;
			}
			for (;;)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnHeaderToRead];
				if (flag)
				{
					if (this._sharedState._nextColumnHeaderToRead < i)
					{
						if (!this._parser.TrySkipValue(sqlMetaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
						{
							break;
						}
						this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
						this._sharedState._nextColumnHeaderToRead++;
					}
					else if (this._sharedState._nextColumnHeaderToRead == i)
					{
						bool flag2;
						ulong num;
						if (!this._parser.TryProcessColumnHeader(sqlMetaData, this._stateObj, this._sharedState._nextColumnHeaderToRead, out flag2, out num))
						{
							return false;
						}
						this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
						this._sharedState._nextColumnHeaderToRead++;
						this._sharedState._columnDataBytesRemaining = (long)num;
						if (flag2)
						{
							if (sqlMetaData.type != SqlDbType.Timestamp)
							{
								TdsParser.GetNullSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, this._parser.Connection);
							}
						}
						else if (!readHeaderOnly && !forStreaming)
						{
							if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)num, this._stateObj, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, sqlMetaData.column, null))
							{
								return false;
							}
							this._sharedState._columnDataBytesRemaining = 0L;
							this._sharedState._nextColumnDataToRead++;
						}
						else
						{
							this._sharedState._columnDataBytesRemaining = (long)num;
						}
					}
				}
				else
				{
					bool flag3;
					ulong num2;
					if (!this._parser.TryProcessColumnHeader(sqlMetaData, this._stateObj, this._sharedState._nextColumnHeaderToRead, out flag3, out num2))
					{
						return false;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
					if (flag3 && (!LocalAppContextSwitches.LegacyRowVersionNullBehavior || sqlMetaData.type != SqlDbType.Timestamp))
					{
						TdsParser.GetNullSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, this._parser.Connection);
						if (!readHeaderOnly)
						{
							this._sharedState._nextColumnDataToRead++;
						}
					}
					else if (i > this._sharedState._nextColumnDataToRead || !readHeaderOnly)
					{
						if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)num2, this._stateObj, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, sqlMetaData.column, null))
						{
							return false;
						}
						this._sharedState._nextColumnDataToRead++;
					}
					else
					{
						this._sharedState._columnDataBytesRemaining = (long)num2;
					}
				}
				if (this._snapshot != null)
				{
					this._snapshot = null;
					this.PrepareAsyncInvocation(true);
				}
				if (this._sharedState._nextColumnHeaderToRead > i)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00044BDC File Offset: 0x00042DDC
		private bool WillHaveEnoughData(int targetColumn, bool headerOnly = false)
		{
			if (this._lastColumnWithDataChunkRead == this._sharedState._nextColumnDataToRead && this._metaData[this._lastColumnWithDataChunkRead].metaType.IsPlp)
			{
				return false;
			}
			int num = Math.Min(checked(this._stateObj._inBytesRead - this._stateObj._inBytesUsed), this._stateObj._inBytesPacket);
			num--;
			if (targetColumn >= this._sharedState._nextColumnDataToRead && this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._columnDataBytesRemaining > (long)num)
				{
					return false;
				}
				checked
				{
					num -= (int)this._sharedState._columnDataBytesRemaining;
				}
			}
			int num2 = this._sharedState._nextColumnHeaderToRead;
			while (num >= 0 && num2 <= targetColumn)
			{
				checked
				{
					if (!this._stateObj.IsNullCompressionBitSet(num2))
					{
						MetaType metaType = this._metaData[num2].metaType;
						if (metaType.IsLong || metaType.IsPlp || metaType.SqlDbType == SqlDbType.Udt || metaType.SqlDbType == SqlDbType.Structured)
						{
							return false;
						}
						byte b = this._metaData[num2].tdsType & 48;
						int num3;
						if (b == 32 || b == 0)
						{
							if ((this._metaData[num2].tdsType & 128) != 0)
							{
								num3 = 2;
							}
							else if ((this._metaData[num2].tdsType & 12) == 0)
							{
								num3 = 4;
							}
							else
							{
								num3 = 1;
							}
						}
						else
						{
							num3 = 0;
						}
						num -= num3;
						if (num2 < targetColumn || !headerOnly)
						{
							num -= this._metaData[num2].length;
						}
					}
				}
				num2++;
			}
			return num >= 0;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00044D7C File Offset: 0x00042F7C
		private bool TryResetBlobState()
		{
			if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._nextColumnHeaderToRead > 0 && this._metaData[this._sharedState._nextColumnHeaderToRead - 1].metaType.IsPlp)
				{
					ulong num;
					if (this._stateObj._longlen != 0UL && !this._stateObj.Parser.TrySkipPlpValue(18446744073709551615UL, this._stateObj, out num))
					{
						return false;
					}
					if (this._streamingXml != null)
					{
						SqlStreamingXml streamingXml = this._streamingXml;
						this._streamingXml = null;
						streamingXml.Close();
					}
				}
				else if (0L < this._sharedState._columnDataBytesRemaining && !this._stateObj.TrySkipLongBytes(this._sharedState._columnDataBytesRemaining))
				{
					return false;
				}
			}
			this._sharedState._columnDataBytesRemaining = 0L;
			this._columnDataBytesRead = 0L;
			this._columnDataCharsRead = 0L;
			this._columnDataChars = null;
			this._columnDataCharsIndex = -1;
			this._stateObj._plpdecoder = null;
			return true;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00044E80 File Offset: 0x00043080
		private void CloseActiveSequentialStreamAndTextReader()
		{
			if (this._currentStream != null)
			{
				this._currentStream.SetClosed();
				this._currentStream = null;
			}
			if (this._currentTextReader != null)
			{
				this._currentTextReader.SetClosed();
				this._currentStream = null;
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00044EB8 File Offset: 0x000430B8
		private void RestoreServerSettings(TdsParser parser, TdsParserStateObject stateObj)
		{
			if (parser != null && this._resetOptionsString != null)
			{
				if (parser.State == TdsParserState.OpenLoggedIn)
				{
					SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlDataReader.RestoreServerSettings|Info|Correlation> ObjectID {0}, ActivityID '{1}'", this.ObjectID, ActivityCorrelator.Current);
					Task task = parser.TdsExecuteSQLBatch(this._resetOptionsString, (this._command != null) ? this._command.CommandTimeout : 0, null, stateObj, true, false, null);
					parser.Run(RunBehavior.UntilDone, this._command, this, null, stateObj);
				}
				this._resetOptionsString = null;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00044F34 File Offset: 0x00043134
		internal bool TrySetAltMetaDataSet(_SqlMetaDataSet metaDataSet, bool metaDataConsumed)
		{
			if (this._altMetaDataSetCollection == null)
			{
				this._altMetaDataSetCollection = new _SqlMetaDataSetCollection();
			}
			else if (this._snapshot != null && this._snapshot._altMetaDataSetCollection == this._altMetaDataSetCollection)
			{
				this._altMetaDataSetCollection = (_SqlMetaDataSetCollection)this._altMetaDataSetCollection.Clone();
			}
			this._altMetaDataSetCollection.SetAltMetaData(metaDataSet);
			this._metaDataConsumed = metaDataConsumed;
			if (this._metaDataConsumed && this._parser != null)
			{
				byte b;
				if (!this._stateObj.TryPeekByte(out b))
				{
					return false;
				}
				if (169 == b)
				{
					bool flag;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
					{
						return false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				if (b == 171)
				{
					try
					{
						this._stateObj._accumulateInfoEvents = true;
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, null, null, this._stateObj, out flag2))
						{
							return false;
						}
					}
					finally
					{
						this._stateObj._accumulateInfoEvents = false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				IL_010F:
				this._hasRows = this.IsRowToken(b);
			}
			if (metaDataSet != null && (this._data == null || this._data.Length < metaDataSet.Length))
			{
				this._data = SqlBuffer.CreateBufferArray(metaDataSet.Length);
			}
			return true;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004509C File Offset: 0x0004329C
		private void ClearMetaData()
		{
			this._metaData = null;
			this._tableNames = null;
			this._fieldNameLookup = null;
			this._metaDataConsumed = false;
			this._browseModeInfoConsumed = false;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000450C1 File Offset: 0x000432C1
		internal bool TrySetSensitivityClassification(SensitivityClassification sensitivityClassification)
		{
			this.SensitivityClassification = sensitivityClassification;
			return true;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000450CC File Offset: 0x000432CC
		internal bool TrySetMetaData(_SqlMetaDataSet metaData, bool moreInfo)
		{
			this._metaData = metaData;
			this._tableNames = null;
			if (this._metaData != null)
			{
				this._metaData._schemaTable = null;
				this._data = SqlBuffer.CreateBufferArray(metaData.Length);
			}
			this._fieldNameLookup = null;
			if (metaData != null)
			{
				if (!moreInfo)
				{
					this._metaDataConsumed = true;
					if (this._parser != null)
					{
						byte b;
						if (!this._stateObj.TryPeekByte(out b))
						{
							return false;
						}
						if (b == 169)
						{
							bool flag;
							if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag))
							{
								return false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						if (b == 171)
						{
							try
							{
								this._stateObj._accumulateInfoEvents = true;
								bool flag2;
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag2))
								{
									return false;
								}
							}
							finally
							{
								this._stateObj._accumulateInfoEvents = false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						IL_00EE:
						this._hasRows = this.IsRowToken(b);
						if (136 == b)
						{
							this._metaDataConsumed = false;
						}
					}
				}
			}
			else
			{
				this._metaDataConsumed = false;
			}
			this._browseModeInfoConsumed = false;
			return true;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00045208 File Offset: 0x00043408
		private void SetTimeout(long timeoutMilliseconds)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.SetTimeoutMilliseconds(timeoutMilliseconds);
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00045228 File Offset: 0x00043428
		private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
		{
			bool flag = false;
			flag |= this._currentStream != null && this._currentStream.ColumnIndex == columnIndex;
			return flag | (this._currentTextReader != null && this._currentTextReader.ColumnIndex == columnIndex);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00045270 File Offset: 0x00043470
		private void CheckMetaDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004528E File Offset: 0x0004348E
		private void CheckMetaDataIsReady(int columnIndex, bool permitAsync = false)
		{
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000452C7 File Offset: 0x000434C7
		private void CheckDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000452F4 File Offset: 0x000434F4
		private void CheckHeaderIsReady(int columnIndex, bool permitAsync = false, string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckHeaderIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnHeaderToRead > columnIndex + 1 || this._lastColumnWithDataChunkRead > columnIndex))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnHeaderToRead - 1, this._lastColumnWithDataChunkRead));
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000453A0 File Offset: 0x000435A0
		private void CheckDataIsReady(int columnIndex, bool allowPartiallyReadColumn = false, bool permitAsync = false, string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckDataIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnDataToRead > columnIndex || this._lastColumnWithDataChunkRead > columnIndex || (!allowPartiallyReadColumn && this._lastColumnWithDataChunkRead == columnIndex) || (allowPartiallyReadColumn && this.HasActiveStreamOrTextReaderOnColumn(columnIndex))))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnDataToRead, this._lastColumnWithDataChunkRead + 1));
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00045462 File Offset: 0x00043662
		[Conditional("DEBUG")]
		private void AssertReaderState(bool requireData, bool permitAsync, int? columnIndex = null, bool enforceSequentialAccess = false)
		{
			bool flag = columnIndex != null;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004546C File Offset: 0x0004366C
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			Task<bool> task;
			using (TryEventScope.Create<int>("<sc.SqlDataReader.NextResultAsync|API> {0}", this.ObjectID))
			{
				using (DisposableTemporaryOnStack<CancellationTokenRegistration> disposableTemporaryOnStack = default(DisposableTemporaryOnStack<CancellationTokenRegistration>))
				{
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					if (this.IsClosed)
					{
						taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("NextResultAsync")));
						task = taskCompletionSource.Task;
					}
					else
					{
						if (cancellationToken.CanBeCanceled)
						{
							if (cancellationToken.IsCancellationRequested)
							{
								taskCompletionSource.SetCanceled();
								return taskCompletionSource.Task;
							}
							disposableTemporaryOnStack.Set(cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this._command));
						}
						Task task2 = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
						if (task2 != null)
						{
							taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
							task = taskCompletionSource.Task;
						}
						else if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
						{
							taskCompletionSource.SetCanceled();
							this._currentTask = null;
							task = taskCompletionSource.Task;
						}
						else
						{
							task = this.InvokeAsyncCall<bool>(new SqlDataReader.HasNextResultAsyncCallContext(this, taskCompletionSource, disposableTemporaryOnStack.Take()));
						}
					}
				}
			}
			return task;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000455A0 File Offset: 0x000437A0
		private static Task<bool> NextResultAsyncExecute(Task task, object state)
		{
			SqlDataReader.HasNextResultAsyncCallContext hasNextResultAsyncCallContext = (SqlDataReader.HasNextResultAsyncCallContext)state;
			if (task != null)
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlDataReader.NextResultAsyncExecute> attempt retry {0}", hasNextResultAsyncCallContext._reader.ObjectID);
				hasNextResultAsyncCallContext._reader.PrepareForAsyncContinuation();
			}
			bool flag;
			if (!hasNextResultAsyncCallContext._reader.TryNextResult(out flag))
			{
				return hasNextResultAsyncCallContext._reader.ExecuteAsyncCall<bool>(hasNextResultAsyncCallContext);
			}
			if (!flag)
			{
				return ADP.FalseTask;
			}
			return ADP.TrueTask;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00045608 File Offset: 0x00043808
		internal Task<int> GetBytesAsync(int columnIndex, byte[] buffer, int index, int length, int timeout, CancellationToken cancellationToken, out int bytesRead)
		{
			bytesRead = 0;
			if (this.IsClosed)
			{
				TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetBytesAsync")));
				return taskCompletionSource.Task;
			}
			if (this._currentTask != null)
			{
				TaskCompletionSource<int> taskCompletionSource2 = new TaskCompletionSource<int>();
				taskCompletionSource2.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource2.Task;
			}
			if (cancellationToken.CanBeCanceled && cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			SqlDataReader.GetBytesAsyncCallContext getBytesAsyncCallContext = new SqlDataReader.GetBytesAsyncCallContext(this)
			{
				columnIndex = columnIndex,
				buffer = buffer,
				index = index,
				length = length,
				timeout = timeout,
				cancellationToken = cancellationToken
			};
			if (this._sharedState._nextColumnHeaderToRead > this._lastColumnWithDataChunkRead && this._sharedState._nextColumnDataToRead >= this._lastColumnWithDataChunkRead)
			{
				getBytesAsyncCallContext.mode = SqlDataReader.GetBytesAsyncCallContext.OperationMode.Read;
				this.PrepareAsyncInvocation(false);
				Task<int> bytesAsyncReadDataStage;
				try
				{
					bytesAsyncReadDataStage = this.GetBytesAsyncReadDataStage(getBytesAsyncCallContext, false, out bytesRead);
				}
				catch
				{
					this.CleanupAfterAsyncInvocation(false);
					throw;
				}
				return bytesAsyncReadDataStage;
			}
			TaskCompletionSource<int> taskCompletionSource3 = new TaskCompletionSource<int>();
			Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource3.Task, null);
			if (task != null)
			{
				taskCompletionSource3.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource3.Task;
			}
			CancellationToken cancellationToken2 = CancellationToken.None;
			CancellationTokenSource cancellationTokenSource = null;
			if (timeout > 0)
			{
				cancellationTokenSource = new CancellationTokenSource();
				cancellationTokenSource.CancelAfter(timeout);
				cancellationToken2 = cancellationTokenSource.Token;
			}
			getBytesAsyncCallContext._disposable = cancellationTokenSource;
			getBytesAsyncCallContext.timeoutToken = cancellationToken2;
			getBytesAsyncCallContext._source = taskCompletionSource3;
			this.PrepareAsyncInvocation(true);
			return this.InvokeAsyncCall<int>(getBytesAsyncCallContext);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00045794 File Offset: 0x00043994
		private static Task<int> GetBytesAsyncSeekExecute(Task task, object state)
		{
			SqlDataReader.GetBytesAsyncCallContext getBytesAsyncCallContext = (SqlDataReader.GetBytesAsyncCallContext)state;
			SqlDataReader reader = getBytesAsyncCallContext._reader;
			if (task != null)
			{
				reader.PrepareForAsyncContinuation();
			}
			reader.SetTimeout(reader._defaultTimeoutMilliseconds);
			if (!reader.TryReadColumnHeader(getBytesAsyncCallContext.columnIndex))
			{
				return reader.ExecuteAsyncCall<int>(getBytesAsyncCallContext);
			}
			if (getBytesAsyncCallContext.cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(getBytesAsyncCallContext.cancellationToken);
			}
			if (getBytesAsyncCallContext.timeoutToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
			}
			getBytesAsyncCallContext.mode = SqlDataReader.GetBytesAsyncCallContext.OperationMode.Read;
			reader.SwitchToAsyncWithoutSnapshot();
			int num;
			Task<int> bytesAsyncReadDataStage = reader.GetBytesAsyncReadDataStage(getBytesAsyncCallContext, true, out num);
			if (bytesAsyncReadDataStage == null)
			{
				return Task.FromResult<int>(num);
			}
			return bytesAsyncReadDataStage;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00045838 File Offset: 0x00043A38
		private static Task<int> GetBytesAsyncReadExecute(Task task, object state)
		{
			SqlDataReader.GetBytesAsyncCallContext getBytesAsyncCallContext = (SqlDataReader.GetBytesAsyncCallContext)state;
			SqlDataReader reader = getBytesAsyncCallContext._reader;
			reader.PrepareForAsyncContinuation();
			if (getBytesAsyncCallContext.cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(getBytesAsyncCallContext.cancellationToken);
			}
			if (getBytesAsyncCallContext.timeoutToken.IsCancellationRequested)
			{
				return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
			}
			reader.SetTimeout(reader._defaultTimeoutMilliseconds);
			int num;
			bool flag = reader.TryGetBytesInternalSequential(getBytesAsyncCallContext.columnIndex, getBytesAsyncCallContext.buffer, getBytesAsyncCallContext.index + getBytesAsyncCallContext.totalBytesRead, getBytesAsyncCallContext.length - getBytesAsyncCallContext.totalBytesRead, out num);
			getBytesAsyncCallContext.totalBytesRead += num;
			if (flag)
			{
				return Task.FromResult<int>(getBytesAsyncCallContext.totalBytesRead);
			}
			return reader.ExecuteAsyncCall<int>(getBytesAsyncCallContext);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000458F4 File Offset: 0x00043AF4
		private Task<int> GetBytesAsyncReadDataStage(SqlDataReader.GetBytesAsyncCallContext context, bool isContinuation, out int bytesRead)
		{
			this._lastColumnWithDataChunkRead = context.columnIndex;
			TaskCompletionSource<int> taskCompletionSource = null;
			this.SetTimeout(this._defaultTimeoutMilliseconds);
			bool flag = context._reader.TryGetBytesInternalSequential(context.columnIndex, context.buffer, context.index + context.totalBytesRead, context.length - context.totalBytesRead, out bytesRead);
			context.totalBytesRead += bytesRead;
			if (flag)
			{
				if (!isContinuation)
				{
					this.CleanupAfterAsyncInvocation(false);
				}
				return null;
			}
			int num = bytesRead;
			if (!isContinuation)
			{
				taskCompletionSource = new TaskCompletionSource<int>();
				Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
				if (task != null)
				{
					taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return taskCompletionSource.Task;
				}
				context._source = taskCompletionSource;
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					this._currentTask = null;
					return taskCompletionSource.Task;
				}
				if (context.timeout > 0)
				{
					CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
					cancellationTokenSource.CancelAfter(context.timeout);
					context._disposable = cancellationTokenSource;
					context.timeoutToken = cancellationTokenSource.Token;
				}
			}
			Task<int> task2 = this.ExecuteAsyncCall<int>(context);
			if (isContinuation)
			{
				return task2;
			}
			task2.ContinueWith(SqlDataReader.AAsyncCallContext<int>.s_completeCallback, context, TaskScheduler.Default);
			return taskCompletionSource.Task;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00045A2C File Offset: 0x00043C2C
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			Task<bool> task;
			using (TryEventScope.Create<int>("<sc.SqlDataReader.ReadAsync|API> {0}", this.ObjectID))
			{
				using (DisposableTemporaryOnStack<CancellationTokenRegistration> disposableTemporaryOnStack = default(DisposableTemporaryOnStack<CancellationTokenRegistration>))
				{
					if (this.IsClosed)
					{
						task = ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("ReadAsync")));
					}
					else
					{
						if (cancellationToken.CanBeCanceled)
						{
							disposableTemporaryOnStack.Set(cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this._command));
						}
						if (cancellationToken.IsCancellationRequested)
						{
							task = ADP.CreatedTaskWithCancellation<bool>();
						}
						else if (this._currentTask != null)
						{
							task = ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
						}
						else
						{
							bool flag = false;
							bool flag2 = false;
							try
							{
								if (!this._haltRead && (!this._sharedState._dataReady || this.WillHaveEnoughData(this._metaData.Length - 1, false)))
								{
									if (this._sharedState._dataReady)
									{
										this.CleanPartialReadReliable();
									}
									if (this._stateObj.IsRowTokenReady())
									{
										bool flag3 = this.TryReadInternal(true, out flag2);
										flag = true;
										if (!flag2)
										{
											return ADP.FalseTask;
										}
										if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
										{
											return ADP.TrueTask;
										}
										if (this.WillHaveEnoughData(this._metaData.Length - 1, false))
										{
											flag3 = this.TryReadColumn(this._metaData.Length - 1, true, false, false);
											return ADP.TrueTask;
										}
									}
								}
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								return ADP.CreatedTaskWithException<bool>(ex);
							}
							TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
							Task task2 = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
							if (task2 != null)
							{
								taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
								task = taskCompletionSource.Task;
							}
							else if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
							{
								taskCompletionSource.SetCanceled();
								this._currentTask = null;
								task = taskCompletionSource.Task;
							}
							else
							{
								SqlDataReader.ReadAsyncCallContext readAsyncCallContext = Interlocked.Exchange<SqlDataReader.ReadAsyncCallContext>(ref this._cachedReadAsyncContext, null) ?? new SqlDataReader.ReadAsyncCallContext();
								readAsyncCallContext.Set(this, taskCompletionSource, disposableTemporaryOnStack.Take());
								readAsyncCallContext._hasMoreData = flag2;
								readAsyncCallContext._hasReadRowToken = flag;
								this.PrepareAsyncInvocation(true);
								task = this.InvokeAsyncCall<bool>(readAsyncCallContext);
							}
						}
					}
				}
			}
			return task;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00045CBC File Offset: 0x00043EBC
		private static Task<bool> ReadAsyncExecute(Task task, object state)
		{
			SqlDataReader.ReadAsyncCallContext readAsyncCallContext = (SqlDataReader.ReadAsyncCallContext)state;
			SqlDataReader reader = readAsyncCallContext._reader;
			ref bool ptr = ref readAsyncCallContext._hasMoreData;
			ref bool ptr2 = ref readAsyncCallContext._hasReadRowToken;
			if (task != null)
			{
				reader.PrepareForAsyncContinuation();
			}
			if (ptr2 || reader.TryReadInternal(true, out ptr))
			{
				if (!ptr || (reader._commandBehavior & CommandBehavior.SequentialAccess) == CommandBehavior.SequentialAccess)
				{
					if (!ptr)
					{
						return ADP.FalseTask;
					}
					return ADP.TrueTask;
				}
				else
				{
					if (!ptr2)
					{
						ptr2 = true;
						reader._snapshot = null;
						reader.PrepareAsyncInvocation(true);
					}
					if (reader.TryReadColumn(reader._metaData.Length - 1, true, false, false))
					{
						return ADP.TrueTask;
					}
				}
			}
			return reader.ExecuteAsyncCall<bool>(readAsyncCallContext);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00045D57 File Offset: 0x00043F57
		private void SetCachedReadAsyncCallContext(SqlDataReader.ReadAsyncCallContext instance)
		{
			Interlocked.CompareExchange<SqlDataReader.ReadAsyncCallContext>(ref this._cachedReadAsyncContext, instance, null);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00045D68 File Offset: 0x00043F68
		public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckHeaderIsReady(i, false, "IsDBNullAsync");
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return ADP.CreatedTaskWithException<bool>(ex);
			}
			Task<bool> task2;
			if (this._sharedState._nextColumnHeaderToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
			{
				SqlBuffer[] data = this._data;
				if (data == null)
				{
					return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("IsDBNullAsync")));
				}
				if (!data[i].IsNull)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}
			else
			{
				if (this._currentTask != null)
				{
					return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return ADP.CreatedTaskWithCancellation<bool>();
				}
				try
				{
					if (this.WillHaveEnoughData(i, true))
					{
						this.ReadColumnHeader(i);
						return this._data[i].IsNull ? ADP.TrueTask : ADP.FalseTask;
					}
				}
				catch (Exception ex2)
				{
					if (!ADP.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					return ADP.CreatedTaskWithException<bool>(ex2);
				}
				using (DisposableTemporaryOnStack<CancellationTokenRegistration> disposableTemporaryOnStack = default(DisposableTemporaryOnStack<CancellationTokenRegistration>))
				{
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
					if (task != null)
					{
						taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
						task2 = taskCompletionSource.Task;
					}
					else if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
						this._currentTask = null;
						task2 = taskCompletionSource.Task;
					}
					else
					{
						if (cancellationToken.CanBeCanceled)
						{
							disposableTemporaryOnStack.Set(cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this._command));
						}
						SqlDataReader.IsDBNullAsyncCallContext isDBNullAsyncCallContext = Interlocked.Exchange<SqlDataReader.IsDBNullAsyncCallContext>(ref this._cachedIsDBNullContext, null) ?? new SqlDataReader.IsDBNullAsyncCallContext();
						isDBNullAsyncCallContext.Set(this, taskCompletionSource, disposableTemporaryOnStack.Take());
						isDBNullAsyncCallContext._columnIndex = i;
						this.PrepareAsyncInvocation(true);
						task2 = this.InvokeAsyncCall<bool>(isDBNullAsyncCallContext);
					}
				}
			}
			return task2;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00045F68 File Offset: 0x00044168
		private static Task<bool> IsDBNullAsyncExecute(Task task, object state)
		{
			SqlDataReader.IsDBNullAsyncCallContext isDBNullAsyncCallContext = (SqlDataReader.IsDBNullAsyncCallContext)state;
			SqlDataReader reader = isDBNullAsyncCallContext._reader;
			if (task != null)
			{
				reader.PrepareForAsyncContinuation();
			}
			if (!reader.TryReadColumnHeader(isDBNullAsyncCallContext._columnIndex))
			{
				return reader.ExecuteAsyncCall<bool>(isDBNullAsyncCallContext);
			}
			if (!reader._data[isDBNullAsyncCallContext._columnIndex].IsNull)
			{
				return ADP.FalseTask;
			}
			return ADP.TrueTask;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00045FC1 File Offset: 0x000441C1
		private void SetCachedIDBNullAsyncCallContext(SqlDataReader.IsDBNullAsyncCallContext instance)
		{
			Interlocked.CompareExchange<SqlDataReader.IsDBNullAsyncCallContext>(ref this._cachedIsDBNullContext, instance, null);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00045FD4 File Offset: 0x000441D4
		public override Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckDataIsReady(i, false, false, "GetFieldValueAsync");
				if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) && this._sharedState._nextColumnDataToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
				{
					SqlBuffer[] data = this._data;
					_SqlMetaDataSet metaData = this._metaData;
					if (data != null && metaData != null)
					{
						return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(data[i], metaData[i], false));
					}
					return ADP.CreatedTaskWithException<T>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetFieldValueAsync")));
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return ADP.CreatedTaskWithException<T>(ex);
			}
			if (this._currentTask != null)
			{
				return ADP.CreatedTaskWithException<T>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<T>();
			}
			try
			{
				if (this.WillHaveEnoughData(i, false))
				{
					return Task.FromResult<T>(this.GetFieldValueInternal<T>(i, true));
				}
			}
			catch (Exception ex2)
			{
				if (!ADP.IsCatchableExceptionType(ex2))
				{
					throw;
				}
				return ADP.CreatedTaskWithException<T>(ex2);
			}
			Task<T> task2;
			using (DisposableTemporaryOnStack<CancellationTokenRegistration> disposableTemporaryOnStack = default(DisposableTemporaryOnStack<CancellationTokenRegistration>))
			{
				TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
				Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
				if (task != null)
				{
					taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					task2 = taskCompletionSource.Task;
				}
				else if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					this._currentTask = null;
					task2 = taskCompletionSource.Task;
				}
				else
				{
					if (cancellationToken.CanBeCanceled)
					{
						disposableTemporaryOnStack.Set(cancellationToken.Register(SqlCommand.s_cancelIgnoreFailure, this._command));
					}
					task2 = this.InvokeAsyncCall<T>(new SqlDataReader.GetFieldValueAsyncCallContext<T>(this, taskCompletionSource, disposableTemporaryOnStack.Take(), i));
				}
			}
			return task2;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000461B8 File Offset: 0x000443B8
		private static Task<T> GetFieldValueAsyncExecute<T>(Task task, object state)
		{
			SqlDataReader.GetFieldValueAsyncCallContext<T> getFieldValueAsyncCallContext = (SqlDataReader.GetFieldValueAsyncCallContext<T>)state;
			SqlDataReader reader = getFieldValueAsyncCallContext._reader;
			int columnIndex = getFieldValueAsyncCallContext._columnIndex;
			if (task != null)
			{
				reader.PrepareForAsyncContinuation();
			}
			if ((typeof(T) == typeof(Stream) || typeof(T) == typeof(TextReader) || typeof(T) == typeof(XmlReader)) && reader.IsCommandBehavior(CommandBehavior.SequentialAccess) && reader._sharedState._dataReady)
			{
				bool flag = false;
				TdsParser.ReliabilitySection reliabilitySection = default(TdsParser.ReliabilitySection);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					flag = reader.TryReadColumnInternal(getFieldValueAsyncCallContext._columnIndex, true, false);
				}
				finally
				{
				}
				if (flag)
				{
					return Task.FromResult<T>(reader.GetFieldValueFromSqlBufferInternal<T>(reader._data[columnIndex], reader._metaData[columnIndex], true));
				}
			}
			if (reader.TryReadColumn(columnIndex, false, false, false))
			{
				return Task.FromResult<T>(reader.GetFieldValueFromSqlBufferInternal<T>(reader._data[columnIndex], reader._metaData[columnIndex], false));
			}
			return reader.ExecuteAsyncCall<T>(getFieldValueAsyncCallContext);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000462D4 File Offset: 0x000444D4
		private static Task<T> ExecuteAsyncCallCallback<T>(Task task, object state)
		{
			SqlDataReader.AAsyncCallContext<T> aasyncCallContext = (SqlDataReader.AAsyncCallContext<T>)state;
			return aasyncCallContext._reader.ExecuteAsyncCall<T>(task, aasyncCallContext);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000462F8 File Offset: 0x000444F8
		private static void CompleteAsyncCallCallback<T>(Task<T> task, object state)
		{
			SqlDataReader.AAsyncCallContext<T> aasyncCallContext = (SqlDataReader.AAsyncCallContext<T>)state;
			aasyncCallContext._reader.CompleteAsyncCall<T>(task, aasyncCallContext);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004631C File Offset: 0x0004451C
		private Task<T> InvokeAsyncCall<T>(SqlDataReader.AAsyncCallContext<T> context)
		{
			TaskCompletionSource<T> source = context._source;
			try
			{
				Task<T> task;
				try
				{
					task = context.Execute(null, context);
				}
				catch (Exception ex)
				{
					task = ADP.CreatedTaskWithException<T>(ex);
				}
				if (task.IsCompleted)
				{
					this.CompleteAsyncCall<T>(task, context);
				}
				else
				{
					task.ContinueWith(SqlDataReader.AAsyncCallContext<T>.s_completeCallback, context, TaskScheduler.Default);
				}
			}
			catch (AggregateException ex2)
			{
				source.TrySetException(ex2.InnerException);
			}
			catch (Exception ex3)
			{
				source.TrySetException(ex3);
			}
			return source.Task;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000463BC File Offset: 0x000445BC
		private Task<T> ExecuteAsyncCall<T>(SqlDataReader.AAsyncCallContext<T> context)
		{
			TaskCompletionSource<object> networkPacketTaskSource = this._stateObj._networkPacketTaskSource;
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested || networkPacketTaskSource == null)
			{
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			return networkPacketTaskSource.Task.ContinueWith<Task<T>>(SqlDataReader.AAsyncCallContext<T>.s_executeCallback, context, TaskScheduler.Default).Unwrap<T>();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00046410 File Offset: 0x00044610
		private Task<T> ExecuteAsyncCall<T>(Task task, SqlDataReader.AAsyncCallContext<T> context)
		{
			if (task.IsFaulted)
			{
				return Task.FromException<T>(task.Exception.InnerException);
			}
			if (!this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				TdsParserStateObject stateObj = this._stateObj;
				if (stateObj != null)
				{
					TdsParserStateObject tdsParserStateObject = stateObj;
					lock (tdsParserStateObject)
					{
						if (this._stateObj != null)
						{
							if (task.IsCanceled)
							{
								if (this._parser != null)
								{
									this._parser.State = TdsParserState.Broken;
									this._parser.Connection.BreakConnection();
									this._parser.ThrowExceptionAndWarning(this._stateObj, false, false);
								}
							}
							else if (!this.IsClosed)
							{
								try
								{
									return context.Execute(task, context);
								}
								catch (Exception)
								{
									this.CleanupAfterAsyncInvocation(false);
									throw;
								}
							}
						}
					}
				}
			}
			return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00046504 File Offset: 0x00044704
		private void CompleteAsyncCall<T>(Task<T> task, SqlDataReader.AAsyncCallContext<T> context)
		{
			TaskCompletionSource<T> source = context._source;
			context.Dispose();
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = stateObj != null && stateObj._syncOverAsync;
			this.CleanupAfterAsyncInvocation(flag);
			Task task2 = Interlocked.CompareExchange<Task>(ref this._currentTask, null, source.Task);
			if (task.IsFaulted)
			{
				Exception innerException = task.Exception.InnerException;
				source.TrySetException(innerException);
				return;
			}
			if (task.IsCanceled)
			{
				source.TrySetCanceled();
				return;
			}
			source.TrySetResult(task.Result);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00046588 File Offset: 0x00044788
		private void PrepareAsyncInvocation(bool useSnapshot)
		{
			if (useSnapshot)
			{
				if (this._snapshot == null)
				{
					this._snapshot = new SqlDataReader.Snapshot
					{
						_dataReady = this._sharedState._dataReady,
						_haltRead = this._haltRead,
						_metaDataConsumed = this._metaDataConsumed,
						_browseModeInfoConsumed = this._browseModeInfoConsumed,
						_hasRows = this._hasRows,
						_altRowStatus = this._altRowStatus,
						_nextColumnDataToRead = this._sharedState._nextColumnDataToRead,
						_nextColumnHeaderToRead = this._sharedState._nextColumnHeaderToRead,
						_columnDataBytesRead = this._columnDataBytesRead,
						_columnDataBytesRemaining = this._sharedState._columnDataBytesRemaining,
						_metadata = this._metaData,
						_altMetaDataSetCollection = this._altMetaDataSetCollection,
						_tableNames = this._tableNames,
						_currentStream = this._currentStream,
						_currentTextReader = this._currentTextReader
					};
					this._stateObj.SetSnapshot();
				}
			}
			else
			{
				this._stateObj._asyncReadWithoutSnapshot = true;
			}
			this._stateObj._syncOverAsync = false;
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000466B0 File Offset: 0x000448B0
		private void CleanupAfterAsyncInvocation(bool ignoreCloseToken = false)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null && (ignoreCloseToken || !this._cancelAsyncOnCloseToken.IsCancellationRequested || stateObj._asyncReadWithoutSnapshot))
			{
				TdsParserStateObject tdsParserStateObject = stateObj;
				lock (tdsParserStateObject)
				{
					if (this._stateObj != null)
					{
						this.CleanupAfterAsyncInvocationInternal(this._stateObj, true);
					}
				}
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004671C File Offset: 0x0004491C
		private void CleanupAfterAsyncInvocationInternal(TdsParserStateObject stateObj, bool resetNetworkPacketTaskSource = true)
		{
			if (resetNetworkPacketTaskSource)
			{
				stateObj._networkPacketTaskSource = null;
			}
			stateObj.ResetSnapshot();
			stateObj._syncOverAsync = true;
			stateObj._executionContext = null;
			stateObj._asyncReadWithoutSnapshot = false;
			this._snapshot = null;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004674C File Offset: 0x0004494C
		private void PrepareForAsyncContinuation()
		{
			if (this._snapshot != null)
			{
				this._sharedState._dataReady = this._snapshot._dataReady;
				this._haltRead = this._snapshot._haltRead;
				this._metaDataConsumed = this._snapshot._metaDataConsumed;
				this._browseModeInfoConsumed = this._snapshot._browseModeInfoConsumed;
				this._hasRows = this._snapshot._hasRows;
				this._altRowStatus = this._snapshot._altRowStatus;
				this._sharedState._nextColumnDataToRead = this._snapshot._nextColumnDataToRead;
				this._sharedState._nextColumnHeaderToRead = this._snapshot._nextColumnHeaderToRead;
				this._columnDataBytesRead = this._snapshot._columnDataBytesRead;
				this._sharedState._columnDataBytesRemaining = this._snapshot._columnDataBytesRemaining;
				this._metaData = this._snapshot._metadata;
				this._altMetaDataSetCollection = this._snapshot._altMetaDataSetCollection;
				this._tableNames = this._snapshot._tableNames;
				this._currentStream = this._snapshot._currentStream;
				this._currentTextReader = this._snapshot._currentTextReader;
				this._stateObj.PrepareReplaySnapshot();
			}
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00046892 File Offset: 0x00044A92
		private void SwitchToAsyncWithoutSnapshot()
		{
			this._snapshot = null;
			this._stateObj.ResetSnapshot();
			this._stateObj._asyncReadWithoutSnapshot = true;
		}

		// Token: 0x0400073E RID: 1854
		internal SqlDataReader.SharedState _sharedState = new SqlDataReader.SharedState();

		// Token: 0x0400073F RID: 1855
		private TdsParser _parser;

		// Token: 0x04000740 RID: 1856
		private TdsParserStateObject _stateObj;

		// Token: 0x04000741 RID: 1857
		private SqlCommand _command;

		// Token: 0x04000742 RID: 1858
		private SqlConnection _connection;

		// Token: 0x04000743 RID: 1859
		private int _defaultLCID;

		// Token: 0x04000744 RID: 1860
		private bool _haltRead;

		// Token: 0x04000745 RID: 1861
		private bool _metaDataConsumed;

		// Token: 0x04000746 RID: 1862
		private bool _browseModeInfoConsumed;

		// Token: 0x04000747 RID: 1863
		private bool _isClosed;

		// Token: 0x04000748 RID: 1864
		private bool _isInitialized;

		// Token: 0x04000749 RID: 1865
		private bool _hasRows;

		// Token: 0x0400074A RID: 1866
		private SqlDataReader.ALTROWSTATUS _altRowStatus;

		// Token: 0x0400074B RID: 1867
		private int _recordsAffected = -1;

		// Token: 0x0400074C RID: 1868
		private long _defaultTimeoutMilliseconds;

		// Token: 0x0400074D RID: 1869
		private SqlConnectionString.TypeSystem _typeSystem;

		// Token: 0x0400074E RID: 1870
		private SqlStatistics _statistics;

		// Token: 0x0400074F RID: 1871
		private SqlBuffer[] _data;

		// Token: 0x04000750 RID: 1872
		private SqlStreamingXml _streamingXml;

		// Token: 0x04000751 RID: 1873
		private _SqlMetaDataSet _metaData;

		// Token: 0x04000752 RID: 1874
		private _SqlMetaDataSetCollection _altMetaDataSetCollection;

		// Token: 0x04000753 RID: 1875
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000754 RID: 1876
		private CommandBehavior _commandBehavior;

		// Token: 0x04000755 RID: 1877
		private static int _objectTypeCount;

		// Token: 0x04000756 RID: 1878
		internal readonly int ObjectID = Interlocked.Increment(ref SqlDataReader._objectTypeCount);

		// Token: 0x04000757 RID: 1879
		private MultiPartTableName[] _tableNames;

		// Token: 0x04000758 RID: 1880
		private string _resetOptionsString;

		// Token: 0x04000759 RID: 1881
		private int _lastColumnWithDataChunkRead;

		// Token: 0x0400075A RID: 1882
		private long _columnDataBytesRead;

		// Token: 0x0400075B RID: 1883
		private long _columnDataCharsRead;

		// Token: 0x0400075C RID: 1884
		private char[] _columnDataChars;

		// Token: 0x0400075D RID: 1885
		private int _columnDataCharsIndex;

		// Token: 0x0400075E RID: 1886
		private Task _currentTask;

		// Token: 0x0400075F RID: 1887
		private SqlDataReader.Snapshot _snapshot;

		// Token: 0x04000760 RID: 1888
		private CancellationTokenSource _cancelAsyncOnCloseTokenSource;

		// Token: 0x04000761 RID: 1889
		private CancellationToken _cancelAsyncOnCloseToken;

		// Token: 0x04000762 RID: 1890
		private SqlSequentialStream _currentStream;

		// Token: 0x04000763 RID: 1891
		private SqlSequentialTextReader _currentTextReader;

		// Token: 0x04000764 RID: 1892
		private SqlDataReader.IsDBNullAsyncCallContext _cachedIsDBNullContext;

		// Token: 0x04000765 RID: 1893
		private SqlDataReader.ReadAsyncCallContext _cachedReadAsyncContext;

		// Token: 0x02000239 RID: 569
		private enum ALTROWSTATUS
		{
			// Token: 0x0400163E RID: 5694
			Null,
			// Token: 0x0400163F RID: 5695
			AltRow,
			// Token: 0x04001640 RID: 5696
			Done
		}

		// Token: 0x0200023A RID: 570
		internal class SharedState
		{
			// Token: 0x04001641 RID: 5697
			internal int _nextColumnHeaderToRead;

			// Token: 0x04001642 RID: 5698
			internal int _nextColumnDataToRead;

			// Token: 0x04001643 RID: 5699
			internal long _columnDataBytesRemaining;

			// Token: 0x04001644 RID: 5700
			internal bool _dataReady;
		}

		// Token: 0x0200023B RID: 571
		private class Snapshot
		{
			// Token: 0x04001645 RID: 5701
			public bool _dataReady;

			// Token: 0x04001646 RID: 5702
			public bool _haltRead;

			// Token: 0x04001647 RID: 5703
			public bool _metaDataConsumed;

			// Token: 0x04001648 RID: 5704
			public bool _browseModeInfoConsumed;

			// Token: 0x04001649 RID: 5705
			public bool _hasRows;

			// Token: 0x0400164A RID: 5706
			public SqlDataReader.ALTROWSTATUS _altRowStatus;

			// Token: 0x0400164B RID: 5707
			public int _nextColumnDataToRead;

			// Token: 0x0400164C RID: 5708
			public int _nextColumnHeaderToRead;

			// Token: 0x0400164D RID: 5709
			public long _columnDataBytesRead;

			// Token: 0x0400164E RID: 5710
			public long _columnDataBytesRemaining;

			// Token: 0x0400164F RID: 5711
			public _SqlMetaDataSet _metadata;

			// Token: 0x04001650 RID: 5712
			public _SqlMetaDataSetCollection _altMetaDataSetCollection;

			// Token: 0x04001651 RID: 5713
			public MultiPartTableName[] _tableNames;

			// Token: 0x04001652 RID: 5714
			public SqlSequentialStream _currentStream;

			// Token: 0x04001653 RID: 5715
			public SqlSequentialTextReader _currentTextReader;
		}

		// Token: 0x0200023C RID: 572
		private abstract class AAsyncCallContext<T> : IDisposable
		{
			// Token: 0x06001E92 RID: 7826 RVA: 0x000027D1 File Offset: 0x000009D1
			protected AAsyncCallContext()
			{
			}

			// Token: 0x06001E93 RID: 7827 RVA: 0x0007D58E File Offset: 0x0007B78E
			protected AAsyncCallContext(SqlDataReader reader, TaskCompletionSource<T> source, IDisposable disposable = null)
			{
				this.Set(reader, source, disposable);
			}

			// Token: 0x06001E94 RID: 7828 RVA: 0x0007D59F File Offset: 0x0007B79F
			internal void Set(SqlDataReader reader, TaskCompletionSource<T> source, IDisposable disposable = null)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}
				this._reader = reader;
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				this._source = source;
				this._disposable = disposable;
			}

			// Token: 0x06001E95 RID: 7829 RVA: 0x0007D5D4 File Offset: 0x0007B7D4
			internal void Clear()
			{
				this._source = null;
				this._reader = null;
				IDisposable disposable = this._disposable;
				this._disposable = null;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x17000A38 RID: 2616
			// (get) Token: 0x06001E96 RID: 7830
			internal abstract Func<Task, object, Task<T>> Execute { get; }

			// Token: 0x06001E97 RID: 7831 RVA: 0x0007D606 File Offset: 0x0007B806
			public virtual void Dispose()
			{
				this.Clear();
			}

			// Token: 0x04001654 RID: 5716
			internal static readonly Action<Task<T>, object> s_completeCallback = new Action<Task<T>, object>(SqlDataReader.CompleteAsyncCallCallback<T>);

			// Token: 0x04001655 RID: 5717
			internal static readonly Func<Task, object, Task<T>> s_executeCallback = new Func<Task, object, Task<T>>(SqlDataReader.ExecuteAsyncCallCallback<T>);

			// Token: 0x04001656 RID: 5718
			internal SqlDataReader _reader;

			// Token: 0x04001657 RID: 5719
			internal TaskCompletionSource<T> _source;

			// Token: 0x04001658 RID: 5720
			internal IDisposable _disposable;
		}

		// Token: 0x0200023D RID: 573
		private sealed class ReadAsyncCallContext : SqlDataReader.AAsyncCallContext<bool>
		{
			// Token: 0x06001E99 RID: 7833 RVA: 0x0007D632 File Offset: 0x0007B832
			internal ReadAsyncCallContext()
			{
			}

			// Token: 0x17000A39 RID: 2617
			// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0007D63A File Offset: 0x0007B83A
			internal override Func<Task, object, Task<bool>> Execute
			{
				get
				{
					return SqlDataReader.ReadAsyncCallContext.s_execute;
				}
			}

			// Token: 0x06001E9B RID: 7835 RVA: 0x0007D644 File Offset: 0x0007B844
			public override void Dispose()
			{
				SqlDataReader reader = this._reader;
				base.Dispose();
				reader.SetCachedReadAsyncCallContext(this);
			}

			// Token: 0x04001659 RID: 5721
			internal static readonly Func<Task, object, Task<bool>> s_execute = new Func<Task, object, Task<bool>>(SqlDataReader.ReadAsyncExecute);

			// Token: 0x0400165A RID: 5722
			internal bool _hasMoreData;

			// Token: 0x0400165B RID: 5723
			internal bool _hasReadRowToken;
		}

		// Token: 0x0200023E RID: 574
		private sealed class IsDBNullAsyncCallContext : SqlDataReader.AAsyncCallContext<bool>
		{
			// Token: 0x06001E9D RID: 7837 RVA: 0x0007D632 File Offset: 0x0007B832
			internal IsDBNullAsyncCallContext()
			{
			}

			// Token: 0x17000A3A RID: 2618
			// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0007D678 File Offset: 0x0007B878
			internal override Func<Task, object, Task<bool>> Execute
			{
				get
				{
					return SqlDataReader.IsDBNullAsyncCallContext.s_execute;
				}
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x0007D680 File Offset: 0x0007B880
			public override void Dispose()
			{
				SqlDataReader reader = this._reader;
				base.Dispose();
				reader.SetCachedIDBNullAsyncCallContext(this);
			}

			// Token: 0x0400165C RID: 5724
			internal static readonly Func<Task, object, Task<bool>> s_execute = new Func<Task, object, Task<bool>>(SqlDataReader.IsDBNullAsyncExecute);

			// Token: 0x0400165D RID: 5725
			internal int _columnIndex;
		}

		// Token: 0x0200023F RID: 575
		private sealed class HasNextResultAsyncCallContext : SqlDataReader.AAsyncCallContext<bool>
		{
			// Token: 0x06001EA1 RID: 7841 RVA: 0x0007D6B4 File Offset: 0x0007B8B4
			public HasNextResultAsyncCallContext(SqlDataReader reader, TaskCompletionSource<bool> source, IDisposable disposable)
				: base(reader, source, disposable)
			{
			}

			// Token: 0x17000A3B RID: 2619
			// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0007D6BF File Offset: 0x0007B8BF
			internal override Func<Task, object, Task<bool>> Execute
			{
				get
				{
					return SqlDataReader.HasNextResultAsyncCallContext.s_execute;
				}
			}

			// Token: 0x0400165E RID: 5726
			private static readonly Func<Task, object, Task<bool>> s_execute = new Func<Task, object, Task<bool>>(SqlDataReader.NextResultAsyncExecute);
		}

		// Token: 0x02000240 RID: 576
		private sealed class GetBytesAsyncCallContext : SqlDataReader.AAsyncCallContext<int>
		{
			// Token: 0x06001EA4 RID: 7844 RVA: 0x0007D6D9 File Offset: 0x0007B8D9
			internal GetBytesAsyncCallContext(SqlDataReader reader)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}
				this._reader = reader;
			}

			// Token: 0x17000A3C RID: 2620
			// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0007D6F7 File Offset: 0x0007B8F7
			internal override Func<Task, object, Task<int>> Execute
			{
				get
				{
					if (this.mode != SqlDataReader.GetBytesAsyncCallContext.OperationMode.Seek)
					{
						return SqlDataReader.GetBytesAsyncCallContext.s_executeRead;
					}
					return SqlDataReader.GetBytesAsyncCallContext.s_executeSeek;
				}
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x0007D70C File Offset: 0x0007B90C
			public override void Dispose()
			{
				this.buffer = null;
				this.cancellationToken = default(CancellationToken);
				this.timeoutToken = default(CancellationToken);
				base.Dispose();
			}

			// Token: 0x0400165F RID: 5727
			private static readonly Func<Task, object, Task<int>> s_executeSeek = new Func<Task, object, Task<int>>(SqlDataReader.GetBytesAsyncSeekExecute);

			// Token: 0x04001660 RID: 5728
			private static readonly Func<Task, object, Task<int>> s_executeRead = new Func<Task, object, Task<int>>(SqlDataReader.GetBytesAsyncReadExecute);

			// Token: 0x04001661 RID: 5729
			internal int columnIndex;

			// Token: 0x04001662 RID: 5730
			internal byte[] buffer;

			// Token: 0x04001663 RID: 5731
			internal int index;

			// Token: 0x04001664 RID: 5732
			internal int length;

			// Token: 0x04001665 RID: 5733
			internal int timeout;

			// Token: 0x04001666 RID: 5734
			internal CancellationToken cancellationToken;

			// Token: 0x04001667 RID: 5735
			internal CancellationToken timeoutToken;

			// Token: 0x04001668 RID: 5736
			internal int totalBytesRead;

			// Token: 0x04001669 RID: 5737
			internal SqlDataReader.GetBytesAsyncCallContext.OperationMode mode;

			// Token: 0x02000295 RID: 661
			internal enum OperationMode
			{
				// Token: 0x040017BE RID: 6078
				Seek,
				// Token: 0x040017BF RID: 6079
				Read
			}
		}

		// Token: 0x02000241 RID: 577
		private sealed class GetFieldValueAsyncCallContext<T> : SqlDataReader.AAsyncCallContext<T>
		{
			// Token: 0x06001EA8 RID: 7848 RVA: 0x0007D757 File Offset: 0x0007B957
			internal GetFieldValueAsyncCallContext(SqlDataReader reader, TaskCompletionSource<T> source, IDisposable disposable, int columnIndex)
				: base(reader, source, disposable)
			{
				this._columnIndex = columnIndex;
			}

			// Token: 0x17000A3D RID: 2621
			// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0007D76A File Offset: 0x0007B96A
			internal override Func<Task, object, Task<T>> Execute
			{
				get
				{
					return SqlDataReader.GetFieldValueAsyncCallContext<T>.s_execute;
				}
			}

			// Token: 0x0400166A RID: 5738
			private static readonly Func<Task, object, Task<T>> s_execute = new Func<Task, object, Task<T>>(SqlDataReader.GetFieldValueAsyncExecute<T>);

			// Token: 0x0400166B RID: 5739
			internal readonly int _columnIndex;
		}
	}
}
