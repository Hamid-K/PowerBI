using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D98 RID: 11672
	[GeneratedCode("DomGen", "2.0")]
	internal class Checked : OnOffType
	{
		// Token: 0x17008764 RID: 34660
		// (get) Token: 0x06018D88 RID: 101768 RVA: 0x002EDF95 File Offset: 0x002EC195
		public override string LocalName
		{
			get
			{
				return "checked";
			}
		}

		// Token: 0x17008765 RID: 34661
		// (get) Token: 0x06018D89 RID: 101769 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008766 RID: 34662
		// (get) Token: 0x06018D8A RID: 101770 RVA: 0x00344DC3 File Offset: 0x00342FC3
		internal override int ElementTypeId
		{
			get
			{
				return 11739;
			}
		}

		// Token: 0x06018D8B RID: 101771 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D8D RID: 101773 RVA: 0x00344DCA File Offset: 0x00342FCA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Checked>(deep);
		}

		// Token: 0x0400A533 RID: 42291
		private const string tagName = "checked";

		// Token: 0x0400A534 RID: 42292
		private const byte tagNsId = 23;

		// Token: 0x0400A535 RID: 42293
		internal const int ElementTypeIdConst = 11739;
	}
}
