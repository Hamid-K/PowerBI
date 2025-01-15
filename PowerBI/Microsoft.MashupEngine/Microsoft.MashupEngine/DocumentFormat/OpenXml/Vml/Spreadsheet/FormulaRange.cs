using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021DF RID: 8671
	[GeneratedCode("DomGen", "2.0")]
	internal class FormulaRange : OpenXmlLeafTextElement
	{
		// Token: 0x170037AA RID: 14250
		// (get) Token: 0x0600DCAA RID: 56490 RVA: 0x002BCDF4 File Offset: 0x002BAFF4
		public override string LocalName
		{
			get
			{
				return "FmlaRange";
			}
		}

		// Token: 0x170037AB RID: 14251
		// (get) Token: 0x0600DCAB RID: 56491 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037AC RID: 14252
		// (get) Token: 0x0600DCAC RID: 56492 RVA: 0x002BCDFB File Offset: 0x002BAFFB
		internal override int ElementTypeId
		{
			get
			{
				return 12468;
			}
		}

		// Token: 0x0600DCAD RID: 56493 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCAE RID: 56494 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormulaRange()
		{
		}

		// Token: 0x0600DCAF RID: 56495 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormulaRange(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCB0 RID: 56496 RVA: 0x002BCE04 File Offset: 0x002BB004
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCB1 RID: 56497 RVA: 0x002BCE1F File Offset: 0x002BB01F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormulaRange>(deep);
		}

		// Token: 0x04006CD1 RID: 27857
		private const string tagName = "FmlaRange";

		// Token: 0x04006CD2 RID: 27858
		private const byte tagNsId = 29;

		// Token: 0x04006CD3 RID: 27859
		internal const int ElementTypeIdConst = 12468;
	}
}
