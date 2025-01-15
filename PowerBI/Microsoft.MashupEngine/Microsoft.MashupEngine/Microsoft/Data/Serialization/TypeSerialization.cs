using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200015E RID: 350
	public static class TypeSerialization
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0000AB30 File Offset: 0x00008D30
		static TypeSerialization()
		{
			TypeSerialization.Add("Guid", typeof(Guid));
			TypeSerialization.Add("DateTimeOffset", typeof(DateTimeOffset));
			TypeSerialization.Add("TimeSpan", typeof(TimeSpan));
			TypeSerialization.Add("Byte[]", typeof(byte[]));
			TypeSerialization.Add("Object", typeof(object));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public static void Serialize(BinaryWriter writer, Type type)
		{
			if (type == null)
			{
				writer.Write(0);
				return;
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			writer.Write(checked((byte)typeCode));
			if (typeCode == TypeCode.Object)
			{
				Dictionary<string, Type> dictionary = TypeSerialization.stringToType;
				lock (dictionary)
				{
					string text;
					if (!TypeSerialization.typeToString.TryGetValue(type, out text))
					{
						text = TypeSerialization.EncodeTypeName(type);
						TypeSerialization.Add(text, type);
					}
					writer.Write(text);
				}
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public static Type Deserialize(BinaryReader reader)
		{
			switch (reader.ReadByte())
			{
			case 0:
				return null;
			case 1:
			{
				string text = reader.ReadString();
				Dictionary<string, Type> dictionary = TypeSerialization.stringToType;
				Type type;
				lock (dictionary)
				{
					if (!TypeSerialization.stringToType.TryGetValue(text, out type))
					{
						type = TypeSerialization.DecodeTypeName(text);
						TypeSerialization.Add(text, type);
					}
				}
				return type;
			}
			case 2:
				return typeof(DBNull);
			case 3:
				return typeof(bool);
			case 4:
				return typeof(char);
			case 5:
				return typeof(sbyte);
			case 6:
				return typeof(byte);
			case 7:
				return typeof(short);
			case 8:
				return typeof(ushort);
			case 9:
				return typeof(int);
			case 10:
				return typeof(uint);
			case 11:
				return typeof(long);
			case 12:
				return typeof(ulong);
			case 13:
				return typeof(float);
			case 14:
				return typeof(double);
			case 15:
				return typeof(decimal);
			case 16:
				return typeof(DateTime);
			case 18:
				return typeof(string);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		private static Type DecodeTypeName(string encoded)
		{
			return Type.GetType(encoded, true);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000ADBD File Offset: 0x00008FBD
		private static string EncodeTypeName(Type type)
		{
			return type.AssemblyQualifiedName;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000ADC5 File Offset: 0x00008FC5
		private static void Add(string label, Type type)
		{
			TypeSerialization.stringToType[label] = type;
			TypeSerialization.typeToString[type] = label;
		}

		// Token: 0x040003F7 RID: 1015
		private static Dictionary<string, Type> stringToType = new Dictionary<string, Type>();

		// Token: 0x040003F8 RID: 1016
		private static Dictionary<Type, string> typeToString = new Dictionary<Type, string>();
	}
}
