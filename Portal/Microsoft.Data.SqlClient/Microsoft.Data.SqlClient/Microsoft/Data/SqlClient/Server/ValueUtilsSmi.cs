using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000141 RID: 321
	internal static class ValueUtilsSmi
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x000669E6 File Offset: 0x00064BE6
		internal static bool IsDBNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000669F0 File Offset: 0x00064BF0
		internal static bool GetBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Boolean))
			{
				return ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (bool)value;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00066A34 File Offset: 0x00064C34
		internal static byte GetByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Byte))
			{
				return ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (byte)value;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00066A78 File Offset: 0x00064C78
		private static long GetBytesConversion(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
			if (sqlValue == null)
			{
				throw ADP.InvalidCast();
			}
			SqlBinary sqlBinary = (SqlBinary)sqlValue;
			if (sqlBinary.IsNull)
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				return 0L;
			}
			else
			{
				if (buffer == null)
				{
					return (long)sqlBinary.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)sqlBinary.Length, fieldOffset, buffer.Length, bufferOffset, length);
				Buffer.BlockCopy(sqlBinary.Value, checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00066B04 File Offset: 0x00064D04
		internal static long GetBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiExtendedMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if ((-1L != metaData.MaxLength && (metaData.SqlDbType == SqlDbType.VarChar || metaData.SqlDbType == SqlDbType.NVarChar || metaData.SqlDbType == SqlDbType.Char || metaData.SqlDbType == SqlDbType.NChar)) || SqlDbType.Xml == metaData.SqlDbType)
			{
				throw SQL.NonBlobColumn(metaData.Name);
			}
			return ValueUtilsSmi.GetBytesInternal(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00066B6C File Offset: 0x00064D6C
		internal static long GetBytesInternal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if (!ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.ByteArray))
			{
				return ValueUtilsSmi.GetBytesConversion(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
			}
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, 0L, fieldOffset, buffer.Length, bufferOffset, length);
				return 0L;
			}
			else
			{
				long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return bytesLength_Unchecked;
				}
				if (MetaDataUtilsSmi.IsCharOrXmlType(metaData.SqlDbType))
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				else
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				if (length > 0)
				{
					length = ValueUtilsSmi.GetBytes_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00066C48 File Offset: 0x00064E48
		internal static long GetChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.CharArray))
			{
				long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return charsLength_Unchecked;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, charsLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				if (length > 0)
				{
					length = ValueUtilsSmi.GetChars_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
			else
			{
				string text = (string)ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
				if (text == null)
				{
					throw ADP.InvalidCast();
				}
				if (buffer == null)
				{
					return (long)text.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)text.Length, fieldOffset, buffer.Length, bufferOffset, length);
				text.CopyTo(checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00066D10 File Offset: 0x00064F10
		internal static DateTime GetDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTime))
			{
				return ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTime)value;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00066D54 File Offset: 0x00064F54
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool gettersSupport2008DateTime)
		{
			if (gettersSupport2008DateTime)
			{
				return ValueUtilsSmi.GetDateTimeOffset(sink, (SmiTypedGetterSetter)getters, ordinal, metaData);
			}
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTimeOffset)value;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00066D96 File Offset: 0x00064F96
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTimeOffset))
			{
				return ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
			}
			return (DateTimeOffset)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData, null);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00066DC4 File Offset: 0x00064FC4
		internal static decimal GetDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Decimal))
			{
				return ValueUtilsSmi.GetDecimal_PossiblyMoney(sink, getters, ordinal, metaData);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (decimal)value;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00066E08 File Offset: 0x00065008
		internal static double GetDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Double))
			{
				return ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (double)value;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00066E4C File Offset: 0x0006504C
		internal static Guid GetGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Guid))
			{
				return ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (Guid)value;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00066E90 File Offset: 0x00065090
		internal static short GetInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int16))
			{
				return ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (short)value;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00066ED4 File Offset: 0x000650D4
		internal static int GetInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int32))
			{
				return ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (int)value;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00066F18 File Offset: 0x00065118
		internal static long GetInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int64))
			{
				return ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (long)value;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00066F5C File Offset: 0x0006515C
		internal static float GetSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Single))
			{
				return ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (float)value;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00066FA0 File Offset: 0x000651A0
		internal static SqlBinary GetSqlBinary(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBinary))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBinary.Null;
				}
				return ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBinary)sqlValue;
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00066FEC File Offset: 0x000651EC
		internal static SqlBoolean GetSqlBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBoolean))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBoolean)sqlValue;
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0006703C File Offset: 0x0006523C
		internal static SqlByte GetSqlByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlByte))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlByte.Null;
				}
				return new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlByte)sqlValue;
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0006708C File Offset: 0x0006528C
		internal static SqlBytes GetSqlBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			SqlBytes sqlBytes;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBytes))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlBytes = SqlBytes.Null;
				}
				else
				{
					long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
					if (bytesLength_Unchecked >= 0L && bytesLength_Unchecked < 8000L)
					{
						byte[] byteArray_Unchecked = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
						sqlBytes = new SqlBytes(byteArray_Unchecked);
					}
					else
					{
						Stream stream = new SmiGettersStream(sink, getters, ordinal, metaData);
						stream = ValueUtilsSmi.CopyIntoNewSmiScratchStream(stream, sink, context);
						sqlBytes = new SqlBytes(stream);
					}
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlBinary sqlBinary = (SqlBinary)sqlValue;
				if (sqlBinary.IsNull)
				{
					sqlBytes = SqlBytes.Null;
				}
				else
				{
					sqlBytes = new SqlBytes(sqlBinary.Value);
				}
			}
			return sqlBytes;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0006713C File Offset: 0x0006533C
		internal static SqlChars GetSqlChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			SqlChars sqlChars;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlChars))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlChars = SqlChars.Null;
				}
				else
				{
					long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
					if (charsLength_Unchecked < 8000L || !InOutOfProcHelper.InProc)
					{
						char[] charArray_Unchecked = ValueUtilsSmi.GetCharArray_Unchecked(sink, getters, ordinal);
						sqlChars = new SqlChars(charArray_Unchecked);
					}
					else
					{
						Stream stream = new SmiGettersStream(sink, getters, ordinal, metaData);
						SqlStreamChars sqlStreamChars = ValueUtilsSmi.CopyIntoNewSmiScratchStreamChars(stream, sink, context);
						Type typeFromHandle = typeof(SqlChars);
						Type[] array = new Type[] { typeof(SqlStreamChars) };
						SqlChars sqlChars2 = (SqlChars)typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, array, null).Invoke(new object[] { sqlStreamChars });
						sqlChars = sqlChars2;
					}
				}
			}
			else if (metaData.SqlDbType == SqlDbType.Xml)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, null);
				if (sqlXml_Unchecked.IsNull)
				{
					sqlChars = SqlChars.Null;
				}
				else
				{
					sqlChars = new SqlChars(sqlXml_Unchecked.Value.ToCharArray());
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlString sqlString = (SqlString)sqlValue;
				if (sqlString.IsNull)
				{
					sqlChars = SqlChars.Null;
				}
				else
				{
					sqlChars = new SqlChars(sqlString.Value.ToCharArray());
				}
			}
			return sqlChars;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00067278 File Offset: 0x00065478
		internal static SqlDateTime GetSqlDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDateTime sqlDateTime;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDateTime))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlDateTime = SqlDateTime.Null;
				}
				else
				{
					DateTime dateTime_Unchecked = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					sqlDateTime = new SqlDateTime(dateTime_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlDateTime = (SqlDateTime)sqlValue;
			}
			return sqlDateTime;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000672D0 File Offset: 0x000654D0
		internal static SqlDecimal GetSqlDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDecimal sqlDecimal;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDecimal))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlDecimal = SqlDecimal.Null;
				}
				else
				{
					sqlDecimal = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlDecimal = (SqlDecimal)sqlValue;
			}
			return sqlDecimal;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00067320 File Offset: 0x00065520
		internal static SqlDouble GetSqlDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDouble sqlDouble;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDouble))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlDouble = SqlDouble.Null;
				}
				else
				{
					double double_Unchecked = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					sqlDouble = new SqlDouble(double_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlDouble = (SqlDouble)sqlValue;
			}
			return sqlDouble;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00067378 File Offset: 0x00065578
		internal static SqlGuid GetSqlGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlGuid sqlGuid;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlGuid))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlGuid = SqlGuid.Null;
				}
				else
				{
					Guid guid_Unchecked = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					sqlGuid = new SqlGuid(guid_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlGuid = (SqlGuid)sqlValue;
			}
			return sqlGuid;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000673D0 File Offset: 0x000655D0
		internal static SqlInt16 GetSqlInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt16 sqlInt;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt16))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlInt = SqlInt16.Null;
				}
				else
				{
					short int16_Unchecked = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					sqlInt = new SqlInt16(int16_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlInt = (SqlInt16)sqlValue;
			}
			return sqlInt;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00067428 File Offset: 0x00065628
		internal static SqlInt32 GetSqlInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt32 sqlInt;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt32))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlInt = SqlInt32.Null;
				}
				else
				{
					int int32_Unchecked = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					sqlInt = new SqlInt32(int32_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlInt = (SqlInt32)sqlValue;
			}
			return sqlInt;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00067480 File Offset: 0x00065680
		internal static SqlInt64 GetSqlInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt64 sqlInt;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt64))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlInt = SqlInt64.Null;
				}
				else
				{
					long int64_Unchecked = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					sqlInt = new SqlInt64(int64_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlInt = (SqlInt64)sqlValue;
			}
			return sqlInt;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000674D8 File Offset: 0x000656D8
		internal static SqlMoney GetSqlMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlMoney sqlMoney;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlMoney))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlMoney = SqlMoney.Null;
				}
				else
				{
					sqlMoney = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlMoney = (SqlMoney)sqlValue;
			}
			return sqlMoney;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00067528 File Offset: 0x00065728
		internal static SqlSingle GetSqlSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlSingle sqlSingle;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlSingle))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlSingle = SqlSingle.Null;
				}
				else
				{
					float single_Unchecked = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					sqlSingle = new SqlSingle(single_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlSingle = (SqlSingle)sqlValue;
			}
			return sqlSingle;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00067580 File Offset: 0x00065780
		internal static SqlString GetSqlString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlString sqlString;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlString))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlString = SqlString.Null;
				}
				else
				{
					string string_Unchecked = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					sqlString = new SqlString(string_Unchecked);
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, null);
				if (sqlXml_Unchecked.IsNull)
				{
					sqlString = SqlString.Null;
				}
				else
				{
					sqlString = new SqlString(sqlXml_Unchecked.Value);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlString = (SqlString)sqlValue;
			}
			return sqlString;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0006760C File Offset: 0x0006580C
		internal static SqlXml GetSqlXml(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			SqlXml sqlXml;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlXml))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					sqlXml = SqlXml.Null;
				}
				else
				{
					sqlXml = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, context);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				sqlXml = (SqlXml)sqlValue;
			}
			return sqlXml;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00067660 File Offset: 0x00065860
		internal static string GetString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.String))
			{
				return ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (string)value;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000676A2 File Offset: 0x000658A2
		internal static TimeSpan GetTimeSpan(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TimeSpan))
			{
				return ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
			}
			return (TimeSpan)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData, null);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000676D0 File Offset: 0x000658D0
		internal static object GetValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			object obj;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				obj = DBNull.Value;
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						obj = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						obj = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						obj = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, context);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData, context);
				}
			}
			return obj;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00067774 File Offset: 0x00065974
		internal static object GetValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context = null)
		{
			object obj = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				obj = DBNull.Value;
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					obj = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Binary:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					obj = ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Char:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.DateTime:
					obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Decimal:
					obj = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Float:
					obj = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Image:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					obj = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Money:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.NChar:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NText:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NVarChar:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Real:
					obj = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.UniqueIdentifier:
					obj = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallDateTime:
					obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallInt:
					obj = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallMoney:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Text:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Timestamp:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					obj = ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarBinary:
					obj = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					obj = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, context);
					break;
				case SqlDbType.Xml:
					obj = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, context).Value;
					break;
				case SqlDbType.Udt:
					obj = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return obj;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000679E8 File Offset: 0x00065BE8
		internal static object GetSqlValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData, SmiContext context = null)
		{
			object obj;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (metaData.SqlDbType == SqlDbType.Udt)
				{
					obj = ValueUtilsSmi.NullUdtInstance(metaData);
				}
				else
				{
					obj = ValueUtilsSmi.s_typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
				}
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						obj = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						obj = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						obj = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						obj = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, context);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetSqlValue200(sink, getters, ordinal, metaData, context);
				}
			}
			return obj;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00067AAC File Offset: 0x00065CAC
		internal static object GetSqlValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context = null)
		{
			object obj = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (metaData.SqlDbType == SqlDbType.Udt)
				{
					obj = ValueUtilsSmi.NullUdtInstance(metaData);
				}
				else
				{
					obj = ValueUtilsSmi.s_typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
				}
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					obj = new SqlInt64(ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Binary:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					obj = new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Char:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.DateTime:
					obj = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Decimal:
					obj = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Float:
					obj = new SqlDouble(ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Image:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					obj = new SqlInt32(ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Money:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NChar:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NText:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NVarChar:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Real:
					obj = new SqlSingle(ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.UniqueIdentifier:
					obj = new SqlGuid(ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallDateTime:
					obj = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallInt:
					obj = new SqlInt16(ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallMoney:
					obj = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Text:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Timestamp:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					obj = new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.VarBinary:
					obj = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					obj = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					obj = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, context);
					break;
				case SqlDbType.Xml:
					obj = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, context);
					break;
				case SqlDbType.Udt:
					obj = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return obj;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00067DA4 File Offset: 0x00065FA4
		internal static object NullUdtInstance(SmiMetaData metaData)
		{
			Type type = metaData.Type;
			return type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, Array.Empty<object>(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00067DD4 File Offset: 0x00065FD4
		internal static void SetDBNull(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal)
		{
			ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00067DDE File Offset: 0x00065FDE
		internal static void SetBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, bool value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Boolean);
			ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00067DF1 File Offset: 0x00065FF1
		internal static void SetByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Byte);
			ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00067E04 File Offset: 0x00066004
		internal static long SetBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetBytes_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00067E64 File Offset: 0x00066064
		internal static long SetBytesLength(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (length < 0L)
			{
				throw ADP.InvalidDataLength(length);
			}
			if (metaData.MaxLength >= 0L && length > metaData.MaxLength)
			{
				length = metaData.MaxLength;
			}
			setters.SetBytesLength(sink, ordinal, length);
			sink.ProcessMessagesAndThrow();
			return length;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00067EB8 File Offset: 0x000660B8
		internal static long SetChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.CharArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetChars_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00067F17 File Offset: 0x00066117
		internal static void SetDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTime);
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00067F2B File Offset: 0x0006612B
		internal static void SetDateTimeOffset(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTimeOffset value, bool settersSupport2008DateTime = true)
		{
			if (!settersSupport2008DateTime)
			{
				throw ADP.InvalidCast();
			}
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTimeOffset);
			ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, (SmiTypedGetterSetter)setters, ordinal, value);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00067F4E File Offset: 0x0006614E
		internal static void SetDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Decimal);
			ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00067F62 File Offset: 0x00066162
		internal static void SetDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, double value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Double);
			ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00067F75 File Offset: 0x00066175
		internal static void SetGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, Guid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Guid);
			ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00067F89 File Offset: 0x00066189
		internal static void SetInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, short value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int16);
			ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00067F9C File Offset: 0x0006619C
		internal static void SetInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, int value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int32);
			ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00067FB0 File Offset: 0x000661B0
		internal static void SetInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int64);
			ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00067FC4 File Offset: 0x000661C4
		internal static void SetSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, float value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Single);
			ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00067FD8 File Offset: 0x000661D8
		internal static void SetSqlBinary(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBinary);
			ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00067FEE File Offset: 0x000661EE
		internal static void SetSqlBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBoolean value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBoolean);
			ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00068002 File Offset: 0x00066202
		internal static void SetSqlByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlByte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlByte);
			ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00068016 File Offset: 0x00066216
		internal static void SetSqlBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBytes);
			ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0006802C File Offset: 0x0006622C
		internal static void SetSqlChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlChars);
			ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00068042 File Offset: 0x00066242
		internal static void SetSqlDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDateTime);
			ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00068057 File Offset: 0x00066257
		internal static void SetSqlDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDecimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDecimal);
			ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0006806B File Offset: 0x0006626B
		internal static void SetSqlDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDouble value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDouble);
			ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0006807F File Offset: 0x0006627F
		internal static void SetSqlGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlGuid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlGuid);
			ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00068093 File Offset: 0x00066293
		internal static void SetSqlInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt16 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt16);
			ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000680A7 File Offset: 0x000662A7
		internal static void SetSqlInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt32 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt32);
			ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000680BB File Offset: 0x000662BB
		internal static void SetSqlInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt64 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt64);
			ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000680CF File Offset: 0x000662CF
		internal static void SetSqlMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlMoney);
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000680E4 File Offset: 0x000662E4
		internal static void SetSqlSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlSingle value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlSingle);
			ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000680F8 File Offset: 0x000662F8
		internal static void SetSqlString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlString);
			ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0006810E File Offset: 0x0006630E
		internal static void SetSqlXml(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlXml value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlXml);
			ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00068122 File Offset: 0x00066322
		internal static void SetString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.String);
			ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00068138 File Offset: 0x00066338
		internal static void SetTimeSpan(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, TimeSpan value, bool settersSupport2008DateTime = true)
		{
			if (!settersSupport2008DateTime)
			{
				throw ADP.InvalidCast();
			}
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.TimeSpan);
			ValueUtilsSmi.SetTimeSpan_Checked(sink, (SmiTypedGetterSetter)setters, ordinal, metaData, value);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0006815C File Offset: 0x0006635C
		internal static void SetCompatibleValue(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.Invalid:
				throw ADP.UnknownDataType(value.GetType());
			case ExtendedClrTypeCode.Boolean:
				ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, (bool)value);
				return;
			case ExtendedClrTypeCode.Byte:
				ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, (byte)value);
				return;
			case ExtendedClrTypeCode.Char:
			{
				char[] array = new char[] { (char)value };
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, array, ExtendedClrTypeCode.CharArray, 0);
				return;
			}
			case ExtendedClrTypeCode.DateTime:
				ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			case ExtendedClrTypeCode.DBNull:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Decimal:
				ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, (decimal)value);
				return;
			case ExtendedClrTypeCode.Double:
				ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, (double)value);
				return;
			case ExtendedClrTypeCode.Empty:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Int16:
				ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, (short)value);
				return;
			case ExtendedClrTypeCode.Int32:
				ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, (int)value);
				return;
			case ExtendedClrTypeCode.Int64:
				ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, (long)value);
				return;
			case ExtendedClrTypeCode.SByte:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Single:
				ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, (float)value);
				return;
			case ExtendedClrTypeCode.String:
				ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, (string)value, offset);
				return;
			case ExtendedClrTypeCode.UInt16:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt32:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt64:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Object:
				ValueUtilsSmi.SetUdt_LengthChecked(sink, setters, ordinal, metaData, value);
				return;
			case ExtendedClrTypeCode.ByteArray:
				ValueUtilsSmi.SetByteArray_LengthChecked(sink, setters, ordinal, metaData, (byte[])value, offset);
				return;
			case ExtendedClrTypeCode.CharArray:
				ValueUtilsSmi.SetCharArray_LengthChecked(sink, setters, ordinal, metaData, (char[])value, offset);
				return;
			case ExtendedClrTypeCode.Guid:
				ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, (Guid)value);
				return;
			case ExtendedClrTypeCode.SqlBinary:
				ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, (SqlBinary)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBoolean:
				ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, (SqlBoolean)value);
				return;
			case ExtendedClrTypeCode.SqlByte:
				ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, (SqlByte)value);
				return;
			case ExtendedClrTypeCode.SqlDateTime:
				ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, (SqlDateTime)value);
				return;
			case ExtendedClrTypeCode.SqlDouble:
				ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, (SqlDouble)value);
				return;
			case ExtendedClrTypeCode.SqlGuid:
				ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, (SqlGuid)value);
				return;
			case ExtendedClrTypeCode.SqlInt16:
				ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, (SqlInt16)value);
				return;
			case ExtendedClrTypeCode.SqlInt32:
				ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, (SqlInt32)value);
				return;
			case ExtendedClrTypeCode.SqlInt64:
				ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, (SqlInt64)value);
				return;
			case ExtendedClrTypeCode.SqlMoney:
				ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, (SqlMoney)value);
				return;
			case ExtendedClrTypeCode.SqlDecimal:
				ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, (SqlDecimal)value);
				return;
			case ExtendedClrTypeCode.SqlSingle:
				ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, (SqlSingle)value);
				return;
			case ExtendedClrTypeCode.SqlString:
				ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, (SqlString)value, offset);
				return;
			case ExtendedClrTypeCode.SqlChars:
				ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, (SqlChars)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBytes:
				ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, (SqlBytes)value, offset);
				return;
			case ExtendedClrTypeCode.SqlXml:
				ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, (SqlXml)value);
				return;
			case ExtendedClrTypeCode.DataTable:
			case ExtendedClrTypeCode.DbDataReader:
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
			case ExtendedClrTypeCode.TimeSpan:
			case ExtendedClrTypeCode.DateTimeOffset:
				break;
			case ExtendedClrTypeCode.Stream:
				ValueUtilsSmi.SetStream_Unchecked(sink, setters, ordinal, metaData, (StreamDataFeed)value);
				return;
			case ExtendedClrTypeCode.TextReader:
				ValueUtilsSmi.SetTextReader_Unchecked(sink, setters, ordinal, metaData, (TextDataFeed)value);
				return;
			case ExtendedClrTypeCode.XmlReader:
				ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, ((XmlDataFeed)value)._source);
				break;
			default:
				return;
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000684AC File Offset: 0x000666AC
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, ParameterPeekAheadValue peekAhead, SqlBuffer.StorageType storageType)
		{
			if (typeCode != ExtendedClrTypeCode.DateTime)
			{
				ValueUtilsSmi.SetCompatibleValueV200(sink, setters, ordinal, metaData, value, typeCode, offset, peekAhead);
				return;
			}
			if (storageType == SqlBuffer.StorageType.DateTime2)
			{
				ValueUtilsSmi.SetDateTime2_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			if (storageType == SqlBuffer.StorageType.Date)
			{
				ValueUtilsSmi.SetDate_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00068510 File Offset: 0x00066710
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, ParameterPeekAheadValue peekAhead)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.DataTable:
				ValueUtilsSmi.SetDataTable_Unchecked(sink, setters, ordinal, metaData, (DataTable)value);
				return;
			case ExtendedClrTypeCode.DbDataReader:
				ValueUtilsSmi.SetDbDataReader_Unchecked(sink, setters, ordinal, metaData, (DbDataReader)value);
				return;
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
				ValueUtilsSmi.SetIEnumerableOfSqlDataRecord_Unchecked(sink, setters, ordinal, metaData, (IEnumerable<SqlDataRecord>)value, peekAhead);
				return;
			case ExtendedClrTypeCode.TimeSpan:
				ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, ordinal, metaData, (TimeSpan)value);
				return;
			case ExtendedClrTypeCode.DateTimeOffset:
				ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, ordinal, (DateTimeOffset)value);
				return;
			default:
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, value, typeCode, offset);
				return;
			}
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000685A4 File Offset: 0x000667A4
		internal static void FillCompatibleITypedSettersFromReader(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataReader reader)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_02BB;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BB;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_02BB;
					case SqlDbType.Char:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BB;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_02BB;
					case SqlDbType.Decimal:
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, reader.GetSqlDecimal(i));
						goto IL_02BB;
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_02BB;
					case SqlDbType.Image:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BB;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_02BB;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_02BB;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BB;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_02BB;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_02BB;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_02BB;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_02BB;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_02BB;
					case SqlDbType.Text:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BB;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BB;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_02BB;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BB;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_02BB;
					case SqlDbType.Variant:
					{
						object sqlValue = reader.GetSqlValue(i);
						ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
						ValueUtilsSmi.SetCompatibleValue(sink, setters, i, metaData[i], sqlValue, extendedClrTypeCode, 0);
						goto IL_02BB;
					}
					case SqlDbType.Xml:
						ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, reader.GetSqlXml(i));
						goto IL_02BB;
					case SqlDbType.Udt:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_02BB;
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_02BB:;
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0006887C File Offset: 0x00066A7C
		internal static void FillCompatibleSettersFromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, IList<SmiExtendedMetaData> metaData, DbDataReader reader)
		{
			for (int i = 0; i < metaData.Count; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_0429;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_0429;
					case SqlDbType.Char:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_0429;
					case SqlDbType.Decimal:
					{
						SqlDataReader sqlDataReader = reader as SqlDataReader;
						if (sqlDataReader != null)
						{
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, sqlDataReader.GetSqlDecimal(i));
							goto IL_0429;
						}
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, new SqlDecimal(reader.GetDecimal(i)));
						goto IL_0429;
					}
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_0429;
					case SqlDbType.Image:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_0429;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_0429;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_0429;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_0429;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_0429;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_0429;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_0429;
					case SqlDbType.Text:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_0429;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.Variant:
					{
						SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
						SqlDataReader sqlDataReader2 = reader as SqlDataReader;
						object obj;
						if (sqlDataReader2 != null)
						{
							obj = sqlDataReader2.GetSqlValue(i);
							storageType = sqlDataReader2.GetVariantInternalStorageType(i);
						}
						else
						{
							obj = reader.GetValue(i);
						}
						ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(metaData[i].SqlDbType, metaData[i].IsMultiValued, obj, null, 210UL);
						if (storageType == SqlBuffer.StorageType.DateTime2 || storageType == SqlBuffer.StorageType.Date)
						{
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], obj, extendedClrTypeCode, 0, null, storageType);
							goto IL_0429;
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], obj, extendedClrTypeCode, 0, null);
						goto IL_0429;
					}
					case SqlDbType.Xml:
					{
						SqlDataReader sqlDataReader3 = reader as SqlDataReader;
						if (sqlDataReader3 != null)
						{
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, sqlDataReader3.GetSqlXml(i));
							goto IL_0429;
						}
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					}
					case SqlDbType.Udt:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_0429;
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_0429;
					case SqlDbType.Time:
					{
						SqlDataReader sqlDataReader4 = reader as SqlDataReader;
						TimeSpan timeSpan;
						if (sqlDataReader4 != null)
						{
							timeSpan = sqlDataReader4.GetTimeSpan(i);
						}
						else
						{
							timeSpan = (TimeSpan)reader.GetValue(i);
						}
						ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], timeSpan);
						goto IL_0429;
					}
					case SqlDbType.DateTimeOffset:
					{
						SqlDataReader sqlDataReader5 = reader as SqlDataReader;
						DateTimeOffset dateTimeOffset;
						if (sqlDataReader5 != null)
						{
							dateTimeOffset = sqlDataReader5.GetDateTimeOffset(i);
						}
						else
						{
							dateTimeOffset = (DateTimeOffset)reader.GetValue(i);
						}
						ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, dateTimeOffset);
						goto IL_0429;
					}
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_0429:;
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00068CC4 File Offset: 0x00066EC4
		internal static void FillCompatibleSettersFromRecord(SmiEventSink_Default sink, SmiTypedGetterSetter setters, SmiMetaData[] metaData, SqlDataRecord record, SmiDefaultFieldsProperty useDefaultValues)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (useDefaultValues == null || !useDefaultValues[i])
				{
					if (!record.IsDBNull(i))
					{
						switch (metaData[i].SqlDbType)
						{
						case SqlDbType.BigInt:
							ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, record.GetInt64(i));
							goto IL_0329;
						case SqlDbType.Binary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.Bit:
							ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, record.GetBoolean(i));
							goto IL_0329;
						case SqlDbType.Char:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.DateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0329;
						case SqlDbType.Decimal:
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, record.GetSqlDecimal(i));
							goto IL_0329;
						case SqlDbType.Float:
							ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, record.GetDouble(i));
							goto IL_0329;
						case SqlDbType.Image:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.Int:
							ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, record.GetInt32(i));
							goto IL_0329;
						case SqlDbType.Money:
							ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_0329;
						case SqlDbType.NChar:
						case SqlDbType.NText:
						case SqlDbType.NVarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.Real:
							ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, record.GetFloat(i));
							goto IL_0329;
						case SqlDbType.UniqueIdentifier:
							ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, record.GetGuid(i));
							goto IL_0329;
						case SqlDbType.SmallDateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0329;
						case SqlDbType.SmallInt:
							ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, record.GetInt16(i));
							goto IL_0329;
						case SqlDbType.SmallMoney:
							ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_0329;
						case SqlDbType.Text:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.Timestamp:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.TinyInt:
							ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, record.GetByte(i));
							goto IL_0329;
						case SqlDbType.VarBinary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.VarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.Variant:
						{
							object sqlValue = record.GetSqlValue(i);
							ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], sqlValue, extendedClrTypeCode, 0, null);
							goto IL_0329;
						}
						case SqlDbType.Xml:
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, record.GetSqlXml(i));
							goto IL_0329;
						case SqlDbType.Udt:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0329;
						case SqlDbType.Date:
						case SqlDbType.DateTime2:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0329;
						case SqlDbType.Time:
						{
							TimeSpan timeSpan;
							if (record != null)
							{
								timeSpan = record.GetTimeSpan(i);
							}
							else
							{
								timeSpan = (TimeSpan)record.GetValue(i);
							}
							ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], timeSpan);
							goto IL_0329;
						}
						case SqlDbType.DateTimeOffset:
						{
							DateTimeOffset dateTimeOffset;
							if (record != null)
							{
								dateTimeOffset = record.GetDateTimeOffset(i);
							}
							else
							{
								dateTimeOffset = (DateTimeOffset)record.GetValue(i);
							}
							ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, dateTimeOffset);
							goto IL_0329;
						}
						}
						throw ADP.NotSupported();
					}
					ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				}
				IL_0329:;
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00069008 File Offset: 0x00067208
		private static object GetUdt_LengthChecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object obj;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				Type type = metaData.Type;
				obj = type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, Array.Empty<object>(), CultureInfo.InvariantCulture);
			}
			else
			{
				Stream stream = new SmiGettersStream(sink, getters, ordinal, metaData);
				obj = SerializationHelperSql9.Deserialize(stream, metaData.Type);
			}
			return obj;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00069060 File Offset: 0x00067260
		private static decimal GetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (metaData.SqlDbType == SqlDbType.Decimal)
			{
				return ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
			}
			return ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00069097 File Offset: 0x00067297
		private static void SetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			if (metaData.SqlDbType == SqlDbType.Decimal || metaData.SqlDbType == SqlDbType.Variant)
			{
				ValueUtilsSmi.SetDecimal_Unchecked(sink, setters, ordinal, value);
				return;
			}
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, new SqlMoney(value));
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000690C7 File Offset: 0x000672C7
		private static void VerifyDateTimeRange(SqlDbType dbType, DateTime value)
		{
			if (dbType == SqlDbType.SmallDateTime && (ValueUtilsSmi.s_smallDateTimeMax < value || ValueUtilsSmi.s_smallDateTimeMin > value))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000690EE File Offset: 0x000672EE
		private static void VerifyTimeRange(SqlDbType dbType, TimeSpan value)
		{
			if (dbType == SqlDbType.Time && (ValueUtilsSmi.s_timeSpanMin > value || value > ValueUtilsSmi.s_timeSpanMax))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00069115 File Offset: 0x00067315
		private static void SetDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime_Unchecked(sink, setters, ordinal, (metaData.SqlDbType == SqlDbType.Date) ? value.Date : value);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00069141 File Offset: 0x00067341
		private static void SetTimeSpan_Checked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, TimeSpan value)
		{
			ValueUtilsSmi.VerifyTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetTimeSpan_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0006915A File Offset: 0x0006735A
		private static void SetSqlDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			if (!value.IsNull)
			{
				ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value.Value);
			}
			ValueUtilsSmi.SetSqlDateTime_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00069181 File Offset: 0x00067381
		private static void SetDateTime2_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime2_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006919B File Offset: 0x0006739B
		private static void SetDate_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDate_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000691B8 File Offset: 0x000673B8
		private static void SetSqlMoney_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (!value.IsNull && metaData.SqlDbType == SqlDbType.SmallMoney)
			{
				decimal value2 = value.Value;
				if (value2 < TdsEnums.SQL_SMALL_MONEY_MIN || value2 > TdsEnums.SQL_SMALL_MONEY_MAX)
				{
					throw SQL.MoneyOverflow(value2.ToString(CultureInfo.InvariantCulture));
				}
			}
			ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00069218 File Offset: 0x00067418
		private static void SetByteArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte[] buffer, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, buffer, offset, num);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00069258 File Offset: 0x00067458
		private static void SetCharArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, char[] buffer, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetCharArray_Unchecked(sink, setters, ordinal, buffer, offset, num);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00069298 File Offset: 0x00067498
		private static void SetSqlBinary_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, value.Length - offset);
			}
			ValueUtilsSmi.SetSqlBinary_Unchecked(sink, setters, ordinal, value, offset, num);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000692E8 File Offset: 0x000674E8
		private static void SetBytes_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetBytes(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num));
			int num3;
			if (num2 > 8000 || num2 < 0)
			{
				num3 = 8000;
			}
			else
			{
				num3 = num2;
			}
			byte[] array = new byte[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long bytes;
			while ((num2 < 0 || num6 < (long)num2) && (bytes = record.GetBytes(ordinal, num5, array, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetBytes(sink, ordinal, num5, array, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetBytesLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000693B0 File Offset: 0x000675B0
		private static void SetBytes_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2 = 8000;
			byte[] array = new byte[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long bytes;
			while ((num < 0 || num5 < (long)num) && (bytes = reader.GetBytes(ordinal, num4, array, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetBytes(sink, ordinal, num4, array, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetBytesLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00069448 File Offset: 0x00067648
		private static void SetSqlBytes_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				long num2 = value.Length;
				if (num2 > 2147483647L)
				{
					num2 = -1L;
				}
				num = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num2, offset, (int)num2));
			}
			ValueUtilsSmi.SetSqlBytes_Unchecked(sink, setters, ordinal, value, 0, (long)num);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000694A0 File Offset: 0x000676A0
		private static void SetChars_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetChars(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num - offset));
			int num3;
			if (num2 > 4000 || num2 < 0)
			{
				if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
				{
					num3 = 8000;
				}
				else
				{
					num3 = 4000;
				}
			}
			else
			{
				num3 = num2;
			}
			char[] array = new char[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long chars;
			while ((num2 < 0 || num6 < (long)num2) && (chars = record.GetChars(ordinal, num5, array, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetChars(sink, ordinal, num5, array, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetCharsLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00069580 File Offset: 0x00067780
		private static void SetCharsOrString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			bool flag = false;
			try
			{
				ValueUtilsSmi.SetChars_FromReader(sink, setters, ordinal, metaData, reader, offset);
				flag = true;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
			}
			if (!flag)
			{
				ValueUtilsSmi.SetString_FromReader(sink, setters, ordinal, metaData, reader, offset);
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000695D0 File Offset: 0x000677D0
		private static void SetChars_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2;
			if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
			{
				num2 = 8000;
			}
			else
			{
				num2 = 4000;
			}
			char[] array = new char[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long chars;
			while ((num < 0 || num5 < (long)num) && (chars = reader.GetChars(ordinal, num4, array, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetChars(sink, ordinal, num4, array, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetCharsLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0006967C File Offset: 0x0006787C
		private static void SetString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			string @string = reader.GetString(ordinal);
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, (long)@string.Length, 0L, -1, offset, -1);
			setters.SetString(sink, ordinal, @string, offset, num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000696C4 File Offset: 0x000678C4
		private static void SetSqlChars_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				long num2 = value.Length;
				if (num2 > 2147483647L)
				{
					num2 = -1L;
				}
				num = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num2, offset, (int)num2 - offset));
			}
			ValueUtilsSmi.SetSqlChars_Unchecked(sink, setters, ordinal, value, 0, num);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0006971C File Offset: 0x0006791C
		private static void SetSqlString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset)
		{
			if (value.IsNull)
			{
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			}
			string value2 = value.Value;
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value2.Length, offset, value2.Length - offset);
			ValueUtilsSmi.SetSqlString_Unchecked(sink, setters, ordinal, metaData, value, offset, num);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00069778 File Offset: 0x00067978
		private static void SetString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, checked(value.Length - offset));
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value, offset, num);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000697BC File Offset: 0x000679BC
		private static void SetUdt_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value)
		{
			if (ADP.IsNull(value))
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			Stream stream = new SmiSettersStream(sink, setters, ordinal, metaData);
			SerializationHelperSql9.Serialize(stream, value);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000697F3 File Offset: 0x000679F3
		private static void ThrowIfInvalidSetterAccess(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			if (!ValueUtilsSmi.CanAccessSetterDirectly(metaData, setterTypeCode))
			{
				throw ADP.InvalidCast();
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00069804 File Offset: 0x00067A04
		private static void ThrowIfITypedGettersIsNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				throw SQL.SqlNullValue();
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00069818 File Offset: 0x00067A18
		private static bool CanAccessGetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.s_canAccessGetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (setterTypeCode == ExtendedClrTypeCode.DataTable || setterTypeCode == ExtendedClrTypeCode.DbDataReader || setterTypeCode == ExtendedClrTypeCode.IEnumerableOfSqlDataRecord))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00069854 File Offset: 0x00067A54
		private static bool CanAccessSetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.s_canAccessSetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (setterTypeCode == ExtendedClrTypeCode.DataTable || setterTypeCode == ExtendedClrTypeCode.DbDataReader || setterTypeCode == ExtendedClrTypeCode.IEnumerableOfSqlDataRecord))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0006988D File Offset: 0x00067A8D
		private static long PositiveMin(long first, long second)
		{
			if (first < 0L)
			{
				return second;
			}
			if (second < 0L)
			{
				return first;
			}
			return Math.Min(first, second);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000698A4 File Offset: 0x00067AA4
		private static int CheckXetParameters(SqlDbType dbType, long maxLength, long actualLength, long fieldOffset, int bufferLength, int bufferOffset, int length)
		{
			if (fieldOffset < 0L)
			{
				throw ADP.NegativeParameter("fieldOffset");
			}
			if (bufferOffset < 0)
			{
				throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
			}
			checked
			{
				if (bufferLength < 0)
				{
					length = (int)ValueUtilsSmi.PositiveMin(unchecked((long)length), ValueUtilsSmi.PositiveMin(maxLength, actualLength));
					if (length < -1)
					{
						length = -1;
					}
					return length;
				}
				if (bufferOffset > bufferLength)
				{
					throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
				}
				if (length + bufferOffset > bufferLength)
				{
					throw ADP.InvalidBufferSizeOrIndex(length, bufferOffset);
				}
			}
			if (length < 0)
			{
				throw ADP.InvalidDataLength((long)length);
			}
			if (actualLength >= 0L && actualLength <= fieldOffset)
			{
				return 0;
			}
			length = Math.Min(length, bufferLength - bufferOffset);
			if (dbType == SqlDbType.Variant)
			{
				length = Math.Min(length, 8000);
			}
			if (actualLength >= 0L)
			{
				length = (int)Math.Min((long)length, actualLength - fieldOffset);
			}
			else if (dbType != SqlDbType.Udt && maxLength >= 0L)
			{
				length = (int)Math.Min((long)length, maxLength - fieldOffset);
			}
			if (length < 0)
			{
				return 0;
			}
			return length;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00069990 File Offset: 0x00067B90
		private static bool IsDBNull_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool flag = getters.IsDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return flag;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000699B0 File Offset: 0x00067BB0
		private static bool GetBoolean_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool boolean = getters.GetBoolean(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return boolean;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000699D0 File Offset: 0x00067BD0
		private static byte GetByte_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			byte @byte = getters.GetByte(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @byte;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000699F0 File Offset: 0x00067BF0
		private static byte[] GetByteArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long bytesLength = getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = checked((int)bytesLength);
			byte[] array = new byte[num];
			getters.GetBytes(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00069A2C File Offset: 0x00067C2C
		internal static int GetBytes_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int bytes = getters.GetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return bytes;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00069A50 File Offset: 0x00067C50
		private static long GetBytesLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long bytesLength = getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return bytesLength;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00069A70 File Offset: 0x00067C70
		private static char[] GetCharArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long charsLength = getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = checked((int)charsLength);
			char[] array = new char[num];
			getters.GetChars(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00069AAC File Offset: 0x00067CAC
		internal static int GetChars_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int chars = getters.GetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return chars;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00069AD0 File Offset: 0x00067CD0
		private static long GetCharsLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long charsLength = getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return charsLength;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00069AF0 File Offset: 0x00067CF0
		private static DateTime GetDateTime_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			DateTime dateTime = getters.GetDateTime(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTime;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00069B10 File Offset: 0x00067D10
		private static DateTimeOffset GetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			DateTimeOffset dateTimeOffset = getters.GetDateTimeOffset(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTimeOffset;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00069B30 File Offset: 0x00067D30
		private static double GetDouble_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			double @double = getters.GetDouble(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @double;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00069B50 File Offset: 0x00067D50
		private static Guid GetGuid_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			Guid guid = getters.GetGuid(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return guid;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00069B70 File Offset: 0x00067D70
		private static short GetInt16_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			short @int = getters.GetInt16(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00069B90 File Offset: 0x00067D90
		private static int GetInt32_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int @int = getters.GetInt32(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00069BB0 File Offset: 0x00067DB0
		private static long GetInt64_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00069BD0 File Offset: 0x00067DD0
		private static float GetSingle_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			float single = getters.GetSingle(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return single;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00069BF0 File Offset: 0x00067DF0
		private static SqlBinary GetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			byte[] byteArray_Unchecked = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
			return new SqlBinary(byteArray_Unchecked);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00069C0C File Offset: 0x00067E0C
		private static SqlDecimal GetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			SqlDecimal sqlDecimal = getters.GetSqlDecimal(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return sqlDecimal;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00069C2C File Offset: 0x00067E2C
		private static SqlMoney GetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return SqlTypeWorkarounds.SqlMoneyCtor(@int, 1);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00069C50 File Offset: 0x00067E50
		private static SqlXml GetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiContext context)
		{
			if (context == null && InOutOfProcHelper.InProc)
			{
				context = SmiContextFactory.Instance.GetCurrentContext();
			}
			Stream stream = new SmiGettersStream(sink, getters, ordinal, SmiMetaData.DefaultXml);
			Stream stream2 = ValueUtilsSmi.CopyIntoNewSmiScratchStream(stream, sink, context);
			return new SqlXml(stream2);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00069C94 File Offset: 0x00067E94
		private static string GetString_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			string @string = getters.GetString(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @string;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00069CB4 File Offset: 0x00067EB4
		private static TimeSpan GetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			TimeSpan timeSpan = getters.GetTimeSpan(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return timeSpan;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00069CD1 File Offset: 0x00067ED1
		private static void SetBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			setters.SetBoolean(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00069CE2 File Offset: 0x00067EE2
		private static void SetByteArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetBytes(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetBytesLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00069D10 File Offset: 0x00067F10
		private static void SetStream_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, StreamDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			byte[] array = new byte[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(array, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetBytes(sink, ordinal, (long)num, array, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetBytesLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00069D94 File Offset: 0x00067F94
		private static void SetTextReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, TextDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			char[] array = new char[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(array, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetChars(sink, ordinal, (long)num, array, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetCharsLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00069E16 File Offset: 0x00068016
		private static void SetByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte value)
		{
			setters.SetByte(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00069E28 File Offset: 0x00068028
		private static int SetBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = setters.SetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00069E4C File Offset: 0x0006804C
		private static void SetCharArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, char[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetChars(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetCharsLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00069E7C File Offset: 0x0006807C
		private static int SetChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = setters.SetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return num;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00069EA0 File Offset: 0x000680A0
		private static void SetDBNull_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal)
		{
			setters.SetDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00069EB0 File Offset: 0x000680B0
		private static void SetDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, decimal value)
		{
			setters.SetSqlDecimal(sink, ordinal, new SqlDecimal(value));
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00069EC6 File Offset: 0x000680C6
		private static void SetDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, DateTime value)
		{
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00069ED7 File Offset: 0x000680D7
		private static void SetDateTime2_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDateTime2);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00069EF6 File Offset: 0x000680F6
		private static void SetDate_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDate);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00069F15 File Offset: 0x00068115
		private static void SetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, TimeSpan value)
		{
			setters.SetTimeSpan(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00069F26 File Offset: 0x00068126
		private static void SetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, DateTimeOffset value)
		{
			setters.SetDateTimeOffset(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00069F37 File Offset: 0x00068137
		private static void SetDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, double value)
		{
			setters.SetDouble(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00069F48 File Offset: 0x00068148
		private static void SetGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, Guid value)
		{
			setters.SetGuid(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00069F59 File Offset: 0x00068159
		private static void SetInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, short value)
		{
			setters.SetInt16(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00069F6A File Offset: 0x0006816A
		private static void SetInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, int value)
		{
			setters.SetInt32(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00069F7B File Offset: 0x0006817B
		private static void SetInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long value)
		{
			setters.SetInt64(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00069F8C File Offset: 0x0006818C
		private static void SetSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, float value)
		{
			setters.SetSingle(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00069F9D File Offset: 0x0006819D
		private static void SetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBinary value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, value.Value, offset, length);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00069FCB File Offset: 0x000681CB
		private static void SetSqlBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBoolean value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetBoolean(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00069FF5 File Offset: 0x000681F5
		private static void SetSqlByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlByte value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetByte(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0006A020 File Offset: 0x00068220
		private static void SetSqlBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBytes value, int offset, long length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 8000L || length < 0L)
			{
				num = 8000;
			}
			else
			{
				num = checked((int)length);
			}
			byte[] array = new byte[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0L || num4 < length) && (num5 = value.Read(num3, array, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetBytes(sink, ordinal, num3, array, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetBytesLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0006A0C8 File Offset: 0x000682C8
		private static void SetSqlChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlChars value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 4000 || length < 0)
			{
				num = 4000;
			}
			else
			{
				num = length;
			}
			char[] array = new char[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0 || num4 < (long)length) && (num5 = value.Read(num3, array, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetChars(sink, ordinal, num3, array, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetCharsLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0006A16A File Offset: 0x0006836A
		private static void SetSqlDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDateTime value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDateTime(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0006A194 File Offset: 0x00068394
		private static void SetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDecimal value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSqlDecimal(sink, ordinal, value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0006A1B8 File Offset: 0x000683B8
		private static void SetSqlDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDouble value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDouble(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0006A1E2 File Offset: 0x000683E2
		private static void SetSqlGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlGuid value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetGuid(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0006A20C File Offset: 0x0006840C
		private static void SetSqlInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt16 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt16(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0006A236 File Offset: 0x00068436
		private static void SetSqlInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt32 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt32(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0006A260 File Offset: 0x00068460
		private static void SetSqlInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt64 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt64(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0006A28C File Offset: 0x0006848C
		private static void SetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				if (metaData.SqlDbType == SqlDbType.Variant)
				{
					setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultMoney);
					sink.ProcessMessagesAndThrow();
				}
				setters.SetInt64(sink, ordinal, SqlTypeWorkarounds.SqlMoneyToSqlInternalRepresentation(value));
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0006A2DE File Offset: 0x000684DE
		private static void SetSqlSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlSingle value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSingle(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0006A308 File Offset: 0x00068508
		private static void SetSqlString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			if (metaData.SqlDbType == SqlDbType.Variant)
			{
				metaData = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, (long)value.LCID, value.SqlCompareOptions, null);
				setters.SetVariantMetaData(sink, ordinal, metaData);
				sink.ProcessMessagesAndThrow();
			}
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value.Value, offset, length);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006A37A File Offset: 0x0006857A
		private static void SetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlXml value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, value.CreateReader());
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0006A3A4 File Offset: 0x000685A4
		private static void SetXmlReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, XmlReader xmlReader)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				CloseOutput = false,
				ConformanceLevel = ConformanceLevel.Fragment,
				Encoding = Encoding.Unicode,
				OmitXmlDeclaration = true
			};
			using (Stream stream = new SmiSettersStream(sink, setters, ordinal, SmiMetaData.DefaultXml))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
				{
					xmlReader.Read();
					while (!xmlReader.EOF)
					{
						xmlWriter.WriteNode(xmlReader, true);
					}
					xmlWriter.Flush();
				}
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0006A444 File Offset: 0x00068644
		private static void SetString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, string value, int offset, int length)
		{
			setters.SetString(sink, ordinal, value, offset, length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0006A45C File Offset: 0x0006865C
		private static void SetDbDataReader_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			while (value.Read())
			{
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				ValueUtilsSmi.FillCompatibleSettersFromReader(sink, setters, metaData.FieldMetaData, value);
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0006A4B0 File Offset: 0x000686B0
		private static void SetIEnumerableOfSqlDataRecord_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, IEnumerable<SqlDataRecord> value, ParameterPeekAheadValue peekAhead)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			IEnumerator<SqlDataRecord> enumerator = null;
			try
			{
				SmiExtendedMetaData[] array = new SmiExtendedMetaData[metaData.FieldMetaData.Count];
				metaData.FieldMetaData.CopyTo(array, 0);
				SmiDefaultFieldsProperty smiDefaultFieldsProperty = (SmiDefaultFieldsProperty)metaData.ExtendedProperties[SmiPropertySelector.DefaultFields];
				int num = 1;
				if (peekAhead != null && peekAhead.FirstRecord != null)
				{
					enumerator = peekAhead.Enumerator;
					setters.NewElement(sink);
					sink.ProcessMessagesAndThrow();
					SmiTypedGetterSetter smiTypedGetterSetter = setters;
					SmiMetaData[] array2 = array;
					ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, smiTypedGetterSetter, array2, peekAhead.FirstRecord, smiDefaultFieldsProperty);
					num++;
				}
				else
				{
					enumerator = value.GetEnumerator();
				}
				while (enumerator.MoveNext())
				{
					setters.NewElement(sink);
					sink.ProcessMessagesAndThrow();
					SqlDataRecord sqlDataRecord = enumerator.Current;
					if (sqlDataRecord.FieldCount != array.Length)
					{
						throw SQL.EnumeratedRecordFieldCountChanged(num);
					}
					for (int i = 0; i < sqlDataRecord.FieldCount; i++)
					{
						if (!MetaDataUtilsSmi.IsCompatible(metaData.FieldMetaData[i], sqlDataRecord.GetSqlMetaData(i)))
						{
							throw SQL.EnumeratedRecordMetaDataChanged(sqlDataRecord.GetName(i), num);
						}
					}
					SmiTypedGetterSetter smiTypedGetterSetter2 = setters;
					SmiMetaData[] array2 = array;
					ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, smiTypedGetterSetter2, array2, sqlDataRecord, smiDefaultFieldsProperty);
					num++;
				}
				setters.EndElements(sink);
				sink.ProcessMessagesAndThrow();
			}
			finally
			{
				IDisposable disposable = enumerator;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0006A610 File Offset: 0x00068810
		private static void SetDataTable_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DataTable value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[metaData.FieldMetaData.Count];
			for (int i = 0; i < metaData.FieldMetaData.Count; i++)
			{
				array[i] = ExtendedClrTypeCode.Invalid;
			}
			foreach (object obj in value.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				for (int j = 0; j < metaData.FieldMetaData.Count; j++)
				{
					SmiMetaData smiMetaData = metaData.FieldMetaData[j];
					if (dataRow.IsNull(j))
					{
						ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, j);
					}
					else
					{
						object obj2 = dataRow[j];
						if (ExtendedClrTypeCode.Invalid == array[j])
						{
							array[j] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(smiMetaData.SqlDbType, smiMetaData.IsMultiValued, obj2, smiMetaData.Type, 210UL);
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, j, smiMetaData, obj2, array[j], 0, null);
					}
				}
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0006A74C File Offset: 0x0006894C
		internal static Stream CopyIntoNewSmiScratchStream(Stream source, SmiEventSink_Default sink, SmiContext context)
		{
			Stream stream = null;
			if (context != null)
			{
				stream = new SqlClientWrapperSmiStream(sink, context.GetScratchStream(sink));
			}
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			int num;
			if (source.CanSeek && source.Length > 8000L)
			{
				num = (int)source.Length;
			}
			else
			{
				num = 8000;
			}
			byte[] array = new byte[num];
			int num2;
			while ((num2 = source.Read(array, 0, num)) != 0)
			{
				stream.Write(array, 0, num2);
			}
			stream.Flush();
			stream.Seek(0L, SeekOrigin.Begin);
			return stream;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006A7CC File Offset: 0x000689CC
		internal static SqlSequentialStreamSmi GetSequentialStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool bypassTypeCheck = false)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (!bypassTypeCheck && !ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Stream))
			{
				throw ADP.InvalidCast();
			}
			long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
			return new SqlSequentialStreamSmi(sink, getters, ordinal, bytesLength_Unchecked);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0006A808 File Offset: 0x00068A08
		internal static SqlSequentialTextReaderSmi GetSequentialTextReader(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (!ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TextReader))
			{
				throw ADP.InvalidCast();
			}
			long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
			return new SqlSequentialTextReaderSmi(sink, getters, ordinal, charsLength_Unchecked);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0006A840 File Offset: 0x00068A40
		internal static Stream GetStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool bypassTypeCheck = false)
		{
			bool flag = ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
			if (!bypassTypeCheck)
			{
				if (!flag && metaData.SqlDbType == SqlDbType.Variant)
				{
					metaData = getters.GetVariantType(sink, ordinal);
				}
				if (metaData.SqlDbType != SqlDbType.Variant && !ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Stream))
				{
					throw ADP.InvalidCast();
				}
			}
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				array = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
			}
			return new MemoryStream(array, false);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0006A8A8 File Offset: 0x00068AA8
		internal static TextReader GetTextReader(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			bool flag = ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
			if (!flag && metaData.SqlDbType == SqlDbType.Variant)
			{
				metaData = getters.GetVariantType(sink, ordinal);
			}
			if (metaData.SqlDbType != SqlDbType.Variant && !ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TextReader))
			{
				throw ADP.InvalidCast();
			}
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				text = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
			}
			return new StringReader(text);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0006A90C File Offset: 0x00068B0C
		internal static TimeSpan GetTimeSpan(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool gettersSupport2008DateTime)
		{
			if (gettersSupport2008DateTime)
			{
				return ValueUtilsSmi.GetTimeSpan(sink, (SmiTypedGetterSetter)getters, ordinal, metaData);
			}
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (TimeSpan)value;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0006A950 File Offset: 0x00068B50
		internal static SqlBuffer.StorageType SqlDbTypeToStorageType(SqlDbType dbType)
		{
			return ValueUtilsSmi.s_dbTypeToStorageType[(int)dbType];
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0006A968 File Offset: 0x00068B68
		private static void GetNullOutputParameterSmi(SmiMetaData metaData, SqlBuffer targetBuffer, ref object result)
		{
			if (SqlDbType.Udt == metaData.SqlDbType)
			{
				result = ValueUtilsSmi.NullUdtInstance(metaData);
				return;
			}
			SqlBuffer.StorageType storageType = ValueUtilsSmi.SqlDbTypeToStorageType(metaData.SqlDbType);
			if (storageType == SqlBuffer.StorageType.Empty)
			{
				result = DBNull.Value;
				return;
			}
			if (SqlBuffer.StorageType.SqlBinary == storageType)
			{
				targetBuffer.SqlBinary = SqlBinary.Null;
				return;
			}
			if (SqlBuffer.StorageType.SqlGuid == storageType)
			{
				targetBuffer.SqlGuid = SqlGuid.Null;
				return;
			}
			targetBuffer.SetToNullOfType(storageType);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0006A9C8 File Offset: 0x00068BC8
		internal static object GetOutputParameterV3Smi(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context, SqlBuffer targetBuffer)
		{
			object obj = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				ValueUtilsSmi.GetNullOutputParameterSmi(metaData, targetBuffer, ref obj);
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					targetBuffer.Int64 = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Binary:
				case SqlDbType.Image:
				case SqlDbType.Timestamp:
				case SqlDbType.VarBinary:
					targetBuffer.SqlBinary = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					targetBuffer.Boolean = ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Char:
				case SqlDbType.NChar:
				case SqlDbType.NText:
				case SqlDbType.NVarChar:
				case SqlDbType.Text:
				case SqlDbType.VarChar:
					targetBuffer.SetToString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.DateTime:
				case SqlDbType.SmallDateTime:
				{
					SqlDateTime sqlDateTime = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					targetBuffer.SetToDateTime(sqlDateTime.DayTicks, sqlDateTime.TimeTicks);
					break;
				}
				case SqlDbType.Decimal:
				{
					SqlDecimal sqlDecimal_Unchecked = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
					targetBuffer.SetToDecimal(sqlDecimal_Unchecked.Precision, sqlDecimal_Unchecked.Scale, sqlDecimal_Unchecked.IsPositive, sqlDecimal_Unchecked.Data);
					break;
				}
				case SqlDbType.Float:
					targetBuffer.Double = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					targetBuffer.Int32 = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Money:
				case SqlDbType.SmallMoney:
					targetBuffer.SetToMoney(ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Real:
					targetBuffer.Single = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.UniqueIdentifier:
					targetBuffer.SqlGuid = new SqlGuid(ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallInt:
					targetBuffer.Int16 = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					targetBuffer.Byte = ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					ValueUtilsSmi.GetOutputParameterV3Smi(sink, getters, ordinal, metaData, context, targetBuffer);
					break;
				case SqlDbType.Xml:
					targetBuffer.SqlXml = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, null);
					break;
				case SqlDbType.Udt:
					obj = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return obj;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0006ABE8 File Offset: 0x00068DE8
		internal static object GetOutputParameterV200Smi(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData, SmiContext context, SqlBuffer targetBuffer)
		{
			object obj = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				ValueUtilsSmi.GetNullOutputParameterSmi(metaData, targetBuffer, ref obj);
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
						targetBuffer.SetToDate(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
						break;
					case SqlDbType.Time:
						targetBuffer.SetToTime(ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal), metaData.Scale);
						break;
					case SqlDbType.DateTime2:
						targetBuffer.SetToDateTime2(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal), metaData.Scale);
						break;
					case SqlDbType.DateTimeOffset:
						targetBuffer.SetToDateTimeOffset(ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal), metaData.Scale);
						break;
					default:
						obj = ValueUtilsSmi.GetOutputParameterV3Smi(sink, getters, ordinal, metaData, context, targetBuffer);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					ValueUtilsSmi.GetOutputParameterV200Smi(sink, getters, ordinal, metaData, context, targetBuffer);
				}
			}
			return obj;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0006ACBC File Offset: 0x00068EBC
		internal static void FillCompatibleITypedSettersFromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataRecord record)
		{
			ValueUtilsSmi.FillCompatibleITypedSettersFromRecord(sink, setters, metaData, record, null);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0006ACC8 File Offset: 0x00068EC8
		internal static void FillCompatibleITypedSettersFromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataRecord record, SmiDefaultFieldsProperty useDefaultValues)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (useDefaultValues == null || !useDefaultValues[i])
				{
					if (!record.IsDBNull(i))
					{
						switch (metaData[i].SqlDbType)
						{
						case SqlDbType.BigInt:
							ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, record.GetInt64(i));
							goto IL_0293;
						case SqlDbType.Binary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.Bit:
							ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, record.GetBoolean(i));
							goto IL_0293;
						case SqlDbType.Char:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.DateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0293;
						case SqlDbType.Decimal:
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, record.GetSqlDecimal(i));
							goto IL_0293;
						case SqlDbType.Float:
							ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, record.GetDouble(i));
							goto IL_0293;
						case SqlDbType.Image:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.Int:
							ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, record.GetInt32(i));
							goto IL_0293;
						case SqlDbType.Money:
							ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_0293;
						case SqlDbType.NChar:
						case SqlDbType.NText:
						case SqlDbType.NVarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.Real:
							ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, record.GetFloat(i));
							goto IL_0293;
						case SqlDbType.UniqueIdentifier:
							ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, record.GetGuid(i));
							goto IL_0293;
						case SqlDbType.SmallDateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_0293;
						case SqlDbType.SmallInt:
							ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, record.GetInt16(i));
							goto IL_0293;
						case SqlDbType.SmallMoney:
							ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_0293;
						case SqlDbType.Text:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.Timestamp:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.TinyInt:
							ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, record.GetByte(i));
							goto IL_0293;
						case SqlDbType.VarBinary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.VarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						case SqlDbType.Variant:
						{
							object sqlValue = record.GetSqlValue(i);
							ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
							ValueUtilsSmi.SetCompatibleValue(sink, setters, i, metaData[i], sqlValue, extendedClrTypeCode, 0);
							goto IL_0293;
						}
						case SqlDbType.Xml:
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, record.GetSqlXml(i));
							goto IL_0293;
						case SqlDbType.Udt:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_0293;
						}
						throw ADP.NotSupported();
					}
					ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				}
				IL_0293:;
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006AF78 File Offset: 0x00069178
		internal static SqlStreamChars CopyIntoNewSmiScratchStreamChars(Stream source, SmiEventSink_Default sink, SmiContext context)
		{
			SqlClientWrapperSmiStreamChars sqlClientWrapperSmiStreamChars = new SqlClientWrapperSmiStreamChars(sink, context.GetScratchStream(sink));
			int num;
			if (source.CanSeek && source.Length < 8000L)
			{
				num = (int)source.Length;
			}
			else
			{
				num = 8000;
			}
			byte[] array = new byte[num];
			int num2;
			while ((num2 = source.Read(array, 0, num)) != 0)
			{
				sqlClientWrapperSmiStreamChars.Write(array, 0, num2);
			}
			sqlClientWrapperSmiStreamChars.Flush();
			sqlClientWrapperSmiStreamChars.Seek(0L, SeekOrigin.Begin);
			return sqlClientWrapperSmiStreamChars;
		}

		// Token: 0x040009BE RID: 2494
		private const int MaxByteChunkSize = 8000;

		// Token: 0x040009BF RID: 2495
		private const int MaxCharChunkSize = 4000;

		// Token: 0x040009C0 RID: 2496
		private const int NoLengthLimit = -1;

		// Token: 0x040009C1 RID: 2497
		private const int DefaultBinaryBufferSize = 4096;

		// Token: 0x040009C2 RID: 2498
		private const int DefaultTextBufferSize = 4096;

		// Token: 0x040009C3 RID: 2499
		private static readonly object[] s_typeSpecificNullForSqlValue = new object[]
		{
			SqlInt64.Null,
			SqlBinary.Null,
			SqlBoolean.Null,
			SqlString.Null,
			SqlDateTime.Null,
			SqlDecimal.Null,
			SqlDouble.Null,
			SqlBinary.Null,
			SqlInt32.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlString.Null,
			SqlString.Null,
			SqlSingle.Null,
			SqlGuid.Null,
			SqlDateTime.Null,
			SqlInt16.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlBinary.Null,
			SqlByte.Null,
			SqlBinary.Null,
			SqlString.Null,
			DBNull.Value,
			null,
			SqlXml.Null,
			null,
			null,
			null,
			null,
			null,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x040009C4 RID: 2500
		private static readonly DateTime s_smallDateTimeMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x040009C5 RID: 2501
		private static readonly DateTime s_smallDateTimeMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x040009C6 RID: 2502
		private static readonly TimeSpan s_timeSpanMin = TimeSpan.Zero;

		// Token: 0x040009C7 RID: 2503
		private static readonly TimeSpan s_timeSpanMax = new TimeSpan(863999999999L);

		// Token: 0x040009C8 RID: 2504
		private const bool X = true;

		// Token: 0x040009C9 RID: 2505
		private const bool _ = false;

		// Token: 0x040009CA RID: 2506
		private static readonly bool[,] s_canAccessGetterDirectly = new bool[,]
		{
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, true, false, true, false, false, false, true, false, false,
				true, true, true, false, false, false, false, false, true, true,
				false, true, true, false, false, true, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			}
		};

		// Token: 0x040009CB RID: 2507
		private static readonly bool[,] s_canAccessSetterDirectly = new bool[,]
		{
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, true, false, true, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, true, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, true, false, true, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, true, false, false, false, false, false, false,
				true, true, true, false, false, false, false, false, true, false,
				false, false, true, true, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false, false, false, true,
				false, true, false, true, false, false, false, false, false, true,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false
			}
		};

		// Token: 0x040009CC RID: 2508
		private static readonly SqlBuffer.StorageType[] s_dbTypeToStorageType = new SqlBuffer.StorageType[]
		{
			SqlBuffer.StorageType.Int64,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.Boolean,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.DateTime,
			SqlBuffer.StorageType.Decimal,
			SqlBuffer.StorageType.Double,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.Int32,
			SqlBuffer.StorageType.Money,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.Single,
			SqlBuffer.StorageType.SqlGuid,
			SqlBuffer.StorageType.DateTime,
			SqlBuffer.StorageType.Int16,
			SqlBuffer.StorageType.Money,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.Byte,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.SqlXml,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Date,
			SqlBuffer.StorageType.Time,
			SqlBuffer.StorageType.DateTime2,
			SqlBuffer.StorageType.DateTimeOffset
		};
	}
}
