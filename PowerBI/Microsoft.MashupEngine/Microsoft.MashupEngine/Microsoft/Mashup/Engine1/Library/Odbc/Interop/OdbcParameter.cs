using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x02000716 RID: 1814
	internal class OdbcParameter
	{
		// Token: 0x0600361E RID: 13854 RVA: 0x000AC5FD File Offset: 0x000AA7FD
		public OdbcParameter(object value, OdbcTypeMap bindType)
		{
			this.value = value;
			this.bindType = bindType;
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000AC613 File Offset: 0x000AA813
		public OdbcTypeMap BindType
		{
			get
			{
				return this.bindType;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000AC61B File Offset: 0x000AA81B
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000AC624 File Offset: 0x000AA824
		public int GetColumnSize()
		{
			int num = this.bindType.ColumnSize;
			if (num <= 0)
			{
				if (Odbc32.SQL_C.NUMERIC == this.bindType.CType)
				{
					num = 62;
				}
				else
				{
					if (OdbcUtils.IsNull(this.value))
					{
						num = 0;
					}
					else if (this.value is string)
					{
						num = ((string)this.value).Length;
					}
					else if (this.value is char[])
					{
						num = ((char[])this.value).Length;
					}
					else
					{
						if (!(this.value is byte[]))
						{
							throw new InvalidOperationException(this.value.GetType().FullName);
						}
						num = ((byte[])this.value).Length;
					}
					num = Math.Max(2, num);
				}
			}
			return num;
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000AC6E4 File Offset: 0x000AA8E4
		public int GetValueSize()
		{
			int num = this.bindType.ColumnSize;
			if (num <= 0)
			{
				bool flag = false;
				if (this.value is string)
				{
					num = ((string)this.value).Length;
					flag = true;
				}
				else if (this.value is char[])
				{
					num = ((char[])this.value).Length;
					flag = true;
				}
				else if (this.value is byte[])
				{
					num = ((byte[])this.value).Length;
				}
				else
				{
					num = 0;
				}
				if (flag)
				{
					num *= 2;
				}
			}
			return num;
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000AC76C File Offset: 0x000AA96C
		public int GetParameterSize()
		{
			int num = this.bindType.BufferSize;
			if (num <= 0)
			{
				if (Odbc32.SQL_C.NUMERIC == this.bindType.CType)
				{
					num = 518;
				}
				else if (OdbcUtils.IsNull(this.value))
				{
					if (this.bindType.CType == Odbc32.SQL_C.WCHAR)
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
				}
				else if (this.value is string)
				{
					num = ((string)this.value).Length * 2 + 2;
				}
				else if (this.value is char[])
				{
					num = ((char[])this.value).Length * 2 + 2;
				}
				else
				{
					if (!(this.value is byte[]))
					{
						throw new InvalidOperationException(this.value.GetType().FullName);
					}
					num = ((byte[])this.value).Length;
				}
			}
			return (num + (IntPtr.Size - 1)) & ~(IntPtr.Size - 1);
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000AC858 File Offset: 0x000AAA58
		public void PrepareForBind(ref int parameterBufferSize)
		{
			if (this.bindType.CType == Odbc32.SQL_C.CHAR)
			{
				this.value = Encoding.ASCII.GetBytes((string)this.value);
			}
			int parameterSize = this.GetParameterSize();
			this.size = 0;
			this.bufferSize = parameterSize;
			this.intOffset = parameterBufferSize;
			this.valueOffset = this.intOffset + IntPtr.Size;
			parameterBufferSize += parameterSize + IntPtr.Size;
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000AC8CC File Offset: 0x000AAACC
		public void Bind(OdbcStatementHandle hstmt, short ordinal, OdbcBuffer parameterBuffer)
		{
			Odbc32.SQL_C ctype = this.bindType.CType;
			int num = this.size;
			object obj = this.value;
			int valueSize = this.GetValueSize();
			int columnSize = this.GetColumnSize();
			byte parameterPrecision = OdbcParameter.GetParameterPrecision(obj);
			byte parameterScale = OdbcParameter.GetParameterScale(obj);
			HandleRef handleRef = parameterBuffer.PtrOffset(this.valueOffset, this.bufferSize);
			HandleRef handleRef2 = parameterBuffer.PtrOffset(this.intOffset, IntPtr.Size);
			if (Odbc32.SQL_C.NUMERIC == ctype)
			{
				this.SetInputValue(obj, ctype, valueSize, (int)parameterPrecision, 0, parameterBuffer);
			}
			else
			{
				this.SetInputValue(obj, ctype, valueSize, num, 0, parameterBuffer);
			}
			OdbcUtils.HandleError(hstmt, hstmt.BindParameter(ordinal, 1, ctype, this.bindType.SqlType, checked((ulong)columnSize), (long)((ulong)parameterScale), handleRef, (long)this.bufferSize, handleRef2));
			if (Odbc32.SQL_C.NUMERIC == ctype)
			{
				OdbcDescriptorHandle descriptorHandle = hstmt.GetDescriptorHandle(Odbc32.SQL_ATTR.APP_PARAM_DESC);
				OdbcUtils.HandleError(hstmt, descriptorHandle.SetDescriptionField(ordinal, Odbc32.SQL_DESC.TYPE, (IntPtr)2));
				OdbcUtils.HandleError(hstmt, descriptorHandle.SetDescriptionField(ordinal, Odbc32.SQL_DESC.PRECISION, (IntPtr)((int)parameterPrecision)));
				OdbcUtils.HandleError(hstmt, descriptorHandle.SetDescriptionField(ordinal, Odbc32.SQL_DESC.SCALE, (IntPtr)((int)parameterScale)));
				OdbcUtils.HandleError(hstmt, descriptorHandle.SetDescriptionField(ordinal, Odbc32.SQL_DESC.DATA_PTR, handleRef));
			}
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000AC9FC File Offset: 0x000AABFC
		private void SetInputValue(object value, Odbc32.SQL_C cType, int cbSize, int sizeOrPrecision, int offset, OdbcBuffer parameterBuffer)
		{
			if (OdbcUtils.IsNull(value))
			{
				parameterBuffer.WriteIntPtr(this.intOffset, (IntPtr)(-1));
				return;
			}
			if (cType == Odbc32.SQL_C.WCHAR || cType == Odbc32.SQL_C.BINARY || cType == Odbc32.SQL_C.CHAR)
			{
				parameterBuffer.WriteIntPtr(this.intOffset, (IntPtr)cbSize);
			}
			else
			{
				parameterBuffer.WriteIntPtr(this.intOffset, IntPtr.Zero);
			}
			parameterBuffer.MarshalToNative(this.valueOffset, value, cType, sizeOrPrecision, offset);
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000ACA6D File Offset: 0x000AAC6D
		private static byte GetParameterPrecision(object value)
		{
			if (OdbcUtils.IsNull(value) || value is decimal)
			{
				return 28;
			}
			return 0;
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000ACA84 File Offset: 0x000AAC84
		private static byte GetParameterScale(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			if (value is DateTime && ((DateTime)value).Millisecond != 0)
			{
				return 7;
			}
			return 0;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000ACACC File Offset: 0x000AACCC
		public static OdbcParameter FromValue(Value value, OdbcTypeInfoCollection types)
		{
			Odbc32.SQL_TYPE? sql_TYPE = null;
			if (value.IsRecord)
			{
				RecordValue asRecord = value.AsRecord;
				Value value2;
				if (asRecord.TryGetValue("SqlType", out value2))
				{
					OdbcTypeInfo odbcTypeInfo;
					if (!value2.IsText || value2.IsNull || !types.TryGetType(value2.AsString, out odbcTypeInfo))
					{
						throw ValueException.NewExpressionError<Message1>(Strings.OdbcInvalidParameterType(value2.PrimitiveAndRecordToString(5)), value, null);
					}
					sql_TYPE = new Odbc32.SQL_TYPE?(odbcTypeInfo.SqlType);
				}
				if (!asRecord.TryGetValue("Value", out value2))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.OdbcNoParameterValue, asRecord, null);
				}
				value = value2;
			}
			OdbcTypeMap odbcTypeMap = null;
			if (sql_TYPE != null && !OdbcTypeMap.TryGet(sql_TYPE.Value, out odbcTypeMap))
			{
				throw ValueException.NewExpressionError<Message1>(Strings.OdbcInvalidParameterType(sql_TYPE.Value), value, null);
			}
			return new OdbcParameter(OdbcParameter.MapValue(value, sql_TYPE, ref odbcTypeMap), odbcTypeMap);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000ACBA8 File Offset: 0x000AADA8
		private static object MapValue(Value value, Odbc32.SQL_TYPE? sqlType, ref OdbcTypeMap odbcType)
		{
			switch (value.Kind)
			{
			case ValueKind.Null:
				odbcType = odbcType ?? OdbcTypeMap.FromSqlType(Odbc32.SQL_TYPE.UNKNOWN);
				return DBNull.Value;
			case ValueKind.Time:
				odbcType = odbcType ?? OdbcTypeMap.TypeTime;
				return value.AsTime.AsClrTimeSpan;
			case ValueKind.Date:
				odbcType = odbcType ?? OdbcTypeMap.TypeDate;
				return value.AsDate.AsClrDateTime;
			case ValueKind.DateTime:
				odbcType = odbcType ?? OdbcTypeMap.DateTime;
				return value.AsDateTime.AsClrDateTime;
			case ValueKind.Duration:
				odbcType = odbcType ?? OdbcTypeMap.TypeTime;
				return value.AsDuration.AsClrTimeSpan;
			case ValueKind.Number:
				if (sqlType != null)
				{
					return OdbcParameter.MapNumberValue(value.AsNumber, sqlType.Value);
				}
				odbcType = OdbcTypeMap.Double;
				return value.AsNumber.AsDouble;
			case ValueKind.Logical:
				odbcType = odbcType ?? OdbcTypeMap.Bit;
				return value.AsBoolean;
			case ValueKind.Text:
				odbcType = odbcType ?? OdbcTypeMap.WVarchar;
				return OdbcParameter.MapTextValue(value.AsText, odbcType);
			case ValueKind.Binary:
				odbcType = odbcType ?? OdbcTypeMap.Binary;
				return value.AsBinary.AsBytes;
			}
			throw ValueException.NewExpressionError<Message1>(Strings.OdbcInvalidParameterType(value.Kind), value, null);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000ACD24 File Offset: 0x000AAF24
		private static object MapTextValue(TextValue text, OdbcTypeMap odbcType)
		{
			if (odbcType == OdbcTypeMap.Guid)
			{
				try
				{
					return new Guid(text.AsString);
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			return text.AsString;
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000ACD78 File Offset: 0x000AAF78
		private static object MapNumberValue(NumberValue number, Odbc32.SQL_TYPE sqlType)
		{
			try
			{
				if (sqlType == Odbc32.SQL_TYPE.TINYINT)
				{
					return number.ToInt8();
				}
				if (sqlType == Odbc32.SQL_TYPE.BIGINT)
				{
					return number.ToInt64();
				}
				switch (sqlType)
				{
				case Odbc32.SQL_TYPE.DECIMAL:
					return number.ToDecimal();
				case Odbc32.SQL_TYPE.INTEGER:
					return number.ToInt32();
				case Odbc32.SQL_TYPE.SMALLINT:
					return number.ToInt16();
				case Odbc32.SQL_TYPE.REAL:
					return (float)number.ToDouble();
				case Odbc32.SQL_TYPE.DOUBLE:
					return number.ToDouble();
				}
			}
			catch (ValueException)
			{
			}
			catch (OverflowException)
			{
			}
			return number.ToDouble();
		}

		// Token: 0x04001BD0 RID: 7120
		private readonly OdbcTypeMap bindType;

		// Token: 0x04001BD1 RID: 7121
		private object value;

		// Token: 0x04001BD2 RID: 7122
		private int size;

		// Token: 0x04001BD3 RID: 7123
		private int bufferSize;

		// Token: 0x04001BD4 RID: 7124
		private int intOffset;

		// Token: 0x04001BD5 RID: 7125
		private int valueOffset;
	}
}
