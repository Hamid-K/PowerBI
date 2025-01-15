using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B7 RID: 9143
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ModificationId : RandomIdType
	{
		// Token: 0x17004C7C RID: 19580
		// (get) Token: 0x06010907 RID: 67847 RVA: 0x002E4C1E File Offset: 0x002E2E1E
		public override string LocalName
		{
			get
			{
				return "modId";
			}
		}

		// Token: 0x17004C7D RID: 19581
		// (get) Token: 0x06010908 RID: 67848 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C7E RID: 19582
		// (get) Token: 0x06010909 RID: 67849 RVA: 0x002E4C25 File Offset: 0x002E2E25
		internal override int ElementTypeId
		{
			get
			{
				return 12797;
			}
		}

		// Token: 0x0601090A RID: 67850 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601090C RID: 67852 RVA: 0x002E4C2C File Offset: 0x002E2E2C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ModificationId>(deep);
		}

		// Token: 0x04007548 RID: 30024
		private const string tagName = "modId";

		// Token: 0x04007549 RID: 30025
		private const byte tagNsId = 49;

		// Token: 0x0400754A RID: 30026
		internal const int ElementTypeIdConst = 12797;
	}
}
