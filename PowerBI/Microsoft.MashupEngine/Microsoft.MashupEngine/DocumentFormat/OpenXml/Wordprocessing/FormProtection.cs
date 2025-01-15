using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D7C RID: 11644
	[GeneratedCode("DomGen", "2.0")]
	internal class FormProtection : OnOffType
	{
		// Token: 0x17008710 RID: 34576
		// (get) Token: 0x06018CE0 RID: 101600 RVA: 0x00344B77 File Offset: 0x00342D77
		public override string LocalName
		{
			get
			{
				return "formProt";
			}
		}

		// Token: 0x17008711 RID: 34577
		// (get) Token: 0x06018CE1 RID: 101601 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008712 RID: 34578
		// (get) Token: 0x06018CE2 RID: 101602 RVA: 0x00344B7E File Offset: 0x00342D7E
		internal override int ElementTypeId
		{
			get
			{
				return 11536;
			}
		}

		// Token: 0x06018CE3 RID: 101603 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CE5 RID: 101605 RVA: 0x00344B85 File Offset: 0x00342D85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormProtection>(deep);
		}

		// Token: 0x0400A4DF RID: 42207
		private const string tagName = "formProt";

		// Token: 0x0400A4E0 RID: 42208
		private const byte tagNsId = 23;

		// Token: 0x0400A4E1 RID: 42209
		internal const int ElementTypeIdConst = 11536;
	}
}
