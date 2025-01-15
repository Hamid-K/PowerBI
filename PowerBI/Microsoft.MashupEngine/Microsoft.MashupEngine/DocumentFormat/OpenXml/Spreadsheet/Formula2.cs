using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B40 RID: 11072
	[GeneratedCode("DomGen", "2.0")]
	internal class Formula2 : XstringType
	{
		// Token: 0x170077A7 RID: 30631
		// (get) Token: 0x06016AA3 RID: 92835 RVA: 0x002E7D92 File Offset: 0x002E5F92
		public override string LocalName
		{
			get
			{
				return "formula2";
			}
		}

		// Token: 0x170077A8 RID: 30632
		// (get) Token: 0x06016AA4 RID: 92836 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077A9 RID: 30633
		// (get) Token: 0x06016AA5 RID: 92837 RVA: 0x0032DA58 File Offset: 0x0032BC58
		internal override int ElementTypeId
		{
			get
			{
				return 11230;
			}
		}

		// Token: 0x06016AA6 RID: 92838 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016AA7 RID: 92839 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public Formula2()
		{
		}

		// Token: 0x06016AA8 RID: 92840 RVA: 0x0032D835 File Offset: 0x0032BA35
		public Formula2(string text)
			: base(text)
		{
		}

		// Token: 0x06016AA9 RID: 92841 RVA: 0x0032DA60 File Offset: 0x0032BC60
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016AAA RID: 92842 RVA: 0x0032DA7B File Offset: 0x0032BC7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formula2>(deep);
		}

		// Token: 0x04009980 RID: 39296
		private const string tagName = "formula2";

		// Token: 0x04009981 RID: 39297
		private const byte tagNsId = 22;

		// Token: 0x04009982 RID: 39298
		internal const int ElementTypeIdConst = 11230;
	}
}
