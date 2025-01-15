using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA9 RID: 11689
	[GeneratedCode("DomGen", "2.0")]
	internal class SaveSmartTagAsXml : OnOffType
	{
		// Token: 0x17008797 RID: 34711
		// (get) Token: 0x06018DEE RID: 101870 RVA: 0x00344F3C File Offset: 0x0034313C
		public override string LocalName
		{
			get
			{
				return "saveSmartTagsAsXml";
			}
		}

		// Token: 0x17008798 RID: 34712
		// (get) Token: 0x06018DEF RID: 101871 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008799 RID: 34713
		// (get) Token: 0x06018DF0 RID: 101872 RVA: 0x00344F43 File Offset: 0x00343143
		internal override int ElementTypeId
		{
			get
			{
				return 11847;
			}
		}

		// Token: 0x06018DF1 RID: 101873 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DF3 RID: 101875 RVA: 0x00344F4A File Offset: 0x0034314A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaveSmartTagAsXml>(deep);
		}

		// Token: 0x0400A566 RID: 42342
		private const string tagName = "saveSmartTagsAsXml";

		// Token: 0x0400A567 RID: 42343
		private const byte tagNsId = 23;

		// Token: 0x0400A568 RID: 42344
		internal const int ElementTypeIdConst = 11847;
	}
}
