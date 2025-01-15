using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A24 RID: 10788
	[GeneratedCode("DomGen", "2.0")]
	internal class PreviousConditionList : TimeListTimeConditionalListType
	{
		// Token: 0x17007096 RID: 28822
		// (get) Token: 0x06015A8F RID: 88719 RVA: 0x00321DAC File Offset: 0x0031FFAC
		public override string LocalName
		{
			get
			{
				return "prevCondLst";
			}
		}

		// Token: 0x17007097 RID: 28823
		// (get) Token: 0x06015A90 RID: 88720 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007098 RID: 28824
		// (get) Token: 0x06015A91 RID: 88721 RVA: 0x00321DB3 File Offset: 0x0031FFB3
		internal override int ElementTypeId
		{
			get
			{
				return 12213;
			}
		}

		// Token: 0x06015A92 RID: 88722 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015A93 RID: 88723 RVA: 0x00321DBA File Offset: 0x0031FFBA
		public PreviousConditionList()
		{
		}

		// Token: 0x06015A94 RID: 88724 RVA: 0x00321DC2 File Offset: 0x0031FFC2
		public PreviousConditionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A95 RID: 88725 RVA: 0x00321DCB File Offset: 0x0031FFCB
		public PreviousConditionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A96 RID: 88726 RVA: 0x00321DD4 File Offset: 0x0031FFD4
		public PreviousConditionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015A97 RID: 88727 RVA: 0x00321DDD File Offset: 0x0031FFDD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousConditionList>(deep);
		}

		// Token: 0x0400943E RID: 37950
		private const string tagName = "prevCondLst";

		// Token: 0x0400943F RID: 37951
		private const byte tagNsId = 24;

		// Token: 0x04009440 RID: 37952
		internal const int ElementTypeIdConst = 12213;
	}
}
