using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DEF RID: 11759
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintColorBlackWhite : OnOffType
	{
		// Token: 0x17008869 RID: 34921
		// (get) Token: 0x06018F92 RID: 102290 RVA: 0x00345586 File Offset: 0x00343786
		public override string LocalName
		{
			get
			{
				return "printColBlack";
			}
		}

		// Token: 0x1700886A RID: 34922
		// (get) Token: 0x06018F93 RID: 102291 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700886B RID: 34923
		// (get) Token: 0x06018F94 RID: 102292 RVA: 0x0034558D File Offset: 0x0034378D
		internal override int ElementTypeId
		{
			get
			{
				return 12069;
			}
		}

		// Token: 0x06018F95 RID: 102293 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F97 RID: 102295 RVA: 0x00345594 File Offset: 0x00343794
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintColorBlackWhite>(deep);
		}

		// Token: 0x0400A638 RID: 42552
		private const string tagName = "printColBlack";

		// Token: 0x0400A639 RID: 42553
		private const byte tagNsId = 23;

		// Token: 0x0400A63A RID: 42554
		internal const int ElementTypeIdConst = 12069;
	}
}
