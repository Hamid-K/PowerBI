using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A8 RID: 8872
	[GeneratedCode("DomGen", "2.0")]
	internal class CompanyFaxNumber : OpenXmlLeafTextElement
	{
		// Token: 0x17004125 RID: 16677
		// (get) Token: 0x0600F0B3 RID: 61619 RVA: 0x002D0DA0 File Offset: 0x002CEFA0
		public override string LocalName
		{
			get
			{
				return "CompanyFax";
			}
		}

		// Token: 0x17004126 RID: 16678
		// (get) Token: 0x0600F0B4 RID: 61620 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x17004127 RID: 16679
		// (get) Token: 0x0600F0B5 RID: 61621 RVA: 0x002D0DA7 File Offset: 0x002CEFA7
		internal override int ElementTypeId
		{
			get
			{
				return 12626;
			}
		}

		// Token: 0x0600F0B6 RID: 61622 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0B7 RID: 61623 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CompanyFaxNumber()
		{
		}

		// Token: 0x0600F0B8 RID: 61624 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CompanyFaxNumber(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0B9 RID: 61625 RVA: 0x002D0DB0 File Offset: 0x002CEFB0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0BA RID: 61626 RVA: 0x002D0DCB File Offset: 0x002CEFCB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompanyFaxNumber>(deep);
		}

		// Token: 0x0400708E RID: 28814
		private const string tagName = "CompanyFax";

		// Token: 0x0400708F RID: 28815
		private const byte tagNsId = 36;

		// Token: 0x04007090 RID: 28816
		internal const int ElementTypeIdConst = 12626;
	}
}
