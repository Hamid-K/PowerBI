using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C2F RID: 11311
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class XmlColumnProperties : OpenXmlCompositeElement
	{
		// Token: 0x170080DF RID: 32991
		// (get) Token: 0x06017E8E RID: 97934 RVA: 0x0033C798 File Offset: 0x0033A998
		public override string LocalName
		{
			get
			{
				return "xmlColumnPr";
			}
		}

		// Token: 0x170080E0 RID: 32992
		// (get) Token: 0x06017E8F RID: 97935 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080E1 RID: 32993
		// (get) Token: 0x06017E90 RID: 97936 RVA: 0x0033C79F File Offset: 0x0033A99F
		internal override int ElementTypeId
		{
			get
			{
				return 11292;
			}
		}

		// Token: 0x06017E91 RID: 97937 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080E2 RID: 32994
		// (get) Token: 0x06017E92 RID: 97938 RVA: 0x0033C7A6 File Offset: 0x0033A9A6
		internal override string[] AttributeTagNames
		{
			get
			{
				return XmlColumnProperties.attributeTagNames;
			}
		}

		// Token: 0x170080E3 RID: 32995
		// (get) Token: 0x06017E93 RID: 97939 RVA: 0x0033C7AD File Offset: 0x0033A9AD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return XmlColumnProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170080E4 RID: 32996
		// (get) Token: 0x06017E94 RID: 97940 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017E95 RID: 97941 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170080E5 RID: 32997
		// (get) Token: 0x06017E96 RID: 97942 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017E97 RID: 97943 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170080E6 RID: 32998
		// (get) Token: 0x06017E98 RID: 97944 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017E99 RID: 97945 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "denormalized")]
		public BooleanValue Denormalized
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170080E7 RID: 32999
		// (get) Token: 0x06017E9A RID: 97946 RVA: 0x0033C7B4 File Offset: 0x0033A9B4
		// (set) Token: 0x06017E9B RID: 97947 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "xmlDataType")]
		public EnumValue<XmlDataValues> XmlDataType
		{
			get
			{
				return (EnumValue<XmlDataValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06017E9C RID: 97948 RVA: 0x00293ECF File Offset: 0x002920CF
		public XmlColumnProperties()
		{
		}

		// Token: 0x06017E9D RID: 97949 RVA: 0x00293ED7 File Offset: 0x002920D7
		public XmlColumnProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E9E RID: 97950 RVA: 0x00293EE0 File Offset: 0x002920E0
		public XmlColumnProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E9F RID: 97951 RVA: 0x00293EE9 File Offset: 0x002920E9
		public XmlColumnProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017EA0 RID: 97952 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170080E8 RID: 33000
		// (get) Token: 0x06017EA1 RID: 97953 RVA: 0x0033C7C3 File Offset: 0x0033A9C3
		internal override string[] ElementTagNames
		{
			get
			{
				return XmlColumnProperties.eleTagNames;
			}
		}

		// Token: 0x170080E9 RID: 33001
		// (get) Token: 0x06017EA2 RID: 97954 RVA: 0x0033C7CA File Offset: 0x0033A9CA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return XmlColumnProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170080EA RID: 33002
		// (get) Token: 0x06017EA3 RID: 97955 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170080EB RID: 33003
		// (get) Token: 0x06017EA4 RID: 97956 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017EA5 RID: 97957 RVA: 0x00332911 File Offset: 0x00330B11
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

		// Token: 0x06017EA6 RID: 97958 RVA: 0x0033C7D4 File Offset: 0x0033A9D4
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
			if (namespaceId == 0 && "denormalized" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xmlDataType" == name)
			{
				return new EnumValue<XmlDataValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017EA7 RID: 97959 RVA: 0x0033C841 File Offset: 0x0033AA41
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XmlColumnProperties>(deep);
		}

		// Token: 0x06017EA8 RID: 97960 RVA: 0x0033C84C File Offset: 0x0033AA4C
		// Note: this type is marked as 'beforefieldinit'.
		static XmlColumnProperties()
		{
			byte[] array = new byte[4];
			XmlColumnProperties.attributeNamespaceIds = array;
			XmlColumnProperties.eleTagNames = new string[] { "extLst" };
			XmlColumnProperties.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009E19 RID: 40473
		private const string tagName = "xmlColumnPr";

		// Token: 0x04009E1A RID: 40474
		private const byte tagNsId = 22;

		// Token: 0x04009E1B RID: 40475
		internal const int ElementTypeIdConst = 11292;

		// Token: 0x04009E1C RID: 40476
		private static string[] attributeTagNames = new string[] { "mapId", "xpath", "denormalized", "xmlDataType" };

		// Token: 0x04009E1D RID: 40477
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E1E RID: 40478
		private static readonly string[] eleTagNames;

		// Token: 0x04009E1F RID: 40479
		private static readonly byte[] eleNamespaceIds;
	}
}
