using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C6B RID: 11371
	[GeneratedCode("DomGen", "2.0")]
	internal class RightBorder : BorderPropertiesType
	{
		// Token: 0x170082BC RID: 33468
		// (get) Token: 0x060182D7 RID: 99031 RVA: 0x002BF396 File Offset: 0x002BD596
		public override string LocalName
		{
			get
			{
				return "right";
			}
		}

		// Token: 0x170082BD RID: 33469
		// (get) Token: 0x060182D8 RID: 99032 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082BE RID: 33470
		// (get) Token: 0x060182D9 RID: 99033 RVA: 0x0033F329 File Offset: 0x0033D529
		internal override int ElementTypeId
		{
			get
			{
				return 11351;
			}
		}

		// Token: 0x060182DA RID: 99034 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060182DB RID: 99035 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public RightBorder()
		{
		}

		// Token: 0x060182DC RID: 99036 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public RightBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182DD RID: 99037 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public RightBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182DE RID: 99038 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public RightBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182DF RID: 99039 RVA: 0x0033F330 File Offset: 0x0033D530
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightBorder>(deep);
		}

		// Token: 0x04009F2F RID: 40751
		private const string tagName = "right";

		// Token: 0x04009F30 RID: 40752
		private const byte tagNsId = 22;

		// Token: 0x04009F31 RID: 40753
		internal const int ElementTypeIdConst = 11351;
	}
}
