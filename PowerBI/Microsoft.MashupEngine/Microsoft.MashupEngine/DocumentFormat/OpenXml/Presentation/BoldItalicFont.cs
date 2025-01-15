using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A59 RID: 10841
	[GeneratedCode("DomGen", "2.0")]
	internal class BoldItalicFont : EmbeddedFontDataIdType
	{
		// Token: 0x17007211 RID: 29201
		// (get) Token: 0x06015DDA RID: 89562 RVA: 0x00323D7D File Offset: 0x00321F7D
		public override string LocalName
		{
			get
			{
				return "boldItalic";
			}
		}

		// Token: 0x17007212 RID: 29202
		// (get) Token: 0x06015DDB RID: 89563 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007213 RID: 29203
		// (get) Token: 0x06015DDC RID: 89564 RVA: 0x00323D84 File Offset: 0x00321F84
		internal override int ElementTypeId
		{
			get
			{
				return 12259;
			}
		}

		// Token: 0x06015DDD RID: 89565 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015DDF RID: 89567 RVA: 0x00323D8B File Offset: 0x00321F8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BoldItalicFont>(deep);
		}

		// Token: 0x0400952F RID: 38191
		private const string tagName = "boldItalic";

		// Token: 0x04009530 RID: 38192
		private const byte tagNsId = 24;

		// Token: 0x04009531 RID: 38193
		internal const int ElementTypeIdConst = 12259;
	}
}
