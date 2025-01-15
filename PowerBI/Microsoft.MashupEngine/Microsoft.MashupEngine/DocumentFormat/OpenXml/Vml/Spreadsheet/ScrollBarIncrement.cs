using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F8 RID: 8696
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollBarIncrement : OpenXmlLeafTextElement
	{
		// Token: 0x170037F5 RID: 14325
		// (get) Token: 0x0600DD72 RID: 56690 RVA: 0x002BD308 File Offset: 0x002BB508
		public override string LocalName
		{
			get
			{
				return "Inc";
			}
		}

		// Token: 0x170037F6 RID: 14326
		// (get) Token: 0x0600DD73 RID: 56691 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037F7 RID: 14327
		// (get) Token: 0x0600DD74 RID: 56692 RVA: 0x002BD30F File Offset: 0x002BB50F
		internal override int ElementTypeId
		{
			get
			{
				return 12488;
			}
		}

		// Token: 0x0600DD75 RID: 56693 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD76 RID: 56694 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScrollBarIncrement()
		{
		}

		// Token: 0x0600DD77 RID: 56695 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScrollBarIncrement(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD78 RID: 56696 RVA: 0x002BD318 File Offset: 0x002BB518
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD79 RID: 56697 RVA: 0x002BD333 File Offset: 0x002BB533
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollBarIncrement>(deep);
		}

		// Token: 0x04006D1C RID: 27932
		private const string tagName = "Inc";

		// Token: 0x04006D1D RID: 27933
		private const byte tagNsId = 29;

		// Token: 0x04006D1E RID: 27934
		internal const int ElementTypeIdConst = 12488;
	}
}
