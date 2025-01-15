using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Xml;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200038D RID: 909
	internal class SerializationUtility
	{
		// Token: 0x06002005 RID: 8197 RVA: 0x00061199 File Offset: 0x0005F399
		public static byte[][] Serialize(object obj)
		{
			return SerializationUtility.NetDataContractSerialize(obj, null);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000611A2 File Offset: 0x0005F3A2
		public static bool IsDataContractSerialization(int mark1, int mark2)
		{
			return mark1 == mark2 && mark1 == 3;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00061199 File Offset: 0x0005F399
		public static byte[][] NetDataContractSerialize(object obj)
		{
			return SerializationUtility.NetDataContractSerialize(obj, null);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000611AE File Offset: 0x0005F3AE
		public static byte[][] NetDataContractSerialize(object obj, bool isCompressionEnabled)
		{
			return SerializationUtility.NetDataContractSerialize(obj, null, isCompressionEnabled);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000611B8 File Offset: 0x0005F3B8
		public static byte[][] NetDataContractSerialize(object obj, object context)
		{
			return SerializationUtility.NetDataContractSerialize(obj, context, false);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000611C4 File Offset: 0x0005F3C4
		public static byte[][] NetDataContractSerialize(object obj, object context, bool isCompressionEnabled)
		{
			byte[] array = obj as byte[];
			if (array != null)
			{
				using (ChunkStream chunkStream = new ChunkStream(array.Length + 2))
				{
					if (isCompressionEnabled && array.Length > 1024)
					{
						chunkStream.WriteByte(64);
						chunkStream.WriteByte(1);
						using (DeflateStream deflateStream = new DeflateStream(chunkStream, CompressionMode.Compress))
						{
							deflateStream.Write(array, 0, array.Length);
							goto IL_0069;
						}
					}
					chunkStream.WriteByte(1);
					chunkStream.WriteByte(1);
					chunkStream.Write(array, 0, array.Length);
					IL_0069:
					return chunkStream.ToChunkedArray();
				}
			}
			SessionStoreProviderData sessionStoreProviderData = obj as SessionStoreProviderData;
			if (sessionStoreProviderData != null)
			{
				using (ChunkStream chunkStream2 = new ChunkStream())
				{
					if (isCompressionEnabled)
					{
						chunkStream2.WriteByte(64);
						chunkStream2.WriteByte(2);
						using (DeflateStream deflateStream2 = new DeflateStream(chunkStream2, CompressionMode.Compress))
						{
							sessionStoreProviderData.WriteObject(deflateStream2);
							goto IL_00DD;
						}
					}
					chunkStream2.WriteByte(2);
					chunkStream2.WriteByte(2);
					sessionStoreProviderData.WriteObject(chunkStream2);
					IL_00DD:
					return chunkStream2.ToChunkedArray();
				}
			}
			byte[][] array2;
			using (ChunkStream chunkStream3 = new ChunkStream())
			{
				Stream stream = chunkStream3;
				if (isCompressionEnabled)
				{
					stream.WriteByte(64);
					stream.WriteByte(0);
					DeflateStream deflateStream3 = new DeflateStream(stream, CompressionMode.Compress);
					stream = deflateStream3;
				}
				else
				{
					stream.WriteByte(0);
					stream.WriteByte(0);
				}
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream))
				{
					NetDataContractSerializer netDataContractSerializer;
					if (context != null)
					{
						StreamingContext streamingContext = new StreamingContext(StreamingContextStates.All, context);
						netDataContractSerializer = new NetDataContractSerializer(streamingContext);
					}
					else
					{
						netDataContractSerializer = new NetDataContractSerializer();
					}
					netDataContractSerializer.WriteObject(xmlDictionaryWriter, obj);
					stream.Flush();
				}
				array2 = chunkStream3.ToChunkedArray();
			}
			return array2;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000613B8 File Offset: 0x0005F5B8
		public static byte[][] DataContractSerialize(object obj)
		{
			return SerializationUtility.DataContractSerialize(obj, DataContractKnownTypes.KnownTypes);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000613C8 File Offset: 0x0005F5C8
		public static byte[][] DataContractSerialize(object obj, IEnumerable<Type> knownTypes)
		{
			byte[][] array;
			using (ChunkStream chunkStream = new ChunkStream())
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(object), knownTypes);
				chunkStream.WriteByte(3);
				chunkStream.WriteByte(3);
				dataContractSerializer.WriteObject(chunkStream, obj);
				array = chunkStream.ToChunkedArray();
			}
			return array;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00061428 File Offset: 0x0005F628
		public static object DataContractDeserialize(byte[][] value)
		{
			return SerializationUtility.Deserialize(value, false);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00061434 File Offset: 0x0005F634
		internal static string SerializeAuditEntryProperty(object serializableObject)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlDictionaryWriter.CreateTextWriter(memoryStream))
				{
					NetDataContractSerializer netDataContractSerializer = new NetDataContractSerializer();
					netDataContractSerializer.WriteObject(memoryStream, serializableObject);
					memoryStream.Flush();
					memoryStream.Seek(0L, SeekOrigin.Begin);
					StreamReader streamReader = new StreamReader(memoryStream);
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000614B0 File Offset: 0x0005F6B0
		public static object Deserialize(byte[][] buffers, bool checkTypeToLoad)
		{
			return SerializationUtility.Deserialize(buffers, checkTypeToLoad, null);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000614BC File Offset: 0x0005F6BC
		private static byte[] DecompressBytes(Stream stream)
		{
			byte[] array2;
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
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0006151C File Offset: 0x0005F71C
		public static object Deserialize(byte[][] buffers, bool checkTypeToLoad, object context)
		{
			return SerializationUtility.Deserialize(buffers, checkTypeToLoad, context, DataContractKnownTypes.KnownTypes);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0006152C File Offset: 0x0005F72C
		public static object Deserialize(byte[][] buffers, bool checkTypeToLoad, object context, IEnumerable<Type> knownTypes)
		{
			Stream stream = null;
			object obj2;
			try
			{
				stream = new ChunkStream(buffers);
				byte b = (byte)stream.ReadByte();
				int num = stream.ReadByte();
				if (num == -1)
				{
					throw new SerializationException();
				}
				byte b2 = (byte)num;
				if (b == 64)
				{
					b = b2;
					stream = new DeflateStream(stream, CompressionMode.Decompress);
				}
				object obj;
				if (ValueFlagsUtility.DeserializeUserObject(stream, ref b, ref b2, out obj) == ValueFlagsUtility.SerializationResult.Completed)
				{
					obj2 = obj;
				}
				else
				{
					if (b != b2)
					{
						throw new SerializationException();
					}
					if (b == 1)
					{
						obj2 = SerializationUtility.DecompressBytes(stream);
					}
					else
					{
						if (b != 2)
						{
							if (b == 0)
							{
								using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
								{
									NetDataContractSerializer netDataContractSerializer;
									if (context != null)
									{
										StreamingContext streamingContext = new StreamingContext(StreamingContextStates.All, context);
										netDataContractSerializer = new NetDataContractSerializer(streamingContext);
									}
									else
									{
										netDataContractSerializer = new NetDataContractSerializer();
									}
									if (checkTypeToLoad)
									{
										netDataContractSerializer.Binder = CustomSerializationBinder.Singleton;
									}
									netDataContractSerializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
									return netDataContractSerializer.ReadObject(xmlDictionaryReader);
								}
							}
							if (b == 3)
							{
								DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(object), knownTypes);
								try
								{
									return dataContractSerializer.ReadObject(stream);
								}
								catch (InvalidCastException)
								{
									throw new SerializationException();
								}
							}
							throw new SerializationException();
						}
						SessionStoreProviderData sessionStoreProviderData = new SessionStoreProviderData();
						sessionStoreProviderData.ReadObject(stream);
						obj2 = sessionStoreProviderData;
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return obj2;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000616AC File Offset: 0x0005F8AC
		public static object Clone(object original)
		{
			return SerializationUtility.Deserialize(SerializationUtility.NetDataContractSerialize(original), true);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000616BC File Offset: 0x0005F8BC
		public static byte[] SerializeToByteArray(object obj, object context)
		{
			byte[] array = obj as byte[];
			byte[] array2;
			if (array != null)
			{
				array2 = new byte[array.Length + 2];
				array2[0] = 1;
				array2[1] = 1;
				Array.Copy(array, 0, array2, 2, array.Length);
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					NetDataContractSerializer netDataContractSerializer;
					if (context != null)
					{
						StreamingContext streamingContext = new StreamingContext(StreamingContextStates.All, context);
						netDataContractSerializer = new NetDataContractSerializer(streamingContext);
					}
					else
					{
						netDataContractSerializer = new NetDataContractSerializer();
					}
					memoryStream.WriteByte(0);
					memoryStream.WriteByte(0);
					netDataContractSerializer.WriteObject(memoryStream, obj);
					array2 = memoryStream.ToArray();
				}
			}
			return array2;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00061754 File Offset: 0x0005F954
		public static object DeserializeFromByteArray(byte[] buf, object context)
		{
			if (buf[0] == 1)
			{
				byte[] array = new byte[buf.Length - 2];
				Array.Copy(buf, 2, array, 0, array.Length);
				return array;
			}
			object obj2;
			using (MemoryStream memoryStream = new MemoryStream(buf, false))
			{
				int num = memoryStream.ReadByte();
				memoryStream.ReadByte();
				if (num == 0)
				{
					NetDataContractSerializer netDataContractSerializer;
					if (context != null)
					{
						StreamingContext streamingContext = new StreamingContext(StreamingContextStates.All, context);
						netDataContractSerializer = new NetDataContractSerializer(streamingContext);
					}
					else
					{
						netDataContractSerializer = new NetDataContractSerializer();
					}
					netDataContractSerializer.Binder = CustomSerializationBinder.Singleton;
					netDataContractSerializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
					object obj = netDataContractSerializer.Deserialize(memoryStream);
					obj2 = obj;
				}
				else
				{
					if (num != 3)
					{
						throw new SerializationException("Unknown Marker");
					}
					DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(object), DataContractKnownTypes.KnownTypes);
					obj2 = dataContractSerializer.ReadObject(memoryStream);
				}
			}
			return obj2;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00061828 File Offset: 0x0005FA28
		public static byte[][] SerializeBinaryFormat(IBinarySerializable item)
		{
			byte[][] array;
			using (ChunkStream chunkStream = new ChunkStream())
			{
				using (SerializationWriter serializationWriter = new SerializationWriter(chunkStream))
				{
					item.WriteStream(serializationWriter);
					serializationWriter.WriteEndMarker();
					array = chunkStream.ToChunkedArray();
				}
			}
			return array;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0006188C File Offset: 0x0005FA8C
		public static void DeserializeBinaryFormat(byte[][] buffer, IBinarySerializable input)
		{
			using (SerializationReader serializationReader = new SerializationReader(new ChunkStream(buffer)))
			{
				input.ReadStream(serializationReader);
				if (!serializationReader.IsCurrentEndMarker())
				{
					throw new SerializationException();
				}
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000618D8 File Offset: 0x0005FAD8
		public static string SerializeToXML(object obj)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.Unicode))
				{
					NetDataContractSerializer netDataContractSerializer = new NetDataContractSerializer();
					netDataContractSerializer.WriteObject(xmlDictionaryWriter, obj);
				}
				using (MemoryStream memoryStream2 = new MemoryStream(memoryStream.ToArray()))
				{
					StreamReader streamReader = new StreamReader(memoryStream2, Encoding.Unicode);
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00061974 File Offset: 0x0005FB74
		public static object DeserializeFromXML(string text)
		{
			object obj2;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(text)))
			{
				using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.Unicode, new XmlDictionaryReaderQuotas(), null))
				{
					NetDataContractSerializer netDataContractSerializer = new NetDataContractSerializer();
					object obj = netDataContractSerializer.ReadObject(xmlDictionaryReader, true);
					obj2 = obj;
				}
			}
			return obj2;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000619EC File Offset: 0x0005FBEC
		internal static byte[] SerializeTags(IEnumerable<DataCacheTag> tags, Encoding encoding)
		{
			if (tags == null)
			{
				return null;
			}
			string[] array = tags.Select(delegate(DataCacheTag t)
			{
				if (t != null)
				{
					return t.ToString();
				}
				return "";
			}).ToArray<string>();
			if (array.Length == 0)
			{
				return null;
			}
			int num = 2;
			foreach (string text in array)
			{
				if (text != null)
				{
					num += encoding.GetMaxByteCount(text.Length) + 2;
				}
			}
			byte[] array4;
			using (MemoryStream memoryStream = new MemoryStream(num))
			{
				using (StreamBackedWriter streamBackedWriter = new StreamBackedWriter(memoryStream, false))
				{
					streamBackedWriter.Write((ushort)array.Length);
					foreach (string text2 in array)
					{
						byte[] bytes = encoding.GetBytes(text2);
						if (bytes.Length > VelocityWireProtocol.MaxTagLength)
						{
							throw Utility.CreateClientException("DataCacheTag", 38, 12, null);
						}
						streamBackedWriter.Write((ushort)bytes.Length);
						streamBackedWriter.Write(bytes, 0, bytes.Length);
					}
				}
				array4 = memoryStream.ToArray();
			}
			return array4;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00061B18 File Offset: 0x0005FD18
		internal static IEnumerable<DataCacheTag> DeserializeTags(byte[] buffer, Encoding encoding)
		{
			if (buffer.Length < 2)
			{
				throw new InvalidOperationException(string.Format("Tags buffer length is too small: {0}", buffer.Length));
			}
			int index = 0;
			ushort count = BitConverter.ToUInt16(buffer, index);
			index += 2;
			for (ushort i = 0; i < count; i += 1)
			{
				if (index + 2 > buffer.Length)
				{
					throw new InvalidOperationException(string.Format("The tags buffer was truncated at {0}. {1} of {2} tags read.", buffer.Length, i, count));
				}
				ushort length = BitConverter.ToUInt16(buffer, index);
				if (index + 2 + (int)length > buffer.Length)
				{
					throw new InvalidOperationException(string.Format("The tags buffer was truncated at {0}. {1} of {2} tags read.", buffer.Length, i, count));
				}
				string tag = encoding.GetString(buffer, index + 2, (int)length);
				index += (int)(2 + length);
				yield return new DataCacheTag(tag, false);
			}
			yield break;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00061B3C File Offset: 0x0005FD3C
		internal static void AssertBufferAvailable(GenericReader reader, int bytes)
		{
			if (!reader.AreBytesAvailable(bytes))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Buffer truncated. Expected: {0}, Actual: {1}", new object[]
				{
					bytes + reader.Position,
					reader.Length
				}));
			}
		}

		// Token: 0x040012E9 RID: 4841
		internal const byte _byteArraySerializationMark = 1;

		// Token: 0x040012EA RID: 4842
		internal const byte _netDataObjectSerializationMark = 0;

		// Token: 0x040012EB RID: 4843
		internal const byte _sessionProviderDataMark = 2;

		// Token: 0x040012EC RID: 4844
		internal const byte _dataContractSerializationMark = 3;

		// Token: 0x040012ED RID: 4845
		private const int _serializationHeaderLength = 2;

		// Token: 0x040012EE RID: 4846
		internal const byte _compressionMark = 64;

		// Token: 0x040012EF RID: 4847
		internal const int CompressionMinLengthThreshold = 1024;
	}
}
