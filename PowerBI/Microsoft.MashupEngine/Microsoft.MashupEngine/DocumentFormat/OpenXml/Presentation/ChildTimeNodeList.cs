using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC3 RID: 10947
	[GeneratedCode("DomGen", "2.0")]
	internal class ChildTimeNodeList : TimeTypeListType
	{
		// Token: 0x17007526 RID: 29990
		// (get) Token: 0x060164DA RID: 91354 RVA: 0x00328CD2 File Offset: 0x00326ED2
		public override string LocalName
		{
			get
			{
				return "childTnLst";
			}
		}

		// Token: 0x17007527 RID: 29991
		// (get) Token: 0x060164DB RID: 91355 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007528 RID: 29992
		// (get) Token: 0x060164DC RID: 91356 RVA: 0x00328CD9 File Offset: 0x00326ED9
		internal override int ElementTypeId
		{
			get
			{
				return 12364;
			}
		}

		// Token: 0x060164DD RID: 91357 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060164DE RID: 91358 RVA: 0x00328CE0 File Offset: 0x00326EE0
		public ChildTimeNodeList()
		{
		}

		// Token: 0x060164DF RID: 91359 RVA: 0x00328CE8 File Offset: 0x00326EE8
		public ChildTimeNodeList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164E0 RID: 91360 RVA: 0x00328CF1 File Offset: 0x00326EF1
		public ChildTimeNodeList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164E1 RID: 91361 RVA: 0x00328CFA File Offset: 0x00326EFA
		public ChildTimeNodeList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060164E2 RID: 91362 RVA: 0x00328D03 File Offset: 0x00326F03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChildTimeNodeList>(deep);
		}

		// Token: 0x0400971B RID: 38683
		private const string tagName = "childTnLst";

		// Token: 0x0400971C RID: 38684
		private const byte tagNsId = 24;

		// Token: 0x0400971D RID: 38685
		internal const int ElementTypeIdConst = 12364;
	}
}
