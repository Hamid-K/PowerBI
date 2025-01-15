using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DFF RID: 11775
	[GeneratedCode("DomGen", "2.0")]
	internal class FootnoteLayoutLikeWord8 : OnOffType
	{
		// Token: 0x17008899 RID: 34969
		// (get) Token: 0x06018FF2 RID: 102386 RVA: 0x003456F6 File Offset: 0x003438F6
		public override string LocalName
		{
			get
			{
				return "footnoteLayoutLikeWW8";
			}
		}

		// Token: 0x1700889A RID: 34970
		// (get) Token: 0x06018FF3 RID: 102387 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700889B RID: 34971
		// (get) Token: 0x06018FF4 RID: 102388 RVA: 0x003456FD File Offset: 0x003438FD
		internal override int ElementTypeId
		{
			get
			{
				return 12085;
			}
		}

		// Token: 0x06018FF5 RID: 102389 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FF7 RID: 102391 RVA: 0x00345704 File Offset: 0x00343904
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnoteLayoutLikeWord8>(deep);
		}

		// Token: 0x0400A668 RID: 42600
		private const string tagName = "footnoteLayoutLikeWW8";

		// Token: 0x0400A669 RID: 42601
		private const byte tagNsId = 23;

		// Token: 0x0400A66A RID: 42602
		internal const int ElementTypeIdConst = 12085;
	}
}
