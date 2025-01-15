using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BD6 RID: 7126
	public static class BinaryReaderWriterExtensions
	{
		// Token: 0x0600B1C3 RID: 45507 RVA: 0x00244371 File Offset: 0x00242571
		public static void WriteByte(this BinaryWriter writer, byte value)
		{
			writer.Write(value);
		}

		// Token: 0x0600B1C4 RID: 45508 RVA: 0x001DB6AE File Offset: 0x001D98AE
		public static void WriteBool(this BinaryWriter writer, bool value)
		{
			writer.Write(value);
		}

		// Token: 0x0600B1C5 RID: 45509 RVA: 0x001DB337 File Offset: 0x001D9537
		public static bool ReadBool(this BinaryReader reader)
		{
			return reader.ReadBoolean();
		}

		// Token: 0x0600B1C6 RID: 45510 RVA: 0x001DB33F File Offset: 0x001D953F
		public static string ReadString(this BinaryReader reader)
		{
			return reader.ReadString();
		}

		// Token: 0x0600B1C7 RID: 45511 RVA: 0x0024437C File Offset: 0x0024257C
		public unsafe static SegmentedString ReadSegmentedString(this BinaryReader reader)
		{
			int num = reader.ReadInt32();
			SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
			Decoder decoder = BinaryReaderWriterExtensions.utf8WithoutPreamble.GetDecoder();
			byte[] array = new byte[256];
			char[] array2 = new char[Math.Min(32768, num)];
			int num2 = 0;
			int num3 = 0;
			bool flag = true;
			char[] array3;
			char* ptr;
			if ((array3 = array2) == null || array3.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array3[0];
			}
			byte[] array4;
			byte* ptr2;
			if ((array4 = array) == null || array4.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array4[0];
			}
			while (num > 0 || num3 > 0)
			{
				int num4 = 0;
				int num5 = Math.Min(num, array.Length - num3);
				if (num5 > 0)
				{
					num4 = reader.Read(array, num3, num5);
					num -= num4;
					if (num4 == 0)
					{
						throw new EndOfStreamException();
					}
				}
				int num6;
				int num7;
				decoder.Convert(ptr2, num3 + num4, ptr + num2, array2.Length - num2, num == 0, out num6, out num7, out flag);
				num3 = num3 + num4 - num6;
				for (int i = 0; i < num3; i++)
				{
					ptr2[i] = (ptr2 + num6)[i];
				}
				num2 += num7;
				if (num2 == 32768 || (num == 0 && flag))
				{
					segmentedStringBuilder.Append(new string(ptr, 0, num2));
					num2 = 0;
				}
			}
			array4 = null;
			array3 = null;
			return segmentedStringBuilder.ToSegmentedString();
		}

		// Token: 0x0600B1C8 RID: 45512 RVA: 0x001DB693 File Offset: 0x001D9893
		public static void WriteInt32(this BinaryWriter writer, int value)
		{
			writer.Write(value);
		}

		// Token: 0x0600B1C9 RID: 45513 RVA: 0x002444D6 File Offset: 0x002426D6
		public static void WriteGuid(this BinaryWriter writer, Guid value)
		{
			writer.WriteByteArray(value.ToByteArray());
		}

		// Token: 0x0600B1CA RID: 45514 RVA: 0x002444E5 File Offset: 0x002426E5
		public static void WriteInt64(this BinaryWriter writer, long value)
		{
			writer.Write(value);
		}

		// Token: 0x0600B1CB RID: 45515 RVA: 0x001DB6B7 File Offset: 0x001D98B7
		public static void WriteString(this BinaryWriter writer, string value)
		{
			writer.Write(value);
		}

		// Token: 0x0600B1CC RID: 45516 RVA: 0x002444F0 File Offset: 0x002426F0
		public unsafe static void WriteSegmentedString(this BinaryWriter writer, SegmentedString value)
		{
			int num = value.GetSubstringSegments(0, value.Length).Select(delegate(StringSegment segment)
			{
				fixed (string @string = segment.String)
				{
					char* ptr3 = @string;
					if (ptr3 != null)
					{
						ptr3 += RuntimeHelpers.OffsetToStringData / 2;
					}
					return Encoding.UTF8.GetByteCount(ptr3 + segment.Offset, segment.Length);
				}
			}).Sum();
			writer.Write(num);
			Encoder encoder = BinaryReaderWriterExtensions.utf8WithoutPreamble.GetEncoder();
			byte[] array = new byte[256];
			bool flag = true;
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			using (IEnumerator<StringSegment> enumerator = value.GetSubstringSegments(0, value.Length).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StringSegment stringSegment = enumerator.Current;
					try
					{
						fixed (string text = stringSegment.String)
						{
							char* ptr2 = text;
							if (ptr2 != null)
							{
								ptr2 += RuntimeHelpers.OffsetToStringData / 2;
							}
							int num2;
							for (int i = 0; i < stringSegment.Length; i += num2)
							{
								int num3;
								encoder.Convert(ptr2 + stringSegment.Offset + i, stringSegment.Length - i, ptr, array.Length, false, out num2, out num3, out flag);
								writer.Write(array, 0, num3);
							}
						}
					}
					finally
					{
						string text = null;
					}
				}
				goto IL_013F;
			}
			IL_0120:
			int num4;
			int num5;
			encoder.Convert(null, 0, ptr, array.Length, true, out num4, out num5, out flag);
			writer.Write(array, 0, num5);
			IL_013F:
			if (flag)
			{
				array2 = null;
				return;
			}
			goto IL_0120;
		}

		// Token: 0x0600B1CD RID: 45517 RVA: 0x00244660 File Offset: 0x00242860
		public static void WriteDateTime(this BinaryWriter writer, DateTime value)
		{
			writer.Write(value.ToBinary());
		}

		// Token: 0x0600B1CE RID: 45518 RVA: 0x0024466F File Offset: 0x0024286F
		public static DateTime ReadDateTime(this BinaryReader reader)
		{
			return DateTime.FromBinary(reader.ReadInt64());
		}

		// Token: 0x0600B1CF RID: 45519 RVA: 0x001DB645 File Offset: 0x001D9845
		public static void WriteTimeSpan(this BinaryWriter writer, TimeSpan value)
		{
			writer.Write(value.Ticks);
		}

		// Token: 0x0600B1D0 RID: 45520 RVA: 0x0024467C File Offset: 0x0024287C
		public static TimeSpan ReadTimeSpan(this BinaryReader reader)
		{
			return TimeSpan.FromTicks(reader.ReadInt64());
		}

		// Token: 0x0600B1D1 RID: 45521 RVA: 0x00244689 File Offset: 0x00242889
		public static void WriteDateTimeOffset(this BinaryWriter writer, DateTimeOffset value)
		{
			writer.Write(value.Ticks);
			writer.WriteTimeSpan(value.Offset);
		}

		// Token: 0x0600B1D2 RID: 45522 RVA: 0x002446A8 File Offset: 0x002428A8
		public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
		{
			long num = reader.ReadInt64();
			TimeSpan timeSpan = reader.ReadTimeSpan();
			return new DateTimeOffset(num, timeSpan);
		}

		// Token: 0x0600B1D3 RID: 45523 RVA: 0x002446C8 File Offset: 0x002428C8
		public static Guid ReadGuid(this BinaryReader reader)
		{
			return new Guid(reader.ReadByteArray());
		}

		// Token: 0x0600B1D4 RID: 45524 RVA: 0x002446D5 File Offset: 0x002428D5
		public static void WriteNullableString(this BinaryWriter writer, string value)
		{
			writer.Write(value != null);
			if (value != null)
			{
				writer.Write(value);
			}
		}

		// Token: 0x0600B1D5 RID: 45525 RVA: 0x002446EB File Offset: 0x002428EB
		public static string ReadNullableString(this BinaryReader reader)
		{
			if (reader.ReadBool())
			{
				return reader.ReadString();
			}
			return null;
		}

		// Token: 0x0600B1D6 RID: 45526 RVA: 0x002446FD File Offset: 0x002428FD
		public static void WriteNullableInt(this BinaryWriter writer, int? value)
		{
			writer.Write(value != null);
			if (value != null)
			{
				writer.WriteInt32(value.Value);
			}
		}

		// Token: 0x0600B1D7 RID: 45527 RVA: 0x00244724 File Offset: 0x00242924
		public static int? ReadNullableInt(this BinaryReader reader)
		{
			if (!reader.ReadBool())
			{
				return null;
			}
			return new int?(reader.ReadInt32());
		}

		// Token: 0x0600B1D8 RID: 45528 RVA: 0x00244750 File Offset: 0x00242950
		public static void WriteNullableBool(this BinaryWriter writer, bool? value)
		{
			bool? flag = value;
			bool flag2 = true;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				writer.Write(1);
				return;
			}
			flag = value;
			flag2 = false;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				writer.Write(2);
				return;
			}
			writer.Write(0);
		}

		// Token: 0x0600B1D9 RID: 45529 RVA: 0x002447A4 File Offset: 0x002429A4
		public static bool? ReadNullableBool(this BinaryReader reader)
		{
			byte b = reader.ReadByte();
			if (b == 1)
			{
				return new bool?(true);
			}
			if (b != 2)
			{
				return null;
			}
			return new bool?(false);
		}

		// Token: 0x0600B1DA RID: 45530 RVA: 0x002447D9 File Offset: 0x002429D9
		public static void WriteNullable<T>(this BinaryWriter writer, T item, Action<BinaryWriter, T> writeItem) where T : class
		{
			writer.Write(item != null);
			if (item != null)
			{
				writeItem(writer, item);
			}
		}

		// Token: 0x0600B1DB RID: 45531 RVA: 0x002447FC File Offset: 0x002429FC
		public static T ReadNullable<T>(this BinaryReader reader, Func<BinaryReader, T> readItem) where T : class
		{
			if (reader.ReadBool())
			{
				return readItem(reader);
			}
			return default(T);
		}

		// Token: 0x0600B1DC RID: 45532 RVA: 0x00244824 File Offset: 0x00242A24
		public static void WriteArray<T>(this BinaryWriter writer, T[] array, Action<BinaryWriter, T> writeItem)
		{
			writer.Write(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				writeItem(writer, array[i]);
			}
		}

		// Token: 0x0600B1DD RID: 45533 RVA: 0x00244858 File Offset: 0x00242A58
		public static T[] ReadArray<T>(this BinaryReader reader, Func<BinaryReader, T> readItem)
		{
			int num = reader.ReadInt32();
			if (num == 0)
			{
				return EmptyArray<T>.Instance;
			}
			T[] array = new T[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = readItem(reader);
			}
			return array;
		}

		// Token: 0x0600B1DE RID: 45534 RVA: 0x0024489C File Offset: 0x00242A9C
		public static void WriteList<T>(this BinaryWriter writer, List<T> list, Action<BinaryWriter, T> writeItem)
		{
			writer.WriteInt32(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				writeItem(writer, list[i]);
			}
		}

		// Token: 0x0600B1DF RID: 45535 RVA: 0x002448D4 File Offset: 0x00242AD4
		public static List<T> ReadList<T>(this BinaryReader reader, Func<BinaryReader, T> readItem)
		{
			List<T> list = new List<T>();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				list.Add(readItem(reader));
			}
			return list;
		}

		// Token: 0x0600B1E0 RID: 45536 RVA: 0x00244908 File Offset: 0x00242B08
		public static void WriteCollection<T>(this BinaryWriter writer, ICollection<T> list, Action<BinaryWriter, T> writeItem)
		{
			writer.WriteInt32(list.Count);
			foreach (T t in list)
			{
				writeItem(writer, t);
			}
		}

		// Token: 0x0600B1E1 RID: 45537 RVA: 0x00244960 File Offset: 0x00242B60
		public static List<T> ReadCollection<T>(this BinaryReader reader, Func<BinaryReader, T> readItem)
		{
			return reader.ReadList(readItem);
		}

		// Token: 0x0600B1E2 RID: 45538 RVA: 0x0024496C File Offset: 0x00242B6C
		public static void WriteDictionary<K, V>(this BinaryWriter writer, Dictionary<K, V> dictionary, Action<BinaryWriter, K> writeKey, Action<BinaryWriter, V> writeValue)
		{
			writer.WriteInt32(dictionary.Count);
			foreach (KeyValuePair<K, V> keyValuePair in dictionary)
			{
				writeKey(writer, keyValuePair.Key);
				writeValue(writer, keyValuePair.Value);
			}
		}

		// Token: 0x0600B1E3 RID: 45539 RVA: 0x002449DC File Offset: 0x00242BDC
		public static void ReadDictionary<K, V>(this BinaryReader reader, Dictionary<K, V> dictionary, Func<BinaryReader, K> readKey, Func<BinaryReader, V> readValue)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				K k = readKey(reader);
				V v = readValue(reader);
				dictionary.Add(k, v);
			}
		}

		// Token: 0x0600B1E4 RID: 45540 RVA: 0x00244A14 File Offset: 0x00242C14
		public static void WriteByteArray(this BinaryWriter writer, byte[] array)
		{
			writer.Write(array.Length);
			writer.Write(array);
		}

		// Token: 0x0600B1E5 RID: 45541 RVA: 0x00244A28 File Offset: 0x00242C28
		public static byte[] ReadByteArray(this BinaryReader reader)
		{
			int num = reader.ReadInt32();
			byte[] array = reader.ReadBytes(num);
			if (num != array.Length)
			{
				throw new EndOfStreamException();
			}
			return array;
		}

		// Token: 0x0600B1E6 RID: 45542 RVA: 0x00244A54 File Offset: 0x00242C54
		public static void WriteAny<T>(this BinaryWriter writer, T value, Func<T, bool>[] identifiers, Action<BinaryWriter, T>[] writers)
		{
			for (int i = 0; i < identifiers.Length; i++)
			{
				if (identifiers[i](value))
				{
					writer.WriteInt32(i);
					writers[i](writer, value);
					return;
				}
			}
			string text = "Don't know how to serialize ";
			T t = value;
			throw new InvalidOperationException(text + ((t != null) ? t.ToString() : null));
		}

		// Token: 0x0600B1E7 RID: 45543 RVA: 0x00244AB8 File Offset: 0x00242CB8
		public static T ReadAny<T>(this BinaryReader reader, Func<BinaryReader, T>[] readers)
		{
			int num = reader.ReadInt32();
			if (num < 0 || num >= readers.Length)
			{
				throw new InvalidOperationException();
			}
			return readers[num](reader);
		}

		// Token: 0x04005B1F RID: 23327
		private static readonly Encoding utf8WithoutPreamble = new UTF8Encoding(false);
	}
}
