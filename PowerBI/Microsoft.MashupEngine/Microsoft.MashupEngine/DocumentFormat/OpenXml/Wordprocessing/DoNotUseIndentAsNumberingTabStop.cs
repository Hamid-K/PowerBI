using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E14 RID: 11796
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotUseIndentAsNumberingTabStop : OnOffType
	{
		// Token: 0x170088D8 RID: 35032
		// (get) Token: 0x06019070 RID: 102512 RVA: 0x003458D9 File Offset: 0x00343AD9
		public override string LocalName
		{
			get
			{
				return "doNotUseIndentAsNumberingTabStop";
			}
		}

		// Token: 0x170088D9 RID: 35033
		// (get) Token: 0x06019071 RID: 102513 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088DA RID: 35034
		// (get) Token: 0x06019072 RID: 102514 RVA: 0x003458E0 File Offset: 0x00343AE0
		internal override int ElementTypeId
		{
			get
			{
				return 12106;
			}
		}

		// Token: 0x06019073 RID: 102515 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019075 RID: 102517 RVA: 0x003458E7 File Offset: 0x00343AE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotUseIndentAsNumberingTabStop>(deep);
		}

		// Token: 0x0400A6A7 RID: 42663
		private const string tagName = "doNotUseIndentAsNumberingTabStop";

		// Token: 0x0400A6A8 RID: 42664
		private const byte tagNsId = 23;

		// Token: 0x0400A6A9 RID: 42665
		internal const int ElementTypeIdConst = 12106;
	}
}
