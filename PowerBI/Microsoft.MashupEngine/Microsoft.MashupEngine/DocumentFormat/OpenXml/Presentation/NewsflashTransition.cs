using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029EA RID: 10730
	[GeneratedCode("DomGen", "2.0")]
	internal class NewsflashTransition : EmptyType
	{
		// Token: 0x17006E57 RID: 28247
		// (get) Token: 0x0601559D RID: 87453 RVA: 0x0031E21C File Offset: 0x0031C41C
		public override string LocalName
		{
			get
			{
				return "newsflash";
			}
		}

		// Token: 0x17006E58 RID: 28248
		// (get) Token: 0x0601559E RID: 87454 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E59 RID: 28249
		// (get) Token: 0x0601559F RID: 87455 RVA: 0x0031E223 File Offset: 0x0031C423
		internal override int ElementTypeId
		{
			get
			{
				return 12384;
			}
		}

		// Token: 0x060155A0 RID: 87456 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155A2 RID: 87458 RVA: 0x0031E22A File Offset: 0x0031C42A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NewsflashTransition>(deep);
		}

		// Token: 0x04009318 RID: 37656
		private const string tagName = "newsflash";

		// Token: 0x04009319 RID: 37657
		private const byte tagNsId = 24;

		// Token: 0x0400931A RID: 37658
		internal const int ElementTypeIdConst = 12384;
	}
}
