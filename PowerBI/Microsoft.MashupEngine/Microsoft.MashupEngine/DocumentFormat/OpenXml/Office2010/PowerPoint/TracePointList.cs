using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C6 RID: 9158
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TracePoint), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TracePointList : OpenXmlCompositeElement
	{
		// Token: 0x17004CF6 RID: 19702
		// (get) Token: 0x06010A0E RID: 68110 RVA: 0x002E57B7 File Offset: 0x002E39B7
		public override string LocalName
		{
			get
			{
				return "tracePtLst";
			}
		}

		// Token: 0x17004CF7 RID: 19703
		// (get) Token: 0x06010A0F RID: 68111 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CF8 RID: 19704
		// (get) Token: 0x06010A10 RID: 68112 RVA: 0x002E57BE File Offset: 0x002E39BE
		internal override int ElementTypeId
		{
			get
			{
				return 12812;
			}
		}

		// Token: 0x06010A11 RID: 68113 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A12 RID: 68114 RVA: 0x00293ECF File Offset: 0x002920CF
		public TracePointList()
		{
		}

		// Token: 0x06010A13 RID: 68115 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TracePointList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A14 RID: 68116 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TracePointList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A15 RID: 68117 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TracePointList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010A16 RID: 68118 RVA: 0x002E57C5 File Offset: 0x002E39C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "tracePt" == name)
			{
				return new TracePoint();
			}
			return null;
		}

		// Token: 0x06010A17 RID: 68119 RVA: 0x002E57E0 File Offset: 0x002E39E0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TracePointList>(deep);
		}

		// Token: 0x04007595 RID: 30101
		private const string tagName = "tracePtLst";

		// Token: 0x04007596 RID: 30102
		private const byte tagNsId = 49;

		// Token: 0x04007597 RID: 30103
		internal const int ElementTypeIdConst = 12812;
	}
}
