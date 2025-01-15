using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200255B RID: 9563
	[GeneratedCode("DomGen", "2.0")]
	internal class CrossesAt : DoubleType
	{
		// Token: 0x1700558D RID: 21901
		// (get) Token: 0x06011D4F RID: 73039 RVA: 0x002F2ECF File Offset: 0x002F10CF
		public override string LocalName
		{
			get
			{
				return "crossesAt";
			}
		}

		// Token: 0x1700558E RID: 21902
		// (get) Token: 0x06011D50 RID: 73040 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700558F RID: 21903
		// (get) Token: 0x06011D51 RID: 73041 RVA: 0x002F2ED6 File Offset: 0x002F10D6
		internal override int ElementTypeId
		{
			get
			{
				return 10385;
			}
		}

		// Token: 0x06011D52 RID: 73042 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D54 RID: 73044 RVA: 0x002F2EE5 File Offset: 0x002F10E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CrossesAt>(deep);
		}

		// Token: 0x04007CC1 RID: 31937
		private const string tagName = "crossesAt";

		// Token: 0x04007CC2 RID: 31938
		private const byte tagNsId = 11;

		// Token: 0x04007CC3 RID: 31939
		internal const int ElementTypeIdConst = 10385;
	}
}
