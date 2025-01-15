using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F7 RID: 8695
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollBarMax : OpenXmlLeafTextElement
	{
		// Token: 0x170037F2 RID: 14322
		// (get) Token: 0x0600DD6A RID: 56682 RVA: 0x002BD2D4 File Offset: 0x002BB4D4
		public override string LocalName
		{
			get
			{
				return "Max";
			}
		}

		// Token: 0x170037F3 RID: 14323
		// (get) Token: 0x0600DD6B RID: 56683 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037F4 RID: 14324
		// (get) Token: 0x0600DD6C RID: 56684 RVA: 0x002BD2DB File Offset: 0x002BB4DB
		internal override int ElementTypeId
		{
			get
			{
				return 12487;
			}
		}

		// Token: 0x0600DD6D RID: 56685 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD6E RID: 56686 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScrollBarMax()
		{
		}

		// Token: 0x0600DD6F RID: 56687 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScrollBarMax(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD70 RID: 56688 RVA: 0x002BD2E4 File Offset: 0x002BB4E4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD71 RID: 56689 RVA: 0x002BD2FF File Offset: 0x002BB4FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollBarMax>(deep);
		}

		// Token: 0x04006D19 RID: 27929
		private const string tagName = "Max";

		// Token: 0x04006D1A RID: 27930
		private const byte tagNsId = 29;

		// Token: 0x04006D1B RID: 27931
		internal const int ElementTypeIdConst = 12487;
	}
}
