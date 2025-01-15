using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DFA RID: 11770
	[GeneratedCode("DomGen", "2.0")]
	internal class TruncateFontHeightsLikeWordPerfect : OnOffType
	{
		// Token: 0x1700888A RID: 34954
		// (get) Token: 0x06018FD4 RID: 102356 RVA: 0x00345683 File Offset: 0x00343883
		public override string LocalName
		{
			get
			{
				return "truncateFontHeightsLikeWP6";
			}
		}

		// Token: 0x1700888B RID: 34955
		// (get) Token: 0x06018FD5 RID: 102357 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700888C RID: 34956
		// (get) Token: 0x06018FD6 RID: 102358 RVA: 0x0034568A File Offset: 0x0034388A
		internal override int ElementTypeId
		{
			get
			{
				return 12080;
			}
		}

		// Token: 0x06018FD7 RID: 102359 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FD9 RID: 102361 RVA: 0x00345691 File Offset: 0x00343891
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TruncateFontHeightsLikeWordPerfect>(deep);
		}

		// Token: 0x0400A659 RID: 42585
		private const string tagName = "truncateFontHeightsLikeWP6";

		// Token: 0x0400A65A RID: 42586
		private const byte tagNsId = 23;

		// Token: 0x0400A65B RID: 42587
		internal const int ElementTypeIdConst = 12080;
	}
}
