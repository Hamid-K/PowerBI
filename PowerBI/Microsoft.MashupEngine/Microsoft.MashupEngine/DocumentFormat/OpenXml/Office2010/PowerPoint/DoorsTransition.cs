using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A4 RID: 9124
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DoorsTransition : OrientationTransitionType
	{
		// Token: 0x17004C14 RID: 19476
		// (get) Token: 0x06010828 RID: 67624 RVA: 0x002E43B3 File Offset: 0x002E25B3
		public override string LocalName
		{
			get
			{
				return "doors";
			}
		}

		// Token: 0x17004C15 RID: 19477
		// (get) Token: 0x06010829 RID: 67625 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C16 RID: 19478
		// (get) Token: 0x0601082A RID: 67626 RVA: 0x002E43BA File Offset: 0x002E25BA
		internal override int ElementTypeId
		{
			get
			{
				return 12774;
			}
		}

		// Token: 0x0601082B RID: 67627 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601082D RID: 67629 RVA: 0x002E43C9 File Offset: 0x002E25C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoorsTransition>(deep);
		}

		// Token: 0x040074F8 RID: 29944
		private const string tagName = "doors";

		// Token: 0x040074F9 RID: 29945
		private const byte tagNsId = 49;

		// Token: 0x040074FA RID: 29946
		internal const int ElementTypeIdConst = 12774;
	}
}
