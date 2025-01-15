using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B36 RID: 11062
	[GeneratedCode("DomGen", "2.0")]
	internal class CellValue : XstringType
	{
		// Token: 0x17007789 RID: 30601
		// (get) Token: 0x06016A53 RID: 92755 RVA: 0x002F33CF File Offset: 0x002F15CF
		public override string LocalName
		{
			get
			{
				return "v";
			}
		}

		// Token: 0x1700778A RID: 30602
		// (get) Token: 0x06016A54 RID: 92756 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700778B RID: 30603
		// (get) Token: 0x06016A55 RID: 92757 RVA: 0x0032D890 File Offset: 0x0032BA90
		internal override int ElementTypeId
		{
			get
			{
				return 11179;
			}
		}

		// Token: 0x06016A56 RID: 92758 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A57 RID: 92759 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public CellValue()
		{
		}

		// Token: 0x06016A58 RID: 92760 RVA: 0x0032D835 File Offset: 0x0032BA35
		public CellValue(string text)
			: base(text)
		{
		}

		// Token: 0x06016A59 RID: 92761 RVA: 0x0032D898 File Offset: 0x0032BA98
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A5A RID: 92762 RVA: 0x0032D8B3 File Offset: 0x0032BAB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellValue>(deep);
		}

		// Token: 0x04009962 RID: 39266
		private const string tagName = "v";

		// Token: 0x04009963 RID: 39267
		private const byte tagNsId = 22;

		// Token: 0x04009964 RID: 39268
		internal const int ElementTypeIdConst = 11179;
	}
}
