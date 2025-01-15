using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D1 RID: 209
	internal class EwsServiceXmlWriter : IDisposable
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x0001E858 File Offset: 0x0001D858
		internal EwsServiceXmlWriter(ExchangeServiceBase service, Stream stream)
		{
			this.service = service;
			this.xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
			{
				Indent = true,
				Encoding = EwsServiceXmlWriter.utf8Encoding
			});
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001E898 File Offset: 0x0001D898
		internal bool TryConvertObjectToString(object value, out string strValue)
		{
			strValue = null;
			bool flag = true;
			if (value != null)
			{
				IConvertible convertible = value as IConvertible;
				if (value.GetType().IsEnum)
				{
					strValue = EwsUtilities.SerializeEnum((Enum)value);
				}
				else if (convertible != null)
				{
					TypeCode typeCode = convertible.GetTypeCode();
					if (typeCode != 3)
					{
						if (typeCode != 16)
						{
							strValue = convertible.ToString(CultureInfo.InvariantCulture);
						}
						else
						{
							strValue = this.Service.ConvertDateTimeToUniversalDateTimeString((DateTime)value);
						}
					}
					else
					{
						strValue = EwsUtilities.BoolToXSBool((bool)value);
					}
				}
				else
				{
					IFormattable formattable = value as IFormattable;
					if (formattable != null)
					{
						strValue = formattable.ToString(null, null);
					}
					else if (value is ISearchStringProvider)
					{
						ISearchStringProvider searchStringProvider = value as ISearchStringProvider;
						strValue = searchStringProvider.GetSearchString();
					}
					else if (value is byte[])
					{
						strValue = Convert.ToBase64String((byte[])value);
					}
					else
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001E96A File Offset: 0x0001D96A
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.xmlWriter.Close();
				this.isDisposed = true;
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001E986 File Offset: 0x0001D986
		public void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001E993 File Offset: 0x0001D993
		public void WriteStartElement(XmlNamespace xmlNamespace, string localName)
		{
			this.xmlWriter.WriteStartElement(EwsUtilities.GetNamespacePrefix(xmlNamespace), localName, EwsUtilities.GetNamespaceUri(xmlNamespace));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001E9AD File Offset: 0x0001D9AD
		public void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001E9BA File Offset: 0x0001D9BA
		public void WriteAttributeValue(string localName, object value)
		{
			this.WriteAttributeValue(localName, false, value);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001E9C8 File Offset: 0x0001D9C8
		public void WriteAttributeValue(string localName, bool alwaysWriteEmptyString, object value)
		{
			string text;
			if (!this.TryConvertObjectToString(value, out text))
			{
				throw new ServiceXmlSerializationException(string.Format(Strings.AttributeValueCannotBeSerialized, value.GetType().Name, localName));
			}
			if (text != null && (alwaysWriteEmptyString || text.Length != 0))
			{
				this.WriteAttributeString(localName, text);
				return;
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001EA18 File Offset: 0x0001DA18
		public void WriteAttributeValue(string namespacePrefix, string localName, object value)
		{
			string text;
			if (!this.TryConvertObjectToString(value, out text))
			{
				throw new ServiceXmlSerializationException(string.Format(Strings.AttributeValueCannotBeSerialized, value.GetType().Name, localName));
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.WriteAttributeString(namespacePrefix, localName, text);
				return;
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001EA64 File Offset: 0x0001DA64
		internal void WriteAttributeString(string localName, string stringValue)
		{
			try
			{
				this.xmlWriter.WriteAttributeString(localName, stringValue);
			}
			catch (ArgumentException ex)
			{
				throw new ServiceXmlSerializationException(string.Format(Strings.InvalidAttributeValue, stringValue, localName), ex);
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001EAAC File Offset: 0x0001DAAC
		internal void WriteAttributeString(string namespacePrefix, string localName, string stringValue)
		{
			try
			{
				this.xmlWriter.WriteAttributeString(namespacePrefix, localName, null, stringValue);
			}
			catch (ArgumentException ex)
			{
				throw new ServiceXmlSerializationException(string.Format(Strings.InvalidAttributeValue, stringValue, localName), ex);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001EAF4 File Offset: 0x0001DAF4
		public void WriteValue(string value, string name)
		{
			try
			{
				this.xmlWriter.WriteValue(value);
			}
			catch (ArgumentException ex)
			{
				throw new ServiceXmlSerializationException(string.Format(Strings.InvalidElementStringValue, value, name), ex);
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001EB38 File Offset: 0x0001DB38
		internal void WriteElementValue(XmlNamespace xmlNamespace, string localName, string displayName, object value)
		{
			string text;
			if (!this.TryConvertObjectToString(value, out text))
			{
				throw new ServiceXmlSerializationException(string.Format(Strings.ElementValueCannotBeSerialized, value.GetType().Name, localName));
			}
			if (text != null)
			{
				this.WriteStartElement(xmlNamespace, localName);
				this.WriteValue(text, displayName);
				this.WriteEndElement();
				return;
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001EB8D File Offset: 0x0001DB8D
		public void WriteNode(XmlNode xmlNode)
		{
			if (xmlNode != null)
			{
				xmlNode.WriteTo(this.xmlWriter);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001EB9E File Offset: 0x0001DB9E
		public void WriteElementValue(XmlNamespace xmlNamespace, string localName, object value)
		{
			this.WriteElementValue(xmlNamespace, localName, localName, value);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001EBAA File Offset: 0x0001DBAA
		public void WriteBase64ElementValue(byte[] buffer)
		{
			this.xmlWriter.WriteBase64(buffer, 0, buffer.Length);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001EBBC File Offset: 0x0001DBBC
		public void WriteBase64ElementValue(Stream stream)
		{
			byte[] array = new byte[4096];
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				int num;
				do
				{
					num = binaryReader.Read(array, 0, 4096);
					if (num > 0)
					{
						this.xmlWriter.WriteBase64(array, 0, num);
					}
				}
				while (num > 0);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001EC1C File Offset: 0x0001DC1C
		public XmlWriter InternalWriter
		{
			get
			{
				return this.xmlWriter;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001EC24 File Offset: 0x0001DC24
		public ExchangeServiceBase Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001EC2C File Offset: 0x0001DC2C
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0001EC34 File Offset: 0x0001DC34
		public bool IsTimeZoneHeaderEmitted
		{
			get
			{
				return this.isTimeZoneHeaderEmitted;
			}
			set
			{
				this.isTimeZoneHeaderEmitted = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001EC3D File Offset: 0x0001DC3D
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0001EC45 File Offset: 0x0001DC45
		public bool RequireWSSecurityUtilityNamespace
		{
			get
			{
				return this.requireWSSecurityUtilityNamespace;
			}
			set
			{
				this.requireWSSecurityUtilityNamespace = value;
			}
		}

		// Token: 0x040002CA RID: 714
		private const int BufferSize = 4096;

		// Token: 0x040002CB RID: 715
		private static Encoding utf8Encoding = new UTF8Encoding(false);

		// Token: 0x040002CC RID: 716
		private bool isDisposed;

		// Token: 0x040002CD RID: 717
		private ExchangeServiceBase service;

		// Token: 0x040002CE RID: 718
		private XmlWriter xmlWriter;

		// Token: 0x040002CF RID: 719
		private bool isTimeZoneHeaderEmitted;

		// Token: 0x040002D0 RID: 720
		private bool requireWSSecurityUtilityNamespace;
	}
}
