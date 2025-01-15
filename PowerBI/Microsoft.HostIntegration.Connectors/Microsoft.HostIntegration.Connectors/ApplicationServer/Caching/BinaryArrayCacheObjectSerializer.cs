using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001DE RID: 478
	internal class BinaryArrayCacheObjectSerializer : ISizeBasedCacheObjectSerializer, IDataCacheObjectSerializer, IChunkedArrayBackedSerializer
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x00035140 File Offset: 0x00033340
		public void Serialize(Stream stream, object value)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] array = value as byte[];
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003517C File Offset: 0x0003337C
		public object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			object obj;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[1024];
				for (;;)
				{
					int num = stream.Read(array, 0, 1024);
					if (num == 0)
					{
						break;
					}
					memoryStream.Write(array, 0, num);
				}
				obj = memoryStream.ToArray();
			}
			return obj;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000351E8 File Offset: 0x000333E8
		public int EstimateSerializationSize(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] array = value as byte[];
			if (array == null)
			{
				throw new SerializationException();
			}
			return array.Length;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00035218 File Offset: 0x00033418
		public bool TrySerialize(object value, bool isCompressionEnabled, out byte[][] serialized)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] array = value as byte[];
			if (array == null)
			{
				throw new SerializationException();
			}
			isCompressionEnabled = isCompressionEnabled && array.Length > 1024;
			if (!isCompressionEnabled)
			{
				serialized = new byte[][]
				{
					ValueFlagsUtility.ByteArrayPrefix,
					array
				};
				return true;
			}
			serialized = null;
			return false;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00035274 File Offset: 0x00033474
		public bool TryDeserialize(byte[][] serializedData, out object deserialized)
		{
			if (serializedData.Length == 2 && serializedData[0].Length == 4 && serializedData[0].SequenceEqual(ValueFlagsUtility.ByteArrayPrefix))
			{
				deserialized = serializedData[1];
				return true;
			}
			if (serializedData.Length == 1 && serializedData[0].Length > 4 && serializedData[0].SequenceEqual(ValueFlagsUtility.ByteArrayPrefix))
			{
				byte[] array = new byte[serializedData[0].Length - 4];
				Array.Copy(serializedData[0], 4, array, 0, array.Length);
				deserialized = array;
				return true;
			}
			deserialized = null;
			return false;
		}
	}
}
