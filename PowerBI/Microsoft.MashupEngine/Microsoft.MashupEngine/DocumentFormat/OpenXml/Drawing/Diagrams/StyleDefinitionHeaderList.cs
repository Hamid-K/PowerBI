using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002650 RID: 9808
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StyleDefinitionHeader))]
	internal class StyleDefinitionHeaderList : OpenXmlCompositeElement
	{
		// Token: 0x17005B49 RID: 23369
		// (get) Token: 0x06012A25 RID: 76325 RVA: 0x002FD84B File Offset: 0x002FBA4B
		public override string LocalName
		{
			get
			{
				return "styleDefHdrLst";
			}
		}

		// Token: 0x17005B4A RID: 23370
		// (get) Token: 0x06012A26 RID: 76326 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B4B RID: 23371
		// (get) Token: 0x06012A27 RID: 76327 RVA: 0x002FD852 File Offset: 0x002FBA52
		internal override int ElementTypeId
		{
			get
			{
				return 10626;
			}
		}

		// Token: 0x06012A28 RID: 76328 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A29 RID: 76329 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleDefinitionHeaderList()
		{
		}

		// Token: 0x06012A2A RID: 76330 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleDefinitionHeaderList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A2B RID: 76331 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleDefinitionHeaderList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A2C RID: 76332 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleDefinitionHeaderList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A2D RID: 76333 RVA: 0x002FD859 File Offset: 0x002FBA59
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "styleDefHdr" == name)
			{
				return new StyleDefinitionHeader();
			}
			return null;
		}

		// Token: 0x06012A2E RID: 76334 RVA: 0x002FD874 File Offset: 0x002FBA74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleDefinitionHeaderList>(deep);
		}

		// Token: 0x040080F9 RID: 33017
		private const string tagName = "styleDefHdrLst";

		// Token: 0x040080FA RID: 33018
		private const byte tagNsId = 14;

		// Token: 0x040080FB RID: 33019
		internal const int ElementTypeIdConst = 10626;
	}
}
