using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F4 RID: 8692
	[GeneratedCode("DomGen", "2.0")]
	internal class Checked : OpenXmlLeafTextElement
	{
		// Token: 0x170037E9 RID: 14313
		// (get) Token: 0x0600DD52 RID: 56658 RVA: 0x002BD238 File Offset: 0x002BB438
		public override string LocalName
		{
			get
			{
				return "Checked";
			}
		}

		// Token: 0x170037EA RID: 14314
		// (get) Token: 0x0600DD53 RID: 56659 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037EB RID: 14315
		// (get) Token: 0x0600DD54 RID: 56660 RVA: 0x002BD23F File Offset: 0x002BB43F
		internal override int ElementTypeId
		{
			get
			{
				return 12479;
			}
		}

		// Token: 0x0600DD55 RID: 56661 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD56 RID: 56662 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Checked()
		{
		}

		// Token: 0x0600DD57 RID: 56663 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Checked(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD58 RID: 56664 RVA: 0x002BD248 File Offset: 0x002BB448
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD59 RID: 56665 RVA: 0x002BD263 File Offset: 0x002BB463
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Checked>(deep);
		}

		// Token: 0x04006D10 RID: 27920
		private const string tagName = "Checked";

		// Token: 0x04006D11 RID: 27921
		private const byte tagNsId = 29;

		// Token: 0x04006D12 RID: 27922
		internal const int ElementTypeIdConst = 12479;
	}
}
