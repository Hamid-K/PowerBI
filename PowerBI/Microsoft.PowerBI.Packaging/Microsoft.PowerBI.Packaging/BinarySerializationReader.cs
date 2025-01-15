using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000007 RID: 7
	public class BinarySerializationReader : IDisposable
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022F7 File Offset: 0x000004F7
		public BinarySerializationReader(Stream stream)
		{
			this.reader = new BinaryReader(stream);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000230B File Offset: 0x0000050B
		void IDisposable.Dispose()
		{
			((IDisposable)this.reader).Dispose();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002318 File Offset: 0x00000518
		public static T DeserializeBytes<T>(byte[] bytes) where T : IBinarySerializable, new()
		{
			T t2;
			using (BinarySerializationReader binarySerializationReader = new BinarySerializationReader(new MemoryStream(bytes)))
			{
				T t = new T();
				t.Deserialize(binarySerializationReader);
				t2 = t;
			}
			return t2;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002364 File Offset: 0x00000564
		public void ReadDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Func<BinarySerializationReader, TKey> keyFunc, Func<BinarySerializationReader, TValue> valueFunc)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				TKey tkey = keyFunc(this);
				TValue tvalue = valueFunc(this);
				dictionary.Add(tkey, tvalue);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A4 File Offset: 0x000005A4
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

		// Token: 0x06000015 RID: 21 RVA: 0x000023E0 File Offset: 0x000005E0
		public T[] ReadArray<T>(Func<BinarySerializationReader, T> func)
		{
			return this.ReadAnyArray<T>(func);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023E9 File Offset: 0x000005E9
		public T[] ReadNullableArray<T>() where T : IBinarySerializable, new()
		{
			return this.ReadNullable<T[]>((BinarySerializationReader r) => r.ReadArray<T>());
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002410 File Offset: 0x00000610
		public T[] ReadNullableArray<T>(Func<BinarySerializationReader, T> func)
		{
			if (this.ReadBool())
			{
				return this.ReadAnyArray<T>(func);
			}
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002424 File Offset: 0x00000624
		public List<T> ReadList<T>() where T : IBinarySerializable, new()
		{
			List<T> list = new List<T>();
			this.ReadList<T>(list);
			return list;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002440 File Offset: 0x00000640
		public void ReadList<T>(List<T> list) where T : IBinarySerializable, new()
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				T t = this.Read<T>();
				list.Add(t);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002474 File Offset: 0x00000674
		public List<T> ReadList<T>(Func<BinarySerializationReader, T> func)
		{
			List<T> list = new List<T>();
			this.ReadList<T>(list, func);
			return list;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002490 File Offset: 0x00000690
		public void ReadList<T>(List<T> list, Func<BinarySerializationReader, T> func)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				T t = func(this);
				list.Add(t);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024C4 File Offset: 0x000006C4
		public T Read<T>() where T : IBinarySerializable, new()
		{
			T t = new T();
			t.Deserialize(this);
			return t;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024E8 File Offset: 0x000006E8
		public byte[] ReadBytes()
		{
			int num = this.reader.ReadInt32();
			return this.reader.ReadBytes(num);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000250D File Offset: 0x0000070D
		public int ReadInt()
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000251A File Offset: 0x0000071A
		public bool ReadBool()
		{
			return this.reader.ReadBoolean();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002528 File Offset: 0x00000728
		public T? ReadOptional<T>(Func<BinarySerializationReader, T> func) where T : struct
		{
			if (this.reader.ReadBoolean())
			{
				return new T?(func(this));
			}
			return null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002558 File Offset: 0x00000758
		public T ReadNullable<T>() where T : class, IBinarySerializable, new()
		{
			if (this.reader.ReadBoolean())
			{
				return this.Read<T>();
			}
			return default(T);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002584 File Offset: 0x00000784
		public T ReadNullable<T>(Func<BinarySerializationReader, T> func) where T : class
		{
			if (this.reader.ReadBoolean())
			{
				return func(this);
			}
			return default(T);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025AF File Offset: 0x000007AF
		public string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025BC File Offset: 0x000007BC
		public string[] ReadStringArray()
		{
			return this.ReadAnyArray<string>((BinarySerializationReader reader) => reader.ReadString());
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025E3 File Offset: 0x000007E3
		public long ReadLong()
		{
			return this.reader.ReadInt64();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025F0 File Offset: 0x000007F0
		public double ReadDouble()
		{
			return this.reader.ReadDouble();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025FD File Offset: 0x000007FD
		public DateTime ReadDateTime()
		{
			return DateTime.FromBinary(this.reader.ReadInt64());
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002610 File Offset: 0x00000810
		public T ReadAny<T>(Func<BinarySerializationReader, T>[] funcs) where T : IBinarySerializable
		{
			int num = this.reader.ReadInt32();
			if (num < 0 || num >= funcs.Length)
			{
				throw new InvalidOperationException();
			}
			return funcs[num](this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002642 File Offset: 0x00000842
		public Guid ReadGuid()
		{
			return new Guid(this.reader.ReadBytes(16));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002656 File Offset: 0x00000856
		public string ReadNullableString()
		{
			return this.ReadNullable<string>((BinarySerializationReader r) => r.ReadString());
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000267D File Offset: 0x0000087D
		public DateTime? ReadOptionalDateTime()
		{
			return this.ReadOptional<DateTime>((BinarySerializationReader r) => r.ReadDateTime());
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026A4 File Offset: 0x000008A4
		public int? ReadOptionalInt()
		{
			return this.ReadOptional<int>((BinarySerializationReader r) => r.ReadInt());
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026CB File Offset: 0x000008CB
		public bool? ReadOptionalBool()
		{
			return this.ReadOptional<bool>((BinarySerializationReader r) => r.ReadBool());
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026F2 File Offset: 0x000008F2
		public long? ReadOptionalLong()
		{
			return this.ReadOptional<long>((BinarySerializationReader r) => r.ReadLong());
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002719 File Offset: 0x00000919
		public bool EndOfStream
		{
			get
			{
				return this.reader.PeekChar() == -1;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000272C File Offset: 0x0000092C
		private T[] ReadAnyArray<T>(Func<BinarySerializationReader, T> func)
		{
			int num = this.reader.ReadInt32();
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				T t = func(this);
				array[i] = t;
			}
			return array;
		}

		// Token: 0x04000009 RID: 9
		private readonly BinaryReader reader;
	}
}
