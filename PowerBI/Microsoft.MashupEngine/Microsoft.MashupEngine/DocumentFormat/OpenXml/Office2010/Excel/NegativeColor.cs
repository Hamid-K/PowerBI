using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023FE RID: 9214
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NegativeColor : ColorType
	{
		// Token: 0x17004EA2 RID: 20130
		// (get) Token: 0x06010DCD RID: 69069 RVA: 0x002E83DE File Offset: 0x002E65DE
		public override string LocalName
		{
			get
			{
				return "colorNegative";
			}
		}

		// Token: 0x17004EA3 RID: 20131
		// (get) Token: 0x06010DCE RID: 69070 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EA4 RID: 20132
		// (get) Token: 0x06010DCF RID: 69071 RVA: 0x002E83E5 File Offset: 0x002E65E5
		internal override int ElementTypeId
		{
			get
			{
				return 12938;
			}
		}

		// Token: 0x06010DD0 RID: 69072 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DD2 RID: 69074 RVA: 0x002E83EC File Offset: 0x002E65EC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NegativeColor>(deep);
		}

		// Token: 0x04007689 RID: 30345
		private const string tagName = "colorNegative";

		// Token: 0x0400768A RID: 30346
		private const byte tagNsId = 53;

		// Token: 0x0400768B RID: 30347
		internal const int ElementTypeIdConst = 12938;
	}
}
