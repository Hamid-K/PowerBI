using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200274C RID: 10060
	[GeneratedCode("DomGen", "2.0")]
	internal class LatinFont : TextFontType
	{
		// Token: 0x1700608B RID: 24715
		// (get) Token: 0x060135C7 RID: 79303 RVA: 0x00306546 File Offset: 0x00304746
		public override string LocalName
		{
			get
			{
				return "latin";
			}
		}

		// Token: 0x1700608C RID: 24716
		// (get) Token: 0x060135C8 RID: 79304 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700608D RID: 24717
		// (get) Token: 0x060135C9 RID: 79305 RVA: 0x0030654D File Offset: 0x0030474D
		internal override int ElementTypeId
		{
			get
			{
				return 10131;
			}
		}

		// Token: 0x060135CA RID: 79306 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135CC RID: 79308 RVA: 0x00306554 File Offset: 0x00304754
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LatinFont>(deep);
		}

		// Token: 0x040085D5 RID: 34261
		private const string tagName = "latin";

		// Token: 0x040085D6 RID: 34262
		private const byte tagNsId = 10;

		// Token: 0x040085D7 RID: 34263
		internal const int ElementTypeIdConst = 10131;
	}
}
