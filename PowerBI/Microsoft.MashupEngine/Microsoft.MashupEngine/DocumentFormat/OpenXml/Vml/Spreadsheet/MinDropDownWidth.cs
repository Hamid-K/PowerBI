using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F1 RID: 8689
	[GeneratedCode("DomGen", "2.0")]
	internal class MinDropDownWidth : OpenXmlLeafTextElement
	{
		// Token: 0x170037E0 RID: 14304
		// (get) Token: 0x0600DD3A RID: 56634 RVA: 0x002BD19C File Offset: 0x002BB39C
		public override string LocalName
		{
			get
			{
				return "WidthMin";
			}
		}

		// Token: 0x170037E1 RID: 14305
		// (get) Token: 0x0600DD3B RID: 56635 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037E2 RID: 14306
		// (get) Token: 0x0600DD3C RID: 56636 RVA: 0x002BD1A3 File Offset: 0x002BB3A3
		internal override int ElementTypeId
		{
			get
			{
				return 12469;
			}
		}

		// Token: 0x0600DD3D RID: 56637 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD3E RID: 56638 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MinDropDownWidth()
		{
		}

		// Token: 0x0600DD3F RID: 56639 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MinDropDownWidth(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD40 RID: 56640 RVA: 0x002BD1AC File Offset: 0x002BB3AC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD41 RID: 56641 RVA: 0x002BD1C7 File Offset: 0x002BB3C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinDropDownWidth>(deep);
		}

		// Token: 0x04006D07 RID: 27911
		private const string tagName = "WidthMin";

		// Token: 0x04006D08 RID: 27912
		private const byte tagNsId = 29;

		// Token: 0x04006D09 RID: 27913
		internal const int ElementTypeIdConst = 12469;
	}
}
