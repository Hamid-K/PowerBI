using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC4 RID: 10948
	[GeneratedCode("DomGen", "2.0")]
	internal class SubTimeNodeList : TimeTypeListType
	{
		// Token: 0x17007529 RID: 29993
		// (get) Token: 0x060164E3 RID: 91363 RVA: 0x00328D0C File Offset: 0x00326F0C
		public override string LocalName
		{
			get
			{
				return "subTnLst";
			}
		}

		// Token: 0x1700752A RID: 29994
		// (get) Token: 0x060164E4 RID: 91364 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700752B RID: 29995
		// (get) Token: 0x060164E5 RID: 91365 RVA: 0x00328D13 File Offset: 0x00326F13
		internal override int ElementTypeId
		{
			get
			{
				return 12365;
			}
		}

		// Token: 0x060164E6 RID: 91366 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060164E7 RID: 91367 RVA: 0x00328CE0 File Offset: 0x00326EE0
		public SubTimeNodeList()
		{
		}

		// Token: 0x060164E8 RID: 91368 RVA: 0x00328CE8 File Offset: 0x00326EE8
		public SubTimeNodeList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164E9 RID: 91369 RVA: 0x00328CF1 File Offset: 0x00326EF1
		public SubTimeNodeList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164EA RID: 91370 RVA: 0x00328CFA File Offset: 0x00326EFA
		public SubTimeNodeList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060164EB RID: 91371 RVA: 0x00328D1A File Offset: 0x00326F1A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubTimeNodeList>(deep);
		}

		// Token: 0x0400971E RID: 38686
		private const string tagName = "subTnLst";

		// Token: 0x0400971F RID: 38687
		private const byte tagNsId = 24;

		// Token: 0x04009720 RID: 38688
		internal const int ElementTypeIdConst = 12365;
	}
}
