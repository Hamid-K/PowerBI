using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C06 RID: 11270
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class XmlProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007F74 RID: 32628
		// (get) Token: 0x06017B6E RID: 97134 RVA: 0x0033A3EB File Offset: 0x003385EB
		public override string LocalName
		{
			get
			{
				return "xmlPr";
			}
		}

		// Token: 0x17007F75 RID: 32629
		// (get) Token: 0x06017B6F RID: 97135 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F76 RID: 32630
		// (get) Token: 0x06017B70 RID: 97136 RVA: 0x0033A3F2 File Offset: 0x003385F2
		internal override int ElementTypeId
		{
			get
			{
				return 11249;
			}
		}

		// Token: 0x06017B71 RID: 97137 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F77 RID: 32631
		// (get) Token: 0x06017B72 RID: 97138 RVA: 0x0033A3F9 File Offset: 0x003385F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return XmlProperties.attributeTagNames;
			}
		}

		// Token: 0x17007F78 RID: 32632
		// (get) Token: 0x06017B73 RID: 97139 RVA: 0x0033A400 File Offset: 0x00338600
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return XmlProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F79 RID: 32633
		// (get) Token: 0x06017B74 RID: 97140 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B75 RID: 97141 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "mapId")]
		public UInt32Value MapId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007F7A RID: 32634
		// (get) Token: 0x06017B76 RID: 97142 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017B77 RID: 97143 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "xpath")]
		public StringValue XPath
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007F7B RID: 32635
		// (get) Token: 0x06017B78 RID: 97144 RVA: 0x0033A407 File Offset: 0x00338607
		// (set) Token: 0x06017B79 RID: 97145 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "xmlDataType")]
		public EnumValue<XmlDataValues> XmlDataType
		{
			get
			{
				return (EnumValue<XmlDataValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017B7A RID: 97146 RVA: 0x00293ECF File Offset: 0x002920CF
		public XmlProperties()
		{
		}

		// Token: 0x06017B7B RID: 97147 RVA: 0x00293ED7 File Offset: 0x002920D7
		public XmlProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B7C RID: 97148 RVA: 0x00293EE0 File Offset: 0x002920E0
		public XmlProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B7D RID: 97149 RVA: 0x00293EE9 File Offset: 0x002920E9
		public XmlProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017B7E RID: 97150 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007F7C RID: 32636
		// (get) Token: 0x06017B7F RID: 97151 RVA: 0x0033A416 File Offset: 0x00338616
		internal override string[] ElementTagNames
		{
			get
			{
				return XmlProperties.eleTagNames;
			}
		}

		// Token: 0x17007F7D RID: 32637
		// (get) Token: 0x06017B80 RID: 97152 RVA: 0x0033A41D File Offset: 0x0033861D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return XmlProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007F7E RID: 32638
		// (get) Token: 0x06017B81 RID: 97153 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007F7F RID: 32639
		// (get) Token: 0x06017B82 RID: 97154 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017B83 RID: 97155 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06017B84 RID: 97156 RVA: 0x0033A424 File Offset: 0x00338624
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "mapId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "xpath" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "xmlDataType" == name)
			{
				return new EnumValue<XmlDataValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B85 RID: 97157 RVA: 0x0033A47B File Offset: 0x0033867B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XmlProperties>(deep);
		}

		// Token: 0x06017B86 RID: 97158 RVA: 0x0033A484 File Offset: 0x00338684
		// Note: this type is marked as 'beforefieldinit'.
		static XmlProperties()
		{
			byte[] array = new byte[3];
			XmlProperties.attributeNamespaceIds = array;
			XmlProperties.eleTagNames = new string[] { "extLst" };
			XmlProperties.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009D49 RID: 40265
		private const string tagName = "xmlPr";

		// Token: 0x04009D4A RID: 40266
		private const byte tagNsId = 22;

		// Token: 0x04009D4B RID: 40267
		internal const int ElementTypeIdConst = 11249;

		// Token: 0x04009D4C RID: 40268
		private static string[] attributeTagNames = new string[] { "mapId", "xpath", "xmlDataType" };

		// Token: 0x04009D4D RID: 40269
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D4E RID: 40270
		private static readonly string[] eleTagNames;

		// Token: 0x04009D4F RID: 40271
		private static readonly byte[] eleNamespaceIds;
	}
}
