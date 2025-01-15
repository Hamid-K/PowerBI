using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x0200239A RID: 9114
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class FlipTransition : LeftRightDirectionTransitionType
	{
		// Token: 0x17004BF1 RID: 19441
		// (get) Token: 0x060107E0 RID: 67552 RVA: 0x002E418A File Offset: 0x002E238A
		public override string LocalName
		{
			get
			{
				return "flip";
			}
		}

		// Token: 0x17004BF2 RID: 19442
		// (get) Token: 0x060107E1 RID: 67553 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BF3 RID: 19443
		// (get) Token: 0x060107E2 RID: 67554 RVA: 0x002E4191 File Offset: 0x002E2391
		internal override int ElementTypeId
		{
			get
			{
				return 12770;
			}
		}

		// Token: 0x060107E3 RID: 67555 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107E5 RID: 67557 RVA: 0x002E4198 File Offset: 0x002E2398
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FlipTransition>(deep);
		}

		// Token: 0x040074DA RID: 29914
		private const string tagName = "flip";

		// Token: 0x040074DB RID: 29915
		private const byte tagNsId = 49;

		// Token: 0x040074DC RID: 29916
		internal const int ElementTypeIdConst = 12770;
	}
}
