using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.IO
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public static class StreamUtilities
	{
		// Token: 0x0600011E RID: 286 RVA: 0x000102F8 File Offset: 0x0000E4F8
		public static bool IsSimpleValueType(Type t)
		{
			return t == typeof(int) || t == typeof(long) || t == typeof(short) || t == typeof(float) || t == typeof(double) || t == typeof(uint) || t == typeof(ulong) || t == typeof(ushort) || t == typeof(bool) || t == typeof(byte) || t == typeof(char);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0001039C File Offset: 0x0000E59C
		public static BitArray CreateIsSimpleValueBitArray(DataTable schema)
		{
			SchemaUtils.CreateIndexOnColumnOrdinal(schema);
			BitArray bitArray = new BitArray(schema.Rows.Count);
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				DataRow schemaRow = SchemaUtils.GetSchemaRow(schema, i);
				if (StreamUtilities.IsSimpleValueType((Type)schemaRow[SchemaTableColumn.DataType]))
				{
					bitArray[(int)schemaRow[SchemaTableColumn.ColumnOrdinal]] = true;
				}
			}
			return bitArray;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00010410 File Offset: 0x0000E610
		public static Type[] GetTypesByOrdinal(DataTable schema)
		{
			SchemaUtils.CreateIndexOnColumnOrdinal(schema);
			Type[] array = new Type[schema.Rows.Count];
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				DataRow schemaRow = SchemaUtils.GetSchemaRow(schema, i);
				array[(int)schemaRow[SchemaTableColumn.ColumnOrdinal]] = (Type)schemaRow[SchemaTableColumn.DataType];
			}
			return array;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00010475 File Offset: 0x0000E675
		public static void Write(Stream s, short v)
		{
			StreamUtilities.WriteInt16(s, v);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0001047E File Offset: 0x0000E67E
		public static void Write(Stream s, int v)
		{
			StreamUtilities.WriteInt32(s, v);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00010487 File Offset: 0x0000E687
		public static void Write(Stream s, long v)
		{
			StreamUtilities.WriteInt64(s, v);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00010490 File Offset: 0x0000E690
		public static void Write(Stream s, ushort v)
		{
			StreamUtilities.WriteUInt16(s, v);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00010499 File Offset: 0x0000E699
		public static void Write(Stream s, uint v)
		{
			StreamUtilities.WriteUInt32(s, v);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000104A2 File Offset: 0x0000E6A2
		public static void Write(Stream s, ulong v)
		{
			StreamUtilities.WriteUInt64(s, v);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000104AB File Offset: 0x0000E6AB
		public static void Write(Stream s, bool v)
		{
			StreamUtilities.WriteBoolean(s, v);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000104B4 File Offset: 0x0000E6B4
		public static void Write(Stream s, byte v)
		{
			s.WriteByte(v);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000104BD File Offset: 0x0000E6BD
		public static void Write(Stream s, char v)
		{
			StreamUtilities.WriteChar(s, v);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000104C6 File Offset: 0x0000E6C6
		public static void Write(Stream s, float v)
		{
			StreamUtilities.WriteSingle(s, v);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000104CF File Offset: 0x0000E6CF
		public static void Write(Stream s, double v)
		{
			StreamUtilities.WriteDouble(s, v);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000104D8 File Offset: 0x0000E6D8
		public static void WriteSimpleValueType(Stream s, object o)
		{
			if (o is int)
			{
				StreamUtilities.WriteInt32(s, (int)o);
				return;
			}
			if (o is long)
			{
				StreamUtilities.WriteInt64(s, (long)o);
				return;
			}
			if (o is short)
			{
				StreamUtilities.WriteInt16(s, (short)o);
				return;
			}
			if (o is uint)
			{
				StreamUtilities.WriteUInt32(s, (uint)o);
				return;
			}
			if (o is ulong)
			{
				StreamUtilities.WriteUInt64(s, (ulong)o);
				return;
			}
			if (o is ushort)
			{
				StreamUtilities.WriteUInt16(s, (ushort)o);
				return;
			}
			if (o is float)
			{
				StreamUtilities.WriteSingle(s, (float)o);
				return;
			}
			if (o is double)
			{
				StreamUtilities.WriteDouble(s, (double)o);
				return;
			}
			if (o is bool)
			{
				StreamUtilities.WriteBoolean(s, (bool)o);
				return;
			}
			if (o is char)
			{
				StreamUtilities.WriteChar(s, (char)o);
				return;
			}
			if (o is byte)
			{
				s.WriteByte((byte)o);
				return;
			}
			throw new ArgumentException("Unsupported type.");
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public static object ReadSimpleValueType(Stream s, Type t)
		{
			if (t == typeof(int))
			{
				return StreamUtilities.ReadInt32(s);
			}
			if (t == typeof(long))
			{
				return StreamUtilities.ReadInt64(s);
			}
			if (t == typeof(short))
			{
				return StreamUtilities.ReadInt16(s);
			}
			if (t == typeof(uint))
			{
				return StreamUtilities.ReadUInt32(s);
			}
			if (t == typeof(ulong))
			{
				return StreamUtilities.ReadUInt64(s);
			}
			if (t == typeof(ushort))
			{
				return StreamUtilities.ReadUInt16(s);
			}
			if (t == typeof(float))
			{
				return StreamUtilities.ReadSingle(s);
			}
			if (t == typeof(double))
			{
				return StreamUtilities.ReadDouble(s);
			}
			if (t == typeof(bool))
			{
				return StreamUtilities.ReadBoolean(s);
			}
			if (t == typeof(char))
			{
				return StreamUtilities.ReadChar(s);
			}
			if (t == typeof(byte))
			{
				return (byte)s.ReadByte();
			}
			throw new ArgumentException("Unsupported type.");
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00010704 File Offset: 0x0000E904
		public static void WriteString(Stream s, string str)
		{
			StreamUtilities.WriteInt32(s, str.Length);
			for (int i = 0; i < str.Length; i++)
			{
				s.WriteByte((byte)((str.get_Chars(i) >> 8) & 'ÿ'));
				s.WriteByte((byte)(str.get_Chars(i) & 'ÿ'));
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00010758 File Offset: 0x0000E958
		public static string ReadString(Stream s)
		{
			int num = StreamUtilities.ReadInt32(s);
			StringBuilder stringBuilder = new StringBuilder(num);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append((char)((ushort)(s.ReadByte() << 8) | (ushort)s.ReadByte()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public static void WriteStringExtent(Stream s, StringExtent str)
		{
			StreamUtilities.WriteInt32(s, str.Length);
			for (int i = 0; i < str.Length; i++)
			{
				s.WriteByte((byte)((str[i] >> 8) & 'ÿ'));
				s.WriteByte((byte)(str[i] & 'ÿ'));
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public static StringExtent ReadStringExtent(Stream s)
		{
			int num = StreamUtilities.ReadInt32(s);
			StringBuilder stringBuilder = new StringBuilder(num);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append((char)((ushort)(s.ReadByte() << 8) | (ushort)s.ReadByte()));
			}
			return new StringExtent(stringBuilder.ToString());
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00010843 File Offset: 0x0000EA43
		public static void WriteInt64(Stream s, long i)
		{
			StreamUtilities.WriteInt64_LittleEndian(s, i);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0001084C File Offset: 0x0000EA4C
		public static void WriteUInt64(Stream s, ulong i)
		{
			StreamUtilities.WriteUInt64_LittleEndian(s, i);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00010855 File Offset: 0x0000EA55
		public static void WriteInt32(Stream s, int i)
		{
			StreamUtilities.WriteInt32_LittleEndian(s, i);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001085E File Offset: 0x0000EA5E
		public static void WriteUInt32(Stream s, uint i)
		{
			StreamUtilities.WriteUInt32_LittleEndian(s, i);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00010867 File Offset: 0x0000EA67
		public static void WriteInt16(Stream s, short i)
		{
			StreamUtilities.WriteInt16_LittleEndian(s, i);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00010870 File Offset: 0x0000EA70
		public static void WriteUInt16(Stream s, ushort i)
		{
			StreamUtilities.WriteUInt16_LittleEndian(s, i);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00010879 File Offset: 0x0000EA79
		public static void WriteChar(Stream s, char i)
		{
			StreamUtilities.WriteChar_LittleEndian(s, i);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00010882 File Offset: 0x0000EA82
		public static void WriteDouble(Stream s, double i)
		{
			StreamUtilities.WriteDouble_LittleEndian(s, i);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0001088B File Offset: 0x0000EA8B
		public static void WriteSingle(Stream s, float i)
		{
			StreamUtilities.WriteSingle_LittleEndian(s, i);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00010894 File Offset: 0x0000EA94
		public static void WriteInt32(byte[] array, int offset, int i)
		{
			StreamUtilities.WriteInt32_LittleEndian(array, offset, i);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0001089E File Offset: 0x0000EA9E
		public static long ReadInt64(Stream s)
		{
			return StreamUtilities.ReadInt64_LittleEndian(s);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public static ulong ReadUInt64(Stream s)
		{
			return StreamUtilities.ReadUInt64_LittleEndian(s);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000108AE File Offset: 0x0000EAAE
		public static int ReadInt32(Stream s)
		{
			return StreamUtilities.ReadInt32_LittleEndian(s);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000108B6 File Offset: 0x0000EAB6
		public static uint ReadUInt32(Stream s)
		{
			return StreamUtilities.ReadUInt32_LittleEndian(s);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000108BE File Offset: 0x0000EABE
		public static short ReadInt16(Stream s)
		{
			return StreamUtilities.ReadInt16_LittleEndian(s);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000108C6 File Offset: 0x0000EAC6
		public static ushort ReadUInt16(Stream s)
		{
			return StreamUtilities.ReadUInt16_LittleEndian(s);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000108CE File Offset: 0x0000EACE
		public static char ReadChar(Stream s)
		{
			return StreamUtilities.ReadChar_LittleEndian(s);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000108D6 File Offset: 0x0000EAD6
		public static double ReadDouble(Stream s)
		{
			return StreamUtilities.ReadDouble_LittleEndian(s);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000108DE File Offset: 0x0000EADE
		public static float ReadSingle(Stream s)
		{
			return StreamUtilities.ReadSingle(s, EndianType.Default);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000108E7 File Offset: 0x0000EAE7
		public static int ReadInt32(byte[] array, int offset)
		{
			return StreamUtilities.ReadInt32_LittleEndian(array, offset);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public static void WriteInt64_BigEndian(Stream s, long i)
		{
			s.WriteByte((byte)(i >> 56));
			s.WriteByte((byte)(i >> 48));
			s.WriteByte((byte)(i >> 40));
			s.WriteByte((byte)(i >> 32));
			s.WriteByte((byte)(i >> 24));
			s.WriteByte((byte)(i >> 16));
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00010954 File Offset: 0x0000EB54
		public static void WriteUInt64_BigEndian(Stream s, ulong i)
		{
			s.WriteByte((byte)(i >> 56));
			s.WriteByte((byte)(i >> 48));
			s.WriteByte((byte)(i >> 40));
			s.WriteByte((byte)(i >> 32));
			s.WriteByte((byte)(i >> 24));
			s.WriteByte((byte)(i >> 16));
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000109B5 File Offset: 0x0000EBB5
		public static void WriteInt32_BigEndian(Stream s, int i)
		{
			s.WriteByte((byte)(i >> 24));
			s.WriteByte((byte)(i >> 16));
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000109DF File Offset: 0x0000EBDF
		public static void WriteUInt32_BigEndian(Stream s, uint i)
		{
			s.WriteByte((byte)(i >> 24));
			s.WriteByte((byte)(i >> 16));
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public static void WriteSingle_BigEndian(Stream s, float i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (!BitConverter.IsLittleEndian)
			{
				s.Write(bytes, 0, bytes.Length);
				return;
			}
			s.Write(Enumerable.Reverse<byte>(bytes));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00010A40 File Offset: 0x0000EC40
		public static void WriteDouble_BigEndian(Stream s, double i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (!BitConverter.IsLittleEndian)
			{
				s.Write(bytes, 0, bytes.Length);
				return;
			}
			s.Write(Enumerable.Reverse<byte>(bytes));
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00010A73 File Offset: 0x0000EC73
		public static void WriteInt32_BigEndian(byte[] array, int offset, int i)
		{
			array[offset] = (byte)(i >> 24);
			array[++offset] = (byte)(i >> 16);
			array[++offset] = (byte)(i >> 8);
			array[++offset] = (byte)i;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		public static void WriteInt16_BigEndian(Stream s, short i)
		{
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public static void WriteChar_BigEndian(Stream s, char i)
		{
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		public static void WriteUInt16_BigEndian(Stream s, ushort i)
		{
			s.WriteByte((byte)(i >> 8));
			s.WriteByte((byte)i);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00010ADC File Offset: 0x0000ECDC
		public static void WriteInt64_LittleEndian(Stream s, long i)
		{
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00010B54 File Offset: 0x0000ED54
		public static void WriteUInt64_LittleEndian(Stream s, ulong i)
		{
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00010BC9 File Offset: 0x0000EDC9
		public static void WriteInt32_LittleEndian(Stream s, int i)
		{
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00010BFF File Offset: 0x0000EDFF
		public static void WriteInt32_LittleEndian(byte[] array, int offset, int i)
		{
			array[offset++] = (byte)i;
			i >>= 8;
			array[offset++] = (byte)i;
			i >>= 8;
			array[offset++] = (byte)i;
			i >>= 8;
			array[offset++] = (byte)i;
			i >>= 8;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00010C3D File Offset: 0x0000EE3D
		public static void WriteUInt32_LittleEndian(Stream s, uint i)
		{
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00010C73 File Offset: 0x0000EE73
		public static void WriteInt16_LittleEndian(Stream s, short i)
		{
			s.WriteByte((byte)i);
			i = (short)(i >> 8);
			s.WriteByte((byte)i);
			i = (short)(i >> 8);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00010C91 File Offset: 0x0000EE91
		public static void WriteUInt16_LittleEndian(Stream s, ushort i)
		{
			s.WriteByte((byte)i);
			i = (ushort)(i >> 8);
			s.WriteByte((byte)i);
			i = (ushort)(i >> 8);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public static void WriteSingle_LittleEndian(Stream s, float i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (BitConverter.IsLittleEndian)
			{
				s.Write(bytes, 0, bytes.Length);
				return;
			}
			s.Write(Enumerable.Reverse<byte>(bytes));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		public static void WriteDouble_LittleEndian(Stream s, double i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (BitConverter.IsLittleEndian)
			{
				s.Write(bytes, 0, bytes.Length);
				return;
			}
			s.Write(Enumerable.Reverse<byte>(bytes));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00010D17 File Offset: 0x0000EF17
		public static void WriteChar_LittleEndian(Stream s, char i)
		{
			s.WriteByte((byte)i);
			i >>= 8;
			s.WriteByte((byte)i);
			i >>= 8;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00010D35 File Offset: 0x0000EF35
		public static void WriteBoolean(Stream s, bool i)
		{
			s.WriteByte(i ? 1 : 0);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00010D44 File Offset: 0x0000EF44
		public static long ReadInt64_BigEndian(Stream s)
		{
			return (long)(((ulong)StreamUtilities.ReadUInt32_BigEndian(s) << 32) | (ulong)StreamUtilities.ReadUInt32_BigEndian(s));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00010D58 File Offset: 0x0000EF58
		public static ulong ReadUInt64_BigEndian(Stream s)
		{
			return ((ulong)StreamUtilities.ReadUInt32_BigEndian(s) << 32) | (ulong)StreamUtilities.ReadUInt32_BigEndian(s);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public static short ReadInt16_BigEndian(Stream s)
		{
			return (short)((s.ReadByte() << 8) | s.ReadByte());
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00010D7E File Offset: 0x0000EF7E
		public static int ReadInt32_BigEndian(Stream s)
		{
			return (s.ReadByte() << 24) | (s.ReadByte() << 16) | (s.ReadByte() << 8) | s.ReadByte();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00010DA3 File Offset: 0x0000EFA3
		public static int ReadInt32_BigEndian(byte[] array, int offset)
		{
			return ((int)array[offset] << 24) | ((int)array[++offset] << 16) | ((int)array[++offset] << 8) | (int)array[++offset];
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00010DCB File Offset: 0x0000EFCB
		public static ushort ReadUInt16_BigEndian(Stream s)
		{
			return (ushort)((s.ReadByte() << 8) | s.ReadByte());
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00010DDD File Offset: 0x0000EFDD
		public static uint ReadUInt32_BigEndian(Stream s)
		{
			return (uint)((s.ReadByte() << 24) | (s.ReadByte() << 16) | (s.ReadByte() << 8) | s.ReadByte());
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00010E02 File Offset: 0x0000F002
		public static char ReadChar_BigEndian(Stream s)
		{
			return (char)((s.ReadByte() << 8) | s.ReadByte());
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00010E14 File Offset: 0x0000F014
		public static float ReadSingle_BigEndian(Stream s)
		{
			return StreamUtilities.ReadSingle(s, EndianType.BigEndian);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00010E1D File Offset: 0x0000F01D
		public static float ReadSingle_LittleEndian(Stream s)
		{
			return StreamUtilities.ReadSingle(s, EndianType.Default);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00010E26 File Offset: 0x0000F026
		public static double ReadDouble_BigEndian(Stream s)
		{
			return StreamUtilities.ReadDouble(s, EndianType.BigEndian);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00010E2F File Offset: 0x0000F02F
		public static double ReadDouble_LittleEndian(Stream s)
		{
			return StreamUtilities.ReadDouble(s, EndianType.Default);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00010E38 File Offset: 0x0000F038
		public static float ReadSingle(Stream s, EndianType e)
		{
			byte[] array = new byte[4];
			if ((BitConverter.IsLittleEndian && e == EndianType.Default) || (!BitConverter.IsLittleEndian && EndianType.BigEndian == e))
			{
				s.Read(array, 0, 4);
			}
			else
			{
				array[3] = (byte)s.ReadByte();
				array[2] = (byte)s.ReadByte();
				array[1] = (byte)s.ReadByte();
				array[0] = (byte)s.ReadByte();
			}
			return BitConverter.ToSingle(array, 0);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00010E9C File Offset: 0x0000F09C
		public static double ReadDouble(Stream s, EndianType e)
		{
			byte[] array = new byte[8];
			if ((BitConverter.IsLittleEndian && e == EndianType.Default) || (!BitConverter.IsLittleEndian && EndianType.BigEndian == e))
			{
				s.Read(array, 0, 8);
			}
			else
			{
				array[7] = (byte)s.ReadByte();
				array[6] = (byte)s.ReadByte();
				array[5] = (byte)s.ReadByte();
				array[4] = (byte)s.ReadByte();
				array[3] = (byte)s.ReadByte();
				array[2] = (byte)s.ReadByte();
				array[1] = (byte)s.ReadByte();
				array[0] = (byte)s.ReadByte();
			}
			return BitConverter.ToDouble(array, 0);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00010F28 File Offset: 0x0000F128
		public static long ReadInt64_LittleEndian(Stream s)
		{
			return (long)((ulong)StreamUtilities.ReadUInt32_LittleEndian(s) | ((ulong)StreamUtilities.ReadUInt32_LittleEndian(s) << 32));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00010F3C File Offset: 0x0000F13C
		public static ulong ReadUInt64_LittleEndian(Stream s)
		{
			return (ulong)StreamUtilities.ReadUInt32_LittleEndian(s) | ((ulong)StreamUtilities.ReadUInt32_LittleEndian(s) << 32);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00010F50 File Offset: 0x0000F150
		public static short ReadInt16_LittleEndian(Stream s)
		{
			return (short)(s.ReadByte() | (s.ReadByte() << 8));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00010F62 File Offset: 0x0000F162
		public static int ReadInt32_LittleEndian(Stream s)
		{
			return s.ReadByte() | (s.ReadByte() << 8) | (s.ReadByte() << 16) | (s.ReadByte() << 24);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00010F87 File Offset: 0x0000F187
		public static int ReadInt32_LittleEndian(byte[] array, int offset)
		{
			return (int)array[offset] | ((int)array[++offset] << 8) | ((int)array[++offset] << 16) | ((int)array[++offset] << 24);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00010FAF File Offset: 0x0000F1AF
		public static ushort ReadUInt16_LittleEndian(Stream s)
		{
			return (ushort)(s.ReadByte() | (s.ReadByte() << 8));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00010FC1 File Offset: 0x0000F1C1
		public static uint ReadUInt32_LittleEndian(Stream s)
		{
			return (uint)(s.ReadByte() | (s.ReadByte() << 8) | (s.ReadByte() << 16) | (s.ReadByte() << 24));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00010FE6 File Offset: 0x0000F1E6
		public static char ReadChar_LittleEndian(Stream s)
		{
			return (char)(s.ReadByte() | (s.ReadByte() << 8));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public static bool ReadBoolean(Stream s)
		{
			return s.ReadByte() != 0;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00011008 File Offset: 0x0000F208
		public static ulong ReadVariableLengthInt(byte[] data, ref int offset)
		{
			ulong num = (ulong)((long)(data[offset] & 127));
			int num2 = 0;
			while ((data[offset] & 128) != 0)
			{
				offset++;
				num2 += 7;
				num |= (ulong)((ulong)((long)(data[offset] & 127)) << num2);
			}
			offset++;
			return num;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00011050 File Offset: 0x0000F250
		public static ulong ReadVariableLengthInt(Stream stream)
		{
			int num = stream.ReadByte();
			ulong num2 = (ulong)((long)(num & 127));
			int num3 = 0;
			while ((num & 128) != 0)
			{
				num = stream.ReadByte();
				num3 += 7;
				num2 |= (ulong)((ulong)((long)(num & 127)) << num3);
			}
			return num2;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00011090 File Offset: 0x0000F290
		public static void WriteVariableLengthInt(ulong value, Stream stream)
		{
			do
			{
				byte b = (byte)(value & 127UL);
				byte b2 = ((value > 127UL) ? 128 : 0);
				stream.WriteByte(b | b2);
				value >>= 7;
			}
			while (value > 0UL);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public static void WriteVariableLengthInt(ulong value, IList<byte> data)
		{
			do
			{
				byte b = (byte)(value & 127UL);
				byte b2 = ((value > 127UL) ? 128 : 0);
				data.Add(b | b2);
				value >>= 7;
			}
			while (value > 0UL);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00011100 File Offset: 0x0000F300
		public static void WriteVariableLengthInt(ulong value, byte[] dest, int offset, out int numBytes)
		{
			int num = offset;
			do
			{
				byte b = (byte)(value & 127UL);
				byte b2 = ((value > 127UL) ? 128 : 0);
				dest[num++] = b | b2;
				value >>= 7;
			}
			while (value > 0UL);
			numBytes = num - offset;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00011140 File Offset: 0x0000F340
		public static void Write(this Stream s, IEnumerable<byte> bytes)
		{
			foreach (byte b in bytes)
			{
				s.WriteByte(b);
			}
		}
	}
}
