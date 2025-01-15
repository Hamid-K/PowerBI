using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D8 RID: 9944
	[GeneratedCode("DomGen", "2.0")]
	internal class Inverse : OpenXmlLeafElement
	{
		// Token: 0x17005DA7 RID: 23975
		// (get) Token: 0x06012F78 RID: 77688 RVA: 0x00301672 File Offset: 0x002FF872
		public override string LocalName
		{
			get
			{
				return "inv";
			}
		}

		// Token: 0x17005DA8 RID: 23976
		// (get) Token: 0x06012F79 RID: 77689 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DA9 RID: 23977
		// (get) Token: 0x06012F7A RID: 77690 RVA: 0x00301679 File Offset: 0x002FF879
		internal override int ElementTypeId
		{
			get
			{
				return 10009;
			}
		}

		// Token: 0x06012F7B RID: 77691 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F7D RID: 77693 RVA: 0x00301680 File Offset: 0x002FF880
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Inverse>(deep);
		}

		// Token: 0x040083EE RID: 33774
		private const string tagName = "inv";

		// Token: 0x040083EF RID: 33775
		private const byte tagNsId = 10;

		// Token: 0x040083F0 RID: 33776
		internal const int ElementTypeIdConst = 10009;
	}
}
