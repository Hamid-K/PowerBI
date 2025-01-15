using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF9 RID: 11769
	[GeneratedCode("DomGen", "2.0")]
	internal class ConvertMailMergeEscape : OnOffType
	{
		// Token: 0x17008887 RID: 34951
		// (get) Token: 0x06018FCE RID: 102350 RVA: 0x0034566C File Offset: 0x0034386C
		public override string LocalName
		{
			get
			{
				return "convMailMergeEsc";
			}
		}

		// Token: 0x17008888 RID: 34952
		// (get) Token: 0x06018FCF RID: 102351 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008889 RID: 34953
		// (get) Token: 0x06018FD0 RID: 102352 RVA: 0x00345673 File Offset: 0x00343873
		internal override int ElementTypeId
		{
			get
			{
				return 12079;
			}
		}

		// Token: 0x06018FD1 RID: 102353 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FD3 RID: 102355 RVA: 0x0034567A File Offset: 0x0034387A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConvertMailMergeEscape>(deep);
		}

		// Token: 0x0400A656 RID: 42582
		private const string tagName = "convMailMergeEsc";

		// Token: 0x0400A657 RID: 42583
		private const byte tagNsId = 23;

		// Token: 0x0400A658 RID: 42584
		internal const int ElementTypeIdConst = 12079;
	}
}
