using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Client.Packaging.BinarySerialization
{
	// Token: 0x02000018 RID: 24
	public class BinarySerializationReader : IDisposable
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000031E0 File Offset: 0x000013E0
		public BinarySerializationReader(Stream stream)
		{
			this.reader = new BinaryReader(stream);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031F4 File Offset: 0x000013F4
		void IDisposable.Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003204 File Offset: 0x00001404
		public void ReadDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Func<BinarySerializationReader, TKey> keyFunc, Func<BinarySerializationReader, TValue> valueFunc)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				TKey tkey = keyFunc.Invoke(this);
				TValue tvalue = valueFunc.Invoke(this);
				dictionary.Add(tkey, tvalue);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003244 File Offset: 0x00001444
		public T[] ReadArray<T>() where T : IBinarySerializable, new()
		{
			int num = this.reader.ReadInt32();
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				T t = this.Read<T>();
				array[i] = t;
			}
			return array;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003280 File Offset: 0x00001480
		public T[] ReadArray<T>(Func<BinarySerializationReader, T> func)
		{
			return this.ReadAnyArray<T>(func);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003289 File Offset: 0x00001489
		public T[] ReadNullableArray<T>() where T : IBinarySerializable, new()
		{
			return this.ReadNullable<T[]>((BinarySerializationReader r) => r.ReadArray<T>());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032B0 File Offset: 0x000014B0
		public T[] ReadNullableArray<T>(Func<BinarySerializationReader, T> func)
		{
			if (this.ReadBool())
			{
				return this.ReadAnyArray<T>(func);
			}
			return null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000032C4 File Offset: 0x000014C4
		public List<T> ReadList<T>() where T : IBinarySerializable, new()
		{
			List<T> list = new List<T>();
			this.ReadList<T>(list);
			return list;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000032E0 File Offset: 0x000014E0
		public void ReadList<T>(List<T> list) where T : IBinarySerializable, new()
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				T t = this.Read<T>();
				list.Add(t);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003314 File Offset: 0x00001514
		public List<T> ReadList<T>(Func<BinarySerializationReader, T> func)
		{
			List<T> list = new List<T>();
			this.ReadList<T>(list, func);
			return list;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003330 File Offset: 0x00001530
		public void ReadList<T>(List<T> list, Func<BinarySerializationReader, T> func)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				T t = func.Invoke(this);
				list.Add(t);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003364 File Offset: 0x00001564
		public T Read<T>() where T : IBinarySerializable, new()
		{
			T t = new T();
			t.Deserialize(this);
			return t;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003388 File Offset: 0x00001588
		public byte[] ReadBytes()
		{
			int num = this.reader.ReadInt32();
			return this.reader.ReadBytes(num);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000033AD File Offset: 0x000015AD
		public int ReadInt()
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000033BA File Offset: 0x000015BA
		public bool ReadBool()
		{
			return this.reader.ReadBoolean();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000033C8 File Offset: 0x000015C8
		public T? ReadOptional<T>(Func<BinarySerializationReader, T> func) where T : struct
		{
			bool flag = this.reader.ReadBoolean();
			if (flag)
			{
				return new T?(func.Invoke(this));
			}
			return default(T?);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000033FC File Offset: 0x000015FC
		public T ReadNullable<T>() where T : class, IBinarySerializable, new()
		{
			bool flag = this.reader.ReadBoolean();
			if (flag)
			{
				return this.Read<T>();
			}
			return default(T);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003428 File Offset: 0x00001628
		public T ReadNullable<T>(Func<BinarySerializationReader, T> func) where T : class
		{
			bool flag = this.reader.ReadBoolean();
			if (flag)
			{
				return func.Invoke(this);
			}
			return default(T);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003455 File Offset: 0x00001655
		public string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003462 File Offset: 0x00001662
		public string[] ReadStringArray()
		{
			return this.ReadAnyArray<string>((BinarySerializationReader reader) => reader.ReadString());
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003489 File Offset: 0x00001689
		public long ReadLong()
		{
			return this.reader.ReadInt64();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003496 File Offset: 0x00001696
		public double ReadDouble()
		{
			return this.reader.ReadDouble();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034A4 File Offset: 0x000016A4
		public DateTime ReadDateTime()
		{
			long num = this.reader.ReadInt64();
			return DateTime.FromBinary(num);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000034C4 File Offset: 0x000016C4
		public T ReadAny<T>(Func<BinarySerializationReader, T>[] funcs) where T : IBinarySerializable
		{
			int num = this.reader.ReadInt32();
			if (num < 0 || num >= funcs.Length)
			{
				throw new InvalidOperationException();
			}
			Func<BinarySerializationReader, T> func = funcs[num];
			return func.Invoke(this);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000034F8 File Offset: 0x000016F8
		public Guid ReadGuid()
		{
			return new Guid(this.reader.ReadBytes(16));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000350C File Offset: 0x0000170C
		public string ReadNullableString()
		{
			return this.ReadNullable<string>((BinarySerializationReader r) => r.ReadString());
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003533 File Offset: 0x00001733
		public DateTime? ReadOptionalDateTime()
		{
			return this.ReadOptional<DateTime>((BinarySerializationReader r) => r.ReadDateTime());
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000355A File Offset: 0x0000175A
		public int? ReadOptionalInt()
		{
			return this.ReadOptional<int>((BinarySerializationReader r) => r.ReadInt());
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003581 File Offset: 0x00001781
		public bool? ReadOptionalBool()
		{
			return this.ReadOptional<bool>((BinarySerializationReader r) => r.ReadBool());
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000035A8 File Offset: 0x000017A8
		public long? ReadOptionalLong()
		{
			return this.ReadOptional<long>((BinarySerializationReader r) => r.ReadLong());
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000035CF File Offset: 0x000017CF
		public bool EndOfStream
		{
			get
			{
				return this.reader.PeekChar() == -1;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000035E0 File Offset: 0x000017E0
		private T[] ReadAnyArray<T>(Func<BinarySerializationReader, T> func)
		{
			int num = this.reader.ReadInt32();
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				T t = func.Invoke(this);
				array[i] = t;
			}
			return array;
		}

		// Token: 0x0400005F RID: 95
		private readonly BinaryReader reader;
	}
}
