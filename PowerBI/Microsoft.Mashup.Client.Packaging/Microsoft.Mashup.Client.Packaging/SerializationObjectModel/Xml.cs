using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x02000013 RID: 19
	internal static class Xml<T> where T : XmlRoot
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002C86 File Offset: 0x00000E86
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002C8D File Offset: 0x00000E8D
		public static XmlSerializer Serializer
		{
			get
			{
				return Xml<T>.serializer;
			}
			set
			{
				Xml<T>.serializer = value;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C95 File Offset: 0x00000E95
		public static bool TryDeserializeBytes(byte[] bytes, out T value)
		{
			return Xml<T>.TryDeserialize<byte[]>(bytes, (byte[] x) => Xml<T>.DeserializeBytes(x), out value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static T DeserializeBytes(byte[] bytes)
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			T t;
			using (XmlReader xmlReader = Xml<T>.XmlReaderCreate(memoryStream, Xml<T>.CreateReaderSettings()))
			{
				t = (T)((object)Xml<T>.Serializer.Deserialize(xmlReader));
			}
			return t;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D10 File Offset: 0x00000F10
		public static byte[] SerializeBytes(T value)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, Xml<T>.CreateWriterSettings()))
			{
				Xml<T>.Serializer.Serialize(xmlWriter, value);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D64 File Offset: 0x00000F64
		public static bool TryDeserializeString(string text, out T value)
		{
			return Xml<T>.TryDeserialize<string>(text, (string x) => Xml<T>.DeserializeString(x), out value);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static T DeserializeString(string text)
		{
			StringReader stringReader = new StringReader(text);
			T t;
			using (XmlReader xmlReader = Xml<T>.XmlReaderCreate(stringReader, Xml<T>.CreateReaderSettings()))
			{
				t = (T)((object)Xml<T>.Serializer.Deserialize(xmlReader));
			}
			return t;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DDC File Offset: 0x00000FDC
		public static string SerializeString(T value)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, Xml<T>.CreateWriterSettings()))
			{
				Xml<T>.Serializer.Serialize(xmlWriter, value);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E34 File Offset: 0x00001034
		public static XmlReader XmlReaderCreate(StringReader stringReader, XmlReaderSettings readerSettings)
		{
			readerSettings.XmlResolver = null;
			return XmlReader.Create(stringReader, readerSettings);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E44 File Offset: 0x00001044
		public static XmlReader XmlReaderCreate(MemoryStream stream, XmlReaderSettings readerSettings)
		{
			readerSettings.XmlResolver = null;
			return XmlReader.Create(stream, readerSettings);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E54 File Offset: 0x00001054
		private static XmlReaderSettings CreateReaderSettings()
		{
			return new XmlReaderSettings
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E78 File Offset: 0x00001078
		private static XmlWriterSettings CreateWriterSettings()
		{
			return new XmlWriterSettings
			{
				Indent = false,
				NewLineHandling = 1
			};
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E9C File Offset: 0x0000109C
		private static bool TryDeserialize<TInput>(TInput input, Func<TInput, T> func, out T value)
		{
			bool flag;
			try
			{
				value = func.Invoke(input);
				flag = true;
			}
			catch (FormatException)
			{
				value = default(T);
				flag = false;
			}
			catch (InvalidOperationException)
			{
				value = default(T);
				flag = false;
			}
			catch (XmlException)
			{
				value = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000054 RID: 84
		private static XmlSerializer serializer = new XmlSerializer(typeof(T));
	}
}
