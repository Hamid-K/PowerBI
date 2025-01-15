using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D74 RID: 11636
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoSpaceDE : OnOffType
	{
		// Token: 0x170086F8 RID: 34552
		// (get) Token: 0x06018CB0 RID: 101552 RVA: 0x00344ABF File Offset: 0x00342CBF
		public override string LocalName
		{
			get
			{
				return "autoSpaceDE";
			}
		}

		// Token: 0x170086F9 RID: 34553
		// (get) Token: 0x06018CB1 RID: 101553 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086FA RID: 34554
		// (get) Token: 0x06018CB2 RID: 101554 RVA: 0x00344AC6 File Offset: 0x00342CC6
		internal override int ElementTypeId
		{
			get
			{
				return 11508;
			}
		}

		// Token: 0x06018CB3 RID: 101555 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CB5 RID: 101557 RVA: 0x00344ACD File Offset: 0x00342CCD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoSpaceDE>(deep);
		}

		// Token: 0x0400A4C7 RID: 42183
		private const string tagName = "autoSpaceDE";

		// Token: 0x0400A4C8 RID: 42184
		private const byte tagNsId = 23;

		// Token: 0x0400A4C9 RID: 42185
		internal const int ElementTypeIdConst = 11508;
	}
}
