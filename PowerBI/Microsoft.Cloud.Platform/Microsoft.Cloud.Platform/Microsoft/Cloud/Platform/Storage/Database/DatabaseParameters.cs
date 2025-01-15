using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000032 RID: 50
	public static class DatabaseParameters
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00004B52 File Offset: 0x00002D52
		public static SqlParameter CreateParameter(string name, SqlDbType type, int size, object value)
		{
			return new SqlParameter(name, type, size)
			{
				Value = value
			};
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004B63 File Offset: 0x00002D63
		public static SqlParameter CreateParameter(string name, SqlDbType type, object value)
		{
			return new SqlParameter(name, type)
			{
				Value = value
			};
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004B73 File Offset: 0x00002D73
		public static SqlParameter CreateParameter(string name, string value)
		{
			return new SqlParameter(name, SqlDbType.NVarChar)
			{
				Value = value
			};
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B84 File Offset: 0x00002D84
		public static SqlParameter CreateParameter(string name, DateTime value)
		{
			return new SqlParameter(name, SqlDbType.DateTime)
			{
				Value = DatabaseParameters.Normalize(value)
			};
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B9E File Offset: 0x00002D9E
		public static SqlParameter CreateParameter(string name, int value)
		{
			return new SqlParameter(name, SqlDbType.Int)
			{
				Value = value
			};
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004BB3 File Offset: 0x00002DB3
		public static SqlParameter CreateParameter(string name, long value)
		{
			return new SqlParameter(name, SqlDbType.BigInt)
			{
				Value = value
			};
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public static SqlParameter CreateParameter(string name, Guid value)
		{
			return new SqlParameter(name, SqlDbType.UniqueIdentifier)
			{
				Value = value
			};
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004BDE File Offset: 0x00002DDE
		public static SqlParameter CreateParameter(string name, DateTime? value, SqlDbType sqlDateType = SqlDbType.DateTime)
		{
			if (sqlDateType != SqlDbType.DateTime && sqlDateType != SqlDbType.DateTime2)
			{
				throw new ArgumentException("Expected DateTime or DateTime2 as argument");
			}
			return new SqlParameter(name, sqlDateType)
			{
				Value = DatabaseParameters.GetDbValue(value)
			};
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004C07 File Offset: 0x00002E07
		public static SqlParameter CreateParameter(string name, bool value)
		{
			return new SqlParameter(name, SqlDbType.Bit)
			{
				Value = (value ? 1 : 0)
			};
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004C22 File Offset: 0x00002E22
		public static SqlParameter CreateParameter(string name, byte[] value)
		{
			return new SqlParameter(name, SqlDbType.VarBinary)
			{
				Value = value
			};
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004C34 File Offset: 0x00002E34
		public static DateTime Normalize(DateTime dt)
		{
			if (dt < (DateTime)SqlDateTime.MinValue)
			{
				return SqlDateTime.MinValue.Value;
			}
			if (dt > (DateTime)SqlDateTime.MaxValue)
			{
				return SqlDateTime.MaxValue.Value;
			}
			return dt;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004C82 File Offset: 0x00002E82
		public static object GetDbValue<T>(T? value) where T : struct
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			return value.Value;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004C9F File Offset: 0x00002E9F
		public static object GetDbValue<T>(T value) where T : class
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			return value;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004CB5 File Offset: 0x00002EB5
		public static object GetDbValue(DateTime? value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			return DatabaseParameters.Normalize(value.Value);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public static short? GetClientNullableInt16(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new short?(reader.GetInt16(ordinal));
			}
			return null;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004D04 File Offset: 0x00002F04
		public static int? GetClientNullableInt32(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new int?(reader.GetInt32(ordinal));
			}
			return null;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004D30 File Offset: 0x00002F30
		public static long? GetClientNullableInt64(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new long?(reader.GetInt64(ordinal));
			}
			return null;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004D5C File Offset: 0x00002F5C
		public static decimal? GetClientNullableDecimal(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new decimal?(reader.GetDecimal(ordinal));
			}
			return null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004D88 File Offset: 0x00002F88
		public static DateTime? GetClientNullableDateTime(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new DateTime?(DatabaseParameters.GetClientDateTime(reader, ordinal));
			}
			return null;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public static Guid? GetClientNullableGuid(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new Guid?(reader.GetGuid(ordinal));
			}
			return null;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public static DateTime GetClientDateTime(SqlDataReader reader, int ordinal)
		{
			DateTime dateTime = reader.GetDateTime(ordinal);
			if (dateTime.Equals(SqlDateTime.MinValue.Value))
			{
				dateTime = DateTime.MinValue;
			}
			else if (dateTime.Equals(SqlDateTime.MaxValue.Value))
			{
				dateTime = DateTime.MaxValue;
			}
			return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004E38 File Offset: 0x00003038
		public static bool? GetClientNullableBoolean(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return new bool?(reader.GetBoolean(ordinal));
			}
			return null;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004E64 File Offset: 0x00003064
		public static string GetClientNullableString(SqlDataReader reader, int ordinal)
		{
			if (!reader.IsDBNull(ordinal))
			{
				return reader.GetString(ordinal);
			}
			return null;
		}
	}
}
