using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D5 RID: 8661
	[GeneratedCode("DomGen", "2.0")]
	internal class HorizontalScrollBar : OpenXmlLeafTextElement
	{
		// Token: 0x1700378C RID: 14220
		// (get) Token: 0x0600DC5A RID: 56410 RVA: 0x002BCBEC File Offset: 0x002BADEC
		public override string LocalName
		{
			get
			{
				return "Horiz";
			}
		}

		// Token: 0x1700378D RID: 14221
		// (get) Token: 0x0600DC5B RID: 56411 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700378E RID: 14222
		// (get) Token: 0x0600DC5C RID: 56412 RVA: 0x002BCBF3 File Offset: 0x002BADF3
		internal override int ElementTypeId
		{
			get
			{
				return 12490;
			}
		}

		// Token: 0x0600DC5D RID: 56413 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC5E RID: 56414 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HorizontalScrollBar()
		{
		}

		// Token: 0x0600DC5F RID: 56415 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HorizontalScrollBar(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC60 RID: 56416 RVA: 0x002BCBFC File Offset: 0x002BADFC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC61 RID: 56417 RVA: 0x002BCC17 File Offset: 0x002BAE17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HorizontalScrollBar>(deep);
		}

		// Token: 0x04006CB3 RID: 27827
		private const string tagName = "Horiz";

		// Token: 0x04006CB4 RID: 27828
		private const byte tagNsId = 29;

		// Token: 0x04006CB5 RID: 27829
		internal const int ElementTypeIdConst = 12490;
	}
}
