using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF0 RID: 12016
	[GeneratedCode("DomGen", "2.0")]
	internal class NoResizeAllowed : OnOffOnlyType
	{
		// Token: 0x17008D84 RID: 36228
		// (get) Token: 0x06019A58 RID: 105048 RVA: 0x003538F2 File Offset: 0x00351AF2
		public override string LocalName
		{
			get
			{
				return "noResizeAllowed";
			}
		}

		// Token: 0x17008D85 RID: 36229
		// (get) Token: 0x06019A59 RID: 105049 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D86 RID: 36230
		// (get) Token: 0x06019A5A RID: 105050 RVA: 0x003538F9 File Offset: 0x00351AF9
		internal override int ElementTypeId
		{
			get
			{
				return 11854;
			}
		}

		// Token: 0x06019A5B RID: 105051 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A5D RID: 105053 RVA: 0x00353900 File Offset: 0x00351B00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoResizeAllowed>(deep);
		}

		// Token: 0x0400A9DD RID: 43485
		private const string tagName = "noResizeAllowed";

		// Token: 0x0400A9DE RID: 43486
		private const byte tagNsId = 23;

		// Token: 0x0400A9DF RID: 43487
		internal const int ElementTypeIdConst = 11854;
	}
}
