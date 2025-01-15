using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200288B RID: 10379
	[GeneratedCode("DomGen", "2.0")]
	internal class RowOffset : OpenXmlLeafTextElement
	{
		// Token: 0x1700678C RID: 26508
		// (get) Token: 0x060145F5 RID: 83445 RVA: 0x003129C8 File Offset: 0x00310BC8
		public override string LocalName
		{
			get
			{
				return "rowOff";
			}
		}

		// Token: 0x1700678D RID: 26509
		// (get) Token: 0x060145F6 RID: 83446 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700678E RID: 26510
		// (get) Token: 0x060145F7 RID: 83447 RVA: 0x003129CF File Offset: 0x00310BCF
		internal override int ElementTypeId
		{
			get
			{
				return 10742;
			}
		}

		// Token: 0x060145F8 RID: 83448 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060145F9 RID: 83449 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public RowOffset()
		{
		}

		// Token: 0x060145FA RID: 83450 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public RowOffset(string text)
			: base(text)
		{
		}

		// Token: 0x060145FB RID: 83451 RVA: 0x003129D8 File Offset: 0x00310BD8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int64Value
			{
				InnerText = text
			};
		}

		// Token: 0x060145FC RID: 83452 RVA: 0x003129F3 File Offset: 0x00310BF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowOffset>(deep);
		}

		// Token: 0x04008DCC RID: 36300
		private const string tagName = "rowOff";

		// Token: 0x04008DCD RID: 36301
		private const byte tagNsId = 18;

		// Token: 0x04008DCE RID: 36302
		internal const int ElementTypeIdConst = 10742;
	}
}
