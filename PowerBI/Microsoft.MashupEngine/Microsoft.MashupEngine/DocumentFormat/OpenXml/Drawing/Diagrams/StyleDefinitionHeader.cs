using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200264F RID: 9807
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StyleLabelDescription))]
	[ChildElementInfo(typeof(StyleDisplayCategories))]
	[ChildElementInfo(typeof(StyleDefinitionTitle))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class StyleDefinitionHeader : OpenXmlCompositeElement
	{
		// Token: 0x17005B41 RID: 23361
		// (get) Token: 0x06012A11 RID: 76305 RVA: 0x002FD71F File Offset: 0x002FB91F
		public override string LocalName
		{
			get
			{
				return "styleDefHdr";
			}
		}

		// Token: 0x17005B42 RID: 23362
		// (get) Token: 0x06012A12 RID: 76306 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B43 RID: 23363
		// (get) Token: 0x06012A13 RID: 76307 RVA: 0x002FD726 File Offset: 0x002FB926
		internal override int ElementTypeId
		{
			get
			{
				return 10625;
			}
		}

		// Token: 0x06012A14 RID: 76308 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B44 RID: 23364
		// (get) Token: 0x06012A15 RID: 76309 RVA: 0x002FD72D File Offset: 0x002FB92D
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleDefinitionHeader.attributeTagNames;
			}
		}

		// Token: 0x17005B45 RID: 23365
		// (get) Token: 0x06012A16 RID: 76310 RVA: 0x002FD734 File Offset: 0x002FB934
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleDefinitionHeader.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B46 RID: 23366
		// (get) Token: 0x06012A17 RID: 76311 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012A18 RID: 76312 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueId")]
		public StringValue UniqueId
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

		// Token: 0x17005B47 RID: 23367
		// (get) Token: 0x06012A19 RID: 76313 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012A1A RID: 76314 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minVer")]
		public StringValue MinVersion
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

		// Token: 0x17005B48 RID: 23368
		// (get) Token: 0x06012A1B RID: 76315 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06012A1C RID: 76316 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "resId")]
		public Int32Value ResourceId
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06012A1D RID: 76317 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleDefinitionHeader()
		{
		}

		// Token: 0x06012A1E RID: 76318 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleDefinitionHeader(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A1F RID: 76319 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleDefinitionHeader(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A20 RID: 76320 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleDefinitionHeader(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A21 RID: 76321 RVA: 0x002FD73C File Offset: 0x002FB93C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "title" == name)
			{
				return new StyleDefinitionTitle();
			}
			if (14 == namespaceId && "desc" == name)
			{
				return new StyleLabelDescription();
			}
			if (14 == namespaceId && "catLst" == name)
			{
				return new StyleDisplayCategories();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012A22 RID: 76322 RVA: 0x002FD7AC File Offset: 0x002FB9AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minVer" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "resId" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012A23 RID: 76323 RVA: 0x002FD803 File Offset: 0x002FBA03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleDefinitionHeader>(deep);
		}

		// Token: 0x06012A24 RID: 76324 RVA: 0x002FD80C File Offset: 0x002FBA0C
		// Note: this type is marked as 'beforefieldinit'.
		static StyleDefinitionHeader()
		{
			byte[] array = new byte[3];
			StyleDefinitionHeader.attributeNamespaceIds = array;
		}

		// Token: 0x040080F4 RID: 33012
		private const string tagName = "styleDefHdr";

		// Token: 0x040080F5 RID: 33013
		private const byte tagNsId = 14;

		// Token: 0x040080F6 RID: 33014
		internal const int ElementTypeIdConst = 10625;

		// Token: 0x040080F7 RID: 33015
		private static string[] attributeTagNames = new string[] { "uniqueId", "minVer", "resId" };

		// Token: 0x040080F8 RID: 33016
		private static byte[] attributeNamespaceIds;
	}
}
