using System;
using System.IO;

namespace Microsoft.Mashup.Client.Packaging.BinarySerialization
{
	// Token: 0x0200001A RID: 26
	public class BinarySerializer
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003A8C File Offset: 0x00001C8C
		public static T DeserializeBytes<T>(byte[] bytes) where T : IBinarySerializable, new()
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			T t2;
			using (BinarySerializationReader binarySerializationReader = new BinarySerializationReader(memoryStream))
			{
				T t = new T();
				t.Deserialize(binarySerializationReader);
				t2 = t;
			}
			return t2;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003ADC File Offset: 0x00001CDC
		public static T DeserializeBytes<T>(byte[] bytes, Func<BinarySerializationReader, T> func)
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			T t;
			using (BinarySerializationReader binarySerializationReader = new BinarySerializationReader(memoryStream))
			{
				t = func.Invoke(binarySerializationReader);
			}
			return t;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003B1C File Offset: 0x00001D1C
		public static byte[] SerializeBytes<T>(T value) where T : IBinarySerializable, new()
		{
			return BinarySerializer.SerializeBytesCore<T>(value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003B24 File Offset: 0x00001D24
		public static byte[] SerializeBytes<T>(T value, Action<BinarySerializationWriter, T> writeFunc, Func<BinarySerializationReader, T> readFunc)
		{
			return BinarySerializer.SerializeBytesCore<T>(value, writeFunc);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003B30 File Offset: 0x00001D30
		private static byte[] SerializeBytesCore<T>(T value) where T : IBinarySerializable
		{
			MemoryStream memoryStream = new MemoryStream();
			using (BinarySerializationWriter binarySerializationWriter = new BinarySerializationWriter(memoryStream))
			{
				binarySerializationWriter.Write<T>(value);
				binarySerializationWriter.Flush();
			}
			return memoryStream.ToArray();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003B7C File Offset: 0x00001D7C
		private static byte[] SerializeBytesCore<T>(T value, Action<BinarySerializationWriter, T> func)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (BinarySerializationWriter binarySerializationWriter = new BinarySerializationWriter(memoryStream))
			{
				func.Invoke(binarySerializationWriter, value);
				binarySerializationWriter.Flush();
			}
			return memoryStream.ToArray();
		}
	}
}
