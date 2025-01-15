using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB2 RID: 11698
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintFractionalCharacterWidth : OnOffType
	{
		// Token: 0x170087B2 RID: 34738
		// (get) Token: 0x06018E24 RID: 101924 RVA: 0x0034500B File Offset: 0x0034320B
		public override string LocalName
		{
			get
			{
				return "printFractionalCharacterWidth";
			}
		}

		// Token: 0x170087B3 RID: 34739
		// (get) Token: 0x06018E25 RID: 101925 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087B4 RID: 34740
		// (get) Token: 0x06018E26 RID: 101926 RVA: 0x00345012 File Offset: 0x00343212
		internal override int ElementTypeId
		{
			get
			{
				return 11966;
			}
		}

		// Token: 0x06018E27 RID: 101927 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E29 RID: 101929 RVA: 0x00345019 File Offset: 0x00343219
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintFractionalCharacterWidth>(deep);
		}

		// Token: 0x0400A581 RID: 42369
		private const string tagName = "printFractionalCharacterWidth";

		// Token: 0x0400A582 RID: 42370
		private const byte tagNsId = 23;

		// Token: 0x0400A583 RID: 42371
		internal const int ElementTypeIdConst = 11966;
	}
}
