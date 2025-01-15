using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C5 RID: 8645
	[GeneratedCode("DomGen", "2.0")]
	internal class JustifyLastLine : OpenXmlLeafTextElement
	{
		// Token: 0x1700375C RID: 14172
		// (get) Token: 0x0600DBDA RID: 56282 RVA: 0x002BC8AC File Offset: 0x002BAAAC
		public override string LocalName
		{
			get
			{
				return "JustLastX";
			}
		}

		// Token: 0x1700375D RID: 14173
		// (get) Token: 0x0600DBDB RID: 56283 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700375E RID: 14174
		// (get) Token: 0x0600DBDC RID: 56284 RVA: 0x002BC8B3 File Offset: 0x002BAAB3
		internal override int ElementTypeId
		{
			get
			{
				return 12451;
			}
		}

		// Token: 0x0600DBDD RID: 56285 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBDE RID: 56286 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public JustifyLastLine()
		{
		}

		// Token: 0x0600DBDF RID: 56287 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public JustifyLastLine(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBE0 RID: 56288 RVA: 0x002BC8BC File Offset: 0x002BAABC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBE1 RID: 56289 RVA: 0x002BC8D7 File Offset: 0x002BAAD7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<JustifyLastLine>(deep);
		}

		// Token: 0x04006C83 RID: 27779
		private const string tagName = "JustLastX";

		// Token: 0x04006C84 RID: 27780
		private const byte tagNsId = 29;

		// Token: 0x04006C85 RID: 27781
		internal const int ElementTypeIdConst = 12451;
	}
}
