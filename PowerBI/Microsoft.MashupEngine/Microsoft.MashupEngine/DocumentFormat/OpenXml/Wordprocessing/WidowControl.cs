using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D6D RID: 11629
	[GeneratedCode("DomGen", "2.0")]
	internal class WidowControl : OnOffType
	{
		// Token: 0x170086E3 RID: 34531
		// (get) Token: 0x06018C86 RID: 101510 RVA: 0x00344A25 File Offset: 0x00342C25
		public override string LocalName
		{
			get
			{
				return "widowControl";
			}
		}

		// Token: 0x170086E4 RID: 34532
		// (get) Token: 0x06018C87 RID: 101511 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086E5 RID: 34533
		// (get) Token: 0x06018C88 RID: 101512 RVA: 0x00344A2C File Offset: 0x00342C2C
		internal override int ElementTypeId
		{
			get
			{
				return 11497;
			}
		}

		// Token: 0x06018C89 RID: 101513 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C8B RID: 101515 RVA: 0x00344A33 File Offset: 0x00342C33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WidowControl>(deep);
		}

		// Token: 0x0400A4B2 RID: 42162
		private const string tagName = "widowControl";

		// Token: 0x0400A4B3 RID: 42163
		private const byte tagNsId = 23;

		// Token: 0x0400A4B4 RID: 42164
		internal const int ElementTypeIdConst = 11497;
	}
}
