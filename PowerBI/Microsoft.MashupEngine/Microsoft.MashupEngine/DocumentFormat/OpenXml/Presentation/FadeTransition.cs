using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD6 RID: 10966
	[GeneratedCode("DomGen", "2.0")]
	internal class FadeTransition : OptionalBlackTransitionType
	{
		// Token: 0x17007578 RID: 30072
		// (get) Token: 0x0601658D RID: 91533 RVA: 0x002E532B File Offset: 0x002E352B
		public override string LocalName
		{
			get
			{
				return "fade";
			}
		}

		// Token: 0x17007579 RID: 30073
		// (get) Token: 0x0601658E RID: 91534 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700757A RID: 30074
		// (get) Token: 0x0601658F RID: 91535 RVA: 0x003292E2 File Offset: 0x003274E2
		internal override int ElementTypeId
		{
			get
			{
				return 12383;
			}
		}

		// Token: 0x06016590 RID: 91536 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016592 RID: 91538 RVA: 0x003292E9 File Offset: 0x003274E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FadeTransition>(deep);
		}

		// Token: 0x0400975A RID: 38746
		private const string tagName = "fade";

		// Token: 0x0400975B RID: 38747
		private const byte tagNsId = 24;

		// Token: 0x0400975C RID: 38748
		internal const int ElementTypeIdConst = 12383;
	}
}
