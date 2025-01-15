using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A27 RID: 10791
	[GeneratedCode("DomGen", "2.0")]
	internal class EndConditionList : TimeListTimeConditionalListType
	{
		// Token: 0x1700709F RID: 28831
		// (get) Token: 0x06015AAA RID: 88746 RVA: 0x00321E14 File Offset: 0x00320014
		public override string LocalName
		{
			get
			{
				return "endCondLst";
			}
		}

		// Token: 0x170070A0 RID: 28832
		// (get) Token: 0x06015AAB RID: 88747 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070A1 RID: 28833
		// (get) Token: 0x06015AAC RID: 88748 RVA: 0x00321E1B File Offset: 0x0032001B
		internal override int ElementTypeId
		{
			get
			{
				return 12361;
			}
		}

		// Token: 0x06015AAD RID: 88749 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015AAE RID: 88750 RVA: 0x00321DBA File Offset: 0x0031FFBA
		public EndConditionList()
		{
		}

		// Token: 0x06015AAF RID: 88751 RVA: 0x00321DC2 File Offset: 0x0031FFC2
		public EndConditionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015AB0 RID: 88752 RVA: 0x00321DCB File Offset: 0x0031FFCB
		public EndConditionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015AB1 RID: 88753 RVA: 0x00321DD4 File Offset: 0x0031FFD4
		public EndConditionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015AB2 RID: 88754 RVA: 0x00321E22 File Offset: 0x00320022
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndConditionList>(deep);
		}

		// Token: 0x04009447 RID: 37959
		private const string tagName = "endCondLst";

		// Token: 0x04009448 RID: 37960
		private const byte tagNsId = 24;

		// Token: 0x04009449 RID: 37961
		internal const int ElementTypeIdConst = 12361;
	}
}
