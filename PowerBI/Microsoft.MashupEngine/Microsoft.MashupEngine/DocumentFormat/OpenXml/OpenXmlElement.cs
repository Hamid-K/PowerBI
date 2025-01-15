using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020D9 RID: 8409
	public abstract class OpenXmlElement : IEnumerable<OpenXmlElement>, IEnumerable, ICloneable
	{
		// Token: 0x17003183 RID: 12675
		// (get) Token: 0x0600CE15 RID: 52757 RVA: 0x0028FE17 File Offset: 0x0028E017
		// (set) Token: 0x0600CE16 RID: 52758 RVA: 0x0028FE1F File Offset: 0x0028E01F
		internal MiscAttrContainer MiscAttrContainer { get; set; }

		// Token: 0x17003184 RID: 12676
		// (get) Token: 0x0600CE17 RID: 52759 RVA: 0x0028FE28 File Offset: 0x0028E028
		// (set) Token: 0x0600CE18 RID: 52760 RVA: 0x0028FE3F File Offset: 0x0028E03F
		private List<OpenXmlAttribute> ExtendedAttributesField
		{
			get
			{
				if (this.MiscAttrContainer == null)
				{
					return null;
				}
				return this.MiscAttrContainer.ExtendedAttributesField;
			}
			set
			{
				if (this.MiscAttrContainer == null)
				{
					this.MiscAttrContainer = new MiscAttrContainer();
				}
				this.MiscAttrContainer.ExtendedAttributesField = value;
			}
		}

		// Token: 0x17003185 RID: 12677
		// (get) Token: 0x0600CE19 RID: 52761 RVA: 0x0028FE60 File Offset: 0x0028E060
		// (set) Token: 0x0600CE1A RID: 52762 RVA: 0x0028FE77 File Offset: 0x0028E077
		private MarkupCompatibilityAttributes McAttributesFiled
		{
			get
			{
				if (this.MiscAttrContainer == null)
				{
					return null;
				}
				return this.MiscAttrContainer._mcAttributes;
			}
			set
			{
				if (this.MiscAttrContainer == null)
				{
					this.MiscAttrContainer = new MiscAttrContainer();
				}
				this.MiscAttrContainer._mcAttributes = value;
			}
		}

		// Token: 0x17003186 RID: 12678
		// (get) Token: 0x0600CE1B RID: 52763 RVA: 0x0028FE98 File Offset: 0x0028E098
		// (set) Token: 0x0600CE1C RID: 52764 RVA: 0x0028FEAF File Offset: 0x0028E0AF
		internal List<KeyValuePair<string, string>> NamespaceDeclField
		{
			get
			{
				if (this.MiscAttrContainer == null)
				{
					return null;
				}
				return this.MiscAttrContainer._nsMappings;
			}
			set
			{
				if (this.MiscAttrContainer == null)
				{
					this.MiscAttrContainer = new MiscAttrContainer();
				}
				this.MiscAttrContainer._nsMappings = value;
			}
		}

		// Token: 0x0600CE1D RID: 52765 RVA: 0x0028FED0 File Offset: 0x0028E0D0
		protected OpenXmlElement()
		{
		}

		// Token: 0x0600CE1E RID: 52766 RVA: 0x0028FEE3 File Offset: 0x0028E0E3
		protected OpenXmlElement(string outerXml)
			: this()
		{
			if (!string.IsNullOrEmpty(outerXml))
			{
				if (!this.ValidOuterXml(outerXml, this.NamespaceUri, this.LocalName))
				{
					throw new ArgumentException(ExceptionMessages.InvalidOuterXml, "outerXml");
				}
				this.RawOuterXml = outerXml;
			}
		}

		// Token: 0x17003187 RID: 12679
		// (get) Token: 0x0600CE1F RID: 52767 RVA: 0x0028FF1F File Offset: 0x0028E11F
		// (set) Token: 0x0600CE20 RID: 52768 RVA: 0x0028FF27 File Offset: 0x0028E127
		internal OpenXmlElement next
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x17003188 RID: 12680
		// (get) Token: 0x0600CE21 RID: 52769 RVA: 0x0028FF30 File Offset: 0x0028E130
		internal bool XmlParsed
		{
			get
			{
				return string.IsNullOrEmpty(this._rawOuterXml);
			}
		}

		// Token: 0x17003189 RID: 12681
		// (get) Token: 0x0600CE22 RID: 52770 RVA: 0x0028FF3D File Offset: 0x0028E13D
		// (set) Token: 0x0600CE23 RID: 52771 RVA: 0x0028FF45 File Offset: 0x0028E145
		internal string RawOuterXml
		{
			get
			{
				return this._rawOuterXml;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this._rawOuterXml = string.Empty;
				}
				this._rawOuterXml = value;
			}
		}

		// Token: 0x1700318A RID: 12682
		// (get) Token: 0x0600CE24 RID: 52772 RVA: 0x0028FF61 File Offset: 0x0028E161
		private OpenXmlSimpleType[] FixedAttributesArray
		{
			get
			{
				if (this._fixedAttributes == null && this.FixedAttributeTotal > 0)
				{
					this._fixedAttributes = new OpenXmlSimpleType[this.FixedAttributeTotal];
				}
				return this._fixedAttributes;
			}
		}

		// Token: 0x1700318B RID: 12683
		// (get) Token: 0x0600CE25 RID: 52773 RVA: 0x0028FF8B File Offset: 0x0028E18B
		internal OpenXmlSimpleType[] Attributes
		{
			get
			{
				this.MakeSureParsed();
				return this.FixedAttributesArray;
			}
		}

		// Token: 0x1700318C RID: 12684
		// (get) Token: 0x0600CE26 RID: 52774
		internal abstract byte NamespaceId { get; }

		// Token: 0x1700318D RID: 12685
		// (get) Token: 0x0600CE27 RID: 52775 RVA: 0x0028FF99 File Offset: 0x0028E199
		internal int FixedAttributeTotal
		{
			get
			{
				if (this.AttributeTagNames != null)
				{
					return this.AttributeTagNames.Length;
				}
				return 0;
			}
		}

		// Token: 0x1700318E RID: 12686
		// (get) Token: 0x0600CE28 RID: 52776 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual string[] AttributeTagNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700318F RID: 12687
		// (get) Token: 0x0600CE29 RID: 52777 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual byte[] AttributeNamespaceIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17003190 RID: 12688
		// (get) Token: 0x0600CE2A RID: 52778 RVA: 0x0028FFAD File Offset: 0x0028E1AD
		internal virtual int ElementTypeId
		{
			get
			{
				return 9000;
			}
		}

		// Token: 0x17003191 RID: 12689
		// (get) Token: 0x0600CE2B RID: 52779 RVA: 0x0028FFB4 File Offset: 0x0028E1B4
		public OpenXmlElementContext OpenXmlElementContext
		{
			get
			{
				return this.RootElementContext;
			}
		}

		// Token: 0x17003192 RID: 12690
		// (get) Token: 0x0600CE2C RID: 52780 RVA: 0x0028FFBC File Offset: 0x0028E1BC
		internal virtual OpenXmlElementContext RootElementContext
		{
			get
			{
				if (this.Parent != null)
				{
					return this.Parent.RootElementContext;
				}
				return null;
			}
		}

		// Token: 0x17003193 RID: 12691
		// (get) Token: 0x0600CE2D RID: 52781 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual OpenXmlElement FirstChild
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17003194 RID: 12692
		// (get) Token: 0x0600CE2E RID: 52782 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual OpenXmlElement LastChild
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17003195 RID: 12693
		// (get) Token: 0x0600CE2F RID: 52783 RVA: 0x0028FFD4 File Offset: 0x0028E1D4
		public bool HasAttributes
		{
			get
			{
				this.MakeSureParsed();
				if (this.ExtendedAttributesField != null && this.ExtendedAttributesField.Count != 0)
				{
					return true;
				}
				if (this.Attributes != null)
				{
					foreach (OpenXmlSimpleType openXmlSimpleType in this.Attributes)
					{
						if (openXmlSimpleType != null)
						{
							return true;
						}
					}
				}
				return this.MCAttributeCount() > 0;
			}
		}

		// Token: 0x17003196 RID: 12694
		// (get) Token: 0x0600CE30 RID: 52784 RVA: 0x00290034 File Offset: 0x0028E234
		public IEnumerable<OpenXmlAttribute> ExtendedAttributes
		{
			get
			{
				this.MakeSureParsed();
				if (this.ExtendedAttributesField != null)
				{
					return this.ExtendedAttributesField;
				}
				return EmptyEnumerable<OpenXmlAttribute>.EmptyEnumerableSingleton;
			}
		}

		// Token: 0x17003197 RID: 12695
		// (get) Token: 0x0600CE31 RID: 52785
		public abstract bool HasChildren { get; }

		// Token: 0x17003198 RID: 12696
		// (get) Token: 0x0600CE32 RID: 52786 RVA: 0x00290050 File Offset: 0x0028E250
		public virtual OpenXmlElementList ChildElements
		{
			get
			{
				if (this.HasChildren)
				{
					return new OpenXmlChildElements(this);
				}
				return EmptyElementList.EmptyElementListSingleton;
			}
		}

		// Token: 0x17003199 RID: 12697
		// (get) Token: 0x0600CE33 RID: 52787 RVA: 0x00290066 File Offset: 0x0028E266
		// (set) Token: 0x0600CE34 RID: 52788 RVA: 0x0029006E File Offset: 0x0028E26E
		public OpenXmlElement Parent
		{
			get
			{
				return this._parent;
			}
			internal set
			{
				this._parent = value;
			}
		}

		// Token: 0x1700319A RID: 12698
		// (get) Token: 0x0600CE35 RID: 52789 RVA: 0x00290077 File Offset: 0x0028E277
		public virtual string NamespaceUri
		{
			get
			{
				return NamespaceIdMap.GetNamespaceUri(this.NamespaceId);
			}
		}

		// Token: 0x1700319B RID: 12699
		// (get) Token: 0x0600CE36 RID: 52790 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700319C RID: 12700
		// (get) Token: 0x0600CE37 RID: 52791 RVA: 0x00290084 File Offset: 0x0028E284
		public virtual string Prefix
		{
			get
			{
				this.MakeSureParsed();
				string text = this.LookupPrefix(this.NamespaceUri);
				if (string.IsNullOrEmpty(text))
				{
					text = NamespaceIdMap.GetNamespacePrefix(this.NamespaceId);
				}
				return text;
			}
		}

		// Token: 0x1700319D RID: 12701
		// (get) Token: 0x0600CE38 RID: 52792 RVA: 0x002900B9 File Offset: 0x0028E2B9
		public IEnumerable<KeyValuePair<string, string>> NamespaceDeclarations
		{
			get
			{
				this.MakeSureParsed();
				if (this.NamespaceDeclField == null)
				{
					return EmptyEnumerable<KeyValuePair<string, string>>.EmptyEnumerableSingleton;
				}
				return this.NamespaceDeclField;
			}
		}

		// Token: 0x1700319E RID: 12702
		// (get) Token: 0x0600CE39 RID: 52793 RVA: 0x002900D5 File Offset: 0x0028E2D5
		public virtual XmlQualifiedName XmlQualifiedName
		{
			get
			{
				return new XmlQualifiedName(this.LocalName, this.NamespaceUri);
			}
		}

		// Token: 0x1700319F RID: 12703
		// (get) Token: 0x0600CE3A RID: 52794 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		// (set) Token: 0x0600CE3B RID: 52795 RVA: 0x0000EE09 File Offset: 0x0000D009
		public virtual string InnerText
		{
			get
			{
				return string.Empty;
			}
			protected set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170031A0 RID: 12704
		// (get) Token: 0x0600CE3C RID: 52796 RVA: 0x002900E8 File Offset: 0x0028E2E8
		// (set) Token: 0x0600CE3D RID: 52797 RVA: 0x00290188 File Offset: 0x0028E388
		public virtual string InnerXml
		{
			get
			{
				if (this.XmlParsed)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
					try
					{
						this.WriteContentTo(xmlDOMTextWriter);
					}
					finally
					{
						xmlDOMTextWriter.Close();
					}
					return stringWriter.ToString();
				}
				string text;
				using (TextReader textReader = new StringReader(this.RawOuterXml))
				{
					using (XmlReader xmlReader = XmlReader.Create(textReader))
					{
						xmlReader.Read();
						text = xmlReader.ReadInnerXml();
					}
				}
				return text;
			}
			set
			{
				throw new InvalidOperationException(ExceptionMessages.InnerXmlCannotBeSet);
			}
		}

		// Token: 0x170031A1 RID: 12705
		// (get) Token: 0x0600CE3E RID: 52798 RVA: 0x00290194 File Offset: 0x0028E394
		// (set) Token: 0x0600CE3F RID: 52799 RVA: 0x002901E8 File Offset: 0x0028E3E8
		public string OuterXml
		{
			get
			{
				if (this.XmlParsed)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlTextWriter xmlTextWriter = new XmlDOMTextWriter(stringWriter);
					try
					{
						this.WriteTo(xmlTextWriter);
					}
					finally
					{
						xmlTextWriter.Close();
					}
					return stringWriter.ToString();
				}
				return this.RawOuterXml;
			}
			internal set
			{
				if (this.XmlParsed)
				{
					this.RemoveAllChildren();
					this.NamespaceDeclField = null;
					this.ExtendedAttributesField = null;
					if (this.FixedAttributesArray != null)
					{
						for (int i = 0; i < this.FixedAttributesArray.Length; i++)
						{
							this.FixedAttributesArray[i] = null;
						}
					}
					this.MCAttributes = null;
				}
				if (!string.IsNullOrEmpty(value))
				{
					this.RawOuterXml = value;
					return;
				}
				this._rawOuterXml = string.Empty;
			}
		}

		// Token: 0x0600CE40 RID: 52800 RVA: 0x00290258 File Offset: 0x0028E458
		public OpenXmlAttribute GetAttribute(string localName, string namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				namespaceUri = string.Empty;
			}
			if (localName == string.Empty)
			{
				throw new ArgumentOutOfRangeException("localName", ExceptionMessages.StringIsEmpty);
			}
			if (this.HasAttributes)
			{
				if (this.Attributes != null && namespaceUri != null)
				{
					for (int i = 0; i < this.Attributes.Length; i++)
					{
						if (this.Attributes[i] != null && this.AttributeTagNames[i] == localName && NamespaceIdMap.GetNamespaceUri(this.AttributeNamespaceIds[i]) == namespaceUri)
						{
							OpenXmlAttribute openXmlAttribute = new OpenXmlAttribute(NamespaceIdMap.GetNamespacePrefix(this.AttributeNamespaceIds[i]), this.AttributeTagNames[i], NamespaceIdMap.GetNamespaceUri(this.AttributeNamespaceIds[i]), this.Attributes[i].ToString());
							return openXmlAttribute;
						}
					}
				}
				foreach (OpenXmlAttribute openXmlAttribute2 in this.ExtendedAttributes)
				{
					if (openXmlAttribute2.LocalName == localName && openXmlAttribute2.NamespaceUri == namespaceUri)
					{
						OpenXmlAttribute openXmlAttribute = new OpenXmlAttribute(openXmlAttribute2.Prefix, openXmlAttribute2.LocalName, openXmlAttribute2.NamespaceUri, openXmlAttribute2.Value);
						return openXmlAttribute;
					}
				}
				if (namespaceUri == AlternateContent.MarkupCompatibilityNamespace)
				{
					return this.GetMCAttribute(localName);
				}
			}
			IL_014E:
			throw new KeyNotFoundException(ExceptionMessages.CannotFindAttribute);
		}

		// Token: 0x0600CE41 RID: 52801 RVA: 0x002903D0 File Offset: 0x0028E5D0
		public IList<OpenXmlAttribute> GetAttributes()
		{
			if (this.HasAttributes)
			{
				List<OpenXmlAttribute> list = new List<OpenXmlAttribute>();
				this.GetAttributes(list);
				return list;
			}
			return new List<OpenXmlAttribute>();
		}

		// Token: 0x0600CE42 RID: 52802 RVA: 0x002903FC File Offset: 0x0028E5FC
		private void GetAttributes(ICollection<OpenXmlAttribute> attributes)
		{
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			if (this.HasAttributes)
			{
				if (this.Attributes != null)
				{
					for (int i = 0; i < this.Attributes.Length; i++)
					{
						if (this.Attributes[i] != null)
						{
							OpenXmlAttribute openXmlAttribute = new OpenXmlAttribute(NamespaceIdMap.GetNamespacePrefix(this.AttributeNamespaceIds[i]), this.AttributeTagNames[i], NamespaceIdMap.GetNamespaceUri(this.AttributeNamespaceIds[i]), this.Attributes[i].ToString());
							attributes.Add(openXmlAttribute);
						}
					}
				}
				foreach (OpenXmlAttribute openXmlAttribute2 in this.ExtendedAttributes)
				{
					OpenXmlAttribute openXmlAttribute = new OpenXmlAttribute(openXmlAttribute2.Prefix, openXmlAttribute2.LocalName, openXmlAttribute2.NamespaceUri, openXmlAttribute2.Value);
					attributes.Add(openXmlAttribute);
				}
				if (this.MCAttributes != null)
				{
					this.AddMCAttributes(attributes);
				}
			}
		}

		// Token: 0x0600CE43 RID: 52803 RVA: 0x002904F8 File Offset: 0x0028E6F8
		public void SetAttribute(OpenXmlAttribute openXmlAttribute)
		{
			if (string.IsNullOrEmpty(openXmlAttribute.LocalName))
			{
				throw new ArgumentOutOfRangeException("openXmlAttribute", ExceptionMessages.LocalNameIsNull);
			}
			if (openXmlAttribute.Prefix == "xmlns")
			{
				throw new InvalidOperationException(ExceptionMessages.CannotSetAttribute);
			}
			this.MakeSureParsed();
			if (!this.TrySetFixedAttribute(openXmlAttribute.NamespaceUri, openXmlAttribute.LocalName, openXmlAttribute.Value))
			{
				if (openXmlAttribute.NamespaceUri == AlternateContent.MarkupCompatibilityNamespace && this.LoadMCAttribute(openXmlAttribute.LocalName, openXmlAttribute.Value))
				{
					return;
				}
				int num = 0;
				if (this.ExtendedAttributesField != null)
				{
					foreach (OpenXmlAttribute openXmlAttribute2 in this.ExtendedAttributesField)
					{
						if (openXmlAttribute2.LocalName == openXmlAttribute.LocalName && openXmlAttribute2.NamespaceUri == openXmlAttribute.NamespaceUri)
						{
							this.ExtendedAttributesField.RemoveAt(num);
							break;
						}
						num++;
					}
				}
				if (this.ExtendedAttributesField == null)
				{
					this.ExtendedAttributesField = new List<OpenXmlAttribute>();
				}
				OpenXmlAttribute openXmlAttribute3 = new OpenXmlAttribute(openXmlAttribute.Prefix, openXmlAttribute.LocalName, openXmlAttribute.NamespaceUri, openXmlAttribute.Value);
				this.ExtendedAttributesField.Add(openXmlAttribute3);
			}
		}

		// Token: 0x0600CE44 RID: 52804 RVA: 0x00290658 File Offset: 0x0028E858
		public void RemoveAttribute(string localName, string namespaceUri)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				namespaceUri = string.Empty;
			}
			if (localName == string.Empty)
			{
				throw new ArgumentOutOfRangeException("localName", ExceptionMessages.StringIsEmpty);
			}
			if (this.HasAttributes)
			{
				bool flag = false;
				if (this.FixedAttributeTotal > 0)
				{
					int num = this.TryFindAttributeIndex(namespaceUri, localName);
					if (num >= 0)
					{
						this.FixedAttributesArray[num] = null;
						flag = true;
					}
				}
				if (!flag)
				{
					int num2 = 0;
					if (this.ExtendedAttributesField != null)
					{
						foreach (OpenXmlAttribute openXmlAttribute in this.ExtendedAttributesField)
						{
							if (openXmlAttribute.LocalName == localName && openXmlAttribute.NamespaceUri == namespaceUri)
							{
								this.ExtendedAttributesField.RemoveAt(num2);
								return;
							}
							num2++;
						}
					}
				}
				if (namespaceUri == AlternateContent.MarkupCompatibilityNamespace)
				{
					this.RemoveMCAttribute(localName);
				}
			}
		}

		// Token: 0x0600CE45 RID: 52805 RVA: 0x0029075C File Offset: 0x0028E95C
		public void SetAttributes(IEnumerable<OpenXmlAttribute> openXmlAttributes)
		{
			if (openXmlAttributes == null)
			{
				throw new ArgumentNullException("openXmlAttributes");
			}
			foreach (OpenXmlAttribute openXmlAttribute in openXmlAttributes)
			{
				this.SetAttribute(openXmlAttribute);
			}
		}

		// Token: 0x0600CE46 RID: 52806 RVA: 0x002907B4 File Offset: 0x0028E9B4
		public void ClearAllAttributes()
		{
			this.MakeSureParsed();
			if (this.FixedAttributesArray != null)
			{
				for (int i = 0; i < this.FixedAttributeTotal; i++)
				{
					this.FixedAttributesArray[i] = null;
				}
			}
			this.ExtendedAttributesField = null;
			this.MCAttributes = null;
		}

		// Token: 0x0600CE47 RID: 52807 RVA: 0x002907F8 File Offset: 0x0028E9F8
		public void AddNamespaceDeclaration(string prefix, string uri)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw new ArgumentNullException("prefix");
			}
			if (string.IsNullOrEmpty(uri))
			{
				throw new ArgumentNullException("uri");
			}
			this.MakeSureParsed();
			if (this.NamespaceDeclField == null)
			{
				this.NamespaceDeclField = new List<KeyValuePair<string, string>>();
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.NamespaceDeclField)
			{
				if (keyValuePair.Key == prefix)
				{
					string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.DuplicatedPrefix, new object[] { prefix });
					throw new InvalidOperationException(text);
				}
			}
			this.NamespaceDeclField.Add(new KeyValuePair<string, string>(prefix, uri));
		}

		// Token: 0x0600CE48 RID: 52808 RVA: 0x002908C8 File Offset: 0x0028EAC8
		public void RemoveNamespaceDeclaration(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw new ArgumentNullException("prefix");
			}
			this.MakeSureParsed();
			if (this.NamespaceDeclField != null)
			{
				for (int i = 0; i < this.NamespaceDeclField.Count; i++)
				{
					if (this.NamespaceDeclField[i].Key == prefix)
					{
						this.NamespaceDeclField.RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x0600CE49 RID: 52809 RVA: 0x00290935 File Offset: 0x0028EB35
		public T GetFirstChild<T>() where T : OpenXmlElement
		{
			return this.ChildElements.First<T>();
		}

		// Token: 0x0600CE4A RID: 52810 RVA: 0x00290944 File Offset: 0x0028EB44
		public OpenXmlElement PreviousSibling()
		{
			OpenXmlCompositeElement openXmlCompositeElement = this.Parent as OpenXmlCompositeElement;
			if (openXmlCompositeElement == null)
			{
				return null;
			}
			OpenXmlElement openXmlElement;
			OpenXmlElement openXmlElement2;
			for (openXmlElement = openXmlCompositeElement.FirstChild; openXmlElement != null; openXmlElement = openXmlElement2)
			{
				openXmlElement2 = openXmlElement.NextSibling();
				if (openXmlElement2 == this)
				{
					return openXmlElement;
				}
			}
			return openXmlElement;
		}

		// Token: 0x0600CE4B RID: 52811 RVA: 0x00290980 File Offset: 0x0028EB80
		public T PreviousSibling<T>() where T : OpenXmlElement
		{
			for (OpenXmlElement openXmlElement = this.PreviousSibling(); openXmlElement != null; openXmlElement = openXmlElement.PreviousSibling())
			{
				if (openXmlElement is T)
				{
					return (T)((object)openXmlElement);
				}
			}
			return default(T);
		}

		// Token: 0x0600CE4C RID: 52812 RVA: 0x002909B8 File Offset: 0x0028EBB8
		public OpenXmlElement NextSibling()
		{
			OpenXmlElement parent = this.Parent;
			if (parent != null && this.next != parent.FirstChild)
			{
				return this.next;
			}
			return null;
		}

		// Token: 0x0600CE4D RID: 52813 RVA: 0x002909E8 File Offset: 0x0028EBE8
		public T NextSibling<T>() where T : OpenXmlElement
		{
			for (OpenXmlElement openXmlElement = this.NextSibling(); openXmlElement != null; openXmlElement = openXmlElement.NextSibling())
			{
				if (openXmlElement is T)
				{
					return (T)((object)openXmlElement);
				}
			}
			return default(T);
		}

		// Token: 0x0600CE4E RID: 52814 RVA: 0x00290A20 File Offset: 0x0028EC20
		public IEnumerable<OpenXmlElement> Ancestors()
		{
			for (OpenXmlElement ancestor = this.Parent; ancestor != null; ancestor = ancestor.Parent)
			{
				yield return ancestor;
			}
			yield break;
		}

		// Token: 0x0600CE4F RID: 52815 RVA: 0x00290A40 File Offset: 0x0028EC40
		public IEnumerable<T> Ancestors<T>() where T : OpenXmlElement
		{
			for (OpenXmlElement ancestor = this.Parent; ancestor != null; ancestor = ancestor.Parent)
			{
				if (ancestor is T)
				{
					yield return (T)((object)ancestor);
				}
			}
			yield break;
		}

		// Token: 0x0600CE50 RID: 52816 RVA: 0x00290A5D File Offset: 0x0028EC5D
		public IEnumerable<T> Elements<T>() where T : OpenXmlElement
		{
			return this.ChildElements.OfType<T>();
		}

		// Token: 0x0600CE51 RID: 52817 RVA: 0x00290A6A File Offset: 0x0028EC6A
		public IEnumerable<OpenXmlElement> Elements()
		{
			return this.ChildElements;
		}

		// Token: 0x0600CE52 RID: 52818 RVA: 0x00290A74 File Offset: 0x0028EC74
		public IEnumerable<T> Descendants<T>() where T : OpenXmlElement
		{
			T elementT = default(T);
			foreach (OpenXmlElement element in this.Descendants())
			{
				elementT = element as T;
				if (elementT != null)
				{
					yield return elementT;
				}
			}
			yield break;
		}

		// Token: 0x0600CE53 RID: 52819 RVA: 0x00290A94 File Offset: 0x0028EC94
		public IEnumerable<OpenXmlElement> Descendants()
		{
			if (this.FirstChild != null)
			{
				OpenXmlElement root = this.FirstChild;
				yield return root;
				Stack<OpenXmlElement> stack = new Stack<OpenXmlElement>();
				stack.Push(root);
				for (;;)
				{
					if (stack.Peek() == root && stack.Peek().FirstChild != null)
					{
						root = stack.Peek().FirstChild;
						stack.Push(root);
						yield return root;
					}
					else if (stack.Peek().NextSibling() != null)
					{
						root = stack.Peek().NextSibling();
						stack.Pop();
						stack.Push(root);
						yield return root;
					}
					else
					{
						stack.Pop();
						if (stack.Count == 0)
						{
							break;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600CE54 RID: 52820 RVA: 0x00290AB4 File Offset: 0x0028ECB4
		public IEnumerable<OpenXmlElement> ElementsBefore()
		{
			if (this.Parent != null)
			{
				for (OpenXmlElement element = this.Parent.FirstChild; element != this; element = element.NextSibling())
				{
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x0600CE55 RID: 52821 RVA: 0x00290AD4 File Offset: 0x0028ECD4
		public IEnumerable<OpenXmlElement> ElementsAfter()
		{
			if (this.Parent != null)
			{
				for (OpenXmlElement element = this.NextSibling(); element != null; element = element.NextSibling())
				{
					yield return element;
				}
			}
			yield break;
		}

		// Token: 0x0600CE56 RID: 52822 RVA: 0x00290AF1 File Offset: 0x0028ECF1
		public virtual OpenXmlElement CloneNode(bool deep)
		{
			throw new NotImplementedException(ExceptionMessages.NonImplemented);
		}

		// Token: 0x0600CE57 RID: 52823 RVA: 0x00290B00 File Offset: 0x0028ED00
		public virtual void WriteTo(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			if (this.XmlParsed)
			{
				string text = this.LookupPrefixLocal(this.NamespaceUri);
				if (string.IsNullOrEmpty(text))
				{
					text = xmlWriter.LookupPrefix(this.NamespaceUri);
				}
				if (string.IsNullOrEmpty(text))
				{
					text = NamespaceIdMap.GetNamespacePrefix(this.NamespaceId);
				}
				xmlWriter.WriteStartElement(text, this.LocalName, this.NamespaceUri);
				this.WriteAttributesTo(xmlWriter);
				this.WriteContentTo(xmlWriter);
				xmlWriter.WriteEndElement();
				return;
			}
			xmlWriter.WriteRaw(this.RawOuterXml);
		}

		// Token: 0x0600CE58 RID: 52824 RVA: 0x00290B90 File Offset: 0x0028ED90
		public void Append(IEnumerable<OpenXmlElement> newChildren)
		{
			if (newChildren == null)
			{
				throw new ArgumentNullException("newChildren");
			}
			foreach (OpenXmlElement openXmlElement in newChildren)
			{
				this.AppendChild<OpenXmlElement>(openXmlElement);
			}
		}

		// Token: 0x0600CE59 RID: 52825 RVA: 0x00290BE8 File Offset: 0x0028EDE8
		public void Append(params OpenXmlElement[] newChildren)
		{
			if (newChildren != null)
			{
				foreach (OpenXmlElement openXmlElement in newChildren)
				{
					this.AppendChild<OpenXmlElement>(openXmlElement);
				}
			}
		}

		// Token: 0x0600CE5A RID: 52826 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T AppendChild<T>(T newChild) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE5B RID: 52827 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T InsertAfter<T>(T newChild, OpenXmlElement refChild) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE5C RID: 52828 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T InsertBefore<T>(T newChild, OpenXmlElement refChild) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE5D RID: 52829 RVA: 0x00290C20 File Offset: 0x0028EE20
		public T InsertAfterSelf<T>(T newElement) where T : OpenXmlElement
		{
			if (newElement == null)
			{
				throw new ArgumentNullException("newElement");
			}
			if (this.Parent == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ParentIsNull);
			}
			return this.Parent.InsertAfter<T>(newElement, this);
		}

		// Token: 0x0600CE5E RID: 52830 RVA: 0x00290C55 File Offset: 0x0028EE55
		public T InsertBeforeSelf<T>(T newElement) where T : OpenXmlElement
		{
			if (newElement == null)
			{
				throw new ArgumentNullException("newElement");
			}
			if (this.Parent == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ParentIsNull);
			}
			return this.Parent.InsertBefore<T>(newElement, this);
		}

		// Token: 0x0600CE5F RID: 52831 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T InsertAt<T>(T newChild, int index) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE60 RID: 52832 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T PrependChild<T>(T newChild) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE61 RID: 52833 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T RemoveChild<T>(T oldChild) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE62 RID: 52834 RVA: 0x00290C14 File Offset: 0x0028EE14
		public virtual T ReplaceChild<T>(OpenXmlElement newChild, T oldChild) where T : OpenXmlElement
		{
			throw new InvalidOperationException(ExceptionMessages.NonCompositeNoChild);
		}

		// Token: 0x0600CE63 RID: 52835
		public abstract void RemoveAllChildren();

		// Token: 0x0600CE64 RID: 52836 RVA: 0x00290C8C File Offset: 0x0028EE8C
		public void RemoveAllChildren<T>() where T : OpenXmlElement
		{
			OpenXmlElement openXmlElement2;
			for (OpenXmlElement openXmlElement = this.FirstChild; openXmlElement != null; openXmlElement = openXmlElement2)
			{
				openXmlElement2 = openXmlElement.NextSibling();
				if (openXmlElement is T)
				{
					this.RemoveChild<OpenXmlElement>(openXmlElement);
				}
			}
		}

		// Token: 0x0600CE65 RID: 52837 RVA: 0x00290CBE File Offset: 0x0028EEBE
		public void Remove()
		{
			if (this.Parent == null)
			{
				throw new InvalidOperationException(ExceptionMessages.ParentIsNull);
			}
			this.Parent.RemoveChild<OpenXmlElement>(this);
		}

		// Token: 0x0600CE66 RID: 52838 RVA: 0x00290CE0 File Offset: 0x0028EEE0
		public bool IsAfter(OpenXmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return OpenXmlElement.GetOrder(this, element) == OpenXmlElement.ElementOrder.After;
		}

		// Token: 0x0600CE67 RID: 52839 RVA: 0x00290CFA File Offset: 0x0028EEFA
		public bool IsBefore(OpenXmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return OpenXmlElement.GetOrder(this, element) == OpenXmlElement.ElementOrder.Before;
		}

		// Token: 0x0600CE68 RID: 52840 RVA: 0x00290D14 File Offset: 0x0028EF14
		private static OpenXmlElement.ElementOrder GetOrder(OpenXmlElement element1, OpenXmlElement element2)
		{
			if (element1.Parent == null && element2.Parent == null)
			{
				return OpenXmlElement.ElementOrder.NotInSameTree;
			}
			if (object.ReferenceEquals(element1.Parent, element2.Parent))
			{
				return OpenXmlElement.GetSiblingOrder(element1, element2);
			}
			Stack<OpenXmlElement> stack = new Stack<OpenXmlElement>();
			Stack<OpenXmlElement> stack2 = new Stack<OpenXmlElement>();
			stack.Push(element1);
			stack2.Push(element2);
			foreach (OpenXmlElement openXmlElement in element1.Ancestors())
			{
				stack.Push(openXmlElement);
			}
			foreach (OpenXmlElement openXmlElement2 in element2.Ancestors())
			{
				stack2.Push(openXmlElement2);
			}
			OpenXmlElement openXmlElement3 = stack.Pop();
			OpenXmlElement openXmlElement4 = stack2.Pop();
			if (openXmlElement3 != openXmlElement4)
			{
				return OpenXmlElement.ElementOrder.NotInSameTree;
			}
			while (stack.Count > 0 && stack2.Count > 0)
			{
				openXmlElement3 = stack.Pop();
				openXmlElement4 = stack2.Pop();
				OpenXmlElement.ElementOrder siblingOrder = OpenXmlElement.GetSiblingOrder(openXmlElement3, openXmlElement4);
				if (siblingOrder != OpenXmlElement.ElementOrder.Same)
				{
					return siblingOrder;
				}
			}
			if (stack.Count == 0)
			{
				return OpenXmlElement.ElementOrder.Before;
			}
			return OpenXmlElement.ElementOrder.After;
		}

		// Token: 0x0600CE69 RID: 52841 RVA: 0x00290E48 File Offset: 0x0028F048
		private static OpenXmlElement.ElementOrder GetSiblingOrder(OpenXmlElement element1, OpenXmlElement element2)
		{
			if (element1 == element2)
			{
				return OpenXmlElement.ElementOrder.Same;
			}
			for (OpenXmlElement openXmlElement = element1.NextSibling(); openXmlElement != null; openXmlElement = openXmlElement.NextSibling())
			{
				if (object.ReferenceEquals(openXmlElement, element2))
				{
					return OpenXmlElement.ElementOrder.Before;
				}
			}
			return OpenXmlElement.ElementOrder.After;
		}

		// Token: 0x0600CE6A RID: 52842 RVA: 0x00290E7C File Offset: 0x0028F07C
		internal virtual void WriteAttributesTo(XmlWriter xmlWriter)
		{
			if (this.NamespaceDeclField != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.NamespaceDeclField)
				{
					xmlWriter.WriteAttributeString("xmlns", keyValuePair.Key, "http://www.w3.org/2000/xmlns/", keyValuePair.Value);
				}
			}
			if (this.XmlParsed && this.HasAttributes)
			{
				int num = 0;
				if (this.Attributes != null)
				{
					foreach (OpenXmlSimpleType openXmlSimpleType in this.Attributes)
					{
						if (openXmlSimpleType != null)
						{
							string namespaceUri = NamespaceIdMap.GetNamespaceUri(this.AttributeNamespaceIds[num]);
							string text = string.Empty;
							if (!string.IsNullOrEmpty(namespaceUri))
							{
								text = xmlWriter.LookupPrefix(namespaceUri);
								if (string.IsNullOrEmpty(text))
								{
									text = NamespaceIdMap.GetNamespacePrefix(this.AttributeNamespaceIds[num]);
								}
							}
							xmlWriter.WriteStartAttribute(text, this.AttributeTagNames[num], namespaceUri);
							xmlWriter.WriteString(openXmlSimpleType.InnerText);
							xmlWriter.WriteEndAttribute();
						}
						num++;
					}
				}
				foreach (OpenXmlAttribute openXmlAttribute in this.ExtendedAttributes)
				{
					xmlWriter.WriteAttributeString(openXmlAttribute.Prefix, openXmlAttribute.LocalName, openXmlAttribute.NamespaceUri, openXmlAttribute.Value);
				}
				this.WriteMCAttribute(xmlWriter);
			}
		}

		// Token: 0x0600CE6B RID: 52843
		internal abstract void WriteContentTo(XmlWriter w);

		// Token: 0x0600CE6C RID: 52844 RVA: 0x00291000 File Offset: 0x0028F200
		internal int TryFindAttributeIndex(string namespaceUri, string tagName)
		{
			byte b = 0;
			if (namespaceUri != null && NamespaceIdMap.TryGetNamespaceId(namespaceUri, out b))
			{
				for (int i = 0; i < this.AttributeTagNames.Length; i++)
				{
					string text = this.AttributeTagNames[i];
					if (text.Equals(tagName) && this.AttributeNamespaceIds[i] == b)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600CE6D RID: 52845 RVA: 0x00291050 File Offset: 0x0028F250
		internal bool TrySetFixedAttribute(string namespaceUri, string localName, string value)
		{
			if (this.FixedAttributeTotal > 0)
			{
				int num = this.TryFindAttributeIndex(namespaceUri, localName);
				if (num >= 0)
				{
					OpenXmlSimpleType openXmlSimpleType = this.AttributeFactory(namespaceUri, localName);
					this.FixedAttributesArray[num] = openXmlSimpleType;
					openXmlSimpleType.InnerText = value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CE6E RID: 52846 RVA: 0x00291090 File Offset: 0x0028F290
		internal virtual void LoadAttributes(XmlReader xmlReader)
		{
			if (xmlReader.HasAttributes)
			{
				while (xmlReader.MoveToNextAttribute())
				{
					if (!this.TrySetFixedAttribute(xmlReader.NamespaceURI, xmlReader.LocalName, xmlReader.Value) && (!(xmlReader.NamespaceURI == AlternateContent.MarkupCompatibilityNamespace) || !this.LoadMCAttribute(xmlReader.LocalName, xmlReader.Value)))
					{
						if (!(xmlReader.NamespaceURI == "http://www.w3.org/2000/xmlns/"))
						{
							if (this.ExtendedAttributesField == null)
							{
								this.ExtendedAttributesField = new List<OpenXmlAttribute>();
							}
							this.ExtendedAttributesField.Add(new OpenXmlAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI, xmlReader.Value));
						}
						else if (!string.IsNullOrEmpty(xmlReader.Prefix))
						{
							if (this.NamespaceDeclField == null)
							{
								this.NamespaceDeclField = new List<KeyValuePair<string, string>>();
							}
							this.NamespaceDeclField.Add(new KeyValuePair<string, string>(xmlReader.LocalName, xmlReader.Value));
						}
					}
				}
				this.RemoveAttributesBasedonMC();
				xmlReader.MoveToElement();
			}
		}

		// Token: 0x0600CE6F RID: 52847 RVA: 0x00291198 File Offset: 0x0028F398
		internal bool PushMcContext(XmlReader xmlReader)
		{
			if (this.OpenXmlElementContext != null && this.OpenXmlElementContext.MCSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess)
			{
				this.OpenXmlElementContext.MCContext.LookupNamespaceDelegate = new MCContext.LookupNamespace(xmlReader.LookupNamespace);
				MarkupCompatibilityAttributes markupCompatibilityAttributes = OpenXmlElement.LoadMCAttribute(xmlReader);
				if (markupCompatibilityAttributes != null)
				{
					this.OpenXmlElementContext.MCContext.PushMCAttributes(markupCompatibilityAttributes);
					if (this.OpenXmlElementContext.ACBlockLevel == 0U)
					{
						OpenXmlElement.CheckMustUnderstandAttr(xmlReader, markupCompatibilityAttributes, this.OpenXmlElementContext.MCSettings);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CE70 RID: 52848 RVA: 0x00291219 File Offset: 0x0028F419
		internal void PopMcContext()
		{
			if (this.OpenXmlElementContext != null && this.OpenXmlElementContext.MCSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess)
			{
				this.OpenXmlElementContext.MCContext.PopMCAttributes();
			}
		}

		// Token: 0x0600CE71 RID: 52849 RVA: 0x00291248 File Offset: 0x0028F448
		internal static void CheckMustUnderstandAttr(XmlReader reader, MarkupCompatibilityAttributes mcAttributes, MarkupCompatibilityProcessSettings mcSettings)
		{
			if (mcAttributes.MustUnderstand != null && !string.IsNullOrEmpty(mcAttributes.MustUnderstand.Value))
			{
				string[] array = mcAttributes.MustUnderstand.Value.Trim().Split(new char[] { ' ' });
				foreach (string text in array)
				{
					string text2 = reader.LookupNamespace(text);
					if (string.IsNullOrEmpty(text2))
					{
						string text3 = string.Format(CultureInfo.CurrentCulture, ExceptionMessages.UnknowMCContent, new object[] { mcAttributes.MustUnderstand.Value });
						throw new InvalidMCContentException(text3);
					}
					if (!NamespaceIdMap.IsInFileFormat(text2, mcSettings.TargetFileFormatVersions))
					{
						string text4 = string.Format(CultureInfo.CurrentCulture, ExceptionMessages.NsNotUnderStand, new object[] { text });
						throw new NamespaceNotUnderstandException(text4);
					}
				}
			}
		}

		// Token: 0x0600CE72 RID: 52850 RVA: 0x00291334 File Offset: 0x0028F534
		internal void CheckMustUnderstandAttr()
		{
			if (this.MCAttributes == null || this.OpenXmlElementContext.MCSettings.ProcessMode == MarkupCompatibilityProcessMode.NoProcess)
			{
				return;
			}
			if (this.MCAttributes.MustUnderstand != null && !string.IsNullOrEmpty(this.MCAttributes.MustUnderstand.Value))
			{
				string[] array = this.MCAttributes.MustUnderstand.Value.Trim().Split(new char[] { ' ' });
				foreach (string text in array)
				{
					string text2 = this.LookupNamespace(text);
					if (string.IsNullOrEmpty(text2))
					{
						string text3 = string.Format(CultureInfo.CurrentCulture, ExceptionMessages.UnknowMCContent, new object[] { this.MCAttributes.MustUnderstand.Value });
						throw new InvalidMCContentException(text3);
					}
					if (!NamespaceIdMap.IsInFileFormat(text2, this.OpenXmlElementContext.MCSettings.TargetFileFormatVersions))
					{
						string text4 = string.Format(CultureInfo.CurrentCulture, ExceptionMessages.NsNotUnderStand, new object[] { text });
						throw new NamespaceNotUnderstandException(text4);
					}
				}
			}
			foreach (OpenXmlElement openXmlElement in this.ChildElements)
			{
				openXmlElement.CheckMustUnderstandAttr();
			}
		}

		// Token: 0x0600CE73 RID: 52851 RVA: 0x002914A0 File Offset: 0x0028F6A0
		internal void Load(XmlReader xmlReader, OpenXmlLoadMode loadMode)
		{
			switch (loadMode)
			{
			case OpenXmlLoadMode.Full:
				this.Populate(xmlReader, loadMode);
				return;
			case OpenXmlLoadMode.Lazy:
				if (this.OpenXmlElementContext != null && xmlReader.Depth < this.OpenXmlElementContext.LazySteps)
				{
					this.Populate(xmlReader, loadMode);
					return;
				}
				this.LazyLoad(xmlReader);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600CE74 RID: 52852 RVA: 0x002914F2 File Offset: 0x0028F6F2
		internal void MakeSureParsed()
		{
			if (this.XmlParsed)
			{
				return;
			}
			this.ParseXml();
			this.RawOuterXml = string.Empty;
		}

		// Token: 0x0600CE75 RID: 52853 RVA: 0x0029150E File Offset: 0x0028F70E
		internal virtual void LazyLoad(XmlReader xmlReader)
		{
			this.RawOuterXml = xmlReader.ReadOuterXml();
		}

		// Token: 0x0600CE76 RID: 52854
		internal abstract void Populate(XmlReader xmlReader, OpenXmlLoadMode loadMode);

		// Token: 0x0600CE77 RID: 52855 RVA: 0x0029151C File Offset: 0x0028F71C
		internal virtual void ParseXml()
		{
			if (string.IsNullOrEmpty(this.RawOuterXml))
			{
				return;
			}
			using (XmlReader xmlReader = this.CreateXmlReader())
			{
				xmlReader.Read();
				if (this.OpenXmlElementContext != null)
				{
					this.Populate(xmlReader, this.OpenXmlElementContext.LoadMode);
				}
				else
				{
					this.Populate(xmlReader, OpenXmlLoadMode.Full);
				}
			}
		}

		// Token: 0x0600CE78 RID: 52856 RVA: 0x00291588 File Offset: 0x0028F788
		internal XmlReader CreateXmlReader()
		{
			TextReader textReader = new StringReader(this.RawOuterXml);
			XmlReader xmlReader;
			if (this.OpenXmlElementContext != null)
			{
				xmlReader = XmlReader.Create(textReader, this.OpenXmlElementContext.XmlReaderSettings);
			}
			else
			{
				xmlReader = XmlReader.Create(textReader, OpenXmlElementContext.CreateDefaultXmlReaderSettings());
			}
			return xmlReader;
		}

		// Token: 0x0600CE79 RID: 52857 RVA: 0x002915CC File Offset: 0x0028F7CC
		internal XmlReader CreateXmlReader(string outerXml)
		{
			TextReader textReader = new StringReader(outerXml);
			XmlReader xmlReader;
			if (this.OpenXmlElementContext != null)
			{
				xmlReader = XmlReader.Create(textReader, this.OpenXmlElementContext.XmlReaderSettings);
			}
			else
			{
				xmlReader = XmlReader.Create(textReader, OpenXmlElementContext.CreateDefaultXmlReaderSettings());
			}
			return xmlReader;
		}

		// Token: 0x0600CE7A RID: 52858 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0600CE7B RID: 52859 RVA: 0x0029160C File Offset: 0x0028F80C
		internal virtual OpenXmlSimpleType AttributeFactory(string namespaceUri, string name)
		{
			OpenXmlSimpleType openXmlSimpleType = null;
			byte b;
			if (namespaceUri != null && NamespaceIdMap.TryGetNamespaceId(namespaceUri, out b))
			{
				openXmlSimpleType = this.AttributeFactory(b, name);
			}
			if (openXmlSimpleType == null)
			{
				openXmlSimpleType = new StringValue();
			}
			return openXmlSimpleType;
		}

		// Token: 0x0600CE7C RID: 52860 RVA: 0x0029163C File Offset: 0x0028F83C
		internal OpenXmlElement ElementFactory(XmlReader xmlReader)
		{
			switch (xmlReader.NodeType)
			{
			case XmlNodeType.Element:
				return this.ElementFactory(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return new OpenXmlMiscNode(xmlReader.NodeType);
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.XmlDeclaration:
				return new OpenXmlMiscNode(xmlReader.NodeType);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600CE7D RID: 52861 RVA: 0x002916D8 File Offset: 0x0028F8D8
		internal virtual OpenXmlElement ElementFactory(string prefix, string name, string namespaceUri)
		{
			OpenXmlElement openXmlElement = null;
			byte b;
			if (!string.IsNullOrEmpty(namespaceUri) && NamespaceIdMap.TryGetNamespaceId(namespaceUri, out b))
			{
				openXmlElement = this.ElementFactory(b, name);
				if (openXmlElement == null && AlternateContent.MarkupCompatibilityNamespaceId == b && AlternateContent.TagName == name)
				{
					openXmlElement = new AlternateContent();
				}
			}
			if (openXmlElement == null)
			{
				openXmlElement = new OpenXmlUnknownElement(prefix, name, namespaceUri);
			}
			return openXmlElement;
		}

		// Token: 0x0600CE7E RID: 52862 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0600CE7F RID: 52863 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual OpenXmlElement AlternateContentElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0600CE80 RID: 52864 RVA: 0x00291730 File Offset: 0x0028F930
		internal virtual T CloneImp<T>(bool deep) where T : OpenXmlElement, new()
		{
			T t = new T();
			t.CopyAttributes(this);
			if (deep)
			{
				t.CopyChilden(this, deep);
			}
			return t;
		}

		// Token: 0x0600CE81 RID: 52865 RVA: 0x00291764 File Offset: 0x0028F964
		internal void CopyAttributes(OpenXmlElement container)
		{
			if (container.HasAttributes)
			{
				if (container.ExtendedAttributesField != null)
				{
					this.ExtendedAttributesField = new List<OpenXmlAttribute>(container.ExtendedAttributesField);
				}
				if (container.Attributes != null)
				{
					for (int i = 0; i < container.Attributes.Length; i++)
					{
						OpenXmlSimpleType openXmlSimpleType = container.Attributes[i];
						if (openXmlSimpleType != null)
						{
							this.FixedAttributesArray[i] = (OpenXmlSimpleType)openXmlSimpleType.Clone();
						}
					}
				}
				if (container.MCAttributes != null)
				{
					this.MCAttributes = OpenXmlElement.CloneMCAttributes(container.MCAttributes);
				}
			}
			if (container.NamespaceDeclField != null)
			{
				this.NamespaceDeclField = new List<KeyValuePair<string, string>>(container.NamespaceDeclField);
			}
		}

		// Token: 0x0600CE82 RID: 52866 RVA: 0x00291800 File Offset: 0x0028FA00
		private static MarkupCompatibilityAttributes CloneMCAttributes(MarkupCompatibilityAttributes source)
		{
			MarkupCompatibilityAttributes markupCompatibilityAttributes = new MarkupCompatibilityAttributes();
			if (source.Ignorable != null)
			{
				markupCompatibilityAttributes.Ignorable = (StringValue)source.Ignorable.Clone();
			}
			if (source.MustUnderstand != null)
			{
				markupCompatibilityAttributes.MustUnderstand = (StringValue)source.MustUnderstand.Clone();
			}
			if (source.PreserveAttributes != null)
			{
				markupCompatibilityAttributes.PreserveAttributes = (StringValue)source.PreserveAttributes.Clone();
			}
			if (source.PreserveElements != null)
			{
				markupCompatibilityAttributes.PreserveElements = (StringValue)source.PreserveElements.Clone();
			}
			if (source.ProcessContent != null)
			{
				markupCompatibilityAttributes.ProcessContent = (StringValue)source.ProcessContent.Clone();
			}
			return markupCompatibilityAttributes;
		}

		// Token: 0x0600CE83 RID: 52867 RVA: 0x002918AC File Offset: 0x0028FAAC
		internal void CopyChilden(OpenXmlElement container, bool deep)
		{
			foreach (OpenXmlElement openXmlElement in container.ChildElements)
			{
				this.Append(new OpenXmlElement[] { openXmlElement.CloneNode(deep) });
			}
		}

		// Token: 0x0600CE84 RID: 52868
		internal abstract bool IsInVersion(FileFormatVersions version);

		// Token: 0x0600CE85 RID: 52869 RVA: 0x0029190C File Offset: 0x0028FB0C
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this._annotations == null)
			{
				this._annotations = ((annotation is object[]) ? new object[] { annotation } : annotation);
				return;
			}
			object[] array = this._annotations as object[];
			if (array == null)
			{
				this._annotations = new object[] { this._annotations, annotation };
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this._annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x0600CE86 RID: 52870 RVA: 0x002919A8 File Offset: 0x0028FBA8
		public T Annotation<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					return this._annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600CE87 RID: 52871 RVA: 0x00291A14 File Offset: 0x0028FC14
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						return this._annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600CE88 RID: 52872 RVA: 0x00291A7C File Offset: 0x0028FC7C
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] annotations = this._annotations as object[];
				if (annotations == null)
				{
					if (this._annotations is T)
					{
						yield return (T)((object)this._annotations);
					}
				}
				else
				{
					foreach (object obj in annotations)
					{
						if (obj == null)
						{
							break;
						}
						if (obj is T)
						{
							yield return (T)((object)obj);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600CE89 RID: 52873 RVA: 0x00291A9C File Offset: 0x0028FC9C
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] annotations = this._annotations as object[];
				if (annotations == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						yield return this._annotations;
					}
				}
				else
				{
					foreach (object obj in annotations)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							yield return obj;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600CE8A RID: 52874 RVA: 0x00291AC0 File Offset: 0x0028FCC0
		public void RemoveAnnotations<T>() where T : class
		{
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (this._annotations is T)
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x0600CE8B RID: 52875 RVA: 0x00291B3C File Offset: 0x0028FD3C
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._annotations != null)
			{
				object[] array = this._annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this._annotations))
					{
						this._annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j != 0)
					{
						while (j < i)
						{
							array[j++] = null;
						}
						return;
					}
					this._annotations = null;
				}
			}
		}

		// Token: 0x0600CE8C RID: 52876 RVA: 0x00291BC5 File Offset: 0x0028FDC5
		public IEnumerator<OpenXmlElement> GetEnumerator()
		{
			return new OpenXmlChildElements(this).GetEnumerator();
		}

		// Token: 0x0600CE8D RID: 52877 RVA: 0x00291BC5 File Offset: 0x0028FDC5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OpenXmlChildElements(this).GetEnumerator();
		}

		// Token: 0x0600CE8E RID: 52878 RVA: 0x00291BD2 File Offset: 0x0028FDD2
		public object Clone()
		{
			return this.CloneNode(true);
		}

		// Token: 0x0600CE8F RID: 52879 RVA: 0x00291BDB File Offset: 0x0028FDDB
		internal bool IsXmlnsUri(string nsUri)
		{
			if (this.OpenXmlElementContext != null)
			{
				return this.OpenXmlElementContext.IsXmlnsUri(nsUri);
			}
			return nsUri == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x0600CE90 RID: 52880 RVA: 0x00291C00 File Offset: 0x0028FE00
		internal static bool IsKnownNamespace(string namespaceUri)
		{
			byte b;
			return NamespaceIdMap.TryGetNamespaceId(namespaceUri, out b);
		}

		// Token: 0x0600CE91 RID: 52881 RVA: 0x00291C15 File Offset: 0x0028FE15
		internal static bool IsKnownElement(OpenXmlElement element)
		{
			return !(element is OpenXmlUnknownElement) && !(element is OpenXmlMiscNode);
		}

		// Token: 0x0600CE92 RID: 52882 RVA: 0x00291C2C File Offset: 0x0028FE2C
		internal static void SplitName(string name, out string prefix, out string localName)
		{
			int num = name.IndexOf(':');
			if (-1 == num || num == 0 || name.Length - 1 == num)
			{
				prefix = string.Empty;
				localName = name;
				return;
			}
			prefix = name.Substring(0, num);
			localName = name.Substring(num + 1);
		}

		// Token: 0x0600CE93 RID: 52883 RVA: 0x00291C74 File Offset: 0x0028FE74
		private bool ValidOuterXml(string outerXml, string namespaceURI, string localName)
		{
			using (XmlReader xmlReader = this.CreateXmlReader(outerXml))
			{
				if (xmlReader.Read() && xmlReader.NodeType == XmlNodeType.Element)
				{
					return string.Equals(namespaceURI, xmlReader.NamespaceURI) && string.Equals(localName, xmlReader.LocalName);
				}
			}
			return false;
		}

		// Token: 0x170031A2 RID: 12706
		// (get) Token: 0x0600CE94 RID: 52884 RVA: 0x00291CDC File Offset: 0x0028FEDC
		// (set) Token: 0x0600CE95 RID: 52885 RVA: 0x00291CEA File Offset: 0x0028FEEA
		public MarkupCompatibilityAttributes MCAttributes
		{
			get
			{
				this.MakeSureParsed();
				return this.McAttributesFiled;
			}
			set
			{
				this.MakeSureParsed();
				this.McAttributesFiled = value;
			}
		}

		// Token: 0x0600CE96 RID: 52886 RVA: 0x00291CFC File Offset: 0x0028FEFC
		private bool LoadMCAttribute(string localName, string value)
		{
			if (this.McAttributesFiled == null)
			{
				this.McAttributesFiled = new MarkupCompatibilityAttributes();
			}
			if (localName.Equals("Ignorable"))
			{
				this.McAttributesFiled.Ignorable = value;
				return true;
			}
			if (localName.Equals("ProcessContent"))
			{
				this.McAttributesFiled.ProcessContent = value;
				return true;
			}
			if (localName.Equals("PreserveElements"))
			{
				this.McAttributesFiled.PreserveElements = value;
				return true;
			}
			if (localName.Equals("PreserveAttributes"))
			{
				this.McAttributesFiled.PreserveAttributes = value;
				return true;
			}
			if (localName.Equals("MustUnderstand"))
			{
				this.McAttributesFiled.MustUnderstand = value;
				return true;
			}
			return false;
		}

		// Token: 0x0600CE97 RID: 52887 RVA: 0x00291DC0 File Offset: 0x0028FFC0
		private static MarkupCompatibilityAttributes LoadMCAttribute(XmlReader xmlReader)
		{
			MarkupCompatibilityAttributes markupCompatibilityAttributes = null;
			if (xmlReader.HasAttributes)
			{
				while (xmlReader.MoveToNextAttribute())
				{
					string localName;
					if (xmlReader.NamespaceURI == AlternateContent.MarkupCompatibilityNamespace && (localName = xmlReader.LocalName) != null)
					{
						if (!(localName == "Ignorable"))
						{
							if (!(localName == "ProcessContent"))
							{
								if (!(localName == "PreserveElements"))
								{
									if (!(localName == "PreserveAttributes"))
									{
										if (localName == "MustUnderstand")
										{
											if (markupCompatibilityAttributes == null)
											{
												markupCompatibilityAttributes = new MarkupCompatibilityAttributes();
											}
											markupCompatibilityAttributes.MustUnderstand = xmlReader.Value;
										}
									}
									else
									{
										if (markupCompatibilityAttributes == null)
										{
											markupCompatibilityAttributes = new MarkupCompatibilityAttributes();
										}
										markupCompatibilityAttributes.PreserveAttributes = xmlReader.Value;
									}
								}
								else
								{
									if (markupCompatibilityAttributes == null)
									{
										markupCompatibilityAttributes = new MarkupCompatibilityAttributes();
									}
									markupCompatibilityAttributes.PreserveElements = xmlReader.Value;
								}
							}
							else
							{
								if (markupCompatibilityAttributes == null)
								{
									markupCompatibilityAttributes = new MarkupCompatibilityAttributes();
								}
								markupCompatibilityAttributes.ProcessContent = xmlReader.Value;
							}
						}
						else
						{
							if (markupCompatibilityAttributes == null)
							{
								markupCompatibilityAttributes = new MarkupCompatibilityAttributes();
							}
							markupCompatibilityAttributes.Ignorable = xmlReader.Value;
						}
					}
				}
				xmlReader.MoveToElement();
			}
			return markupCompatibilityAttributes;
		}

		// Token: 0x0600CE98 RID: 52888 RVA: 0x00291EE4 File Offset: 0x002900E4
		private bool RemoveMCAttribute(string localName)
		{
			if (this.MCAttributes == null)
			{
				return false;
			}
			if (localName.Equals("Ignorable"))
			{
				this.MCAttributes.Ignorable = null;
				return true;
			}
			if (localName.Equals("ProcessContent"))
			{
				this.MCAttributes.ProcessContent = null;
				return true;
			}
			if (localName.Equals("PreserveElements"))
			{
				this.MCAttributes.PreserveElements = null;
				return true;
			}
			if (localName.Equals("PreserveAttributes"))
			{
				this.MCAttributes.PreserveAttributes = null;
				return true;
			}
			if (localName.Equals("MustUnderstand"))
			{
				this.MCAttributes.MustUnderstand = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600CE99 RID: 52889 RVA: 0x00291F84 File Offset: 0x00290184
		private void AddMCAttributes(ICollection<OpenXmlAttribute> attributes)
		{
			string text = this.LookupPrefix(AlternateContent.MarkupCompatibilityNamespace);
			if (string.IsNullOrEmpty(text))
			{
				text = MarkupCompatibilityAttributes.MCPrefix;
			}
			if (this.MCAttributes.Ignorable != null)
			{
				attributes.Add(new OpenXmlAttribute(text, "Ignorable", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.Ignorable));
			}
			if (this.MCAttributes.ProcessContent != null)
			{
				attributes.Add(new OpenXmlAttribute(text, "ProcessContent", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.ProcessContent));
			}
			if (this.MCAttributes.PreserveElements != null)
			{
				attributes.Add(new OpenXmlAttribute(text, "PreserveElements", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.PreserveElements));
			}
			if (this.MCAttributes.PreserveAttributes != null)
			{
				attributes.Add(new OpenXmlAttribute(text, "PreserveAttributes", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.PreserveAttributes));
			}
			if (this.MCAttributes.MustUnderstand != null)
			{
				attributes.Add(new OpenXmlAttribute(text, "MustUnderstand", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.MustUnderstand));
			}
		}

		// Token: 0x0600CE9A RID: 52890 RVA: 0x002920AC File Offset: 0x002902AC
		private OpenXmlAttribute GetMCAttribute(string localName)
		{
			if (this.MCAttributes == null)
			{
				throw new KeyNotFoundException(ExceptionMessages.CannotFindAttribute);
			}
			string text = this.LookupPrefix(AlternateContent.MarkupCompatibilityNamespace);
			if (string.IsNullOrEmpty(text))
			{
				text = MarkupCompatibilityAttributes.MCPrefix;
			}
			if (localName.Equals("Ignorable"))
			{
				return new OpenXmlAttribute(text, localName, AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.Ignorable);
			}
			if (localName.Equals("ProcessContent"))
			{
				return new OpenXmlAttribute(text, localName, AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.ProcessContent);
			}
			if (localName.Equals("PreserveElements"))
			{
				return new OpenXmlAttribute(text, localName, AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.PreserveElements);
			}
			if (localName.Equals("PreserveAttributes"))
			{
				return new OpenXmlAttribute(text, localName, AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.PreserveAttributes);
			}
			if (localName.Equals("MustUnderstand"))
			{
				return new OpenXmlAttribute(text, localName, AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.MustUnderstand);
			}
			throw new KeyNotFoundException(ExceptionMessages.CannotFindAttribute);
		}

		// Token: 0x0600CE9B RID: 52891 RVA: 0x002921C4 File Offset: 0x002903C4
		private void WriteMCAttribute(XmlWriter xmlWriter)
		{
			if (this.MCAttributes == null)
			{
				return;
			}
			string text = xmlWriter.LookupPrefix(AlternateContent.MarkupCompatibilityNamespace);
			if (string.IsNullOrEmpty(text))
			{
				text = MarkupCompatibilityAttributes.MCPrefix;
			}
			if (this.MCAttributes.Ignorable != null)
			{
				xmlWriter.WriteAttributeString(text, "Ignorable", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.Ignorable);
			}
			if (this.MCAttributes.ProcessContent != null)
			{
				xmlWriter.WriteAttributeString(text, "ProcessContent", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.ProcessContent);
			}
			if (this.MCAttributes.PreserveElements != null)
			{
				xmlWriter.WriteAttributeString(text, "PreserveElements", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.PreserveElements);
			}
			if (this.MCAttributes.PreserveAttributes != null)
			{
				xmlWriter.WriteAttributeString(text, "PreserveAttributes", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.PreserveAttributes);
			}
			if (this.MCAttributes.MustUnderstand != null)
			{
				xmlWriter.WriteAttributeString(text, "MustUnderstand", AlternateContent.MarkupCompatibilityNamespace, this.MCAttributes.MustUnderstand);
			}
		}

		// Token: 0x0600CE9C RID: 52892 RVA: 0x002922DC File Offset: 0x002904DC
		private int MCAttributeCount()
		{
			if (this.MCAttributes == null)
			{
				return 0;
			}
			int num = 0;
			if (this.MCAttributes.Ignorable != null)
			{
				num++;
			}
			if (this.MCAttributes.ProcessContent != null)
			{
				num++;
			}
			if (this.MCAttributes.PreserveElements != null)
			{
				num++;
			}
			if (this.MCAttributes.PreserveAttributes != null)
			{
				num++;
			}
			if (this.MCAttributes.MustUnderstand != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600CE9D RID: 52893 RVA: 0x0029234C File Offset: 0x0029054C
		internal void RemoveAttributesBasedonMC()
		{
			if (this.OpenXmlElementContext == null || this.OpenXmlElementContext.MCSettings.ProcessMode == MarkupCompatibilityProcessMode.NoProcess)
			{
				return;
			}
			if (!this.OpenXmlElementContext.MCContext.HasIgnorable())
			{
				return;
			}
			if (this.FixedAttributesArray != null)
			{
				int num = 0;
				foreach (OpenXmlSimpleType openXmlSimpleType in this.FixedAttributesArray)
				{
					if (openXmlSimpleType != null)
					{
						string namespaceUri = NamespaceIdMap.GetNamespaceUri(this.AttributeNamespaceIds[num]);
						AttributeAction attributeAction = this.OpenXmlElementContext.MCContext.GetAttributeAction(namespaceUri, this.AttributeTagNames[num], this.OpenXmlElementContext.MCSettings.TargetFileFormatVersions);
						if (attributeAction == AttributeAction.Ignore)
						{
							this.FixedAttributesArray[num] = null;
						}
					}
					num++;
				}
			}
			if (this.ExtendedAttributesField != null)
			{
				List<OpenXmlAttribute> list = new List<OpenXmlAttribute>();
				foreach (OpenXmlAttribute openXmlAttribute in this.ExtendedAttributesField)
				{
					AttributeAction attributeAction2 = this.OpenXmlElementContext.MCContext.GetAttributeAction(openXmlAttribute.NamespaceUri, openXmlAttribute.LocalName, this.OpenXmlElementContext.MCSettings.TargetFileFormatVersions);
					if (attributeAction2 == AttributeAction.Ignore)
					{
						list.Add(openXmlAttribute);
					}
				}
				foreach (OpenXmlAttribute openXmlAttribute2 in list)
				{
					this.ExtendedAttributesField.Remove(openXmlAttribute2);
				}
			}
		}

		// Token: 0x0600CE9E RID: 52894 RVA: 0x002924D8 File Offset: 0x002906D8
		internal string LookupNamespaceLocal(string prefix)
		{
			if (this.NamespaceDeclField != null)
			{
				for (int i = 0; i < this.NamespaceDeclField.Count; i++)
				{
					if (this.NamespaceDeclField[i].Key == prefix)
					{
						return this.NamespaceDeclField[i].Value;
					}
				}
			}
			return null;
		}

		// Token: 0x0600CE9F RID: 52895 RVA: 0x00292538 File Offset: 0x00290738
		internal string LookupPrefixLocal(string uri)
		{
			if (this.NamespaceDeclField != null)
			{
				for (int i = 0; i < this.NamespaceDeclField.Count; i++)
				{
					if (this.NamespaceDeclField[i].Value == uri)
					{
						return this.NamespaceDeclField[i].Key;
					}
				}
			}
			return null;
		}

		// Token: 0x0600CEA0 RID: 52896 RVA: 0x00292598 File Offset: 0x00290798
		public string LookupNamespace(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			for (OpenXmlElement openXmlElement = this; openXmlElement != null; openXmlElement = openXmlElement.Parent)
			{
				string text = openXmlElement.LookupNamespaceLocal(prefix);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600CEA1 RID: 52897 RVA: 0x002925D8 File Offset: 0x002907D8
		public string LookupPrefix(string namespaceUri)
		{
			if (string.IsNullOrEmpty(namespaceUri))
			{
				throw new ArgumentNullException("namespaceUri");
			}
			for (OpenXmlElement openXmlElement = this; openXmlElement != null; openXmlElement = openXmlElement.Parent)
			{
				string text = openXmlElement.LookupPrefixLocal(namespaceUri);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600CEA2 RID: 52898 RVA: 0x0029261C File Offset: 0x0029081C
		internal OpenXmlElement GetNextNonMiscElementSibling()
		{
			OpenXmlElement openXmlElement = this.NextSibling();
			while (openXmlElement != null && openXmlElement is OpenXmlMiscNode)
			{
				openXmlElement = openXmlElement.NextSibling();
			}
			return openXmlElement;
		}

		// Token: 0x0600CEA3 RID: 52899 RVA: 0x00292648 File Offset: 0x00290848
		internal OpenXmlElement GetFirstNonMiscElementChild()
		{
			OpenXmlElement firstChild = this.FirstChild;
			if (firstChild is OpenXmlMiscNode)
			{
				return firstChild.GetNextNonMiscElementSibling();
			}
			return firstChild;
		}

		// Token: 0x0600CEA4 RID: 52900 RVA: 0x0029266C File Offset: 0x0029086C
		internal OpenXmlPartRootElement GetPartRootElement()
		{
			OpenXmlElement openXmlElement = this;
			while (openXmlElement.Parent != null)
			{
				openXmlElement = openXmlElement.Parent;
			}
			return openXmlElement as OpenXmlPartRootElement;
		}

		// Token: 0x0400682C RID: 26668
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private OpenXmlElement _parent;

		// Token: 0x0400682D RID: 26669
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private OpenXmlElement _next;

		// Token: 0x0400682E RID: 26670
		private object _annotations;

		// Token: 0x0400682F RID: 26671
		private string _rawOuterXml = string.Empty;

		// Token: 0x04006830 RID: 26672
		private OpenXmlSimpleType[] _fixedAttributes;

		// Token: 0x020020DA RID: 8410
		private enum ElementOrder
		{
			// Token: 0x04006833 RID: 26675
			Same,
			// Token: 0x04006834 RID: 26676
			Before,
			// Token: 0x04006835 RID: 26677
			After,
			// Token: 0x04006836 RID: 26678
			NotInSameTree
		}
	}
}
