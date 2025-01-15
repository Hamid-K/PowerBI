using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A6 RID: 8870
	[GeneratedCode("DomGen", "2.0")]
	internal class CompanyAddress : OpenXmlLeafTextElement
	{
		// Token: 0x1700411F RID: 16671
		// (get) Token: 0x0600F0A3 RID: 61603 RVA: 0x002D0D38 File Offset: 0x002CEF38
		public override string LocalName
		{
			get
			{
				return "CompanyAddress";
			}
		}

		// Token: 0x17004120 RID: 16672
		// (get) Token: 0x0600F0A4 RID: 61604 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x17004121 RID: 16673
		// (get) Token: 0x0600F0A5 RID: 61605 RVA: 0x002D0D3F File Offset: 0x002CEF3F
		internal override int ElementTypeId
		{
			get
			{
				return 12624;
			}
		}

		// Token: 0x0600F0A6 RID: 61606 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0A7 RID: 61607 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CompanyAddress()
		{
		}

		// Token: 0x0600F0A8 RID: 61608 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CompanyAddress(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0A9 RID: 61609 RVA: 0x002D0D48 File Offset: 0x002CEF48
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0AA RID: 61610 RVA: 0x002D0D63 File Offset: 0x002CEF63
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompanyAddress>(deep);
		}

		// Token: 0x04007088 RID: 28808
		private const string tagName = "CompanyAddress";

		// Token: 0x04007089 RID: 28809
		private const byte tagNsId = 36;

		// Token: 0x0400708A RID: 28810
		internal const int ElementTypeIdConst = 12624;
	}
}
