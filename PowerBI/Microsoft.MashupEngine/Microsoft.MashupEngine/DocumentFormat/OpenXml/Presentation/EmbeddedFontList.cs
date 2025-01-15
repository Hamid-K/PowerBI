using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB5 RID: 10933
	[ChildElementInfo(typeof(EmbeddedFont))]
	[GeneratedCode("DomGen", "2.0")]
	internal class EmbeddedFontList : OpenXmlCompositeElement
	{
		// Token: 0x170074D4 RID: 29908
		// (get) Token: 0x0601641E RID: 91166 RVA: 0x0032842C File Offset: 0x0032662C
		public override string LocalName
		{
			get
			{
				return "embeddedFontLst";
			}
		}

		// Token: 0x170074D5 RID: 29909
		// (get) Token: 0x0601641F RID: 91167 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074D6 RID: 29910
		// (get) Token: 0x06016420 RID: 91168 RVA: 0x00328433 File Offset: 0x00326633
		internal override int ElementTypeId
		{
			get
			{
				return 12348;
			}
		}

		// Token: 0x06016421 RID: 91169 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016422 RID: 91170 RVA: 0x00293ECF File Offset: 0x002920CF
		public EmbeddedFontList()
		{
		}

		// Token: 0x06016423 RID: 91171 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EmbeddedFontList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016424 RID: 91172 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EmbeddedFontList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016425 RID: 91173 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EmbeddedFontList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016426 RID: 91174 RVA: 0x0032843A File Offset: 0x0032663A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "embeddedFont" == name)
			{
				return new EmbeddedFont();
			}
			return null;
		}

		// Token: 0x06016427 RID: 91175 RVA: 0x00328455 File Offset: 0x00326655
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbeddedFontList>(deep);
		}

		// Token: 0x040096E9 RID: 38633
		private const string tagName = "embeddedFontLst";

		// Token: 0x040096EA RID: 38634
		private const byte tagNsId = 24;

		// Token: 0x040096EB RID: 38635
		internal const int ElementTypeIdConst = 12348;
	}
}
