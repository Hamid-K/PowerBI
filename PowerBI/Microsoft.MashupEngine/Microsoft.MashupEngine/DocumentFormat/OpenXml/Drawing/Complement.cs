using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D7 RID: 9943
	[GeneratedCode("DomGen", "2.0")]
	internal class Complement : OpenXmlLeafElement
	{
		// Token: 0x17005DA4 RID: 23972
		// (get) Token: 0x06012F72 RID: 77682 RVA: 0x0030165B File Offset: 0x002FF85B
		public override string LocalName
		{
			get
			{
				return "comp";
			}
		}

		// Token: 0x17005DA5 RID: 23973
		// (get) Token: 0x06012F73 RID: 77683 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DA6 RID: 23974
		// (get) Token: 0x06012F74 RID: 77684 RVA: 0x00301662 File Offset: 0x002FF862
		internal override int ElementTypeId
		{
			get
			{
				return 10008;
			}
		}

		// Token: 0x06012F75 RID: 77685 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F77 RID: 77687 RVA: 0x00301669 File Offset: 0x002FF869
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Complement>(deep);
		}

		// Token: 0x040083EB RID: 33771
		private const string tagName = "comp";

		// Token: 0x040083EC RID: 33772
		private const byte tagNsId = 10;

		// Token: 0x040083ED RID: 33773
		internal const int ElementTypeIdConst = 10008;
	}
}
