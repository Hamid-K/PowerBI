using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F9 RID: 8697
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollBarPageIncrement : OpenXmlLeafTextElement
	{
		// Token: 0x170037F8 RID: 14328
		// (get) Token: 0x0600DD7A RID: 56698 RVA: 0x002BD33C File Offset: 0x002BB53C
		public override string LocalName
		{
			get
			{
				return "Page";
			}
		}

		// Token: 0x170037F9 RID: 14329
		// (get) Token: 0x0600DD7B RID: 56699 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037FA RID: 14330
		// (get) Token: 0x0600DD7C RID: 56700 RVA: 0x002BD343 File Offset: 0x002BB543
		internal override int ElementTypeId
		{
			get
			{
				return 12489;
			}
		}

		// Token: 0x0600DD7D RID: 56701 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD7E RID: 56702 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScrollBarPageIncrement()
		{
		}

		// Token: 0x0600DD7F RID: 56703 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScrollBarPageIncrement(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD80 RID: 56704 RVA: 0x002BD34C File Offset: 0x002BB54C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD81 RID: 56705 RVA: 0x002BD367 File Offset: 0x002BB567
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollBarPageIncrement>(deep);
		}

		// Token: 0x04006D1F RID: 27935
		private const string tagName = "Page";

		// Token: 0x04006D20 RID: 27936
		private const byte tagNsId = 29;

		// Token: 0x04006D21 RID: 27937
		internal const int ElementTypeIdConst = 12489;
	}
}
