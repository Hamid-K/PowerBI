using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A25 RID: 10789
	[GeneratedCode("DomGen", "2.0")]
	internal class NextConditionList : TimeListTimeConditionalListType
	{
		// Token: 0x17007099 RID: 28825
		// (get) Token: 0x06015A98 RID: 88728 RVA: 0x00321DE6 File Offset: 0x0031FFE6
		public override string LocalName
		{
			get
			{
				return "nextCondLst";
			}
		}

		// Token: 0x1700709A RID: 28826
		// (get) Token: 0x06015A99 RID: 88729 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700709B RID: 28827
		// (get) Token: 0x06015A9A RID: 88730 RVA: 0x00321DED File Offset: 0x0031FFED
		internal override int ElementTypeId
		{
			get
			{
				return 12214;
			}
		}

		// Token: 0x06015A9B RID: 88731 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015A9C RID: 88732 RVA: 0x00321DBA File Offset: 0x0031FFBA
		public NextConditionList()
		{
		}

		// Token: 0x06015A9D RID: 88733 RVA: 0x00321DC2 File Offset: 0x0031FFC2
		public NextConditionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A9E RID: 88734 RVA: 0x00321DCB File Offset: 0x0031FFCB
		public NextConditionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A9F RID: 88735 RVA: 0x00321DD4 File Offset: 0x0031FFD4
		public NextConditionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015AA0 RID: 88736 RVA: 0x00321DF4 File Offset: 0x0031FFF4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NextConditionList>(deep);
		}

		// Token: 0x04009441 RID: 37953
		private const string tagName = "nextCondLst";

		// Token: 0x04009442 RID: 37954
		private const byte tagNsId = 24;

		// Token: 0x04009443 RID: 37955
		internal const int ElementTypeIdConst = 12214;
	}
}
