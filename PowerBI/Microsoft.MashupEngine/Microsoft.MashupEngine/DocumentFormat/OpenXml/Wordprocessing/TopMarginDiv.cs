using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FBF RID: 12223
	[GeneratedCode("DomGen", "2.0")]
	internal class TopMarginDiv : SignedTwipsMeasureType
	{
		// Token: 0x170093D4 RID: 37844
		// (get) Token: 0x0601A80F RID: 108559 RVA: 0x003632BA File Offset: 0x003614BA
		public override string LocalName
		{
			get
			{
				return "marTop";
			}
		}

		// Token: 0x170093D5 RID: 37845
		// (get) Token: 0x0601A810 RID: 108560 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093D6 RID: 37846
		// (get) Token: 0x0601A811 RID: 108561 RVA: 0x003632C1 File Offset: 0x003614C1
		internal override int ElementTypeId
		{
			get
			{
				return 11931;
			}
		}

		// Token: 0x0601A812 RID: 108562 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A814 RID: 108564 RVA: 0x003632C8 File Offset: 0x003614C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopMarginDiv>(deep);
		}

		// Token: 0x0400AD40 RID: 44352
		private const string tagName = "marTop";

		// Token: 0x0400AD41 RID: 44353
		private const byte tagNsId = 23;

		// Token: 0x0400AD42 RID: 44354
		internal const int ElementTypeIdConst = 11931;
	}
}
