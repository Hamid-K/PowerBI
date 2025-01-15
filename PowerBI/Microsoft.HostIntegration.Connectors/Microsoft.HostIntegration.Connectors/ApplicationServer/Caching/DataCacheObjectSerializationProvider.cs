using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E1 RID: 481
	internal class DataCacheObjectSerializationProvider
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x000356B8 File Offset: 0x000338B8
		public DataCacheObjectSerializationProvider(DataCacheSerializationProperties cacheSerializationProperties)
		{
			this.preferredSerializer = cacheSerializationProperties.CacheObjectSerializerType;
			if (cacheSerializationProperties.CacheObjectSerializerType == DataCacheObjectSerializerType.CustomSerializer)
			{
				if (cacheSerializationProperties.CustomCacheObjectSerializer == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "CustomSerializerNotSpecified"));
				}
				this.legacySerializerDictionary.Add(6, cacheSerializationProperties.CustomCacheObjectSerializer);
			}
			this.legacySerializerDictionary.Add(0, DataCacheObjectSerializationProvider.NetDataContractCacheObjectSerializer);
			this.legacySerializerDictionary.Add(5, DataCacheObjectSerializationProvider.BinaryFormatCacheObjectSerializer);
			this.legacySerializerDictionary.Add(2, new SessionStoreProviderDataSerializer());
			this.legacySerializerDictionary.Add(1, new BinaryArrayCacheObjectSerializer());
			this.legacySerializerDictionary.Add(4, new NativeCacheObjectSerializer(ValueFlagsVersion.LegacyWcfType));
			this.legacySerializerDictionary.Add(3, new InternalDataContractCacheObjectSerializer());
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00035784 File Offset: 0x00033984
		public byte[][] SerializeUserObject(object userObject, bool isCompressionEnabled, ValueFlagsVersion flagsType)
		{
			if (flagsType == ValueFlagsVersion.EitherType)
			{
				throw new ArgumentException("Can serialize only in either VW or WCF format.", "flagsType");
			}
			byte serializerCode = this.GetSerializerCode(userObject);
			IDataCacheObjectSerializer dataCacheObjectSerializer = this.legacySerializerDictionary[serializerCode];
			ChunkStream chunkStream = null;
			ISizeBasedCacheObjectSerializer sizeBasedCacheObjectSerializer = dataCacheObjectSerializer as ISizeBasedCacheObjectSerializer;
			IChunkedArrayBackedSerializer chunkedArrayBackedSerializer = dataCacheObjectSerializer as IChunkedArrayBackedSerializer;
			byte[][] array2;
			try
			{
				byte[][] array;
				if (flagsType == ValueFlagsVersion.WireProtocolType && chunkedArrayBackedSerializer != null && chunkedArrayBackedSerializer.TrySerialize(userObject, isCompressionEnabled, out array))
				{
					array2 = array;
				}
				else
				{
					if (sizeBasedCacheObjectSerializer != null)
					{
						int num = sizeBasedCacheObjectSerializer.EstimateSerializationSize(userObject);
						int num2 = 2;
						if (flagsType == ValueFlagsVersion.WireProtocolType)
						{
							num2 = ValueFlagsUtility.GetExtraFlagsSize(serializerCode);
						}
						isCompressionEnabled = isCompressionEnabled && num > 1024;
						if (!isCompressionEnabled)
						{
							chunkStream = new ChunkStream(num + num2);
						}
					}
					if (chunkStream == null)
					{
						chunkStream = new ChunkStream();
					}
					if (flagsType == ValueFlagsVersion.LegacyWcfType)
					{
						if (isCompressionEnabled)
						{
							chunkStream.WriteByte(64);
							chunkStream.WriteByte(serializerCode);
						}
						else
						{
							chunkStream.WriteByte(serializerCode);
							chunkStream.WriteByte(serializerCode);
						}
					}
					else
					{
						switch (ValueFlagsUtility.SerializeUserObject(userObject, chunkStream, serializerCode, isCompressionEnabled))
						{
						case ValueFlagsUtility.SerializationResult.Completed:
							return chunkStream.ToChunkedArray();
						case ValueFlagsUtility.SerializationResult.HandOffToLegacy:
							flagsType = ValueFlagsVersion.LegacyWcfType;
							break;
						}
					}
					if (flagsType != ValueFlagsVersion.LegacyWcfType)
					{
						throw new SerializationException();
					}
					if (isCompressionEnabled)
					{
						using (DeflateStream deflateStream = new DeflateStream(chunkStream, CompressionMode.Compress))
						{
							dataCacheObjectSerializer.Serialize(deflateStream, userObject);
							goto IL_011D;
						}
					}
					dataCacheObjectSerializer.Serialize(chunkStream, userObject);
					IL_011D:
					array2 = chunkStream.ToChunkedArray();
				}
			}
			finally
			{
				if (chunkStream != null)
				{
					chunkStream.Dispose();
				}
			}
			return array2;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000358E8 File Offset: 0x00033AE8
		private byte GetSerializerCode(object obj)
		{
			Type type = obj.GetType();
			if (this.preferredSerializer == DataCacheObjectSerializerType.CustomSerializer)
			{
				return 6;
			}
			if (type == typeof(byte[]))
			{
				return 1;
			}
			if (type == typeof(SessionStoreProviderData))
			{
				return 2;
			}
			if (type == typeof(string) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong) || type == typeof(short) || type == typeof(ushort) || type == typeof(double) || type == typeof(float) || type == typeof(decimal) || type == typeof(bool) || type == typeof(byte) || type == typeof(sbyte))
			{
				return 4;
			}
			DataCacheObjectSerializerType dataCacheObjectSerializerType = this.preferredSerializer;
			if (dataCacheObjectSerializerType == DataCacheObjectSerializerType.NetDataContractSerializer)
			{
				return 0;
			}
			if (dataCacheObjectSerializerType != DataCacheObjectSerializerType.BinaryFormatter)
			{
				throw new ArgumentException(this.preferredSerializer.ToString());
			}
			return 5;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00035A54 File Offset: 0x00033C54
		public object DeserializeUserObject(byte[][] serializedData, ValueFlagsVersion flagsType)
		{
			using (ChunkStream chunkStream = new ChunkStream(serializedData))
			{
				byte b = (byte)chunkStream.ReadByte();
				byte b2 = (byte)chunkStream.ReadByte();
				if ((flagsType & ValueFlagsVersion.WireProtocolType) != (ValueFlagsVersion)0)
				{
					IChunkedArrayBackedSerializer chunkedArrayBackedDeserializer = ValueFlagsUtility.GetChunkedArrayBackedDeserializer(b, b2);
					object obj;
					if (chunkedArrayBackedDeserializer != null && chunkedArrayBackedDeserializer.TryDeserialize(serializedData, out obj))
					{
						return obj;
					}
					switch (ValueFlagsUtility.DeserializeUserObject(chunkStream, ref b, ref b2, out obj))
					{
					case ValueFlagsUtility.SerializationResult.Completed:
						return obj;
					case ValueFlagsUtility.SerializationResult.HandOffToLegacy:
						flagsType |= ValueFlagsVersion.LegacyWcfType;
						break;
					}
				}
				if ((flagsType & ValueFlagsVersion.LegacyWcfType) != (ValueFlagsVersion)0)
				{
					Stream stream = chunkStream;
					try
					{
						if (b == 64)
						{
							stream = new DeflateStream(chunkStream, CompressionMode.Decompress);
						}
						else if (b != b2)
						{
							throw new SerializationException();
						}
						IDataCacheObjectSerializer dataCacheObjectSerializer;
						if (!this.legacySerializerDictionary.TryGetValue(b2, out dataCacheObjectSerializer))
						{
							throw new SerializationException();
						}
						return dataCacheObjectSerializer.Deserialize(stream);
					}
					finally
					{
						stream.Dispose();
					}
				}
			}
			throw new SerializationException();
		}

		// Token: 0x04000A9C RID: 2716
		internal const byte NativeSerializationMark = 4;

		// Token: 0x04000A9D RID: 2717
		private static readonly IDataCacheObjectSerializer NetDataContractCacheObjectSerializer = new NetDataContractCacheObjectSerializer();

		// Token: 0x04000A9E RID: 2718
		private static readonly IDataCacheObjectSerializer BinaryFormatCacheObjectSerializer = new BinaryFormatCacheObjectSerializer();

		// Token: 0x04000A9F RID: 2719
		private DataCacheObjectSerializerType preferredSerializer;

		// Token: 0x04000AA0 RID: 2720
		private readonly Dictionary<byte, IDataCacheObjectSerializer> legacySerializerDictionary = new Dictionary<byte, IDataCacheObjectSerializer>();
	}
}
