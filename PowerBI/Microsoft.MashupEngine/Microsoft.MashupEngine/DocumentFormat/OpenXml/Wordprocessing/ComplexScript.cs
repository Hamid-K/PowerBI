using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D90 RID: 11664
	[GeneratedCode("DomGen", "2.0")]
	internal class ComplexScript : OnOffType
	{
		// Token: 0x1700874C RID: 34636
		// (get) Token: 0x06018D58 RID: 101720 RVA: 0x00306574 File Offset: 0x00304774
		public override string LocalName
		{
			get
			{
				return "cs";
			}
		}

		// Token: 0x1700874D RID: 34637
		// (get) Token: 0x06018D59 RID: 101721 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700874E RID: 34638
		// (get) Token: 0x06018D5A RID: 101722 RVA: 0x00344D19 File Offset: 0x00342F19
		internal override int ElementTypeId
		{
			get
			{
				return 11606;
			}
		}

		// Token: 0x06018D5B RID: 101723 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D5D RID: 101725 RVA: 0x00344D20 File Offset: 0x00342F20
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ComplexScript>(deep);
		}

		// Token: 0x0400A51B RID: 42267
		private const string tagName = "cs";

		// Token: 0x0400A51C RID: 42268
		private const byte tagNsId = 23;

		// Token: 0x0400A51D RID: 42269
		internal const int ElementTypeIdConst = 11606;
	}
}
