using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E83 RID: 11907
	[GeneratedCode("DomGen", "2.0")]
	internal class ParagraphStyleIdInLevel : String253Type
	{
		// Token: 0x17008AF2 RID: 35570
		// (get) Token: 0x060194D4 RID: 103636 RVA: 0x0034476C File Offset: 0x0034296C
		public override string LocalName
		{
			get
			{
				return "pStyle";
			}
		}

		// Token: 0x17008AF3 RID: 35571
		// (get) Token: 0x060194D5 RID: 103637 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AF4 RID: 35572
		// (get) Token: 0x060194D6 RID: 103638 RVA: 0x0034865F File Offset: 0x0034685F
		internal override int ElementTypeId
		{
			get
			{
				return 11865;
			}
		}

		// Token: 0x060194D7 RID: 103639 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194D9 RID: 103641 RVA: 0x00348666 File Offset: 0x00346866
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphStyleIdInLevel>(deep);
		}

		// Token: 0x0400A82F RID: 43055
		private const string tagName = "pStyle";

		// Token: 0x0400A830 RID: 43056
		private const byte tagNsId = 23;

		// Token: 0x0400A831 RID: 43057
		internal const int ElementTypeIdConst = 11865;
	}
}
