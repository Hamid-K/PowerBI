using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200270A RID: 9994
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaCeiling : OpenXmlLeafElement
	{
		// Token: 0x17005EB2 RID: 24242
		// (get) Token: 0x060131C0 RID: 78272 RVA: 0x00303CB3 File Offset: 0x00301EB3
		public override string LocalName
		{
			get
			{
				return "alphaCeiling";
			}
		}

		// Token: 0x17005EB3 RID: 24243
		// (get) Token: 0x060131C1 RID: 78273 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EB4 RID: 24244
		// (get) Token: 0x060131C2 RID: 78274 RVA: 0x00303CBA File Offset: 0x00301EBA
		internal override int ElementTypeId
		{
			get
			{
				return 10056;
			}
		}

		// Token: 0x060131C3 RID: 78275 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060131C5 RID: 78277 RVA: 0x00303CC1 File Offset: 0x00301EC1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaCeiling>(deep);
		}

		// Token: 0x040084B7 RID: 33975
		private const string tagName = "alphaCeiling";

		// Token: 0x040084B8 RID: 33976
		private const byte tagNsId = 10;

		// Token: 0x040084B9 RID: 33977
		internal const int ElementTypeIdConst = 10056;
	}
}
