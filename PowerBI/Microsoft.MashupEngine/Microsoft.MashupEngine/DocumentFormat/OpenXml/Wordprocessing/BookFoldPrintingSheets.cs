using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE6 RID: 12262
	[GeneratedCode("DomGen", "2.0")]
	internal class BookFoldPrintingSheets : NonNegativeShortType
	{
		// Token: 0x1700950C RID: 38156
		// (get) Token: 0x0601AAB6 RID: 109238 RVA: 0x00365C4B File Offset: 0x00363E4B
		public override string LocalName
		{
			get
			{
				return "bookFoldPrintingSheets";
			}
		}

		// Token: 0x1700950D RID: 38157
		// (get) Token: 0x0601AAB7 RID: 109239 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700950E RID: 38158
		// (get) Token: 0x0601AAB8 RID: 109240 RVA: 0x00365C52 File Offset: 0x00363E52
		internal override int ElementTypeId
		{
			get
			{
				return 12008;
			}
		}

		// Token: 0x0601AAB9 RID: 109241 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AABB RID: 109243 RVA: 0x00365C59 File Offset: 0x00363E59
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookFoldPrintingSheets>(deep);
		}

		// Token: 0x0400ADE8 RID: 44520
		private const string tagName = "bookFoldPrintingSheets";

		// Token: 0x0400ADE9 RID: 44521
		private const byte tagNsId = 23;

		// Token: 0x0400ADEA RID: 44522
		internal const int ElementTypeIdConst = 12008;
	}
}
