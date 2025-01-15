using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A26 RID: 10790
	[GeneratedCode("DomGen", "2.0")]
	internal class StartConditionList : TimeListTimeConditionalListType
	{
		// Token: 0x1700709C RID: 28828
		// (get) Token: 0x06015AA1 RID: 88737 RVA: 0x00321DFD File Offset: 0x0031FFFD
		public override string LocalName
		{
			get
			{
				return "stCondLst";
			}
		}

		// Token: 0x1700709D RID: 28829
		// (get) Token: 0x06015AA2 RID: 88738 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700709E RID: 28830
		// (get) Token: 0x06015AA3 RID: 88739 RVA: 0x00321E04 File Offset: 0x00320004
		internal override int ElementTypeId
		{
			get
			{
				return 12360;
			}
		}

		// Token: 0x06015AA4 RID: 88740 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015AA5 RID: 88741 RVA: 0x00321DBA File Offset: 0x0031FFBA
		public StartConditionList()
		{
		}

		// Token: 0x06015AA6 RID: 88742 RVA: 0x00321DC2 File Offset: 0x0031FFC2
		public StartConditionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015AA7 RID: 88743 RVA: 0x00321DCB File Offset: 0x0031FFCB
		public StartConditionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015AA8 RID: 88744 RVA: 0x00321DD4 File Offset: 0x0031FFD4
		public StartConditionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015AA9 RID: 88745 RVA: 0x00321E0B File Offset: 0x0032000B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartConditionList>(deep);
		}

		// Token: 0x04009444 RID: 37956
		private const string tagName = "stCondLst";

		// Token: 0x04009445 RID: 37957
		private const byte tagNsId = 24;

		// Token: 0x04009446 RID: 37958
		internal const int ElementTypeIdConst = 12360;
	}
}
