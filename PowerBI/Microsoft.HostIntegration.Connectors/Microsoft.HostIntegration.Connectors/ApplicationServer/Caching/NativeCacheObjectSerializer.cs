using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000295 RID: 661
	internal class NativeCacheObjectSerializer : ISizeBasedCacheObjectSerializer, IDataCacheObjectSerializer
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x00049098 File Offset: 0x00047298
		public NativeCacheObjectSerializer(ValueFlagsVersion ver)
		{
			if (ver == ValueFlagsVersion.EitherType)
			{
				throw new ArgumentOutOfRangeException("ver", ver, "ValueFlagsVersion.EitherType is not supported.");
			}
			this._flagsVersion = ver;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000490C4 File Offset: 0x000472C4
		internal void Serialize(Stream flagsStream, Stream payloadStream, object value)
		{
			if (flagsStream == null)
			{
				throw new ArgumentNullException("flagsStream");
			}
			if (payloadStream == null)
			{
				throw new ArgumentNullException("payloadStream");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			BinaryWriter binaryWriter = new BinaryWriter(flagsStream);
			BinaryWriter binaryWriter2 = new BinaryWriter(payloadStream, Encoding.UTF8);
			Type type = value.GetType();
			if (type == typeof(string))
			{
				string text = (string)value;
				if (this._flagsVersion == ValueFlagsVersion.WireProtocolType)
				{
					binaryWriter.Write(0);
					binaryWriter2.Write(NativeCacheObjectSerializer._utf8Encoding.GetBytes(text));
					return;
				}
				binaryWriter.Write(0);
				binaryWriter2.Write(text);
				return;
			}
			else
			{
				if (type == typeof(int))
				{
					binaryWriter.Write(1);
					binaryWriter2.Write((int)value);
					return;
				}
				if (type == typeof(uint))
				{
					binaryWriter.Write(2);
					binaryWriter2.Write((uint)value);
					return;
				}
				if (type == typeof(short))
				{
					binaryWriter.Write(5);
					binaryWriter2.Write((short)value);
					return;
				}
				if (type == typeof(ushort))
				{
					binaryWriter.Write(6);
					binaryWriter2.Write((ushort)value);
					return;
				}
				if (type == typeof(long))
				{
					binaryWriter.Write(3);
					binaryWriter2.Write((long)value);
					return;
				}
				if (type == typeof(ulong))
				{
					binaryWriter.Write(4);
					binaryWriter2.Write((ulong)value);
					return;
				}
				if (type == typeof(decimal))
				{
					binaryWriter.Write(8);
					binaryWriter2.Write((decimal)value);
					return;
				}
				if (type == typeof(bool))
				{
					binaryWriter.Write(9);
					binaryWriter2.Write((bool)value);
					return;
				}
				if (type == typeof(double))
				{
					binaryWriter.Write(7);
					binaryWriter2.Write((double)value);
					return;
				}
				if (type == typeof(float))
				{
					binaryWriter.Write(10);
					binaryWriter2.Write((float)value);
					return;
				}
				if (type == typeof(byte))
				{
					binaryWriter.Write(11);
					binaryWriter2.Write((byte)value);
					return;
				}
				if (type == typeof(sbyte))
				{
					binaryWriter.Write(12);
					binaryWriter2.Write((sbyte)value);
					return;
				}
				throw new SerializationException();
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00049330 File Offset: 0x00047530
		internal object Deserialize(Stream flagsStream, Stream payloadStream)
		{
			if (flagsStream == null)
			{
				throw new ArgumentNullException("flagsStream");
			}
			if (payloadStream == null)
			{
				throw new ArgumentNullException("payloadStream");
			}
			object obj;
			try
			{
				NativeCacheObjectSerializer.NativeTypeMarker nativeTypeMarker = (NativeCacheObjectSerializer.NativeTypeMarker)flagsStream.ReadByte();
				if (nativeTypeMarker == NativeCacheObjectSerializer.NativeTypeMarker.String && this._flagsVersion == ValueFlagsVersion.WireProtocolType)
				{
					if (payloadStream.CanSeek)
					{
						long num = payloadStream.Length - payloadStream.Position;
						if (num == 0L)
						{
							return string.Empty;
						}
						if (num < (long)ConfigManager.LargeObjectHeapCheckSize)
						{
							return new StreamBackedReader(payloadStream).ReadString((int)num, NativeCacheObjectSerializer._utf8Encoding);
						}
						IBufferManager bufferManager = SocketTransportChannel.BufferManager;
						if (bufferManager != null && num <= (long)bufferManager.MaxMessageSize)
						{
							return new StreamBackedReader(payloadStream).ReadString((int)num, NativeCacheObjectSerializer._utf8Encoding, bufferManager);
						}
					}
					obj = new StreamReader(payloadStream, NativeCacheObjectSerializer._utf8Encoding, false, 65536).ReadToEnd();
				}
				else
				{
					BinaryReader binaryReader = new BinaryReader(payloadStream, NativeCacheObjectSerializer._utf8Encoding);
					switch (nativeTypeMarker)
					{
					case NativeCacheObjectSerializer.NativeTypeMarker.String:
						obj = binaryReader.ReadString();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Int32:
						obj = binaryReader.ReadInt32();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.UInt32:
						obj = binaryReader.ReadUInt32();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Int64:
						obj = binaryReader.ReadInt64();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.UInt64:
						obj = binaryReader.ReadUInt64();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Int16:
						obj = binaryReader.ReadInt16();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.UInt16:
						obj = binaryReader.ReadUInt16();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Double:
						obj = binaryReader.ReadDouble();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Decimal:
						obj = binaryReader.ReadDecimal();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Boolean:
						obj = binaryReader.ReadBoolean();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Single:
						obj = binaryReader.ReadSingle();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.Byte:
						obj = binaryReader.ReadByte();
						break;
					case NativeCacheObjectSerializer.NativeTypeMarker.SByte:
						obj = binaryReader.ReadSByte();
						break;
					default:
						throw new SerializationException();
					}
				}
			}
			catch (EndOfStreamException ex)
			{
				throw new SerializationException("Invalid Serialization", ex);
			}
			catch (IOException ex2)
			{
				throw new SerializationException("Invalid Serialization", ex2);
			}
			return obj;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00049580 File Offset: 0x00047780
		public void Serialize(Stream stream, object value)
		{
			this.Serialize(stream, stream, value);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0004958B File Offset: 0x0004778B
		public object Deserialize(Stream stream)
		{
			return this.Deserialize(stream, stream);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00049598 File Offset: 0x00047798
		public int EstimateSerializationSize(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == typeof(string))
			{
				int byteCount = NativeCacheObjectSerializer._utf8Encoding.GetByteCount((string)value);
				int num = 0;
				if (this._flagsVersion == ValueFlagsVersion.LegacyWcfType)
				{
					if (byteCount < 128)
					{
						num = 1;
					}
					else if (byteCount < 16384)
					{
						num = 2;
					}
					else if (byteCount < 2097152)
					{
						num = 3;
					}
					else
					{
						num = 4;
					}
				}
				return byteCount + num + 1;
			}
			if (type == typeof(int) || type == typeof(uint) || type == typeof(float))
			{
				return 5;
			}
			if (type == typeof(short) || type == typeof(ushort))
			{
				return 3;
			}
			if (type == typeof(long) || type == typeof(ulong) || type == typeof(double))
			{
				return 9;
			}
			if (type == typeof(bool) || type == typeof(byte) || type == typeof(sbyte))
			{
				return 2;
			}
			if (type == typeof(decimal))
			{
				return 17;
			}
			throw new SerializationException();
		}

		// Token: 0x04000D47 RID: 3399
		private const string InvalidSerializationFormatString = "Invalid Serialization";

		// Token: 0x04000D48 RID: 3400
		private static readonly Encoding _utf8Encoding = new UTF8Encoding(false, false);

		// Token: 0x04000D49 RID: 3401
		private readonly ValueFlagsVersion _flagsVersion;

		// Token: 0x02000296 RID: 662
		private enum NativeTypeMarker
		{
			// Token: 0x04000D4B RID: 3403
			String,
			// Token: 0x04000D4C RID: 3404
			Int32,
			// Token: 0x04000D4D RID: 3405
			UInt32,
			// Token: 0x04000D4E RID: 3406
			Int64,
			// Token: 0x04000D4F RID: 3407
			UInt64,
			// Token: 0x04000D50 RID: 3408
			Int16,
			// Token: 0x04000D51 RID: 3409
			UInt16,
			// Token: 0x04000D52 RID: 3410
			Double,
			// Token: 0x04000D53 RID: 3411
			Decimal,
			// Token: 0x04000D54 RID: 3412
			Boolean,
			// Token: 0x04000D55 RID: 3413
			Single,
			// Token: 0x04000D56 RID: 3414
			Byte,
			// Token: 0x04000D57 RID: 3415
			SByte
		}
	}
}
