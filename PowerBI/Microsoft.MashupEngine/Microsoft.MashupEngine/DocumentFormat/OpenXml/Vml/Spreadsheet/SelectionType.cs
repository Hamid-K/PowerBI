using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E0 RID: 8672
	[GeneratedCode("DomGen", "2.0")]
	internal class SelectionType : OpenXmlLeafTextElement
	{
		// Token: 0x170037AD RID: 14253
		// (get) Token: 0x0600DCB2 RID: 56498 RVA: 0x002BCE28 File Offset: 0x002BB028
		public override string LocalName
		{
			get
			{
				return "SelType";
			}
		}

		// Token: 0x170037AE RID: 14254
		// (get) Token: 0x0600DCB3 RID: 56499 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037AF RID: 14255
		// (get) Token: 0x0600DCB4 RID: 56500 RVA: 0x002BCE2F File Offset: 0x002BB02F
		internal override int ElementTypeId
		{
			get
			{
				return 12472;
			}
		}

		// Token: 0x0600DCB5 RID: 56501 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCB6 RID: 56502 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public SelectionType()
		{
		}

		// Token: 0x0600DCB7 RID: 56503 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public SelectionType(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCB8 RID: 56504 RVA: 0x002BCE38 File Offset: 0x002BB038
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCB9 RID: 56505 RVA: 0x002BCE53 File Offset: 0x002BB053
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SelectionType>(deep);
		}

		// Token: 0x04006CD4 RID: 27860
		private const string tagName = "SelType";

		// Token: 0x04006CD5 RID: 27861
		private const byte tagNsId = 29;

		// Token: 0x04006CD6 RID: 27862
		internal const int ElementTypeIdConst = 12472;
	}
}
