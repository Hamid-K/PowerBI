using System;
using System.IO;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001030 RID: 4144
	public static class BinaryWriterExtensions
	{
		// Token: 0x06006C1B RID: 27675 RVA: 0x00174B2C File Offset: 0x00172D2C
		public static void WriteObject(this BinaryWriter writer, object value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.DBNull:
				writer.WriteDBNull();
				return;
			case TypeCode.Boolean:
				writer.WriteBoolean((bool)value);
				return;
			case TypeCode.SByte:
				writer.WriteSByte((sbyte)value);
				return;
			case TypeCode.Byte:
				writer.WriteByteValue((byte)value);
				return;
			case TypeCode.Int16:
				writer.WriteInt16((short)value);
				return;
			case TypeCode.UInt16:
				writer.WriteUInt16((ushort)value);
				return;
			case TypeCode.Int32:
				writer.WriteInt32((int)value);
				return;
			case TypeCode.UInt32:
				writer.WriteUInt32((uint)value);
				return;
			case TypeCode.Int64:
				writer.WriteInt64((long)value);
				return;
			case TypeCode.UInt64:
				writer.WriteUInt64((ulong)value);
				return;
			case TypeCode.Single:
				writer.WriteSingle((float)value);
				return;
			case TypeCode.Double:
				writer.WriteDouble((double)value);
				return;
			case TypeCode.Decimal:
				writer.WriteDecimal((decimal)value);
				return;
			case TypeCode.DateTime:
				writer.WriteDateTime((DateTime)value);
				return;
			case TypeCode.String:
				writer.WriteString((string)value);
				return;
			}
			if (value is TimeSpan)
			{
				writer.WriteTimeSpan((TimeSpan)value);
				return;
			}
			if (value is DateTimeOffset)
			{
				writer.WriteDateTimeOffset((DateTimeOffset)value);
				return;
			}
			if (value is Date)
			{
				writer.WriteDate((Date)value);
				return;
			}
			if (value is Time)
			{
				writer.WriteTime((Time)value);
				return;
			}
			if (value is Currency)
			{
				writer.WriteCurrency((Currency)value);
				return;
			}
			if (value is Number)
			{
				writer.WriteNumber((Number)value);
				return;
			}
			if (value is Guid)
			{
				writer.WriteGuid((Guid)value);
				return;
			}
			if (value is byte[])
			{
				writer.WriteBinary((byte[])value);
				return;
			}
			if (value is Type)
			{
				writer.WriteType((Type)value);
				return;
			}
			if (value is ValueException)
			{
				writer.WriteValueException((ValueException)value);
				return;
			}
			writer.WriteUnsupportedType(value.GetType().ToString());
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x00174D40 File Offset: 0x00172F40
		public static void WriteUnsupportedType(this BinaryWriter writer, string type)
		{
			writer.WriteObjectTag(ObjectTag.UnsupportedType);
			writer.Write(type);
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x00174D51 File Offset: 0x00172F51
		public static void WriteObjectTag(this BinaryWriter writer, ObjectTag tag)
		{
			writer.Write((byte)tag);
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x00174D5B File Offset: 0x00172F5B
		private static void WriteDouble(this BinaryWriter writer, double value)
		{
			writer.WriteObjectTag(ObjectTag.Double);
			writer.Write(value);
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x00174D6B File Offset: 0x00172F6B
		private static void WriteDecimal(this BinaryWriter writer, decimal value)
		{
			writer.WriteObjectTag(ObjectTag.Decimal);
			writer.Write(value);
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x00174D7B File Offset: 0x00172F7B
		private static void WriteInt16(this BinaryWriter writer, short value)
		{
			writer.WriteObjectTag(ObjectTag.Int16);
			writer.Write(value);
		}

		// Token: 0x06006C21 RID: 27681 RVA: 0x00174D8C File Offset: 0x00172F8C
		private static void WriteInt32(this BinaryWriter writer, int value)
		{
			writer.WriteObjectTag(ObjectTag.Int32);
			writer.Write(value);
		}

		// Token: 0x06006C22 RID: 27682 RVA: 0x00174D9C File Offset: 0x00172F9C
		private static void WriteInt64(this BinaryWriter writer, long value)
		{
			writer.WriteObjectTag(ObjectTag.Int64);
			writer.Write(value);
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x00174DAC File Offset: 0x00172FAC
		private static void WriteSByte(this BinaryWriter writer, sbyte value)
		{
			writer.WriteObjectTag(ObjectTag.SByte);
			writer.Write(value);
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x00174DBD File Offset: 0x00172FBD
		private static void WriteUInt16(this BinaryWriter writer, ushort value)
		{
			writer.WriteObjectTag(ObjectTag.UInt16);
			writer.Write(value);
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x00174DCE File Offset: 0x00172FCE
		private static void WriteUInt32(this BinaryWriter writer, uint value)
		{
			writer.WriteObjectTag(ObjectTag.UInt32);
			writer.Write(value);
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x00174DDF File Offset: 0x00172FDF
		private static void WriteUInt64(this BinaryWriter writer, ulong value)
		{
			writer.WriteObjectTag(ObjectTag.UInt64);
			writer.Write(value);
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x00174DF0 File Offset: 0x00172FF0
		private static void WriteString(this BinaryWriter writer, string value)
		{
			writer.WriteObjectTag(ObjectTag.String);
			writer.Write(value);
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x00174E01 File Offset: 0x00173001
		private static void WriteDateTime(this BinaryWriter writer, DateTime value)
		{
			writer.WriteObjectTag(ObjectTag.DateTime);
			writer.Write(value.ToBinary());
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x00174E18 File Offset: 0x00173018
		private static void WriteBoolean(this BinaryWriter writer, bool value)
		{
			if (value)
			{
				writer.WriteObjectTag(ObjectTag.True);
				return;
			}
			writer.WriteObjectTag(ObjectTag.False);
		}

		// Token: 0x06006C2A RID: 27690 RVA: 0x00174E2E File Offset: 0x0017302E
		private static void WriteNull(this BinaryWriter writer)
		{
			writer.WriteObjectTag(ObjectTag.Null);
		}

		// Token: 0x06006C2B RID: 27691 RVA: 0x00174E37 File Offset: 0x00173037
		private static void WriteDBNull(this BinaryWriter writer)
		{
			writer.WriteObjectTag(ObjectTag.DBNull);
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x00174E41 File Offset: 0x00173041
		private static void WriteByteValue(this BinaryWriter writer, byte value)
		{
			writer.WriteObjectTag(ObjectTag.Byte);
			writer.Write(value);
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x00174E52 File Offset: 0x00173052
		private static void WriteSingle(this BinaryWriter writer, float value)
		{
			writer.WriteObjectTag(ObjectTag.Single);
			writer.Write(value);
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x00174E63 File Offset: 0x00173063
		private static void WriteTimeSpan(this BinaryWriter writer, TimeSpan value)
		{
			writer.WriteObjectTag(ObjectTag.TimeSpan);
			writer.Write(value.Ticks);
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x00174E7C File Offset: 0x0017307C
		private static void WriteDateTimeOffset(this BinaryWriter writer, DateTimeOffset value)
		{
			writer.WriteObjectTag(ObjectTag.DateTimeOffset);
			writer.Write(value.DateTime.ToBinary());
			writer.Write(value.Offset.Ticks);
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x00174EBC File Offset: 0x001730BC
		private static void WriteDate(this BinaryWriter writer, Date value)
		{
			writer.WriteObjectTag(ObjectTag.Date);
			writer.Write(value.DateTime.ToBinary());
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x00174EE8 File Offset: 0x001730E8
		private static void WriteTime(this BinaryWriter writer, Time value)
		{
			writer.WriteObjectTag(ObjectTag.Time);
			writer.Write(value.TimeSpan.Ticks);
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x00174F12 File Offset: 0x00173112
		private static void WriteCurrency(this BinaryWriter writer, Currency value)
		{
			writer.WriteObjectTag(ObjectTag.Currency);
			writer.Write(value.Value);
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x00174F2C File Offset: 0x0017312C
		private unsafe static void WriteNumber(this BinaryWriter writer, Number number)
		{
			writer.WriteObjectTag(ObjectTag.Number);
			byte* ptr = (byte*)(&number);
			for (int i = 0; i < sizeof(Number); i++)
			{
				writer.Write(ptr[i]);
			}
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x00174F60 File Offset: 0x00173160
		private static void WriteGuid(this BinaryWriter writer, Guid value)
		{
			writer.WriteObjectTag(ObjectTag.Guid);
			writer.Write(value.ToByteArray());
		}

		// Token: 0x06006C35 RID: 27701 RVA: 0x00174F77 File Offset: 0x00173177
		private static void WriteBinary(this BinaryWriter writer, byte[] value)
		{
			writer.WriteObjectTag(ObjectTag.Binary);
			writer.Write(value.Length);
			writer.Write(value);
		}

		// Token: 0x06006C36 RID: 27702 RVA: 0x00174F91 File Offset: 0x00173191
		private static void WriteType(this BinaryWriter writer, Type type)
		{
			writer.WriteObjectTag(ObjectTag.Type);
			writer.Write((int)BinaryWriterExtensions.GetTypeTag(type));
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x00174FA8 File Offset: 0x001731A8
		private static void WriteValueException(this BinaryWriter writer, ValueException valueException)
		{
			writer.WriteObjectTag(ObjectTag.ValueException);
			byte[] array = ValueTreeSerializer.SerializeValue(valueException.Value);
			writer.Write(array.Length);
			writer.Write(array);
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x00174FDC File Offset: 0x001731DC
		private static ObjectTypeTag GetTypeTag(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.DBNull:
				return ObjectTypeTag.DBNull;
			case TypeCode.Boolean:
				return ObjectTypeTag.Boolean;
			case TypeCode.Char:
				return ObjectTypeTag.Char;
			case TypeCode.SByte:
				return ObjectTypeTag.SByte;
			case TypeCode.Byte:
				return ObjectTypeTag.Byte;
			case TypeCode.Int16:
				return ObjectTypeTag.Int16;
			case TypeCode.UInt16:
				return ObjectTypeTag.UInt16;
			case TypeCode.Int32:
				return ObjectTypeTag.Int32;
			case TypeCode.UInt32:
				return ObjectTypeTag.UInt32;
			case TypeCode.Int64:
				return ObjectTypeTag.Int64;
			case TypeCode.UInt64:
				return ObjectTypeTag.UInt64;
			case TypeCode.Single:
				return ObjectTypeTag.Single;
			case TypeCode.Double:
				return ObjectTypeTag.Double;
			case TypeCode.Decimal:
				return ObjectTypeTag.Decimal;
			case TypeCode.DateTime:
				return ObjectTypeTag.DateTime;
			case TypeCode.String:
				return ObjectTypeTag.String;
			}
			if (typeof(Guid) == type)
			{
				return ObjectTypeTag.Guid;
			}
			if (typeof(DateTimeOffset) == type)
			{
				return ObjectTypeTag.DateTimeOffset;
			}
			if (typeof(TimeSpan) == type)
			{
				return ObjectTypeTag.TimeSpan;
			}
			if (typeof(byte[]) == type)
			{
				return ObjectTypeTag.ByteArray;
			}
			if (typeof(object) == type)
			{
				return ObjectTypeTag.Object;
			}
			if (typeof(Type).IsAssignableFrom(type))
			{
				return ObjectTypeTag.Type;
			}
			return ObjectTypeTag.UnsupportedType;
		}
	}
}
