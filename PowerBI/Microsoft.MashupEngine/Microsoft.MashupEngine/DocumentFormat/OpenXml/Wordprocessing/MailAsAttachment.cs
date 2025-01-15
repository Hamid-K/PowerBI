using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA1 RID: 11681
	[GeneratedCode("DomGen", "2.0")]
	internal class MailAsAttachment : OnOffType
	{
		// Token: 0x1700877F RID: 34687
		// (get) Token: 0x06018DBE RID: 101822 RVA: 0x00344E84 File Offset: 0x00343084
		public override string LocalName
		{
			get
			{
				return "mailAsAttachment";
			}
		}

		// Token: 0x17008780 RID: 34688
		// (get) Token: 0x06018DBF RID: 101823 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008781 RID: 34689
		// (get) Token: 0x06018DC0 RID: 101824 RVA: 0x00344E8B File Offset: 0x0034308B
		internal override int ElementTypeId
		{
			get
			{
				return 11823;
			}
		}

		// Token: 0x06018DC1 RID: 101825 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DC3 RID: 101827 RVA: 0x00344E92 File Offset: 0x00343092
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MailAsAttachment>(deep);
		}

		// Token: 0x0400A54E RID: 42318
		private const string tagName = "mailAsAttachment";

		// Token: 0x0400A54F RID: 42319
		private const byte tagNsId = 23;

		// Token: 0x0400A550 RID: 42320
		internal const int ElementTypeIdConst = 11823;
	}
}
