using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002409 RID: 9225
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NegativeBorderColor : ColorType
	{
		// Token: 0x17004EC3 RID: 20163
		// (get) Token: 0x06010E0F RID: 69135 RVA: 0x002E84DB File Offset: 0x002E66DB
		public override string LocalName
		{
			get
			{
				return "negativeBorderColor";
			}
		}

		// Token: 0x17004EC4 RID: 20164
		// (get) Token: 0x06010E10 RID: 69136 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EC5 RID: 20165
		// (get) Token: 0x06010E11 RID: 69137 RVA: 0x002E84E2 File Offset: 0x002E66E2
		internal override int ElementTypeId
		{
			get
			{
				return 12970;
			}
		}

		// Token: 0x06010E12 RID: 69138 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E14 RID: 69140 RVA: 0x002E84E9 File Offset: 0x002E66E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NegativeBorderColor>(deep);
		}

		// Token: 0x040076AA RID: 30378
		private const string tagName = "negativeBorderColor";

		// Token: 0x040076AB RID: 30379
		private const byte tagNsId = 53;

		// Token: 0x040076AC RID: 30380
		internal const int ElementTypeIdConst = 12970;
	}
}
