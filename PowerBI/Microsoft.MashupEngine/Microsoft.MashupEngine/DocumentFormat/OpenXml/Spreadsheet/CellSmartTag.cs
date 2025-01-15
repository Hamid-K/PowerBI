using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BEF RID: 11247
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CellSmartTagProperties))]
	internal class CellSmartTag : OpenXmlCompositeElement
	{
		// Token: 0x17007E72 RID: 32370
		// (get) Token: 0x06017937 RID: 96567 RVA: 0x003388D5 File Offset: 0x00336AD5
		public override string LocalName
		{
			get
			{
				return "cellSmartTag";
			}
		}

		// Token: 0x17007E73 RID: 32371
		// (get) Token: 0x06017938 RID: 96568 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E74 RID: 32372
		// (get) Token: 0x06017939 RID: 96569 RVA: 0x003388DC File Offset: 0x00336ADC
		internal override int ElementTypeId
		{
			get
			{
				return 11219;
			}
		}

		// Token: 0x0601793A RID: 96570 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E75 RID: 32373
		// (get) Token: 0x0601793B RID: 96571 RVA: 0x003388E3 File Offset: 0x00336AE3
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellSmartTag.attributeTagNames;
			}
		}

		// Token: 0x17007E76 RID: 32374
		// (get) Token: 0x0601793C RID: 96572 RVA: 0x003388EA File Offset: 0x00336AEA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellSmartTag.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E77 RID: 32375
		// (get) Token: 0x0601793D RID: 96573 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601793E RID: 96574 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public UInt32Value Type
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

		// Token: 0x17007E78 RID: 32376
		// (get) Token: 0x0601793F RID: 96575 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017940 RID: 96576 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "deleted")]
		public BooleanValue Deleted
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007E79 RID: 32377
		// (get) Token: 0x06017941 RID: 96577 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017942 RID: 96578 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "xmlBased")]
		public BooleanValue XmlBased
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

		// Token: 0x06017943 RID: 96579 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellSmartTag()
		{
		}

		// Token: 0x06017944 RID: 96580 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellSmartTag(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017945 RID: 96581 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellSmartTag(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017946 RID: 96582 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellSmartTag(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017947 RID: 96583 RVA: 0x003388F1 File Offset: 0x00336AF1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cellSmartTagPr" == name)
			{
				return new CellSmartTagProperties();
			}
			return null;
		}

		// Token: 0x06017948 RID: 96584 RVA: 0x0033890C File Offset: 0x00336B0C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "deleted" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xmlBased" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017949 RID: 96585 RVA: 0x00338963 File Offset: 0x00336B63
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellSmartTag>(deep);
		}

		// Token: 0x0601794A RID: 96586 RVA: 0x0033896C File Offset: 0x00336B6C
		// Note: this type is marked as 'beforefieldinit'.
		static CellSmartTag()
		{
			byte[] array = new byte[3];
			CellSmartTag.attributeNamespaceIds = array;
		}

		// Token: 0x04009CD3 RID: 40147
		private const string tagName = "cellSmartTag";

		// Token: 0x04009CD4 RID: 40148
		private const byte tagNsId = 22;

		// Token: 0x04009CD5 RID: 40149
		internal const int ElementTypeIdConst = 11219;

		// Token: 0x04009CD6 RID: 40150
		private static string[] attributeTagNames = new string[] { "type", "deleted", "xmlBased" };

		// Token: 0x04009CD7 RID: 40151
		private static byte[] attributeNamespaceIds;
	}
}
