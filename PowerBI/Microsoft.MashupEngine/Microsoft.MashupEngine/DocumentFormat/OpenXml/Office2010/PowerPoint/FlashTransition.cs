using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A1 RID: 9121
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class FlashTransition : EmptyType
	{
		// Token: 0x17004C06 RID: 19462
		// (get) Token: 0x0601080B RID: 67595 RVA: 0x002E4272 File Offset: 0x002E2472
		public override string LocalName
		{
			get
			{
				return "flash";
			}
		}

		// Token: 0x17004C07 RID: 19463
		// (get) Token: 0x0601080C RID: 67596 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C08 RID: 19464
		// (get) Token: 0x0601080D RID: 67597 RVA: 0x002E4279 File Offset: 0x002E2479
		internal override int ElementTypeId
		{
			get
			{
				return 12783;
			}
		}

		// Token: 0x0601080E RID: 67598 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010810 RID: 67600 RVA: 0x002E4280 File Offset: 0x002E2480
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FlashTransition>(deep);
		}

		// Token: 0x040074EE RID: 29934
		private const string tagName = "flash";

		// Token: 0x040074EF RID: 29935
		private const byte tagNsId = 49;

		// Token: 0x040074F0 RID: 29936
		internal const int ElementTypeIdConst = 12783;
	}
}
