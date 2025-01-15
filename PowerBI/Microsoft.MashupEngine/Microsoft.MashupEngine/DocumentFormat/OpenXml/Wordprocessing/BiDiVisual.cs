using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EEF RID: 12015
	[GeneratedCode("DomGen", "2.0")]
	internal class BiDiVisual : OnOffOnlyType
	{
		// Token: 0x17008D81 RID: 36225
		// (get) Token: 0x06019A52 RID: 105042 RVA: 0x003538DB File Offset: 0x00351ADB
		public override string LocalName
		{
			get
			{
				return "bidiVisual";
			}
		}

		// Token: 0x17008D82 RID: 36226
		// (get) Token: 0x06019A53 RID: 105043 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D83 RID: 36227
		// (get) Token: 0x06019A54 RID: 105044 RVA: 0x003538E2 File Offset: 0x00351AE2
		internal override int ElementTypeId
		{
			get
			{
				return 11674;
			}
		}

		// Token: 0x06019A55 RID: 105045 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A57 RID: 105047 RVA: 0x003538E9 File Offset: 0x00351AE9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BiDiVisual>(deep);
		}

		// Token: 0x0400A9DA RID: 43482
		private const string tagName = "bidiVisual";

		// Token: 0x0400A9DB RID: 43483
		private const byte tagNsId = 23;

		// Token: 0x0400A9DC RID: 43484
		internal const int ElementTypeIdConst = 11674;
	}
}
