using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B3F RID: 11071
	[GeneratedCode("DomGen", "2.0")]
	internal class Formula1 : XstringType
	{
		// Token: 0x170077A4 RID: 30628
		// (get) Token: 0x06016A9B RID: 92827 RVA: 0x002E7D58 File Offset: 0x002E5F58
		public override string LocalName
		{
			get
			{
				return "formula1";
			}
		}

		// Token: 0x170077A5 RID: 30629
		// (get) Token: 0x06016A9C RID: 92828 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077A6 RID: 30630
		// (get) Token: 0x06016A9D RID: 92829 RVA: 0x0032DA2C File Offset: 0x0032BC2C
		internal override int ElementTypeId
		{
			get
			{
				return 11229;
			}
		}

		// Token: 0x06016A9E RID: 92830 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A9F RID: 92831 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public Formula1()
		{
		}

		// Token: 0x06016AA0 RID: 92832 RVA: 0x0032D835 File Offset: 0x0032BA35
		public Formula1(string text)
			: base(text)
		{
		}

		// Token: 0x06016AA1 RID: 92833 RVA: 0x0032DA34 File Offset: 0x0032BC34
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016AA2 RID: 92834 RVA: 0x0032DA4F File Offset: 0x0032BC4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formula1>(deep);
		}

		// Token: 0x0400997D RID: 39293
		private const string tagName = "formula1";

		// Token: 0x0400997E RID: 39294
		private const byte tagNsId = 22;

		// Token: 0x0400997F RID: 39295
		internal const int ElementTypeIdConst = 11229;
	}
}
