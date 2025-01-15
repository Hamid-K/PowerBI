using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D82 RID: 11650
	[GeneratedCode("DomGen", "2.0")]
	internal class Italic : OnOffType
	{
		// Token: 0x17008722 RID: 34594
		// (get) Token: 0x06018D04 RID: 101636 RVA: 0x002EAA6B File Offset: 0x002E8C6B
		public override string LocalName
		{
			get
			{
				return "i";
			}
		}

		// Token: 0x17008723 RID: 34595
		// (get) Token: 0x06018D05 RID: 101637 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008724 RID: 34596
		// (get) Token: 0x06018D06 RID: 101638 RVA: 0x00344BFA File Offset: 0x00342DFA
		internal override int ElementTypeId
		{
			get
			{
				return 11579;
			}
		}

		// Token: 0x06018D07 RID: 101639 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D09 RID: 101641 RVA: 0x00344C01 File Offset: 0x00342E01
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Italic>(deep);
		}

		// Token: 0x0400A4F1 RID: 42225
		private const string tagName = "i";

		// Token: 0x0400A4F2 RID: 42226
		private const byte tagNsId = 23;

		// Token: 0x0400A4F3 RID: 42227
		internal const int ElementTypeIdConst = 11579;
	}
}
