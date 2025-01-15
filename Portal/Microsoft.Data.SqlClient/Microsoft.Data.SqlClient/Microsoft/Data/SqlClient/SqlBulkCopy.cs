using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D7 RID: 215
	public sealed class SqlBulkCopy : IDisposable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000EFB RID: 3835 RVA: 0x0002F860 File Offset: 0x0002DA60
		// (remove) Token: 0x06000EFC RID: 3836 RVA: 0x0002F898 File Offset: 0x0002DA98
		public event SqlRowsCopiedEventHandler SqlRowsCopied;

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0002F8D0 File Offset: 0x0002DAD0
		private int RowNumber
		{
			get
			{
				int num;
				switch (this._rowSourceType)
				{
				case SqlBulkCopy.ValueSourceType.DataTable:
					num = ((DataTable)this._rowSource).Rows.IndexOf(this._rowEnumerator.Current as DataRow);
					goto IL_0070;
				case SqlBulkCopy.ValueSourceType.RowArray:
					num = this._dataTableSource.Rows.IndexOf(this._rowEnumerator.Current as DataRow);
					goto IL_0070;
				}
				return -1;
				IL_0070:
				return num + 1;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002F954 File Offset: 0x0002DB54
		public SqlBulkCopy(SqlConnection connection)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			this._connection = connection;
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
			this.ColumnOrderHints = new SqlBulkCopyColumnOrderHintCollection();
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002F9AA File Offset: 0x0002DBAA
		public SqlBulkCopy(SqlConnection connection, SqlBulkCopyOptions copyOptions, SqlTransaction externalTransaction)
			: this(connection)
		{
			this._copyOptions = copyOptions;
			if (externalTransaction != null && this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				throw SQL.BulkLoadConflictingTransactionOption();
			}
			if (!this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				this._externalTransaction = externalTransaction;
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002F9E0 File Offset: 0x0002DBE0
		public SqlBulkCopy(string connectionString)
		{
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			this._connection = new SqlConnection(connectionString);
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
			this.ColumnOrderHints = new SqlBulkCopyColumnOrderHintCollection();
			this._ownConnection = true;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002FA42 File Offset: 0x0002DC42
		public SqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions)
			: this(connectionString)
		{
			this._copyOptions = copyOptions;
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0002FA52 File Offset: 0x0002DC52
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0002FA5A File Offset: 0x0002DC5A
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				if (value >= 0)
				{
					this._batchSize = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("BatchSize");
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0002FA72 File Offset: 0x0002DC72
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0002FA7A File Offset: 0x0002DC7A
		public int BulkCopyTimeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value < 0)
				{
					throw SQL.BulkLoadInvalidTimeout(value);
				}
				this._timeout = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0002FA8E File Offset: 0x0002DC8E
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0002FA96 File Offset: 0x0002DC96
		public bool EnableStreaming
		{
			get
			{
				return this._enableStreaming;
			}
			set
			{
				this._enableStreaming = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0002FA9F File Offset: 0x0002DC9F
		public SqlBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0002FAA7 File Offset: 0x0002DCA7
		public SqlBulkCopyColumnOrderHintCollection ColumnOrderHints { get; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0002FAAF File Offset: 0x0002DCAF
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0002FAB7 File Offset: 0x0002DCB7
		public string DestinationTableName
		{
			get
			{
				return this._destinationTableName;
			}
			set
			{
				if (value == null)
				{
					throw ADP.ArgumentNull("DestinationTableName");
				}
				if (value.Length == 0)
				{
					throw ADP.ArgumentOutOfRange("DestinationTableName");
				}
				this._destinationTableName = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0002FAE1 File Offset: 0x0002DCE1
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0002FAE9 File Offset: 0x0002DCE9
		public int NotifyAfter
		{
			get
			{
				return this._notifyAfter;
			}
			set
			{
				if (value >= 0)
				{
					this._notifyAfter = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("NotifyAfter");
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0002FB01 File Offset: 0x0002DD01
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0002FB09 File Offset: 0x0002DD09
		public int RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0002FB11 File Offset: 0x0002DD11
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002FB35 File Offset: 0x0002DD35
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002FB44 File Offset: 0x0002DD44
		private bool IsCopyOption(SqlBulkCopyOptions copyOption)
		{
			return (this._copyOptions & copyOption) == copyOption;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002FB54 File Offset: 0x0002DD54
		private string CreateInitialQuery()
		{
			string[] array;
			try
			{
				array = MultipartIdentifier.ParseMultipartIdentifier(this.DestinationTableName, "[\"", "]\"", Strings.SQL_BulkCopyDestinationTableName, true);
			}
			catch (Exception ex)
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, ex);
			}
			if (ADP.IsEmpty(array[3]))
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, null);
			}
			string text = "select @@trancount; SET FMTONLY ON select * from " + ADP.BuildMultiPartName(array) + " SET FMTONLY OFF ";
			if (this._connection.Is2000)
			{
				string text2;
				if (this._connection.Is2008OrNewer)
				{
					text2 = "sp_tablecollations_100";
				}
				else if (this._connection.Is2005OrNewer)
				{
					text2 = "sp_tablecollations_90";
				}
				else
				{
					text2 = "sp_tablecollations";
				}
				string text3 = array[3];
				bool flag = text3.Length > 0 && '#' == text3[0];
				if (!ADP.IsEmpty(text3))
				{
					text3 = SqlServerEscapeHelper.EscapeStringAsLiteral(text3);
					text3 = SqlServerEscapeHelper.EscapeIdentifier(text3);
				}
				string text4 = array[2];
				if (!ADP.IsEmpty(text4))
				{
					text4 = SqlServerEscapeHelper.EscapeStringAsLiteral(text4);
					text4 = SqlServerEscapeHelper.EscapeIdentifier(text4);
				}
				string text5 = array[1];
				if (flag && ADP.IsEmpty(text5))
				{
					text += string.Format("exec tempdb..{0} N'{1}.{2}'", text2, text4, text3);
				}
				else
				{
					if (!ADP.IsEmpty(text5))
					{
						text5 = SqlServerEscapeHelper.EscapeIdentifier(text5);
					}
					text += string.Format("exec {0}..{1} N'{2}.{3}'", new object[] { text5, text2, text4, text3 });
				}
			}
			return text;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002FCCC File Offset: 0x0002DECC
		private Task<BulkCopySimpleResultSet> CreateAndExecuteInitialQueryAsync(out BulkCopySimpleResultSet result)
		{
			string text = this.CreateInitialQuery();
			SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SqlBulkCopy.CreateAndExecuteInitialQueryAsync|INFO> Initial Query: '{0}'", text);
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlBulkCopy.CreateAndExecuteInitialQueryAsync|Info|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			Task task = this._parser.TdsExecuteSQLBatch(text, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true, null);
			if (task == null)
			{
				result = new BulkCopySimpleResultSet();
				this.RunParser(result);
				return null;
			}
			result = null;
			return task.ContinueWith<BulkCopySimpleResultSet>(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
				this.RunParserReliably(bulkCopySimpleResultSet);
				return bulkCopySimpleResultSet;
			}, TaskScheduler.Default);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002FD60 File Offset: 0x0002DF60
		private string AnalyzeTargetAndCreateUpdateBulkCommand(BulkCopySimpleResultSet internalResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._connection.Is2000 && internalResults[2].Count == 0)
			{
				throw SQL.BulkLoadNoCollation();
			}
			string[] array = MultipartIdentifier.ParseMultipartIdentifier(this.DestinationTableName, "[\"", "]\"", Strings.SQL_BulkCopyDestinationTableName, true);
			stringBuilder.AppendFormat("insert bulk {0} (", ADP.BuildMultiPartName(array));
			int num = 0;
			int num2 = 0;
			bool flag;
			if (this._parser.Is2005OrNewer)
			{
				flag = this._connection.HasLocalTransaction;
			}
			else
			{
				flag = (bool)(0 < (SqlInt32)internalResults[0][0][0]);
			}
			if (flag && this._externalTransaction == null && this._internalTransaction == null && this._connection.Parser != null && this._connection.Parser.CurrentTransaction != null && this._connection.Parser.CurrentTransaction.IsLocal)
			{
				throw SQL.BulkLoadExistingTransaction();
			}
			HashSet<string> hashSet = new HashSet<string>();
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._sortedColumnMappings = new List<_ColumnMapping>(metaData.Length);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				bool flag2 = false;
				if (sqlMetaData.type == SqlDbType.Timestamp || (sqlMetaData.IsIdentity && !this.IsCopyOption(SqlBulkCopyOptions.KeepIdentity)))
				{
					metaData[i] = null;
					flag2 = true;
				}
				int j = 0;
				while (j < this._localColumnMappings.Count)
				{
					if (this._localColumnMappings[j]._destinationColumnOrdinal == sqlMetaData.ordinal || this.UnquotedName(this._localColumnMappings[j]._destinationColumnName) == sqlMetaData.column)
					{
						if (flag2)
						{
							num2++;
							break;
						}
						this._sortedColumnMappings.Add(new _ColumnMapping(this._localColumnMappings[j]._internalSourceColumnOrdinal, sqlMetaData));
						hashSet.Add(sqlMetaData.column);
						num++;
						if (num > 1)
						{
							stringBuilder.Append(", ");
						}
						if (sqlMetaData.type == SqlDbType.Variant)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "sql_variant");
						}
						else if (sqlMetaData.type == SqlDbType.Udt)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "varbinary");
						}
						else
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, typeof(SqlDbType).GetEnumName(sqlMetaData.type));
						}
						byte nullableType = sqlMetaData.metaType.NullableType;
						if (nullableType <= 106)
						{
							if (nullableType - 41 > 2)
							{
								if (nullableType != 106)
								{
									goto IL_0321;
								}
								goto IL_029D;
							}
							else
							{
								stringBuilder.AppendFormat(null, "({0})", sqlMetaData.scale);
							}
						}
						else
						{
							if (nullableType == 108)
							{
								goto IL_029D;
							}
							if (nullableType != 240)
							{
								goto IL_0321;
							}
							if (sqlMetaData.IsLargeUdt)
							{
								stringBuilder.Append("(max)");
							}
							else
							{
								int length = sqlMetaData.length;
								stringBuilder.AppendFormat(null, "({0})", length);
							}
						}
						IL_03B2:
						if (this._connection.Is2000)
						{
							Result result = internalResults[2];
							object obj = result[i][3];
							SqlDbType type = sqlMetaData.type;
							if (type <= SqlDbType.NVarChar)
							{
								if (type != SqlDbType.Char && type - SqlDbType.NChar > 2)
								{
									goto IL_040B;
								}
								goto IL_0406;
							}
							else
							{
								if (type == SqlDbType.Text || type == SqlDbType.VarChar)
								{
									goto IL_0406;
								}
								goto IL_040B;
							}
							IL_040E:
							bool flag3;
							if (obj == null || !flag3)
							{
								break;
							}
							SqlString sqlString = (SqlString)obj;
							if (sqlString.IsNull)
							{
								break;
							}
							stringBuilder.Append(" COLLATE " + sqlString.Value);
							if (this._sqlDataReaderRowSource == null || sqlMetaData.collation == null)
							{
								break;
							}
							int internalSourceColumnOrdinal = this._localColumnMappings[j]._internalSourceColumnOrdinal;
							int lcid = sqlMetaData.collation.LCID;
							int localeId = this._sqlDataReaderRowSource.GetLocaleId(internalSourceColumnOrdinal);
							if (localeId != lcid)
							{
								throw SQL.BulkLoadLcidMismatch(localeId, this._sqlDataReaderRowSource.GetName(internalSourceColumnOrdinal), lcid, sqlMetaData.column);
							}
							break;
							IL_040B:
							flag3 = false;
							goto IL_040E;
							IL_0406:
							flag3 = true;
							goto IL_040E;
						}
						break;
						IL_029D:
						stringBuilder.AppendFormat(null, "({0},{1})", sqlMetaData.precision, sqlMetaData.scale);
						goto IL_03B2;
						IL_0321:
						if (!sqlMetaData.metaType.IsFixed && !sqlMetaData.metaType.IsLong)
						{
							int num3 = sqlMetaData.length;
							byte nullableType2 = sqlMetaData.metaType.NullableType;
							if (nullableType2 == 99 || nullableType2 == 231 || nullableType2 == 239)
							{
								num3 /= 2;
							}
							stringBuilder.AppendFormat(null, "({0})", num3);
							goto IL_03B2;
						}
						if (sqlMetaData.metaType.IsPlp && sqlMetaData.metaType.SqlDbType != SqlDbType.Xml)
						{
							stringBuilder.Append("(max)");
							goto IL_03B2;
						}
						goto IL_03B2;
					}
					else
					{
						j++;
					}
				}
				if (j == this._localColumnMappings.Count)
				{
					metaData[i] = null;
				}
			}
			if (num + num2 != this._localColumnMappings.Count)
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			stringBuilder.Append(")");
			if ((this._copyOptions & (SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.AllowEncryptedValueModifications)) != SqlBulkCopyOptions.Default || this.ColumnOrderHints.Count > 0)
			{
				bool flag4 = false;
				stringBuilder.Append(" with (");
				if (this.IsCopyOption(SqlBulkCopyOptions.KeepNulls))
				{
					stringBuilder.Append("KEEP_NULLS");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.TableLock))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "TABLOCK");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.CheckConstraints))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "CHECK_CONSTRAINTS");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.FireTriggers))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "FIRE_TRIGGERS");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.AllowEncryptedValueModifications))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "ALLOW_ENCRYPTED_VALUE_MODIFICATIONS");
					flag4 = true;
				}
				if (this.ColumnOrderHints.Count > 0)
				{
					stringBuilder.Append((flag4 ? ", " : "") + this.TryGetOrderHintText(hashSet));
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000303C0 File Offset: 0x0002E5C0
		private string TryGetOrderHintText(HashSet<string> destColumnNames)
		{
			StringBuilder stringBuilder = new StringBuilder("ORDER(");
			foreach (object obj in this.ColumnOrderHints)
			{
				SqlBulkCopyColumnOrderHint sqlBulkCopyColumnOrderHint = (SqlBulkCopyColumnOrderHint)obj;
				string column = sqlBulkCopyColumnOrderHint.Column;
				if (!destColumnNames.Contains(column))
				{
					throw SQL.BulkLoadOrderHintInvalidColumn(column);
				}
				if (!string.IsNullOrEmpty(column))
				{
					string text = SqlServerEscapeHelper.EscapeIdentifier(SqlServerEscapeHelper.EscapeStringAsLiteral(column));
					string text2 = ((sqlBulkCopyColumnOrderHint.SortOrder == SortOrder.Descending) ? "DESC" : "ASC");
					stringBuilder.Append(text + " " + text2 + ", ");
				}
			}
			stringBuilder.Length -= 2;
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000304A0 File Offset: 0x0002E6A0
		private Task SubmitUpdateBulkCommand(string TDSCommand)
		{
			SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlBulkCopy.SubmitUpdateBulkCommand|Info|Correlation> ObjectID{0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
			Task task = this._parser.TdsExecuteSQLBatch(TDSCommand, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true, null);
			if (task == null)
			{
				this.RunParser(null);
				return null;
			}
			return task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				this.RunParserReliably(null);
			}, TaskScheduler.Default);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00030510 File Offset: 0x0002E710
		private void WriteMetaData(BulkCopySimpleResultSet internalResults)
		{
			this._stateObj.SetTimeoutSeconds(this.BulkCopyTimeout);
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._stateObj._outputMessageType = 7;
			this._parser.WriteBulkCopyMetaData(metaData, this._sortedColumnMappings.Count, this._stateObj);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00030564 File Offset: 0x0002E764
		public void Close()
		{
			if (this._insideRowsCopiedEvent)
			{
				throw SQL.InvalidOperationInsideEvent();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00030584 File Offset: 0x0002E784
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._columnMappings = null;
				this._parser = null;
				try
				{
					if (this._internalTransaction != null)
					{
						this._internalTransaction.Rollback();
						this._internalTransaction.Dispose();
						this._internalTransaction = null;
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex);
				}
				finally
				{
					if (this._connection != null)
					{
						if (this._ownConnection)
						{
							this._connection.Dispose();
						}
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003061C File Offset: 0x0002E81C
		private object GetValueFromSourceRow(int destRowIndex, out bool isSqlType, out bool isDataFeed, out bool isNull)
		{
			_SqlMetaData metadata = this._sortedColumnMappings[destRowIndex]._metadata;
			int sourceColumnOrdinal = this._sortedColumnMappings[destRowIndex]._sourceColumnOrdinal;
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				if (this._currentRowMetadata[destRowIndex].IsDataFeed)
				{
					if (this._dbDataReaderRowSource.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isDataFeed = false;
						isNull = true;
						return DBNull.Value;
					}
					isSqlType = false;
					isDataFeed = true;
					isNull = false;
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.DataFeedStream:
						return new StreamDataFeed(this._dbDataReaderRowSource.GetStream(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedText:
						return new TextDataFeed(this._dbDataReaderRowSource.GetTextReader(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedXml:
						return new XmlDataFeed(this._sqlDataReaderRowSource.GetXmlReader(sourceColumnOrdinal));
					default:
					{
						isDataFeed = false;
						object value = this._dbDataReaderRowSource.GetValue(sourceColumnOrdinal);
						ADP.IsNullOrSqlType(value, out isNull, out isSqlType);
						return value;
					}
					}
				}
				else if (this._sqlDataReaderRowSource != null)
				{
					if (this._currentRowMetadata[destRowIndex].IsSqlType)
					{
						isSqlType = true;
						isDataFeed = false;
						INullable nullable;
						switch (this._currentRowMetadata[destRowIndex].Method)
						{
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
							nullable = this._sqlDataReaderRowSource.GetSqlDecimal(sourceColumnOrdinal);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
							nullable = (SqlDecimal)this._sqlDataReaderRowSource.GetSqlDouble(sourceColumnOrdinal);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
							nullable = (SqlDecimal)this._sqlDataReaderRowSource.GetSqlSingle(sourceColumnOrdinal);
							break;
						default:
							nullable = (INullable)this._sqlDataReaderRowSource.GetSqlValue(sourceColumnOrdinal);
							break;
						}
						isNull = nullable.IsNull;
						return nullable;
					}
					isSqlType = false;
					isDataFeed = false;
					object value2 = this._sqlDataReaderRowSource.GetValue(sourceColumnOrdinal);
					isNull = value2 == null || value2 == DBNull.Value;
					if (!isNull && metadata.type == SqlDbType.Udt)
					{
						INullable nullable2 = value2 as INullable;
						isNull = nullable2 != null && nullable2.IsNull;
					}
					return value2;
				}
				else
				{
					isDataFeed = false;
					IDataReader dataReader = (IDataReader)this._rowSource;
					if (this._enableStreaming && this._sqlDataReaderRowSource == null && dataReader.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isNull = true;
						return DBNull.Value;
					}
					object value3 = dataReader.GetValue(sourceColumnOrdinal);
					ADP.IsNullOrSqlType(value3, out isNull, out isSqlType);
					return value3;
				}
				break;
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
			{
				isDataFeed = false;
				object obj = this._currentRow[sourceColumnOrdinal];
				ADP.IsNullOrSqlType(obj, out isNull, out isSqlType);
				if (!isNull && this._currentRowMetadata[destRowIndex].IsSqlType)
				{
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
						if (isSqlType)
						{
							return (SqlDecimal)obj;
						}
						isSqlType = true;
						return new SqlDecimal((decimal)obj);
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
					{
						if (isSqlType)
						{
							return new SqlDecimal(((SqlDouble)obj).Value);
						}
						double num = (double)obj;
						if (!double.IsNaN(num))
						{
							isSqlType = true;
							return new SqlDecimal(num);
						}
						break;
					}
					case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
					{
						if (isSqlType)
						{
							return new SqlDecimal((double)((SqlSingle)obj).Value);
						}
						float num2 = (float)obj;
						if (!float.IsNaN(num2))
						{
							isSqlType = true;
							return new SqlDecimal((double)num2);
						}
						break;
					}
					}
				}
				return obj;
			}
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000309A0 File Offset: 0x0002EBA0
		private Task ReadFromRowSourceAsync(CancellationToken cts)
		{
			if (this._isAsyncBulkCopy && this._dbDataReaderRowSource != null)
			{
				return this._dbDataReaderRowSource.ReadAsync(cts).ContinueWith<Task<bool>>(delegate(Task<bool> task, object state)
				{
					if (task.Status == TaskStatus.RanToCompletion)
					{
						((SqlBulkCopy)state)._hasMoreRowToCopy = task.Result;
					}
					return task;
				}, this, TaskScheduler.Default).Unwrap<bool>();
			}
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			bool canBeReleasedFromAnyThread = openTdsConnection._parserLock.CanBeReleasedFromAnyThread;
			openTdsConnection._parserLock.Release();
			this._hasMoreRowToCopy = false;
			try
			{
				this._hasMoreRowToCopy = this.ReadFromRowSource();
			}
			catch (Exception ex)
			{
				if (this._isAsyncBulkCopy)
				{
					return Task.FromException<bool>(ex);
				}
				throw;
			}
			finally
			{
				openTdsConnection._parserLock.Wait(canBeReleasedFromAnyThread);
			}
			return null;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00030A74 File Offset: 0x0002EC74
		private bool ReadFromRowSource()
		{
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				return ((IDataReader)this._rowSource).Read();
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
				while (this._rowEnumerator.MoveNext())
				{
					this._currentRow = (DataRow)this._rowEnumerator.Current;
					if ((this._currentRow.RowState & this._rowStateToSkip) == (DataRowState)0)
					{
						this._currentRowLength = this._currentRow.ItemArray.Length;
						return true;
					}
				}
				return false;
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00030B08 File Offset: 0x0002ED08
		private SqlBulkCopy.SourceColumnMetadata GetColumnMetadata(int ordinal)
		{
			int sourceColumnOrdinal = this._sortedColumnMappings[ordinal]._sourceColumnOrdinal;
			_SqlMetaData metadata = this._sortedColumnMappings[ordinal]._metadata;
			bool flag;
			bool flag2;
			SqlBulkCopy.ValueMethod valueMethod;
			if ((this._sqlDataReaderRowSource != null || this._dataTableSource != null) && (metadata.metaType.NullableType == 106 || metadata.metaType.NullableType == 108))
			{
				flag = false;
				Type type;
				switch (this._rowSourceType)
				{
				case SqlBulkCopy.ValueSourceType.IDataReader:
				case SqlBulkCopy.ValueSourceType.DbDataReader:
					type = this._sqlDataReaderRowSource.GetFieldType(sourceColumnOrdinal);
					break;
				case SqlBulkCopy.ValueSourceType.DataTable:
				case SqlBulkCopy.ValueSourceType.RowArray:
					type = this._dataTableSource.Columns[sourceColumnOrdinal].DataType;
					break;
				default:
					type = null;
					break;
				}
				if (typeof(SqlDecimal) == type || typeof(decimal) == type)
				{
					flag2 = true;
					valueMethod = SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal;
				}
				else if (typeof(SqlDouble) == type || typeof(double) == type)
				{
					flag2 = true;
					valueMethod = SqlBulkCopy.ValueMethod.SqlTypeSqlDouble;
				}
				else if (typeof(SqlSingle) == type || typeof(float) == type)
				{
					flag2 = true;
					valueMethod = SqlBulkCopy.ValueMethod.SqlTypeSqlSingle;
				}
				else
				{
					flag2 = false;
					valueMethod = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else if (this._enableStreaming && metadata.length == 2147483647 && !this._rowSourceIsSqlDataReaderSmi)
			{
				flag2 = false;
				if (this._sqlDataReaderRowSource != null)
				{
					MetaType metaType = this._sqlDataReaderRowSource.MetaData[sourceColumnOrdinal].metaType;
					if (metadata.type == SqlDbType.VarBinary && metaType.IsBinType && metaType.SqlDbType != SqlDbType.Timestamp && this._sqlDataReaderRowSource.IsCommandBehavior(CommandBehavior.SequentialAccess))
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if ((metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar) && metaType.IsCharType && metaType.SqlDbType != SqlDbType.Xml)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else if (metadata.type == SqlDbType.Xml && metaType.SqlDbType == SqlDbType.Xml)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedXml;
					}
					else
					{
						flag = false;
						valueMethod = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else if (this._dbDataReaderRowSource != null)
				{
					if (metadata.type == SqlDbType.VarBinary)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if (metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar)
					{
						flag = true;
						valueMethod = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else
					{
						flag = false;
						valueMethod = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else
				{
					flag = false;
					valueMethod = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else
			{
				flag2 = false;
				flag = false;
				valueMethod = SqlBulkCopy.ValueMethod.GetValue;
			}
			return new SqlBulkCopy.SourceColumnMetadata(valueMethod, flag2, flag);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00030D7C File Offset: 0x0002EF7C
		private void CreateOrValidateConnection(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			if (this._connection.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this._ownConnection && this._connection.State != ConnectionState.Open)
			{
				this._connection.Open();
			}
			this._connection.ValidateConnectionForExecute(method, null);
			if (this._externalTransaction != null && this._connection != this._externalTransaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00030DFC File Offset: 0x0002EFFC
		private void RunParser(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00030E4C File Offset: 0x0002F04C
		private void RunParserReliably(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.RunReliably(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00030E9C File Offset: 0x0002F09C
		private void CommitTransaction()
		{
			if (this._internalTransaction != null)
			{
				SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
				openTdsConnection.ThreadHasParserLockForClose = true;
				try
				{
					this._internalTransaction.Commit();
					this._internalTransaction.Dispose();
					this._internalTransaction = null;
				}
				finally
				{
					openTdsConnection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00030EFC File Offset: 0x0002F0FC
		private void AbortTransaction()
		{
			if (this._internalTransaction != null)
			{
				if (!this._internalTransaction.IsZombied)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._internalTransaction.Rollback();
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
				}
				this._internalTransaction.Dispose();
				this._internalTransaction = null;
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00030F68 File Offset: 0x0002F168
		private void AppendColumnNameAndTypeName(StringBuilder query, string columnName, string typeName)
		{
			SqlServerEscapeHelper.EscapeIdentifier(query, columnName);
			query.Append(" ");
			query.Append(typeName);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00030F88 File Offset: 0x0002F188
		private string UnquotedName(string name)
		{
			if (ADP.IsEmpty(name))
			{
				return null;
			}
			if (name[0] == '[')
			{
				int length = name.Length;
				name = name.Substring(1, length - 2);
			}
			return name;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00030FC0 File Offset: 0x0002F1C0
		private object ValidateBulkCopyVariant(object value)
		{
			MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(value, true);
			byte tdstype = metaTypeFromValue.TDSType;
			if (tdstype <= 108)
			{
				if (tdstype <= 43)
				{
					if (tdstype != 36 && tdstype - 40 > 3)
					{
						goto IL_00AE;
					}
				}
				else
				{
					switch (tdstype)
					{
					case 48:
					case 50:
					case 52:
					case 56:
					case 59:
					case 60:
					case 61:
					case 62:
						break;
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
					case 58:
						goto IL_00AE;
					default:
						if (tdstype != 108)
						{
							goto IL_00AE;
						}
						break;
					}
				}
			}
			else if (tdstype <= 165)
			{
				if (tdstype != 127 && tdstype != 165)
				{
					goto IL_00AE;
				}
			}
			else if (tdstype != 167 && tdstype != 231)
			{
				goto IL_00AE;
			}
			if (value is INullable)
			{
				return MetaType.GetComValueFromSqlVariant(value);
			}
			return value;
			IL_00AE:
			throw SQL.BulkLoadInvalidVariantValue();
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00031080 File Offset: 0x0002F280
		private object ConvertValue(object value, _SqlMetaData metadata, bool isNull, ref bool isSqlType, out bool coercedToDataFeed)
		{
			coercedToDataFeed = false;
			if (!isNull)
			{
				MetaType metaType = metadata.metaType;
				bool flag = false;
				byte b = metadata.scale;
				byte b2 = metadata.precision;
				int num = metadata.length;
				if (metadata.isEncrypted)
				{
					metaType = metadata.baseTI.metaType;
					b = metadata.baseTI.scale;
					b2 = metadata.baseTI.precision;
					num = metadata.baseTI.length;
				}
				object obj;
				try
				{
					byte nullableType = metaType.NullableType;
					MetaType metaType2;
					if (nullableType <= 165)
					{
						if (nullableType <= 59)
						{
							switch (nullableType)
							{
							case 34:
							case 35:
							case 36:
							case 38:
							case 40:
							case 41:
							case 42:
							case 43:
							case 50:
								break;
							case 37:
							case 39:
							case 44:
							case 45:
							case 46:
							case 47:
							case 48:
							case 49:
								goto IL_0384;
							default:
								if (nullableType - 58 > 1)
								{
									goto IL_0384;
								}
								break;
							}
						}
						else if (nullableType - 61 > 1)
						{
							switch (nullableType)
							{
							case 98:
								value = this.ValidateBulkCopyVariant(value);
								flag = true;
								goto IL_03B0;
							case 99:
								goto IL_02AF;
							case 100:
							case 101:
							case 102:
							case 103:
							case 105:
							case 107:
								goto IL_0384;
							case 104:
							case 109:
							case 110:
							case 111:
								break;
							case 106:
							case 108:
							{
								metaType2 = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
								value = SqlParameter.CoerceValue(value, metaType2, out coercedToDataFeed, out flag, false);
								SqlDecimal sqlDecimal;
								if (isSqlType && !flag)
								{
									sqlDecimal = (SqlDecimal)value;
								}
								else
								{
									sqlDecimal = new SqlDecimal((decimal)value);
								}
								if (sqlDecimal.Scale != b)
								{
									sqlDecimal = TdsParser.AdjustSqlDecimalScale(sqlDecimal, (int)b);
								}
								if (sqlDecimal.Precision > b2)
								{
									try
									{
										sqlDecimal = SqlDecimal.ConvertToPrecScale(sqlDecimal, (int)b2, (int)sqlDecimal.Scale);
									}
									catch (SqlTruncateException)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType2, metadata.ordinal, this.RowNumber, metadata.isEncrypted, metadata.column, value.ToString(), ADP.ParameterValueOutOfRange(sqlDecimal));
									}
									catch (Exception ex)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType2, metadata.ordinal, this.RowNumber, metadata.isEncrypted, metadata.column, value.ToString(), ex);
									}
								}
								value = sqlDecimal;
								isSqlType = true;
								flag = false;
								goto IL_03B0;
							}
							default:
								if (nullableType != 165)
								{
									goto IL_0384;
								}
								break;
							}
						}
					}
					else if (nullableType <= 173)
					{
						if (nullableType != 167 && nullableType != 173)
						{
							goto IL_0384;
						}
					}
					else if (nullableType != 175)
					{
						if (nullableType == 231)
						{
							goto IL_02AF;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_02AF;
						case 240:
							if (!(value is byte[]))
							{
								value = this._connection.GetBytes(value);
								flag = true;
								goto IL_03B0;
							}
							goto IL_03B0;
						case 241:
							if (value is XmlReader)
							{
								value = new XmlDataFeed((XmlReader)value);
								flag = true;
								coercedToDataFeed = true;
								goto IL_03B0;
							}
							goto IL_03B0;
						default:
							goto IL_0384;
						}
					}
					metaType2 = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaType2, out coercedToDataFeed, out flag, false);
					goto IL_03B0;
					IL_02AF:
					metaType2 = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaType2, out coercedToDataFeed, out flag, false);
					if (coercedToDataFeed)
					{
						goto IL_03B0;
					}
					string text = ((isSqlType && !flag) ? ((SqlString)value).Value : ((string)value));
					int num2 = num / 2;
					if (text.Length > num2)
					{
						if (metadata.isEncrypted)
						{
							text = "<encrypted>";
						}
						else
						{
							text = text.Remove(Math.Min(num2, 100));
						}
						throw SQL.BulkLoadStringTooLong(this._destinationTableName, metadata.column, text);
					}
					goto IL_03B0;
					IL_0384:
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType, metadata.ordinal, this.RowNumber, metadata.isEncrypted, metadata.column, value.ToString(), null);
					IL_03B0:
					if (flag)
					{
						isSqlType = false;
					}
					obj = value;
				}
				catch (Exception ex2)
				{
					if (!ADP.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType, metadata.ordinal, this.RowNumber, metadata.isEncrypted, metadata.column, value.ToString(), ex2);
				}
				return obj;
			}
			if (!metadata.IsNullable)
			{
				throw SQL.BulkLoadBulkLoadNotAllowDBNull(metadata.column);
			}
			return value;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000314D4 File Offset: 0x0002F6D4
		public void WriteToServer(DbDataReader reader)
		{
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._dbDataReaderRowSource = reader;
				this._sqlDataReaderRowSource = reader as SqlDataReader;
				if (this._sqlDataReaderRowSource != null)
				{
					this._rowSourceIsSqlDataReaderSmi = this._sqlDataReaderRowSource is SqlDataReaderSmi;
				}
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00031590 File Offset: 0x0002F790
		public void WriteToServer(IDataReader reader)
		{
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._sqlDataReaderRowSource = this._rowSource as SqlDataReader;
				if (this._sqlDataReaderRowSource != null)
				{
					this._rowSourceIsSqlDataReaderSmi = this._sqlDataReaderRowSource is SqlDataReaderSmi;
				}
				this._dbDataReaderRowSource = this._rowSource as DbDataReader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003165C File Offset: 0x0002F85C
		public void WriteToServer(DataTable table)
		{
			this.WriteToServer(table, (DataRowState)0);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00031668 File Offset: 0x0002F868
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			SqlConnection.ExecutePermission.Demand();
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._dataTableSource = table;
				this._sqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00031728 File Offset: 0x0002F928
		public void WriteToServer(DataRow[] rows)
		{
			SqlConnection.ExecutePermission.Demand();
			SqlStatistics sqlStatistics = this.Statistics;
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			if (rows.Length == 0)
			{
				return;
			}
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._sqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000317E4 File Offset: 0x0002F9E4
		public Task WriteToServerAsync(DataRow[] rows)
		{
			return this.WriteToServerAsync(rows, CancellationToken.None);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000317F4 File Offset: 0x0002F9F4
		public Task WriteToServerAsync(DataRow[] rows, CancellationToken cancellationToken)
		{
			Task task = null;
			SqlConnection.ExecutePermission.Demand();
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				if (rows.Length == 0)
				{
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					if (cancellationToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
					}
					else
					{
						taskCompletionSource.SetResult(null);
					}
					task = taskCompletionSource.Task;
					return task;
				}
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._sqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000318DC File Offset: 0x0002FADC
		public Task WriteToServerAsync(DbDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000318EC File Offset: 0x0002FAEC
		public Task WriteToServerAsync(DbDataReader reader, CancellationToken cancellationToken)
		{
			Task task = null;
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._sqlDataReaderRowSource = reader as SqlDataReader;
				this._dbDataReaderRowSource = reader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003198C File Offset: 0x0002FB8C
		public Task WriteToServerAsync(IDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003199C File Offset: 0x0002FB9C
		public Task WriteToServerAsync(IDataReader reader, CancellationToken cancellationToken)
		{
			Task task = null;
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._sqlDataReaderRowSource = this._rowSource as SqlDataReader;
				this._dbDataReaderRowSource = this._rowSource as DbDataReader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00031A4C File Offset: 0x0002FC4C
		public Task WriteToServerAsync(DataTable table)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, CancellationToken.None);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00031A5B File Offset: 0x0002FC5B
		public Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, cancellationToken);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00031A66 File Offset: 0x0002FC66
		public Task WriteToServerAsync(DataTable table, DataRowState rowState)
		{
			return this.WriteToServerAsync(table, rowState, CancellationToken.None);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00031A78 File Offset: 0x0002FC78
		public Task WriteToServerAsync(DataTable table, DataRowState rowState, CancellationToken cancellationToken)
		{
			Task task = null;
			SqlConnection.ExecutePermission.Demand();
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics sqlStatistics = this.Statistics;
			try
			{
				sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._sqlDataReaderRowSource = null;
				this._dataTableSource = table;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				task = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return task;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00031B38 File Offset: 0x0002FD38
		private Task WriteRowSourceToServerAsync(int columnCount, CancellationToken ctoken)
		{
			if (ctoken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<object>();
			}
			Task currentReconnectionTask = this._connection._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				if (this._isAsyncBulkCopy)
				{
					TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
					currentReconnectionTask.ContinueWith(delegate(Task t)
					{
						Task task3 = this.WriteRowSourceToServerAsync(columnCount, ctoken);
						if (task3 == null)
						{
							tcs.SetResult(null);
							return;
						}
						AsyncHelper.ContinueTaskWithState(task3, tcs, tcs, delegate(object state)
						{
							((TaskCompletionSource<object>)state).SetResult(null);
						}, null, null, null, null, null);
					}, ctoken);
					return tcs.Task;
				}
				AsyncHelper.WaitForCompletion(currentReconnectionTask, this.BulkCopyTimeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, false);
			}
			bool flag = true;
			this._isBulkCopyingInProgress = true;
			this.CreateOrValidateConnection("WriteToServer");
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			this._parserLock = openTdsConnection._parserLock;
			this._parserLock.Wait(this._isAsyncBulkCopy);
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			Task task2;
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
				this.WriteRowSourceToServerCommon(columnCount);
				Task task4 = this.WriteToServerInternalAsync(ctoken);
				if (task4 != null)
				{
					flag = false;
					task2 = task4.ContinueWith<Task>(delegate(Task task, object state)
					{
						SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
						try
						{
							sqlBulkCopy.AbortTransaction();
						}
						finally
						{
							sqlBulkCopy._isBulkCopyingInProgress = false;
							if (sqlBulkCopy._parser != null)
							{
								sqlBulkCopy._parser._asyncWrite = false;
							}
							if (sqlBulkCopy._parserLock != null)
							{
								sqlBulkCopy._parserLock.Release();
								sqlBulkCopy._parserLock = null;
							}
						}
						return task;
					}, this, TaskScheduler.Default).Unwrap();
				}
				else
				{
					task2 = null;
				}
			}
			catch (OutOfMemoryException ex)
			{
				this._connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this._connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this._connection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			finally
			{
				this._columnMappings.ReadOnly = false;
				if (flag)
				{
					try
					{
						this.AbortTransaction();
					}
					finally
					{
						this._isBulkCopyingInProgress = false;
						if (this._parser != null)
						{
							this._parser._asyncWrite = false;
						}
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}
				}
			}
			return task2;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00031D6C File Offset: 0x0002FF6C
		private void WriteRowSourceToServerCommon(int columnCount)
		{
			bool flag = false;
			this._columnMappings.ReadOnly = true;
			this._localColumnMappings = this._columnMappings;
			if (this._localColumnMappings.Count > 0)
			{
				this._localColumnMappings.ValidateCollection();
				using (IEnumerator enumerator = this._localColumnMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
						if (sqlBulkCopyColumnMapping._internalSourceColumnOrdinal == -1)
						{
							flag = true;
							break;
						}
					}
					goto IL_008C;
				}
			}
			this._localColumnMappings = new SqlBulkCopyColumnMappingCollection();
			this._localColumnMappings.CreateDefaultMapping(columnCount);
			IL_008C:
			if (flag)
			{
				int num = -1;
				flag = false;
				if (this._localColumnMappings.Count > 0)
				{
					foreach (object obj2 in this._localColumnMappings)
					{
						SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping2 = (SqlBulkCopyColumnMapping)obj2;
						if (sqlBulkCopyColumnMapping2._internalSourceColumnOrdinal == -1)
						{
							string text = this.UnquotedName(sqlBulkCopyColumnMapping2.SourceColumn);
							switch (this._rowSourceType)
							{
							case SqlBulkCopy.ValueSourceType.IDataReader:
							case SqlBulkCopy.ValueSourceType.DbDataReader:
								try
								{
									num = ((IDataRecord)this._rowSource).GetOrdinal(text);
								}
								catch (IndexOutOfRangeException ex)
								{
									throw SQL.BulkLoadNonMatchingColumnName(text, ex);
								}
								break;
							case SqlBulkCopy.ValueSourceType.DataTable:
								num = ((DataTable)this._rowSource).Columns.IndexOf(text);
								break;
							case SqlBulkCopy.ValueSourceType.RowArray:
								num = ((DataRow[])this._rowSource)[0].Table.Columns.IndexOf(text);
								break;
							}
							if (num == -1)
							{
								throw SQL.BulkLoadNonMatchingColumnName(text);
							}
							sqlBulkCopyColumnMapping2._internalSourceColumnOrdinal = num;
						}
					}
				}
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00031F40 File Offset: 0x00030140
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00031F60 File Offset: 0x00030160
		private bool FireRowsCopiedEvent(long rowsCopied)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			bool canBeReleasedFromAnyThread = openTdsConnection._parserLock.CanBeReleasedFromAnyThread;
			openTdsConnection._parserLock.Release();
			SqlRowsCopiedEventArgs sqlRowsCopiedEventArgs = new SqlRowsCopiedEventArgs(rowsCopied);
			try
			{
				this._insideRowsCopiedEvent = true;
				SqlRowsCopiedEventHandler sqlRowsCopied = this.SqlRowsCopied;
				if (sqlRowsCopied != null)
				{
					sqlRowsCopied(this, sqlRowsCopiedEventArgs);
				}
			}
			finally
			{
				this._insideRowsCopiedEvent = false;
				openTdsConnection._parserLock.Wait(canBeReleasedFromAnyThread);
			}
			return sqlRowsCopiedEventArgs.Abort;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00031FE0 File Offset: 0x000301E0
		private Task ReadWriteColumnValueAsync(int col)
		{
			bool flag;
			bool flag2;
			bool flag3;
			object obj = this.GetValueFromSourceRow(col, out flag, out flag2, out flag3);
			_SqlMetaData metadata = this._sortedColumnMappings[col]._metadata;
			if (!flag2)
			{
				obj = this.ConvertValue(obj, metadata, flag3, ref flag, out flag2);
				if (!flag3 && metadata.isEncrypted)
				{
					obj = this._parser.EncryptColumnValue(obj, metadata, metadata.column, this._stateObj, flag2, flag);
					flag = false;
				}
			}
			Task task = null;
			if (metadata.type != SqlDbType.Variant)
			{
				task = this._parser.WriteBulkCopyValue(obj, metadata, this._stateObj, flag, flag2, flag3);
			}
			else
			{
				SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
				if (this._sqlDataReaderRowSource != null && this._connection.Is2008OrNewer)
				{
					storageType = this._sqlDataReaderRowSource.GetVariantInternalStorageType(this._sortedColumnMappings[col]._sourceColumnOrdinal);
				}
				if (storageType == SqlBuffer.StorageType.DateTime2)
				{
					this._parser.WriteSqlVariantDateTime2((DateTime)obj, this._stateObj);
				}
				else if (storageType == SqlBuffer.StorageType.Date)
				{
					this._parser.WriteSqlVariantDate((DateTime)obj, this._stateObj);
				}
				else
				{
					task = this._parser.WriteSqlVariantDataRowValue(obj, this._stateObj, true);
				}
			}
			return task;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00032104 File Offset: 0x00030304
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outterTask)
		{
			SqlConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			connection.RegisterForConnectionCloseNotification<T>(ref outterTask, this, 3);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003212C File Offset: 0x0003032C
		private Task CopyColumnsAsync(int col, TaskCompletionSource<object> source = null)
		{
			Task task = null;
			Task task2 = null;
			try
			{
				int i;
				for (i = col; i < this._sortedColumnMappings.Count; i++)
				{
					task2 = this.ReadWriteColumnValueAsync(i);
					if (task2 != null)
					{
						break;
					}
				}
				if (task2 != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
						task = source.Task;
					}
					this.CopyColumnsAsyncSetupContinuation(source, task2, i);
					return task;
				}
				if (source != null)
				{
					source.SetResult(null);
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
			}
			return task;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000321B4 File Offset: 0x000303B4
		private void CopyColumnsAsyncSetupContinuation(TaskCompletionSource<object> source, Task task, int i)
		{
			AsyncHelper.ContinueTaskWithState(task, source, this, delegate(object state)
			{
				SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
				if (i + 1 < sqlBulkCopy._sortedColumnMappings.Count)
				{
					sqlBulkCopy.CopyColumnsAsync(i + 1, source);
					return;
				}
				source.SetResult(null);
			}, null, null, null, this._connection.GetOpenTdsConnection(), null);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00032200 File Offset: 0x00030400
		private void CheckAndRaiseNotification()
		{
			bool flag = false;
			Exception ex = null;
			this._rowsCopied++;
			if (this._notifyAfter > 0 && this._rowsUntilNotification > 0)
			{
				int num = this._rowsUntilNotification - 1;
				this._rowsUntilNotification = num;
				if (num == 0)
				{
					try
					{
						this._stateObj.BcpLock = true;
						flag = this.FireRowsCopiedEvent((long)this._rowsCopied);
						SqlClientEventSource.Log.TryTraceEvent("<sc.SqlBulkCopy.WriteToServerInternal|INFO>");
						if (ConnectionState.Open != this._connection.State)
						{
							ex = ADP.OpenConnectionRequired("CheckAndRaiseNotification", this._connection.State);
						}
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							ex = ex2;
						}
						else
						{
							ex = OperationAbortedException.Aborted(ex2);
						}
					}
					finally
					{
						this._stateObj.BcpLock = false;
					}
					if (!flag)
					{
						this._rowsUntilNotification = this._notifyAfter;
					}
				}
			}
			if (!flag && this._rowsUntilNotification > this._notifyAfter)
			{
				this._rowsUntilNotification = this._notifyAfter;
			}
			if (ex == null && flag)
			{
				ex = OperationAbortedException.Aborted(null);
			}
			if (this._connection.State != ConnectionState.Open)
			{
				throw ADP.OpenConnectionRequired("WriteToServer", this._connection.State);
			}
			if (ex != null)
			{
				this._parser._asyncWrite = false;
				Task task = this._parser.WriteBulkCopyDone(this._stateObj);
				this.RunParser(null);
				this.AbortTransaction();
				throw ex;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003236C File Offset: 0x0003056C
		private Task CheckForCancellation(CancellationToken cts, TaskCompletionSource<object> tcs)
		{
			if (cts.IsCancellationRequested)
			{
				if (tcs == null)
				{
					tcs = new TaskCompletionSource<object>();
				}
				tcs.SetCanceled();
				return tcs.Task;
			}
			return null;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00032390 File Offset: 0x00030590
		private Task CopyRowsAsync(int rowsSoFar, int totalRows, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			Task task = null;
			try
			{
				int i = rowsSoFar;
				Action<object> <>9__1;
				Action<object> <>9__2;
				while ((totalRows <= 0 || i < totalRows) && this._hasMoreRowToCopy)
				{
					if (this._isAsyncBulkCopy)
					{
						task = this.CheckForCancellation(cts, source);
						if (task != null)
						{
							return task;
						}
					}
					this._stateObj.WriteByte(209);
					Task task2 = this.CopyColumnsAsync(0, null);
					if (task2 != null)
					{
						source = source ?? new TaskCompletionSource<object>();
						task = source.Task;
						AsyncHelper.ContinueTaskWithState(task2, source, this, delegate(object state)
						{
							SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
							sqlBulkCopy.CheckAndRaiseNotification();
							Task task5 = sqlBulkCopy.ReadFromRowSourceAsync(cts);
							if (task5 == null)
							{
								sqlBulkCopy.CopyRowsAsync(i + 1, totalRows, cts, source);
								return;
							}
							Task task6 = task5;
							TaskCompletionSource<object> source3 = source;
							object obj = sqlBulkCopy;
							Action<object> action2;
							if ((action2 = <>9__2) == null)
							{
								action2 = (<>9__2 = delegate(object state2)
								{
									((SqlBulkCopy)state2).CopyRowsAsync(i + 1, totalRows, cts, source);
								});
							}
							AsyncHelper.ContinueTaskWithState(task6, source3, obj, action2, null, null, null, this._connection.GetOpenTdsConnection(), null);
						}, null, null, null, this._connection.GetOpenTdsConnection(), null);
						return task;
					}
					this.CheckAndRaiseNotification();
					Task task3 = this.ReadFromRowSourceAsync(cts);
					if (task3 != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						task = source.Task;
						Task task4 = task3;
						TaskCompletionSource<object> source2 = source;
						Action<object> action;
						if ((action = <>9__1) == null)
						{
							action = (<>9__1 = delegate(object state)
							{
								((SqlBulkCopy)state).CopyRowsAsync(i + 1, totalRows, cts, source);
							});
						}
						AsyncHelper.ContinueTaskWithState(task4, source2, this, action, null, null, null, this._connection.GetOpenTdsConnection(), null);
						return task;
					}
					int j = i;
					i = j + 1;
				}
				if (source != null)
				{
					source.TrySetResult(null);
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
			}
			return task;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00032568 File Offset: 0x00030768
		private Task CopyBatchesAsync(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			try
			{
				Action<object> <>9__0;
				while (this._hasMoreRowToCopy)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					if (this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
					{
						openTdsConnection.ThreadHasParserLockForClose = true;
						try
						{
							this._internalTransaction = this._connection.BeginTransaction();
						}
						finally
						{
							openTdsConnection.ThreadHasParserLockForClose = false;
						}
					}
					Task task = this.SubmitUpdateBulkCommand(updateBulkCommandText);
					if (task != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						Task task2 = task;
						TaskCompletionSource<object> source2 = source;
						Action<object> action;
						if ((action = <>9__0) == null)
						{
							action = (<>9__0 = delegate(object state)
							{
								SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
								if (sqlBulkCopy.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source) == null)
								{
									sqlBulkCopy.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
								}
							});
						}
						AsyncHelper.ContinueTaskWithState(task2, source2, this, action, null, null, null, this._connection.GetOpenTdsConnection(), null);
						return source.Task;
					}
					Task task3 = this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source);
					if (task3 != null)
					{
						return task3;
					}
				}
			}
			catch (Exception ex)
			{
				if (source != null)
				{
					source.TrySetException(ex);
					return source.Task;
				}
				throw;
			}
			if (source != null)
			{
				source.SetResult(null);
				return source.Task;
			}
			return null;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000326E4 File Offset: 0x000308E4
		private Task CopyBatchesAsyncContinued(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task2;
			try
			{
				this.WriteMetaData(internalResults);
				this._parser.LoadColumnEncryptionKeys(internalResults[1].MetaData, this._connection, null);
				Task task = this.CopyRowsAsync(0, this._savedBatchSize, cts, null);
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTaskWithState(task, source, this, delegate(object state)
					{
						SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
						if (sqlBulkCopy.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source) == null)
						{
							sqlBulkCopy.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
						}
					}, delegate(Exception _, object state)
					{
						((SqlBulkCopy)state).CopyBatchesAsyncContinuedOnError(false);
					}, delegate(object state)
					{
						((SqlBulkCopy)state).CopyBatchesAsyncContinuedOnError(true);
					}, null, this._connection.GetOpenTdsConnection(), null);
					task2 = source.Task;
				}
				else
				{
					task2 = this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source);
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
				task2 = source.Task;
			}
			return task2;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003283C File Offset: 0x00030A3C
		private Task CopyBatchesAsyncContinuedOnSuccess(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task2;
			try
			{
				Task task = this._parser.WriteBulkCopyDone(this._stateObj);
				if (task == null)
				{
					this.RunParser(null);
					this.CommitTransaction();
					task2 = null;
				}
				else
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTaskWithState(task, source, this, delegate(object state)
					{
						SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
						try
						{
							sqlBulkCopy.RunParser(null);
							sqlBulkCopy.CommitTransaction();
						}
						catch (Exception)
						{
							sqlBulkCopy.CopyBatchesAsyncContinuedOnError(false);
							throw;
						}
						sqlBulkCopy.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
					}, delegate(Exception _, object state)
					{
						((SqlBulkCopy)state).CopyBatchesAsyncContinuedOnError(false);
					}, null, null, this._connection.GetOpenTdsConnection(), null);
					task2 = source.Task;
				}
			}
			catch (Exception ex)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex);
				task2 = source.Task;
			}
			return task2;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00032934 File Offset: 0x00030B34
		private void CopyBatchesAsyncContinuedOnError(bool cleanupParser)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (cleanupParser && this._parser != null && this._stateObj != null)
				{
					this._parser._asyncWrite = false;
					Task task = this._parser.WriteBulkCopyDone(this._stateObj);
					this.RunParser(null);
				}
				if (this._stateObj != null)
				{
					this.CleanUpStateObject(true);
				}
			}
			catch (OutOfMemoryException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			this.AbortTransaction();
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000329E4 File Offset: 0x00030BE4
		private void CleanUpStateObject(bool isCancelRequested = true)
		{
			if (this._stateObj != null)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this._stateObj.ResetBuffer();
					this._stateObj.ResetPacketCounters();
					if (isCancelRequested && (this._parser.State == TdsParserState.OpenNotLoggedIn || this._parser.State == TdsParserState.OpenLoggedIn))
					{
						this._stateObj.CancelRequest();
					}
					this._stateObj.SetTimeoutStateStopped();
					this._stateObj.CloseSession();
					this._stateObj._bulkCopyOpperationInProgress = false;
					this._stateObj._bulkCopyWriteTimeout = false;
					this._stateObj = null;
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00032AA8 File Offset: 0x00030CA8
		private void WriteToServerInternalRestContinuedAsync(BulkCopySimpleResultSet internalResults, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task = null;
			try
			{
				string text = this.AnalyzeTargetAndCreateUpdateBulkCommand(internalResults);
				if (this._sortedColumnMappings.Count != 0)
				{
					this._stateObj.SniContext = SniContext.Snix_SendRows;
					this._savedBatchSize = this._batchSize;
					this._rowsUntilNotification = this._notifyAfter;
					this._rowsCopied = 0;
					this._currentRowMetadata = new SqlBulkCopy.SourceColumnMetadata[this._sortedColumnMappings.Count];
					for (int i = 0; i < this._currentRowMetadata.Length; i++)
					{
						this._currentRowMetadata[i] = this.GetColumnMetadata(i);
					}
					task = this.CopyBatchesAsync(internalResults, text, cts, null);
				}
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTaskWithState(task, source, this, delegate(object state)
					{
						SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
						if (task.IsCanceled)
						{
							sqlBulkCopy._localColumnMappings = null;
							try
							{
								sqlBulkCopy.CleanUpStateObject(true);
								return;
							}
							finally
							{
								source.SetCanceled();
							}
						}
						if (task.Exception != null)
						{
							source.SetException(task.Exception.InnerException);
							return;
						}
						sqlBulkCopy._localColumnMappings = null;
						try
						{
							sqlBulkCopy.CleanUpStateObject(false);
						}
						finally
						{
							if (source != null)
							{
								if (cts.IsCancellationRequested)
								{
									source.SetCanceled();
								}
								else
								{
									source.SetResult(null);
								}
							}
						}
					}, null, null, null, this._connection.GetOpenTdsConnection(), null);
				}
				else
				{
					this._localColumnMappings = null;
					try
					{
						this.CleanUpStateObject(false);
					}
					catch (Exception ex)
					{
					}
					if (source != null)
					{
						source.SetResult(null);
					}
				}
			}
			catch (Exception ex2)
			{
				this._localColumnMappings = null;
				try
				{
					this.CleanUpStateObject(true);
				}
				catch (Exception ex3)
				{
				}
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex2);
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00032C58 File Offset: 0x00030E58
		private void WriteToServerInternalRestAsync(CancellationToken cts, TaskCompletionSource<object> source)
		{
			this._hasMoreRowToCopy = true;
			Task<BulkCopySimpleResultSet> internalResultsTask = null;
			BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			try
			{
				this._parser = this._connection.Parser;
				this._parser._asyncWrite = this._isAsyncBulkCopy;
				Task task;
				try
				{
					task = this._connection.ValidateAndReconnect(delegate
					{
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}, this.BulkCopyTimeout);
				}
				catch (SqlException ex)
				{
					SqlException ex5;
					throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex5);
				}
				if (task != null)
				{
					if (this._isAsyncBulkCopy)
					{
						StrongBox<CancellationTokenRegistration> strongBox = new StrongBox<CancellationTokenRegistration>(default(CancellationTokenRegistration));
						TaskCompletionSource<object> cancellableReconnectTS = new TaskCompletionSource<object>();
						if (cts.CanBeCanceled)
						{
							strongBox.Value = cts.Register(delegate
							{
								cancellableReconnectTS.TrySetCanceled();
							});
						}
						AsyncHelper.ContinueTaskWithState(task, cancellableReconnectTS, cancellableReconnectTS, delegate(object state)
						{
							((TaskCompletionSource<object>)state).SetResult(null);
						}, null, null, null, null, null);
						AsyncHelper.SetTimeoutException(cancellableReconnectTS, this.BulkCopyTimeout, () => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, SQL.CR_ReconnectTimeout()), CancellationToken.None);
						AsyncHelper.ContinueTaskWithState(cancellableReconnectTS.Task, source, strongBox, delegate(object state)
						{
							((StrongBox<CancellationTokenRegistration>)state).Value.Dispose();
							if (this._parserLock != null)
							{
								this._parserLock.Release();
								this._parserLock = null;
							}
							this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
							this._parserLock.Wait(true);
							this.WriteToServerInternalRestAsync(cts, source);
						}, delegate(Exception _, object state)
						{
							((StrongBox<CancellationTokenRegistration>)state).Value.Dispose();
						}, delegate(object state)
						{
							((StrongBox<CancellationTokenRegistration>)state).Value.Dispose();
						}, (Exception ex, object state) => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex), null, this._connection);
					}
					else
					{
						try
						{
							AsyncHelper.WaitForCompletion(task, this.BulkCopyTimeout, delegate
							{
								throw SQL.CR_ReconnectTimeout();
							}, true);
						}
						catch (SqlException ex2)
						{
							throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex2);
						}
						this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
						this._parserLock.Wait(false);
						this.WriteToServerInternalRestAsync(cts, source);
					}
				}
				else
				{
					if (this._isAsyncBulkCopy)
					{
						this._connection.AddWeakReference(this, 3);
					}
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._stateObj = this._parser.GetSession(this);
						this._stateObj._bulkCopyOpperationInProgress = true;
						this._stateObj.StartSession(this.ObjectID);
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
					try
					{
						internalResultsTask = this.CreateAndExecuteInitialQueryAsync(out bulkCopySimpleResultSet);
					}
					catch (SqlException ex3)
					{
						throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex3);
					}
					if (internalResultsTask != null)
					{
						AsyncHelper.ContinueTaskWithState(internalResultsTask, source, this, delegate(object state)
						{
							((SqlBulkCopy)state).WriteToServerInternalRestContinuedAsync(internalResultsTask.Result, cts, source);
						}, null, null, null, this._connection.GetOpenTdsConnection(), null);
					}
					else
					{
						this.WriteToServerInternalRestContinuedAsync(bulkCopySimpleResultSet, cts, source);
					}
				}
			}
			catch (Exception ex4)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(ex4);
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00033000 File Offset: 0x00031200
		private Task WriteToServerInternalAsync(CancellationToken ctoken)
		{
			TaskCompletionSource<object> source = null;
			Task<object> task = null;
			if (this._isAsyncBulkCopy)
			{
				source = new TaskCompletionSource<object>();
				task = source.Task;
				this.RegisterForConnectionCloseNotification<object>(ref task);
			}
			if (this._destinationTableName != null)
			{
				try
				{
					Task task2 = this.ReadFromRowSourceAsync(ctoken);
					if (task2 != null)
					{
						AsyncHelper.ContinueTaskWithState(task2, source, this, delegate(object state)
						{
							SqlBulkCopy sqlBulkCopy = (SqlBulkCopy)state;
							if (!sqlBulkCopy._hasMoreRowToCopy)
							{
								source.SetResult(null);
								return;
							}
							sqlBulkCopy.WriteToServerInternalRestAsync(ctoken, source);
						}, null, null, null, this._connection.GetOpenTdsConnection(), null);
						return task;
					}
					if (!this._hasMoreRowToCopy)
					{
						if (source != null)
						{
							source.SetResult(null);
						}
						return task;
					}
					this.WriteToServerInternalRestAsync(ctoken, source);
					return task;
				}
				catch (Exception ex)
				{
					if (source == null)
					{
						throw;
					}
					source.TrySetException(ex);
				}
				return task;
			}
			if (source != null)
			{
				source.SetException(SQL.BulkLoadMissingDestinationTable());
				return task;
			}
			throw SQL.BulkLoadMissingDestinationTable();
		}

		// Token: 0x04000666 RID: 1638
		private const int TranCountResultId = 0;

		// Token: 0x04000667 RID: 1639
		private const int TranCountRowId = 0;

		// Token: 0x04000668 RID: 1640
		private const int TranCountValueId = 0;

		// Token: 0x04000669 RID: 1641
		private const int MetaDataResultId = 1;

		// Token: 0x0400066A RID: 1642
		private const int CollationResultId = 2;

		// Token: 0x0400066B RID: 1643
		private const int ColIdId = 0;

		// Token: 0x0400066C RID: 1644
		private const int NameId = 1;

		// Token: 0x0400066D RID: 1645
		private const int Tds_CollationId = 2;

		// Token: 0x0400066E RID: 1646
		private const int CollationId = 3;

		// Token: 0x0400066F RID: 1647
		private const int MAX_LENGTH = 2147483647;

		// Token: 0x04000670 RID: 1648
		private const int DefaultCommandTimeout = 30;

		// Token: 0x04000672 RID: 1650
		private bool _enableStreaming;

		// Token: 0x04000673 RID: 1651
		private int _batchSize;

		// Token: 0x04000674 RID: 1652
		private readonly bool _ownConnection;

		// Token: 0x04000675 RID: 1653
		private readonly SqlBulkCopyOptions _copyOptions;

		// Token: 0x04000676 RID: 1654
		private int _timeout = 30;

		// Token: 0x04000677 RID: 1655
		private string _destinationTableName;

		// Token: 0x04000678 RID: 1656
		private int _rowsCopied;

		// Token: 0x04000679 RID: 1657
		private int _notifyAfter;

		// Token: 0x0400067A RID: 1658
		private int _rowsUntilNotification;

		// Token: 0x0400067B RID: 1659
		private bool _insideRowsCopiedEvent;

		// Token: 0x0400067C RID: 1660
		private object _rowSource;

		// Token: 0x0400067D RID: 1661
		private SqlDataReader _sqlDataReaderRowSource;

		// Token: 0x0400067E RID: 1662
		private bool _rowSourceIsSqlDataReaderSmi;

		// Token: 0x0400067F RID: 1663
		private DbDataReader _dbDataReaderRowSource;

		// Token: 0x04000680 RID: 1664
		private DataTable _dataTableSource;

		// Token: 0x04000681 RID: 1665
		private SqlBulkCopyColumnMappingCollection _columnMappings;

		// Token: 0x04000682 RID: 1666
		private SqlBulkCopyColumnMappingCollection _localColumnMappings;

		// Token: 0x04000683 RID: 1667
		private SqlConnection _connection;

		// Token: 0x04000684 RID: 1668
		private SqlTransaction _internalTransaction;

		// Token: 0x04000685 RID: 1669
		private SqlTransaction _externalTransaction;

		// Token: 0x04000686 RID: 1670
		private SqlBulkCopy.ValueSourceType _rowSourceType;

		// Token: 0x04000687 RID: 1671
		private DataRow _currentRow;

		// Token: 0x04000688 RID: 1672
		private int _currentRowLength;

		// Token: 0x04000689 RID: 1673
		private DataRowState _rowStateToSkip;

		// Token: 0x0400068A RID: 1674
		private IEnumerator _rowEnumerator;

		// Token: 0x0400068B RID: 1675
		private TdsParser _parser;

		// Token: 0x0400068C RID: 1676
		private TdsParserStateObject _stateObj;

		// Token: 0x0400068D RID: 1677
		private List<_ColumnMapping> _sortedColumnMappings;

		// Token: 0x0400068E RID: 1678
		private static int _objectTypeCount;

		// Token: 0x0400068F RID: 1679
		internal readonly int _objectID = Interlocked.Increment(ref SqlBulkCopy._objectTypeCount);

		// Token: 0x04000690 RID: 1680
		private int _savedBatchSize;

		// Token: 0x04000691 RID: 1681
		private bool _hasMoreRowToCopy;

		// Token: 0x04000692 RID: 1682
		private bool _isAsyncBulkCopy;

		// Token: 0x04000693 RID: 1683
		private bool _isBulkCopyingInProgress;

		// Token: 0x04000694 RID: 1684
		private SqlInternalConnectionTds.SyncAsyncLock _parserLock;

		// Token: 0x04000695 RID: 1685
		private SqlBulkCopy.SourceColumnMetadata[] _currentRowMetadata;

		// Token: 0x02000205 RID: 517
		private enum TableNameComponents
		{
			// Token: 0x04001552 RID: 5458
			Server,
			// Token: 0x04001553 RID: 5459
			Catalog,
			// Token: 0x04001554 RID: 5460
			Owner,
			// Token: 0x04001555 RID: 5461
			TableName
		}

		// Token: 0x02000206 RID: 518
		private enum ValueSourceType
		{
			// Token: 0x04001557 RID: 5463
			Unspecified,
			// Token: 0x04001558 RID: 5464
			IDataReader,
			// Token: 0x04001559 RID: 5465
			DataTable,
			// Token: 0x0400155A RID: 5466
			RowArray,
			// Token: 0x0400155B RID: 5467
			DbDataReader
		}

		// Token: 0x02000207 RID: 519
		private enum ValueMethod : byte
		{
			// Token: 0x0400155D RID: 5469
			GetValue,
			// Token: 0x0400155E RID: 5470
			SqlTypeSqlDecimal,
			// Token: 0x0400155F RID: 5471
			SqlTypeSqlDouble,
			// Token: 0x04001560 RID: 5472
			SqlTypeSqlSingle,
			// Token: 0x04001561 RID: 5473
			DataFeedStream,
			// Token: 0x04001562 RID: 5474
			DataFeedText,
			// Token: 0x04001563 RID: 5475
			DataFeedXml
		}

		// Token: 0x02000208 RID: 520
		private readonly struct SourceColumnMetadata
		{
			// Token: 0x06001E09 RID: 7689 RVA: 0x0007BB12 File Offset: 0x00079D12
			public SourceColumnMetadata(SqlBulkCopy.ValueMethod method, bool isSqlType, bool isDataFeed)
			{
				this.Method = method;
				this.IsSqlType = isSqlType;
				this.IsDataFeed = isDataFeed;
			}

			// Token: 0x04001564 RID: 5476
			public readonly SqlBulkCopy.ValueMethod Method;

			// Token: 0x04001565 RID: 5477
			public readonly bool IsSqlType;

			// Token: 0x04001566 RID: 5478
			public readonly bool IsDataFeed;
		}
	}
}
