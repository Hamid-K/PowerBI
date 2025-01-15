using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C28 RID: 11304
	[ChildElementInfo(typeof(OleItems))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OleLink : OpenXmlCompositeElement
	{
		// Token: 0x170080A9 RID: 32937
		// (get) Token: 0x06017E17 RID: 97815 RVA: 0x0033C21A File Offset: 0x0033A41A
		public override string LocalName
		{
			get
			{
				return "oleLink";
			}
		}

		// Token: 0x170080AA RID: 32938
		// (get) Token: 0x06017E18 RID: 97816 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080AB RID: 32939
		// (get) Token: 0x06017E19 RID: 97817 RVA: 0x0033C221 File Offset: 0x0033A421
		internal override int ElementTypeId
		{
			get
			{
				return 11285;
			}
		}

		// Token: 0x06017E1A RID: 97818 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080AC RID: 32940
		// (get) Token: 0x06017E1B RID: 97819 RVA: 0x0033C228 File Offset: 0x0033A428
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleLink.attributeTagNames;
			}
		}

		// Token: 0x170080AD RID: 32941
		// (get) Token: 0x06017E1C RID: 97820 RVA: 0x0033C22F File Offset: 0x0033A42F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleLink.attributeNamespaceIds;
			}
		}

		// Token: 0x170080AE RID: 32942
		// (get) Token: 0x06017E1D RID: 97821 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017E1E RID: 97822 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170080AF RID: 32943
		// (get) Token: 0x06017E1F RID: 97823 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017E20 RID: 97824 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "progId")]
		public StringValue ProgId
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

		// Token: 0x06017E21 RID: 97825 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleLink()
		{
		}

		// Token: 0x06017E22 RID: 97826 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleLink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E23 RID: 97827 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleLink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017E24 RID: 97828 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleLink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017E25 RID: 97829 RVA: 0x0033C236 File Offset: 0x0033A436
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "oleItems" == name)
			{
				return new OleItems();
			}
			return null;
		}

		// Token: 0x170080B0 RID: 32944
		// (get) Token: 0x06017E26 RID: 97830 RVA: 0x0033C251 File Offset: 0x0033A451
		internal override string[] ElementTagNames
		{
			get
			{
				return OleLink.eleTagNames;
			}
		}

		// Token: 0x170080B1 RID: 32945
		// (get) Token: 0x06017E27 RID: 97831 RVA: 0x0033C258 File Offset: 0x0033A458
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OleLink.eleNamespaceIds;
			}
		}

		// Token: 0x170080B2 RID: 32946
		// (get) Token: 0x06017E28 RID: 97832 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170080B3 RID: 32947
		// (get) Token: 0x06017E29 RID: 97833 RVA: 0x0033C25F File Offset: 0x0033A45F
		// (set) Token: 0x06017E2A RID: 97834 RVA: 0x0033C268 File Offset: 0x0033A468
		public OleItems OleItems
		{
			get
			{
				return base.GetElement<OleItems>(0);
			}
			set
			{
				base.SetElement<OleItems>(0, value);
			}
		}

		// Token: 0x06017E2B RID: 97835 RVA: 0x0033C272 File Offset: 0x0033A472
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "progId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017E2C RID: 97836 RVA: 0x0033C2AA File Offset: 0x0033A4AA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleLink>(deep);
		}

		// Token: 0x06017E2D RID: 97837 RVA: 0x0033C2B4 File Offset: 0x0033A4B4
		// Note: this type is marked as 'beforefieldinit'.
		static OleLink()
		{
			byte[] array = new byte[2];
			array[0] = 19;
			OleLink.attributeNamespaceIds = array;
			OleLink.eleTagNames = new string[] { "oleItems" };
			OleLink.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009DFB RID: 40443
		private const string tagName = "oleLink";

		// Token: 0x04009DFC RID: 40444
		private const byte tagNsId = 22;

		// Token: 0x04009DFD RID: 40445
		internal const int ElementTypeIdConst = 11285;

		// Token: 0x04009DFE RID: 40446
		private static string[] attributeTagNames = new string[] { "id", "progId" };

		// Token: 0x04009DFF RID: 40447
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E00 RID: 40448
		private static readonly string[] eleTagNames;

		// Token: 0x04009E01 RID: 40449
		private static readonly byte[] eleNamespaceIds;
	}
}
