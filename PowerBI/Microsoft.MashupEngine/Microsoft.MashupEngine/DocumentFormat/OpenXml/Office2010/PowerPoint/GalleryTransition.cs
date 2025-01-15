using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x0200239C RID: 9116
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class GalleryTransition : LeftRightDirectionTransitionType
	{
		// Token: 0x17004BF7 RID: 19447
		// (get) Token: 0x060107EC RID: 67564 RVA: 0x002C92B0 File Offset: 0x002C74B0
		public override string LocalName
		{
			get
			{
				return "gallery";
			}
		}

		// Token: 0x17004BF8 RID: 19448
		// (get) Token: 0x060107ED RID: 67565 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BF9 RID: 19449
		// (get) Token: 0x060107EE RID: 67566 RVA: 0x002E41B8 File Offset: 0x002E23B8
		internal override int ElementTypeId
		{
			get
			{
				return 12777;
			}
		}

		// Token: 0x060107EF RID: 67567 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060107F1 RID: 67569 RVA: 0x002E41BF File Offset: 0x002E23BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GalleryTransition>(deep);
		}

		// Token: 0x040074E0 RID: 29920
		private const string tagName = "gallery";

		// Token: 0x040074E1 RID: 29921
		private const byte tagNsId = 49;

		// Token: 0x040074E2 RID: 29922
		internal const int ElementTypeIdConst = 12777;
	}
}
