using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DFD RID: 11773
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotSuppressParagraphBorders : OnOffType
	{
		// Token: 0x17008893 RID: 34963
		// (get) Token: 0x06018FE6 RID: 102374 RVA: 0x003456C8 File Offset: 0x003438C8
		public override string LocalName
		{
			get
			{
				return "doNotSuppressParagraphBorders";
			}
		}

		// Token: 0x17008894 RID: 34964
		// (get) Token: 0x06018FE7 RID: 102375 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008895 RID: 34965
		// (get) Token: 0x06018FE8 RID: 102376 RVA: 0x003456CF File Offset: 0x003438CF
		internal override int ElementTypeId
		{
			get
			{
				return 12083;
			}
		}

		// Token: 0x06018FE9 RID: 102377 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FEB RID: 102379 RVA: 0x003456D6 File Offset: 0x003438D6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotSuppressParagraphBorders>(deep);
		}

		// Token: 0x0400A662 RID: 42594
		private const string tagName = "doNotSuppressParagraphBorders";

		// Token: 0x0400A663 RID: 42595
		private const byte tagNsId = 23;

		// Token: 0x0400A664 RID: 42596
		internal const int ElementTypeIdConst = 12083;
	}
}
