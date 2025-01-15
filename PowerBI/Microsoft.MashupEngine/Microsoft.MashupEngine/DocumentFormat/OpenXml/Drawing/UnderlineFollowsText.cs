using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002754 RID: 10068
	[GeneratedCode("DomGen", "2.0")]
	internal class UnderlineFollowsText : OpenXmlLeafElement
	{
		// Token: 0x170060AE RID: 24750
		// (get) Token: 0x0601360F RID: 79375 RVA: 0x00306744 File Offset: 0x00304944
		public override string LocalName
		{
			get
			{
				return "uLnTx";
			}
		}

		// Token: 0x170060AF RID: 24751
		// (get) Token: 0x06013610 RID: 79376 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060B0 RID: 24752
		// (get) Token: 0x06013611 RID: 79377 RVA: 0x0030674B File Offset: 0x0030494B
		internal override int ElementTypeId
		{
			get
			{
				return 10113;
			}
		}

		// Token: 0x06013612 RID: 79378 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013614 RID: 79380 RVA: 0x00306752 File Offset: 0x00304952
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnderlineFollowsText>(deep);
		}

		// Token: 0x040085F3 RID: 34291
		private const string tagName = "uLnTx";

		// Token: 0x040085F4 RID: 34292
		private const byte tagNsId = 10;

		// Token: 0x040085F5 RID: 34293
		internal const int ElementTypeIdConst = 10113;
	}
}
