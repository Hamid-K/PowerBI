using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200264B RID: 9803
	[ChildElementInfo(typeof(CategoryList))]
	[ChildElementInfo(typeof(Description))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Title))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class LayoutDefinitionHeader : OpenXmlCompositeElement
	{
		// Token: 0x17005B24 RID: 23332
		// (get) Token: 0x060129C8 RID: 76232 RVA: 0x002FD3A7 File Offset: 0x002FB5A7
		public override string LocalName
		{
			get
			{
				return "layoutDefHdr";
			}
		}

		// Token: 0x17005B25 RID: 23333
		// (get) Token: 0x060129C9 RID: 76233 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B26 RID: 23334
		// (get) Token: 0x060129CA RID: 76234 RVA: 0x002FD3AE File Offset: 0x002FB5AE
		internal override int ElementTypeId
		{
			get
			{
				return 10621;
			}
		}

		// Token: 0x060129CB RID: 76235 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B27 RID: 23335
		// (get) Token: 0x060129CC RID: 76236 RVA: 0x002FD3B5 File Offset: 0x002FB5B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return LayoutDefinitionHeader.attributeTagNames;
			}
		}

		// Token: 0x17005B28 RID: 23336
		// (get) Token: 0x060129CD RID: 76237 RVA: 0x002FD3BC File Offset: 0x002FB5BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LayoutDefinitionHeader.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B29 RID: 23337
		// (get) Token: 0x060129CE RID: 76238 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060129CF RID: 76239 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005B2A RID: 23338
		// (get) Token: 0x060129D0 RID: 76240 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060129D1 RID: 76241 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005B2B RID: 23339
		// (get) Token: 0x060129D2 RID: 76242 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060129D3 RID: 76243 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "defStyle")]
		public StringValue DefaultStyle
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005B2C RID: 23340
		// (get) Token: 0x060129D4 RID: 76244 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x060129D5 RID: 76245 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "resId")]
		public Int32Value ResourceId
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060129D6 RID: 76246 RVA: 0x00293ECF File Offset: 0x002920CF
		public LayoutDefinitionHeader()
		{
		}

		// Token: 0x060129D7 RID: 76247 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LayoutDefinitionHeader(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060129D8 RID: 76248 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LayoutDefinitionHeader(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060129D9 RID: 76249 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LayoutDefinitionHeader(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060129DA RID: 76250 RVA: 0x002FD3C4 File Offset: 0x002FB5C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "title" == name)
			{
				return new Title();
			}
			if (14 == namespaceId && "desc" == name)
			{
				return new Description();
			}
			if (14 == namespaceId && "catLst" == name)
			{
				return new CategoryList();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060129DB RID: 76251 RVA: 0x002FD434 File Offset: 0x002FB634
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
			if (namespaceId == 0 && "defStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "resId" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060129DC RID: 76252 RVA: 0x002FD4A1 File Offset: 0x002FB6A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutDefinitionHeader>(deep);
		}

		// Token: 0x060129DD RID: 76253 RVA: 0x002FD4AC File Offset: 0x002FB6AC
		// Note: this type is marked as 'beforefieldinit'.
		static LayoutDefinitionHeader()
		{
			byte[] array = new byte[4];
			LayoutDefinitionHeader.attributeNamespaceIds = array;
		}

		// Token: 0x040080E2 RID: 32994
		private const string tagName = "layoutDefHdr";

		// Token: 0x040080E3 RID: 32995
		private const byte tagNsId = 14;

		// Token: 0x040080E4 RID: 32996
		internal const int ElementTypeIdConst = 10621;

		// Token: 0x040080E5 RID: 32997
		private static string[] attributeTagNames = new string[] { "uniqueId", "minVer", "defStyle", "resId" };

		// Token: 0x040080E6 RID: 32998
		private static byte[] attributeNamespaceIds;
	}
}
