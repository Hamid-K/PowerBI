using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E0 RID: 480
	internal sealed class PrimitiveDataCacheObjectSerializationProvider
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00035348 File Offset: 0x00033548
		private PrimitiveDataCacheObjectSerializationProvider(ValueFlagsVersion ver)
		{
			this.valueFlagsVersion = ver;
			this.objectSerializer = new NativeCacheObjectSerializer(ver);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00035364 File Offset: 0x00033564
		public object DeserializeUserObject(byte[][] serializedData, SerializationCategory serializationInformation)
		{
			bool flag;
			return this.DeserializeUserObject(serializedData, serializationInformation, out flag);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003537C File Offset: 0x0003357C
		public object DeserializeUserObject(byte[][] serializedData, SerializationCategory serializationInformation, out bool isCompressionEnabled)
		{
			isCompressionEnabled = false;
			if (serializationInformation == SerializationCategory.Native)
			{
				try
				{
					using (ChunkStream chunkStream = new ChunkStream(serializedData))
					{
						byte b;
						byte b2;
						switch (this.valueFlagsVersion)
						{
						case ValueFlagsVersion.LegacyWcfType:
						{
							b = (byte)chunkStream.ReadByte();
							b2 = (byte)chunkStream.ReadByte();
							if (b == 4 && b2 == 4)
							{
								return this.objectSerializer.Deserialize(chunkStream);
							}
							if (b != 64 || b2 != 4)
							{
								goto IL_0100;
							}
							isCompressionEnabled = true;
							using (Stream stream = new DeflateStream(chunkStream, CompressionMode.Decompress))
							{
								return this.objectSerializer.Deserialize(chunkStream, stream);
							}
							break;
						}
						case ValueFlagsVersion.WireProtocolType:
							break;
						default:
							goto IL_0100;
						}
						b = (byte)chunkStream.ReadByte();
						b2 = (byte)chunkStream.ReadByte();
						byte b3 = (byte)chunkStream.ReadByte();
						if ((b & 128) == 128 && b2 == 2 && b3 == 0)
						{
							if ((b & 64) != 0)
							{
								isCompressionEnabled = true;
								using (Stream stream2 = new DeflateStream(chunkStream, CompressionMode.Decompress))
								{
									return this.objectSerializer.Deserialize(chunkStream, stream2);
								}
							}
							return this.objectSerializer.Deserialize(chunkStream);
						}
						IL_0100:;
					}
				}
				catch (SerializationException ex)
				{
					throw new DataCacheException("SerializationProvider", 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), ex, true);
				}
			}
			throw new DataCacheException("SerializationProvider", 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), true);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00035538 File Offset: 0x00033738
		public byte[][] SerializeUserObject(object userObject, SerializationCategory serializationInformation)
		{
			return this.SerializeUserObject(userObject, serializationInformation, false);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00035544 File Offset: 0x00033744
		public byte[][] SerializeUserObject(object userObject, SerializationCategory serializationInformation, bool isCompressionEnabled)
		{
			if (serializationInformation == SerializationCategory.Native)
			{
				ChunkStream chunkStream = new ChunkStream();
				try
				{
					switch (this.valueFlagsVersion)
					{
					case ValueFlagsVersion.LegacyWcfType:
					{
						byte b;
						if (isCompressionEnabled)
						{
							b = 64;
						}
						else
						{
							b = 4;
						}
						chunkStream.WriteByte(b);
						chunkStream.WriteByte(4);
						if (isCompressionEnabled)
						{
							using (Stream stream = new DeflateStream(chunkStream, CompressionMode.Compress))
							{
								this.objectSerializer.Serialize(stream, userObject);
								goto IL_0073;
							}
						}
						this.objectSerializer.Serialize(chunkStream, userObject);
						IL_0073:
						return chunkStream.ToChunkedArray();
					}
					case ValueFlagsVersion.WireProtocolType:
					{
						byte b = 128;
						if (isCompressionEnabled)
						{
							b = 192;
						}
						chunkStream.WriteByte(b);
						chunkStream.WriteByte(2);
						chunkStream.WriteByte(0);
						if (isCompressionEnabled)
						{
							using (Stream stream2 = new DeflateStream(chunkStream, CompressionMode.Compress))
							{
								this.objectSerializer.Serialize(chunkStream, stream2, userObject);
								goto IL_00D6;
							}
						}
						this.objectSerializer.Serialize(chunkStream, userObject);
						IL_00D6:
						return chunkStream.ToChunkedArray();
					}
					}
				}
				catch (SerializationException)
				{
					throw new DataCacheException("SerializationProvider", 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), true);
				}
				finally
				{
					chunkStream.Dispose();
				}
			}
			throw new DataCacheException("SerializationProvider", 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), true);
		}

		// Token: 0x04000A99 RID: 2713
		public static readonly PrimitiveDataCacheObjectSerializationProvider WireProtocolType = new PrimitiveDataCacheObjectSerializationProvider(ValueFlagsVersion.WireProtocolType);

		// Token: 0x04000A9A RID: 2714
		private readonly ValueFlagsVersion valueFlagsVersion;

		// Token: 0x04000A9B RID: 2715
		private readonly NativeCacheObjectSerializer objectSerializer;
	}
}
