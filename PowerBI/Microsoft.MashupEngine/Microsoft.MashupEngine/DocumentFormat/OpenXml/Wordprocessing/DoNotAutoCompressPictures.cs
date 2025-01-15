using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DDF RID: 11743
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotAutoCompressPictures : OnOffType
	{
		// Token: 0x17008839 RID: 34873
		// (get) Token: 0x06018F32 RID: 102194 RVA: 0x00345416 File Offset: 0x00343616
		public override string LocalName
		{
			get
			{
				return "doNotAutoCompressPictures";
			}
		}

		// Token: 0x1700883A RID: 34874
		// (get) Token: 0x06018F33 RID: 102195 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700883B RID: 34875
		// (get) Token: 0x06018F34 RID: 102196 RVA: 0x0034541D File Offset: 0x0034361D
		internal override int ElementTypeId
		{
			get
			{
				return 12046;
			}
		}

		// Token: 0x06018F35 RID: 102197 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F37 RID: 102199 RVA: 0x00345424 File Offset: 0x00343624
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotAutoCompressPictures>(deep);
		}

		// Token: 0x0400A608 RID: 42504
		private const string tagName = "doNotAutoCompressPictures";

		// Token: 0x0400A609 RID: 42505
		private const byte tagNsId = 23;

		// Token: 0x0400A60A RID: 42506
		internal const int ElementTypeIdConst = 12046;
	}
}
