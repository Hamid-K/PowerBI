using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D7E RID: 11646
	[GeneratedCode("DomGen", "2.0")]
	internal class TitlePage : OnOffType
	{
		// Token: 0x17008716 RID: 34582
		// (get) Token: 0x06018CEC RID: 101612 RVA: 0x00344BA5 File Offset: 0x00342DA5
		public override string LocalName
		{
			get
			{
				return "titlePg";
			}
		}

		// Token: 0x17008717 RID: 34583
		// (get) Token: 0x06018CED RID: 101613 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008718 RID: 34584
		// (get) Token: 0x06018CEE RID: 101614 RVA: 0x00344BAC File Offset: 0x00342DAC
		internal override int ElementTypeId
		{
			get
			{
				return 11539;
			}
		}

		// Token: 0x06018CEF RID: 101615 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CF1 RID: 101617 RVA: 0x00344BB3 File Offset: 0x00342DB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TitlePage>(deep);
		}

		// Token: 0x0400A4E5 RID: 42213
		private const string tagName = "titlePg";

		// Token: 0x0400A4E6 RID: 42214
		private const byte tagNsId = 23;

		// Token: 0x0400A4E7 RID: 42215
		internal const int ElementTypeIdConst = 11539;
	}
}
