using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002525 RID: 9509
	[GeneratedCode("DomGen", "2.0")]
	internal class NoMultiLevelLabels : BooleanType
	{
		// Token: 0x17005488 RID: 21640
		// (get) Token: 0x06011B0B RID: 72459 RVA: 0x002F147C File Offset: 0x002EF67C
		public override string LocalName
		{
			get
			{
				return "noMultiLvlLbl";
			}
		}

		// Token: 0x17005489 RID: 21641
		// (get) Token: 0x06011B0C RID: 72460 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700548A RID: 21642
		// (get) Token: 0x06011B0D RID: 72461 RVA: 0x002F1483 File Offset: 0x002EF683
		internal override int ElementTypeId
		{
			get
			{
				return 10486;
			}
		}

		// Token: 0x06011B0E RID: 72462 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B10 RID: 72464 RVA: 0x002F148A File Offset: 0x002EF68A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoMultiLevelLabels>(deep);
		}

		// Token: 0x04007C03 RID: 31747
		private const string tagName = "noMultiLvlLbl";

		// Token: 0x04007C04 RID: 31748
		private const byte tagNsId = 11;

		// Token: 0x04007C05 RID: 31749
		internal const int ElementTypeIdConst = 10486;
	}
}
