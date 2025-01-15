using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002556 RID: 9558
	[GeneratedCode("DomGen", "2.0")]
	internal class MajorTickMark : TickMarkType
	{
		// Token: 0x17005578 RID: 21880
		// (get) Token: 0x06011D24 RID: 72996 RVA: 0x002F2D2F File Offset: 0x002F0F2F
		public override string LocalName
		{
			get
			{
				return "majorTickMark";
			}
		}

		// Token: 0x17005579 RID: 21881
		// (get) Token: 0x06011D25 RID: 72997 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700557A RID: 21882
		// (get) Token: 0x06011D26 RID: 72998 RVA: 0x002F2D36 File Offset: 0x002F0F36
		internal override int ElementTypeId
		{
			get
			{
				return 10380;
			}
		}

		// Token: 0x06011D27 RID: 72999 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D29 RID: 73001 RVA: 0x002F2D45 File Offset: 0x002F0F45
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MajorTickMark>(deep);
		}

		// Token: 0x04007CAF RID: 31919
		private const string tagName = "majorTickMark";

		// Token: 0x04007CB0 RID: 31920
		private const byte tagNsId = 11;

		// Token: 0x04007CB1 RID: 31921
		internal const int ElementTypeIdConst = 10380;
	}
}
