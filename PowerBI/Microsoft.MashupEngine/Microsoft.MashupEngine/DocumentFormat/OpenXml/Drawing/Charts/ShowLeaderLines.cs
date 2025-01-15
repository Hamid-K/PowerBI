using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002510 RID: 9488
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowLeaderLines : BooleanType
	{
		// Token: 0x17005449 RID: 21577
		// (get) Token: 0x06011A8D RID: 72333 RVA: 0x002F1299 File Offset: 0x002EF499
		public override string LocalName
		{
			get
			{
				return "showLeaderLines";
			}
		}

		// Token: 0x1700544A RID: 21578
		// (get) Token: 0x06011A8E RID: 72334 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700544B RID: 21579
		// (get) Token: 0x06011A8F RID: 72335 RVA: 0x002F12A0 File Offset: 0x002EF4A0
		internal override int ElementTypeId
		{
			get
			{
				return 10355;
			}
		}

		// Token: 0x06011A90 RID: 72336 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A92 RID: 72338 RVA: 0x002F12A7 File Offset: 0x002EF4A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowLeaderLines>(deep);
		}

		// Token: 0x04007BC4 RID: 31684
		private const string tagName = "showLeaderLines";

		// Token: 0x04007BC5 RID: 31685
		private const byte tagNsId = 11;

		// Token: 0x04007BC6 RID: 31686
		internal const int ElementTypeIdConst = 10355;
	}
}
