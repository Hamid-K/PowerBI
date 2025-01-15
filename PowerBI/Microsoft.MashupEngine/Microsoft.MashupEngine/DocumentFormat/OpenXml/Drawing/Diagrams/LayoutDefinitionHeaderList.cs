using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200264C RID: 9804
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LayoutDefinitionHeader))]
	internal class LayoutDefinitionHeaderList : OpenXmlCompositeElement
	{
		// Token: 0x17005B2D RID: 23341
		// (get) Token: 0x060129DE RID: 76254 RVA: 0x002FD4F3 File Offset: 0x002FB6F3
		public override string LocalName
		{
			get
			{
				return "layoutDefHdrLst";
			}
		}

		// Token: 0x17005B2E RID: 23342
		// (get) Token: 0x060129DF RID: 76255 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B2F RID: 23343
		// (get) Token: 0x060129E0 RID: 76256 RVA: 0x002FD4FA File Offset: 0x002FB6FA
		internal override int ElementTypeId
		{
			get
			{
				return 10622;
			}
		}

		// Token: 0x060129E1 RID: 76257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060129E2 RID: 76258 RVA: 0x00293ECF File Offset: 0x002920CF
		public LayoutDefinitionHeaderList()
		{
		}

		// Token: 0x060129E3 RID: 76259 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LayoutDefinitionHeaderList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060129E4 RID: 76260 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LayoutDefinitionHeaderList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060129E5 RID: 76261 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LayoutDefinitionHeaderList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060129E6 RID: 76262 RVA: 0x002FD501 File Offset: 0x002FB701
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "layoutDefHdr" == name)
			{
				return new LayoutDefinitionHeader();
			}
			return null;
		}

		// Token: 0x060129E7 RID: 76263 RVA: 0x002FD51C File Offset: 0x002FB71C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutDefinitionHeaderList>(deep);
		}

		// Token: 0x040080E7 RID: 32999
		private const string tagName = "layoutDefHdrLst";

		// Token: 0x040080E8 RID: 33000
		private const byte tagNsId = 14;

		// Token: 0x040080E9 RID: 33001
		internal const int ElementTypeIdConst = 10622;
	}
}
