using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023AE RID: 9134
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Section), FileFormatVersions.Office2010)]
	internal class SectionList : OpenXmlCompositeElement
	{
		// Token: 0x17004C4C RID: 19532
		// (get) Token: 0x0601089C RID: 67740 RVA: 0x002E4831 File Offset: 0x002E2A31
		public override string LocalName
		{
			get
			{
				return "sectionLst";
			}
		}

		// Token: 0x17004C4D RID: 19533
		// (get) Token: 0x0601089D RID: 67741 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C4E RID: 19534
		// (get) Token: 0x0601089E RID: 67742 RVA: 0x002E4838 File Offset: 0x002E2A38
		internal override int ElementTypeId
		{
			get
			{
				return 12789;
			}
		}

		// Token: 0x0601089F RID: 67743 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060108A0 RID: 67744 RVA: 0x00293ECF File Offset: 0x002920CF
		public SectionList()
		{
		}

		// Token: 0x060108A1 RID: 67745 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SectionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060108A2 RID: 67746 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SectionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060108A3 RID: 67747 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SectionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060108A4 RID: 67748 RVA: 0x002E483F File Offset: 0x002E2A3F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "section" == name)
			{
				return new Section();
			}
			return null;
		}

		// Token: 0x060108A5 RID: 67749 RVA: 0x002E485A File Offset: 0x002E2A5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionList>(deep);
		}

		// Token: 0x04007524 RID: 29988
		private const string tagName = "sectionLst";

		// Token: 0x04007525 RID: 29989
		private const byte tagNsId = 49;

		// Token: 0x04007526 RID: 29990
		internal const int ElementTypeIdConst = 12789;
	}
}
