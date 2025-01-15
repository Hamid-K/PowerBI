using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A9 RID: 8873
	[GeneratedCode("DomGen", "2.0")]
	internal class CompanyEmailAddress : OpenXmlLeafTextElement
	{
		// Token: 0x17004128 RID: 16680
		// (get) Token: 0x0600F0BB RID: 61627 RVA: 0x002D0DD4 File Offset: 0x002CEFD4
		public override string LocalName
		{
			get
			{
				return "CompanyEmail";
			}
		}

		// Token: 0x17004129 RID: 16681
		// (get) Token: 0x0600F0BC RID: 61628 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x1700412A RID: 16682
		// (get) Token: 0x0600F0BD RID: 61629 RVA: 0x002D0DDB File Offset: 0x002CEFDB
		internal override int ElementTypeId
		{
			get
			{
				return 12627;
			}
		}

		// Token: 0x0600F0BE RID: 61630 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0BF RID: 61631 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CompanyEmailAddress()
		{
		}

		// Token: 0x0600F0C0 RID: 61632 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CompanyEmailAddress(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0C1 RID: 61633 RVA: 0x002D0DE4 File Offset: 0x002CEFE4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0C2 RID: 61634 RVA: 0x002D0DFF File Offset: 0x002CEFFF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompanyEmailAddress>(deep);
		}

		// Token: 0x04007091 RID: 28817
		private const string tagName = "CompanyEmail";

		// Token: 0x04007092 RID: 28818
		private const byte tagNsId = 36;

		// Token: 0x04007093 RID: 28819
		internal const int ElementTypeIdConst = 12627;
	}
}
