using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F9 RID: 9721
	[GeneratedCode("DomGen", "2.0")]
	internal class MajorTimeUnit : TimeUnitType
	{
		// Token: 0x1700596A RID: 22890
		// (get) Token: 0x060125E5 RID: 75237 RVA: 0x002FA43E File Offset: 0x002F863E
		public override string LocalName
		{
			get
			{
				return "majorTimeUnit";
			}
		}

		// Token: 0x1700596B RID: 22891
		// (get) Token: 0x060125E6 RID: 75238 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700596C RID: 22892
		// (get) Token: 0x060125E7 RID: 75239 RVA: 0x002FA445 File Offset: 0x002F8645
		internal override int ElementTypeId
		{
			get
			{
				return 10566;
			}
		}

		// Token: 0x060125E8 RID: 75240 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060125EA RID: 75242 RVA: 0x002FA44C File Offset: 0x002F864C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MajorTimeUnit>(deep);
		}

		// Token: 0x04007F45 RID: 32581
		private const string tagName = "majorTimeUnit";

		// Token: 0x04007F46 RID: 32582
		private const byte tagNsId = 11;

		// Token: 0x04007F47 RID: 32583
		internal const int ElementTypeIdConst = 10566;
	}
}
