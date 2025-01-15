using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A57 RID: 10839
	[GeneratedCode("DomGen", "2.0")]
	internal class BoldFont : EmbeddedFontDataIdType
	{
		// Token: 0x1700720B RID: 29195
		// (get) Token: 0x06015DCE RID: 89550 RVA: 0x00323D4F File Offset: 0x00321F4F
		public override string LocalName
		{
			get
			{
				return "bold";
			}
		}

		// Token: 0x1700720C RID: 29196
		// (get) Token: 0x06015DCF RID: 89551 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700720D RID: 29197
		// (get) Token: 0x06015DD0 RID: 89552 RVA: 0x00323D56 File Offset: 0x00321F56
		internal override int ElementTypeId
		{
			get
			{
				return 12257;
			}
		}

		// Token: 0x06015DD1 RID: 89553 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015DD3 RID: 89555 RVA: 0x00323D5D File Offset: 0x00321F5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BoldFont>(deep);
		}

		// Token: 0x04009529 RID: 38185
		private const string tagName = "bold";

		// Token: 0x0400952A RID: 38186
		private const byte tagNsId = 24;

		// Token: 0x0400952B RID: 38187
		internal const int ElementTypeIdConst = 12257;
	}
}
