using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D73 RID: 11635
	[GeneratedCode("DomGen", "2.0")]
	internal class TopLinePunctuation : OnOffType
	{
		// Token: 0x170086F5 RID: 34549
		// (get) Token: 0x06018CAA RID: 101546 RVA: 0x00344AA8 File Offset: 0x00342CA8
		public override string LocalName
		{
			get
			{
				return "topLinePunct";
			}
		}

		// Token: 0x170086F6 RID: 34550
		// (get) Token: 0x06018CAB RID: 101547 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086F7 RID: 34551
		// (get) Token: 0x06018CAC RID: 101548 RVA: 0x00344AAF File Offset: 0x00342CAF
		internal override int ElementTypeId
		{
			get
			{
				return 11507;
			}
		}

		// Token: 0x06018CAD RID: 101549 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CAF RID: 101551 RVA: 0x00344AB6 File Offset: 0x00342CB6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopLinePunctuation>(deep);
		}

		// Token: 0x0400A4C4 RID: 42180
		private const string tagName = "topLinePunct";

		// Token: 0x0400A4C5 RID: 42181
		private const byte tagNsId = 23;

		// Token: 0x0400A4C6 RID: 42182
		internal const int ElementTypeIdConst = 11507;
	}
}
