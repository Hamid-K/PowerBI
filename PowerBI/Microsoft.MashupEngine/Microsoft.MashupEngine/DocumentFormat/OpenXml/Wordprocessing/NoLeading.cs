using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE4 RID: 11748
	[GeneratedCode("DomGen", "2.0")]
	internal class NoLeading : OnOffType
	{
		// Token: 0x17008848 RID: 34888
		// (get) Token: 0x06018F50 RID: 102224 RVA: 0x00345489 File Offset: 0x00343689
		public override string LocalName
		{
			get
			{
				return "noLeading";
			}
		}

		// Token: 0x17008849 RID: 34889
		// (get) Token: 0x06018F51 RID: 102225 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700884A RID: 34890
		// (get) Token: 0x06018F52 RID: 102226 RVA: 0x00345490 File Offset: 0x00343690
		internal override int ElementTypeId
		{
			get
			{
				return 12058;
			}
		}

		// Token: 0x06018F53 RID: 102227 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F55 RID: 102229 RVA: 0x00345497 File Offset: 0x00343697
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoLeading>(deep);
		}

		// Token: 0x0400A617 RID: 42519
		private const string tagName = "noLeading";

		// Token: 0x0400A618 RID: 42520
		private const byte tagNsId = 23;

		// Token: 0x0400A619 RID: 42521
		internal const int ElementTypeIdConst = 12058;
	}
}
