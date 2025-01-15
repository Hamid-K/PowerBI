using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D4F RID: 11599
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentReference : MarkupType
	{
		// Token: 0x17008689 RID: 34441
		// (get) Token: 0x06018BD0 RID: 101328 RVA: 0x003446F0 File Offset: 0x003428F0
		public override string LocalName
		{
			get
			{
				return "commentReference";
			}
		}

		// Token: 0x1700868A RID: 34442
		// (get) Token: 0x06018BD1 RID: 101329 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700868B RID: 34443
		// (get) Token: 0x06018BD2 RID: 101330 RVA: 0x003446F7 File Offset: 0x003428F7
		internal override int ElementTypeId
		{
			get
			{
				return 11571;
			}
		}

		// Token: 0x06018BD3 RID: 101331 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BD5 RID: 101333 RVA: 0x003446FE File Offset: 0x003428FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentReference>(deep);
		}

		// Token: 0x0400A45A RID: 42074
		private const string tagName = "commentReference";

		// Token: 0x0400A45B RID: 42075
		private const byte tagNsId = 23;

		// Token: 0x0400A45C RID: 42076
		internal const int ElementTypeIdConst = 11571;
	}
}
