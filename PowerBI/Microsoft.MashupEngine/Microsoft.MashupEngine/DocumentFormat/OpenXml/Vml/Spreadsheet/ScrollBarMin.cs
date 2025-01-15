using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F6 RID: 8694
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollBarMin : OpenXmlLeafTextElement
	{
		// Token: 0x170037EF RID: 14319
		// (get) Token: 0x0600DD62 RID: 56674 RVA: 0x002BD2A0 File Offset: 0x002BB4A0
		public override string LocalName
		{
			get
			{
				return "Min";
			}
		}

		// Token: 0x170037F0 RID: 14320
		// (get) Token: 0x0600DD63 RID: 56675 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037F1 RID: 14321
		// (get) Token: 0x0600DD64 RID: 56676 RVA: 0x002BD2A7 File Offset: 0x002BB4A7
		internal override int ElementTypeId
		{
			get
			{
				return 12486;
			}
		}

		// Token: 0x0600DD65 RID: 56677 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD66 RID: 56678 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScrollBarMin()
		{
		}

		// Token: 0x0600DD67 RID: 56679 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScrollBarMin(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD68 RID: 56680 RVA: 0x002BD2B0 File Offset: 0x002BB4B0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD69 RID: 56681 RVA: 0x002BD2CB File Offset: 0x002BB4CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollBarMin>(deep);
		}

		// Token: 0x04006D16 RID: 27926
		private const string tagName = "Min";

		// Token: 0x04006D17 RID: 27927
		private const byte tagNsId = 29;

		// Token: 0x04006D18 RID: 27928
		internal const int ElementTypeIdConst = 12486;
	}
}
