using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005A0 RID: 1440
	internal class OdbcFetchPlanFactory
	{
		// Token: 0x06002D8B RID: 11659 RVA: 0x0008A6F1 File Offset: 0x000888F1
		protected OdbcFetchPlanFactory(bool isBindColumnSupported)
		{
			this.isBindColumnSupported = isBindColumnSupported;
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x0008A700 File Offset: 0x00088900
		public virtual int MaxCellByteLength
		{
			get
			{
				return 1024;
			}
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void ColumnNotBoundHandler(OdbcPageReaderColumnInfo columnInfo, long maxDataLength)
		{
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x0008A707 File Offset: 0x00088907
		public virtual bool GetUseMultipleRowFetch(bool allowBind, OdbcStatementHandle statement)
		{
			return allowBind && !OdbcFetchPlanFactory.IsForwardOnlyCursor(statement);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x0008A717 File Offset: 0x00088917
		public virtual OdbcFetchPlan BuildFetchPlan(OdbcStatementHandle statement, int maxRowCount, bool allowBind, OdbcPageReaderColumnInfo[] columnInfos)
		{
			return new OdbcFetchPlan(columnInfos, this.GetUseMultipleRowFetch(allowBind, statement), maxRowCount, this.MaxCellByteLength);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0008A730 File Offset: 0x00088930
		public OdbcFetchPlan NewFetchPlan(OdbcConnectionHandle connection, OdbcStatementHandle statement, short columnCount, OdbcTypeMap[] typeMaps, TableSchema schema, int maxRowCount)
		{
			bool flag = connection.TryGetFunction(Odbc32.SQL_API.SQL_API_SQLFETCHSCROLL);
			Odbc32.SQL_GD dataExtensions = OdbcFetchPlanFactory.GetDataExtensions(connection);
			bool flag2 = this.isBindColumnSupported && (statement.IsV3Driver || flag);
			int num = 0;
			int num2 = 0;
			OdbcPageReaderColumnInfo[] array = new OdbcPageReaderColumnInfo[(int)columnCount];
			for (int i = 0; i < (int)columnCount; i++)
			{
				SchemaColumn column = schema.GetColumn(i);
				Odbc32.SQL_TYPE sql_TYPE = (Odbc32.SQL_TYPE)column.ProviderType.Value;
				OdbcTypeMap odbcTypeMap = typeMaps[i];
				bool flag3 = OdbcTypeMap.IsKnownType(sql_TYPE);
				long num3 = (long)odbcTypeMap.BufferSize;
				if (num3 < 0L && flag3)
				{
					num3 = statement.GetColumnAttribute(i + 1, Odbc32.SQL_DESC.OCTET_LENGTH, Odbc32.SQL_COLUMN.LENGTH);
					if (num3 <= 0L)
					{
						num3 = long.MaxValue;
					}
					Odbc32.SQL_TYPE sqlType = odbcTypeMap.SqlType;
					if (sqlType <= Odbc32.SQL_TYPE.LONGVARCHAR)
					{
						if (sqlType != Odbc32.SQL_TYPE.SS_VARIANT)
						{
							if (sqlType == Odbc32.SQL_TYPE.LONGVARCHAR)
							{
								goto IL_00CA;
							}
						}
						else
						{
							num3 = long.MaxValue;
						}
					}
					else if (sqlType == Odbc32.SQL_TYPE.CHAR || sqlType == Odbc32.SQL_TYPE.VARCHAR)
					{
						goto IL_00CA;
					}
					IL_00E9:
					if (odbcTypeMap.CType == Odbc32.SQL_C.WCHAR && 9223372036854775805L >= num3)
					{
						num3 += 2L;
						goto IL_0108;
					}
					goto IL_0108;
					IL_00CA:
					if (num3 < (long)this.MaxCellByteLength)
					{
						num3 *= 2L;
						goto IL_00E9;
					}
					goto IL_00E9;
				}
				IL_0108:
				int num4 = -1;
				int num5 = -1;
				int num6 = -1;
				int num7 = -1;
				bool flag4 = num3 <= (long)this.MaxCellByteLength;
				if (this.isBindColumnSupported && (flag2 || (dataExtensions & Odbc32.SQL_GD.ANY_COLUMN) != Odbc32.SQL_GD.None) && (flag4 || (dataExtensions & Odbc32.SQL_GD.BOUND) > Odbc32.SQL_GD.None) && flag3)
				{
					num4 = num2++;
					num6 = (int)Math.Min(num3, (long)this.MaxCellByteLength);
					num5 = num6 * maxRowCount;
					num7 = num;
					num += num5;
					if (!flag4)
					{
						flag2 = false;
					}
				}
				else
				{
					flag2 = false;
				}
				array[i] = new OdbcPageReaderColumnInfo(column.Name, odbcTypeMap, num7, num6, num5, num4);
				if (this.isBindColumnSupported && !array[i].IsColumnBound)
				{
					this.ColumnNotBoundHandler(array[i], num3);
				}
			}
			return this.BuildFetchPlan(statement, maxRowCount, flag2, array);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0008A910 File Offset: 0x00088B10
		private static bool IsForwardOnlyCursor(OdbcStatementHandle stmt)
		{
			IntPtr intPtr;
			int num;
			return OdbcUtils.IsSuccess(stmt.GetStatementAttribute(Odbc32.SQL_ATTR.CURSOR_TYPE, out intPtr, out num)) && intPtr == Odbc32.SQL_CURSOR_FORWARD_ONLY;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x0008A93C File Offset: 0x00088B3C
		private static Odbc32.SQL_GD GetDataExtensions(OdbcConnectionHandle connection)
		{
			int num;
			if (connection.GetInfoInt32Unhandled(Odbc32.SQL_INFO.SQL_GETDATA_EXTENSIONS, out num) == Odbc32.RetCode.SUCCESS)
			{
				return (Odbc32.SQL_GD)num;
			}
			return Odbc32.SQL_GD.None;
		}

		// Token: 0x040013D5 RID: 5077
		public static readonly OdbcFetchPlanFactory BindColumnSupportedInstance = new OdbcFetchPlanFactory(true);

		// Token: 0x040013D6 RID: 5078
		public static readonly OdbcFetchPlanFactory BindColumnNotSupportedInstance = new OdbcFetchPlanFactory(false);

		// Token: 0x040013D7 RID: 5079
		private const int DefaultMaxCellByteLength = 1024;

		// Token: 0x040013D8 RID: 5080
		private readonly bool isBindColumnSupported;
	}
}
