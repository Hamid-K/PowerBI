using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A84 RID: 10884
	[ChildElementInfo(typeof(OutlineViewSlideListEntry))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OutlineViewSlideList : OpenXmlCompositeElement
	{
		// Token: 0x17007369 RID: 29545
		// (get) Token: 0x060160D0 RID: 90320 RVA: 0x00323EF8 File Offset: 0x003220F8
		public override string LocalName
		{
			get
			{
				return "sldLst";
			}
		}

		// Token: 0x1700736A RID: 29546
		// (get) Token: 0x060160D1 RID: 90321 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700736B RID: 29547
		// (get) Token: 0x060160D2 RID: 90322 RVA: 0x003260C4 File Offset: 0x003242C4
		internal override int ElementTypeId
		{
			get
			{
				return 12297;
			}
		}

		// Token: 0x060160D3 RID: 90323 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060160D4 RID: 90324 RVA: 0x00293ECF File Offset: 0x002920CF
		public OutlineViewSlideList()
		{
		}

		// Token: 0x060160D5 RID: 90325 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OutlineViewSlideList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060160D6 RID: 90326 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OutlineViewSlideList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060160D7 RID: 90327 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OutlineViewSlideList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060160D8 RID: 90328 RVA: 0x003260CB File Offset: 0x003242CB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sld" == name)
			{
				return new OutlineViewSlideListEntry();
			}
			return null;
		}

		// Token: 0x060160D9 RID: 90329 RVA: 0x003260E6 File Offset: 0x003242E6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OutlineViewSlideList>(deep);
		}

		// Token: 0x040095FD RID: 38397
		private const string tagName = "sldLst";

		// Token: 0x040095FE RID: 38398
		private const byte tagNsId = 24;

		// Token: 0x040095FF RID: 38399
		internal const int ElementTypeIdConst = 12297;
	}
}
