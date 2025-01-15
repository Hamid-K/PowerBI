using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C72 RID: 11378
	[ChildElementInfo(typeof(Font))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Fonts : OpenXmlCompositeElement
	{
		// Token: 0x170082D4 RID: 33492
		// (get) Token: 0x0601831D RID: 99101 RVA: 0x002AD888 File Offset: 0x002ABA88
		public override string LocalName
		{
			get
			{
				return "fonts";
			}
		}

		// Token: 0x170082D5 RID: 33493
		// (get) Token: 0x0601831E RID: 99102 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082D6 RID: 33494
		// (get) Token: 0x0601831F RID: 99103 RVA: 0x0033F40F File Offset: 0x0033D60F
		internal override int ElementTypeId
		{
			get
			{
				return 11358;
			}
		}

		// Token: 0x06018320 RID: 99104 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082D7 RID: 33495
		// (get) Token: 0x06018321 RID: 99105 RVA: 0x0033F416 File Offset: 0x0033D616
		internal override string[] AttributeTagNames
		{
			get
			{
				return Fonts.attributeTagNames;
			}
		}

		// Token: 0x170082D8 RID: 33496
		// (get) Token: 0x06018322 RID: 99106 RVA: 0x0033F41D File Offset: 0x0033D61D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Fonts.attributeNamespaceIds;
			}
		}

		// Token: 0x170082D9 RID: 33497
		// (get) Token: 0x06018323 RID: 99107 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018324 RID: 99108 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x170082DA RID: 33498
		// (get) Token: 0x06018325 RID: 99109 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018326 RID: 99110 RVA: 0x002BD47A File Offset: 0x002BB67A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(55, "knownFonts")]
		public BooleanValue KnownFonts
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

		// Token: 0x06018327 RID: 99111 RVA: 0x00293ECF File Offset: 0x002920CF
		public Fonts()
		{
		}

		// Token: 0x06018328 RID: 99112 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Fonts(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018329 RID: 99113 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Fonts(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601832A RID: 99114 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Fonts(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601832B RID: 99115 RVA: 0x0033F424 File Offset: 0x0033D624
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "font" == name)
			{
				return new Font();
			}
			return null;
		}

		// Token: 0x0601832C RID: 99116 RVA: 0x0033F43F File Offset: 0x0033D63F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (55 == namespaceId && "knownFonts" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601832D RID: 99117 RVA: 0x0033F477 File Offset: 0x0033D677
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fonts>(deep);
		}

		// Token: 0x04009F46 RID: 40774
		private const string tagName = "fonts";

		// Token: 0x04009F47 RID: 40775
		private const byte tagNsId = 22;

		// Token: 0x04009F48 RID: 40776
		internal const int ElementTypeIdConst = 11358;

		// Token: 0x04009F49 RID: 40777
		private static string[] attributeTagNames = new string[] { "count", "knownFonts" };

		// Token: 0x04009F4A RID: 40778
		private static byte[] attributeNamespaceIds = new byte[] { 0, 55 };
	}
}
