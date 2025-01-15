using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C05 RID: 11269
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(XmlProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class XmlCellProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007F68 RID: 32616
		// (get) Token: 0x06017B55 RID: 97109 RVA: 0x0033A2CF File Offset: 0x003384CF
		public override string LocalName
		{
			get
			{
				return "xmlCellPr";
			}
		}

		// Token: 0x17007F69 RID: 32617
		// (get) Token: 0x06017B56 RID: 97110 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F6A RID: 32618
		// (get) Token: 0x06017B57 RID: 97111 RVA: 0x0033A2D6 File Offset: 0x003384D6
		internal override int ElementTypeId
		{
			get
			{
				return 11248;
			}
		}

		// Token: 0x06017B58 RID: 97112 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F6B RID: 32619
		// (get) Token: 0x06017B59 RID: 97113 RVA: 0x0033A2DD File Offset: 0x003384DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return XmlCellProperties.attributeTagNames;
			}
		}

		// Token: 0x17007F6C RID: 32620
		// (get) Token: 0x06017B5A RID: 97114 RVA: 0x0033A2E4 File Offset: 0x003384E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return XmlCellProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F6D RID: 32621
		// (get) Token: 0x06017B5B RID: 97115 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B5C RID: 97116 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x17007F6E RID: 32622
		// (get) Token: 0x06017B5D RID: 97117 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017B5E RID: 97118 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x06017B5F RID: 97119 RVA: 0x00293ECF File Offset: 0x002920CF
		public XmlCellProperties()
		{
		}

		// Token: 0x06017B60 RID: 97120 RVA: 0x00293ED7 File Offset: 0x002920D7
		public XmlCellProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B61 RID: 97121 RVA: 0x00293EE0 File Offset: 0x002920E0
		public XmlCellProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B62 RID: 97122 RVA: 0x00293EE9 File Offset: 0x002920E9
		public XmlCellProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017B63 RID: 97123 RVA: 0x0033A2EB File Offset: 0x003384EB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "xmlPr" == name)
			{
				return new XmlProperties();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007F6F RID: 32623
		// (get) Token: 0x06017B64 RID: 97124 RVA: 0x0033A31E File Offset: 0x0033851E
		internal override string[] ElementTagNames
		{
			get
			{
				return XmlCellProperties.eleTagNames;
			}
		}

		// Token: 0x17007F70 RID: 32624
		// (get) Token: 0x06017B65 RID: 97125 RVA: 0x0033A325 File Offset: 0x00338525
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return XmlCellProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007F71 RID: 32625
		// (get) Token: 0x06017B66 RID: 97126 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007F72 RID: 32626
		// (get) Token: 0x06017B67 RID: 97127 RVA: 0x0033A32C File Offset: 0x0033852C
		// (set) Token: 0x06017B68 RID: 97128 RVA: 0x0033A335 File Offset: 0x00338535
		public XmlProperties XmlProperties
		{
			get
			{
				return base.GetElement<XmlProperties>(0);
			}
			set
			{
				base.SetElement<XmlProperties>(0, value);
			}
		}

		// Token: 0x17007F73 RID: 32627
		// (get) Token: 0x06017B69 RID: 97129 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06017B6A RID: 97130 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06017B6B RID: 97131 RVA: 0x0033A33F File Offset: 0x0033853F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B6C RID: 97132 RVA: 0x0033A375 File Offset: 0x00338575
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XmlCellProperties>(deep);
		}

		// Token: 0x06017B6D RID: 97133 RVA: 0x0033A380 File Offset: 0x00338580
		// Note: this type is marked as 'beforefieldinit'.
		static XmlCellProperties()
		{
			byte[] array = new byte[2];
			XmlCellProperties.attributeNamespaceIds = array;
			XmlCellProperties.eleTagNames = new string[] { "xmlPr", "extLst" };
			XmlCellProperties.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009D42 RID: 40258
		private const string tagName = "xmlCellPr";

		// Token: 0x04009D43 RID: 40259
		private const byte tagNsId = 22;

		// Token: 0x04009D44 RID: 40260
		internal const int ElementTypeIdConst = 11248;

		// Token: 0x04009D45 RID: 40261
		private static string[] attributeTagNames = new string[] { "id", "uniqueName" };

		// Token: 0x04009D46 RID: 40262
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D47 RID: 40263
		private static readonly string[] eleTagNames;

		// Token: 0x04009D48 RID: 40264
		private static readonly byte[] eleNamespaceIds;
	}
}
