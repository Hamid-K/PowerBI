using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D84 RID: 11652
	[GeneratedCode("DomGen", "2.0")]
	internal class Caps : OnOffType
	{
		// Token: 0x17008728 RID: 34600
		// (get) Token: 0x06018D10 RID: 101648 RVA: 0x00344C21 File Offset: 0x00342E21
		public override string LocalName
		{
			get
			{
				return "caps";
			}
		}

		// Token: 0x17008729 RID: 34601
		// (get) Token: 0x06018D11 RID: 101649 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700872A RID: 34602
		// (get) Token: 0x06018D12 RID: 101650 RVA: 0x00344C28 File Offset: 0x00342E28
		internal override int ElementTypeId
		{
			get
			{
				return 11581;
			}
		}

		// Token: 0x06018D13 RID: 101651 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D15 RID: 101653 RVA: 0x00344C2F File Offset: 0x00342E2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Caps>(deep);
		}

		// Token: 0x0400A4F7 RID: 42231
		private const string tagName = "caps";

		// Token: 0x0400A4F8 RID: 42232
		private const byte tagNsId = 23;

		// Token: 0x0400A4F9 RID: 42233
		internal const int ElementTypeIdConst = 11581;
	}
}
