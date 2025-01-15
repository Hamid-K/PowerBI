using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B4 RID: 9140
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TracePointList), FileFormatVersions.Office2010)]
	internal class LaserTraceList : OpenXmlCompositeElement
	{
		// Token: 0x17004C73 RID: 19571
		// (get) Token: 0x060108F0 RID: 67824 RVA: 0x002E4B8F File Offset: 0x002E2D8F
		public override string LocalName
		{
			get
			{
				return "laserTraceLst";
			}
		}

		// Token: 0x17004C74 RID: 19572
		// (get) Token: 0x060108F1 RID: 67825 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C75 RID: 19573
		// (get) Token: 0x060108F2 RID: 67826 RVA: 0x002E4B96 File Offset: 0x002E2D96
		internal override int ElementTypeId
		{
			get
			{
				return 12795;
			}
		}

		// Token: 0x060108F3 RID: 67827 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060108F4 RID: 67828 RVA: 0x00293ECF File Offset: 0x002920CF
		public LaserTraceList()
		{
		}

		// Token: 0x060108F5 RID: 67829 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LaserTraceList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060108F6 RID: 67830 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LaserTraceList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060108F7 RID: 67831 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LaserTraceList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060108F8 RID: 67832 RVA: 0x002E4B9D File Offset: 0x002E2D9D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "tracePtLst" == name)
			{
				return new TracePointList();
			}
			return null;
		}

		// Token: 0x060108F9 RID: 67833 RVA: 0x002E4BB8 File Offset: 0x002E2DB8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LaserTraceList>(deep);
		}

		// Token: 0x04007540 RID: 30016
		private const string tagName = "laserTraceLst";

		// Token: 0x04007541 RID: 30017
		private const byte tagNsId = 49;

		// Token: 0x04007542 RID: 30018
		internal const int ElementTypeIdConst = 12795;
	}
}
