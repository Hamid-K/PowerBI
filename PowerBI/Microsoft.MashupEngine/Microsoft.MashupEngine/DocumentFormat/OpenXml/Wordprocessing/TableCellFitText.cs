using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EEB RID: 12011
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellFitText : OnOffOnlyType
	{
		// Token: 0x17008D75 RID: 36213
		// (get) Token: 0x06019A3A RID: 105018 RVA: 0x0035387F File Offset: 0x00351A7F
		public override string LocalName
		{
			get
			{
				return "tcFitText";
			}
		}

		// Token: 0x17008D76 RID: 36214
		// (get) Token: 0x06019A3B RID: 105019 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D77 RID: 36215
		// (get) Token: 0x06019A3C RID: 105020 RVA: 0x00353886 File Offset: 0x00351A86
		internal override int ElementTypeId
		{
			get
			{
				return 11657;
			}
		}

		// Token: 0x06019A3D RID: 105021 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A3F RID: 105023 RVA: 0x0035388D File Offset: 0x00351A8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellFitText>(deep);
		}

		// Token: 0x0400A9CE RID: 43470
		private const string tagName = "tcFitText";

		// Token: 0x0400A9CF RID: 43471
		private const byte tagNsId = 23;

		// Token: 0x0400A9D0 RID: 43472
		internal const int ElementTypeIdConst = 11657;
	}
}
