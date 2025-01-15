using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002093 RID: 8339
	public static class Xml<T> where T : XmlRoot
	{
		// Token: 0x17003120 RID: 12576
		// (get) Token: 0x0600CC18 RID: 52248 RVA: 0x00289ACA File Offset: 0x00287CCA
		// (set) Token: 0x0600CC19 RID: 52249 RVA: 0x00289AD1 File Offset: 0x00287CD1
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

		// Token: 0x0600CC1A RID: 52250 RVA: 0x00289AD9 File Offset: 0x00287CD9
		public static bool TryDeserializeBytes(byte[] bytes, out T value)
		{
			return Xml<T>.TryDeserialize<byte[]>(bytes, (byte[] x) => Xml<T>.DeserializeBytes(x), out value);
		}

		// Token: 0x0600CC1B RID: 52251 RVA: 0x00289B04 File Offset: 0x00287D04
		public static T DeserializeBytes(byte[] bytes)
		{
			T t;
			using (XmlReader xmlReader = Xml<T>.XmlReaderCreate(new MemoryStream(bytes), Xml<T>.CreateReaderSettings()))
			{
				t = (T)((object)Xml<T>.Serializer.Deserialize(xmlReader));
			}
			return t;
		}

		// Token: 0x0600CC1C RID: 52252 RVA: 0x00289B50 File Offset: 0x00287D50
		public static byte[] SerializeBytes(T value)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, Xml<T>.CreateWriterSettings()))
			{
				Xml<T>.Serializer.Serialize(xmlWriter, value);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600CC1D RID: 52253 RVA: 0x00289BA4 File Offset: 0x00287DA4
		public static bool TryDeserializeString(string text, out T value)
		{
			return Xml<T>.TryDeserialize<string>(text, (string x) => Xml<T>.DeserializeString(x), out value);
		}

		// Token: 0x0600CC1E RID: 52254 RVA: 0x00289BCC File Offset: 0x00287DCC
		public static T DeserializeString(string text)
		{
			T t;
			using (XmlReader xmlReader = Xml<T>.XmlReaderCreate(new StringReader(text), Xml<T>.CreateReaderSettings()))
			{
				t = (T)((object)Xml<T>.Serializer.Deserialize(xmlReader));
			}
			return t;
		}

		// Token: 0x0600CC1F RID: 52255 RVA: 0x00289C18 File Offset: 0x00287E18
		public static string SerializeString(T value)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, Xml<T>.CreateWriterSettings()))
			{
				Xml<T>.Serializer.Serialize(xmlWriter, value);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600CC20 RID: 52256 RVA: 0x00289C70 File Offset: 0x00287E70
		private static XmlReaderSettings CreateReaderSettings()
		{
			return new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x0600CC21 RID: 52257 RVA: 0x00289C85 File Offset: 0x00287E85
		private static XmlWriterSettings CreateWriterSettings()
		{
			return new XmlWriterSettings
			{
				Indent = false,
				NewLineHandling = NewLineHandling.Entitize
			};
		}

		// Token: 0x0600CC22 RID: 52258 RVA: 0x0001B93D File Offset: 0x00019B3D
		private static XmlReader XmlReaderCreate(MemoryStream stream, XmlReaderSettings readerSettings)
		{
			readerSettings.XmlResolver = null;
			return XmlReader.Create(stream, readerSettings);
		}

		// Token: 0x0600CC23 RID: 52259 RVA: 0x0001B94D File Offset: 0x00019B4D
		private static XmlReader XmlReaderCreate(StringReader stringReader, XmlReaderSettings readerSettings)
		{
			readerSettings.XmlResolver = null;
			return XmlReader.Create(stringReader, readerSettings);
		}

		// Token: 0x0600CC24 RID: 52260 RVA: 0x00289C9C File Offset: 0x00287E9C
		private static bool TryDeserialize<TInput>(TInput input, Func<TInput, T> func, out T value)
		{
			bool flag;
			try
			{
				value = func(input);
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

		// Token: 0x04006777 RID: 26487
		private static XmlSerializer serializer = new XmlSerializer(typeof(T));
	}
}
