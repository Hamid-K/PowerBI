using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A58 RID: 10840
	[GeneratedCode("DomGen", "2.0")]
	internal class ItalicFont : EmbeddedFontDataIdType
	{
		// Token: 0x1700720E RID: 29198
		// (get) Token: 0x06015DD4 RID: 89556 RVA: 0x00323D66 File Offset: 0x00321F66
		public override string LocalName
		{
			get
			{
				return "italic";
			}
		}

		// Token: 0x1700720F RID: 29199
		// (get) Token: 0x06015DD5 RID: 89557 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007210 RID: 29200
		// (get) Token: 0x06015DD6 RID: 89558 RVA: 0x00323D6D File Offset: 0x00321F6D
		internal override int ElementTypeId
		{
			get
			{
				return 12258;
			}
		}

		// Token: 0x06015DD7 RID: 89559 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015DD9 RID: 89561 RVA: 0x00323D74 File Offset: 0x00321F74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ItalicFont>(deep);
		}

		// Token: 0x0400952C RID: 38188
		private const string tagName = "italic";

		// Token: 0x0400952D RID: 38189
		private const byte tagNsId = 24;

		// Token: 0x0400952E RID: 38190
		internal const int ElementTypeIdConst = 12258;
	}
}
