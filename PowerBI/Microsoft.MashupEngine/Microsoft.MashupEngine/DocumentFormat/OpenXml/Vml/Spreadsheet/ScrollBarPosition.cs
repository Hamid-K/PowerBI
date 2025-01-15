using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F5 RID: 8693
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollBarPosition : OpenXmlLeafTextElement
	{
		// Token: 0x170037EC RID: 14316
		// (get) Token: 0x0600DD5A RID: 56666 RVA: 0x002BD26C File Offset: 0x002BB46C
		public override string LocalName
		{
			get
			{
				return "Val";
			}
		}

		// Token: 0x170037ED RID: 14317
		// (get) Token: 0x0600DD5B RID: 56667 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037EE RID: 14318
		// (get) Token: 0x0600DD5C RID: 56668 RVA: 0x002BD273 File Offset: 0x002BB473
		internal override int ElementTypeId
		{
			get
			{
				return 12485;
			}
		}

		// Token: 0x0600DD5D RID: 56669 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD5E RID: 56670 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScrollBarPosition()
		{
		}

		// Token: 0x0600DD5F RID: 56671 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScrollBarPosition(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD60 RID: 56672 RVA: 0x002BD27C File Offset: 0x002BB47C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD61 RID: 56673 RVA: 0x002BD297 File Offset: 0x002BB497
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollBarPosition>(deep);
		}

		// Token: 0x04006D13 RID: 27923
		private const string tagName = "Val";

		// Token: 0x04006D14 RID: 27924
		private const byte tagNsId = 29;

		// Token: 0x04006D15 RID: 27925
		internal const int ElementTypeIdConst = 12485;
	}
}
