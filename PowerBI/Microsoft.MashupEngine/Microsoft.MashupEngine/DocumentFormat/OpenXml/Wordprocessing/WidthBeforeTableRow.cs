using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EDC RID: 11996
	[GeneratedCode("DomGen", "2.0")]
	internal class WidthBeforeTableRow : TableWidthType
	{
		// Token: 0x17008D35 RID: 36149
		// (get) Token: 0x060199B7 RID: 104887 RVA: 0x00353404 File Offset: 0x00351604
		public override string LocalName
		{
			get
			{
				return "wBefore";
			}
		}

		// Token: 0x17008D36 RID: 36150
		// (get) Token: 0x060199B8 RID: 104888 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D37 RID: 36151
		// (get) Token: 0x060199B9 RID: 104889 RVA: 0x0035340B File Offset: 0x0035160B
		internal override int ElementTypeId
		{
			get
			{
				return 11663;
			}
		}

		// Token: 0x060199BA RID: 104890 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199BC RID: 104892 RVA: 0x00353412 File Offset: 0x00351612
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WidthBeforeTableRow>(deep);
		}

		// Token: 0x0400A99C RID: 43420
		private const string tagName = "wBefore";

		// Token: 0x0400A99D RID: 43421
		private const byte tagNsId = 23;

		// Token: 0x0400A99E RID: 43422
		internal const int ElementTypeIdConst = 11663;
	}
}
