using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E5 RID: 8677
	[GeneratedCode("DomGen", "2.0")]
	internal class FormulaLink : OpenXmlLeafTextElement
	{
		// Token: 0x170037BC RID: 14268
		// (get) Token: 0x0600DCDA RID: 56538 RVA: 0x002BCF2C File Offset: 0x002BB12C
		public override string LocalName
		{
			get
			{
				return "FmlaLink";
			}
		}

		// Token: 0x170037BD RID: 14269
		// (get) Token: 0x0600DCDB RID: 56539 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037BE RID: 14270
		// (get) Token: 0x0600DCDC RID: 56540 RVA: 0x002BCF33 File Offset: 0x002BB133
		internal override int ElementTypeId
		{
			get
			{
				return 12480;
			}
		}

		// Token: 0x0600DCDD RID: 56541 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCDE RID: 56542 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormulaLink()
		{
		}

		// Token: 0x0600DCDF RID: 56543 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormulaLink(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCE0 RID: 56544 RVA: 0x002BCF3C File Offset: 0x002BB13C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCE1 RID: 56545 RVA: 0x002BCF57 File Offset: 0x002BB157
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormulaLink>(deep);
		}

		// Token: 0x04006CE3 RID: 27875
		private const string tagName = "FmlaLink";

		// Token: 0x04006CE4 RID: 27876
		private const byte tagNsId = 29;

		// Token: 0x04006CE5 RID: 27877
		internal const int ElementTypeIdConst = 12480;
	}
}
