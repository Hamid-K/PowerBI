using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001D4 RID: 468
	internal static class ValueFlagsUtility
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003381D File Offset: 0x00031A1D
		internal static byte[] ByteArrayPrefix
		{
			get
			{
				return ValueFlagsUtility._byteArrayVwPrefix;
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00033824 File Offset: 0x00031A24
		public static IChunkedArrayBackedSerializer GetChunkedArrayBackedDeserializer(byte firstByte, byte secondByte)
		{
			if (firstByte == 128 && secondByte == 1)
			{
				return ValueFlagsUtility._byteArraySerializer;
			}
			return null;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003383C File Offset: 0x00031A3C
		public static byte[][] GetChunkedArray(byte[] valueFlags, ArraySegment<byte> valueSegment, bool isRequest)
		{
			if (valueSegment.Count == 0)
			{
				return new byte[][] { valueFlags };
			}
			if (!isRequest && valueFlags[0] == 128 && valueFlags[1] == 1 && valueFlags[2] == 0 && valueFlags[3] == 0)
			{
				byte[] array = new byte[valueSegment.Count];
				Array.Copy(valueSegment.Array, valueSegment.Offset, array, 0, valueSegment.Count);
				return new byte[][] { valueFlags, array };
			}
			byte[][] array2;
			using (ChunkStream chunkStream = new ChunkStream(4 + valueSegment.Count))
			{
				chunkStream.Write(valueFlags, 0, 4);
				chunkStream.Write(valueSegment.Array, valueSegment.Offset, valueSegment.Count);
				array2 = chunkStream.ToChunkedArray();
			}
			return array2;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00033914 File Offset: 0x00031B14
		internal static int GetExtraFlagsSize(byte serializerCode)
		{
			if (serializerCode != 1)
			{
				switch (serializerCode)
				{
				case 4:
					return 3;
				case 6:
					return 2;
				}
			}
			return 4;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00033944 File Offset: 0x00031B44
		public static ValueFlagsUtility.SerializationResult SerializeUserObject(object userObject, Stream stream, byte serializerCode, bool isCompressionEnabled)
		{
			byte b = (isCompressionEnabled ? 64 : 0);
			if (serializerCode != 1)
			{
				switch (serializerCode)
				{
				case 4:
					stream.WriteByte(128 | b);
					stream.WriteByte(2);
					stream.WriteByte(0);
					if (isCompressionEnabled)
					{
						using (Stream stream2 = new DeflateStream(stream, CompressionMode.Compress))
						{
							ValueFlagsUtility._nativeSerializer.Serialize(stream, stream2, userObject);
							return ValueFlagsUtility.SerializationResult.Completed;
						}
					}
					ValueFlagsUtility._nativeSerializer.Serialize(stream, userObject);
					return ValueFlagsUtility.SerializationResult.Completed;
				case 6:
					stream.WriteByte(32 | b);
					stream.WriteByte(0);
					return ValueFlagsUtility.SerializationResult.HandOffToLegacy;
				}
				stream.WriteByte(16);
				stream.WriteByte(0);
				if (isCompressionEnabled)
				{
					stream.WriteByte(64);
				}
				else
				{
					stream.WriteByte(serializerCode);
				}
				stream.WriteByte(serializerCode);
				return ValueFlagsUtility.SerializationResult.HandOffToLegacy;
			}
			stream.WriteByte(128 | b);
			stream.WriteByte(1);
			stream.WriteByte(0);
			stream.WriteByte(0);
			return ValueFlagsUtility.SerializationResult.HandOffToLegacy;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00033A3C File Offset: 0x00031C3C
		public static ValueFlagsUtility.SerializationResult DeserializeUserObject(Stream stream, ref byte firstByte, ref byte secondByte, out object deserialized)
		{
			deserialized = null;
			bool flag = (firstByte & 64) != 0;
			byte b = firstByte;
			if (b <= 32)
			{
				if (b != 16)
				{
					if (b != 32)
					{
						return ValueFlagsUtility.SerializationResult.Failed;
					}
				}
				else
				{
					if (secondByte == 0)
					{
						firstByte = (byte)stream.ReadByte();
						secondByte = (byte)stream.ReadByte();
						return ValueFlagsUtility.SerializationResult.HandOffToLegacy;
					}
					return ValueFlagsUtility.SerializationResult.Failed;
				}
			}
			else if (b != 96)
			{
				if (b != 128 && b != 192)
				{
					return ValueFlagsUtility.SerializationResult.Failed;
				}
				if (secondByte == 1)
				{
					byte b2 = (byte)stream.ReadByte();
					byte b3 = (byte)stream.ReadByte();
					if (b2 == 0 && b3 == 0)
					{
						firstByte = (flag ? 64 : 1);
						secondByte = 1;
						return ValueFlagsUtility.SerializationResult.HandOffToLegacy;
					}
					return ValueFlagsUtility.SerializationResult.Failed;
				}
				else
				{
					if (secondByte == 2 && (byte)stream.ReadByte() == 0)
					{
						Stream stream2 = stream;
						if (flag)
						{
							stream2 = new DeflateStream(stream, CompressionMode.Decompress);
						}
						deserialized = ValueFlagsUtility._nativeSerializer.Deserialize(stream, stream2);
						return ValueFlagsUtility.SerializationResult.Completed;
					}
					return ValueFlagsUtility.SerializationResult.Failed;
				}
			}
			if (secondByte == 0)
			{
				firstByte = (flag ? 64 : 6);
				secondByte = 6;
				return ValueFlagsUtility.SerializationResult.HandOffToLegacy;
			}
			return ValueFlagsUtility.SerializationResult.Failed;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00033B20 File Offset: 0x00031D20
		internal static bool IsNormalized(byte[][] userData, ValueFlagsVersion version)
		{
			if (version == ValueFlagsVersion.EitherType || userData == null)
			{
				return true;
			}
			switch (version)
			{
			case ValueFlagsVersion.LegacyWcfType:
				if (ValueFlagsUtility.CheckMinLength(userData, 2))
				{
					byte byteAtIndex = ValueFlagsUtility.GetByteAtIndex(userData, 0);
					byte byteAtIndex2 = ValueFlagsUtility.GetByteAtIndex(userData, 1);
					return ((int)byteAtIndex2 & -8) == 0 && (byteAtIndex == 64 || byteAtIndex == byteAtIndex2);
				}
				break;
			case ValueFlagsVersion.WireProtocolType:
				if (ValueFlagsUtility.CheckMinLength(userData, 4))
				{
					byte byteAtIndex3 = ValueFlagsUtility.GetByteAtIndex(userData, 0);
					byte byteAtIndex4 = ValueFlagsUtility.GetByteAtIndex(userData, 1);
					byte byteAtIndex5 = ValueFlagsUtility.GetByteAtIndex(userData, 2);
					byte byteAtIndex6 = ValueFlagsUtility.GetByteAtIndex(userData, 3);
					byte b = byteAtIndex3;
					if (b <= 32)
					{
						if (b == 16)
						{
							return byteAtIndex4 == 0 && ((int)byteAtIndex6 & -8) == 0 && (byteAtIndex5 == 64 || byteAtIndex5 == byteAtIndex6);
						}
						if (b != 32)
						{
							break;
						}
					}
					else if (b != 96)
					{
						if (b != 128 && b != 192)
						{
							break;
						}
						switch (byteAtIndex4)
						{
						case 1:
							return byteAtIndex5 == 0 && byteAtIndex6 == 0;
						case 2:
							return byteAtIndex5 == 0;
						default:
							return false;
						}
					}
					return byteAtIndex4 == 0;
				}
				break;
			}
			return false;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00033C28 File Offset: 0x00031E28
		internal static byte[][] NormalizeUserData(byte[][] userData, ValueFlagsVersion from, ValueFlagsVersion to)
		{
			if (userData == null || from == to || to == ValueFlagsVersion.EitherType || (from == ValueFlagsVersion.EitherType && ValueFlagsUtility.IsNormalized(userData, to)))
			{
				return userData;
			}
			try
			{
				bool flag = false;
				switch (to)
				{
				case ValueFlagsVersion.LegacyWcfType:
				{
					byte[][] array;
					if (ValueFlagsUtility.CheckMinLength(userData, 4))
					{
						byte byteAtIndex = ValueFlagsUtility.GetByteAtIndex(userData, 0);
						byte byteAtIndex2 = ValueFlagsUtility.GetByteAtIndex(userData, 1);
						byte byteAtIndex3 = ValueFlagsUtility.GetByteAtIndex(userData, 2);
						byte byteAtIndex4 = ValueFlagsUtility.GetByteAtIndex(userData, 3);
						flag = (byteAtIndex & 64) != 0;
						byte b = byteAtIndex;
						if (b <= 32)
						{
							if (b != 16)
							{
								if (b != 32)
								{
									goto IL_0207;
								}
							}
							else
							{
								if (byteAtIndex2 == 0)
								{
									if (userData[0].Length == 2)
									{
										array = new byte[userData.Length - 1][];
										Array.Copy(userData, 1, array, 0, array.Length);
									}
									else
									{
										array = new byte[userData.Length][];
										Array.Copy(userData, 1, array, 1, userData.Length - 1);
										array[0] = new byte[userData[0].Length - 2];
										Array.Copy(userData[0], 2, array[0], 0, array[0].Length);
									}
									return array;
								}
								goto IL_0207;
							}
						}
						else if (b != 96)
						{
							if (b != 128 && b != 192)
							{
								goto IL_0207;
							}
							if (byteAtIndex2 == 1 && byteAtIndex3 == 0 && byteAtIndex4 == 0)
							{
								array = new byte[userData.Length][];
								Array.Copy(userData, 1, array, 1, userData.Length - 1);
								array[0] = new byte[userData[0].Length - 2];
								Array.Copy(userData[0], 4, array[0], 2, userData[0].Length - 4);
								ValueFlagsUtility.TrySetByteAtIndex(array, 0, flag ? 64 : 1);
								ValueFlagsUtility.TrySetByteAtIndex(array, 1, 1);
								return array;
							}
							if (byteAtIndex2 == 2 && byteAtIndex3 == 0)
							{
								object obj = ValueFlagsUtility._serializationProvider.DeserializeUserObject(userData, from);
								return ValueFlagsUtility._serializationProvider.SerializeUserObject(obj, flag, to);
							}
							goto IL_0207;
						}
						if (byteAtIndex2 == 0)
						{
							array = new byte[userData.Length][];
							Array.Copy(userData, 1, array, 1, userData.Length - 1);
							array[0] = new byte[userData[0].Length];
							Array.Copy(userData[0], 2, array[0], 2, userData[0].Length - 2);
							ValueFlagsUtility.TrySetByteAtIndex(array, 0, flag ? 64 : 6);
							ValueFlagsUtility.TrySetByteAtIndex(array, 1, 6);
							return array;
						}
					}
					IL_0207:
					array = new byte[userData.Length + 1][];
					array[0] = ValueFlagsUtility._reservedWcfPrefix;
					Array.Copy(userData, 0, array, 1, userData.Length);
					return array;
				}
				case ValueFlagsVersion.WireProtocolType:
				{
					byte[][] array;
					if (ValueFlagsUtility.CheckMinLength(userData, 2))
					{
						byte byteAtIndex5 = ValueFlagsUtility.GetByteAtIndex(userData, 0);
						byte byteAtIndex6 = ValueFlagsUtility.GetByteAtIndex(userData, 1);
						flag = byteAtIndex5 == 64;
						byte b2 = (flag ? 64 : 0);
						switch (byteAtIndex6)
						{
						case 1:
							array = new byte[userData.Length][];
							Array.Copy(userData, 1, array, 1, userData.Length - 1);
							array[0] = new byte[userData[0].Length + 2];
							Array.Copy(userData[0], 2, array[0], 4, userData[0].Length - 2);
							ValueFlagsUtility.TrySetByteAtIndex(array, 0, 128 | b2);
							ValueFlagsUtility.TrySetByteAtIndex(array, 1, 1);
							ValueFlagsUtility.TrySetByteAtIndex(array, 2, 0);
							ValueFlagsUtility.TrySetByteAtIndex(array, 3, 0);
							return array;
						case 4:
						{
							object obj2 = ValueFlagsUtility._serializationProvider.DeserializeUserObject(userData, from);
							return ValueFlagsUtility._serializationProvider.SerializeUserObject(obj2, flag, to);
						}
						case 6:
							array = new byte[userData.Length][];
							Array.Copy(userData, 1, array, 1, userData.Length - 1);
							array[0] = new byte[userData[0].Length];
							Array.Copy(userData[0], 2, array[0], 2, userData[0].Length - 2);
							array[0][0] = 32 | b2;
							array[0][1] = 0;
							return array;
						case 7:
							if (userData[0].Length == 2)
							{
								array = new byte[userData.Length - 1][];
								Array.Copy(userData, 1, array, 0, array.Length);
							}
							else
							{
								array = new byte[userData.Length][];
								Array.Copy(userData, 1, array, 1, userData.Length - 1);
								array[0] = new byte[userData[0].Length - 2];
								Array.Copy(userData[0], 2, array[0], 0, array[0].Length);
							}
							return array;
						}
					}
					array = new byte[userData.Length + 1][];
					array[0] = ValueFlagsUtility._internalVwPrefix;
					Array.Copy(userData, 0, array, 1, userData.Length);
					return array;
				}
				default:
				{
					object obj3 = ValueFlagsUtility._serializationProvider.DeserializeUserObject(userData, from);
					return ValueFlagsUtility._serializationProvider.SerializeUserObject(obj3, flag, to);
				}
				}
			}
			catch (SerializationException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteInfo("ValueFlagsUtility.NormalizeUserData", "Exception normalizing {0} to {1}: {2}", new object[] { from, to, ex });
				}
			}
			return userData;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000340A8 File Offset: 0x000322A8
		private static bool CheckMinLength(byte[][] response, int minLength)
		{
			if (response != null)
			{
				for (int i = 0; i < response.Length; i++)
				{
					minLength -= response[i].Length;
					if (minLength <= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000340D8 File Offset: 0x000322D8
		private static byte GetByteAtIndex(byte[][] response, int index)
		{
			for (int i = 0; i < response.Length; i++)
			{
				if (index < response[i].Length)
				{
					return response[i][index];
				}
				index -= response[i].Length;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00034110 File Offset: 0x00032310
		private static bool TrySetByteAtIndex(byte[][] response, int index, byte value)
		{
			for (int i = 0; i < response.Length; i++)
			{
				if (index < response[i].Length)
				{
					response[i][index] = value;
					return true;
				}
				index -= response[i].Length;
			}
			return false;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00034148 File Offset: 0x00032348
		// Note: this type is marked as 'beforefieldinit'.
		static ValueFlagsUtility()
		{
			byte[] array = new byte[2];
			array[0] = 16;
			ValueFlagsUtility._internalVwPrefix = array;
			byte[] array2 = new byte[4];
			array2[0] = 128;
			array2[1] = 1;
			ValueFlagsUtility._byteArrayVwPrefix = array2;
			ValueFlagsUtility._byteArraySerializer = new BinaryArrayCacheObjectSerializer();
		}

		// Token: 0x04000A75 RID: 2677
		public const byte Predefined = 128;

		// Token: 0x04000A76 RID: 2678
		public const byte Compressed = 64;

		// Token: 0x04000A77 RID: 2679
		public const byte UserDefined = 32;

		// Token: 0x04000A78 RID: 2680
		public const byte Internal = 16;

		// Token: 0x04000A79 RID: 2681
		public const byte Reserved = 7;

		// Token: 0x04000A7A RID: 2682
		public const byte Undefined = 0;

		// Token: 0x04000A7B RID: 2683
		public const byte ByteArray = 1;

		// Token: 0x04000A7C RID: 2684
		public const byte NativeType = 2;

		// Token: 0x04000A7D RID: 2685
		private static readonly NativeCacheObjectSerializer _nativeSerializer = new NativeCacheObjectSerializer(ValueFlagsVersion.WireProtocolType);

		// Token: 0x04000A7E RID: 2686
		private static readonly DataCacheObjectSerializationProvider _serializationProvider = new DataCacheObjectSerializationProvider(new DataCacheSerializationProperties());

		// Token: 0x04000A7F RID: 2687
		private static readonly byte[] _reservedWcfPrefix = new byte[] { 7, 7 };

		// Token: 0x04000A80 RID: 2688
		private static readonly byte[] _internalVwPrefix;

		// Token: 0x04000A81 RID: 2689
		private static readonly byte[] _byteArrayVwPrefix;

		// Token: 0x04000A82 RID: 2690
		private static readonly IChunkedArrayBackedSerializer _byteArraySerializer;

		// Token: 0x020001D5 RID: 469
		internal enum SerializationResult
		{
			// Token: 0x04000A84 RID: 2692
			Failed,
			// Token: 0x04000A85 RID: 2693
			Completed,
			// Token: 0x04000A86 RID: 2694
			HandOffToLegacy
		}
	}
}
