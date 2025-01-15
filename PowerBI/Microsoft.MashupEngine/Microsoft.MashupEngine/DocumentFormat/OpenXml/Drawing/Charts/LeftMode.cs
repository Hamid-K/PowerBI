using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002588 RID: 9608
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftMode : LayoutModeType
	{
		// Token: 0x1700564A RID: 22090
		// (get) Token: 0x06011F11 RID: 73489 RVA: 0x002F3E53 File Offset: 0x002F2053
		public override string LocalName
		{
			get
			{
				return "xMode";
			}
		}

		// Token: 0x1700564B RID: 22091
		// (get) Token: 0x06011F12 RID: 73490 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700564C RID: 22092
		// (get) Token: 0x06011F13 RID: 73491 RVA: 0x002F3E5A File Offset: 0x002F205A
		internal override int ElementTypeId
		{
			get
			{
				return 10407;
			}
		}

		// Token: 0x06011F14 RID: 73492 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011F16 RID: 73494 RVA: 0x002F3E69 File Offset: 0x002F2069
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftMode>(deep);
		}

		// Token: 0x04007D5A RID: 32090
		private const string tagName = "xMode";

		// Token: 0x04007D5B RID: 32091
		private const byte tagNsId = 11;

		// Token: 0x04007D5C RID: 32092
		internal const int ElementTypeIdConst = 10407;
	}
}
