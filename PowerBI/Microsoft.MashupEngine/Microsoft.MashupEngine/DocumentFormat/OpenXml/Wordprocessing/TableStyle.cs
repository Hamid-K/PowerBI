using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E82 RID: 11906
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyle : String253Type
	{
		// Token: 0x17008AEF RID: 35567
		// (get) Token: 0x060194CE RID: 103630 RVA: 0x0030DEEC File Offset: 0x0030C0EC
		public override string LocalName
		{
			get
			{
				return "tblStyle";
			}
		}

		// Token: 0x17008AF0 RID: 35568
		// (get) Token: 0x060194CF RID: 103631 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AF1 RID: 35569
		// (get) Token: 0x060194D0 RID: 103632 RVA: 0x0034864F File Offset: 0x0034684F
		internal override int ElementTypeId
		{
			get
			{
				return 11671;
			}
		}

		// Token: 0x060194D1 RID: 103633 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194D3 RID: 103635 RVA: 0x00348656 File Offset: 0x00346856
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyle>(deep);
		}

		// Token: 0x0400A82C RID: 43052
		private const string tagName = "tblStyle";

		// Token: 0x0400A82D RID: 43053
		private const byte tagNsId = 23;

		// Token: 0x0400A82E RID: 43054
		internal const int ElementTypeIdConst = 11671;
	}
}
