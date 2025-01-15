using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200271A RID: 10010
	[GeneratedCode("DomGen", "2.0")]
	internal class Grayscale : OpenXmlLeafElement
	{
		// Token: 0x17005F38 RID: 24376
		// (get) Token: 0x060132DC RID: 78556 RVA: 0x00304842 File Offset: 0x00302A42
		public override string LocalName
		{
			get
			{
				return "grayscl";
			}
		}

		// Token: 0x17005F39 RID: 24377
		// (get) Token: 0x060132DD RID: 78557 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F3A RID: 24378
		// (get) Token: 0x060132DE RID: 78558 RVA: 0x00304849 File Offset: 0x00302A49
		internal override int ElementTypeId
		{
			get
			{
				return 10072;
			}
		}

		// Token: 0x060132DF RID: 78559 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060132E1 RID: 78561 RVA: 0x00304850 File Offset: 0x00302A50
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Grayscale>(deep);
		}

		// Token: 0x04008509 RID: 34057
		private const string tagName = "grayscl";

		// Token: 0x0400850A RID: 34058
		private const byte tagNsId = 10;

		// Token: 0x0400850B RID: 34059
		internal const int ElementTypeIdConst = 10072;
	}
}
