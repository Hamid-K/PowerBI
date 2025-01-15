using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021BF RID: 8639
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintObject : OpenXmlLeafTextElement
	{
		// Token: 0x1700374A RID: 14154
		// (get) Token: 0x0600DBAA RID: 56234 RVA: 0x002BC774 File Offset: 0x002BA974
		public override string LocalName
		{
			get
			{
				return "PrintObject";
			}
		}

		// Token: 0x1700374B RID: 14155
		// (get) Token: 0x0600DBAB RID: 56235 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700374C RID: 14156
		// (get) Token: 0x0600DBAC RID: 56236 RVA: 0x002BC77B File Offset: 0x002BA97B
		internal override int ElementTypeId
		{
			get
			{
				return 12442;
			}
		}

		// Token: 0x0600DBAD RID: 56237 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBAE RID: 56238 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PrintObject()
		{
		}

		// Token: 0x0600DBAF RID: 56239 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PrintObject(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBB0 RID: 56240 RVA: 0x002BC784 File Offset: 0x002BA984
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBB1 RID: 56241 RVA: 0x002BC79F File Offset: 0x002BA99F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintObject>(deep);
		}

		// Token: 0x04006C71 RID: 27761
		private const string tagName = "PrintObject";

		// Token: 0x04006C72 RID: 27762
		private const byte tagNsId = 29;

		// Token: 0x04006C73 RID: 27763
		internal const int ElementTypeIdConst = 12442;
	}
}
