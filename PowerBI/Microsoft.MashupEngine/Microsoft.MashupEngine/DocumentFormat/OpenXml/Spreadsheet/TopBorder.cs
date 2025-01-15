using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C6C RID: 11372
	[GeneratedCode("DomGen", "2.0")]
	internal class TopBorder : BorderPropertiesType
	{
		// Token: 0x170082BF RID: 33471
		// (get) Token: 0x060182E0 RID: 99040 RVA: 0x002BF37F File Offset: 0x002BD57F
		public override string LocalName
		{
			get
			{
				return "top";
			}
		}

		// Token: 0x170082C0 RID: 33472
		// (get) Token: 0x060182E1 RID: 99041 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082C1 RID: 33473
		// (get) Token: 0x060182E2 RID: 99042 RVA: 0x0033F339 File Offset: 0x0033D539
		internal override int ElementTypeId
		{
			get
			{
				return 11352;
			}
		}

		// Token: 0x060182E3 RID: 99043 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060182E4 RID: 99044 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public TopBorder()
		{
		}

		// Token: 0x060182E5 RID: 99045 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public TopBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182E6 RID: 99046 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public TopBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182E7 RID: 99047 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public TopBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182E8 RID: 99048 RVA: 0x0033F340 File Offset: 0x0033D540
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopBorder>(deep);
		}

		// Token: 0x04009F32 RID: 40754
		private const string tagName = "top";

		// Token: 0x04009F33 RID: 40755
		private const byte tagNsId = 22;

		// Token: 0x04009F34 RID: 40756
		internal const int ElementTypeIdConst = 11352;
	}
}
