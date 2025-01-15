using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF7 RID: 12023
	[GeneratedCode("DomGen", "2.0")]
	internal class UnhideWhenUsed : OnOffOnlyType
	{
		// Token: 0x17008D99 RID: 36249
		// (get) Token: 0x06019A82 RID: 105090 RVA: 0x0035398C File Offset: 0x00351B8C
		public override string LocalName
		{
			get
			{
				return "unhideWhenUsed";
			}
		}

		// Token: 0x17008D9A RID: 36250
		// (get) Token: 0x06019A83 RID: 105091 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D9B RID: 36251
		// (get) Token: 0x06019A84 RID: 105092 RVA: 0x00353993 File Offset: 0x00351B93
		internal override int ElementTypeId
		{
			get
			{
				return 11901;
			}
		}

		// Token: 0x06019A85 RID: 105093 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A87 RID: 105095 RVA: 0x0035399A File Offset: 0x00351B9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnhideWhenUsed>(deep);
		}

		// Token: 0x0400A9F2 RID: 43506
		private const string tagName = "unhideWhenUsed";

		// Token: 0x0400A9F3 RID: 43507
		private const byte tagNsId = 23;

		// Token: 0x0400A9F4 RID: 43508
		internal const int ElementTypeIdConst = 11901;
	}
}
