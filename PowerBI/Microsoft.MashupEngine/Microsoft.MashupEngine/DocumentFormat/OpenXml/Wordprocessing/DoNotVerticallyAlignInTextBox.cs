using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E1F RID: 11807
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotVerticallyAlignInTextBox : OnOffType
	{
		// Token: 0x170088F9 RID: 35065
		// (get) Token: 0x060190B2 RID: 102578 RVA: 0x003459D6 File Offset: 0x00343BD6
		public override string LocalName
		{
			get
			{
				return "doNotVertAlignInTxbx";
			}
		}

		// Token: 0x170088FA RID: 35066
		// (get) Token: 0x060190B3 RID: 102579 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088FB RID: 35067
		// (get) Token: 0x060190B4 RID: 102580 RVA: 0x003459DD File Offset: 0x00343BDD
		internal override int ElementTypeId
		{
			get
			{
				return 12117;
			}
		}

		// Token: 0x060190B5 RID: 102581 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190B7 RID: 102583 RVA: 0x003459E4 File Offset: 0x00343BE4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotVerticallyAlignInTextBox>(deep);
		}

		// Token: 0x0400A6C8 RID: 42696
		private const string tagName = "doNotVertAlignInTxbx";

		// Token: 0x0400A6C9 RID: 42697
		private const byte tagNsId = 23;

		// Token: 0x0400A6CA RID: 42698
		internal const int ElementTypeIdConst = 12117;
	}
}
