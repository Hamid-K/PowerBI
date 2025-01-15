using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A7 RID: 8871
	[GeneratedCode("DomGen", "2.0")]
	internal class CompanyPhoneNumber : OpenXmlLeafTextElement
	{
		// Token: 0x17004122 RID: 16674
		// (get) Token: 0x0600F0AB RID: 61611 RVA: 0x002D0D6C File Offset: 0x002CEF6C
		public override string LocalName
		{
			get
			{
				return "CompanyPhone";
			}
		}

		// Token: 0x17004123 RID: 16675
		// (get) Token: 0x0600F0AC RID: 61612 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x17004124 RID: 16676
		// (get) Token: 0x0600F0AD RID: 61613 RVA: 0x002D0D73 File Offset: 0x002CEF73
		internal override int ElementTypeId
		{
			get
			{
				return 12625;
			}
		}

		// Token: 0x0600F0AE RID: 61614 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0AF RID: 61615 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CompanyPhoneNumber()
		{
		}

		// Token: 0x0600F0B0 RID: 61616 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CompanyPhoneNumber(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0B1 RID: 61617 RVA: 0x002D0D7C File Offset: 0x002CEF7C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0B2 RID: 61618 RVA: 0x002D0D97 File Offset: 0x002CEF97
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompanyPhoneNumber>(deep);
		}

		// Token: 0x0400708B RID: 28811
		private const string tagName = "CompanyPhone";

		// Token: 0x0400708C RID: 28812
		private const byte tagNsId = 36;

		// Token: 0x0400708D RID: 28813
		internal const int ElementTypeIdConst = 12625;
	}
}
