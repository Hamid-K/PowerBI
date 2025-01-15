using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav
{
	// Token: 0x02000028 RID: 40
	internal static class SerializerExtensions
	{
		// Token: 0x06000203 RID: 515 RVA: 0x000063DC File Offset: 0x000045DC
		internal static T FromXmlString<T>(this XmlSerializer serializer, string xml)
		{
			T t;
			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, SerializerExtensions._xmlReaderSettings))
				{
					t = (T)((object)serializer.Deserialize(xmlReader));
				}
			}
			return t;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000643C File Offset: 0x0000463C
		internal static T FromXmlBytes<T>(this XmlSerializer serializer, byte[] content)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(content))
			{
				using (XmlReader xmlReader = XmlReader.Create(memoryStream, SerializerExtensions._xmlReaderSettings))
				{
					t = (T)((object)serializer.Deserialize(xmlReader));
				}
			}
			return t;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000649C File Offset: 0x0000469C
		internal static string ToXmlString<T>(this XmlSerializer serializer, T item, bool indent = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, indent ? SerializerExtensions._xmlWriterIndentSettings : SerializerExtensions._xmlWriterSettings))
			{
				serializer.Serialize(xmlWriter, item);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000064F4 File Offset: 0x000046F4
		internal static T FromXmlString<T>(this DataContractSerializer serializer, string xml)
		{
			T t;
			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, SerializerExtensions._xmlReaderSettings))
				{
					t = (T)((object)serializer.ReadObject(xmlReader));
				}
			}
			return t;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006554 File Offset: 0x00004754
		internal static string ToXmlString<T>(this DataContractSerializer serializer, T item, bool indent = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, indent ? SerializerExtensions._xmlWriterIndentSettings : SerializerExtensions._xmlWriterSettings))
			{
				serializer.WriteObject(xmlWriter, item);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000065AC File Offset: 0x000047AC
		internal static T FromBinaryXml<T>(this DataContractSerializer serializer, byte[] content)
		{
			T t;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(content, SerializerExtensions._readerQuotas))
			{
				t = (T)((object)serializer.ReadObject(xmlDictionaryReader));
			}
			return t;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000065F0 File Offset: 0x000047F0
		internal static byte[] ToBinaryXml<T>(this DataContractSerializer serializer, T item)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream))
			{
				serializer.WriteObject(xmlDictionaryWriter, item);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000663C File Offset: 0x0000483C
		internal static T FromJsonStream<T>(this DataContractJsonSerializer serializer, Stream json)
		{
			return (T)((object)serializer.ReadObject(json));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000664C File Offset: 0x0000484C
		internal static T FromJsonString<T>(this DataContractJsonSerializer serializer, string json)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(SerializerExtensions._jsonEncoding.GetBytes(json)))
			{
				t = serializer.FromJsonStream(memoryStream);
			}
			return t;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006690 File Offset: 0x00004890
		internal static string ToJsonString(this DataContractJsonSerializer serializer, object o)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				serializer.WriteObject(memoryStream, o);
				Contract.Check(memoryStream.Length < 2147483647L, "Serialized JSON is too large.");
				@string = SerializerExtensions._jsonEncoding.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return @string;
		}

		// Token: 0x04000061 RID: 97
		private static readonly XmlReaderSettings _xmlReaderSettings = XmlUtil.CreateSafeXmlReaderSettings();

		// Token: 0x04000062 RID: 98
		private static readonly XmlWriterSettings _xmlWriterSettings = new XmlWriterSettings
		{
			OmitXmlDeclaration = true
		};

		// Token: 0x04000063 RID: 99
		private static readonly XmlWriterSettings _xmlWriterIndentSettings = new XmlWriterSettings
		{
			Indent = true,
			OmitXmlDeclaration = true
		};

		// Token: 0x04000064 RID: 100
		private static readonly XmlDictionaryReaderQuotas _readerQuotas = new XmlDictionaryReaderQuotas();

		// Token: 0x04000065 RID: 101
		private static readonly Encoding _jsonEncoding = Encoding.UTF8;
	}
}
