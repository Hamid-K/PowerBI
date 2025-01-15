using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D9 RID: 9945
	[GeneratedCode("DomGen", "2.0")]
	internal class Gray : OpenXmlLeafElement
	{
		// Token: 0x17005DAA RID: 23978
		// (get) Token: 0x06012F7E RID: 77694 RVA: 0x00301689 File Offset: 0x002FF889
		public override string LocalName
		{
			get
			{
				return "gray";
			}
		}

		// Token: 0x17005DAB RID: 23979
		// (get) Token: 0x06012F7F RID: 77695 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DAC RID: 23980
		// (get) Token: 0x06012F80 RID: 77696 RVA: 0x00301690 File Offset: 0x002FF890
		internal override int ElementTypeId
		{
			get
			{
				return 10010;
			}
		}

		// Token: 0x06012F81 RID: 77697 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F83 RID: 77699 RVA: 0x00301697 File Offset: 0x002FF897
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Gray>(deep);
		}

		// Token: 0x040083F1 RID: 33777
		private const string tagName = "gray";

		// Token: 0x040083F2 RID: 33778
		private const byte tagNsId = 10;

		// Token: 0x040083F3 RID: 33779
		internal const int ElementTypeIdConst = 10010;
	}
}
