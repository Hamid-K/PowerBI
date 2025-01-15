using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F34 RID: 12084
	[GeneratedCode("DomGen", "2.0")]
	internal class EntryMacro : MacroNameType
	{
		// Token: 0x17008F79 RID: 36729
		// (get) Token: 0x06019EC5 RID: 106181 RVA: 0x00359EC8 File Offset: 0x003580C8
		public override string LocalName
		{
			get
			{
				return "entryMacro";
			}
		}

		// Token: 0x17008F7A RID: 36730
		// (get) Token: 0x06019EC6 RID: 106182 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F7B RID: 36731
		// (get) Token: 0x06019EC7 RID: 106183 RVA: 0x00359ECF File Offset: 0x003580CF
		internal override int ElementTypeId
		{
			get
			{
				return 11729;
			}
		}

		// Token: 0x06019EC8 RID: 106184 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019ECA RID: 106186 RVA: 0x00359EDE File Offset: 0x003580DE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EntryMacro>(deep);
		}

		// Token: 0x0400AAE7 RID: 43751
		private const string tagName = "entryMacro";

		// Token: 0x0400AAE8 RID: 43752
		private const byte tagNsId = 23;

		// Token: 0x0400AAE9 RID: 43753
		internal const int ElementTypeIdConst = 11729;
	}
}
