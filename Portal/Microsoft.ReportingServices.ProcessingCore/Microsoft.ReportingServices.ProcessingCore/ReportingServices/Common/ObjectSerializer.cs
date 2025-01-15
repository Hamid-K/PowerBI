using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C5 RID: 1477
	internal static class ObjectSerializer
	{
		// Token: 0x06005350 RID: 21328 RVA: 0x0015F5CC File Offset: 0x0015D7CC
		internal static DataTypeCode GetDataTypeCode(object value)
		{
			if (value == null)
			{
				return DataTypeCode.Empty;
			}
			return ObjectSerializer.GetDataTypeCode(value, Type.GetTypeCode(value.GetType()));
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0015F5E4 File Offset: 0x0015D7E4
		internal static DataTypeCode GetDataTypeCode(object value, TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				return DataTypeCode.Empty;
			case TypeCode.Object:
				if (value is TimeSpan)
				{
					return DataTypeCode.TimeSpan;
				}
				if (value is DateTimeOffset)
				{
					return DataTypeCode.DateTimeOffset;
				}
				if (value is byte[])
				{
					return DataTypeCode.ByteArray;
				}
				break;
			case TypeCode.Boolean:
				return DataTypeCode.Boolean;
			case TypeCode.Char:
				return DataTypeCode.Char;
			case TypeCode.SByte:
				return DataTypeCode.SByte;
			case TypeCode.Byte:
				return DataTypeCode.Byte;
			case TypeCode.Int16:
				return DataTypeCode.Int16;
			case TypeCode.UInt16:
				return DataTypeCode.UInt16;
			case TypeCode.Int32:
				return DataTypeCode.Int32;
			case TypeCode.UInt32:
				return DataTypeCode.UInt32;
			case TypeCode.Int64:
				return DataTypeCode.Int64;
			case TypeCode.UInt64:
				return DataTypeCode.UInt64;
			case TypeCode.Single:
				return DataTypeCode.Single;
			case TypeCode.Double:
				return DataTypeCode.Double;
			case TypeCode.Decimal:
				return DataTypeCode.Decimal;
			case TypeCode.DateTime:
				return DataTypeCode.DateTime;
			case TypeCode.String:
				return DataTypeCode.String;
			}
			return DataTypeCode.Unknown;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0015F690 File Offset: 0x0015D890
		internal static bool Equals(object obj1, object obj2, DataTypeCode dataTypeCode1, DataTypeCode dataTypeCode2)
		{
			DataTypeCode dataTypeCode3 = ((dataTypeCode1 == DataTypeCode.Unknown) ? ObjectSerializer.GetDataTypeCode(obj1) : dataTypeCode1);
			DataTypeCode dataTypeCode4 = ((dataTypeCode2 == DataTypeCode.Unknown) ? ObjectSerializer.GetDataTypeCode(obj2) : dataTypeCode2);
			if (dataTypeCode3 != dataTypeCode4)
			{
				return false;
			}
			if (dataTypeCode3 == DataTypeCode.Unknown)
			{
				return false;
			}
			if (obj1 == obj2)
			{
				return true;
			}
			if (dataTypeCode3 == DataTypeCode.Empty)
			{
				return obj1 == null && obj2 == null;
			}
			if (dataTypeCode3 != DataTypeCode.ByteArray)
			{
				return obj1.Equals(obj2);
			}
			byte[] array = (byte[])obj1;
			byte[] array2 = (byte[])obj2;
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0015F71C File Offset: 0x0015D91C
		internal static object Read(BinaryReader binaryReader, DataTypeCode dataTypeCode)
		{
			switch (dataTypeCode)
			{
			case DataTypeCode.Boolean:
				return binaryReader.ReadBoolean();
			case DataTypeCode.Char:
				return binaryReader.ReadChar();
			case DataTypeCode.Byte:
				return binaryReader.ReadByte();
			case DataTypeCode.Int16:
				return binaryReader.ReadInt16();
			case DataTypeCode.Int32:
				return binaryReader.ReadInt32();
			case DataTypeCode.Int64:
				return binaryReader.ReadInt64();
			case DataTypeCode.Single:
				return binaryReader.ReadSingle();
			case DataTypeCode.Double:
				return binaryReader.ReadDouble();
			case DataTypeCode.Decimal:
				return ObjectSerializer.ReadDecimalFromBinary(binaryReader);
			case DataTypeCode.DateTime:
				return DateTime.FromBinary(binaryReader.ReadInt64());
			case DataTypeCode.String:
				return binaryReader.ReadString();
			case DataTypeCode.ByteArray:
			{
				int num = binaryReader.ReadInt32();
				return binaryReader.ReadBytes(num);
			}
			case DataTypeCode.SByte:
				return binaryReader.ReadSByte();
			case DataTypeCode.UInt16:
				return binaryReader.ReadUInt16();
			case DataTypeCode.UInt32:
				return binaryReader.ReadUInt32();
			case DataTypeCode.UInt64:
				return binaryReader.ReadUInt64();
			case DataTypeCode.DateTimeOffset:
			{
				DateTime dateTime = DateTime.FromBinary(binaryReader.ReadInt64());
				TimeSpan timeSpan = TimeSpan.FromTicks(binaryReader.ReadInt64());
				return new DateTimeOffset(dateTime, timeSpan);
			}
			case DataTypeCode.TimeSpan:
				return TimeSpan.FromTicks(binaryReader.ReadInt64());
			default:
				return null;
			}
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0015F878 File Offset: 0x0015DA78
		internal static void Write(BinaryWriter binaryWriter, object value, DataTypeCode dataTypeCode)
		{
			if (value == null || dataTypeCode == DataTypeCode.Unknown || dataTypeCode == DataTypeCode.Empty)
			{
				return;
			}
			switch (dataTypeCode)
			{
			case DataTypeCode.Boolean:
				binaryWriter.Write((bool)value);
				return;
			case DataTypeCode.Char:
				binaryWriter.Write((char)value);
				return;
			case DataTypeCode.Byte:
				binaryWriter.Write((byte)value);
				return;
			case DataTypeCode.Int16:
				binaryWriter.Write((short)value);
				return;
			case DataTypeCode.Int32:
				binaryWriter.Write((int)value);
				return;
			case DataTypeCode.Int64:
				binaryWriter.Write((long)value);
				return;
			case DataTypeCode.Single:
				binaryWriter.Write((float)value);
				return;
			case DataTypeCode.Double:
				binaryWriter.Write((double)value);
				return;
			case DataTypeCode.Decimal:
				ObjectSerializer.WriteDecimalToBinary(binaryWriter, (decimal)value);
				return;
			case DataTypeCode.DateTime:
				ObjectSerializer.WriteDateTimeToBinary(binaryWriter, (DateTime)value);
				return;
			case DataTypeCode.String:
				binaryWriter.Write(value.ToString());
				return;
			case DataTypeCode.ByteArray:
			{
				byte[] array = (byte[])value;
				binaryWriter.Write(array.Length);
				binaryWriter.Write(array);
				return;
			}
			case DataTypeCode.SByte:
				binaryWriter.Write((sbyte)value);
				return;
			case DataTypeCode.UInt16:
				binaryWriter.Write((ushort)value);
				return;
			case DataTypeCode.UInt32:
				binaryWriter.Write((uint)value);
				return;
			case DataTypeCode.UInt64:
				binaryWriter.Write((ulong)value);
				return;
			case DataTypeCode.DateTimeOffset:
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				ObjectSerializer.WriteDateTimeToBinary(binaryWriter, dateTimeOffset.DateTime);
				binaryWriter.Write(dateTimeOffset.Offset.Ticks);
				return;
			}
			case DataTypeCode.TimeSpan:
				binaryWriter.Write(((TimeSpan)value).Ticks);
				return;
			default:
				return;
			}
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0015F9FC File Offset: 0x0015DBFC
		internal static object Read(XmlReader xmlReader, DataTypeCode dataTypeCode)
		{
			switch (dataTypeCode)
			{
			case DataTypeCode.Boolean:
				return XmlConvert.ToBoolean(xmlReader.Value);
			case DataTypeCode.Char:
				return XmlConvert.ToChar(xmlReader.Value);
			case DataTypeCode.Byte:
				return XmlConvert.ToByte(xmlReader.Value);
			case DataTypeCode.Int16:
				return XmlConvert.ToInt16(xmlReader.Value);
			case DataTypeCode.Int32:
				return XmlConvert.ToInt32(xmlReader.Value);
			case DataTypeCode.Int64:
				return XmlConvert.ToInt64(xmlReader.Value);
			case DataTypeCode.Single:
				return XmlConvert.ToSingle(xmlReader.Value);
			case DataTypeCode.Double:
				return XmlConvert.ToDouble(xmlReader.Value);
			case DataTypeCode.Decimal:
				return XmlConvert.ToDecimal(xmlReader.Value);
			case DataTypeCode.DateTime:
				return XmlConvert.ToDateTime(xmlReader.Value, XmlDateTimeSerializationMode.RoundtripKind);
			case DataTypeCode.String:
				return xmlReader.Value;
			case DataTypeCode.ByteArray:
				return Convert.FromBase64String(xmlReader.Value);
			case DataTypeCode.SByte:
				return XmlConvert.ToSByte(xmlReader.Value);
			case DataTypeCode.UInt16:
				return XmlConvert.ToUInt16(xmlReader.Value);
			case DataTypeCode.UInt32:
				return XmlConvert.ToUInt32(xmlReader.Value);
			case DataTypeCode.UInt64:
				return XmlConvert.ToUInt64(xmlReader.Value);
			case DataTypeCode.DateTimeOffset:
				return XmlConvert.ToDateTimeOffset(xmlReader.Value);
			case DataTypeCode.TimeSpan:
				return XmlConvert.ToTimeSpan(xmlReader.Value);
			default:
				return null;
			}
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0015FB84 File Offset: 0x0015DD84
		internal static void Write(XmlWriter xmlWriter, object value, DataTypeCode dataTypeCode)
		{
			if (value == null || dataTypeCode == DataTypeCode.Unknown || dataTypeCode == DataTypeCode.Empty)
			{
				return;
			}
			switch (dataTypeCode)
			{
			case DataTypeCode.Boolean:
				xmlWriter.WriteString(XmlConvert.ToString((bool)value));
				return;
			case DataTypeCode.Char:
				xmlWriter.WriteString(XmlConvert.ToString((char)value));
				return;
			case DataTypeCode.Byte:
				xmlWriter.WriteString(XmlConvert.ToString((byte)value));
				return;
			case DataTypeCode.Int16:
				xmlWriter.WriteString(XmlConvert.ToString((short)value));
				return;
			case DataTypeCode.Int32:
				xmlWriter.WriteString(XmlConvert.ToString((int)value));
				return;
			case DataTypeCode.Int64:
				xmlWriter.WriteString(XmlConvert.ToString((long)value));
				return;
			case DataTypeCode.Single:
				xmlWriter.WriteString(XmlConvert.ToString((float)value));
				return;
			case DataTypeCode.Double:
				xmlWriter.WriteString(XmlConvert.ToString((double)value));
				return;
			case DataTypeCode.Decimal:
				xmlWriter.WriteString(XmlConvert.ToString((decimal)value));
				return;
			case DataTypeCode.DateTime:
				xmlWriter.WriteString(XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind));
				return;
			case DataTypeCode.String:
				xmlWriter.WriteString((string)value);
				return;
			case DataTypeCode.ByteArray:
			{
				byte[] array = (byte[])value;
				xmlWriter.WriteBase64(array, 0, array.Length);
				return;
			}
			case DataTypeCode.SByte:
				xmlWriter.WriteString(XmlConvert.ToString((sbyte)value));
				return;
			case DataTypeCode.UInt16:
				xmlWriter.WriteString(XmlConvert.ToString((ushort)value));
				return;
			case DataTypeCode.UInt32:
				xmlWriter.WriteString(XmlConvert.ToString((uint)value));
				return;
			case DataTypeCode.UInt64:
				xmlWriter.WriteString(XmlConvert.ToString((ulong)value));
				return;
			case DataTypeCode.DateTimeOffset:
				xmlWriter.WriteString(XmlConvert.ToString((DateTimeOffset)value));
				return;
			case DataTypeCode.TimeSpan:
				xmlWriter.WriteString(XmlConvert.ToString((TimeSpan)value));
				return;
			default:
				return;
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0015FD2D File Offset: 0x0015DF2D
		private static decimal ReadDecimalFromBinary(BinaryReader reader)
		{
			return reader.ReadDecimal();
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0015FD35 File Offset: 0x0015DF35
		private static void WriteDateTimeToBinary(BinaryWriter writer, DateTime value)
		{
			writer.Write(value.ToBinary());
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0015FD44 File Offset: 0x0015DF44
		private static void WriteDecimalToBinary(BinaryWriter writer, decimal value)
		{
			writer.Write(value);
		}
	}
}
