using System;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000CD RID: 205
	internal class EwsXmlReader
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0001DDE7 File Offset: 0x0001CDE7
		public EwsXmlReader(Stream stream)
		{
			this.xmlReader = this.InitializeXmlReader(stream);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001DDFC File Offset: 0x0001CDFC
		protected virtual XmlReader InitializeXmlReader(Stream stream)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				ConformanceLevel = 0,
				ProhibitDtd = true,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				IgnoreWhitespace = true,
				XmlResolver = null
			};
			XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stream);
			xmlTextReader.Normalization = false;
			return XmlReader.Create(xmlTextReader, xmlReaderSettings);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001DE50 File Offset: 0x0001CE50
		private static string FormatElementName(string namespacePrefix, string localElementName)
		{
			if (!string.IsNullOrEmpty(namespacePrefix))
			{
				return namespacePrefix + ":" + localElementName;
			}
			return localElementName;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001DE68 File Offset: 0x0001CE68
		private void InternalReadElement(XmlNamespace xmlNamespace, string localName, XmlNodeType nodeType)
		{
			if (xmlNamespace == XmlNamespace.NotSpecified)
			{
				this.InternalReadElement(string.Empty, localName, nodeType);
				return;
			}
			this.Read(nodeType);
			if (this.LocalName != localName || this.NamespaceUri != EwsUtilities.GetNamespaceUri(xmlNamespace))
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.UnexpectedElement, new object[]
				{
					EwsUtilities.GetNamespacePrefix(xmlNamespace),
					localName,
					nodeType,
					this.xmlReader.Name,
					this.NodeType
				}));
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001DF00 File Offset: 0x0001CF00
		private void InternalReadElement(string namespacePrefix, string localName, XmlNodeType nodeType)
		{
			this.Read(nodeType);
			if (this.LocalName != localName || this.NamespacePrefix != namespacePrefix)
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.UnexpectedElement, new object[]
				{
					namespacePrefix,
					localName,
					nodeType,
					this.xmlReader.Name,
					this.NodeType
				}));
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001DF7C File Offset: 0x0001CF7C
		public void Read()
		{
			this.prevNodeType = this.xmlReader.NodeType;
			if (!this.xmlReader.Read())
			{
				throw new ServiceXmlDeserializationException(Strings.UnexpectedEndOfXmlDocument);
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001DFB9 File Offset: 0x0001CFB9
		public void Read(XmlNodeType nodeType)
		{
			this.Read();
			if (this.NodeType != nodeType)
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.UnexpectedElementType, nodeType, this.NodeType));
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001DFF0 File Offset: 0x0001CFF0
		public string ReadAttributeValue(XmlNamespace xmlNamespace, string attributeName)
		{
			if (xmlNamespace == XmlNamespace.NotSpecified)
			{
				return this.ReadAttributeValue(attributeName);
			}
			return this.xmlReader.GetAttribute(attributeName, EwsUtilities.GetNamespaceUri(xmlNamespace));
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001E00F File Offset: 0x0001D00F
		public string ReadAttributeValue(string attributeName)
		{
			return this.xmlReader.GetAttribute(attributeName);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001E01D File Offset: 0x0001D01D
		public T ReadAttributeValue<T>(string attributeName)
		{
			return EwsUtilities.Parse<T>(this.ReadAttributeValue(attributeName));
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001E02C File Offset: 0x0001D02C
		public T? ReadNullableAttributeValue<T>(string attributeName) where T : struct
		{
			string text = this.ReadAttributeValue(attributeName);
			if (text == null)
			{
				return default(T?);
			}
			return new T?(EwsUtilities.Parse<T>(text));
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001E05C File Offset: 0x0001D05C
		public string ReadElementValue(string namespacePrefix, string localName)
		{
			if (!this.IsStartElement(namespacePrefix, localName))
			{
				this.ReadStartElement(namespacePrefix, localName);
			}
			string text = null;
			if (!this.IsEmptyElement)
			{
				text = this.ReadValue();
			}
			return text;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001E090 File Offset: 0x0001D090
		public string ReadElementValue(XmlNamespace xmlNamespace, string localName)
		{
			if (!this.IsStartElement(xmlNamespace, localName))
			{
				this.ReadStartElement(xmlNamespace, localName);
			}
			string text = null;
			if (!this.IsEmptyElement)
			{
				text = this.ReadValue();
			}
			return text;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001E0C1 File Offset: 0x0001D0C1
		public string ReadElementValue()
		{
			this.EnsureCurrentNodeIsStartElement();
			return this.ReadElementValue(this.NamespacePrefix, this.LocalName);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001E0DC File Offset: 0x0001D0DC
		public T ReadElementValue<T>(XmlNamespace xmlNamespace, string localName)
		{
			if (!this.IsStartElement(xmlNamespace, localName))
			{
				this.ReadStartElement(xmlNamespace, localName);
			}
			T t = default(T);
			if (!this.IsEmptyElement)
			{
				t = this.ReadValue<T>();
			}
			return t;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001E114 File Offset: 0x0001D114
		public T ReadElementValue<T>()
		{
			this.EnsureCurrentNodeIsStartElement();
			string namespacePrefix = this.NamespacePrefix;
			string localName = this.LocalName;
			T t = default(T);
			if (!this.IsEmptyElement)
			{
				t = this.ReadValue<T>();
			}
			return t;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001E14D File Offset: 0x0001D14D
		public string ReadValue()
		{
			return this.xmlReader.ReadString();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001E15A File Offset: 0x0001D15A
		public bool TryReadValue(ref string value)
		{
			if (this.IsEmptyElement)
			{
				return false;
			}
			this.Read();
			if (this.NodeType == 3)
			{
				value = this.xmlReader.Value;
				return true;
			}
			return false;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001E185 File Offset: 0x0001D185
		public T ReadValue<T>()
		{
			return EwsUtilities.Parse<T>(this.ReadValue());
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001E194 File Offset: 0x0001D194
		public byte[] ReadBase64ElementValue()
		{
			this.EnsureCurrentNodeIsStartElement();
			byte[] array = new byte[4096];
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num;
				do
				{
					num = this.xmlReader.ReadElementContentAsBase64(array, 0, 4096);
					if (num > 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				while (num > 0);
				array2 = ((memoryStream.Length == (long)memoryStream.Capacity) ? memoryStream.GetBuffer() : memoryStream.ToArray());
			}
			return array2;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001E218 File Offset: 0x0001D218
		public void ReadBase64ElementValue(Stream outputStream)
		{
			this.EnsureCurrentNodeIsStartElement();
			byte[] array = new byte[4096];
			int num;
			do
			{
				num = this.xmlReader.ReadElementContentAsBase64(array, 0, 4096);
				if (num > 0)
				{
					outputStream.Write(array, 0, num);
				}
			}
			while (num > 0);
			outputStream.Flush();
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001E260 File Offset: 0x0001D260
		public void ReadStartElement(string namespacePrefix, string localName)
		{
			this.InternalReadElement(namespacePrefix, localName, 1);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001E26B File Offset: 0x0001D26B
		public void ReadStartElement(XmlNamespace xmlNamespace, string localName)
		{
			this.InternalReadElement(xmlNamespace, localName, 1);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001E276 File Offset: 0x0001D276
		public void ReadEndElement(string namespacePrefix, string elementName)
		{
			this.InternalReadElement(namespacePrefix, elementName, 15);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001E282 File Offset: 0x0001D282
		public void ReadEndElement(XmlNamespace xmlNamespace, string localName)
		{
			this.InternalReadElement(xmlNamespace, localName, 15);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001E28E File Offset: 0x0001D28E
		public void ReadEndElementIfNecessary(XmlNamespace xmlNamespace, string localName)
		{
			if ((!this.IsStartElement(xmlNamespace, localName) || !this.IsEmptyElement) && !this.IsEndElement(xmlNamespace, localName))
			{
				this.ReadEndElement(xmlNamespace, localName);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001E2B4 File Offset: 0x0001D2B4
		public bool IsStartElement(string namespacePrefix, string localName)
		{
			string text = EwsXmlReader.FormatElementName(namespacePrefix, localName);
			return this.NodeType == 1 && this.xmlReader.Name == text;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001E2E5 File Offset: 0x0001D2E5
		public bool IsStartElement(XmlNamespace xmlNamespace, string localName)
		{
			return this.LocalName == localName && this.IsStartElement() && (this.NamespacePrefix == EwsUtilities.GetNamespacePrefix(xmlNamespace) || this.NamespaceUri == EwsUtilities.GetNamespaceUri(xmlNamespace));
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001E325 File Offset: 0x0001D325
		public bool IsStartElement()
		{
			return this.NodeType == 1;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001E330 File Offset: 0x0001D330
		public bool IsEndElement(string namespacePrefix, string localName)
		{
			string text = EwsXmlReader.FormatElementName(namespacePrefix, localName);
			return this.NodeType == 15 && this.xmlReader.Name == text;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001E364 File Offset: 0x0001D364
		public bool IsEndElement(XmlNamespace xmlNamespace, string localName)
		{
			return this.LocalName == localName && this.NodeType == 15 && (this.NamespacePrefix == EwsUtilities.GetNamespacePrefix(xmlNamespace) || this.NamespaceUri == EwsUtilities.GetNamespaceUri(xmlNamespace));
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001E3B1 File Offset: 0x0001D3B1
		public void SkipElement(string namespacePrefix, string localName)
		{
			if (!this.IsEndElement(namespacePrefix, localName))
			{
				if (!this.IsStartElement(namespacePrefix, localName))
				{
					this.ReadStartElement(namespacePrefix, localName);
				}
				if (!this.IsEmptyElement)
				{
					do
					{
						this.Read();
					}
					while (!this.IsEndElement(namespacePrefix, localName));
				}
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001E3E7 File Offset: 0x0001D3E7
		public void SkipElement(XmlNamespace xmlNamespace, string localName)
		{
			if (!this.IsEndElement(xmlNamespace, localName))
			{
				if (!this.IsStartElement(xmlNamespace, localName))
				{
					this.ReadStartElement(xmlNamespace, localName);
				}
				if (!this.IsEmptyElement)
				{
					do
					{
						this.Read();
					}
					while (!this.IsEndElement(xmlNamespace, localName));
				}
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001E41D File Offset: 0x0001D41D
		public void SkipCurrentElement()
		{
			this.SkipElement(this.NamespacePrefix, this.LocalName);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001E431 File Offset: 0x0001D431
		public void EnsureCurrentNodeIsStartElement(XmlNamespace xmlNamespace, string localName)
		{
			if (!this.IsStartElement(xmlNamespace, localName))
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.ElementNotFound, localName, xmlNamespace));
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001E459 File Offset: 0x0001D459
		public void EnsureCurrentNodeIsStartElement()
		{
			if (this.NodeType != 1)
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.ExpectedStartElement, this.xmlReader.Name, this.NodeType));
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001E48F File Offset: 0x0001D48F
		public void EnsureCurrentNodeIsEndElement(XmlNamespace xmlNamespace, string localName)
		{
			if (!this.IsEndElement(xmlNamespace, localName) && (!this.IsStartElement(xmlNamespace, localName) || !this.IsEmptyElement))
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.ElementNotFound, localName, xmlNamespace));
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001E4C9 File Offset: 0x0001D4C9
		public string ReadOuterXml()
		{
			if (!this.IsStartElement())
			{
				throw new ServiceXmlDeserializationException(Strings.CurrentPositionNotElementStart);
			}
			return this.xmlReader.ReadOuterXml();
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001E4EE File Offset: 0x0001D4EE
		public string ReadInnerXml()
		{
			if (!this.IsStartElement())
			{
				throw new ServiceXmlDeserializationException(Strings.CurrentPositionNotElementStart);
			}
			return this.xmlReader.ReadInnerXml();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001E513 File Offset: 0x0001D513
		internal XmlReader GetXmlReaderForNode()
		{
			return this.xmlReader.ReadSubtree();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001E520 File Offset: 0x0001D520
		public void ReadToDescendant(XmlNamespace xmlNamespace, string localName)
		{
			this.xmlReader.ReadToDescendant(localName, EwsUtilities.GetNamespaceUri(xmlNamespace));
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001E535 File Offset: 0x0001D535
		public bool HasAttributes
		{
			get
			{
				return this.xmlReader.AttributeCount > 0;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001E545 File Offset: 0x0001D545
		public bool IsEmptyElement
		{
			get
			{
				return this.xmlReader.IsEmptyElement;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001E552 File Offset: 0x0001D552
		public string LocalName
		{
			get
			{
				return this.xmlReader.LocalName;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001E55F File Offset: 0x0001D55F
		public string NamespacePrefix
		{
			get
			{
				return this.xmlReader.Prefix;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001E56C File Offset: 0x0001D56C
		public string NamespaceUri
		{
			get
			{
				return this.xmlReader.NamespaceURI;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001E579 File Offset: 0x0001D579
		public XmlNodeType NodeType
		{
			get
			{
				return this.xmlReader.NodeType;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001E586 File Offset: 0x0001D586
		public XmlNodeType PrevNodeType
		{
			get
			{
				return this.prevNodeType;
			}
		}

		// Token: 0x040002C5 RID: 709
		private const int ReadWriteBufferSize = 4096;

		// Token: 0x040002C6 RID: 710
		private XmlNodeType prevNodeType;

		// Token: 0x040002C7 RID: 711
		private XmlReader xmlReader;
	}
}
