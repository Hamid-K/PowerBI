using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Client.Packaging.BinarySerialization
{
	// Token: 0x02000019 RID: 25
	public class BinarySerializationWriter : IDisposable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003620 File Offset: 0x00001820
		public BinarySerializationWriter(Stream stream)
		{
			if (BinarySerializationWriter.fallbackEncoding == null)
			{
				Encoding encoding = (Encoding)Encoding.UTF8.Clone();
				encoding.EncoderFallback = EncoderFallback.ReplacementFallback;
				BinarySerializationWriter.fallbackEncoding = encoding;
			}
			this.writer = new BinaryWriter(stream, BinarySerializationWriter.fallbackEncoding);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000366C File Offset: 0x0000186C
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003679 File Offset: 0x00001879
		void IDisposable.Dispose()
		{
			this.writer.Dispose();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003686 File Offset: 0x00001886
		public void Write<T>(T value) where T : IBinarySerializable
		{
			value.Serialize(this);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003698 File Offset: 0x00001898
		public void WriteDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Action<BinarySerializationWriter, TKey> keyFunc, Action<BinarySerializationWriter, TValue> valueFunc)
		{
			this.writer.Write(dictionary.Count);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				keyFunc.Invoke(this, keyValuePair.Key);
				valueFunc.Invoke(this, keyValuePair.Value);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000370C File Offset: 0x0000190C
		public void WriteArray<T>(T[] array) where T : IBinarySerializable
		{
			this.WriteInt(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Serialize(this);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003745 File Offset: 0x00001945
		public void WriteArray<T>(T[] array, Action<BinarySerializationWriter, T> action)
		{
			this.WriteAnyArray<T>(array, action);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000374F File Offset: 0x0000194F
		public void WriteNullableArray<T>(T[] array) where T : IBinarySerializable
		{
			this.WriteNullable<T[]>(array, delegate(BinarySerializationWriter w, T[] v)
			{
				w.WriteArray<T>(v);
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003777 File Offset: 0x00001977
		public void WriteNullableArray<T>(T[] array, Action<BinarySerializationWriter, T> action)
		{
			this.WriteBool(array != null);
			if (array != null)
			{
				this.WriteAnyArray<T>(array, action);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003790 File Offset: 0x00001990
		public void WriteList<T>(List<T> list) where T : IBinarySerializable
		{
			this.WriteInt(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				T t = list[i];
				t.Serialize(this);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000037D0 File Offset: 0x000019D0
		public void WriteList<T>(List<T> list, Action<BinarySerializationWriter, T> action)
		{
			this.WriteInt(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				action.Invoke(this, list[i]);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003808 File Offset: 0x00001A08
		public void WriteBytes(byte[] bytes)
		{
			this.writer.Write(bytes.Length);
			this.writer.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003828 File Offset: 0x00001A28
		public void WriteInt(int value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003836 File Offset: 0x00001A36
		public void WriteBool(bool value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003844 File Offset: 0x00001A44
		public void WriteString(string value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003852 File Offset: 0x00001A52
		public void WriteStringArray(string[] array)
		{
			this.WriteAnyArray<string>(array, delegate(BinarySerializationWriter writer, string stringValue)
			{
				writer.WriteString(stringValue);
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000387A File Offset: 0x00001A7A
		public void WriteLong(long value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003888 File Offset: 0x00001A88
		public void WriteDouble(double value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003896 File Offset: 0x00001A96
		public void WriteOptional<T>(T? value, Action<BinarySerializationWriter, T> action) where T : struct
		{
			if (value != null)
			{
				this.writer.Write(true);
				action.Invoke(this, value.Value);
				return;
			}
			this.writer.Write(false);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000038C8 File Offset: 0x00001AC8
		public void WriteNullable<T>(T value) where T : IBinarySerializable
		{
			if (value != null)
			{
				this.writer.Write(true);
				value.Serialize(this);
				return;
			}
			this.writer.Write(false);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000038F9 File Offset: 0x00001AF9
		public void WriteNullable<T>(T value, Action<BinarySerializationWriter, T> action) where T : class
		{
			if (value != null)
			{
				this.writer.Write(true);
				action.Invoke(this, value);
				return;
			}
			this.writer.Write(false);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003924 File Offset: 0x00001B24
		public void WriteDateTime(DateTime value)
		{
			this.writer.Write(value.ToBinary());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003938 File Offset: 0x00001B38
		public void WriteAny<T>(T value, Func<T, bool>[] funcs) where T : IBinarySerializable
		{
			for (int i = 0; i < funcs.Length; i++)
			{
				if (funcs[i].Invoke(value))
				{
					this.WriteInt(i);
					value.Serialize(this);
					return;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003979 File Offset: 0x00001B79
		public void WriteGuid(Guid guid)
		{
			this.writer.Write(guid.ToByteArray());
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000398D File Offset: 0x00001B8D
		public void WriteNullableString(string value)
		{
			this.WriteNullable<string>(value, delegate(BinarySerializationWriter w, string v)
			{
				w.WriteString(v);
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000039B5 File Offset: 0x00001BB5
		public void WriteOptionalInt(int? value)
		{
			this.WriteOptional<int>(value, delegate(BinarySerializationWriter w, int v)
			{
				w.WriteInt(v);
			});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000039DD File Offset: 0x00001BDD
		public void WriteOptionalLong(long? value)
		{
			this.WriteOptional<long>(value, delegate(BinarySerializationWriter w, long v)
			{
				w.WriteLong(v);
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A05 File Offset: 0x00001C05
		public void WriteOptionalDateTime(DateTime? value)
		{
			this.WriteOptional<DateTime>(value, delegate(BinarySerializationWriter w, DateTime v)
			{
				w.WriteDateTime(v);
			});
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A2D File Offset: 0x00001C2D
		public void WriteOptionalBool(bool? value)
		{
			this.WriteOptional<bool>(value, delegate(BinarySerializationWriter w, bool v)
			{
				w.WriteBool(v);
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A58 File Offset: 0x00001C58
		private void WriteAnyArray<T>(T[] array, Action<BinarySerializationWriter, T> action)
		{
			this.WriteInt(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				action.Invoke(this, array[i]);
			}
		}

		// Token: 0x04000060 RID: 96
		private static Encoding fallbackEncoding;

		// Token: 0x04000061 RID: 97
		private readonly BinaryWriter writer;
	}
}
