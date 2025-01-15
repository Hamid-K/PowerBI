using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002123 RID: 8483
	internal abstract class OpenXmlPartRootElement : OpenXmlCompositeElement
	{
		// Token: 0x0600D1E1 RID: 53729 RVA: 0x0029BDB4 File Offset: 0x00299FB4
		protected OpenXmlPartRootElement()
		{
			this._elementContext = new OpenXmlElementContext();
		}

		// Token: 0x0600D1E2 RID: 53730 RVA: 0x0029BDD3 File Offset: 0x00299FD3
		protected OpenXmlPartRootElement(OpenXmlPart openXmlPart)
		{
			if (openXmlPart == null)
			{
				throw new ArgumentNullException("openXmlPart");
			}
			this._elementContext = new OpenXmlElementContext();
			this.LoadFromPart(openXmlPart);
		}

		// Token: 0x0600D1E3 RID: 53731 RVA: 0x0029BE07 File Offset: 0x0029A007
		protected OpenXmlPartRootElement(string outerXml)
			: base(outerXml)
		{
			this._elementContext = new OpenXmlElementContext();
		}

		// Token: 0x0600D1E4 RID: 53732 RVA: 0x0029BE27 File Offset: 0x0029A027
		protected OpenXmlPartRootElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
			this._elementContext = new OpenXmlElementContext();
		}

		// Token: 0x0600D1E5 RID: 53733 RVA: 0x0029BE47 File Offset: 0x0029A047
		protected OpenXmlPartRootElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
			this._elementContext = new OpenXmlElementContext();
		}

		// Token: 0x170032BE RID: 12990
		// (get) Token: 0x0600D1E6 RID: 53734 RVA: 0x0029BE67 File Offset: 0x0029A067
		internal override OpenXmlElementContext RootElementContext
		{
			get
			{
				return this._elementContext;
			}
		}

		// Token: 0x0600D1E7 RID: 53735 RVA: 0x0029BE70 File Offset: 0x0029A070
		internal void LoadFromPart(OpenXmlPart openXmlPart)
		{
			using (Stream stream = openXmlPart.GetStream(FileMode.Open))
			{
				this.LoadFromPart(openXmlPart, stream);
			}
		}

		// Token: 0x0600D1E8 RID: 53736 RVA: 0x0029BEAC File Offset: 0x0029A0AC
		internal bool LoadFromPart(OpenXmlPart openXmlPart, Stream partStream)
		{
			if (partStream.Length < 4L)
			{
				return false;
			}
			base.OpenXmlElementContext.XmlReaderSettings.MaxCharactersInDocument = openXmlPart.MaxCharactersInPart;
			using (XmlReader xmlReader = XmlReader.Create(partStream, base.OpenXmlElementContext.XmlReaderSettings))
			{
				base.OpenXmlElementContext.MCSettings = openXmlPart.MCSettings;
				xmlReader.Read();
				if (xmlReader.NodeType == XmlNodeType.XmlDeclaration)
				{
					string attribute = xmlReader.GetAttribute("standalone");
					if (attribute != null)
					{
						this._standaloneDeclaration = new bool?(attribute.Equals("yes", StringComparison.OrdinalIgnoreCase));
					}
				}
				if (!xmlReader.EOF)
				{
					xmlReader.MoveToContent();
				}
				if (xmlReader.EOF || XmlNodeType.Element != xmlReader.NodeType || !xmlReader.IsStartElement())
				{
					return false;
				}
				byte b;
				if (!NamespaceIdMap.TryGetNamespaceId(xmlReader.NamespaceURI, out b) || b != this.NamespaceId || xmlReader.LocalName != this.LocalName)
				{
					string text = new XmlQualifiedName(xmlReader.LocalName, xmlReader.NamespaceURI).ToString();
					string text2 = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.Fmt_PartRootIsInvalid, new object[]
					{
						text,
						this.XmlQualifiedName.ToString()
					});
					throw new InvalidDataException(text2);
				}
				base.OuterXml = string.Empty;
				bool flag = base.PushMcContext(xmlReader);
				base.Load(xmlReader, base.OpenXmlElementContext.LoadMode);
				if (flag)
				{
					base.PopMcContext();
				}
			}
			return true;
		}

		// Token: 0x0600D1E9 RID: 53737 RVA: 0x0029C03C File Offset: 0x0029A23C
		internal void LoadFromPart(OpenXmlPart openXmlPart, OpenXmlLoadMode loadMode)
		{
			base.OpenXmlElementContext.LoadMode = loadMode;
			this.LoadFromPart(openXmlPart);
		}

		// Token: 0x0600D1EA RID: 53738 RVA: 0x0029C054 File Offset: 0x0029A254
		internal void SaveToPart(OpenXmlPart openXmlPart)
		{
			if (openXmlPart == null)
			{
				throw new ArgumentNullException("openXmlPart");
			}
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = true;
			using (Stream stream = openXmlPart.GetStream(FileMode.Create))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
				{
					if (this._standaloneDeclaration != null)
					{
						xmlWriter.WriteStartDocument(this._standaloneDeclaration.Value);
					}
					this.WriteTo(xmlWriter);
					if (base.XmlParsed)
					{
						xmlWriter.WriteEndDocument();
					}
				}
			}
		}

		// Token: 0x0600D1EB RID: 53739 RVA: 0x0029C0F4 File Offset: 0x0029A2F4
		public void Save(Stream stream)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CloseOutput = true
			}))
			{
				if (this._standaloneDeclaration != null)
				{
					xmlWriter.WriteStartDocument(this._standaloneDeclaration.Value);
				}
				this.WriteTo(xmlWriter);
				if (base.XmlParsed)
				{
					xmlWriter.WriteEndDocument();
				}
			}
		}

		// Token: 0x170032BF RID: 12991
		// (get) Token: 0x0600D1EC RID: 53740 RVA: 0x0029C168 File Offset: 0x0029A368
		// (set) Token: 0x0600D1ED RID: 53741 RVA: 0x0029C170 File Offset: 0x0029A370
		internal OpenXmlPart OpenXmlPart { get; set; }

		// Token: 0x0600D1EE RID: 53742 RVA: 0x0029C179 File Offset: 0x0029A379
		public void Save()
		{
			if (this.OpenXmlPart == null)
			{
				throw new InvalidOperationException(ExceptionMessages.CannotSaveDomTreeWithoutAssociatedPart);
			}
			this.SaveToPart(this.OpenXmlPart);
		}

		// Token: 0x0600D1EF RID: 53743 RVA: 0x0029C19A File Offset: 0x0029A39A
		public void Reload()
		{
			if (this.OpenXmlPart == null)
			{
				throw new InvalidOperationException(ExceptionMessages.CannotReloadDomTreeWithoutAssociatedPart);
			}
			this.LoadFromPart(this.OpenXmlPart);
		}

		// Token: 0x0600D1F0 RID: 53744 RVA: 0x0029C1BC File Offset: 0x0029A3BC
		public override void WriteTo(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			if (!base.XmlParsed)
			{
				xmlWriter.WriteRaw(base.RawOuterXml);
				return;
			}
			string text = base.LookupNamespaceLocal(this.NamespaceUri);
			if (base.Parent != null && string.IsNullOrEmpty(text))
			{
				text = xmlWriter.LookupPrefix(this.NamespaceUri);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = NamespaceIdMap.GetNamespacePrefix(this.NamespaceId);
			}
			xmlWriter.WriteStartElement(text, this.LocalName, this.NamespaceUri);
			this.WriteNamespaceAtributes(xmlWriter);
			this.WriteAttributesTo(xmlWriter);
			if (this.HasChildren || !string.IsNullOrEmpty(this.InnerText))
			{
				this.WriteContentTo(xmlWriter);
				xmlWriter.WriteFullEndElement();
				return;
			}
			xmlWriter.WriteEndElement();
		}

		// Token: 0x0600D1F1 RID: 53745 RVA: 0x0029C278 File Offset: 0x0029A478
		private void WriteNamespaceAtributes(XmlWriter xmlWrite)
		{
			if (this.WriteAllNamespaceOnRoot)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (OpenXmlElement openXmlElement in base.Descendants())
				{
					if (openXmlElement.NamespaceDeclField != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in openXmlElement.NamespaceDeclField)
						{
							if (!dictionary.ContainsKey(keyValuePair.Key))
							{
								dictionary.Add(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in dictionary)
				{
					if (!string.IsNullOrEmpty(keyValuePair2.Key) && base.NamespaceDeclField != null && string.IsNullOrEmpty(base.LookupPrefixLocal(keyValuePair2.Value)) && string.IsNullOrEmpty(base.LookupNamespaceLocal(keyValuePair2.Key)))
					{
						xmlWrite.WriteAttributeString("xmlns", keyValuePair2.Key, "http://www.w3.org/2000/xmlns/", keyValuePair2.Value);
					}
				}
			}
		}

		// Token: 0x170032C0 RID: 12992
		// (get) Token: 0x0600D1F2 RID: 53746 RVA: 0x00002139 File Offset: 0x00000339
		internal virtual bool WriteAllNamespaceOnRoot
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006968 RID: 26984
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private OpenXmlElementContext _elementContext;

		// Token: 0x04006969 RID: 26985
		private bool? _standaloneDeclaration = null;
	}
}
