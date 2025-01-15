using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C3 RID: 9155
	[ChildElementInfo(typeof(SectionSlideIdListEntry), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SectionSlideIdList : OpenXmlCompositeElement
	{
		// Token: 0x17004CDF RID: 19679
		// (get) Token: 0x060109DB RID: 68059 RVA: 0x002E55A3 File Offset: 0x002E37A3
		public override string LocalName
		{
			get
			{
				return "sldIdLst";
			}
		}

		// Token: 0x17004CE0 RID: 19680
		// (get) Token: 0x060109DC RID: 68060 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CE1 RID: 19681
		// (get) Token: 0x060109DD RID: 68061 RVA: 0x002E55AA File Offset: 0x002E37AA
		internal override int ElementTypeId
		{
			get
			{
				return 12809;
			}
		}

		// Token: 0x060109DE RID: 68062 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060109DF RID: 68063 RVA: 0x00293ECF File Offset: 0x002920CF
		public SectionSlideIdList()
		{
		}

		// Token: 0x060109E0 RID: 68064 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SectionSlideIdList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109E1 RID: 68065 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SectionSlideIdList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109E2 RID: 68066 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SectionSlideIdList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060109E3 RID: 68067 RVA: 0x002E55B1 File Offset: 0x002E37B1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "sldId" == name)
			{
				return new SectionSlideIdListEntry();
			}
			return null;
		}

		// Token: 0x060109E4 RID: 68068 RVA: 0x002E55CC File Offset: 0x002E37CC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionSlideIdList>(deep);
		}

		// Token: 0x04007586 RID: 30086
		private const string tagName = "sldIdLst";

		// Token: 0x04007587 RID: 30087
		private const byte tagNsId = 49;

		// Token: 0x04007588 RID: 30088
		internal const int ElementTypeIdConst = 12809;
	}
}
