using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A56 RID: 10838
	[GeneratedCode("DomGen", "2.0")]
	internal class RegularFont : EmbeddedFontDataIdType
	{
		// Token: 0x17007208 RID: 29192
		// (get) Token: 0x06015DC8 RID: 89544 RVA: 0x00323D30 File Offset: 0x00321F30
		public override string LocalName
		{
			get
			{
				return "regular";
			}
		}

		// Token: 0x17007209 RID: 29193
		// (get) Token: 0x06015DC9 RID: 89545 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700720A RID: 29194
		// (get) Token: 0x06015DCA RID: 89546 RVA: 0x00323D37 File Offset: 0x00321F37
		internal override int ElementTypeId
		{
			get
			{
				return 12256;
			}
		}

		// Token: 0x06015DCB RID: 89547 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015DCD RID: 89549 RVA: 0x00323D46 File Offset: 0x00321F46
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RegularFont>(deep);
		}

		// Token: 0x04009526 RID: 38182
		private const string tagName = "regular";

		// Token: 0x04009527 RID: 38183
		private const byte tagNsId = 24;

		// Token: 0x04009528 RID: 38184
		internal const int ElementTypeIdConst = 12256;
	}
}
