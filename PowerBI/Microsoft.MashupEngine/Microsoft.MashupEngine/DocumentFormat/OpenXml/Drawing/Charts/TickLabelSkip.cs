using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025BA RID: 9658
	[GeneratedCode("DomGen", "2.0")]
	internal class TickLabelSkip : SkipType
	{
		// Token: 0x17005767 RID: 22375
		// (get) Token: 0x0601217C RID: 74108 RVA: 0x002F5767 File Offset: 0x002F3967
		public override string LocalName
		{
			get
			{
				return "tickLblSkip";
			}
		}

		// Token: 0x17005768 RID: 22376
		// (get) Token: 0x0601217D RID: 74109 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005769 RID: 22377
		// (get) Token: 0x0601217E RID: 74110 RVA: 0x002F576E File Offset: 0x002F396E
		internal override int ElementTypeId
		{
			get
			{
				return 10484;
			}
		}

		// Token: 0x0601217F RID: 74111 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012181 RID: 74113 RVA: 0x002F577D File Offset: 0x002F397D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TickLabelSkip>(deep);
		}

		// Token: 0x04007E2C RID: 32300
		private const string tagName = "tickLblSkip";

		// Token: 0x04007E2D RID: 32301
		private const byte tagNsId = 11;

		// Token: 0x04007E2E RID: 32302
		internal const int ElementTypeIdConst = 10484;
	}
}
