using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x0200239D RID: 9117
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConveyorTransition : LeftRightDirectionTransitionType
	{
		// Token: 0x17004BFA RID: 19450
		// (get) Token: 0x060107F2 RID: 67570 RVA: 0x002E41C8 File Offset: 0x002E23C8
		public override string LocalName
		{
			get
			{
				return "conveyor";
			}
		}

		// Token: 0x17004BFB RID: 19451
		// (get) Token: 0x060107F3 RID: 67571 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BFC RID: 19452
		// (get) Token: 0x060107F4 RID: 67572 RVA: 0x002E41CF File Offset: 0x002E23CF
		internal override int ElementTypeId
		{
			get
			{
				return 12778;
			}
		}

		// Token: 0x060107F5 RID: 67573 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107F7 RID: 67575 RVA: 0x002E41D6 File Offset: 0x002E23D6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConveyorTransition>(deep);
		}

		// Token: 0x040074E3 RID: 29923
		private const string tagName = "conveyor";

		// Token: 0x040074E4 RID: 29924
		private const byte tagNsId = 49;

		// Token: 0x040074E5 RID: 29925
		internal const int ElementTypeIdConst = 12778;
	}
}
