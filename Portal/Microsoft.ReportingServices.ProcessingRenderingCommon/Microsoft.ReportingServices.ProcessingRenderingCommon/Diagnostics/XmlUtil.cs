using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000030 RID: 48
	public static class XmlUtil
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00005F7C File Offset: 0x0000417C
		public static List<T> ReadList<T>(XmlDictionaryReader reader, Func<XmlDictionaryReader, T> readObject)
		{
			List<T> list = new List<T>();
			while (XmlUtil.Advance(reader))
			{
				if (XmlUtil.IsStartElement(reader))
				{
					list.Add(readObject(reader));
				}
			}
			return list;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005FB0 File Offset: 0x000041B0
		public static void SkipUnknownContent(XmlDictionaryReader reader)
		{
			int num = 0;
			while (reader.Read() && (!XmlUtil.IsEndElement(reader) || num > 0))
			{
				if (XmlUtil.IsStartElement(reader))
				{
					num++;
				}
				else if (XmlUtil.IsEndElement(reader))
				{
					num--;
				}
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005FEF File Offset: 0x000041EF
		public static bool IsStartElement(XmlDictionaryReader reader)
		{
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005FFA File Offset: 0x000041FA
		public static bool IsEndElement(XmlDictionaryReader reader)
		{
			return reader.NodeType == XmlNodeType.EndElement;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006006 File Offset: 0x00004206
		public static bool Advance(XmlDictionaryReader reader)
		{
			return reader.Read() && !XmlUtil.IsEndElement(reader);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000601B File Offset: 0x0000421B
		[Conditional("DEBUG")]
		public static void AssertReadMethodEnterState(XmlDictionaryReader reader)
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000601D File Offset: 0x0000421D
		[Conditional("DEBUG")]
		public static void AssertReadMethodExitState(XmlDictionaryReader reader)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006020 File Offset: 0x00004220
		public static bool TryParseOptional(XmlDictionaryReader reader, out Guid guid)
		{
			guid = Guid.Empty;
			string text = reader.ReadString();
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					guid = new Guid(text);
				}
				catch (OverflowException)
				{
					return false;
				}
				catch (FormatException)
				{
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006080 File Offset: 0x00004280
		public static bool TryParse(XmlDictionaryReader reader, out Guid guid)
		{
			guid = Guid.Empty;
			reader.ReadStartElement();
			try
			{
				guid = reader.ReadContentAsGuid();
			}
			catch (ArgumentException)
			{
				reader.Read();
				return false;
			}
			catch (FormatException)
			{
				reader.Read();
				return false;
			}
			catch (XmlException)
			{
				reader.Read();
				return false;
			}
			return true;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000060FC File Offset: 0x000042FC
		public static bool TryParse(XmlDictionaryReader reader, out DateTime datetime)
		{
			datetime = DateTime.MinValue;
			reader.ReadStartElement();
			try
			{
				datetime = reader.ReadContentAsDateTime();
			}
			catch (FormatException)
			{
				reader.Read();
				return false;
			}
			catch (InvalidCastException)
			{
				reader.Read();
				return false;
			}
			catch (XmlException)
			{
				reader.Read();
				return false;
			}
			return true;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006178 File Offset: 0x00004378
		public static bool TryParse(XmlDictionaryReader reader, out int i)
		{
			i = int.MinValue;
			reader.ReadStartElement();
			try
			{
				i = reader.ReadContentAsInt();
			}
			catch (XmlException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000061B4 File Offset: 0x000043B4
		public static void SafeOpenXmlDocumentFile(XmlDocument doc, string pathToXmlFile)
		{
			FileStream fileStream = new FileStream(pathToXmlFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			try
			{
				XmlTextReader xmlTextReader = new XmlTextReader(fileStream);
				XmlUtil.ApplyDtdDosDefense(xmlTextReader);
				doc.Load(xmlTextReader);
			}
			finally
			{
				fileStream.Close();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000061FC File Offset: 0x000043FC
		public static void SafeOpenXmlDocumentString(XmlDocument doc, string xmlContent)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xmlContent));
			XmlUtil.ApplyDtdDosDefense(xmlTextReader);
			xmlTextReader.MoveToContent();
			doc.Load(xmlTextReader);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000622A File Offset: 0x0000442A
		public static XmlTextReader SafeCreateXmlTextReader(string xmlContent)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xmlContent));
			XmlUtil.ApplyDtdDosDefense(xmlTextReader);
			return xmlTextReader;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000623E File Offset: 0x0000443E
		public static XmlTextReader SafeCreateXmlTextReader(Stream xmlStream)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(xmlStream);
			XmlUtil.ApplyDtdDosDefense(xmlTextReader);
			return xmlTextReader;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000624D File Offset: 0x0000444D
		public static XmlTextReader SafeCreateXmlTextReader(byte[] xmlBytes)
		{
			return XmlUtil.SafeCreateXmlTextReader(new MemoryStream(xmlBytes, false));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000625C File Offset: 0x0000445C
		public static NameValueCollection ShallowXmlToNameValueCollection(string xml, string topElementTag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (xml == null || xml == string.Empty)
			{
				return nameValueCollection;
			}
			XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType != XmlNodeType.Element || string.Compare(xmlTextReader.Name, topElementTag, StringComparison.Ordinal) != 0)
				{
					throw new InvalidXmlException();
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						bool isEmptyElement = xmlTextReader.IsEmptyElement;
						string text = xmlTextReader.Name;
						text = XmlUtil.DecodePropertyName(text);
						string text2 = xmlTextReader.ReadString();
						if (nameValueCollection.GetValues(text) != null)
						{
							throw new InvalidXmlException();
						}
						nameValueCollection[text] = text2;
						if (!isEmptyElement && xmlTextReader.IsStartElement())
						{
							throw new InvalidXmlException();
						}
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			return nameValueCollection;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006318 File Offset: 0x00004518
		public static NameValueCollection DeepXmlToNameValueCollection(string xml, string topElementTag, string eachElementTag, string nameElementTag, string valueElementTag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(StringComparer.InvariantCulture);
			if (xml == null || xml == string.Empty)
			{
				return nameValueCollection;
			}
			XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType != XmlNodeType.Element || string.Compare(xmlTextReader.Name, topElementTag, StringComparison.Ordinal) != 0)
				{
					throw new InvalidXmlException();
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						if (xmlTextReader.IsEmptyElement || string.Compare(xmlTextReader.Name, eachElementTag, StringComparison.Ordinal) != 0)
						{
							throw new InvalidXmlException();
						}
						xmlTextReader.Read();
						string text = null;
						string text2 = null;
						while (xmlTextReader.IsStartElement())
						{
							bool isEmptyElement = xmlTextReader.IsEmptyElement;
							string name = xmlTextReader.Name;
							string text3 = xmlTextReader.ReadString();
							if (string.Compare(name, nameElementTag, StringComparison.Ordinal) == 0)
							{
								text = text3;
							}
							else if (string.Compare(name, valueElementTag, StringComparison.Ordinal) == 0)
							{
								text2 = text3;
							}
							if (!isEmptyElement)
							{
								xmlTextReader.ReadEndElement();
							}
							else
							{
								xmlTextReader.Read();
							}
						}
						if (text == null)
						{
							throw new InvalidXmlException();
						}
						nameValueCollection.Add(text, text2);
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			return nameValueCollection;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000642C File Offset: 0x0000462C
		public static string NameValueCollectionToShallowXml(NameValueCollection parameters, string topElementTag)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement(topElementTag);
			for (int i = 0; i < parameters.Count; i++)
			{
				string key = parameters.GetKey(i);
				string text = parameters.Get(i);
				if (key != null && text != null)
				{
					if (string.IsNullOrEmpty(key))
					{
						throw new InternalCatalogException("Empty Property Name");
					}
					string text2 = XmlUtil.EncodePropertyName(key);
					RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(text2), "encodedName");
					xmlTextWriter.WriteStartElement(text2);
					xmlTextWriter.WriteString(text);
					xmlTextWriter.WriteEndElement();
				}
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000064D8 File Offset: 0x000046D8
		public static string NameValueCollectionToDeepXml(NameValueCollection parameters, string topElementTag, string eachElementTag, string nameElementTag, string valueElementTag)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement(topElementTag);
			for (int i = 0; i < parameters.Count; i++)
			{
				xmlTextWriter.WriteStartElement(eachElementTag);
				string key = parameters.GetKey(i);
				if (key != null)
				{
					xmlTextWriter.WriteElementString(nameElementTag, key);
				}
				string text = parameters.Get(i);
				if (text != null)
				{
					xmlTextWriter.WriteElementString(valueElementTag, text);
				}
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006559 File Offset: 0x00004759
		public static string EncodePropertyName(string name)
		{
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(name), "name");
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006579 File Offset: 0x00004779
		public static string DecodePropertyName(string name)
		{
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(name), "name");
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006599 File Offset: 0x00004799
		public static XmlReaderSettings ApplyDtdDosDefense(XmlReaderSettings settings)
		{
			settings.DtdProcessing = DtdProcessing.Prohibit;
			settings.XmlResolver = null;
			return settings;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000065AA File Offset: 0x000047AA
		public static XmlTextReader ApplyDtdDosDefense(XmlTextReader reader)
		{
			reader.DtdProcessing = DtdProcessing.Prohibit;
			reader.XmlResolver = null;
			return reader;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000065BB File Offset: 0x000047BB
		public static XmlDocument CreateXmlDocumentWithNullResolver()
		{
			return new XmlDocument
			{
				XmlResolver = null
			};
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000065C9 File Offset: 0x000047C9
		internal static XmlSchema LoadSchemaFromResourceWithNullResolver(Stream resourceStream)
		{
			return XmlSchema.Read(XmlUtil.CreateXmlReaderWithNullResolver(new StreamReader(resourceStream)), null);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000065DC File Offset: 0x000047DC
		internal static object DeserializeWithNullResolver(this XmlSerializer xmlSerializer, StringReader stringReader)
		{
			XmlReader xmlReader = XmlUtil.CreateXmlReaderWithNullResolver(stringReader);
			return xmlSerializer.Deserialize(xmlReader);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000065F8 File Offset: 0x000047F8
		internal static object DeserializeWithNullResolver(this XmlSerializer xmlSerializer, Stream stream)
		{
			XmlReader xmlReader = XmlUtil.CreateXmlReaderWithNullResolver(new StreamReader(stream));
			return xmlSerializer.Deserialize(xmlReader);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006618 File Offset: 0x00004818
		public static void LoadWithNullResolver(this XmlDocument xmlDocument, Stream stream)
		{
			XmlReader xmlReader = XmlUtil.CreateXmlReaderWithNullResolver(new StreamReader(stream));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006638 File Offset: 0x00004838
		public static void LoadWithNullResolver(this XmlDocument xmlDocument, TextReader textReader)
		{
			XmlReader xmlReader = XmlUtil.CreateXmlReaderWithNullResolver(textReader);
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006654 File Offset: 0x00004854
		public static void LoadWithNullResolver(this XmlDocument xmlDocument, string fileLocation)
		{
			XmlReader xmlReader = XmlUtil.CreateXmlReaderWithNullResolver(new StreamReader(fileLocation));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006674 File Offset: 0x00004874
		public static void LoadXmlWithNullResolver(this XmlDocument xmlDocument, string value)
		{
			XmlReader xmlReader = XmlUtil.CreateXmlReaderWithNullResolver(new StringReader(value));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006694 File Offset: 0x00004894
		private static XmlReader CreateXmlReaderWithNullResolver(TextReader textReader)
		{
			return XmlReader.Create(textReader, new XmlReaderSettings
			{
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			});
		}
	}
}
