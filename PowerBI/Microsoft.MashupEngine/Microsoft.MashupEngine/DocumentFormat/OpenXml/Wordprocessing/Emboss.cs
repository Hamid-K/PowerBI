using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D8A RID: 11658
	[GeneratedCode("DomGen", "2.0")]
	internal class Emboss : OnOffType
	{
		// Token: 0x1700873A RID: 34618
		// (get) Token: 0x06018D34 RID: 101684 RVA: 0x00344C96 File Offset: 0x00342E96
		public override string LocalName
		{
			get
			{
				return "emboss";
			}
		}

		// Token: 0x1700873B RID: 34619
		// (get) Token: 0x06018D35 RID: 101685 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700873C RID: 34620
		// (get) Token: 0x06018D36 RID: 101686 RVA: 0x00344C9D File Offset: 0x00342E9D
		internal override int ElementTypeId
		{
			get
			{
				return 11587;
			}
		}

		// Token: 0x06018D37 RID: 101687 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D39 RID: 101689 RVA: 0x00344CA4 File Offset: 0x00342EA4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Emboss>(deep);
		}

		// Token: 0x0400A509 RID: 42249
		private const string tagName = "emboss";

		// Token: 0x0400A50A RID: 42250
		private const byte tagNsId = 23;

		// Token: 0x0400A50B RID: 42251
		internal const int ElementTypeIdConst = 11587;
	}
}
