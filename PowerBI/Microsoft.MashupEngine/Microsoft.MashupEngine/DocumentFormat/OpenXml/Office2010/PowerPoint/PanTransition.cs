using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002397 RID: 9111
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PanTransition : SideDirectionTransitionType
	{
		// Token: 0x17004BE8 RID: 19432
		// (get) Token: 0x060107CD RID: 67533 RVA: 0x002E40E6 File Offset: 0x002E22E6
		public override string LocalName
		{
			get
			{
				return "pan";
			}
		}

		// Token: 0x17004BE9 RID: 19433
		// (get) Token: 0x060107CE RID: 67534 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BEA RID: 19434
		// (get) Token: 0x060107CF RID: 67535 RVA: 0x002E40ED File Offset: 0x002E22ED
		internal override int ElementTypeId
		{
			get
			{
				return 12779;
			}
		}

		// Token: 0x060107D0 RID: 67536 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107D2 RID: 67538 RVA: 0x002E40F4 File Offset: 0x002E22F4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PanTransition>(deep);
		}

		// Token: 0x040074D2 RID: 29906
		private const string tagName = "pan";

		// Token: 0x040074D3 RID: 29907
		private const byte tagNsId = 49;

		// Token: 0x040074D4 RID: 29908
		internal const int ElementTypeIdConst = 12779;
	}
}
