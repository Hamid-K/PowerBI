using System;
using System.Data;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007BF RID: 1983
	public class DataTypeMapping
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x000D33C0 File Offset: 0x000D15C0
		static DataTypeMapping()
		{
			DataTypeMapping.drdaTodb2Mappings[32] = SQLType.Date;
			DataTypeMapping.drdaTodb2Mappings[33] = SQLType.NullableDate;
			DataTypeMapping.drdaTodb2Mappings[34] = SQLType.Time;
			DataTypeMapping.drdaTodb2Mappings[35] = SQLType.NullableTime;
			DataTypeMapping.drdaTodb2Mappings[36] = SQLType.Timestamp;
			DataTypeMapping.drdaTodb2Mappings[37] = SQLType.NullableTimestamp;
			DataTypeMapping.drdaTodb2Mappings[40] = SQLType.VarChar;
			DataTypeMapping.drdaTodb2Mappings[41] = SQLType.NullableVarChar;
			DataTypeMapping.drdaTodb2Mappings[50] = SQLType.VarChar;
			DataTypeMapping.drdaTodb2Mappings[51] = SQLType.NullableVarChar;
			DataTypeMapping.drdaTodb2Mappings[62] = SQLType.VarChar;
			DataTypeMapping.drdaTodb2Mappings[63] = SQLType.NullableVarChar;
			DataTypeMapping.drdaTodb2Mappings[38] = SQLType.Char;
			DataTypeMapping.drdaTodb2Mappings[39] = SQLType.NullableChar;
			DataTypeMapping.drdaTodb2Mappings[48] = SQLType.Char;
			DataTypeMapping.drdaTodb2Mappings[49] = SQLType.NullableChar;
			DataTypeMapping.drdaTodb2Mappings[60] = SQLType.Char;
			DataTypeMapping.drdaTodb2Mappings[61] = SQLType.NullableChar;
			DataTypeMapping.drdaTodb2Mappings[42] = SQLType.VarBinaryString;
			DataTypeMapping.drdaTodb2Mappings[43] = SQLType.NullableVarBinaryString;
			DataTypeMapping.drdaTodb2Mappings[52] = SQLType.LongVarChar;
			DataTypeMapping.drdaTodb2Mappings[53] = SQLType.NullableLongVarChar;
			DataTypeMapping.drdaTodb2Mappings[64] = SQLType.LongVarChar;
			DataTypeMapping.drdaTodb2Mappings[65] = SQLType.NullableLongVarChar;
			DataTypeMapping.drdaTodb2Mappings[44] = SQLType.NullTerminatedChar;
			DataTypeMapping.drdaTodb2Mappings[45] = SQLType.NullableNullTerminatedChar;
			DataTypeMapping.drdaTodb2Mappings[46] = SQLType.NullTerminatedChar;
			DataTypeMapping.drdaTodb2Mappings[47] = SQLType.NullableNullTerminatedChar;
			DataTypeMapping.drdaTodb2Mappings[66] = SQLType.NullTerminatedChar;
			DataTypeMapping.drdaTodb2Mappings[67] = SQLType.NullableNullTerminatedChar;
			DataTypeMapping.drdaTodb2Mappings[56] = SQLType.VarGraphic;
			DataTypeMapping.drdaTodb2Mappings[57] = SQLType.NullableVarGraphic;
			DataTypeMapping.drdaTodb2Mappings[54] = SQLType.Graphic;
			DataTypeMapping.drdaTodb2Mappings[55] = SQLType.NullableGraphic;
			DataTypeMapping.drdaTodb2Mappings[58] = SQLType.LongVarGraphic;
			DataTypeMapping.drdaTodb2Mappings[59] = SQLType.NullableLongVarGraphic;
			DataTypeMapping.drdaTodb2Mappings[8] = SQLType.Float;
			DataTypeMapping.drdaTodb2Mappings[9] = SQLType.NullableFloat;
			DataTypeMapping.drdaTodb2Mappings[10] = SQLType.Float;
			DataTypeMapping.drdaTodb2Mappings[11] = SQLType.NullableFloat;
			DataTypeMapping.drdaTodb2Mappings[12] = SQLType.Float;
			DataTypeMapping.drdaTodb2Mappings[13] = SQLType.NullableFloat;
			DataTypeMapping.drdaTodb2Mappings[14] = SQLType.Decimal;
			DataTypeMapping.drdaTodb2Mappings[15] = SQLType.NullableDecimal;
			DataTypeMapping.drdaTodb2Mappings[16] = SQLType.ZonedDecimal;
			DataTypeMapping.drdaTodb2Mappings[17] = SQLType.NullableZonedDecimal;
			DataTypeMapping.drdaTodb2Mappings[22] = SQLType.BigInt;
			DataTypeMapping.drdaTodb2Mappings[23] = SQLType.NullableBigInt;
			DataTypeMapping.drdaTodb2Mappings[2] = SQLType.Int;
			DataTypeMapping.drdaTodb2Mappings[3] = SQLType.NullableInt;
			DataTypeMapping.drdaTodb2Mappings[4] = SQLType.SmallInt;
			DataTypeMapping.drdaTodb2Mappings[5] = SQLType.NullableSmallInt;
			DataTypeMapping.drdaTodb2Mappings[18] = SQLType.Numeric;
			DataTypeMapping.drdaTodb2Mappings[19] = SQLType.NullableNumeric;
			DataTypeMapping.drdaTodb2Mappings[6] = SQLType.TinyInt;
			DataTypeMapping.drdaTodb2Mappings[7] = SQLType.TinyInt;
			DataTypeMapping.drdaTodb2Mappings[68] = SQLType.Char;
			DataTypeMapping.drdaTodb2Mappings[69] = SQLType.NullableChar;
			DataTypeMapping.drdaTodb2Mappings[70] = SQLType.Char;
			DataTypeMapping.drdaTodb2Mappings[71] = SQLType.NullableChar;
			DataTypeMapping.drdaTodb2Mappings[72] = SQLType.Char;
			DataTypeMapping.drdaTodb2Mappings[73] = SQLType.NullableChar;
			DataTypeMapping.drdaTodb2Mappings[200] = SQLType.BLOB;
			DataTypeMapping.drdaTodb2Mappings[201] = SQLType.NullableBLOB;
			DataTypeMapping.drdaTodb2Mappings[202] = SQLType.CLOB;
			DataTypeMapping.drdaTodb2Mappings[203] = SQLType.NullableCLOB;
			DataTypeMapping.drdaTodb2Mappings[204] = SQLType.CLOB_DBCS;
			DataTypeMapping.drdaTodb2Mappings[205] = SQLType.NullableCLOB_DBCS;
			DataTypeMapping.drdaTodb2Mappings[206] = SQLType.CLOB;
			DataTypeMapping.drdaTodb2Mappings[207] = SQLType.NullableCLOB;
			DataTypeMapping.drdaTodb2Mappings[194] = SQLType.VarBinaryString;
			DataTypeMapping.drdaTodb2Mappings[195] = SQLType.NullableBinaryString;
			DataTypeMapping.drdaTodb2Mappings[192] = SQLType.BinaryString;
			DataTypeMapping.drdaTodb2Mappings[193] = SQLType.NullableBinaryString;
		}

		// Token: 0x17000EEB RID: 3819
		// (set) Token: 0x06003F03 RID: 16131 RVA: 0x000D37B5 File Offset: 0x000D19B5
		public static DrdaType[] MssqlTodrdaMappings
		{
			set
			{
				DataTypeMapping.mssqlTodrdaMappings = value;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (set) Token: 0x06003F04 RID: 16132 RVA: 0x000D37BD File Offset: 0x000D19BD
		public static SqlDbType[] DrdaToMSSqlMappings
		{
			set
			{
				DataTypeMapping.drdaToMSSqlMappings = value;
			}
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x000D37C5 File Offset: 0x000D19C5
		public static DrdaType GetDrdaType(IColumnSchema columnSchema)
		{
			return DataTypeMapping.GetDrdaType(columnSchema.SqlDbType, columnSchema.AllowDBNull);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x000D37D8 File Offset: 0x000D19D8
		public static DrdaType GetDrdaType(SqlDbType dbType, bool nullable)
		{
			DrdaType drdaType = DataTypeMapping.GetDrdaType(dbType);
			if (nullable)
			{
				return drdaType + 1;
			}
			return drdaType;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000D37F4 File Offset: 0x000D19F4
		public static DrdaType GetDrdaType(SqlDbType target)
		{
			DrdaType drdaType = DataTypeMapping.mssqlTodrdaMappings[(int)target];
			if (drdaType != DrdaType.NONE)
			{
				return drdaType;
			}
			throw new NotSupportedException("In DataTypeMapping.GetDrdaType(), SqlDbType not supported: SqlDbType =" + target.ToString());
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x000D382C File Offset: 0x000D1A2C
		public static SQLType GetSQLType(DrdaType drdaType)
		{
			SQLType sqltype = DataTypeMapping.drdaTodb2Mappings[(int)drdaType];
			if (sqltype != SQLType.None)
			{
				return sqltype;
			}
			throw new NotSupportedException("In DataTypeMapping.GetSQLType(), DrdaType not supported: DrdaType =" + drdaType.ToString());
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000D3862 File Offset: 0x000D1A62
		public static SQLType GetSQLType(IColumnSchema columnSchema)
		{
			return DataTypeMapping.GetSQLType(columnSchema.SqlDbType, columnSchema.AllowDBNull);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000D3878 File Offset: 0x000D1A78
		public static SQLType GetSQLType(SqlDbType sqldbType, bool nullable)
		{
			SQLType sqltype = DataTypeMapping.GetSQLType(sqldbType);
			if (nullable)
			{
				return sqltype + 1;
			}
			return sqltype;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x000D3894 File Offset: 0x000D1A94
		public static SQLType GetSQLType(SqlDbType sqldbType)
		{
			return DataTypeMapping.GetSQLType(DataTypeMapping.GetDrdaType(sqldbType));
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000D38A4 File Offset: 0x000D1AA4
		public static SqlDbType GetSqlDbType(int drdaType)
		{
			if (drdaType % 2 != 0)
			{
				drdaType--;
			}
			SqlDbType sqlDbType = DataTypeMapping.drdaToMSSqlMappings[drdaType];
			if (drdaType == 22)
			{
				return sqlDbType;
			}
			if (sqlDbType != SqlDbType.BigInt)
			{
				return sqlDbType;
			}
			throw new NotSupportedException("In DataTypeMapping.GetSqlDbType(), DrdaType not supported: DrdaType =" + drdaType.ToString());
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000D38E8 File Offset: 0x000D1AE8
		public static int GetPrecision(SQLType sqlType, int basePrecision)
		{
			if (sqlType <= SQLType.NullableTimestamp)
			{
				if (sqlType != SQLType.TinyInt && sqlType - SQLType.Timestamp > 1)
				{
					goto IL_005B;
				}
			}
			else if (sqlType - SQLType.Float > 1)
			{
				switch (sqlType)
				{
				case SQLType.BigInt:
				case SQLType.NullableBigInt:
				case SQLType.Int:
				case SQLType.NullableInt:
				case SQLType.SmallInt:
				case SQLType.NullableSmallInt:
					break;
				case (SQLType)494:
				case (SQLType)495:
				case (SQLType)498:
				case (SQLType)499:
					goto IL_005B;
				default:
					goto IL_005B;
				}
			}
			return 0;
			IL_005B:
			if (basePrecision == 255)
			{
				return 0;
			}
			return basePrecision;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000D395C File Offset: 0x000D1B5C
		public static int GetScale(SQLType sqlType, int baseScale)
		{
			if (sqlType <= SQLType.NullableTimestamp)
			{
				if (sqlType != SQLType.TinyInt && sqlType - SQLType.Timestamp > 1)
				{
					goto IL_005B;
				}
			}
			else if (sqlType - SQLType.Float > 1)
			{
				switch (sqlType)
				{
				case SQLType.BigInt:
				case SQLType.NullableBigInt:
				case SQLType.Int:
				case SQLType.NullableInt:
				case SQLType.SmallInt:
				case SQLType.NullableSmallInt:
					break;
				case (SQLType)494:
				case (SQLType)495:
				case (SQLType)498:
				case (SQLType)499:
					goto IL_005B;
				default:
					goto IL_005B;
				}
			}
			return 0;
			IL_005B:
			if (baseScale == 255)
			{
				return 0;
			}
			return baseScale;
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000D39CF File Offset: 0x000D1BCF
		public static int GetPrecision(SQLType sqlType, int fieldLength, int basePrecision)
		{
			if (sqlType - SQLType.Decimal <= 1 || sqlType - SQLType.ZonedDecimal <= 1)
			{
				return (fieldLength & 65280) >> 8;
			}
			return basePrecision;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000D39F0 File Offset: 0x000D1BF0
		public static int GetScale(SQLType sqlType, int fieldLength, int baseScale)
		{
			if (sqlType - SQLType.Decimal <= 1 || sqlType - SQLType.ZonedDecimal <= 1)
			{
				return fieldLength & 255;
			}
			return baseScale;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000D3A0F File Offset: 0x000D1C0F
		public static int GetLength(SQLType sqlType, int fieldLength, int baseLength)
		{
			if (sqlType - SQLType.Decimal <= 1 || sqlType - SQLType.ZonedDecimal <= 1)
			{
				return DataTypeMapping.GetPrecision(sqlType, fieldLength, 0) + 3;
			}
			return baseLength;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000D3A34 File Offset: 0x000D1C34
		public static int GetLength(DrdaType drdaType, int baseLength)
		{
			switch (drdaType)
			{
			case DrdaType.INT:
			case DrdaType.NINT:
				return 4;
			case DrdaType.SMALLINT:
			case DrdaType.NSMALLINT:
				return 2;
			case DrdaType.INT1:
			case DrdaType.NINT1:
				return 1;
			case DrdaType.FLOAT16:
			case DrdaType.NFLOAT16:
				return 16;
			case DrdaType.DOUBLE:
			case DrdaType.NDOUBLE:
				return 8;
			case DrdaType.FLOAT:
			case DrdaType.NFLOAT:
				return 4;
			case DrdaType.INT8:
			case DrdaType.NINT8:
				return 8;
			case DrdaType.DATE:
			case DrdaType.NDATE:
				return 10;
			case DrdaType.TIME:
			case DrdaType.NTIME:
				return 8;
			case DrdaType.TIMESTAMP:
			case DrdaType.NTIMESTAMP:
				return 26;
			case DrdaType.VARBYTE:
			case DrdaType.NVARBYTE:
			case DrdaType.VARCHAR:
			case DrdaType.NVARCHAR:
			case DrdaType.LONGVARCHAR:
			case DrdaType.NLONGVARCHARSBCS:
			case DrdaType.NATIONALCHARACTERVARYING:
			case DrdaType.NVARGRAPHIC:
			case DrdaType.LONGVARCHARDBCS:
			case DrdaType.NLONGVARCHARDBCS:
				if (baseLength > 8000)
				{
					return 32704;
				}
				return baseLength;
			}
			return baseLength;
		}

		// Token: 0x04002BAE RID: 11182
		private static SqlDbType[] drdaToMSSqlMappings;

		// Token: 0x04002BAF RID: 11183
		private static DrdaType[] mssqlTodrdaMappings;

		// Token: 0x04002BB0 RID: 11184
		private static SQLType[] drdaTodb2Mappings = new SQLType[Constants.DRDATYPE_COUNT];
	}
}
