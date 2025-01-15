using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002399 RID: 9113
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SwitchTransition : LeftRightDirectionTransitionType
	{
		// Token: 0x17004BEE RID: 19438
		// (get) Token: 0x060107DA RID: 67546 RVA: 0x002E416B File Offset: 0x002E236B
		public override string LocalName
		{
			get
			{
				return "switch";
			}
		}

		// Token: 0x17004BEF RID: 19439
		// (get) Token: 0x060107DB RID: 67547 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BF0 RID: 19440
		// (get) Token: 0x060107DC RID: 67548 RVA: 0x002E4172 File Offset: 0x002E2372
		internal override int ElementTypeId
		{
			get
			{
				return 12769;
			}
		}

		// Token: 0x060107DD RID: 67549 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107DF RID: 67551 RVA: 0x002E4181 File Offset: 0x002E2381
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SwitchTransition>(deep);
		}

		// Token: 0x040074D7 RID: 29911
		private const string tagName = "switch";

		// Token: 0x040074D8 RID: 29912
		private const byte tagNsId = 49;

		// Token: 0x040074D9 RID: 29913
		internal const int ElementTypeIdConst = 12769;
	}
}
