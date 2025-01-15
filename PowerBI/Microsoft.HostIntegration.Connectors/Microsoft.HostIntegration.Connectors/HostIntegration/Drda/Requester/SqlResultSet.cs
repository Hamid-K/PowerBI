using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000904 RID: 2308
	internal class SqlResultSet : IResultSet
	{
		// Token: 0x060048E9 RID: 18665 RVA: 0x0010BAB0 File Offset: 0x00109CB0
		internal SqlResultSet(SqlStatement sqlStatement)
		{
			this._sqlStatement = sqlStatement;
			this._tracePoint = sqlStatement.TracePoint;
			this._requester = (Requester)sqlStatement.Requester;
			this.EndOfQuery = false;
			this.Pkgnamcsn = null;
			this.State = SqlStatement.SqlState.Initialized;
			this.QueryInstanceId = -1L;
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x0010BB16 File Offset: 0x00109D16
		// (set) Token: 0x060048EB RID: 18667 RVA: 0x0010BB1E File Offset: 0x00109D1E
		internal PKGNAMCSN Pkgnamcsn { get; set; }

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x0010BB27 File Offset: 0x00109D27
		internal SqlStatement SqlStatement
		{
			get
			{
				return this._sqlStatement;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x0010BB2F File Offset: 0x00109D2F
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x0010BB37 File Offset: 0x00109D37
		internal SqlStatement.SqlState State { get; set; }

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x0010BB40 File Offset: 0x00109D40
		// (set) Token: 0x060048F0 RID: 18672 RVA: 0x0010BB48 File Offset: 0x00109D48
		internal long QueryInstanceId { get; set; }

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x0010BB51 File Offset: 0x00109D51
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x0010BB59 File Offset: 0x00109D59
		internal byte[] LeftoverData { get; set; }

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x0010BB62 File Offset: 0x00109D62
		public IColumnInfo[] ColumnInfos
		{
			get
			{
				if (this._columnInfos == null && this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "ColumnInfos not available.");
				}
				return this._columnInfos;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x0010BB91 File Offset: 0x00109D91
		// (set) Token: 0x060048F5 RID: 18677 RVA: 0x0010BB99 File Offset: 0x00109D99
		public bool IsCursorScrollable { get; internal set; }

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0010BBA2 File Offset: 0x00109DA2
		// (set) Token: 0x060048F7 RID: 18679 RVA: 0x0010BBAA File Offset: 0x00109DAA
		public bool EndOfQuery { get; set; }

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0010BBB3 File Offset: 0x00109DB3
		public int CurrentRowIndex
		{
			get
			{
				return this._currentRowIndex;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x0010BBBB File Offset: 0x00109DBB
		public int RowsCount
		{
			get
			{
				return this._rows.Count;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0010BBC8 File Offset: 0x00109DC8
		// (set) Token: 0x060048FB RID: 18683 RVA: 0x0010BBD0 File Offset: 0x00109DD0
		public bool PartialRow
		{
			get
			{
				return this._partialRow;
			}
			set
			{
				this._partialRow = value;
			}
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0010BBDC File Offset: 0x00109DDC
		public async Task<bool> ReadRowAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.Enter(isAsync, cancellationToken);
			try
			{
				if (orientation == QueryScrollOrientation.Next)
				{
					this._currentRowIndex++;
					if (this._currentRowIndex < this._rows.Count)
					{
						return true;
					}
					if (this.EndOfQuery)
					{
						return false;
					}
				}
				this._rows.Clear();
				this._currentRowIndex = -1;
				this._currentLobRowIndex = 0;
				await this._requester.SqlManager.SubmitCntqry(this._sqlStatement, this, orientation, number, isAsync, cancellationToken);
				while (this.PartialRow)
				{
					await this._requester.SqlManager.SubmitCntqry(this._sqlStatement, this, orientation, number, isAsync, cancellationToken);
				}
				if (this._sqlStatement.State == SqlStatement.SqlState.OPNQRY)
				{
					this._sqlStatement.State = SqlStatement.SqlState.CNTQRY;
				}
				if (this.State == SqlStatement.SqlState.OPNQRY)
				{
					this.State = SqlStatement.SqlState.CNTQRY;
				}
				if (this._rows.Count == 0)
				{
					if (orientation == QueryScrollOrientation.Next)
					{
						return false;
					}
				}
				else
				{
					this._currentRowIndex = 0;
				}
			}
			finally
			{
				this._requester.Leave();
			}
			return true;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x0010BC44 File Offset: 0x00109E44
		public async Task<object> GetColumnData(int columnOrdinal, bool isAsync, CancellationToken cancellationToken)
		{
			object obj2;
			if (this._currentRowIndex < this._rows.Count && this._currentRowIndex >= 0)
			{
				object[] row = this._rows[this._currentRowIndex];
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Retrieving Column data for index " + columnOrdinal.ToString());
				}
				object obj = row[columnOrdinal];
				if (obj is SqlData && this._requester.Mode[this._largeLobCounter] == 3)
				{
					await this._requester.SqlManager.ReadLargeLobData(columnOrdinal, this._currentRowIndex, this._largeLobCounter, false, this.SqlStatement, isAsync, cancellationToken);
					this._largeLobCounter++;
					obj = row[columnOrdinal];
				}
				else if (this.ColumnInfos[columnOrdinal].IsLob)
				{
					this._largeLobCounter++;
				}
				if (this._requester.IsConvertToBigEndian && this._requester.RdbName != this._requester.RetDatabaseName)
				{
					if (obj is short)
					{
						new byte[2];
						byte[] bytes = BitConverter.GetBytes((short)obj);
						Array.Reverse(bytes);
						obj = BitConverter.ToInt16(bytes, 0);
					}
					else if (obj is int)
					{
						new byte[4];
						byte[] bytes2 = BitConverter.GetBytes((int)obj);
						Array.Reverse(bytes2);
						obj = BitConverter.ToInt32(bytes2, 0);
					}
					else if (obj is long)
					{
						new byte[8];
						byte[] bytes3 = BitConverter.GetBytes((long)obj);
						Array.Reverse(bytes3);
						obj = BitConverter.ToInt64(bytes3, 0);
					}
				}
				if (obj is byte[] && this._requester.SqlManager.BinaryCcsid != null)
				{
					byte[] array = (byte[])obj;
					string text = null;
					DrdaClientType drdaType = this._columnInfos[columnOrdinal].DrdaType;
					if (drdaType > DrdaClientType.VarChar)
					{
						if (drdaType - DrdaClientType.Graphic > 1)
						{
							switch (drdaType)
							{
							case DrdaClientType.LongVarChar:
							case DrdaClientType.RowId:
								goto IL_02B9;
							case DrdaClientType.LongVarCharForBit:
								goto IL_032D;
							case DrdaClientType.LongVarGraphic:
							case DrdaClientType.NChar:
							case DrdaClientType.NVarChar:
								break;
							default:
								goto IL_032D;
							}
						}
						this._requester.ConnectionManager.DdmReader.Converter.UnpackString(array, 0, array.Length, this._requester.SqlManager.BinaryCcsid._ccsiddbc, ref text, false);
						goto IL_032D;
					}
					if (drdaType != DrdaClientType.Char && drdaType != DrdaClientType.VarChar)
					{
						goto IL_032D;
					}
					IL_02B9:
					this._requester.ConnectionManager.DdmReader.Converter.UnpackString(array, 0, array.Length, this._requester.SqlManager.BinaryCcsid._ccsidsbc, ref text, false);
					IL_032D:
					if (text != null)
					{
						obj = text;
						if (this._tracePoint.IsEnabled(TraceFlags.Debug))
						{
							this._tracePoint.Trace(TraceFlags.Debug, "Converted byte[] to string for index " + columnOrdinal.ToString());
						}
					}
				}
				obj2 = obj;
			}
			else
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "Current row not available: current Index = " + this._currentRowIndex.ToString());
				}
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0010BCA4 File Offset: 0x00109EA4
		internal async Task ReadLobDataAsync(bool isAsync, CancellationToken cancellationToken)
		{
			while (this._currentLobRowIndex < this._rows.Count)
			{
				object[] row = this._rows[this._currentLobRowIndex];
				for (int i = 0; i < row.Length; i++)
				{
					if (row[i] is SqlData)
					{
						SqlData sqlData = (SqlData)row[i];
						if (!sqlData.IsDataReady)
						{
							if (sqlData.DataLengthInExtdta != 0 && sqlData.LobMode == 2)
							{
								if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
								{
									this._tracePoint.Trace(TraceFlags.Verbose, "Retrieving Lob data for index " + i.ToString() + " at row " + this._currentLobRowIndex.ToString());
								}
								SqlColumnInfo sqlColumnInfo = (SqlColumnInfo)this._columnInfos[i];
								EXTDTA extdta = await this._requester.SqlManager.ReadExtdta(sqlColumnInfo.SqlType, sqlColumnInfo.Ccsid, sqlData.DataLengthInExtdta, isAsync, cancellationToken);
								sqlData.Value = extdta.Value;
								sqlData.IsDataReady = true;
								row[i] = sqlData.Value;
								if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
								{
									this._tracePoint.Trace(TraceFlags.Verbose, "Successfully read Lob data for index " + i.ToString() + " at row " + this._currentLobRowIndex.ToString());
								}
								if (i == row.Length - 1)
								{
									this._currentLobRowIndex++;
								}
								return;
							}
							sqlData = null;
						}
					}
				}
				this._currentLobRowIndex++;
				row = null;
			}
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0010BCFC File Offset: 0x00109EFC
		internal async Task ReadLargeLobDataAsync(int row, int column, bool decCounter, bool isModeThree, bool isAsync, CancellationToken cancellationToken)
		{
			object obj = this._rows[row][column];
			if (obj is SqlData)
			{
				SqlData sqlData = (SqlData)obj;
				if (sqlData.DataLengthInExtdta != 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "Retrieving Lob data for index " + column.ToString() + " at row " + row.ToString());
					}
					SqlColumnInfo sqlColumnInfo = (SqlColumnInfo)this._columnInfos[column];
					EXTDTA extdta = await this._requester.SqlManager.ReadExtdta(sqlColumnInfo.SqlType, sqlColumnInfo.Ccsid, sqlData.DataLengthInExtdta, isAsync, cancellationToken);
					sqlData.Value = extdta.Value;
					sqlData.IsDataReady = true;
					obj = sqlData.Value;
					this._rows[row][column] = sqlData.Value;
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "Successfully read Lob data for index " + column.ToString() + " at row " + row.ToString());
					}
				}
				else
				{
					sqlData = null;
				}
			}
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0010BD64 File Offset: 0x00109F64
		internal async Task<int> SetDataRow(bool isAsync, CancellationToken cancellationToken)
		{
			int readCount = 0;
			object[] row = new object[this._columnInfos.Length];
			for (int i = 0; i < this._columnInfos.Length; i++)
			{
				SqlColumnInfo columnInfo = (SqlColumnInfo)this._columnInfos[i];
				SqlData sqlData = await SqlData.ConvertFromDrdaType(columnInfo, this._requester, isAsync, cancellationToken);
				if (sqlData.IsDataReady)
				{
					row[i] = sqlData.Value;
				}
				else
				{
					row[i] = sqlData;
				}
				readCount += sqlData.Length;
				if (this._requester.HostType != HostType.DB2 && this._requester.HostType != HostType.MVS && columnInfo.IsLob && sqlData.Value == "")
				{
					readCount--;
				}
				columnInfo = null;
			}
			this._rows.Add(row);
			return readCount;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x0010BDBC File Offset: 0x00109FBC
		internal void SetColumnDrdaInfo(int columnCount, int columnIndex, byte drdaType, ushort sqlLength, ushort ccsid)
		{
			if (this._columnInfos == null && this._sqlStatement.StatementType == Parser.StatementType.Static && columnCount > 0)
			{
				this._columnInfos = new IColumnInfo[columnCount];
				for (int i = 0; i < columnCount; i++)
				{
					SqlColumnInfo sqlColumnInfo = new SqlColumnInfo();
					sqlColumnInfo.ColumnName = null;
					this._columnInfos[i] = sqlColumnInfo;
				}
			}
			if (this._columnInfos == null || this._columnInfos.Length <= columnIndex)
			{
				throw this._requester.MakeException(RequesterResource.ColumnInfoNoSet(columnIndex), "HY000", -343);
			}
			SqlColumnInfo columnInfo = (SqlColumnInfo)this._columnInfos[columnIndex];
			columnInfo.DrdaServerType = drdaType;
			columnInfo.DataLength = (int)sqlLength;
			if (ccsid > 0)
			{
				columnInfo.Ccsid = Utility.MapCcsidCodeToCodePage(ccsid);
			}
			if (this._sqlStatement.StatementType == Parser.StatementType.Static)
			{
				if (ccsid == 0 && columnInfo.Ccsid == 0)
				{
					columnInfo.Ccsid = (ushort)this._requester.ConnectionManager.DdmReader.Ccsid._ccsidsbc;
				}
				columnInfo.SqlType = SqlType.DrdaTypeToSqlTypeMappings[(int)drdaType];
				columnInfo.ColumnName = "COL" + columnIndex.ToString();
				columnInfo.IsNullable = drdaType % 2 > 0;
				columnInfo.Length = (int)sqlLength;
			}
			if (columnInfo.SqlType > 0)
			{
				SqlType.ProcessSqlType(columnInfo.SqlType, columnInfo.DataLength, columnInfo.Ccsid, drdaType, this._requester, delegate(DrdaClientType drdaClientType, short scale, short precision, int size, DbType dbType, byte drdaServerType, int dataLength)
				{
					columnInfo.DrdaType = drdaClientType;
					if (scale > 0)
					{
						columnInfo.Scale = (short)((byte)scale);
					}
					if (precision > 0)
					{
						columnInfo.Precision = (short)((byte)precision);
					}
					if (size > 0)
					{
						columnInfo.Length = size;
					}
					if (dataLength > 0)
					{
						columnInfo.DataLength = dataLength;
					}
				});
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Update column: " + (string.IsNullOrEmpty(columnInfo.ColumnName) ? columnIndex.ToString() : columnInfo.ColumnName));
				return;
			}
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0010BFBC File Offset: 0x0010A1BC
		internal void SetColumnInfos(SQLDARD sqldard)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Creating Column Info Array, count = " + sqldard.SqlNum.ToString());
			}
			this._columnInfos = null;
			if (sqldard.SqlNum == 0)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "No column in the SQLDARD!");
				}
				return;
			}
			SortedSet<string> sortedSet = new SortedSet<string>();
			this._columnInfos = new IColumnInfo[(int)sqldard.SqlNum];
			for (int i = 0; i < (int)sqldard.SqlNum; i++)
			{
				SQLDAGRP sqldagrp = sqldard.ListSqldagrp[i];
				SqlColumnInfo columnInfo = new SqlColumnInfo();
				if (sqldagrp.SqldOptGrp != null && !string.IsNullOrWhiteSpace(sqldagrp.SqldOptGrp.SqlName))
				{
					columnInfo.ColumnName = sqldagrp.SqldOptGrp.SqlName;
					sortedSet.Add(columnInfo.ColumnName.ToUpperInvariant());
				}
				else
				{
					columnInfo.ColumnName = null;
				}
				columnInfo.Scale = sqldagrp.SqlScale;
				columnInfo.Length = (int)sqldagrp.SqlLength;
				columnInfo.Precision = sqldagrp.SqlPrecision;
				columnInfo.Ccsid = Utility.MapCcsidCodeToCodePage((ushort)sqldagrp.SqlCcsid);
				columnInfo.SqlType = sqldagrp.SqlType;
				columnInfo.IsNullable = SqlType.IsNullable(columnInfo.SqlType);
				columnInfo.IsLob = SqlType.IsLob(columnInfo.SqlType);
				columnInfo.DataLength = columnInfo.Length;
				if (sqldagrp.SqldOptGrp != null && sqldagrp.SqldOptGrp.SqlDxGrp != null)
				{
					columnInfo.GeneratedIdType = sqldagrp.SqldOptGrp.SqlDxGrp.SqlxGenerated;
					columnInfo.BaseTable = sqldagrp.SqldOptGrp.SqlDxGrp.SqlxBaseName;
					columnInfo.Schema = sqldagrp.SqldOptGrp.SqlDxGrp.SqlxSchema;
					columnInfo.Catalog = sqldagrp.SqldOptGrp.SqlDxGrp.SqlxRdbnam;
					if (columnInfo.Catalog != null)
					{
						columnInfo.Catalog = columnInfo.Catalog.Trim();
					}
					columnInfo.IsKey = sqldagrp.SqldOptGrp.SqlDxGrp.SqlxKeyMem != 0;
				}
				if (columnInfo.SqlType > 0)
				{
					SqlType.ProcessSqlType(columnInfo.SqlType, columnInfo.Length, columnInfo.Ccsid, 0, this._requester, delegate(DrdaClientType drdaClientType, short scale, short precision, int size, DbType dbType, byte drdaServerType, int dataLength)
					{
						if (scale > 0)
						{
							columnInfo.Scale = (short)((byte)scale);
						}
						if (precision > 0)
						{
							columnInfo.Precision = (short)((byte)precision);
						}
						if (size > 0)
						{
							columnInfo.Length = size;
						}
						columnInfo.DrdaType = drdaClientType;
					});
				}
				this._columnInfos[i] = columnInfo;
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Added column info: " + columnInfo.ColumnName);
				}
			}
			for (int j = 0; j < this._columnInfos.Length; j++)
			{
				SqlColumnInfo sqlColumnInfo = (SqlColumnInfo)this._columnInfos[j];
				if (sqlColumnInfo.ColumnName == null)
				{
					string text = "COL" + (j + 1).ToString();
					string text2 = text + "_";
					int num = 1;
					while (sortedSet.Contains(text))
					{
						text = text2 + num.ToString();
						num++;
					}
					sqlColumnInfo.ColumnName = text;
				}
			}
		}

		// Token: 0x040035F1 RID: 13809
		private SqlStatement _sqlStatement;

		// Token: 0x040035F2 RID: 13810
		private Requester _requester;

		// Token: 0x040035F3 RID: 13811
		private IColumnInfo[] _columnInfos;

		// Token: 0x040035F4 RID: 13812
		private DrdaArTracePoint _tracePoint;

		// Token: 0x040035F5 RID: 13813
		private int _largeLobCounter;

		// Token: 0x040035F6 RID: 13814
		private bool _partialRow;

		// Token: 0x040035F7 RID: 13815
		private List<object[]> _rows = new List<object[]>();

		// Token: 0x040035F8 RID: 13816
		private int _currentRowIndex = -1;

		// Token: 0x040035F9 RID: 13817
		private int _currentLobRowIndex;
	}
}
