using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x0200239B RID: 9115
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class FerrisTransition : LeftRightDirectionTransitionType
	{
		// Token: 0x17004BF4 RID: 19444
		// (get) Token: 0x060107E6 RID: 67558 RVA: 0x002E41A1 File Offset: 0x002E23A1
		public override string LocalName
		{
			get
			{
				return "ferris";
			}
		}

		// Token: 0x17004BF5 RID: 19445
		// (get) Token: 0x060107E7 RID: 67559 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BF6 RID: 19446
		// (get) Token: 0x060107E8 RID: 67560 RVA: 0x002E41A8 File Offset: 0x002E23A8
		internal override int ElementTypeId
		{
			get
			{
				return 12776;
			}
		}

		// Token: 0x060107E9 RID: 67561 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107EB RID: 67563 RVA: 0x002E41AF File Offset: 0x002E23AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FerrisTransition>(deep);
		}

		// Token: 0x040074DD RID: 29917
		private const string tagName = "ferris";

		// Token: 0x040074DE RID: 29918
		private const byte tagNsId = 49;

		// Token: 0x040074DF RID: 29919
		internal const int ElementTypeIdConst = 12776;
	}
}
