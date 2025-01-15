using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF6 RID: 12022
	[GeneratedCode("DomGen", "2.0")]
	internal class SemiHidden : OnOffOnlyType
	{
		// Token: 0x17008D96 RID: 36246
		// (get) Token: 0x06019A7C RID: 105084 RVA: 0x00353975 File Offset: 0x00351B75
		public override string LocalName
		{
			get
			{
				return "semiHidden";
			}
		}

		// Token: 0x17008D97 RID: 36247
		// (get) Token: 0x06019A7D RID: 105085 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D98 RID: 36248
		// (get) Token: 0x06019A7E RID: 105086 RVA: 0x0035397C File Offset: 0x00351B7C
		internal override int ElementTypeId
		{
			get
			{
				return 11900;
			}
		}

		// Token: 0x06019A7F RID: 105087 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A81 RID: 105089 RVA: 0x00353983 File Offset: 0x00351B83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SemiHidden>(deep);
		}

		// Token: 0x0400A9EF RID: 43503
		private const string tagName = "semiHidden";

		// Token: 0x0400A9F0 RID: 43504
		private const byte tagNsId = 23;

		// Token: 0x0400A9F1 RID: 43505
		internal const int ElementTypeIdConst = 11900;
	}
}
