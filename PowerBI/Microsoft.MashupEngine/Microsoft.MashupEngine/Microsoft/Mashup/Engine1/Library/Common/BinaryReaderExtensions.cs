using System;
using System.IO;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200102F RID: 4143
	public static class BinaryReaderExtensions
	{
		// Token: 0x06006C17 RID: 27671 RVA: 0x00174760 File Offset: 0x00172960
		public static object ReadObject(this BinaryReader reader, ObjectTag tag)
		{
			switch (tag)
			{
			case ObjectTag.Null:
				return null;
			case ObjectTag.Int32:
				return reader.ReadInt32();
			case ObjectTag.Int64:
				return reader.ReadInt64();
			case ObjectTag.Double:
				return reader.ReadDouble();
			case ObjectTag.Decimal:
				return reader.ReadDecimal();
			case ObjectTag.String:
				return reader.ReadString();
			case ObjectTag.True:
				return true;
			case ObjectTag.False:
				return false;
			case ObjectTag.DateTime:
				return DateTime.FromBinary(reader.ReadInt64());
			case ObjectTag.DBNull:
				return DBNull.Value;
			case ObjectTag.Int16:
				return reader.ReadInt16();
			case ObjectTag.Byte:
				return reader.ReadByte();
			case ObjectTag.Single:
				return reader.ReadSingle();
			case ObjectTag.Binary:
				return reader.ReadBytes(reader.ReadInt32());
			case ObjectTag.TimeSpan:
				return new TimeSpan(reader.ReadInt64());
			case ObjectTag.DateTimeOffset:
			{
				DateTime dateTime = DateTime.FromBinary(reader.ReadInt64());
				TimeSpan timeSpan = new TimeSpan(reader.ReadInt64());
				return new DateTimeOffset(dateTime, timeSpan);
			}
			case ObjectTag.Guid:
				return new Guid(reader.ReadBytes(16));
			case ObjectTag.Type:
				return BinaryReaderExtensions.GetType((ObjectTypeTag)reader.ReadInt32());
			case ObjectTag.SByte:
				return reader.ReadSByte();
			case ObjectTag.UInt16:
				return reader.ReadUInt16();
			case ObjectTag.UInt32:
				return reader.ReadUInt32();
			case ObjectTag.UInt64:
				return reader.ReadUInt64();
			case ObjectTag.Date:
				return new Date(DateTime.FromBinary(reader.ReadInt64()));
			case ObjectTag.Time:
				return new Time(new TimeSpan(reader.ReadInt64()));
			case ObjectTag.UnsupportedType:
				return new UnsupportedType(reader.ReadString());
			case ObjectTag.ValueException:
				return ValueException.New(ValueTreeDeserializer.DeserializeValue(reader.ReadBytes(reader.ReadInt32())).AsRecord, null);
			case ObjectTag.Currency:
				return new Currency(reader.ReadDecimal());
			case ObjectTag.Number:
				return reader.ReadNumber();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x00174979 File Offset: 0x00172B79
		public static ObjectTag ReadObjectTag(this BinaryReader reader)
		{
			return (ObjectTag)reader.ReadByte();
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x00174984 File Offset: 0x00172B84
		private unsafe static Number ReadNumber(this BinaryReader reader)
		{
			Number number;
			byte* ptr = (byte*)(&number);
			for (int i = 0; i < sizeof(Number); i++)
			{
				ptr[i] = reader.ReadByte();
			}
			return number;
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x001749B4 File Offset: 0x00172BB4
		private static Type GetType(ObjectTypeTag typeTag)
		{
			switch (typeTag)
			{
			case ObjectTypeTag.Boolean:
				return typeof(bool);
			case ObjectTypeTag.Byte:
				return typeof(byte);
			case ObjectTypeTag.Char:
				return typeof(char);
			case ObjectTypeTag.DBNull:
				return typeof(DBNull);
			case ObjectTypeTag.DateTime:
				return typeof(DateTime);
			case ObjectTypeTag.Decimal:
				return typeof(decimal);
			case ObjectTypeTag.Double:
				return typeof(double);
			case ObjectTypeTag.Int16:
				return typeof(short);
			case ObjectTypeTag.Int32:
				return typeof(int);
			case ObjectTypeTag.Int64:
				return typeof(long);
			case ObjectTypeTag.SByte:
				return typeof(sbyte);
			case ObjectTypeTag.Single:
				return typeof(float);
			case ObjectTypeTag.String:
				return typeof(string);
			case ObjectTypeTag.UInt16:
				return typeof(ushort);
			case ObjectTypeTag.UInt32:
				return typeof(uint);
			case ObjectTypeTag.UInt64:
				return typeof(ulong);
			case ObjectTypeTag.Type:
				return typeof(Type);
			case ObjectTypeTag.Guid:
				return typeof(Guid);
			case ObjectTypeTag.DateTimeOffset:
				return typeof(DateTimeOffset);
			case ObjectTypeTag.TimeSpan:
				return typeof(TimeSpan);
			case ObjectTypeTag.ByteArray:
				return typeof(byte[]);
			case ObjectTypeTag.Object:
				return typeof(object);
			case ObjectTypeTag.UnsupportedType:
				return typeof(UnsupportedType);
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
