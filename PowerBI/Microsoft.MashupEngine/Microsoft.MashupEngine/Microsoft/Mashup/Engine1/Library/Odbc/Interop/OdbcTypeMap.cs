using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x02000714 RID: 1812
	internal sealed class OdbcTypeMap
	{
		// Token: 0x0600360B RID: 13835 RVA: 0x000ABB70 File Offset: 0x000A9D70
		static OdbcTypeMap()
		{
			foreach (OdbcTypeMap odbcTypeMap in OdbcTypeMap.All)
			{
				OdbcTypeMap.fromSqlType[odbcTypeMap.SqlType] = odbcTypeMap;
			}
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000AC250 File Offset: 0x000AA450
		private OdbcTypeMap(Odbc32.SQL_TYPE sqlType, Odbc32.SQL_C cType, Type type, DBTYPE oleDbType, TypeValue typeValue, int bsize, int csize, Odbc32.SQL_INFO? convertInfoType, Odbc32.SQL_CVT? convertBitmask)
		{
			this.SqlType = sqlType;
			this.CType = cType;
			this.Type = type;
			this.OleDbType = oleDbType;
			this.TypeValue = typeValue;
			this.BufferSize = bsize;
			this.ColumnSize = csize;
			this.ConvertInfoType = convertInfoType;
			this.ConvertBitmask = convertBitmask;
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000AC2A8 File Offset: 0x000AA4A8
		public string Token
		{
			get
			{
				Odbc32.SQL_TYPE sqlType = this.SqlType;
				switch (sqlType)
				{
				case Odbc32.SQL_TYPE.GUID:
					return "SQL_GUID";
				case Odbc32.SQL_TYPE.WLONGVARCHAR:
					return "SQL_WLONGVARCHAR";
				case Odbc32.SQL_TYPE.WVARCHAR:
					return "SQL_WVARCHAR";
				case Odbc32.SQL_TYPE.WCHAR:
					return "SQL_WCHAR";
				case Odbc32.SQL_TYPE.BIT:
					return "SQL_BIT";
				case Odbc32.SQL_TYPE.TINYINT:
					return "SQL_TINYINT";
				case Odbc32.SQL_TYPE.BIGINT:
					return "SQL_BIGINT";
				case Odbc32.SQL_TYPE.LONGVARBINARY:
					return "SQL_LONGVARBINARY";
				case Odbc32.SQL_TYPE.VARBINARY:
					return "SQL_VARBINARY";
				case Odbc32.SQL_TYPE.BINARY:
					return "SQL_BINARY";
				case Odbc32.SQL_TYPE.LONGVARCHAR:
					return "SQL_LONGVARCHAR";
				case Odbc32.SQL_TYPE.UNKNOWN:
				case Odbc32.SQL_TYPE.TIMESTAMP:
					break;
				case Odbc32.SQL_TYPE.CHAR:
					return "SQL_CHAR";
				case Odbc32.SQL_TYPE.NUMERIC:
					return "SQL_NUMERIC";
				case Odbc32.SQL_TYPE.DECIMAL:
					return "SQL_DECIMAL";
				case Odbc32.SQL_TYPE.INTEGER:
					return "SQL_INTEGER";
				case Odbc32.SQL_TYPE.SMALLINT:
					return "SQL_SMALLINT";
				case Odbc32.SQL_TYPE.FLOAT:
					return "SQL_FLOAT";
				case Odbc32.SQL_TYPE.REAL:
					return "SQL_REAL";
				case Odbc32.SQL_TYPE.DOUBLE:
					return "SQL_DOUBLE";
				case Odbc32.SQL_TYPE.DATETIME:
					return "SQL_DATETIME";
				case Odbc32.SQL_TYPE.INTERVAL:
					return "SQL_INTERVAL";
				case Odbc32.SQL_TYPE.VARCHAR:
					return "SQL_VARCHAR";
				default:
					if (sqlType == Odbc32.SQL_TYPE.TYPE_DATE)
					{
						return "SQL_DATE";
					}
					if (sqlType == Odbc32.SQL_TYPE.TYPE_TIMESTAMP)
					{
						return "SQL_TIMESTAMP";
					}
					break;
				}
				throw new InvalidOperationException(this.SqlType.ToString());
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000AC3E0 File Offset: 0x000AA5E0
		public bool IsWholeNumber
		{
			get
			{
				Odbc32.SQL_TYPE sqlType = this.SqlType;
				return sqlType - Odbc32.SQL_TYPE.BIT <= 2 || sqlType - Odbc32.SQL_TYPE.INTEGER <= 1;
			}
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000AC404 File Offset: 0x000AA604
		public OdbcTypeMap GetUnsigned()
		{
			Odbc32.SQL_C ctype = this.CType;
			if (ctype <= Odbc32.SQL_C.SBIGINT)
			{
				if (ctype == Odbc32.SQL_C.STINYINT)
				{
					return OdbcTypeMap.UTinyInt;
				}
				if (ctype == Odbc32.SQL_C.SBIGINT)
				{
					return OdbcTypeMap.Decimal;
				}
			}
			else
			{
				if (ctype == Odbc32.SQL_C.SLONG)
				{
					return OdbcTypeMap.BigInt;
				}
				if (ctype == Odbc32.SQL_C.SSHORT)
				{
					return OdbcTypeMap.Integer;
				}
			}
			return null;
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000AC450 File Offset: 0x000AA650
		public static OdbcTypeMap FromSqlType(Odbc32.SQL_TYPE sqlType)
		{
			OdbcTypeMap binary;
			if (!OdbcTypeMap.TryGet(sqlType, out binary))
			{
				binary = OdbcTypeMap.Binary;
			}
			return binary;
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000AC46E File Offset: 0x000AA66E
		public static bool TryGet(Odbc32.SQL_TYPE sqlType, out OdbcTypeMap typeMap)
		{
			return OdbcTypeMap.fromSqlType.TryGetValue(sqlType, out typeMap);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000AC47C File Offset: 0x000AA67C
		public static bool IsKnownType(Odbc32.SQL_TYPE sqlType)
		{
			return OdbcTypeMap.fromSqlType.ContainsKey(sqlType);
		}

		// Token: 0x04001BA6 RID: 7078
		public static readonly OdbcTypeMap BigInt = new OdbcTypeMap(Odbc32.SQL_TYPE.BIGINT, Odbc32.SQL_C.SBIGINT, typeof(long), DBTYPE.I8, TypeValue.Int64, 8, 20, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_BIGINT), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.BIGINT));

		// Token: 0x04001BA7 RID: 7079
		public static readonly OdbcTypeMap Binary = new OdbcTypeMap(Odbc32.SQL_TYPE.BINARY, Odbc32.SQL_C.BINARY, typeof(byte[]), DBTYPE.BYTES, TypeValue.Binary, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_BINARY), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.BINARY));

		// Token: 0x04001BA8 RID: 7080
		public static readonly OdbcTypeMap Bit = new OdbcTypeMap(Odbc32.SQL_TYPE.BIT, Odbc32.SQL_C.BIT, typeof(bool), DBTYPE.BOOL, TypeValue.Logical, 1, 1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_BIT), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.BIT));

		// Token: 0x04001BA9 RID: 7081
		public static readonly OdbcTypeMap Char = new OdbcTypeMap(Odbc32.SQL_TYPE.CHAR, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_CHAR), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.CHAR));

		// Token: 0x04001BAA RID: 7082
		public static readonly OdbcTypeMap DateTime = new OdbcTypeMap(Odbc32.SQL_TYPE.DATETIME, Odbc32.SQL_C.TYPE_TIMESTAMP, typeof(DateTime), DBTYPE.DBTIMESTAMP, TypeValue.DateTime, 16, 23, null, null);

		// Token: 0x04001BAB RID: 7083
		public static readonly OdbcTypeMap Decimal = new OdbcTypeMap(Odbc32.SQL_TYPE.DECIMAL, Odbc32.SQL_C.WCHAR, typeof(decimal), DBTYPE.DECIMAL, TypeValue.Decimal, 256, 256, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_DECIMAL), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.DECIMAL));

		// Token: 0x04001BAC RID: 7084
		public static readonly OdbcTypeMap Double = new OdbcTypeMap(Odbc32.SQL_TYPE.DOUBLE, Odbc32.SQL_C.DOUBLE, typeof(double), DBTYPE.R8, TypeValue.Double, 8, 15, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_DOUBLE), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.DOUBLE));

		// Token: 0x04001BAD RID: 7085
		public static readonly OdbcTypeMap Float = new OdbcTypeMap(Odbc32.SQL_TYPE.FLOAT, Odbc32.SQL_C.DOUBLE, typeof(double), DBTYPE.R8, TypeValue.Double, 8, 15, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_FLOAT), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.FLOAT));

		// Token: 0x04001BAE RID: 7086
		public static readonly OdbcTypeMap Guid = new OdbcTypeMap(Odbc32.SQL_TYPE.GUID, Odbc32.SQL_C.GUID, typeof(Guid), DBTYPE.GUID, TypeValue.Guid, 16, 36, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_GUID), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.GUID));

		// Token: 0x04001BAF RID: 7087
		public static readonly OdbcTypeMap Integer = new OdbcTypeMap(Odbc32.SQL_TYPE.INTEGER, Odbc32.SQL_C.SLONG, typeof(int), DBTYPE.I4, TypeValue.Int32, 4, 10, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_INTEGER), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.INTEGER));

		// Token: 0x04001BB0 RID: 7088
		public static readonly OdbcTypeMap LongVarBinary = new OdbcTypeMap(Odbc32.SQL_TYPE.LONGVARBINARY, Odbc32.SQL_C.BINARY, typeof(byte[]), DBTYPE.BYTES, TypeValue.Binary, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_LONGVARBINARY), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.LONGVARBINARY));

		// Token: 0x04001BB1 RID: 7089
		public static readonly OdbcTypeMap LongVarChar = new OdbcTypeMap(Odbc32.SQL_TYPE.LONGVARCHAR, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_LONGVARCHAR), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.LONGVARCHAR));

		// Token: 0x04001BB2 RID: 7090
		public static readonly OdbcTypeMap Numeric = new OdbcTypeMap(Odbc32.SQL_TYPE.NUMERIC, Odbc32.SQL_C.WCHAR, typeof(decimal), DBTYPE.DECIMAL, TypeValue.Decimal, 256, 256, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_NUMERIC), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.NUMERIC));

		// Token: 0x04001BB3 RID: 7091
		public static readonly OdbcTypeMap Real = new OdbcTypeMap(Odbc32.SQL_TYPE.REAL, Odbc32.SQL_C.FLOAT, typeof(float), DBTYPE.R4, TypeValue.Single, 4, 7, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_REAL), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.REAL));

		// Token: 0x04001BB4 RID: 7092
		public static readonly OdbcTypeMap SmallInt = new OdbcTypeMap(Odbc32.SQL_TYPE.SMALLINT, Odbc32.SQL_C.SSHORT, typeof(short), DBTYPE.I2, TypeValue.Int16, 2, 5, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_SMALLINT), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.SMALLINT));

		// Token: 0x04001BB5 RID: 7093
		public static readonly OdbcTypeMap TimeStamp = new OdbcTypeMap(Odbc32.SQL_TYPE.TIMESTAMP, Odbc32.SQL_C.TYPE_TIMESTAMP, typeof(DateTime), DBTYPE.DBTIMESTAMP, TypeValue.DateTime, 16, 23, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_TIMESTAMP), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.TIMESTAMP));

		// Token: 0x04001BB6 RID: 7094
		public static readonly OdbcTypeMap TinyInt = new OdbcTypeMap(Odbc32.SQL_TYPE.TINYINT, Odbc32.SQL_C.STINYINT, typeof(sbyte), DBTYPE.I1, TypeValue.Int8, 1, 3, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_TINYINT), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.TINYINT));

		// Token: 0x04001BB7 RID: 7095
		public static readonly OdbcTypeMap UTinyInt = new OdbcTypeMap(Odbc32.SQL_TYPE.TINYINT, Odbc32.SQL_C.UTINYINT, typeof(byte), DBTYPE.UI1, TypeValue.Byte, 1, 3, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_TINYINT), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.TINYINT));

		// Token: 0x04001BB8 RID: 7096
		public static readonly OdbcTypeMap TypeDate = new OdbcTypeMap(Odbc32.SQL_TYPE.TYPE_DATE, Odbc32.SQL_C.TYPE_DATE, typeof(Date), DBTYPE.DBDATE, TypeValue.Date, 6, 10, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_DATE), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.DATE));

		// Token: 0x04001BB9 RID: 7097
		public static readonly OdbcTypeMap TypeTime = new OdbcTypeMap(Odbc32.SQL_TYPE.TYPE_TIME, Odbc32.SQL_C.TYPE_TIME, typeof(Time), DBTYPE.DBTIME, TypeValue.Time, 6, 12, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_TIME), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.TIME));

		// Token: 0x04001BBA RID: 7098
		public static readonly OdbcTypeMap TypeTimeStamp = new OdbcTypeMap(Odbc32.SQL_TYPE.TYPE_TIMESTAMP, Odbc32.SQL_C.TYPE_TIMESTAMP, typeof(DateTime), DBTYPE.DBTIMESTAMP, TypeValue.DateTime, 16, 23, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_TIMESTAMP), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.TIMESTAMP));

		// Token: 0x04001BBB RID: 7099
		public static readonly OdbcTypeMap VarBinary = new OdbcTypeMap(Odbc32.SQL_TYPE.VARBINARY, Odbc32.SQL_C.BINARY, typeof(byte[]), DBTYPE.BYTES, TypeValue.Binary, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_VARBINARY), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.VARBINARY));

		// Token: 0x04001BBC RID: 7100
		public static readonly OdbcTypeMap VarChar = new OdbcTypeMap(Odbc32.SQL_TYPE.VARCHAR, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_VARCHAR), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.VARCHAR));

		// Token: 0x04001BBD RID: 7101
		public static readonly OdbcTypeMap WChar = new OdbcTypeMap(Odbc32.SQL_TYPE.WCHAR, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_WCHAR), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.WCHAR));

		// Token: 0x04001BBE RID: 7102
		public static readonly OdbcTypeMap WLongVarChar = new OdbcTypeMap(Odbc32.SQL_TYPE.WLONGVARCHAR, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_WLONGVARCHAR), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.WLONGVARCHAR));

		// Token: 0x04001BBF RID: 7103
		public static readonly OdbcTypeMap WVarchar = new OdbcTypeMap(Odbc32.SQL_TYPE.WVARCHAR, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, new Odbc32.SQL_INFO?(Odbc32.SQL_INFO.SQL_CONVERT_WVARCHAR), new Odbc32.SQL_CVT?(Odbc32.SQL_CVT.WVARCHAR));

		// Token: 0x04001BC0 RID: 7104
		public static readonly OdbcTypeMap SSXml = new OdbcTypeMap(Odbc32.SQL_TYPE.SS_XML, Odbc32.SQL_C.WCHAR, typeof(string), DBTYPE.WSTR, TypeValue.Text, -1, -1, null, null);

		// Token: 0x04001BC1 RID: 7105
		public static readonly OdbcTypeMap SSVariant = new OdbcTypeMap(Odbc32.SQL_TYPE.SS_VARIANT, Odbc32.SQL_C.BINARY, typeof(object), DBTYPE.VARIANT, TypeValue.Any, -1, -1, null, null);

		// Token: 0x04001BC2 RID: 7106
		public static readonly OdbcTypeMap[] All = new OdbcTypeMap[]
		{
			OdbcTypeMap.BigInt,
			OdbcTypeMap.Binary,
			OdbcTypeMap.Bit,
			OdbcTypeMap.Char,
			OdbcTypeMap.DateTime,
			OdbcTypeMap.Decimal,
			OdbcTypeMap.Double,
			OdbcTypeMap.Float,
			OdbcTypeMap.Guid,
			OdbcTypeMap.Integer,
			OdbcTypeMap.LongVarBinary,
			OdbcTypeMap.LongVarChar,
			OdbcTypeMap.Numeric,
			OdbcTypeMap.Real,
			OdbcTypeMap.SmallInt,
			OdbcTypeMap.TimeStamp,
			OdbcTypeMap.TinyInt,
			OdbcTypeMap.TypeDate,
			OdbcTypeMap.TypeTime,
			OdbcTypeMap.TypeTimeStamp,
			OdbcTypeMap.VarBinary,
			OdbcTypeMap.VarChar,
			OdbcTypeMap.WChar,
			OdbcTypeMap.WLongVarChar,
			OdbcTypeMap.WVarchar,
			OdbcTypeMap.SSXml,
			OdbcTypeMap.SSVariant
		};

		// Token: 0x04001BC3 RID: 7107
		private static readonly Dictionary<Odbc32.SQL_TYPE, OdbcTypeMap> fromSqlType = new Dictionary<Odbc32.SQL_TYPE, OdbcTypeMap>();

		// Token: 0x04001BC4 RID: 7108
		public readonly Odbc32.SQL_TYPE SqlType;

		// Token: 0x04001BC5 RID: 7109
		public readonly Type Type;

		// Token: 0x04001BC6 RID: 7110
		public readonly Odbc32.SQL_C CType;

		// Token: 0x04001BC7 RID: 7111
		public readonly DBTYPE OleDbType;

		// Token: 0x04001BC8 RID: 7112
		public readonly TypeValue TypeValue;

		// Token: 0x04001BC9 RID: 7113
		public readonly int BufferSize;

		// Token: 0x04001BCA RID: 7114
		public readonly int ColumnSize;

		// Token: 0x04001BCB RID: 7115
		public readonly Odbc32.SQL_INFO? ConvertInfoType;

		// Token: 0x04001BCC RID: 7116
		public readonly Odbc32.SQL_CVT? ConvertBitmask;
	}
}
