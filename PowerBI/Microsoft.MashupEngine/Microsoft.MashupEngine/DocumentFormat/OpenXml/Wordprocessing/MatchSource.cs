using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D99 RID: 11673
	[GeneratedCode("DomGen", "2.0")]
	internal class MatchSource : OnOffType
	{
		// Token: 0x17008767 RID: 34663
		// (get) Token: 0x06018D8E RID: 101774 RVA: 0x00344DD3 File Offset: 0x00342FD3
		public override string LocalName
		{
			get
			{
				return "matchSrc";
			}
		}

		// Token: 0x17008768 RID: 34664
		// (get) Token: 0x06018D8F RID: 101775 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008769 RID: 34665
		// (get) Token: 0x06018D90 RID: 101776 RVA: 0x00344DDA File Offset: 0x00342FDA
		internal override int ElementTypeId
		{
			get
			{
				return 11751;
			}
		}

		// Token: 0x06018D91 RID: 101777 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D93 RID: 101779 RVA: 0x00344DE1 File Offset: 0x00342FE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatchSource>(deep);
		}

		// Token: 0x0400A536 RID: 42294
		private const string tagName = "matchSrc";

		// Token: 0x0400A537 RID: 42295
		private const byte tagNsId = 23;

		// Token: 0x0400A538 RID: 42296
		internal const int ElementTypeIdConst = 11751;
	}
}
