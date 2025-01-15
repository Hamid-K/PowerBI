using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002396 RID: 9110
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class VortexTransition : SideDirectionTransitionType
	{
		// Token: 0x17004BE5 RID: 19429
		// (get) Token: 0x060107C7 RID: 67527 RVA: 0x002E40C7 File Offset: 0x002E22C7
		public override string LocalName
		{
			get
			{
				return "vortex";
			}
		}

		// Token: 0x17004BE6 RID: 19430
		// (get) Token: 0x060107C8 RID: 67528 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BE7 RID: 19431
		// (get) Token: 0x060107C9 RID: 67529 RVA: 0x002E40CE File Offset: 0x002E22CE
		internal override int ElementTypeId
		{
			get
			{
				return 12768;
			}
		}

		// Token: 0x060107CA RID: 67530 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107CC RID: 67532 RVA: 0x002E40DD File Offset: 0x002E22DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VortexTransition>(deep);
		}

		// Token: 0x040074CF RID: 29903
		private const string tagName = "vortex";

		// Token: 0x040074D0 RID: 29904
		private const byte tagNsId = 49;

		// Token: 0x040074D1 RID: 29905
		internal const int ElementTypeIdConst = 12768;
	}
}
