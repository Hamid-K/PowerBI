using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E95 RID: 11925
	[GeneratedCode("DomGen", "2.0")]
	internal class FontSizeComplexScript : HpsMeasureType
	{
		// Token: 0x17008B45 RID: 35653
		// (get) Token: 0x0601957B RID: 103803 RVA: 0x00348BFC File Offset: 0x00346DFC
		public override string LocalName
		{
			get
			{
				return "szCs";
			}
		}

		// Token: 0x17008B46 RID: 35654
		// (get) Token: 0x0601957C RID: 103804 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B47 RID: 35655
		// (get) Token: 0x0601957D RID: 103805 RVA: 0x00348C03 File Offset: 0x00346E03
		internal override int ElementTypeId
		{
			get
			{
				return 11598;
			}
		}

		// Token: 0x0601957E RID: 103806 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019580 RID: 103808 RVA: 0x00348C0A File Offset: 0x00346E0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontSizeComplexScript>(deep);
		}

		// Token: 0x0400A870 RID: 43120
		private const string tagName = "szCs";

		// Token: 0x0400A871 RID: 43121
		private const byte tagNsId = 23;

		// Token: 0x0400A872 RID: 43122
		internal const int ElementTypeIdConst = 11598;
	}
}
