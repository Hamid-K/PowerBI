using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C6A RID: 11370
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftBorder : BorderPropertiesType
	{
		// Token: 0x170082B9 RID: 33465
		// (get) Token: 0x060182CE RID: 99022 RVA: 0x002BF360 File Offset: 0x002BD560
		public override string LocalName
		{
			get
			{
				return "left";
			}
		}

		// Token: 0x170082BA RID: 33466
		// (get) Token: 0x060182CF RID: 99023 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082BB RID: 33467
		// (get) Token: 0x060182D0 RID: 99024 RVA: 0x0033F319 File Offset: 0x0033D519
		internal override int ElementTypeId
		{
			get
			{
				return 11350;
			}
		}

		// Token: 0x060182D1 RID: 99025 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060182D2 RID: 99026 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public LeftBorder()
		{
		}

		// Token: 0x060182D3 RID: 99027 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public LeftBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182D4 RID: 99028 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public LeftBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182D5 RID: 99029 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public LeftBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182D6 RID: 99030 RVA: 0x0033F320 File Offset: 0x0033D520
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftBorder>(deep);
		}

		// Token: 0x04009F2C RID: 40748
		private const string tagName = "left";

		// Token: 0x04009F2D RID: 40749
		private const byte tagNsId = 22;

		// Token: 0x04009F2E RID: 40750
		internal const int ElementTypeIdConst = 11350;
	}
}
