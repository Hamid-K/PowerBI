using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200288A RID: 10378
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnOffset : OpenXmlLeafTextElement
	{
		// Token: 0x17006789 RID: 26505
		// (get) Token: 0x060145ED RID: 83437 RVA: 0x00312994 File Offset: 0x00310B94
		public override string LocalName
		{
			get
			{
				return "colOff";
			}
		}

		// Token: 0x1700678A RID: 26506
		// (get) Token: 0x060145EE RID: 83438 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700678B RID: 26507
		// (get) Token: 0x060145EF RID: 83439 RVA: 0x0031299B File Offset: 0x00310B9B
		internal override int ElementTypeId
		{
			get
			{
				return 10740;
			}
		}

		// Token: 0x060145F0 RID: 83440 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060145F1 RID: 83441 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ColumnOffset()
		{
		}

		// Token: 0x060145F2 RID: 83442 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ColumnOffset(string text)
			: base(text)
		{
		}

		// Token: 0x060145F3 RID: 83443 RVA: 0x003129A4 File Offset: 0x00310BA4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int64Value
			{
				InnerText = text
			};
		}

		// Token: 0x060145F4 RID: 83444 RVA: 0x003129BF File Offset: 0x00310BBF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnOffset>(deep);
		}

		// Token: 0x04008DC9 RID: 36297
		private const string tagName = "colOff";

		// Token: 0x04008DCA RID: 36298
		private const byte tagNsId = 18;

		// Token: 0x04008DCB RID: 36299
		internal const int ElementTypeIdConst = 10740;
	}
}
