using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002648 RID: 9800
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColorsDefinitionHeader))]
	internal class ColorsDefinitionHeaderList : OpenXmlCompositeElement
	{
		// Token: 0x17005B0C RID: 23308
		// (get) Token: 0x06012988 RID: 76168 RVA: 0x002FD06B File Offset: 0x002FB26B
		public override string LocalName
		{
			get
			{
				return "colorsDefHdrLst";
			}
		}

		// Token: 0x17005B0D RID: 23309
		// (get) Token: 0x06012989 RID: 76169 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B0E RID: 23310
		// (get) Token: 0x0601298A RID: 76170 RVA: 0x002FD072 File Offset: 0x002FB272
		internal override int ElementTypeId
		{
			get
			{
				return 10618;
			}
		}

		// Token: 0x0601298B RID: 76171 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601298C RID: 76172 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorsDefinitionHeaderList()
		{
		}

		// Token: 0x0601298D RID: 76173 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorsDefinitionHeaderList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601298E RID: 76174 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorsDefinitionHeaderList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601298F RID: 76175 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorsDefinitionHeaderList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012990 RID: 76176 RVA: 0x002FD079 File Offset: 0x002FB279
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "colorsDefHdr" == name)
			{
				return new ColorsDefinitionHeader();
			}
			return null;
		}

		// Token: 0x06012991 RID: 76177 RVA: 0x002FD094 File Offset: 0x002FB294
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorsDefinitionHeaderList>(deep);
		}

		// Token: 0x040080D5 RID: 32981
		private const string tagName = "colorsDefHdrLst";

		// Token: 0x040080D6 RID: 32982
		private const byte tagNsId = 14;

		// Token: 0x040080D7 RID: 32983
		internal const int ElementTypeIdConst = 10618;
	}
}
