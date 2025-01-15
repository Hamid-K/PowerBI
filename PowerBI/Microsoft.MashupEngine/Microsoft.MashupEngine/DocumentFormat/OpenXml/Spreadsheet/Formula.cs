using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B37 RID: 11063
	[GeneratedCode("DomGen", "2.0")]
	internal class Formula : XstringType
	{
		// Token: 0x1700778C RID: 30604
		// (get) Token: 0x06016A5B RID: 92763 RVA: 0x0032D8BC File Offset: 0x0032BABC
		public override string LocalName
		{
			get
			{
				return "formula";
			}
		}

		// Token: 0x1700778D RID: 30605
		// (get) Token: 0x06016A5C RID: 92764 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700778E RID: 30606
		// (get) Token: 0x06016A5D RID: 92765 RVA: 0x0032D8C3 File Offset: 0x0032BAC3
		internal override int ElementTypeId
		{
			get
			{
				return 11181;
			}
		}

		// Token: 0x06016A5E RID: 92766 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A5F RID: 92767 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public Formula()
		{
		}

		// Token: 0x06016A60 RID: 92768 RVA: 0x0032D835 File Offset: 0x0032BA35
		public Formula(string text)
			: base(text)
		{
		}

		// Token: 0x06016A61 RID: 92769 RVA: 0x0032D8CC File Offset: 0x0032BACC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A62 RID: 92770 RVA: 0x0032D8E7 File Offset: 0x0032BAE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formula>(deep);
		}

		// Token: 0x04009965 RID: 39269
		private const string tagName = "formula";

		// Token: 0x04009966 RID: 39270
		private const byte tagNsId = 22;

		// Token: 0x04009967 RID: 39271
		internal const int ElementTypeIdConst = 11181;
	}
}
