using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021DD RID: 8669
	[GeneratedCode("DomGen", "2.0")]
	internal class HorizontalTextAlignment : OpenXmlLeafTextElement
	{
		// Token: 0x170037A4 RID: 14244
		// (get) Token: 0x0600DC9A RID: 56474 RVA: 0x002BCD8C File Offset: 0x002BAF8C
		public override string LocalName
		{
			get
			{
				return "TextHAlign";
			}
		}

		// Token: 0x170037A5 RID: 14245
		// (get) Token: 0x0600DC9B RID: 56475 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037A6 RID: 14246
		// (get) Token: 0x0600DC9C RID: 56476 RVA: 0x002BCD93 File Offset: 0x002BAF93
		internal override int ElementTypeId
		{
			get
			{
				return 12448;
			}
		}

		// Token: 0x0600DC9D RID: 56477 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC9E RID: 56478 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HorizontalTextAlignment()
		{
		}

		// Token: 0x0600DC9F RID: 56479 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HorizontalTextAlignment(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCA0 RID: 56480 RVA: 0x002BCD9C File Offset: 0x002BAF9C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCA1 RID: 56481 RVA: 0x002BCDB7 File Offset: 0x002BAFB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HorizontalTextAlignment>(deep);
		}

		// Token: 0x04006CCB RID: 27851
		private const string tagName = "TextHAlign";

		// Token: 0x04006CCC RID: 27852
		private const byte tagNsId = 29;

		// Token: 0x04006CCD RID: 27853
		internal const int ElementTypeIdConst = 12448;
	}
}
